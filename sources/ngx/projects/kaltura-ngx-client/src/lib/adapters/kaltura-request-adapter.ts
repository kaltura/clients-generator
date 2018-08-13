
import {map, catchError} from 'rxjs/operators';
import { KalturaRequest } from '../api/kaltura-request';
import { Observable } from 'rxjs';
import { KalturaResponse } from '../api/kaltura-response';
import { HttpClient } from '@angular/common/http';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaRequestOptions, KalturaRequestOptionsArgs } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';
import { createEndpoint, getHeaders, prepareParameters } from './utils';



export class KalturaRequestAdapter {

    constructor(private _http: HttpClient) {
    }

    public transmit<T>(request: KalturaRequest<T>, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions): Observable<T> {

        const parameters = prepareParameters(request, clientOptions, defaultRequestOptions);

        const endpointUrl = createEndpoint(request, clientOptions, parameters['service'], parameters['action']);
        delete parameters['service'];
        delete parameters['action'];


        return this._http.request('post', endpointUrl,
            {
                body: parameters,
                headers: getHeaders()
            }).pipe(
            catchError(
                error => {
                    const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
                    throw new KalturaClientException("client::request-network-error", errorMessage || 'Error connecting to server');
                }
            ),
            map(
                result => {
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
                }));
    }
}
