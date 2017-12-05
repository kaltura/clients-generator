import { KalturaRequest } from '../kaltura-request';
import { KalturaMultiRequest } from '../kaltura-multi-request';
import { KalturaMultiResponse } from '../kaltura-multi-response';
import { CancelableAction } from '../utils/cancelable-action';
import { KalturaHttpClientBase, KalturaHttpClientBaseConfiguration } from './kaltura-http-client-base';
import { KalturaAPIException } from '../kaltura-api-exception';
import { KalturaFileRequest } from '../kaltura-file-request';


export interface KalturaBrowserHttpClientConfiguration extends KalturaHttpClientBaseConfiguration
{
}

export class KalturaBrowserHttpClient extends KalturaHttpClientBase {


    constructor(config : KalturaBrowserHttpClientConfiguration) {
        super(config);
    }

    protected _createCancelableAction(data : { endpoint : string, headers : any, body : {}, type : any} ) : CancelableAction
    {

        const result = new CancelableAction((resolve, reject) => {
            const promise = new Promise((promiseResolve, promiseReject) => {
                const xhr = new XMLHttpRequest();

                xhr.onreadystatechange = function () {
                    if (xhr.readyState == 4) {
                        let resp;

                        try {
                            if (xhr.status == 200) {
                                resp = JSON.parse(xhr.response);
                            } else {
                                resp = new Error(xhr.responseText);
                            }
                        } catch (e) {
                            resp = new Error(xhr.responseText);
                        }

                        if (resp instanceof Error || resp instanceof KalturaAPIException) {
                            promiseReject({error: resp});
                        } else {
                            promiseResolve(resp);
                        }
                    }
                };

                xhr.open('POST', data.endpoint);

                if (data.headers)
                {
                    Object.keys(data.headers).forEach(headerKey =>
                    {
                        const headerValue = data.headers[headerKey];
                        xhr.setRequestHeader(headerKey,headerValue);
                    });
                }

                xhr.send(JSON.stringify(data.body));
            }).then(resolve, reject);

        });

        return result;
    }

    private _transmitFileRequest(request: KalturaFileRequest): { url: string } {
        const parameters: any = Object.assign(
            {
                format: 1
            },
            request.toRequestObject()
        );

        this._assignDefaultParameters(parameters);

        // build endpoint
        let endpoint = this._createEndpoint(parameters);

        delete parameters.action;
        delete parameters.service;

        return {url: `${endpoint}?${this._buildQuerystring(parameters)}`};
    }

    public request<T>(request: KalturaRequest<T>): Promise<T>;
    public request<T>(request: KalturaFileRequest): Promise<{ url: string }>;
    public request<T>(request: KalturaRequest<T> | KalturaFileRequest): Promise<T | { url: string }> {
        return new Promise((resolve, reject) => {

            if (request instanceof KalturaFileRequest) {
                const response = this._transmitFileRequest(request);
                resolve(response);
            } else {
                super._request(request).then(
                    value => {
                        resolve(value);
                    },
                    reason => {
                        reject(reason);
                    }
                );
            }

        });
    }

    public multiRequest(requests : KalturaRequest<any>[]) : Promise<KalturaMultiResponse>;
    public multiRequest(request : KalturaMultiRequest) : Promise<KalturaMultiResponse>;
    public multiRequest(arg: KalturaMultiRequest | KalturaRequest<any>[]): Promise<KalturaMultiResponse> {
        return new Promise((resolve, reject) => {

            let request = arg instanceof KalturaMultiRequest ? arg : (arg instanceof Array ? new KalturaMultiRequest(...arg) : null);

            if (!request) {
                reject(new Error(`Expected argument of type Array or KalturaMultiRequest`));
            }

            const containsFileRequest = request.requests.some(item => item instanceof KalturaFileRequest);

            if (containsFileRequest) {
                reject(new Error(`multi-request not support requests of type 'KalturaFileRequest'`));
            } else {
                super._multiRequest(arg).then(
                    value => {
                        resolve(value);
                    },
                    reason => {
                        reject(reason);
                    }
                );
            }

        });
    }

}
