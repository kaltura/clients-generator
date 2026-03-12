
export class KalturaClientException extends Error {
    constructor(public code: string, public message: string, public args?: any) {
        super(message);

        // Set the prototype explicitly.
        Object.setPrototypeOf(this, KalturaClientException.prototype);
    }
}