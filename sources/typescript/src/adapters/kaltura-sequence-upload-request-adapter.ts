import { KalturaUploadRequest } from "../api/kaltura-upload-request";
import { createEndpoint, prepareParameters } from "./utils";
import { KalturaClientException } from "../api/kaltura-client-exception";
import { KalturaRequestOptions } from "../api/kaltura-request-options";
import { KalturaClientOptions } from "../kaltura-client-options";
import { CancelableAction } from "../cancelable-action";
import { KalturaUploadRequestAdapter } from "./kaltura-upload-request-adapter";

interface UploadBySequentialChunksData {
    enabled: boolean;
    resume: boolean;
    resumeAt: number;
    finalChunk: boolean;
}

/**
 * Request adapter to allow uploading files chunk after chunk
 */
export class KalturaSequenceUploadRequestAdapter extends KalturaUploadRequestAdapter {


    constructor(clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions) {
        super(clientOptions, defaultRequestOptions);
    }

    transmit(request: KalturaUploadRequest<any>): CancelableAction<any> {
        return new CancelableAction((resolve, reject, action) => {
            const uploadedFileSize = !isNaN(request.uploadedFileSize) && isFinite(request.uploadedFileSize) && request.uploadedFileSize > 0 ? request.uploadedFileSize : 0;
            const data: UploadBySequentialChunksData = {
                enabled: this._chunkUploadSupported(request),
                resume: !!uploadedFileSize,
                finalChunk: false,
                resumeAt: uploadedFileSize,
            };

            let activeAction: CancelableAction<any>;

            const handleChunkUploadError = reason => {
                activeAction = null;
                reject(reason);
            };

            const handleChunkUploadSuccess = result => {
                if (!data.enabled || data.finalChunk) {
                    activeAction = null;
                    this._handleFinalChunkResponse(request, result, reject, resolve);

                } else {
                    activeAction = this._chunkUpload(request, data).then(handleChunkUploadSuccess, handleChunkUploadError);
                }
            };

            activeAction = this._chunkUpload(request, data)
                .then(handleChunkUploadSuccess, handleChunkUploadError);


            return () => {
                if (activeAction) {
                    activeAction.cancel();
                    activeAction = null;
                }
            };
        });
    }

    /**
     * chunk upload for sequential chunks upload
     * @param request
     * @param uploadChunkData
     * @private
     */
    private _chunkUpload(request: KalturaUploadRequest<any>, uploadChunkData: UploadBySequentialChunksData): CancelableAction<any> {
        return new CancelableAction((resolve, reject) => {
            const parameters = prepareParameters(request, this.clientOptions, this.defaultRequestOptions);

            let isComplete = false;
            const {propertyName, file} = request.getFileInfo();
            let data = this._getFormData(propertyName, file.name, file);

            let fileStart = 0;

            if (uploadChunkData.enabled) {
                let actualChunkFileSize: number = this._getChunkSize();

                uploadChunkData.finalChunk = (file.size - uploadChunkData.resumeAt) <= actualChunkFileSize;

                fileStart = uploadChunkData.resumeAt;
                const fileEnd = uploadChunkData.finalChunk ? file.size : fileStart + actualChunkFileSize;

                data = this._getFormData(propertyName, file.name, file.slice(fileStart, fileEnd, file.type));

                parameters.resume = uploadChunkData.resume;
                parameters.resumeAt = uploadChunkData.resumeAt;
                parameters.finalChunk = uploadChunkData.finalChunk;
            } else {
                console.log(`chunk upload not supported by browser or by request. Uploading the file as-is`);
            }

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
                        if (uploadChunkData.enabled) {
                            if (typeof resp.uploadedFileSize === "undefined" || resp.uploadedFileSize === null) {
                                reject(new KalturaClientException("client::upload-failure", `uploaded chunk of file failed, expected response with property 'uploadedFileSize'`));
                                return;
                            } else if (!uploadChunkData.finalChunk) {
                                uploadChunkData.resumeAt = Number(resp.uploadedFileSize);
                                uploadChunkData.resume = true;
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
                        progressCallback.apply(request, [e.loaded + fileStart, file.size]);
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
