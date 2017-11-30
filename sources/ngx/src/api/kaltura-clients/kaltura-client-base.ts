import { KalturaRequest } from "../kaltura-request";
import { KalturaMultiRequest } from "../kaltura-multi-request";
import { KalturaRequestBase } from "../kaltura-request-base";
import { KalturaAPIException } from '../kaltura-api-exception';
import { KalturaUploadRequest } from '../kaltura-upload-request';
import { CancelableAction } from '../utils/cancelable-action';

export interface KalturaClientBaseConfiguration
{
    clientTag : string;
}

export abstract class KalturaClientBase {

    ks : string;
    partnerId : number;
    public clientTag : string;

    constructor(config :  KalturaClientBaseConfiguration)
    {
        this.clientTag = config.clientTag;
    }

    protected abstract _transmitFileUploadRequest(request : KalturaUploadRequest<any>): CancelableAction;
    protected abstract _transmitRequest(request : KalturaRequestBase): CancelableAction;

    protected _multiRequest(arg: KalturaMultiRequest | KalturaRequest<any>[]): CancelableAction {

        let request = arg instanceof KalturaMultiRequest ? arg : (arg instanceof Array ? new KalturaMultiRequest(...arg) : null);

        if (!request)
        {
            throw new Error(`Missing or invalid argument`);
        }

        let transmitAction = this.transmit(request);

        transmitAction.then(
            result => {
                return request.handleResponse(result);
            },
            (error) => {
                throw error;
            }
        );

        return transmitAction;
    }

    protected _request<T>(request: KalturaRequest<T>): CancelableAction {
        let transmitAction = this.transmit(request);

        transmitAction.then(
            (result) =>
            {
                const response: any = request.handleResponse(result);

                if (response.error) {
                    throw response.error;
                } else {
                    return response.result;
                }
            },
            (error) => {
                let kalturaAPIException;
                if (error instanceof KalturaAPIException) {
                    kalturaAPIException = error;
                } else if (error instanceof Error && error.message) {

                    kalturaAPIException = new KalturaAPIException("client::unknown-error", error.message);
                } else {
                    const errorMessage = typeof error === 'string' ? error : 'Error connecting to server';
                    kalturaAPIException = new KalturaAPIException("client::unknown-error", errorMessage);
                }

                throw kalturaAPIException;
            }
        );

        return transmitAction;
    }


    private transmit(request: KalturaRequestBase): CancelableAction {
        if (request instanceof KalturaUploadRequest) {
            return this._transmitFileUploadRequest(request);
        } else if (request instanceof KalturaRequest || request instanceof KalturaMultiRequest) {
            return this._transmitRequest(request);
        } else {
            throw new KalturaAPIException("client::request_type_error", 'unsupported request type requested');
        }
    }

    protected _assignDefaultParameters(parameters : any) : any
    {
        if (parameters) {
            if (this.ks && typeof parameters['ks'] === 'undefined') {
                parameters.ks = this.ks;
            }

            if (this.partnerId && typeof parameters['partnerId'] === 'undefined') {
                parameters.partnerId = this.partnerId;
            }
        }

        if (this.clientTag) {
            parameters.clientTag = this.clientTag;
        }

        return parameters;
    }
}
