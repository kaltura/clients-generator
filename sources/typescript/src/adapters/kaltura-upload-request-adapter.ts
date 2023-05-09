import { KalturaUploadRequest } from "../api/kaltura-upload-request";
import { KalturaClientException } from "../api/kaltura-client-exception";
import { KalturaRequestOptions } from "../api/kaltura-request-options";
import { KalturaClientOptions } from "../kaltura-client-options";
import { KalturaAPIException } from "../api/kaltura-api-exception";

export class KalturaUploadRequestAdapter {

    protected _chunkUploadSupported(request: KalturaUploadRequest<any>): boolean {
        // SUPPORTED BY BROWSER?
        // Check if these features are support by the browser:
        // - File object type
        // - Blob object type
        // - FileList object type
        // - slicing files
        const supportedByBrowser = (
            (typeof(File) !== "undefined")
            &&
            (typeof(Blob) !== "undefined")
            &&
            (typeof(FileList) !== "undefined")
            &&
            (!!(<any>Blob.prototype)["webkitSlice"] || !!(<any>Blob.prototype)["mozSlice"] || !!(<any>Blob.prototype).slice || false)
        );
        const supportedByRequest = request.supportChunkUpload();
        const enabledInClient = !this.clientOptions.chunkFileDisabled;

        return enabledInClient && supportedByBrowser && supportedByRequest;
    }

    constructor(public clientOptions: KalturaClientOptions, public defaultRequestOptions: KalturaRequestOptions) {}

    protected _handleFinalChunkResponse(request: KalturaUploadRequest<any>, result, reject: (reason: Error) => any, resolve: (value: any) => any) {
        try {
            const response = request.handleResponse(result);

            if (response.error) {
                reject(response.error);
            } else {
                resolve(response.result);
            }
        } catch (error) {
            if (error instanceof KalturaClientException || error instanceof KalturaAPIException) {
                reject(error);
            } else {
                const errorMessage = error instanceof Error ? error.message : typeof error === "string" ? error : null;
                reject(new KalturaClientException("client::response-unknown-error", errorMessage || "Failed to parse response"));
            }
        }
    }

    protected _getFormData(filePropertyName: string, fileName: string, fileChunk: File | Blob): FormData {
        const result = new FormData();
        result.append("fileName", fileName);
        result.append(filePropertyName, fileChunk);
        return result;
    }

    protected _prependParametersToFormData(formData: FormData, parameters: Record<string, any>): FormData {
        const result = new FormData();
        for (let key in parameters) {
            result.append(key, parameters[key]);
        }

        // @ts-ignore
        const it = formData.entries();
        let val;
        while (true) {
            val = it.next();
            if (val.done) {
                break;
            }
            result.append(val.value[0], val.value[1]);
        }

        return result;
    }

    protected _getChunkSize() {
        const userChunkFileSize = this.clientOptions ? this.clientOptions.chunkFileSize : null;

        if (userChunkFileSize && Number.isFinite(userChunkFileSize) && !Number.isNaN(userChunkFileSize)) {
            if (userChunkFileSize < 1e5) {
                console.warn(`user requested for invalid upload chunk size '${userChunkFileSize}'. minimal value 100Kb. using minimal value 100Kb instead`);
                return 1e5;
            } else {
                console.log(`using user requested chunk size '${userChunkFileSize}'`);
                return userChunkFileSize;
            }
        } else {
            console.log(`using default chunk size 5Mb`);
            return 5e6; // default
        }
    }
}
