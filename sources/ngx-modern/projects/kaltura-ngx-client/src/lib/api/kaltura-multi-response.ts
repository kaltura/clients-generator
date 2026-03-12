import { KalturaResponse } from "./kaltura-response";
import { KalturaAPIException } from './kaltura-api-exception';

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

    public getFirstError(): KalturaAPIException {
        let result: KalturaAPIException = null;
        for (let i = 0; i < this.length; i++) {
            result = this[i].error;

            if (result) {
                break;
            }
        }
        return result;
    }


}
