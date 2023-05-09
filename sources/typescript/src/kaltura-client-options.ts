export interface KalturaClientOptions {
    clientTag: string;
    endpointUrl: string;
    chunkFileSize?: number;
    chunkFileDisabled?: boolean;
    parallelUploadsDisabled?: boolean;
    maxConcurrentUploadConnections?: number;
}
