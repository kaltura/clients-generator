import { KalturaObjectBase, KalturaObjectBaseArgs } from './kaltura-object-base';
import { Logger } from "./kaltura-logger";

export interface KalturaRequestBaseArgs extends KalturaObjectBaseArgs { }

export class KalturaRequestBase extends KalturaObjectBase {

  private _networkTag: string;

  setNetworkTag(tag: string): this {
    if (!tag || tag.length > 10) {
      Logger.debug(`cannot set network tag longer than 10 characters. ignoring tag '${tag}`);
    } else {
      this._networkTag = tag;
    }

    return this;
  }

  getNetworkTag(): string {
    return this._networkTag;
  }
}

