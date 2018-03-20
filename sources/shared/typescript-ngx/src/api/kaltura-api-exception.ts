import { KalturaTypesFactory } from './kaltura-types-factory';
import { KalturaObjectBase, KalturaObjectMetadata } from './kaltura-object-base';
export class KalturaAPIException extends KalturaObjectBase {
    constructor(public code?: string, public message?: string, public args?: any) {
        super();
    }

    protected _getMetadata() : KalturaObjectMetadata
    {
        const result = super._getMetadata();
        Object.assign(
            result.properties,
            {
                code : { readOnly: true, type: 's' },
                message : { type: 's' }
            });

        return result;
    }
}
KalturaTypesFactory.registerType('KalturaAPIException',KalturaAPIException);