import {DistributionProviderListAction} from "../api/types/DistributionProviderListAction";
import {KalturaDistributionProviderListResponse} from "../api/types/KalturaDistributionProviderListResponse";
import {KalturaDistributionProvider} from "../api/types/KalturaDistributionProvider";
import {DistributionProfileListAction} from "../api/types/DistributionProfileListAction";
import {KalturaDistributionProfileListResponse} from "../api/types/KalturaDistributionProfileListResponse";
import {KalturaDistributionProfile} from "../api/types/KalturaDistributionProfile";
import {EntryDistributionListAction} from "../api/types/EntryDistributionListAction";
import {KalturaEntryDistributionListResponse} from "../api/types/KalturaEntryDistributionListResponse";
import {KalturaEntryDistribution} from "../api/types/KalturaEntryDistribution";
import {getClient} from "./utils";
import {LoggerSettings, LogLevels} from "../api/kaltura-logger";
import {KalturaClient} from "../kaltura-client.service";

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
    kalturaClient.request(new DistributionProviderListAction())
      .subscribe(
        response => {
          expect(response instanceof KalturaDistributionProviderListResponse).toBeTruthy();
          expect(Array.isArray(response.objects)).toBeTruthy();
          expect(response.objects.every(obj => obj instanceof KalturaDistributionProvider)).toBeTruthy();
          done();
        },
        (error) => {
          fail(error);
          done();
        });
  });

  test("distribution profile list", (done) => {
    kalturaClient.request(new DistributionProfileListAction())
      .subscribe(
        response => {
          expect(response instanceof KalturaDistributionProfileListResponse).toBeTruthy();
          expect(Array.isArray(response.objects)).toBeTruthy();
          expect(response.objects.every(obj => obj instanceof KalturaDistributionProfile)).toBeTruthy();
          done();
        },
        () => {
          fail("should not reach this part");
          done();
        });
  });

  test("entry distribution list", (done) => {
    kalturaClient.request(new EntryDistributionListAction())
      .subscribe(
        response => {
          expect(response instanceof KalturaEntryDistributionListResponse).toBeTruthy();
          expect(Array.isArray(response.objects)).toBeTruthy();
          expect(response.objects.every(obj => obj instanceof KalturaEntryDistribution)).toBeTruthy();
          done();
        },
        () => {
          fail("should not reach this part");
          done();
        });
  });
});
