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

export function createEndpoint(request: KalturaRequestBase, options: KalturaClientOptions, service: string, action?: string, additionalQueryParams?: {}): string {
  const endpoint = options.endpointUrl;
  const clientTag = createClientTag(request, options);
  let url = `${endpoint}/api_v3/service/${service}`;

  if (action) {
    url += `/action/${action}`;
  }

  const queryParams = {
    ...(additionalQueryParams || {}),
    ...(clientTag ? { clientTag } : {})
  };

  return buildUrl(url, queryParams);
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

export function safeJsonParse(obj: string): object {
  try {
    return JSON.parse(obj)
  } catch (e) {/* noop */}
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
      headers: data.headers,
    })

    let xMe, xKalturaSession
    cancelableRequest.then((response) => {
      xMe = response?.headers?.['x-me'] || ''
      xKalturaSession = response?.headers?.['x-kaltura-session'] || ''
      Logger.debug(`Kaltura response completed for: ${service}/${action}, x-me: ${xMe}, x-kaltura-session: ${xKalturaSession}, url: ${endPoint}`)
    }).catch(e => {
      xMe = e?.response?.headers?.['x-me'] || ''
      xKalturaSession = e?.response?.headers?.['x-kaltura-session'] || ''
    })

    cancelableRequest[responseType]()
      .then(function (response) {
        if (typeof response === 'string' && response.includes('KalturaAPIException')) {
          response = safeJsonParse(response)
        }
        if (response?.objectType === 'KalturaAPIException') {
          Logger.error(`Kaltura API Exception: '${response.message || ''}', for: ${service}/${action}, x-me: ${xMe}, x-kaltura-session: ${xKalturaSession}, url: ${endPoint}`)
        }
        return response
      })
      .then(<ResolveFn<any>>resolve)
      .catch(e => {
        Logger.error(`Kaltura Error: '${e.message}', for: ${service}/${action}, x-me: ${xMe}, x-kaltura-session: ${xKalturaSession}, url: ${endPoint}`)
        const args = xMe || xKalturaSession ? { xMe, xKalturaSession } : undefined 
        const error = e.response?.statusCode === 200
          ? new Error(e.response?.body)
          : new KalturaClientException('client::failure', e.response?.body || e.message || 'failed to transmit request', args);
        reject(error)
      })
    return () => cancelableRequest.cancel()
  });
  return result;
}
