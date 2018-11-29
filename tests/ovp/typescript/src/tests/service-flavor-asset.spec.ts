import { KalturaClient } from "../kaltura-client-service";
import { FlavorAssetListAction } from "../api/types/FlavorAssetListAction";
import { KalturaFlavorAssetListResponse } from "../api/types/KalturaFlavorAssetListResponse";
import { KalturaFlavorAsset } from "../api/types/KalturaFlavorAsset";
import { KalturaAssetFilter } from "../api/types/KalturaAssetFilter";
import { getClient } from "./utils";
import { LoggerSettings, LogLevels } from "../api/kaltura-logger";
import { asyncAssert } from "./utils";

describe(`service "Flavor" tests`, () => {
  let kalturaClient: KalturaClient = null;

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
	  expect.assertions(4);
    kalturaClient.request(new FlavorAssetListAction({ filter }))
      .then(
        response => {
	        asyncAssert(() => {
		        expect(response instanceof KalturaFlavorAssetListResponse).toBeTruthy();
		        expect(Array.isArray(response.objects)).toBeTruthy();
		        expect(response.objects.length).toBeGreaterThan(0);
		        expect(response.objects[0] instanceof KalturaFlavorAsset).toBeTruthy();
	        });
          done();
        },
        (error) => {
          done.fail(error);
        }
      );
  });
});
