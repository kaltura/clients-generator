import { KalturaRequestBase } from '../api/kaltura-request-base';
import { KalturaClientOptions } from '../kaltura-client-options';
import { KalturaRequestOptions } from '../api/kaltura-request-options';
import { KalturaMultiRequest } from '../api/kaltura-multi-request';
import { KalturaRequest } from '../api/kaltura-request';
import { KalturaFileRequest } from '../api/kaltura-file-request';
import { CancelableAction, ResolveFn } from '../cancelable-action';
import { KalturaClientException } from '../api/kaltura-client-exception';
import { environment } from '../environment';
import got from 'got';
import { Logger } from "../api/kaltura-logger";

export function createEndpoint(request: KalturaRequestBase, options: KalturaClientOptions, service: string, action?: string, additionalQueryparams?: {}): string {
  const endpoint = options.endpointUrl;
  const clientTag = createClientTag(request, options);
  let url = `${endpoint}/api_v3/service/${service}`;

  if (action) {
    url += `/action/${action}`;
  }

  const queryparams = {
    ...(additionalQueryparams || {}),
    ...(clientTag ? { clientTag } : {})
  };

  return buildUrl(url, queryparams);
}

export function buildUrl(url: string, querystring?: {}) {
  let formattedUrl = (url).trim();
  if (!querystring) {
    return formattedUrl;
  }
  const urlHasQuerystring = formattedUrl.indexOf('?') !== -1;
  const formattedQuerystring = _buildQuerystring(querystring);
  return `${formattedUrl}${urlHasQuerystring ? '&' : '?'}${formattedQuerystring}`;
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

export function createClientTag(request: KalturaRequestBase, options: KalturaClientOptions) {
  const networkTag = (request.getNetworkTag() || "").trim();
  const clientTag = (options.clientTag || "").trim() || "ng-app";

  if (networkTag && networkTag.length) {
    return `${clientTag}_${networkTag}`;
  } else {
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

export function getHeaders(customHeaders = {}): any {
  const headers = {
    "Accept": "application/json",
    "Content-Type": "application/json",
    ...customHeaders
  };
  return headers
}

export function prepareParameters(request: KalturaRequest<any> | KalturaMultiRequest | KalturaFileRequest, options: KalturaClientOptions, defaultRequestOptions: KalturaRequestOptions): any {
  return Object.assign(
    {},
    request.buildRequest(defaultRequestOptions),
    {
      apiVersion: environment.request.apiVersion,
      format: 1
    }
  );
}

export function createCancelableAction<T>(
  data: { endpoint: string, headers: any, body: any },
  responseType: 'json'|'text' = 'json'
): CancelableAction<T> {
  const endPoint = data?.endpoint || ''
  const service = endPoint?.match('service/([^\/]+)')?.[1] || ''
  const action = endPoint?.match('action/([^\?]+)')?.[1] || ''

  const result = new CancelableAction<T>((resolve, reject) => {
    const cancelableRequest = got.post(data.endpoint, {
      json: data.body,
      headers: data.headers
    })

    cancelableRequest.then((response) => {
      const xMe = response?.headers?.['x-me'] || ''
      const xKalturaSession = response?.headers?.['x-kaltura-session'] || ''
      // TODO - change to 'debug' level once we are able to print headers for BE Application Errors.
      Logger.error(`Kaltura BE response for: ${service}/${action}, x-me: ${xMe}, x-kaltura-session: ${xKalturaSession}, url: ${endPoint}`)
    }).catch(e => {
      const xMe = e?.response?.headers?.['x-me'] || ''
      const xKalturaSession = e?.response?.headers?.['x-kaltura-session'] || ''
      Logger.error(`Kaltura BE failed for: ${service}/${action} error: ${e?.message || e} x-me: ${xMe}, x-kaltura-session: ${xKalturaSession}, url: ${endPoint}`)
    })

    cancelableRequest[responseType]()
      .then(<ResolveFn<any>>resolve)
      .catch(e => {
        const error = e.response?.statusCode === 200
          ? new Error(e.response?.body)
          : new KalturaClientException('client::failure', e.response?.body || e.message || 'failed to transmit request');
        reject(error)
      })
    return () => cancelableRequest.cancel()
  });
  return result;
}
