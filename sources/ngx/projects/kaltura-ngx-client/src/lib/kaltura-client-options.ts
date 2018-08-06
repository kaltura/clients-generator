import { Injectable, InjectionToken } from '@angular/core';

export const KALTURA_CLIENT_OPTIONS: InjectionToken<KalturaClientOptions> = new InjectionToken('kaltura client options');

export interface KalturaClientOptions {
    clientTag: string;
    endpointUrl: string;
    chunkFileSize?: number;
    chunkFileDisabled?: boolean;
}