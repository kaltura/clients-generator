import { KalturaRequest } from '../api/kaltura-request';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';
import { createCancelableAction, createEndpoint, getHeaders, prepareParameters } from './utils';
import { CancelableAction } from '../cancelable-action';

export class KalturaRequestAdapter {
  public transmit<T>(
    request: KalturaRequest<T>, 
    clientOptions: KalturaClientOptions, 
    defaultRequestOptions: KalturaRequestOptions
  ): CancelableAction<T> {
    const parameters = prepareParameters(request, clientOptions, defaultRequestOptions);
    const { service, action, ...body } = parameters;
    const { customHeaders } = defaultRequestOptions;
    const endpoint = createEndpoint(request, clientOptions, service, action);

    return createCancelableAction<T>({ endpoint, headers: getHeaders(customHeaders), body })
      .then(result => {
        try {
          const response = request.handleResponse(result);
          if (response.error) {
            throw response.error;
          } else {
            return response.result;
          }
        } catch (error) {
          if (error instanceof KalturaClientException) {
            throw new KalturaClientException(error.message, error.code, { ...(error.args || {}), service, action });
          } else if (error instanceof KalturaAPIException) {
            throw new KalturaAPIException(error.message, error.code, { ...(error.args || {}), service, action });
          } else {
            const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
            throw new KalturaClientException('client::response-unknown-error', errorMessage || 'Failed to parse response', { service, action });
          }
        }
      },
        error => {
          const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
          const args = { ...((<any>error).args || {}), service, action }
          throw new KalturaClientException("client::request-network-error", errorMessage || 'Error connecting to server', args);
        });
  }
}
