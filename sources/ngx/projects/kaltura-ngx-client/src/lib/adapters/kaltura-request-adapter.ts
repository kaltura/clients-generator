import {map, catchError} from 'rxjs/operators';
import { KalturaRequest } from '../api/kaltura-request';
import { Observable } from 'rxjs';
import { KalturaResponse } from '../api/kaltura-response';
import { HttpClient } from '@angular/common/http';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaRequestOptions, KalturaRequestOptionsArgs } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';
import { createClientTag, createEndpoint, getHeaders, prepareParameters } from './utils';
import { environment } from '../environment';



export class KalturaRequestAdapter {

  constructor(private _http: HttpClient) {
  }

  public transmit<T>(request: KalturaRequest<T>, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions): Observable<T>;
  public transmit<T>(request: KalturaRequest<any>, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions, format: string): Observable<any>;
  public transmit<T>(request: KalturaRequest<any>, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions, format: string, responseType: 'blob' | 'text'): Observable<any>;
  public transmit<T>(request: KalturaRequest<T>, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions, format?: string, responseType: 'blob' | 'text' = 'text'): Observable<any> {

    const requestSpecificFormat = typeof format !== 'undefined';
    const parameters = prepareParameters(request, clientOptions, defaultRequestOptions);

    const endpointOptions = { ...clientOptions, service: parameters['service'], action:  parameters['action'], format };
    const endpointUrl = createEndpoint(request, endpointOptions);
    delete parameters['service'];
    delete parameters['action'];

    if (environment.request.avoidQueryString) {
      parameters['clientTag'] = createClientTag(request, clientOptions);
    }

    return this._http.request('post', endpointUrl,
      {
        body: parameters,
        responseType: requestSpecificFormat ? responseType || 'text' : 'json',
        headers: requestSpecificFormat ? undefined : getHeaders()
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
            const response = request.handleResponse(result, requestSpecificFormat);

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
