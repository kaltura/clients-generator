import { KalturaResponse } from "./kaltura-response";

export class KalturaMultiResponse extends Array<KalturaResponse<any>> {
    constructor(results: KalturaResponse<any>[] = []) {
        super();

        if (new.target) {
            // Set the prototype explicitly - see: https://github.com/Microsoft/TypeScript/wiki/FAQ#why-doesnt-extending-built-ins-like-error-array-and-map-work
            Object.setPrototypeOf(this, new.target.prototype);
        }

        if (results && results.length > 0) {
            this.push(...results);
        }
    }

    public hasErrors(): boolean {
        return this.filter(result => result.error).length > 0;
    }


}
