import { KalturaRequest, KalturaRequestArgs } from './kaltura-request';
import { environment } from '../environment';

// tslint:disable-next-line:no-empty-interface
export interface KalturaFileRequestArgs extends KalturaRequestArgs {
}

export class KalturaFileRequest extends KalturaRequest<{ url: string }> {

  constructor(data: KalturaFileRequestArgs) {
    super(data, { responseType: 'v', responseSubType: '', responseConstructor: null });
  }

  public getFormatValue() {
    return environment.request.fileFormatValue;
  }
}
