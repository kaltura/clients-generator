import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';
import { KalturaClient } from 'kaltura-ngx-client';
import { KALTURA_CLIENT_OPTIONS, KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS } from 'kaltura-ngx-client';

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    KalturaClient,
    {
      provide: KALTURA_CLIENT_OPTIONS,
      useValue: {
        endpointUrl: '',
        clientTag: 'ngx-modern-test-app'
      }
    },
    {
      provide: KALTURA_CLIENT_DEFAULT_REQUEST_OPTIONS,
      useValue: {}
    }
  ]
}).catch(err => console.error(err));
