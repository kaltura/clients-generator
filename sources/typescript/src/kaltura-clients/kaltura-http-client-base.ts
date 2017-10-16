import { CancelableAction } from "../utils/cancelable-action";
import { KalturaClientBase, KalturaClientBaseConfiguration } from "./kaltura-client-base";
import { KalturaUploadRequest } from '../kaltura-upload-request';

export interface KalturaHttpClientBaseConfiguration extends KalturaClientBaseConfiguration {
  endpointUrl: string;
  chunkFileSize?: number;
}

interface ChunkData {
  resume: boolean;
  resumeAt: number;
  finalChunk: boolean;
}

export abstract class KalturaHttpClientBase extends KalturaClientBase {

  public chunkFileSize: number;
  public endpointUrl: string;

  constructor(config: KalturaHttpClientBaseConfiguration) {
    super(config);

    if (!config || !config.endpointUrl) {
      throw new Error("invalid config, missing endpoint url value");
    }

    this.endpointUrl = config.endpointUrl;
    this.chunkFileSize = config.chunkFileSize;
  }

  private _getHeaders(): any {
    return {
      "Accept": "application/json",
      "Content-Type": "application/json"
    };
  }

  protected _transmitFileUploadRequest(request : KalturaUploadRequest<any>): CancelableAction {
    return new CancelableAction((resolve, reject) => {
      const uploadedFileSize = request.uploadedFileSize;
      const data: ChunkData = { resume: !!uploadedFileSize, finalChunk: false, resumeAt: uploadedFileSize };

      const handleChunkUploadError = reason => {
        chunkUploadRequest = null;
        reject(reason);
      };

      const handleChunkUploadSuccess = response => {
        if (data.finalChunk) {
          chunkUploadRequest = null;
          resolve(response);
        } else {
          chunkUploadRequest = this._chunkUpload(request, data).then(handleChunkUploadSuccess, handleChunkUploadError);
        }
      };

      let chunkUploadRequest = this._chunkUpload(request, data).then(handleChunkUploadSuccess, handleChunkUploadError);

      return () => {
        if (chunkUploadRequest) {
          chunkUploadRequest.cancel();
        }
      };
    });
  }

  private _chunkUpload(request: KalturaUploadRequest<any>, uploadChunkData: ChunkData): CancelableAction {
    return new CancelableAction((resolve, reject) => {
      let isComplete = false;
      const parameters: any = Object.assign(
        {
          format: 1
        },
        request.toRequestObject()
      );

      this._assignDefaultParameters(parameters);

      const data = request.getFormData();
      const file = request.getFileData();

      const { service, action } = parameters;
      delete parameters.service;
      delete parameters.action;

        let actualChunkSize = 5e6; // default
        if (this.chunkFileSize) {
            if (this.chunkFileSize < 1e6) {
                console.warn(`user requested for invalid upload chunk size '${this.chunkFileSize}'. minimal value 1Mb. using minimal value 1Mb instead`);
                actualChunkSize = 1e6;
            } else {
                actualChunkSize = this.chunkFileSize;
                console.log(`using user requetsed chunk size '${this.chunkFileSize}'`);
            }
        } else {
            console.log(`user requested for invalid (empty) upload chunk size. minimal value 1Mb. using default value 5Mb instead`);
        }

      uploadChunkData.finalChunk = (file.size - uploadChunkData.resumeAt) <= actualChunkSize;

      const start = uploadChunkData.resumeAt;
      const end = uploadChunkData.finalChunk ? file.size : start + actualChunkSize;

      data.set(request.getFilePropertyName(), file.slice(start, end, file.type), file.name);

      parameters.resume = uploadChunkData.resume;
      parameters.resumeAt = uploadChunkData.resumeAt;
      parameters.finalChunk = uploadChunkData.finalChunk;

      // build endpoint
      const querystring = this._buildQuerystring(parameters);
      const endpoint = `${this.endpointUrl}/service/${service}/action/${action}?${querystring}`;

      const xhr = new XMLHttpRequest();

      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
          let resp;

          try {
            if (xhr.status === 200) {
              resp = JSON.parse(xhr.response);
            } else {
              resp = new Error(xhr.responseText);
            }
          } catch (e) {
            resp = new Error(xhr.responseText);
          }

          if (resp instanceof Error) {
            reject(resp);
          } else {
            if (!uploadChunkData.finalChunk) {
              uploadChunkData.resumeAt = Number(resp.uploadedFileSize);
              uploadChunkData.resume = true;
            }

            resolve(resp);
          }
        }
      };

      const progressCallback = request._getProgressCallback();
      if (progressCallback) {
        xhr.upload.addEventListener("progress", e => {
          if (e.lengthComputable) {
            const chunkSize = uploadChunkData.finalChunk ? file.size - start : actualChunkSize;
            progressCallback.apply(request, [Math.floor(e.loaded / e.total * chunkSize) + start, file.size]);
          } else {
            // Unable to compute progress information since the total size is unknown
          }
        }, false);
      }

      xhr.open("POST", endpoint);
      xhr.send(data);

      return () => {
        if (!isComplete) {
          xhr.abort();
          isComplete = true;
        }
      };
    });
  }

  protected abstract _createCancelableAction(data: { endpoint: string, headers: any, body: {} }): CancelableAction;


  protected _transmitRequest(request): CancelableAction {


    const parameters: any = Object.assign(
      {
        format: 1
      },
      request.toRequestObject()
    );

    this._assignDefaultParameters(parameters);

    // build endpoint
    const endpoint = `${this.endpointUrl}/service/${parameters.service}/action/${parameters.action}`;

    delete parameters.service;
    delete parameters.action;

    const headers = this._getHeaders();

    return this._createCancelableAction({ endpoint, headers, body: parameters });
  }

  private _buildQuerystring(data: {}, prefix?: string) {
    let str = [], p;
    for (p in data) {
      if (data.hasOwnProperty(p)) {
        let k = prefix ? prefix + "[" + p + "]" : p, v = data[p];
        str.push((v !== null && typeof v === "object") ?
          this._buildQuerystring(v, k) :
          encodeURIComponent(k) + "=" + encodeURIComponent(v));
      }
    }
    return str.join("&");

  }
}
