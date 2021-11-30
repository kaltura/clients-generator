import { Test, TestingModule } from '@nestjs/testing';
import { KalturaClient } from '../kaltura-client.service';
import { HttpModule } from '@nestjs/common';
import { NestJsClientModule } from '../nestjs-client-module';
import { OttUserLoginAction } from '..';
import { catchError } from 'rxjs/operators';

describe('KalturaClientService', () => {
  let app: TestingModule;
  let clientService: KalturaClient;

  beforeAll(async () => {
    app = await Test.createTestingModule({
      imports: [
        HttpModule,
        NestJsClientModule.forRoot(),
      ],
      providers: [
        KalturaClient,
        // { provide: HttpService, useClass: StubHttpService },
      ],
    }).compile();

    clientService = app.get<KalturaClient>(KalturaClient);
    clientService.setOptions({
      endpointUrl: 'ec2-34-215-211-235.us-west-2.compute.amazonaws.com',
      clientTag: 'TVMng',
    });

  });

  describe('Login', () => {

    it('Should successfully login', async (done) => {
      const loginPayload = { partnerId: 1483, username: 'danduh', password: '1qaz@WSX' };
      const loginRequest = clientService.request(new OttUserLoginAction(loginPayload));
      loginRequest
        .subscribe((response) => {
          expect(response).toHaveProperty('debugInfo.beExecutionTime');
          expect(response).toHaveProperty('result.user');
          expect(response).toHaveProperty('result.loginSession');
          done();
        });

    });

    it('Should get login Error', async (done) => {
      const loginPayload = { partnerId: 1483, username: 'danduhd', password: 'ssss' };
      const loginRequest = clientService.request(new OttUserLoginAction(loginPayload));
      loginRequest
        .pipe(
          catchError((err) => {
            expect(err).toHaveProperty('error.message');
            expect(err).toHaveProperty('error.code', '1011');
            expect(err).toHaveProperty('debugInfo.beExecutionTime');
            throw err;
          }),
        )
        .subscribe(
          (response) => {
            done();
          }, (error) => {
            done();
          });

    });

  });
});
