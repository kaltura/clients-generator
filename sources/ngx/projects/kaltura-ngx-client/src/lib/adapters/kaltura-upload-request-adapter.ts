import { KalturaUploadRequest } from '../api/kaltura-upload-request';
import { buildUrl, createEndpoint, prepareParameters, createClientTag } from './utils';
import { SubscriptionLike as ISubscription ,  Observable } from 'rxjs';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { environment } from '../environment';

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
        observer.error(reason);
      };

      const handleChunkUploadSuccess = result => {
        if (!data.enabled || data.finalChunk) {
          requestSubscription = null;

          try {
            const response = request.handleResponse(result);

            if (response.error) {
              observer.error(response.error);
            } else {
              observer.next(response.result);
              observer.complete();
            }
          } catch (error) {
            if (error instanceof KalturaClientException || error instanceof KalturaAPIException) {
              observer.error(error);
            } else {
              const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
              observer.error(new KalturaClientException('client::response-unknown-error', errorMessage || 'Failed to parse response'));
            }
          }


        } else {
          requestSubscription = this._chunkUpload(request, data).subscribe(handleChunkUploadSuccess, handleChunkUploadError);
        }
      };

      requestSubscription = this._chunkUpload(request, data)
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
    result.append("Filename", fileName);
    result.append(filePropertyName, fileChunk, fileName);
    return result;
  }

  private _unwrapResponse(response: any): any {
    if (environment.response.nestedResponse) {
      if (response && response.hasOwnProperty('result')) {
        if (response.result.hasOwnProperty('error')) {
          return response.result.error;
        } else {
          return response.result;
        }
      } else if (response && response.hasOwnProperty('error')) {
        return response.error;
      }
    }

    return response;
  }

  private _chunkUpload(request: KalturaUploadRequest<any>, uploadChunkData: UploadByChunksData): Observable<any> {
    return Observable.create(observer => {
      const parameters = prepareParameters(request, this.clientOptions, this.defaultRequestOptions);

      let isComplete = false;
      const {propertyName, file} = request.getFileInfo();
      let data = this._getFormData(propertyName, file.name, file);

      let fileStart = 0;

      if (uploadChunkData.enabled) {
        let actualChunkFileSize: number = null;
        const userChunkFileSize = this.clientOptions ? this.clientOptions.chunkFileSize : null;

        if (userChunkFileSize && Number.isFinite(userChunkFileSize) && !Number.isNaN(userChunkFileSize)) {
          if (userChunkFileSize < 1e5) {
            console.warn(`user requested for invalid upload chunk size '${userChunkFileSize}'. minimal value 100Kb. using minimal value 100Kb instead`);
            actualChunkFileSize = 1e5;
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

      if (environment.request.avoidQueryString) {
        data.append('clientTag',createClientTag(request, this.clientOptions));
        (Object.keys(parameters) || []).forEach(key => {
          data.append(key, parameters[key]);
        });
      } else {
        endpointUrl = buildUrl(endpointUrl, parameters);
      }

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
              resp = this._unwrapResponse(JSON.parse(xhr.response));

              if (resp && resp.objectType === 'KalturaAPIException') {
                resp = new KalturaAPIException(
                  resp.message,
                  resp.code,
                  resp.args
                );
              }
            } else {
              resp = new KalturaClientException('client::upload-failure', xhr.responseText || 'failed to upload file');
            }
          } catch (e) {
            resp = new KalturaClientException('client::upload-failure', e.message || 'failed to upload file')
          }

          if (resp instanceof Error) {
            observer.error(resp);
          } else {
            if (uploadChunkData.enabled) {
              if (typeof resp.uploadedFileSize === "undefined" || resp.uploadedFileSize === null) {
                observer.error(new KalturaClientException('client::upload-failure', `uploaded chunk of file failed, expected response with property 'uploadedFileSize'`));
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
          isComplete = true;
          xhr.abort();
        }
      };
    });
  }
}
