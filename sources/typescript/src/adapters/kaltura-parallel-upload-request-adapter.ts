import { KalturaUploadRequest } from "../api/kaltura-upload-request";
import { createEndpoint, prepareParameters } from "./utils";
import { KalturaClientException } from "../api/kaltura-client-exception";
import { KalturaRequestOptions } from "../api/kaltura-request-options";
import { KalturaClientOptions } from "../kaltura-client-options";
import { CancelableAction } from "../cancelable-action";
import { KalturaUploadConnectionsManager } from "./kaltura-upload-connections-manager";
import { KalturaUploadRequestAdapter } from "./kaltura-upload-request-adapter";

interface UploadByParallelChunksData {
    chunkUploadEnabled: boolean;

    /**
     * bytes uploaded so far
     */
    loaded: number;

    /**
     * total number of chunks in the current file
     */
    totalChunks: number;

    /**
     * number of bytes per chunk
     */
    chunkSize: number;

    /**
     * number of chunks done uploading
     */
    chunksUploaded: number;

    /**
     * number of the next chunk to start uploading
     */
    nextChunkIndex: number;
}

export class KalturaParallelUploadRequestAdapter extends KalturaUploadRequestAdapter {

    constructor(clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions) {
        super(clientOptions, defaultRequestOptions);
        if (clientOptions.parallelUploadsDisabled) {
            return;
        }
        if (KalturaUploadConnectionsManager.getTotalConnections() === -1) {
            // initialize manager
            KalturaUploadConnectionsManager.setTotalConnections(clientOptions.maxConcurrentUploadConnections || 6);
        }
    }

    transmit(request: KalturaUploadRequest<any>): CancelableAction<any> {
        return new CancelableAction((resolve, reject, action) => {
            const { file} = request.getFileInfo();
            const chunkSize = this._getChunkSize();

            const data: UploadByParallelChunksData = {
                chunkUploadEnabled: this._chunkUploadSupported(request),
                loaded: 0,
                totalChunks: Math.ceil(file.size / chunkSize),
                chunkSize: chunkSize,
                chunksUploaded: 0,
                nextChunkIndex: 0,
            };

            let hasListenerRegistered = false;

            let activeAction: CancelableAction<any>;

            const tryUpload = (waitIfNoConnections = true) => {
                if (KalturaUploadConnectionsManager.retrieveConnection()) {
                    console.log("tryUpload - got connection");
                    activeAction = this._uploadChunk(request, data, data.nextChunkIndex)
                        .then(handleChunkUploadSuccess, handleChunkUploadError);
                    data.nextChunkIndex += 1;
                    return true;
                }
                else if (waitIfNoConnections) {
                    console.log("tryUpload - no connections, waiting");
                    KalturaUploadConnectionsManager.addAvailableConnectionsCallback(handleAvailableConnectionNotification);
                    hasListenerRegistered = true;
                }
                return false;
            };

            const handleChunkUploadError = reason => {
                activeAction = null;
                reject(reason);
            };

            const handleChunkUploadSuccess = result => {
                console.log("handleChunkUploadSuccess");
                // "clean up":
                activeAction = null;
                data.chunksUploaded += 1;
                KalturaUploadConnectionsManager.releaseConnection();

                // was this the final chunk?
                const { file} = request.getFileInfo();
                const chunkSize = this._getChunkSize();
                const totalChunks = Math.ceil(file.size / chunkSize);
                const finalChunk = data.chunksUploaded >= totalChunks;
                const hasMoreChunks = data.nextChunkIndex < totalChunks;

                if (!data.chunkUploadEnabled || finalChunk) {
                    this._handleFinalChunkResponse(request, result, reject, resolve);
                }
                else if (hasMoreChunks) {
                    // tryUpload();
                    // TODO if we add as many parts as possible here?

                    // add as many chunks as possible
                    while (true) {
                        if (data.nextChunkIndex >= data.totalChunks) {
                            // all chunks are uploaded / uploading
                            break;
                        }
                        if (!tryUpload(!hasListenerRegistered)) {
                            // no more available connections
                            // only add listener if the adapter doesn't have one yet
                            break;
                        }
                        console.log("chunk added");
                    }
                }
            };

            const handleAvailableConnectionNotification = () => {
                hasListenerRegistered = false;
                if (KalturaUploadConnectionsManager.retrieveConnection()) {
                    activeAction = this._uploadChunk(request, data, data.nextChunkIndex)
                        .then(handleChunkUploadSuccess, handleChunkUploadError);
                    data.nextChunkIndex += 1;
                }
            };

            // // TODO upload first chunk, only after it is done, upload the rest in parallel
            // const uploadFirstChunk = () => {
            //     return new CancelableAction((resolve, reject) => {
            //         if (KalturaUploadConnectionsManager.retrieveConnection()) {
            //             console.log("upload first chunk - got connection");
            //             activeAction = this._uploadChunk(request, data, data.nextChunkIndex);
            //                 // .then(handleChunkUploadSuccess, handleChunkUploadError);
            //             data.nextChunkIndex += 1;
            //             return activeAction;
            //         }
            //         else {
            //             console.log("upload first chunk - no connections, waiting");
            //             KalturaUploadConnectionsManager.addAvailableConnectionsCallback(handleAvailableConnectionNotification);
            //         }
            //     });
            // };
            //
            // uploadFirstChunk().then(() => {
            //     //TODO we actually want the same as the success handler, but with adding all possible chunks
            //     //TODO add as many chunks as possible
            //     while (true) {
            //         if (data.nextChunkIndex >= data.totalChunks) {
            //             // all chunks are uploaded / uploading
            //             break;
            //         }
            //         if (!tryUpload(false)) {
            //             // no more available connections, listener was not added
            //             break;
            //         }
            //         console.log("chunk added");
            //     }
            // }, handleChunkUploadError)

            // // add as many chunks as possible
            // while (true) {
            //     if (data.nextChunkIndex >= data.totalChunks) {
            //         // all chunks are uploaded / uploading
            //         break;
            //     }
            //     if (!tryUpload(false)) {
            //         // no more available connections, listener was not added
            //         break;
            //     }
            //     console.log("chunk added");
            // }
            tryUpload();

            return () => {
                if (activeAction) {
                    activeAction.cancel();
                    activeAction = null;
                }
            };
        });
    }

