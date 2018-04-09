import { Inject, Injectable, Optional, Self } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import { HttpClient } from '@angular/common/http';
import { KalturaRequest } from './api/kaltura-request';
import { KalturaMultiRequest } from './api/kaltura-multi-request';
import { KalturaMultiResponse } from './api/kaltura-multi-response';
import { KalturaFileRequest } from './api/kaltura-file-request';
import { KalturaUploadRequest } from './api/kaltura-upload-request';
import { KalturaRequestAdapter } from './adapters/kaltura-request-adapter';
import { KalturaFileRequestAdapter } from './adapters/kaltura-file-request-adapter';
import { KalturaClientOptions, KALTURA_CLIENT_OPTIONS } from './kaltura-client-options';
import { KalturaMultiRequestAdapter } from './adapters/kaltura-multi-request-adapter';
import { KalturaClientException } from './api/kaltura-client-exception';
import { KalturaUploadRequestAdapter } from './adapters/kaltura-upload-request-adapter';
import {
    KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS, KalturaRequestOptions,
    KalturaRequestOptionsArgs
} from './api/kaltura-request-options';

@Injectable()
export class KalturaClient {

    private _defaultRequestOptions: KalturaRequestOptions;

    constructor(private _http: HttpClient, @Inject(KALTURA_CLIENT_OPTIONS) @Optional() @Self() private _options: KalturaClientOptions,
                @Inject(KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS) @Optional() @Self()  defaultRequestOptionsArgs: KalturaRequestOptionsArgs) {
        this._defaultRequestOptions = new KalturaRequestOptions(defaultRequestOptionsArgs || {});
    }

    public appendOptions(options: KalturaClientOptions): void {
        if (!options) {
            throw new KalturaClientException('client::append_options',`missing required argument 'options'`);
        }

        this._options = Object.assign(
            this._options || {}, options
        );
    }

    public setOptions(options: KalturaClientOptions): void {
        if (!options) {
            throw new KalturaClientException('client::set_options',`missing required argument 'options'`);
        }

        this._options = options;
    }

    public appendDefaultRequestOptions(args: KalturaRequestOptionsArgs): void {
        if (!args) {
            throw new KalturaClientException('client::append_default_request_options',`missing required argument 'args'`);
        }

        this._defaultRequestOptions = Object.assign(
            this._defaultRequestOptions || new KalturaRequestOptions(), new KalturaRequestOptions(args)
        );
    }

    public setDefaultRequestOptions(args: KalturaRequestOptionsArgs): void {
        if (!args) {
            throw new KalturaClientException('client::set_default_request_options',`missing required argument 'args'`);
        }

        this._defaultRequestOptions = new KalturaRequestOptions(args);
    }

    private _validateOptions(): Error | null {
        if (!this._options) {
            return new KalturaClientException('client::missing_options','cannot transmit request, missing client options (did you forgot to provide options manually or using KALTURA_CLIENT_OPTIONS?)');
        }

        if (!this._options.endpointUrl) {
            return new KalturaClientException('client::missing_options', `cannot transmit request, missing 'endpointUrl' in client options`);
        }

        if (!this._options.clientTag) {
            return new KalturaClientException('client::missing_options', `cannot transmit request, missing 'clientTag' in client options`);
        }

        return null;
    }

    public request<T>(request: KalturaRequest<T>): Observable<T>;
    public request<T>(request: KalturaFileRequest): Observable<{ url: string }>;
    public request<T>(request: KalturaRequest<T> | KalturaFileRequest): Observable<T | { url: string }> {

        const optionsViolationError = this._validateOptions();

        if (optionsViolationError) {
            return Observable.throw(optionsViolationError);
        }

        if (request instanceof KalturaFileRequest) {
            return new KalturaFileRequestAdapter().transmit(request, this._options, this._defaultRequestOptions);

        } else if (request instanceof KalturaUploadRequest) {
            return new KalturaUploadRequestAdapter(this._options, this._defaultRequestOptions).transmit(request);
        }
        else if (request instanceof KalturaRequest) {
            return new KalturaRequestAdapter(this._http).transmit(request, this._options, this._defaultRequestOptions);
        } else {
            return Observable.throw(new KalturaClientException("client::request_type_error", 'unsupported request type requested'));
        }
    }

    public multiRequest(requests: KalturaRequest<any>[]): Observable<KalturaMultiResponse>
    public multiRequest(request: KalturaMultiRequest): Observable<KalturaMultiResponse>;
    public multiRequest(arg: KalturaMultiRequest | KalturaRequest<any>[]): Observable<KalturaMultiResponse> {
        const optionsViolationError = this._validateOptions();
        if (optionsViolationError) {
            return Observable.throw(optionsViolationError);
        }

        const request = arg instanceof KalturaMultiRequest ? arg : (arg instanceof Array ? new KalturaMultiRequest(...arg) : null);
        if (!request) {
            return Observable.throw(new KalturaClientException('client::invalid_request', `Expected argument of type Array or KalturaMultiRequest`));
        }

        const containsFileRequest = request.requests.some(item => item instanceof KalturaFileRequest);
        if (containsFileRequest) {
            return Observable.throw(new KalturaClientException('client::invalid_request', `multi-request not support requests of type 'KalturaFileRequest', use regular request instead`));
        } else {
            return new KalturaMultiRequestAdapter(this._http).transmit(request, this._options, this._defaultRequestOptions);
        }
    }
}
