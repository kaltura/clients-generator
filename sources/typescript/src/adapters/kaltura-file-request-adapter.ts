import { KalturaFileRequest } from '../api/kaltura-file-request';
import { KalturaRequestOptions, KalturaRequestOptionsArgs } from '../api/kaltura-request-options';
import { buildQuerystring, createClientTag, createEndpoint, prepareParameters } from './utils';
import { KalturaClientOptions } from '../kaltura-client-options';
import { CancelableAction } from '../cancelable-action';


export class KalturaFileRequestAdapter {

    public transmit(request: KalturaFileRequest, clientOptions: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions): CancelableAction<{ url: string }> {
        const parameters = prepareParameters(request, clientOptions, defaultRequestOptions);
        const { service, action, ...queryparams } = parameters;
        const endpointUrl = createEndpoint(request, clientOptions, service, action, queryparams);

        return CancelableAction.resolve({url: endpointUrl});
    }
}
