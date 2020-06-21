import { KalturaObjectBase } from "./kaltura-object-base";
import { KalturaRequestBase, KalturaRequestBaseArgs } from './kaltura-request-base';
import { KalturaRequest, KalturaRequestArgs } from './kaltura-request';
import { environment } from '../environment';

export interface KalturaFileRequestArgs extends KalturaRequestArgs  {
}

export class KalturaFileRequest extends KalturaRequest<{url: string}> {

    constructor(data: KalturaFileRequestArgs) {
        super(data, {responseType : 'v', responseSubType : '', responseConstructor : null });
    }

    public getFormatValue() {
      return environment.request.fileFormatValue;
    }
}
