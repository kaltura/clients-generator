import { KalturaRequest } from './api/kaltura-request';
import { KalturaMultiRequest } from './api/kaltura-multi-request';
import { KalturaMultiResponse } from './api/kaltura-multi-response';
import { KalturaFileRequest } from './api/kaltura-file-request';
import { KalturaUploadRequest } from './api/kaltura-upload-request';
import { KalturaRequestAdapter } from './adapters/kaltura-request-adapter';
import { KalturaFileRequestAdapter } from './adapters/kaltura-file-request-adapter';
import { KalturaClientOptions } from './kaltura-client-options';
import { KalturaMultiRequestAdapter } from './adapters/kaltura-multi-request-adapter';
import { KalturaClientException } from './api/kaltura-client-exception';
import { KalturaUploadRequestAdapter } from './adapters/kaltura-upload-request-adapter';
import {
    KalturaRequestOptions,
    KalturaRequestOptionsArgs
} from './api/kaltura-request-options';
import { CancelableAction } from './cancelable-action';
import { KalturaSequenceUploadRequestAdapter } from "./adapters/kaltura-sequence-upload-request-adapter";
import { KalturaParallelUploadRequestAdapter } from "./adapters/kaltura-parallel-upload-request-adapter";

export class KalturaClient {

    private _defaultRequestOptions: KalturaRequestOptions;

    constructor(private _options?: KalturaClientOptions,
                defaultRequestOptionsArgs?: KalturaRequestOptionsArgs) {
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
            return new KalturaClientException('client::missing_options','cannot transmit request, missing client options (did you forgot to provide options manually?)');
        }

        if (!this._options.endpointUrl) {
            return new KalturaClientException('client::missing_options', `cannot transmit request, missing 'endpointUrl' in client options`);
        }

        if (!this._options.clientTag) {
            return new KalturaClientException('client::missing_options', `cannot transmit request, missing 'clientTag' in client options`);
        }

        return null;
    }

    public request<T>(request: KalturaRequest<T>): CancelableAction<T>;
    public request<T>(request: KalturaFileRequest): CancelableAction<{ url: string }>;
    public request<T>(request: KalturaRequest<T> | KalturaFileRequest): CancelableAction<T | { url: string }> {

        const optionsViolationError = this._validateOptions();

        if (optionsViolationError) {
            return CancelableAction.reject(optionsViolationError);
        }

        if (request instanceof KalturaFileRequest) {
            return new KalturaFileRequestAdapter().transmit(request, this._options, this._defaultRequestOptions);

        } else if (request instanceof KalturaUploadRequest) {
            if (this._options.parallelUploadsDisabled) {
                return new KalturaSequenceUploadRequestAdapter(this._options, this._defaultRequestOptions).transmit(request);
            }
            return new KalturaParallelUploadRequestAdapter(this._options, this._defaultRequestOptions).transmit(request);

        }
        else if (request instanceof KalturaRequest) {
            return new KalturaRequestAdapter().transmit(request, this._options, this._defaultRequestOptions);
        } else {
            return CancelableAction.reject(new KalturaClientException("client::request_type_error", 'unsupported request type requested'));
        }
    }

    public multiRequest(requests: KalturaRequest<any>[]): CancelableAction<KalturaMultiResponse>
    public multiRequest(request: KalturaMultiRequest): CancelableAction<KalturaMultiResponse>;
    public multiRequest(arg: KalturaMultiRequest | KalturaRequest<any>[]): CancelableAction<KalturaMultiResponse> {
        const optionsViolationError = this._validateOptions();
        if (optionsViolationError) {
            return CancelableAction.reject(optionsViolationError);
        }

        const request = arg instanceof KalturaMultiRequest ? arg : (arg instanceof Array ? new KalturaMultiRequest(...arg) : null);
        if (!request) {
            return CancelableAction.reject(new KalturaClientException('client::invalid_request', `Expected argument of type Array or KalturaMultiRequest`));
        }

        const containsFileRequest = request.requests.some(item => item instanceof KalturaFileRequest);
        if (containsFileRequest) {
            return CancelableAction.reject(new KalturaClientException('client::invalid_request', `multi-request not support requests of type 'KalturaFileRequest', use regular request instead`));
        } else {
            return new KalturaMultiRequestAdapter().transmit(request, this._options, this._defaultRequestOptions);
        }
    }
}
