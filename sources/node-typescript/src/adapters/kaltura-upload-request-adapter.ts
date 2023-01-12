import { KalturaUploadRequest } from '../api/kaltura-upload-request';
import { createEndpoint, prepareParameters } from './utils';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { CancelableAction } from '../cancelable-action';
import got from 'got';
import * as FormData from 'form-data';
import { Logger } from "../api/kaltura-logger";

interface UploadByChunksData {
  resume: boolean;
  resumeAt: number;
  finalChunk: boolean;
}

const serviceStr = 'uploadtoken' as const
const actionStr = 'upload' as const

export class KalturaUploadRequestAdapter {
  constructor(public clientOptions: KalturaClientOptions, public defaultRequestOptions: KalturaRequestOptions) {
    /* noop */
  }

  transmit(request: KalturaUploadRequest<any>): CancelableAction<any> {
    return new CancelableAction((resolve, reject, action) => {
      const uploadedFileSize = !isNaN(request.uploadedFileSize) && isFinite(request.uploadedFileSize) && request.uploadedFileSize > 0
        ? request.uploadedFileSize
        : 0;
      const data: UploadByChunksData = {
        resume: !!uploadedFileSize,
        finalChunk: false,
        resumeAt: uploadedFileSize
      };
      let activeAction: CancelableAction<any>;

      const handleChunkUploadError = (reason) => {
        activeAction = null;
        reject(reason);
      }

      const handleChunkUploadSuccess = (result) => {
        if (!data.finalChunk) {
          activeAction = this._chunkUpload(request, data).then(handleChunkUploadSuccess, handleChunkUploadError);
          return;
        }

        activeAction = null;
        try {
          const response = request.handleResponse(result);
          if (response.error) {
            throw response.error;
          } else {
            resolve(response.result);
          }
        } catch (error) {
          if (error instanceof KalturaClientException) {
            reject(new KalturaClientException(error.message, error.code, { ...(error.args || {}), service: serviceStr, action: actionStr }));
          } else if (error instanceof KalturaAPIException) {
            reject(new KalturaAPIException(error.message, error.code, { ...(error.args || {}), service: serviceStr, action: actionStr }))
          } else {
            const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
            reject(new KalturaClientException('client::response-unknown-error', errorMessage || 'Failed to parse response', { service: serviceStr, action: actionStr }));
          }
        }
      }

      activeAction = this._chunkUpload(request, data).then(handleChunkUploadSuccess, handleChunkUploadError);
      return () => {
        if (activeAction) {
          activeAction.cancel();
          activeAction = null;
        }
      };
    });
  }

  private async _getFormData(
    { fileName, fileData, ks }: { fileName: string, fileData: Blob, ks:string }
  ): Promise<FormData> {
    const form = new FormData();
    form.append('fileName', fileName)
    form.append('ks', ks)
    const ab = await fileData.arrayBuffer();
    const buffer = Buffer.from(ab);
    form.append('fileData', buffer, { contentType: fileData.type, filename: fileName })
    return form;
  }

  private _getChunkFileSize(): number {
    const userChunkFileSize = this.clientOptions ? this.clientOptions.chunkFileSize : null;
    let actualChunkFileSize = 5e6; // default 5 mb
    if (userChunkFileSize && Number.isFinite(userChunkFileSize) && !Number.isNaN(userChunkFileSize)) {
      if (userChunkFileSize < 1e5) {
        Logger.warn(`user requested for invalid upload chunk size '${userChunkFileSize}'. minimal value 100Kb. using minimal value 100Kb instead`)
        actualChunkFileSize = 1e5;
      } else {
        actualChunkFileSize = userChunkFileSize;
      }
    }
    Logger.info(`using chunk size of ${userChunkFileSize} bytes`)
    return actualChunkFileSize
  }

  private async _prepareRequest(
    request: KalturaUploadRequest<any>,
    uploadChunkData: UploadByChunksData
  ): Promise<{ endpointUrl: string, form: FormData, searchParams: URLSearchParams }> {
    const actualChunkFileSize = this._getChunkFileSize();
    const { file } = request.getFileInfo();
    uploadChunkData.finalChunk = (file.size - uploadChunkData.resumeAt) <= actualChunkFileSize;
    const fileStart = uploadChunkData.resumeAt;
    const fileEnd = uploadChunkData.finalChunk ? file.size : fileStart + actualChunkFileSize;

    const form = await this._getFormData({
      fileName: file.name,
      fileData: file.slice(fileStart, fileEnd, file.type),
      ks: this.defaultRequestOptions.ks
    })
    const { service, action, ks, ...params } = prepareParameters(request, this.clientOptions, this.defaultRequestOptions);
    const endpointUrl = createEndpoint(request, this.clientOptions, service, action);
    const searchParams = new URLSearchParams({
      ...params,
      resume: uploadChunkData.resume,
      resumeAt: uploadChunkData.resumeAt,
      finalChunk: uploadChunkData.finalChunk
    })
    return { endpointUrl, form, searchParams }
  }

  private _chunkUpload(
    request: KalturaUploadRequest<any>,
    uploadChunkData: UploadByChunksData
  ): CancelableAction<any> {
    return new CancelableAction((resolve, reject) => {
      let isComplete = false, isAborted = false
      let xMe, xKalturaSession
      let gotRequest

      this._prepareRequest(request, uploadChunkData).then((
        { endpointUrl, form, searchParams }:{ endpointUrl: string, form: FormData, searchParams: URLSearchParams }
      ) => {
        if (isAborted) { return }
        gotRequest = got.post(endpointUrl, { body: form, searchParams })
        // save headers and parse response:
        gotRequest.then(response => {
          isComplete = true
          xMe = response?.headers?.['x-me'] || ''
          xKalturaSession = response?.headers?.['x-kaltura-session'] || ''
          Logger.debug(`Kaltura response completed for: ${serviceStr}/${actionStr}, x-me: ${xMe}, x-kaltura-session: ${xKalturaSession}`)
          return gotRequest.json()
        })
        // handle parsed response:
        .then((parsedResponse: any) => {
          if (parsedResponse?.objectType === 'KalturaAPIException') {
            reject(new KalturaAPIException(parsedResponse.message, parsedResponse.code, { ...(parsedResponse.args || {}), service: serviceStr, action: actionStr }))
            return
          }
          if (parsedResponse.uploadedFileSize === undefined || parsedResponse.uploadedFileSize === null) {
            reject(new KalturaClientException('client::upload-failure', `uploaded chunk of file failed, expected response with property 'uploadedFileSize'`, { service: serviceStr, action: actionStr }))
            return
          }
          if (!uploadChunkData.finalChunk) {
            uploadChunkData.resumeAt = Number(parsedResponse.uploadedFileSize)
            uploadChunkData.resume = true
          }
          resolve(parsedResponse);
        })
        // handle errors:
        .catch(e => {
          isComplete = true
          xMe ||= e.response?.headers?.['x-me'] || ''
          xKalturaSession ||= e.response?.headers?.['x-kaltura-session'] || ''
          const msg = e.response?.body || e.message || 'failed to upload file'
          const err = new KalturaClientException('client::upload-failure', msg);
          Logger.error(`Kaltura response error: '${msg || ''}', for: ${serviceStr}/${actionStr}, x-me: ${xMe}, x-kaltura-session: ${xKalturaSession}`)
          reject(err)
        })
      })

      return () => {
        if (!isComplete) {
          isComplete = true;
          isAborted = true
          gotRequest?.cancel();
        }
      }
    })
  }
}
