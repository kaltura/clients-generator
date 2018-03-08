import { KalturaUploadRequest } from '../api/kaltura-upload-request';
import { buildQuerystring, createEndpoint, prepareParameters } from './utils';
import { ISubscription } from 'rxjs/Subscription';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { Observable } from 'rxjs/Observable';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';

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

    transmit(request: KalturaUploadRequest<any>): Observable<any> {
        return Observable.create(observer => {
            const uploadedFileSize = !isNaN(request.uploadedFileSize) && isFinite(request.uploadedFileSize) && request.uploadedFileSize > 0 ? request.uploadedFileSize : 0;
            const data: UploadByChunksData = {
                enabled: this._chunkUploadSupported(request),
                resume: !!uploadedFileSize,
                finalChunk: false,
                resumeAt: uploadedFileSize
            };

            let requestSubscription: ISubscription;

            const handleChunkUploadError = reason => {
                requestSubscription = null;
                observer.throw(reason);
            };

            const handleChunkUploadSuccess = response => {
                if (!data.enabled || data.finalChunk) {
                    requestSubscription = null;
                    observer.next(response);
                    observer.complete();
                } else {
                    requestSubscription = this._chunkUpload(request, data).subscribe(handleChunkUploadSuccess, handleChunkUploadError);
                }
            };

            this._chunkUpload(request, data)
                .subscribe(handleChunkUploadSuccess, handleChunkUploadError);


            return () => {
                if (requestSubscription) {
                    requestSubscription.unsubscribe();
                    requestSubscription = null;
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

    private _chunkUpload(request: KalturaUploadRequest<any>, uploadChunkData: UploadByChunksData): Observable<any> {
        return Observable.create(observer => {
            const parameters = prepareParameters(request, this.clientOptions.clientTag, this.defaultRequestOptions);

            let isComplete = false;
            const { propertyName, file } = request.getFileInfo();
            let data = this._getFormData(propertyName, file.name, file);

            let fileStart = 0;
            let uploadSize: number = null;

            if (uploadChunkData.enabled) {
                uploadSize =  (this.clientOptions ? this.clientOptions.chunkFileSize : null)  || 5e6; // default
                if (this.clientOptions.endpointUrl) {
                    if (uploadSize < 1e6) {
                        console.warn(`user requested for invalid upload chunk size '${uploadSize}'. minimal value 1Mb. using minimal value 1Mb instead`);
                        uploadSize = 1e6;
                    } else {
                        console.log(`using user requetsed chunk size '${uploadSize}'`);
                    }
                } else {
                    console.log(`using default chunk size 5Mb`);
                }

                uploadChunkData.finalChunk = (file.size - uploadChunkData.resumeAt) <= uploadSize;

                fileStart = uploadChunkData.resumeAt;
                const fileEnd = uploadChunkData.finalChunk ? file.size : fileStart + uploadSize;

                data = this._getFormData(propertyName, file.name, file.slice(fileStart, fileEnd, file.type));

                parameters.resume = uploadChunkData.resume;
                parameters.resumeAt = uploadChunkData.resumeAt;
                parameters.finalChunk = uploadChunkData.finalChunk;
            } else {
                console.log(`chunk upload not supported by browser or by request. Uploading the file as-is`);
            }

            let endpointUrl = createEndpoint(this.clientOptions.endpointUrl, parameters['service'], parameters['action']);
            delete parameters['service'];
            delete parameters['action'];
            const querystring = buildQuerystring(parameters);
            endpointUrl = `${endpointUrl}?${querystring}`;

            const xhr = new XMLHttpRequest();

            xhr.onreadystatechange = () => {
                if (xhr.readyState === 4) {
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
                        observer.throw(resp);
                    } else {
                        if (uploadChunkData.enabled) {
                            if (typeof resp.uploadedFileSize === "undefined" || resp.uploadedFileSize === null) {
                                observer.throw(new KalturaClientException('client::upload-failure', `uploaded chunk of file failed, expected response with property 'uploadedFileSize'`));
                                return;
                            } else if (!uploadChunkData.finalChunk) {
                                uploadChunkData.resumeAt = Number(resp.uploadedFileSize);
                                uploadChunkData.resume = true;
                            }
                        }

                        observer.next(resp);
                        observer.complete();
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
                    xhr.abort();
                    isComplete = true;
                }
            };
        });
    }

}
