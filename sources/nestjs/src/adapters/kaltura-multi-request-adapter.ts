import { map, catchError } from 'rxjs/operators';
import { KalturaMultiRequest } from '../api/kaltura-multi-request';
import { KalturaMultiResponse } from '../api/kaltura-multi-response';
import { Observable } from 'rxjs';
import { createClientTag, createEndpoint, getHeaders, prepareParameters } from './utils';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaClientOptions } from '../kaltura-client-options';
import { HttpService } from '@nestjs/common';
import { AxiosRequestConfig } from 'axios';

export class KalturaMultiRequestAdapter {
  constructor(private http: HttpService) {
  }

  transmit(request: KalturaMultiRequest, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions): Observable<KalturaMultiResponse> {

    const parameters = prepareParameters(request, clientOptions, defaultRequestOptions);

    const endpointOptions = { ...clientOptions, service: parameters['service'], action: parameters['action'] };
    const endpointUrl = createEndpoint(request, endpointOptions);
    delete parameters['service'];
    delete parameters['action'];

    const config: AxiosRequestConfig = {
      url: endpointUrl,
      method: 'POST',
      data: parameters,
      headers: getHeaders(),
    };

    return this.http.request(config)
      .pipe(
        catchError(
          error => {
            const errorMessage = error instanceof Error ? error.message : typeof error === 'string' ? error : null;
            throw new KalturaClientException('client::multi-request-network-error', errorMessage || 'Error connecting to server');
          },
        ),
        map(
          result => {
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
          }));
  }
}
