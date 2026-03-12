import { InjectionToken } from '@angular/core';
import { KalturaObjectMetadata, KalturaObjectBase, KalturaObjectBaseArgs } from './kaltura-object-base';

export const KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS: InjectionToken<KalturaRequestOptionsArgs> = new InjectionToken('kaltura client default request options');

export interface KalturaRequestOptionsArgs extends KalturaObjectBaseArgs {
  acceptedTypes?: { new(...args: any[]): KalturaObjectBase }[];
  partnerId?: number;
  userId?: number;
  language?: string;
  currency?: string;
  ks?: string;
  responseProfile?: KalturaObjectBase;
  abortOnError?: boolean;
  abortAllOnError?: boolean;
  skipCondition?: KalturaObjectBase;
}

export class KalturaRequestOptions extends KalturaObjectBase {

  acceptedTypes: { new(...args: any[]): KalturaObjectBase }[] = [];
  partnerId?: number;
  userId?: number;
  language?: string;
  currency?: string;
  ks?: string;
  responseProfile?: KalturaObjectBase;
  abortOnError?: boolean;
  abortAllOnError?: boolean;
  skipCondition?: KalturaObjectBase;

  constructor(data?: KalturaRequestOptionsArgs) {
    super(data);
    if (typeof this.acceptedTypes === 'undefined') {
      this.acceptedTypes = [];
    }
  }

  protected _getMetadata(): KalturaObjectMetadata {
    const result = super._getMetadata();
    Object.assign(
      result.properties,
      {
        partnerId: { type: 'n' },
        userId: { type: 'n' },
        language: { type: 's' },
        currency: { type: 's' },
        ks: { type: 's' },
        responseProfile: { type: 'o', subTypeConstructor: KalturaObjectBase, subType: 'KalturaBaseResponseProfile' },
        abortOnError: { type: 'b' },
        abortAllOnError: { type: 'b' },
        skipCondition: { type: 'o', subTypeConstructor: KalturaObjectBase, subType: 'KalturaSkipCondition' },
      },
    );
    return result;
  }
}
