import { KalturaResponse } from "./kaltura-response";
import { KalturaRequestBase, KalturaRequestBaseArgs } from "./kaltura-request-base";
import { KalturaAPIException } from './kaltura-api-exception';
import { KalturaObjectBase } from './kaltura-object-base';

export interface KalturaRequestArgs extends KalturaRequestBaseArgs
{

}


export abstract class KalturaRequest<T> extends KalturaRequestBase {

    protected callback: (response: KalturaResponse<T>) => void;
    private responseType : string;
    private responseSubType : string;
    private _responseConstructor : { new() : KalturaObjectBase}; // NOTICE: this property is not used directly. It is here to force import of that type for bundling issues.

    constructor(data : KalturaRequestBaseArgs, {responseType, responseSubType, responseConstructor} : {responseType : string, responseSubType? : string, responseConstructor : { new() : KalturaObjectBase}  } ) {
        super(data);
        this.responseSubType = responseSubType;
        this.responseType = responseType;
        this._responseConstructor = responseConstructor;
    }

    setCompletion(callback: (response: KalturaResponse<T>) => void): this {
        this.callback = callback;
        return this;
    }

    private _unwrapResponse(response : any) : any
    {
        // if response is object without 'objectType' property and with 'result' property -> it is ott response
        if (response && typeof response === 'object' && !response.objectType && response.result)
        {
            // if response.result is object without 'objectType' property and with 'error' property -> it is ott error response
            if (typeof response.result === 'object' && !response.result.objectType && response.result.error) {
                return response.result.error;
            }else
            {
                return response.result;
            }
        }else {
            return response;
        }
    }

    handleResponse(response: any): KalturaResponse<T> {
        let responseResult: any;
        let responseError: any;

        try {
            const unwrappedResponse = this._unwrapResponse(response);
            let responseObject = null;

            if (unwrappedResponse) {
                if (unwrappedResponse.objectType === 'KalturaAPIException') {
                    responseObject = super._parseResponseProperty(
                        "",
                        {
                            type: 'o',
                            subType: 'KalturaAPIException'
                        },
                        unwrappedResponse
                    );
                } else {
                    responseObject = super._parseResponseProperty(
                        "",
                        {
                            type: this.responseType,
                            subType: this.responseSubType
                        },
                        unwrappedResponse
                    );
                }
            }

            if (!responseObject && this.responseType !== 'v') {
                responseError = new KalturaAPIException('client::response_type_error', `server response is undefined, expected '${this.responseType} / ${this.responseSubType}'`);
            } else if (responseObject instanceof KalturaAPIException) {
                    // got exception from library
                    responseError = responseObject;
            }else {
                responseResult = responseObject;
            }
        } catch (ex) {
            // TODO [kmc] should implement
            responseError = new KalturaAPIException('client::general_error', ex.message);
        }


        const result = new KalturaResponse<T>(responseResult, responseError);

        if (this.callback) {
            try {
                this.callback(result);
            } catch (ex) {
                // do nothing by design
            }
        }

        return result;
    }


}
