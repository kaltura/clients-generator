import { KalturaRequest, KalturaRequestArgs } from "./kaltura-request";

export interface KalturaFileRequestArgs extends KalturaRequestArgs {
}

export class KalturaFileRequest extends KalturaRequest<{ url: string }> {

  constructor(data: KalturaFileRequestArgs) {
    super(data, {responseType: "v", responseSubType: "", responseConstructor: null});
  }

}