    /**
     * chunk upload for parallel chunks upload
     * @param request
     * @param uploadChunkData
     * @param chunkIndex
     * @private
     */
    private _uploadChunk(request: KalturaUploadRequest<any>, uploadChunkData: UploadByParallelChunksData, chunkIndex: number): CancelableAction<any> {
        return new CancelableAction((resolve, reject) => {
            const parameters = prepareParameters(request, this.clientOptions, this.defaultRequestOptions);

            let isComplete = false;
            const {propertyName, file} = request.getFileInfo();
            let data = this._getFormData(propertyName, file.name, file);
            console.log("_uploadChunk, chunkIndex ", chunkIndex, file.name);
            let fileStart = 0;

            if (uploadChunkData.chunkUploadEnabled) {
                const chunkSize = uploadChunkData.chunkSize;
                const resumeAt = chunkIndex * chunkSize;
                const finalChunk = (file.size - resumeAt) <= chunkSize;

                fileStart = resumeAt;
                const fileEnd = finalChunk ? file.size : fileStart + chunkSize;

                data = this._getFormData(propertyName, file.name, file.slice(fileStart, fileEnd, file.type));

                parameters.resume = chunkIndex > 0;
                parameters.resumeAt = resumeAt;
                parameters.finalChunk = finalChunk;
            } else {
                console.log(`chunk upload not supported by browser or by request. Uploading the file as-is`);
            }

            // const { service, action, ks, ...queryparams } = parameters;
            const {service, action, ...queryparams} = parameters;
            const endpointUrl = createEndpoint(request, this.clientOptions, service, action, queryparams);

            const xhr = new XMLHttpRequest();

            xhr.onreadystatechange = () => {
                if (xhr.readyState === 4) {
                    if (isComplete) {
                        return;
                    }
                    isComplete = true;
                    let resp;

                    try {
                        if (xhr.status === 200) {
                            resp = JSON.parse(xhr.response);
                        } else {
                            resp = new KalturaClientException("client::upload-failure", xhr.responseText || "failed to upload file");
                        }
                    } catch (e) {
                        resp = new KalturaClientException("client::upload-failure", e.message || "failed to upload file");
                    }

                    if (resp instanceof Error) {
                        reject(resp);
                    } else {
                        if (uploadChunkData.chunkUploadEnabled) {
                            if (typeof resp.uploadedFileSize === "undefined" || resp.uploadedFileSize === null) {
                                reject(new KalturaClientException("client::upload-failure", `uploaded chunk of file failed, expected response with property 'uploadedFileSize'`));
                                return;
                            }
                        }

                        resolve(resp);
                    }
                }
            };

            // const progressCallback = request._getProgressCallback();
            // if (progressCallback) {
            //     xhr.upload.addEventListener("progress", e => {
            //         if (e.lengthComputable) {
            //             progressCallback.apply(request, [e.loaded + fileStart, file.size]);
            //         } else {
            //             // Unable to compute progress information since the total size is unknown
            //         }
            //     }, false);
            // }

            xhr.open("POST", endpointUrl);
            xhr.send(data);

            return () => {
                if (!isComplete) {
                    isComplete = true;
                    xhr.abort();
                }
            };
        });
    }
}
