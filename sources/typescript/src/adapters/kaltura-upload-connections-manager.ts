export class KalturaUploadConnectionsManager {
     private static _availableConnections: number;
     private static _callbacks: (() => void)[];
     public static _totalConnections: number = -1; // -1 as in not initialized

    public static setTotalConnections(n: number): void {
        // only if not yet initialized
        if (this._totalConnections === -1) {
            this._totalConnections = n;
            this._availableConnections = n;
            // this is a fine opportunity to initialize this as well:
            this._callbacks = [];
        }
    }

    public static getTotalConnections(): number {
        return this._totalConnections;
    }

    /**
     * let an adapter use a connection
     * @return true if there is an available connection, false otherwise
     */
    public static retrieveConnection(): boolean {
        if (this._availableConnections >= 1) {
            this._availableConnections -= 1;
            return true;
        }
        return false;
    }

    /**
     * return a connection to the pool
     */
    public static releaseConnection(): void {
        this._availableConnections += 1;
        if (this._availableConnections === 1) {
            // if we got here, previously there were no available connections,
            // and now there is now - notify the first item in the queue and remove it.
            if (this._callbacks.length) {
                const cb = this._callbacks.shift();
                cb();
            }
        }
    }

    /**
     * request to be notified when a connection is available.
     * callback will be automatically removed from queue once triggered.
     * @param callback
     */
    public static addAvailableConnectionsCallback(callback: () => void): void {
        this._callbacks.push(callback);
    }

    /**
     * stop waiting for free connections
     * @param callback
     */
    public static removeAvailableConnectionsCallback(callback: () => void): void {
        for (let i = 0; this._callbacks.length; i++) {
            if (this._callbacks[i] === callback) {
                this._callbacks.splice(i, 1);
                break;
            }
        }
    }

}
