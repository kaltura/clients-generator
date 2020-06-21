import { KalturaAPIException } from "./kaltura-api-exception";

export class KalturaResponse<T> {

    constructor(public result : T, public error : KalturaAPIException)
    {


    }
}
