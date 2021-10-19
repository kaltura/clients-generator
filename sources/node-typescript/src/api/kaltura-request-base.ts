import { KalturaObjectBase, KalturaObjectBaseArgs } from './kaltura-object-base';
import { KalturaLogger } from './kaltura-logger';

export interface KalturaRequestBaseArgs extends KalturaObjectBaseArgs { }
const logger = new KalturaLogger('KalturaRequestBase');

export class KalturaRequestBase extends KalturaObjectBase {

  private _networkTag: string;

  constructor(data: KalturaRequestBaseArgs) {
    super(data);
  }

  setNetworkTag(tag: string): this {
    if (!tag || tag.length > 10) {
      logger.warn(`cannot set network tag longer than 10 characters. ignoring tag '${tag}`);
    } else {
      this._networkTag = tag;
    }

    return this;
  }

  getNetworkTag(): string {
    return this._networkTag;
  }
}

