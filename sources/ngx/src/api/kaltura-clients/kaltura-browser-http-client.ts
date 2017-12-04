import { KalturaRequest } from '../kaltura-request';
import { KalturaMultiRequest } from '../kaltura-multi-request';
import { KalturaMultiResponse } from '../kaltura-multi-response';
import { CancelableAction } from '../utils/cancelable-action';
import { KalturaHttpClientBase, KalturaHttpClientBaseConfiguration } from './kaltura-http-client-base';
import { KalturaAPIException } from '../kaltura-api-exception';


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

    public request<T>(request: KalturaRequest<T>): Promise<T> {
        return new Promise((resolve, reject) => {
            super._request(request).then(
                value => {
                    resolve(value);
                },
                reason => {
                    reject(reason);
                }
            );

        });
    }

    public multiRequest(requests : KalturaRequest<any>[]) : Promise<KalturaMultiResponse>;
    public multiRequest(request : KalturaMultiRequest) : Promise<KalturaMultiResponse>;
    public multiRequest(arg: KalturaMultiRequest | KalturaRequest<any>[]): Promise<KalturaMultiResponse> {
        return new Promise((resolve, reject) => {
            super._multiRequest(arg).then(
                value => {
                    resolve(value);
                },
                reason => {
                    reject(reason);
                }
            );

        });
    }

}
