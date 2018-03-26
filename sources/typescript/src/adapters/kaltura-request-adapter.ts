import { KalturaRequest } from '../api/kaltura-request';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';
import { createCancelableAction, createEndpoint, getHeaders, prepareParameters } from './utils';
import { CancelableAction } from '../cancelable-action';

export class KalturaRequestAdapter {

    constructor() {
    }

    public transmit<T>(request: KalturaRequest<T>, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions): CancelableAction<T> {

        const body = prepareParameters(request, clientOptions, defaultRequestOptions);

        const endpoint = createEndpoint(request, clientOptions, body['service'], body['action']);
        delete body['service'];
        delete body['action'];

        return <any>createCancelableAction<T>({endpoint, headers: getHeaders(), body})
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