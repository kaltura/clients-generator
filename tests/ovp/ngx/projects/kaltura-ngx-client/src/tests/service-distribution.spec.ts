import {DistributionProviderListAction} from "../lib/api/types/DistributionProviderListAction";
import {KalturaDistributionProviderListResponse} from "../lib/api/types/KalturaDistributionProviderListResponse";
import {KalturaDistributionProvider} from "../lib/api/types/KalturaDistributionProvider";
import {DistributionProfileListAction} from "../lib/api/types/DistributionProfileListAction";
import {KalturaDistributionProfileListResponse} from "../lib/api/types/KalturaDistributionProfileListResponse";
import {KalturaDistributionProfile} from "../lib/api/types/KalturaDistributionProfile";
import {EntryDistributionListAction} from "../lib/api/types/EntryDistributionListAction";
import {KalturaEntryDistributionListResponse} from "../lib/api/types/KalturaEntryDistributionListResponse";
import {KalturaEntryDistribution} from "../lib/api/types/KalturaEntryDistribution";
import { asyncAssert, getClient } from "./utils";
import {LoggerSettings, LogLevels} from "../lib/api/kaltura-logger";
import {KalturaClient} from "../lib/kaltura-client.service";

describe(`service "Distribution" tests`, () => {
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

  test("distribution provider list", (done) => {
    expect.assertions(3);
    kalturaClient.request(new DistributionProviderListAction())
      .subscribe(
        response => {
          asyncAssert(() => {
            expect(response instanceof KalturaDistributionProviderListResponse).toBeTruthy();
            expect(Array.isArray(response.objects)).toBeTruthy();
            expect(response.objects.every(obj => obj instanceof KalturaDistributionProvider)).toBeTruthy();
          });
          done();
        },
        (error) => {
          done.fail(error);
        });
  });

  test("distribution profile list", (done) => {
    expect.assertions(3);
    kalturaClient.request(new DistributionProfileListAction())
      .subscribe(
        response => {
          asyncAssert(() => {
            expect(response instanceof KalturaDistributionProfileListResponse).toBeTruthy();
            expect(Array.isArray(response.objects)).toBeTruthy();
            expect(response.objects.every(obj => obj instanceof KalturaDistributionProfile)).toBeTruthy();
          });
          done();
        },
        () => {
          done.fail("should not reach this part");
        });
  });

  test("entry distribution list", (done) => {
    expect.assertions(3);
    kalturaClient.request(new EntryDistributionListAction())
      .subscribe(
        response => {
          asyncAssert(() => {
            expect(response instanceof KalturaEntryDistributionListResponse).toBeTruthy();
            expect(Array.isArray(response.objects)).toBeTruthy();
            expect(response.objects.every(obj => obj instanceof KalturaEntryDistribution)).toBeTruthy();
          });
          done();
        },
        () => {
          done.fail("should not reach this part");
        });
  });
});
