import { of as observableOf, Observable } from 'rxjs';
import { KalturaFileRequest } from '../api/kaltura-file-request';
import { KalturaRequestOptions, KalturaRequestOptionsArgs } from '../api/kaltura-request-options';
import { buildUrl, createClientTag, createEndpoint, prepareParameters } from './utils';
import { KalturaClientOptions } from '../kaltura-client-options';
import { environment } from '../environment';


export class KalturaFileRequestAdapter {

  public transmit(request: KalturaFileRequest, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions): Observable<{ url: string }> {
    const parameters = prepareParameters(request, clientOptions, defaultRequestOptions);
    const endpointOptions = {
      ...clientOptions,
      service: parameters['service'],
      action: parameters['action'],
      avoidClientTag: environment.request.avoidQueryString
    }
    const endpointUrl = createEndpoint(request, endpointOptions);
    delete parameters['service'];
    delete parameters['action'];

    return observableOf({url: buildUrl(endpointUrl, parameters)});
  }
}
