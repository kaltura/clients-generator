import { KalturaBrowserHttpClient } from "../kaltura-clients/kaltura-browser-http-client";
import { FlavorAssetListAction } from "../types/FlavorAssetListAction";
import { KalturaFlavorAssetListResponse } from "../types/KalturaFlavorAssetListResponse";
import { KalturaFlavorAsset } from "../types/KalturaFlavorAsset";
import { KalturaAssetFilter } from "../types/KalturaAssetFilter";
import { getClient } from "./utils";
import { LoggerSettings, LogLevels } from "../kaltura-logger";

describe(`service "Flavor" tests`, () => {
  let kalturaClient: KalturaBrowserHttpClient = null;

  beforeAll(async () => {
    LoggerSettings.logLevel = LogLevels.error; // suspend warnings

    return getClient()
      .then(client => {
        kalturaClient = client;
      }).catch(error => {
        // can do nothing since jasmine will ignore any exceptions thrown from before all
      });
  });

  afterAll(() => {
    kalturaClient = null;
  });

  test("flavor list", (done) => {
    const filter = new KalturaAssetFilter({
      entryIdEqual: "1_2vp1gp7u"
    });
    kalturaClient.request(new FlavorAssetListAction({ filter }))
      .then(
        response => {
          expect(response instanceof KalturaFlavorAssetListResponse).toBeTruthy();
          expect(Array.isArray(response.objects)).toBeTruthy();
          expect(response.objects.every(obj => obj instanceof KalturaFlavorAsset)).toBeTruthy();
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
  });
});
