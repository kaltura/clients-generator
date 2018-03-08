import { KalturaObjectBase, KalturaObjectBaseArgs } from './kaltura-object-base';
import { KalturaRequestOptions, KalturaRequestOptionsArgs } from './kaltura-request-options';


export interface KalturaRequestBaseArgs  extends KalturaObjectBaseArgs {
}


export class KalturaRequestBase extends KalturaObjectBase {
    constructor(data: KalturaRequestBaseArgs) {
        super(data);
    }
}

