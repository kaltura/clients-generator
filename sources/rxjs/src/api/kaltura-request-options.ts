
import { KalturaObjectMetadata } from './kaltura-object-base';
import { KalturaBaseResponseProfile } from './types/KalturaBaseResponseProfile';
import { KalturaSkipCondition } from './types/KalturaSkipCondition';
import { KalturaObjectBase, KalturaObjectBaseArgs } from './kaltura-object-base';

export interface KalturaRequestOptionsArgs  extends KalturaObjectBaseArgs {
    acceptedTypes? : {new(...args: any[]) : KalturaObjectBase}[];
	partnerId? : number;
	userId? : number;
	language? : string;
	currency? : string;
	ks? : string;
	responseProfile? : KalturaBaseResponseProfile;
	abortOnError? : boolean;
	abortAllOnError? : boolean;
	skipCondition? : KalturaSkipCondition;
}


export class KalturaRequestOptions extends KalturaObjectBase {

    acceptedTypes : {new(...args: any[]) : KalturaObjectBase}[];
	partnerId : number;
	userId : number;
	language : string;
	currency : string;
	ks : string;
	responseProfile : KalturaBaseResponseProfile;
	abortOnError : boolean;
	abortAllOnError : boolean;
	skipCondition : KalturaSkipCondition;

    constructor(data? : KalturaRequestOptionsArgs)
    {
        super(data);
        if (typeof this.acceptedTypes === 'undefined') this.acceptedTypes = [];
    }

    protected _getMetadata() : KalturaObjectMetadata
    {
        const result = super._getMetadata();
        Object.assign(
            result.properties,
            {
                partnerId : { type : 'n' },
				userId : { type : 'n' },
				language : { type : 's' },
				currency : { type : 's' },
				ks : { type : 's' },
				responseProfile : { type : 'o', subTypeConstructor : KalturaBaseResponseProfile, subType : 'KalturaBaseResponseProfile' },
				abortOnError : { type : 'b' },
				abortAllOnError : { type : 'b' },
				skipCondition : { type : 'o', subTypeConstructor : KalturaSkipCondition, subType : 'KalturaSkipCondition' }
            }
        );
        return result;
    }
}
