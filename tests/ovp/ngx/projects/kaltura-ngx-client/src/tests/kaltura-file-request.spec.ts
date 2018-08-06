import {ThumbAssetServeAction} from "../api/types/ThumbAssetServeAction";
import {getClient} from "./utils";
import {LoggerSettings, LogLevels} from "../api/kaltura-logger";
import {KalturaClient} from "../kaltura-client.service";
import "rxjs/add/observable/throw";

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

    kalturaClient.setDefaultRequestOptions({ks: "YWIyZDAxYWRhZmQ1NzhjMzQ5ZmI3Nzc4MzVhYTJkMGI1NDdhYzA5YnwxNzYzMzIxOzE3NjMzMjE7MTUxMjA1MzA1MzsyOzE1MTE5NjY2NTMuNTk7YWRtaW47ZGlzYWJsZWVudGl0bGVtZW50Ozs"});

    kalturaClient.request(thumbRequest)
      .subscribe(
        result => {
          expect(result).toBeDefined();
          expect(result.url).toBeDefined();
          expect(result.url).toBe("https://www.kaltura.com/api_v3/service/thumbasset/action/serve?clientTag=ngx-tests?apiVersion=3.3.0&ks=YWIyZDAxYWRhZmQ1NzhjMzQ5ZmI3Nzc4MzVhYTJkMGI1NDdhYzA5YnwxNzYzMzIxOzE3NjMzMjE7MTUxMjA1MzA1MzsyOzE1MTE5NjY2NTMuNTk7YWRtaW47ZGlzYWJsZWVudGl0bGVtZW50Ozs&thumbAssetId=1_ep9epsxy&format=1");

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

    kalturaClient.multiRequest([thumbRequest])
      .subscribe(
        result => {
          fail("got response instead of error");
        },
        error => {
          expect(error).toBeDefined();
          expect(error).toBeInstanceOf(Error);
          expect(error.message).toBe("multi-request not support requests of type 'KalturaFileRequest', use regular request instead");
          done();
        });

  });
});
