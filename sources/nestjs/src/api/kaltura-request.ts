import { KalturaResponse } from './kaltura-response';
import { KalturaRequestBase, KalturaRequestBaseArgs } from './kaltura-request-base';
import { KalturaAPIException } from './kaltura-api-exception';
import { KalturaObjectBase } from './kaltura-object-base';
import { KalturaRequestOptions, KalturaRequestOptionsArgs } from './kaltura-request-options';
import { environment } from '../environment';
import { createClientTag } from '../adapters/utils';

// tslint:disable-next-line:no-empty-interface
export interface KalturaRequestArgs extends KalturaRequestBaseArgs {

}

export abstract class KalturaRequest<T> extends KalturaRequestBase {

  // tslint:disable-next-line:variable-name
  private __requestOptions__: KalturaRequestOptions;
  protected callback: (response: KalturaResponse<T>) => void;
  private responseType: string;
  private responseSubType: string;
  // tslint:disable-next-line:variable-name
  protected _responseConstructor: { new(): KalturaObjectBase }; // NOTICE: this property is not used directly. It is here to force import of that type for bundling issues.

  constructor(data: KalturaRequestBaseArgs, { responseType, responseSubType, responseConstructor }: { responseType: string, responseSubType?: string, responseConstructor: { new(): KalturaObjectBase } }) {
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
          return new KalturaAPIException(response.result.error.message, response.result.error.code);
        } else {
          return response.result;
        }
      } else if (response && response.hasOwnProperty('error')) {
        return response.error;
      }
    }

    return response;
  }

  parseServerResponse(response: any): { status: boolean, response: any } {
    try {

      const unwrappedResponse = this._unwrapResponse(response);

      if (unwrappedResponse instanceof KalturaAPIException) {
        // handle situation when multi request propagated actual api exception object.
        return { status: false, response: unwrappedResponse };
      }

      if (unwrappedResponse && unwrappedResponse.objectType === 'KalturaAPIException') {
        return {
          status: false,
          response: new KalturaAPIException(
            unwrappedResponse.message,
            unwrappedResponse.code,
            unwrappedResponse.args,
          ),
        };
      }

      const parsedResponse = unwrappedResponse ? super._parseResponseProperty(
        '',
        {
          type: this.responseType,
          subType: this.responseSubType,
        },
        unwrappedResponse,
      ) : undefined;

      if (!parsedResponse && this.responseType !== 'v') {
        return {
          status: false,
          response: new KalturaAPIException(`server response is undefined, expected '${this.responseType} / ${this.responseSubType}'`,
            'client::response_type_error', null),
        };
      }

      return { status: true, response: parsedResponse };
    } catch (ex) {
      return {
        status: false,
        response: new KalturaAPIException(ex.message, 'client::general_error', null),
      };
    }
  }

  handleResponse(response: any, returnRawResponse: boolean = false): KalturaResponse<T> {
    const responseData = response['data'];
    const debugInfo = {
      beExecutionTime: response['data'].executionTime,
    };
    let result: KalturaResponse<T>;

    if (returnRawResponse) {
      result = new KalturaResponse(responseData, undefined, debugInfo);
    } else {
      const parsedResponse = this.parseServerResponse(responseData);

      result = parsedResponse.status ?
        new KalturaResponse<T>(parsedResponse.response, undefined, debugInfo) :
        new KalturaResponse<T>(undefined, parsedResponse.response, debugInfo);
    }

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

  buildRequest(defaultRequestOptions: KalturaRequestOptions | null, clientTag?: string): {} {
    const requestOptionsObject = this.__requestOptions__ ? this.__requestOptions__.toRequestObject() : {};
    const defaultRequestOptionsObject = defaultRequestOptions ? defaultRequestOptions.toRequestObject() : {};

    const result = Object.assign(
      {},
      defaultRequestOptionsObject,
      requestOptionsObject,
      this.toRequestObject(),
    );

    if (environment.request.ottMode) {
      result['clientTag'] = createClientTag(this, clientTag);
    }

    return result;

  }
}
