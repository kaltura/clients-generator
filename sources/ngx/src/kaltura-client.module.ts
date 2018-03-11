import { NgModule, Optional, SkipSelf, ModuleWithProviders } from '@angular/core';
import { KalturaClient } from './kaltura-client.service';
import { KalturaClientConfiguration } from './kaltura-client-configuration.service';
import { Http, Headers, Response } from '@angular/http';

@NgModule({
    imports: <any[]>[
    ],
    declarations: <any[]>[
    ],
    exports: <any[]>[
    ],
    providers: <any[]>[
    ]
})
export class KalturaClientModule {

    constructor(@Optional() @SkipSelf() module : KalturaClientModule)
    {
        if (module) {
            throw new Error("'KalturaClientModule' module imported twice.");
        }
    }

    static forRoot(configuration : KalturaClientConfiguration): ModuleWithProviders {
        return {
            ngModule: KalturaClientModule,
            providers: <any[]>[
                KalturaClient,
                {
                    provide: KalturaClientConfiguration,
                    useValue :configuration
                }
            ]
        };
    }
}
