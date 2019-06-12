import { KalturaMultiRequest } from '../api/kaltura-multi-request';
import { KalturaMultiResponse } from '../api/kaltura-multi-response';
import { createCancelableAction, createEndpoint, getHeaders, prepareParameters } from './utils';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';
import { CancelableAction } from '../cancelable-action';

export class KalturaMultiRequestAdapter {
    constructor() {
    }

    transmit(request: KalturaMultiRequest, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions): CancelableAction<KalturaMultiResponse> {

        const parameters = prepareParameters(request, clientOptions, defaultRequestOptions);

        const { service, action, ...body } = parameters;
        const endpoint = createEndpoint(request, clientOptions, service, action);

        return <any>(createCancelableAction<KalturaMultiResponse>({endpoint, headers: getHeaders(), body})
            .then(result => {
                    try {
                        return request.handleResponse(result);
                    } catch (error) {
                        if (error instanceof KalturaClientException || error instanceof KalturaAPIException) {
                            throw error;
                        } else {
                            const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
                            throw new KalturaClientException('client::multi-response-unknown-error', errorMessage || 'Failed to parse response');
                        }
                    }
                },
                error => {
                    const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
                    throw new KalturaClientException("client::multi-request-network-error", errorMessage || 'Error connecting to server');
                }));
    }
}
