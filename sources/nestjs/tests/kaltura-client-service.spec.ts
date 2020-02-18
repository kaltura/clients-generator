import { Test, TestingModule } from '@nestjs/testing';
import { KalturaClient } from '../kaltura-client.service';
import { HttpModule } from '@nestjs/common';
import { NestJsClientModule } from '../nestjs-client-module';
import { AssetStructListAction, MetaListAction, OttUserLoginAction } from '..';
import { catchError } from 'rxjs/operators';

describe('KalturaClientService', () => {
  let app: TestingModule;
  let clientService: KalturaClient;
  let ks: string;
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
      endpointUrl: 'http://ec2-54-185-249-222.us-west-2.compute.amazonaws.com',
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
          ks = response['result'].loginSession.ks;
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

    it('Should run multirequest', async (done) => {
      const multiRequest = clientService.multiRequest(
        [new MetaListAction().setRequestOptions({ ks }),
          new AssetStructListAction().setRequestOptions({ ks })],
      );
      multiRequest
        .subscribe(
          (response) => {
            expect(response).toBeInstanceOf(Array);
            expect(response.length).toBe(2);
            expect(response[0]).toHaveProperty('result');
            expect(response).toHaveProperty('debugInfo');
            expect(response).toHaveProperty('debugInfo.beExecutionTime');
            done();
          }, done.fail);
    });

  });
});
