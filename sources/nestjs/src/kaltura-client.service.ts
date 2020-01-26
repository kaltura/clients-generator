import { throwError as observableThrowError, Observable } from 'rxjs';
import { KalturaRequest } from './api/kaltura-request';
import { KalturaResponseType } from './api/types/KalturaResponseType';
import { KalturaMultiRequest } from './api/kaltura-multi-request';
import { KalturaMultiResponse } from './api/kaltura-multi-response';
import { KalturaFileRequest } from './api/kaltura-file-request';
import { KalturaUploadRequest } from './api/kaltura-upload-request';
import { KalturaRequestAdapter } from './adapters/kaltura-request-adapter';
import { KalturaFileRequestAdapter } from './adapters/kaltura-file-request-adapter';
import { KalturaClientOptions, KALTURA_CLIENT_OPTIONS, KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS } from './kaltura-client-options';
import { KalturaMultiRequestAdapter } from './adapters/kaltura-multi-request-adapter';
import { KalturaClientException } from './api/kaltura-client-exception';
import { KalturaUploadRequestAdapter } from './adapters/kaltura-upload-request-adapter';
import {
  KalturaRequestOptions,
  KalturaRequestOptionsArgs,
} from './api/kaltura-request-options';
import { HttpService, Inject, Injectable, Optional } from '@nestjs/common';

@Injectable()
export class KalturaClient {

  private defaultRequestOptions: KalturaRequestOptions;

  constructor(private http: HttpService,
              @Inject(KALTURA_CLIENT_OPTIONS) @Optional() private options: KalturaClientOptions,
              @Inject(KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS) @Optional() defaultRequestOptionsArgs: KalturaRequestOptionsArgs) {
    this.defaultRequestOptions = new KalturaRequestOptions(defaultRequestOptionsArgs || {});
  }

  public appendOptions(options: KalturaClientOptions): void {
    if (!options) {
      throw new KalturaClientException('client::append_options', `missing required argument 'options'`);
    }

    this.options = Object.assign(
      this.options || {}, options,
    );
  }

  public setOptions(options: KalturaClientOptions): void {
    if (!options) {
      throw new KalturaClientException('client::set_options', `missing required argument 'options'`);
    }

    this.options = options;
  }

  public appendDefaultRequestOptions(args: KalturaRequestOptionsArgs): void {
    if (!args) {
      throw new KalturaClientException('client::append_default_request_options', `missing required argument 'args'`);
    }

    this.defaultRequestOptions = Object.assign(
      this.defaultRequestOptions || new KalturaRequestOptions(), new KalturaRequestOptions(args),
    );
  }

  public setDefaultRequestOptions(args: KalturaRequestOptionsArgs): void {
    if (!args) {
      throw new KalturaClientException('client::set_default_request_options', `missing required argument 'args'`);
    }

    this.defaultRequestOptions = new KalturaRequestOptions(args);
  }

  private _validateOptions(): Error | null {
    if (!this.options) {
      return new KalturaClientException('client::missing_options',
        'cannot transmit request, missing client options (did you forgot to provide options manually or using KALTURA_CLIENT_OPTIONS?)');
    }

    if (!this.options.endpointUrl) {
      return new KalturaClientException('client::missing_options',
        `cannot transmit request, missing 'endpointUrl' in client options`);
    }

    if (!this.options.clientTag) {
      return new KalturaClientException('client::missing_options',
        `cannot transmit request, missing 'clientTag' in client options`);
    }

    return null;
  }

  public request<T>(request: KalturaRequest<T>): Observable<T>;
  public request<T>(request: KalturaFileRequest): Observable<{ url: string }>;
  public request<T>(request: KalturaRequest<any>, format: KalturaResponseType, responseType: 'blob' | 'text'): Observable<any>;
  public request<T>(request: KalturaRequest<T> | KalturaFileRequest, format?: KalturaResponseType, responseType?: 'blob' | 'text'):
    Observable<T | { url: string }> {

    const optionsViolationError = this._validateOptions();

    if (optionsViolationError) {
      return observableThrowError(optionsViolationError);
    }

    if (typeof format !== 'undefined') {
      return new KalturaRequestAdapter(this.http).transmit(request, this.options, this.defaultRequestOptions, format + '', responseType);
    }

    if (request instanceof KalturaFileRequest) {
      return new KalturaFileRequestAdapter().transmit(request, this.options, this.defaultRequestOptions);

    } else if (request instanceof KalturaUploadRequest) {
      return new KalturaUploadRequestAdapter(this.options, this.defaultRequestOptions).transmit(request);
    } else if (request instanceof KalturaRequest) {
      return new KalturaRequestAdapter(this.http).transmit(request, this.options, this.defaultRequestOptions);
    } else {
      return observableThrowError(new KalturaClientException('client::request_type_error',
        'unsupported request type requested'));
    }
  }

  public multiRequest(requests: Array<KalturaRequest<any>>): Observable<KalturaMultiResponse>;
  public multiRequest(request: KalturaMultiRequest): Observable<KalturaMultiResponse>;
  public multiRequest(arg: KalturaMultiRequest | Array<KalturaRequest<any>>): Observable<KalturaMultiResponse> {
    const optionsViolationError = this._validateOptions();
    if (optionsViolationError) {
      return observableThrowError(optionsViolationError);
    }

    const request = arg instanceof KalturaMultiRequest ? arg : (arg instanceof Array ? new KalturaMultiRequest(...arg) : null);
    if (!request) {
      return observableThrowError(new KalturaClientException('client::invalid_request',
        `Expected argument of type Array or KalturaMultiRequest`));
    }

    const containsFileRequest = request.requests.some(item => item instanceof KalturaFileRequest);
    if (containsFileRequest) {
      return observableThrowError(new KalturaClientException('client::invalid_request',
        `multi-request not support requests of type 'KalturaFileRequest', use regular request instead`));
    } else {
      return new KalturaMultiRequestAdapter(this.http).transmit(request, this.options, this.defaultRequestOptions);
    }
  }
}
