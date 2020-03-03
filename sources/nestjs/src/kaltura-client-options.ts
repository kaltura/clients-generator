export const KALTURA_CLIENT_OPTIONS = 'KALTURA_CLIENT_OPTIONS';
export const KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS = 'KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS';
export interface KalturaClientOptions {
  clientTag: string;
  endpointUrl: string;
  chunkFileSize?: number;
  chunkFileDisabled?: boolean;
}
