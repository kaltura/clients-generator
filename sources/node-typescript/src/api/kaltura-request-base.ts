import { KalturaObjectBase, KalturaObjectBaseArgs } from './kaltura-object-base';
import * as dbg from 'debug'

export interface KalturaRequestBaseArgs extends KalturaObjectBaseArgs { }
const debug = dbg('kaltura:base:request')

export class KalturaRequestBase extends KalturaObjectBase {

  private _networkTag: string;

  constructor(data: KalturaRequestBaseArgs) {
    super(data);
  }

  setNetworkTag(tag: string): this {
    if (!tag || tag.length > 10) {
      debug(`cannot set network tag longer than 10 characters. ignoring tag '${tag}`);
    } else {
      this._networkTag = tag;
    }

    return this;
  }

  getNetworkTag(): string {
    return this._networkTag;
  }
}

