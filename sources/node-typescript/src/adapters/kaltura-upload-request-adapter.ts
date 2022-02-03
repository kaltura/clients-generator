import { KalturaUploadRequest } from '../api/kaltura-upload-request';
import { createEndpoint, prepareParameters } from './utils';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { CancelableAction } from '../cancelable-action';
import got from 'got';
import * as dbg from 'debug'

const debug = dbg('kaltura:adapter:upload')

/*
 * WE DONT SUPPORT UPLOAD YET
 */
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
    const supportedByBrowser = false;
    const supportedByRequest = request.supportChunkUpload();
    const enabledInClient = !this.clientOptions.chunkFileDisabled;

    return enabledInClient && supportedByBrowser && supportedByRequest;
  }

  constructor(public clientOptions: KalturaClientOptions, public defaultRequestOptions: KalturaRequestOptions) {
    // Need to catch outer client.request() for this, so logging also..
    debug('"node-typescript-client" does not yet support uploading, you can use Kaltura typescript client (Web) or Kaltura node client for this.')
    throw new Error('"node-typescript-client" does not yet support uploading, you can use Kaltura typescript client (Web) or Kaltura node client for this.')
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


      return () => {
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
      const { propertyName, file } = request.getFileInfo();
      let data = this._getFormData(propertyName, file.name, file);

      let fileStart = 0;

      if (uploadChunkData.enabled) {
        let actualChunkFileSize: number = null;
        const userChunkFileSize = this.clientOptions ? this.clientOptions.chunkFileSize : null;

        if (userChunkFileSize && Number.isFinite(userChunkFileSize) && !Number.isNaN(userChunkFileSize)) {
          if (userChunkFileSize < 1e5) {
            debug(`user requested for invalid upload chunk size '${userChunkFileSize}'. minimal value 100Kb. using minimal value 100Kb instead`);
            actualChunkFileSize = 1e5;
          } else {
            debug(`using user requested chunk size '${userChunkFileSize}'`);
            actualChunkFileSize = userChunkFileSize;
          }
        } else {
          debug(`using default chunk size 5Mb`);
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
        debug(`chunk upload not supported by browser or by request. Uploading the file as-is`);
      }

      const { service, action, ...queryparams } = parameters;
      const endpointUrl = createEndpoint(request, this.clientOptions, service, action, queryparams);

      const promWithData = got.post('https://httpbin.org/anything', {
        json: {
          hello: 'world'
        }
      }).json();

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
