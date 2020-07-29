import { KalturaRequestBase } from '../api/kaltura-request-base';
import { KalturaClientOptions } from '../kaltura-client-options';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaMultiRequest } from '../api/kaltura-multi-request';
import { KalturaRequest } from '../api/kaltura-request';
import { KalturaFileRequest } from '../api/kaltura-file-request';
import { environment } from '../environment';
import { KalturaAPIException } from '../api/kaltura-api-exception';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { Observable } from 'rxjs';

export type CreateEndpointOptions = KalturaClientOptions & {
  service: string,
  action?: string,
  format?: string,
  avoidClientTag?: boolean
}

export function createEndpoint(request: KalturaRequestBase, options: CreateEndpointOptions): string {
  const endpoint = options.endpointUrl;
  const clientTag = createClientTag(request, options.clientTag);
  let result = `${endpoint}/api_v3/service/${options.service}`;

  if (options.action) {
    result += `/action/${options.action}`;
  }

  const format = options.format || request.getFormatValue();

  result += `?format=${format}`;

  if (!options.avoidClientTag && clientTag) {
    result += `&${_buildQuerystring({clientTag})}`;
  }

  return result;
}

export function createClientTag(request: KalturaRequestBase, clientTag?: string)
{
	const networkTag = (request.getNetworkTag() || "").trim();
	clientTag = (clientTag || "").trim() || "ng-app";

	if (networkTag && networkTag.length)
	{
		return `${clientTag}_${networkTag}`;
	}else {
		return clientTag;
	}
}

function _buildQuerystring(data: {}, prefix?: string) {
	let str = [], p;
	for (p in data) {
		if (data.hasOwnProperty(p)) {
			let k = prefix ? prefix + "[" + p + "]" : p, v = data[p];
			str.push((v !== null && typeof v === "object") ?
				_buildQuerystring(v, k) :
				encodeURIComponent(k) + "=" + encodeURIComponent(v));
		}
	}
	return str.join("&");

}

export function buildUrl(url: string, querystring?: {}) {
  let formattedUrl = (url).trim();
  const urlHasQuerystring = formattedUrl.indexOf('?') !== -1;
  if (!querystring) {
    return formattedUrl;
  }

  const formattedQuerystring = _buildQuerystring(querystring);
  return `${formattedUrl}${urlHasQuerystring ? '&' : '?'}${formattedQuerystring}`;
}

export function getHeaders(): any {
	return {
		"Accept": "application/json",
		"Content-Type": "application/json"
	};
}

export function prepareParameters(request: KalturaRequest<any> | KalturaMultiRequest | KalturaFileRequest,  options: KalturaClientOptions,  defaultRequestOptions: KalturaRequestOptions | null): any {

	return Object.assign(
		{},
		request.buildRequest(defaultRequestOptions, options.clientTag),
		{
			apiVersion: environment.request.apiVersion,
		}
	);
}

export function fetchRequest<T>(data : { method: string, endpoint : string, headers : any, body : any} ) : Observable<T> {
  const result = new Observable<T>(observer => {
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
          observer.error(resp);
        } else {
          observer.next(resp);
          observer.complete();
        }
      }
    };

    xhr.open(data.method, data.endpoint);

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
        observer.complete();
        xhr.abort();
      }
    };
  });

  return result;
}
