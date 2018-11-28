import { KalturaRequestBase } from '../api/kaltura-request-base';
import { KalturaClientOptions } from '../kaltura-client-options';
import { KalturaRequestOptions, KalturaRequestOptionsArgs } from '../api/kaltura-request-options';
import { KalturaMultiRequest } from '../api/kaltura-multi-request';
import { KalturaRequest } from '../api/kaltura-request';
import { KalturaFileRequest } from '../api/kaltura-file-request';
import { CancelableAction } from '../cancelable-action';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { environment } from '../environment';


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
	        apiVersion: environment.request.apiVersion,
            format: 1
        }
    );
}

export function createCancelableAction<T>(data : { endpoint : string, headers : any, body : any} ) : CancelableAction<T> {
	const result = new CancelableAction<T>((resolve, reject) => {
		const xhr = new XMLHttpRequest();
		let isComplete = false;

		xhr.onreadystatechange = function () {
			if (xhr.readyState === 4) {
				if (isComplete) {
					return;
				}
				isComplete = true;

				let resp;

				try {
					if (xhr.status === 200) {
						resp = JSON.parse(xhr.response);
					} else {
						resp = new KalturaClientException('client::requre-failure', xhr.responseText || 'failed to transmit request');
					}
				} catch (e) {
					resp = new Error(xhr.responseText);
				}

				if (resp instanceof Error || resp instanceof KalturaAPIException) {
					reject({error: resp});
				} else {
					resolve(resp);
				}
			}
		};

		xhr.open('POST', data.endpoint);

		if (data.headers) {
			Object.keys(data.headers).forEach(headerKey => {
				const headerValue = data.headers[headerKey];
				xhr.setRequestHeader(headerKey, headerValue);
			});
		}

		xhr.send(JSON.stringify(data.body));

		return () => {
			if (!isComplete) {
				isComplete = true;
				xhr.abort();
			}
		};
	});

	return result;
}
