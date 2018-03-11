import { KalturaRequestBase } from '../api/kaltura-request-base';
import { KalturaClientOptions } from '../kaltura-client-options';
import { KalturaRequestOptions, KalturaRequestOptionsArgs } from '../api/kaltura-request-options';
import { KalturaMultiRequest } from '../api/kaltura-multi-request';
import { KalturaRequest } from '../api/kaltura-request';
import { KalturaFileRequest } from '../api/kaltura-file-request';

export function  createEndpoint(request: KalturaRequestBase, options: KalturaClientOptions, service: string, action?: string): string {
    const endpoint = options.endpointUrl;
    const clientTag = createClientTag(request, options);
    let result = `${endpoint}/api_v3/service/${service}`;

    if (action) {
        result += `/action/${action}`;
    }

    if (clientTag)
    {
        result += `?${buildQuerystring({clientTag})}`;
    }
    return result;
}

export function createClientTag(request: KalturaRequestBase, options: KalturaClientOptions)
{
    const networkTag = (request.getNetworkTag() || "").trim();
    const clientTag = (options.clientTag || "").trim() || "ng-app";

    if (networkTag && networkTag.length)
    {
        return `${clientTag}_${networkTag}`;
    }else {
        return clientTag;
    }
}

export function buildQuerystring(data: {}, prefix?: string) {
    let str = [], p;
    for (p in data) {
        if (data.hasOwnProperty(p)) {
            let k = prefix ? prefix + "[" + p + "]" : p, v = data[p];
            str.push((v !== null && typeof v === "object") ?
                buildQuerystring(v, k) :
                encodeURIComponent(k) + "=" + encodeURIComponent(v));
        }
    }
    return str.join("&");

}

export function getHeaders(): any {
    return {
        "Accept": "application/json",
        "Content-Type": "application/json"
    };
}

export function prepareParameters(request: KalturaRequest<any> | KalturaMultiRequest | KalturaFileRequest,  options: KalturaClientOptions,  defaultRequestOptions: KalturaRequestOptions): any {

    return Object.assign(
        {},
        request.buildRequest(defaultRequestOptions),
        {
            format: 1
        }
    );
}