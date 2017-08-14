import { KalturaResponse } from "./kaltura-response";
import { KalturaRequest } from "./kaltura-request";
import { KalturaRequestBase } from "./kaltura-request-base";

import { KalturaMultiResponse } from "./kaltura-multi-response";
import { KalturaAPIException } from "./kaltura-api-exception";
import { KalturaObjectMetadata } from './kaltura-object-base';


export class KalturaMultiRequest extends KalturaRequestBase {

    protected callback: (response: KalturaMultiResponse) => void;

    requests: KalturaRequest<any>[] = [];

    constructor(...args: KalturaRequest<any>[]) {
        super({});
        this.requests = args;
    }

    toRequestObject(): any {
        const result = super.toRequestObject();

        for (let i = 0, length = this.requests.length; i < length; i++) {
            result[i] = this.requests[i].toRequestObject();
        }

        return result;
    }

    protected _getMetadata() : KalturaObjectMetadata
    {
        const result = super._getMetadata();
        Object.assign(
            result.properties,
            {
                service : { default : 'multirequest', type : 'c'  },
                action : { default : 'null', type : 'c'  }
            });

        return result;

    }

    setCompletion(callback: (response: KalturaMultiResponse) => void): KalturaMultiRequest {
        this.callback = callback;
        return this;
    }

    handleResponse(responses: any[]): KalturaMultiResponse {
        const kalturaResponses: KalturaResponse<any>[] = [];

        if (!responses || !(responses instanceof Array) || responses.length != this.requests.length) {
            const response = new KalturaAPIException('client::response_type_error', `server response is invalid, expected array of ${this.requests.length}`);
            for (let i = 0, len = this.requests.length; i < len; i++) {
                kalturaResponses.push(this.requests[i].handleResponse(response));
            }
        }
        else {

            for (let i = 0, len = this.requests.length; i < len; i++) {
                const serverResponse = responses[i];
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
