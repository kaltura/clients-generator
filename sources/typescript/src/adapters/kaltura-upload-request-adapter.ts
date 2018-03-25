import { KalturaUploadRequest } from '../api/kaltura-upload-request';
import { buildQuerystring, createEndpoint, prepareParameters } from './utils';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { CancelableAction } from '../cancelable-action';

interface UploadByChunksData {
    enabled: boolean;
    resume: boolean;
    resumeAt: number;
    finalChunk: boolean;
}
export class KalturaUploadRequestAdapter {
    private _chunkUploadSupported(request: KalturaUploadRequest<any>): boolean {
        // SUPPORTED BY BROWSER?
        // Check if these features are support by the browser:
        // - File object type
        // - Blob object type
        // - FileList object type
        // - slicing files
        const supportedByBrowser = (
            (typeof(File) !== 'undefined')
            &&
            (typeof(Blob) !== 'undefined')
            &&
            (typeof(FileList) !== 'undefined')
            &&
            (!!(<any>Blob.prototype)['webkitSlice'] || !!(<any>Blob.prototype)['mozSlice'] || !!(<any>Blob.prototype).slice || false)
        );
        const supportedByRequest = request.supportChunkUpload();
        const enabledInClient = !this.clientOptions.chunkFileDisabled;

        return enabledInClient && supportedByBrowser && supportedByRequest;
    }

    constructor(public clientOptions: KalturaClientOptions, public defaultRequestOptions: KalturaRequestOptions)
    {

    }

    transmit(request: KalturaUploadRequest<any>): CancelableAction<any> {
        return new CancelableAction((resolve, reject, action) => {
            const uploadedFileSize = !isNaN(request.uploadedFileSize) && isFinite(request.uploadedFileSize) && request.uploadedFileSize > 0 ? request.uploadedFileSize : 0;
            const data: UploadByChunksData = {
                enabled: this._chunkUploadSupported(request),
                resume: !!uploadedFileSize,
                finalChunk: false,
                resumeAt: uploadedFileSize
            };

            let activeAction: CancelableAction<any>;

            const handleChunkUploadError = reason => {
                activeAction = null;
                reject(reason);
            };

            const handleChunkUploadSuccess = result => {
                if (!data.enabled || data.finalChunk) {
                    activeAction = null;

                    try {
                        const response = request.handleResponse(result);

                        if (response.error) {
                            reject(response.error);
                        } else {
                            resolve(response.result);
                        }
                    } catch (error) {
                        if (error instanceof KalturaClientException || error instanceof KalturaAPIException) {
                            reject(error);
                        } else {
                            const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
                            reject(new KalturaClientException('client::response-unknown-error', errorMessage || 'Failed to parse response'));
                        }
                    }


                } else {
                    activeAction = this._chunkUpload(request, data).then(handleChunkUploadSuccess, handleChunkUploadError);
                }
            };

            activeAction = this._chunkUpload(request, data)
                .then(handleChunkUploadSuccess, handleChunkUploadError);


            return() =>
            {
                if (activeAction) {
                    activeAction.cancel();
                    activeAction = null;
                }
            };
        });
    }

    private _getFormData(filePropertyName: string, fileName: string, fileChunk: File | Blob): FormData {
        const result = new FormData();
        result.append("fileName", fileName);
        result.append(filePropertyName, fileChunk);
        return result;
    }

    private _chunkUpload(request: KalturaUploadRequest<any>, uploadChunkData: UploadByChunksData): CancelableAction<any> {
        return new CancelableAction((resolve, reject) => {
            const parameters = prepareParameters(request, this.clientOptions, this.defaultRequestOptions);

            let isComplete = false;
            const {propertyName, file} = request.getFileInfo();
            let data = this._getFormData(propertyName, file.name, file);

            let fileStart = 0;
            let actualChunkFileSize: number = null;

            if (uploadChunkData.enabled) {
                const userChunkFileSize = this.clientOptions ? this.clientOptions.chunkFileSize : null;

                if (userChunkFileSize && Number.isFinite(userChunkFileSize) && !Number.isNaN(userChunkFileSize)) {
                    if (actualChunkFileSize < 1e6) {
                        console.warn(`user requested for invalid upload chunk size '${userChunkFileSize}'. minimal value 1Mb. using minimal value 1Mb instead`);
                        actualChunkFileSize = 1e6;
                    } else {
                        console.log(`using user requetsed chunk size '${userChunkFileSize}'`);
                        actualChunkFileSize = userChunkFileSize;
                    }
                } else {
                    console.log(`using default chunk size 5Mb`);
                    actualChunkFileSize = 5e6; // default
                }

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

            let endpointUrl = createEndpoint(request, this.clientOptions, parameters['service'], parameters['action']);
            delete parameters['service'];
            delete parameters['action'];
            const querystring = buildQuerystring(parameters);
            endpointUrl = `${endpointUrl}?${querystring}`;

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
                            resp = new KalturaClientException('client::upload-failure', xhr.responseText || 'failed to upload file');
                        }
                    } catch (e) {
                        resp = new KalturaClientException('client::upload-failure', e.message || 'failed to upload file')
                    }

                    if (resp instanceof Error) {
                        reject(resp);
                    } else {
                        if (uploadChunkData.enabled) {
                            if (typeof resp.uploadedFileSize === "undefined" || resp.uploadedFileSize === null) {
                                reject(new KalturaClientException('client::upload-failure', `uploaded chunk of file failed, expected response with property 'uploadedFileSize'`));
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
