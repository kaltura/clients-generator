import { KalturaFileRequest } from '../api/kaltura-file-request';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { createCancelableAction, createEndpoint, getHeaders, prepareParameters } from './utils';
import { KalturaClientOptions } from '../kaltura-client-options';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { CancelableAction } from '../cancelable-action';

export class KalturaFileRequestAdapter {
  public transmit(
    request: KalturaFileRequest, 
    clientOptions: KalturaClientOptions, 
    defaultRequestOptions: KalturaRequestOptions
  ): CancelableAction<string> {
    const parameters = prepareParameters(request, clientOptions, defaultRequestOptions);
    const { service, action, ...body } = parameters;
    const { customHeaders } = defaultRequestOptions;
    const endpoint = createEndpoint(request, clientOptions, service, action);

    return createCancelableAction<string>({ endpoint, headers: getHeaders(customHeaders), body }, 'text')
      .then(result => {
        try {
          const response = request.handleResponse(result);
          if (response.error) {
            throw response.error;
          } else {
            return response.result;
          }
        } catch (error) {
          if (error instanceof KalturaClientException || error instanceof KalturaAPIException) {
            throw error;
          } else {
            const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
            throw new KalturaClientException('client::response-unknown-error', errorMessage || 'Failed to parse response');
          }
        }
      },
      error => {
        const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
        throw new KalturaClientException("client::request-network-error", errorMessage || 'Error connecting to server');
      });
  }
}
