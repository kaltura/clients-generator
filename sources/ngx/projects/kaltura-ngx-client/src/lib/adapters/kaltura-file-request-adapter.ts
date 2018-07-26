
import {of as observableOf,  Observable } from 'rxjs';
import { KalturaFileRequest } from '../api/kaltura-file-request';
import { KalturaRequestOptions, KalturaRequestOptionsArgs } from '../api/kaltura-request-options';
import { buildUrl, createClientTag, createEndpoint, prepareParameters } from './utils';
import { KalturaClientOptions } from '../kaltura-client-options';


export class KalturaFileRequestAdapter {

    public transmit(request: KalturaFileRequest, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions): Observable<{ url: string }> {
        const parameters = prepareParameters(request, clientOptions, defaultRequestOptions);
        const endpointUrl = createEndpoint(request, clientOptions, parameters['service'], parameters['action']);
        delete parameters['service'];
        delete parameters['action'];

        return observableOf({url: buildUrl(endpointUrl, parameters)});
    }
}
