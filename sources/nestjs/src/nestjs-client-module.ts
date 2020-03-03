import { KalturaClient } from './kaltura-client.service';

import { KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS, KALTURA_CLIENT_OPTIONS, KalturaClientOptions } from './kaltura-client-options';
import { KalturaRequestOptionsArgs } from './api/kaltura-request-options';
import { DynamicModule, HttpModule, Module, Optional } from '@nestjs/common';

@Module({
  imports: [
    HttpModule,
  ],
  exports: [],
  providers: [],
})
export class NestJsClientModule {

  constructor(@Optional() module: NestJsClientModule) {
    if (module) {
      throw new Error('\'KalturaClientModule\' module imported twice.');
    }
  }

  static forRoot(clientOptionsFactory?: () => KalturaClientOptions,
                 defaultRequestOptionsArgsFactory?: () => KalturaRequestOptionsArgs): DynamicModule {
    return {
      module: NestJsClientModule,
      providers: [
        KalturaClient,
        {
          provide: KALTURA_CLIENT_OPTIONS,
          useFactory: clientOptionsFactory,
        },
        {
          provide: KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS,
          useFactory: defaultRequestOptionsArgsFactory,
        },
      ],
      exports: [KalturaClient],
    };
  }
}
