export class KalturaAPIException extends Error {
    constructor(public message: string, public code: string, public args: any) {
        super(message);

        // Set the prototype explicitly.
        Object.setPrototypeOf(this, KalturaAPIException.prototype);
    }
}