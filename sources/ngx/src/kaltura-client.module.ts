import { NgModule, Optional, SkipSelf, ModuleWithProviders } from '@angular/core';
import { KalturaClient } from './kaltura-client.service';
import { HttpClientModule } from '@angular/common/http';
import { KALTURA_CLIENT_OPTIONS, KalturaClientOptions } from './kaltura-client-options';
import { KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS, KalturaRequestOptionsArgs } from './api/kaltura-request-options';


@NgModule({
    imports: <any[]>[
        HttpClientModule
    ],
    declarations: <any[]>[
    ],
    exports: <any[]>[
    ],
    providers: <any[]>[
    ]
})
export class KalturaClientModule {

    constructor(@Optional() @SkipSelf() module: KalturaClientModule) {
        if (module) {
            throw new Error("'KalturaClientModule' module imported twice.");
        }
    }

    static forRoot(clientOptionsFactory?: () => KalturaClientOptions, defaultRequestOptionsArgsFactory?: () => KalturaRequestOptionsArgs): ModuleWithProviders {
        return {
            ngModule: KalturaClientModule,
            providers: [
                KalturaClient,
                KALTURA_CLIENT_OPTIONS ? {
                    provide: KALTURA_CLIENT_OPTIONS,
                    useFactory: clientOptionsFactory
                } : [],
                KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS? {
                    provide: KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS,
                    useFactory: defaultRequestOptionsArgsFactory
                } : []
            ]
        };
    }
}
