import {FlavorAssetListAction} from "../api/types/FlavorAssetListAction";
import {KalturaFlavorAssetListResponse} from "../api/types/KalturaFlavorAssetListResponse";
import {KalturaFlavorAsset} from "../api/types/KalturaFlavorAsset";
import {KalturaAssetFilter} from "../api/types/KalturaAssetFilter";
import {getClient} from "./utils";
import {LoggerSettings, LogLevels} from "../api/kaltura-logger";
import {KalturaClient} from "../kaltura-client.service";

describe(`service "Flavor" tests`, () => {
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

  test("flavor list", (done) => {
    const filter = new KalturaAssetFilter({
      entryIdEqual: "1_2vp1gp7u"
    });
    kalturaClient.request(new FlavorAssetListAction({filter}))
      .subscribe(
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
