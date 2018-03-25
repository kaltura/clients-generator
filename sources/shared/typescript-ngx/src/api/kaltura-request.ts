import { KalturaResponse } from "./kaltura-response";
import { KalturaRequestBase, KalturaRequestBaseArgs } from "./kaltura-request-base";
import { KalturaAPIException } from './kaltura-api-exception';
import { KalturaObjectBase } from './kaltura-object-base';
import { KalturaRequestOptions, KalturaRequestOptionsArgs } from './kaltura-request-options';
import { environment } from '../environment';

export interface KalturaRequestArgs extends KalturaRequestBaseArgs
{

}


export abstract class KalturaRequest<T> extends KalturaRequestBase {

    private __requestOptions__: KalturaRequestOptions;
    protected callback: (response: KalturaResponse<T>) => void;
    private responseType : string;
    private responseSubType : string;
    protected _responseConstructor : { new() : KalturaObjectBase}; // NOTICE: this property is not used directly. It is here to force import of that type for bundling issues.

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

    private _unwrapResponse(response: any): any {
        if (environment.response.nestedResponse) {
            if (response && response.hasOwnProperty('result')) {
                if (response.result.hasOwnProperty('error')) {
                    return response.result.error;
                } else {
                    return response.result;
                }
            } else if (response && response.hasOwnProperty('error')) {
                return response.error;
            }
        }

        return response;
    }

    handleResponse(response: any): KalturaResponse<T> {
        let responseResult: any;
        let responseError: any;

        try {
            const unwrappedResponse = this._unwrapResponse(response);
            let responseObject = null;

            if (unwrappedResponse) {
                if (unwrappedResponse instanceof KalturaAPIException)
                {
                    // handle situation when multi request propagated actual api exception object.
                    responseObject = unwrappedResponse;
                }else if (unwrappedResponse.objectType === 'KalturaAPIException') {
                    responseObject = new KalturaAPIException(
                        unwrappedResponse.message,
                        unwrappedResponse.code,
                        unwrappedResponse.args
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
                responseError = new KalturaAPIException(`server response is undefined, expected '${this.responseType} / ${this.responseSubType}'`, 'client::response_type_error', null);
            } else if (responseObject instanceof KalturaAPIException) {
                // got exception from library
                responseError = responseObject;
            }else {
                responseResult = responseObject;
            }
        } catch (ex) {
            responseError = new KalturaAPIException(ex.message, 'client::general_error', null);
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

    setRequestOptions(optionArgs: KalturaRequestOptionsArgs): this;
    setRequestOptions(options: KalturaRequestOptions): this;
    setRequestOptions(arg: KalturaRequestOptionsArgs | KalturaRequestOptions): this {
        this.__requestOptions__ = arg instanceof KalturaRequestOptions ? arg : new KalturaRequestOptions(arg);
        return this;
    }

    getRequestOptions(): KalturaRequestOptions {
        return this.__requestOptions__;
    }

    buildRequest(defaultRequestOptions: KalturaRequestOptions): {} {
        const requestOptionsObject = this.__requestOptions__ ? this.__requestOptions__.toRequestObject() : {};
        const defaultRequestOptionsObject = defaultRequestOptions ? defaultRequestOptions.toRequestObject() : {};

        return Object.assign(
            {},
            defaultRequestOptionsObject,
            requestOptionsObject,
            this.toRequestObject()
        );
    }
}
