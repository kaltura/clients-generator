import {ThumbAssetServeAction} from "../lib/api/types/ThumbAssetServeAction";
import { asyncAssert, getClient } from "./utils";
import {LoggerSettings, LogLevels} from "../lib/api/kaltura-logger";
import {KalturaClient} from "../lib/kaltura-client.service";
import { environment } from '../lib/environment';


describe("Kaltura File request", () => {
  let kalturaClient: KalturaClient = null;

  beforeAll(async () => {
    LoggerSettings.logLevel = LogLevels.error; // suspend warnings

    return new Promise((resolve => {
      getClient()
        .subscribe(client => {
          kalturaClient = client;
          resolve(client);
        });
    }));
  });

  afterAll(() => {
    kalturaClient = null;
  });

  test("thumbasset service > serve action", (done) => {

    const thumbRequest = new ThumbAssetServeAction({
      thumbAssetId: "1_ep9epsxy"
    });

    kalturaClient.setDefaultRequestOptions({ks: "ks123"});

    expect.assertions(3);

    kalturaClient.request(thumbRequest)
      .subscribe(
        result => {
          asyncAssert(() => {
            expect(result).toBeDefined();
            expect(result.url).toBeDefined();
            expect(result.url).toBe(`https://www.kaltura.com/api_v3/service/thumbasset/action/serve?format=1&clientTag=ngx-tests&ks=ks123&thumbAssetId=1_ep9epsxy&apiVersion=${environment.request.apiVersion}`);
          });

          done();
        },
        error => {
          fail(error);
        });

  });

  test("error when sending 'KalturaFileRequest' as part of multi-request", (done) => {

    const thumbRequest: any = new ThumbAssetServeAction({
      thumbAssetId: "thumbAssetId"
    });

    expect.assertions(3);

    kalturaClient.multiRequest([thumbRequest])
      .subscribe(
        result => {
          done.fail("got response instead of error");
        },
        error => {
          asyncAssert(() => {
            expect(error).toBeDefined();
            expect(error).toBeInstanceOf(Error);
            expect(error.message).toBe("multi-request not support requests of type 'KalturaFileRequest', use regular request instead");
          });
          done();
        });

  });
});
