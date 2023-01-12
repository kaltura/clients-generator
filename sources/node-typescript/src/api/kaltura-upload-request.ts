import { KalturaRequest, KalturaRequestArgs } from "./kaltura-request";
import { KalturaObjectBase } from "./kaltura-object-base";

export interface KalturaUploadRequestArgs extends KalturaRequestArgs {
  uploadedFileSize?: number;
}

export class KalturaUploadRequest<T> extends KalturaRequest<T> {
  public uploadedFileSize: number = 0;

  constructor(data: KalturaUploadRequestArgs, { responseType, responseSubType, responseConstructor }: { responseType: string, responseSubType?: string, responseConstructor: { new(): KalturaObjectBase } }) {
    super(data, { responseType, responseSubType, responseConstructor });
    this.uploadedFileSize = data.uploadedFileSize;
  }

  public getFileInfo(): { file: File, propertyName: string } {
    const metadataProperties = this._getMetadata().properties;
    const filePropertyName = Object.keys(metadataProperties).find(propertyName => metadataProperties[propertyName].type === "f");
    return filePropertyName ? { propertyName: filePropertyName, file: this[filePropertyName] } : null;
  }

  public toRequestObject(): {} {
    const result = super.toRequestObject();
    const { propertyName: filePropertyName } = this.getFileInfo();

    if (filePropertyName) {
      delete result[filePropertyName];
    }

    return result;
  }
}