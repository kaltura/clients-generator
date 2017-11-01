import { KalturaRequest, KalturaRequestArgs } from "./kaltura-request";
import { KalturaObjectBase } from "./kaltura-object-base";

export type ProgressCallback = (loaded: number, total: number) => void;

export interface KalturaUploadRequestArgs extends KalturaRequestArgs {
  uploadedFileSize?: number;
}

export class KalturaUploadRequest<T> extends KalturaRequest<T> {
    private _progressCallback: ProgressCallback;
    public uploadedFileSize: number = 0;

    constructor(data: KalturaUploadRequestArgs, {responseType, responseSubType, responseConstructor}: { responseType: string, responseSubType?: string, responseConstructor: { new(): KalturaObjectBase } }) {
        super(data, {responseType, responseSubType, responseConstructor});
        this.uploadedFileSize = data.uploadedFileSize;
    }

    setProgress(callback: ProgressCallback): this {
        this._progressCallback = callback;
        return this;
    }

    public _getProgressCallback(): ProgressCallback {
        return this._progressCallback;
    }

    public supportChunkUpload(): boolean {
        // chunk upload currently assume support according to request/reseponse properties. Should get this information from the client-generator directly.
        const {properties} = this._getMetadata();
        const responseSupportChunk = this._responseConstructor ? (new this._responseConstructor()).hasMetadataProperty("uploadedFileSize") : false;
        return responseSupportChunk
            && !!properties["resume"]
            && !!properties["resumeAt"]
            && !!properties["finalChunk"];
    }

    public getFilePropertyName(): string {
        const metadataProperties = this._getMetadata().properties;
        return Object.keys(metadataProperties).find(propertyName => metadataProperties[propertyName].type === "f");
    }

    public getFileData(): File {
        const filePropertyName = this.getFilePropertyName();
        return filePropertyName ? this[filePropertyName] : null;
    }

    public getFormData(): FormData {
        let result = null;
        const filePropertyName = this.getFilePropertyName();

        if (filePropertyName) {
            const file = this[filePropertyName];

            if (file) {
                result = new FormData();
                result.append("fileName", file.name);
                result.append(filePropertyName, file);
            }
        }

        return result;
    }

    public toRequestObject(): {} {
        const result = super.toRequestObject();
        const filePropertyName = this.getFilePropertyName();

        if (filePropertyName) {
            delete result[filePropertyName];
        }

        return result;
    }
}