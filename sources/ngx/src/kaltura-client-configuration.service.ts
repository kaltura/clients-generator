import { Injectable } from '@angular/core';
import { KalturaHttpClientBaseConfiguration } from './api/kaltura-clients/kaltura-http-client-base';


@Injectable()
export class KalturaClientConfiguration implements KalturaHttpClientBaseConfiguration {
    public clientTag: string = '';
    public endpointUrl: string = '';
    constructor() {

    }

}