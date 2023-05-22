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
    uploaded: number;

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

/**
 * Request adapter to allow uploading files in parallel chunks
 */
export class KalturaParallelUploadRequestAdapter extends KalturaUploadRequestAdapter {

    constructor(clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions) {
        super(clientOptions, defaultRequestOptions);
        if (clientOptions.parallelUploadsDisabled) {
            return;
        }
        // initialize manager
        KalturaUploadConnectionsManager.init(clientOptions.maxConcurrentUploadConnections || 6);
    }

    transmit(request: KalturaUploadRequest<any>): CancelableAction<any> {
        return new CancelableAction((resolve, reject, action) => {
            const { file} = request.getFileInfo();
            const chunkSize = this._getChunkSize();

            const data: UploadByParallelChunksData = {
                chunkUploadEnabled: this._chunkUploadSupported(request),
                uploaded: 0,
                totalChunks: Math.ceil(file.size / chunkSize),
                chunkSize: chunkSize,
                chunksUploaded: 0,
                nextChunkIndex: 0,
            };

            let hasListenerRegistered = false;

            let activeActions: Record<string, CancelableAction<any>> = {};

            const tryUpload = (waitIfNoConnections = true) => {
                if (KalturaUploadConnectionsManager.retrieveConnection()) {
                    activeActions[data.nextChunkIndex] = this._uploadChunk(request, data, data.nextChunkIndex)
                        .then(handleChunkUploadSuccess(data.nextChunkIndex), handleChunkUploadError(data.nextChunkIndex));
                    // success and error are bound to chunk index
                    data.nextChunkIndex += 1;
                    return true;
                }
                else if (waitIfNoConnections) {
                    KalturaUploadConnectionsManager.addAvailableConnectionsCallback(handleAvailableConnectionNotification);
                    hasListenerRegistered = true;
                }
                return false;
            };

            const handleChunkUploadError = (chunkIndex: number) => reason => {
                delete activeActions[chunkIndex];
                // cancel all ongoing chunks
                for (let i in activeActions) {
                    activeActions[i].cancel();
                    delete activeActions[i];
                }

                reject(reason);

                // free connections
                if (hasListenerRegistered) {
                    KalturaUploadConnectionsManager.removeAvailableConnectionsCallback(handleAvailableConnectionNotification);
                }
                KalturaUploadConnectionsManager.releaseConnection();
            };

            const handleChunkUploadSuccess = (chunkIndex: number) => result => {
                // "clean up":
                delete activeActions[chunkIndex];
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
                    }
                }
            };

            const handleAvailableConnectionNotification = () => {
                hasListenerRegistered = false;
                if (KalturaUploadConnectionsManager.retrieveConnection()) {
                    activeActions[data.nextChunkIndex] = this._uploadChunk(request, data, data.nextChunkIndex)
                        .then(handleChunkUploadSuccess(data.nextChunkIndex), handleChunkUploadError(data.nextChunkIndex));
                    // success, error are bound to chunk index
                    data.nextChunkIndex += 1;
                }
            };

            tryUpload();

            return () => {
                for (let i in activeActions) {
                    activeActions[i].cancel();
                    delete activeActions[i];
                    // for each, free a connection
                    KalturaUploadConnectionsManager.releaseConnection();
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
            let fileStart = 0;
            let chunkBytesLoaded = 0;

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
            const endpointUrl = createEndpoint(request, this.clientOptions, service, action);

            // "prepend" queryparams before file data
            data = this._prependParametersToFormData(data, queryparams);

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

            const progressCallback = request._getProgressCallback();
            if (progressCallback) {
                xhr.upload.addEventListener("progress", e => {
                    if (e.lengthComputable) {
                        // update the "global" counter: add only what was added on the current "progress"
                        uploadChunkData.uploaded += (e.loaded - chunkBytesLoaded);
                        chunkBytesLoaded = e.loaded;
                        // report
                        progressCallback.apply(request, [uploadChunkData.uploaded, file.size]);
                    } else {
                        // Unable to compute progress information since the total size is unknown
                    }
                }, false);
            }

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
