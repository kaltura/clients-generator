import { KalturaResponse } from "./kaltura-response";
import { KalturaRequest } from "./kaltura-request";
import { KalturaRequestBase } from "./kaltura-request-base";

import { KalturaMultiResponse } from "./kaltura-multi-response";
import { KalturaAPIException } from "./kaltura-api-exception";
import { KalturaObjectMetadata } from './kaltura-object-base';
import { KalturaRequestOptions } from './kaltura-request-options';
import { environment } from '../environment';


export class KalturaMultiRequest extends KalturaRequestBase {

    protected callback: (response: KalturaMultiResponse) => void;

    requests: KalturaRequest<any>[] = [];

    constructor(...args: KalturaRequest<any>[]) {
        super({});
        this.requests = args;
    }

    buildRequest(defaultRequestOptions: KalturaRequestOptions): {} {
        const result = super.toRequestObject();

        for (let i = 0, length = this.requests.length; i < length; i++) {
            result[i] = this.requests[i].buildRequest(defaultRequestOptions);
        }

        return result;
    }

    protected _getMetadata() : KalturaObjectMetadata
    {
        const result = super._getMetadata();
        Object.assign(
            result.properties,
            {
                service : { default : 'multirequest', type : 'c'  }
            });

        return result;

    }

    private _unwrapResponse(response: any): any {
        if (environment.response.nestedResponse) {
            if (response && response.hasOwnProperty('result')) {
                return response.result;
            } else if (response && response.hasOwnProperty('error')) {
                return response.error;
            }
        }

        return response;
    }

    setCompletion(callback: (response: KalturaMultiResponse) => void): KalturaMultiRequest {
        this.callback = callback;
        return this;
    }

    handleResponse(responses: any): KalturaMultiResponse {
        const kalturaResponses: KalturaResponse<any>[] = [];

        const unwrappedResponse = this._unwrapResponse(responses);
        let responseObject = null;

        if (!unwrappedResponse || !(unwrappedResponse instanceof Array) || unwrappedResponse.length !== this.requests.length) {
            const response = new KalturaAPIException(`server response is invalid, expected array of ${this.requests.length}`, 'client::response_type_error', null);
            for (let i = 0, len = this.requests.length; i < len; i++) {
                kalturaResponses.push(this.requests[i].handleResponse(response));
            }
        }
        else {

            for (let i = 0, len = this.requests.length; i < len; i++) {
                const serverResponse = unwrappedResponse[i];
                kalturaResponses.push(this.requests[i].handleResponse(serverResponse));
            }

            if (this.callback) {
                try {
                    this.callback(new KalturaMultiResponse(kalturaResponses));
                } catch (ex) {
                    // do nothing by design
                }
            }
        }

        return new KalturaMultiResponse(kalturaResponses);
    }
}
