import {FlavorAssetListAction} from "../lib/api/types/FlavorAssetListAction";
import {KalturaFlavorAssetListResponse} from "../lib/api/types/KalturaFlavorAssetListResponse";
import {KalturaFlavorAsset} from "../lib/api/types/KalturaFlavorAsset";
import {KalturaAssetFilter} from "../lib/api/types/KalturaAssetFilter";
import { asyncAssert, getClient } from "./utils";
import {LoggerSettings, LogLevels} from "../lib/api/kaltura-logger";
import {KalturaClient} from "../lib/kaltura-client.service";

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
    expect.assertions(3);
    kalturaClient.request(new FlavorAssetListAction({filter}))
      .subscribe(
        response => {
          asyncAssert(() => {
            expect(response instanceof KalturaFlavorAssetListResponse).toBeTruthy();
            expect(Array.isArray(response.objects)).toBeTruthy();
            expect(response.objects.every(obj => obj instanceof KalturaFlavorAsset)).toBeTruthy();
          });
          done();
        },
        (error) => {
          done.fail(error);
        }
      );
  });
});
