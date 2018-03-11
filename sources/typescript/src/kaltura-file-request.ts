import { KalturaObjectBase } from "./kaltura-object-base";
import { KalturaRequestBase, KalturaRequestBaseArgs } from './kaltura-request-base';



export interface KalturaFileRequestArgs extends KalturaRequestBaseArgs  {
}

export class KalturaFileRequest extends KalturaRequestBase {

    constructor(data: KalturaFileRequestArgs) {
        super(data);
    }
}