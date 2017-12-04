import { KalturaBrowserHttpClient } from "../kaltura-clients/kaltura-browser-http-client";
import { DistributionProviderListAction } from "../types/DistributionProviderListAction";
import { KalturaDistributionProviderListResponse } from "../types/KalturaDistributionProviderListResponse";
import { KalturaDistributionProvider } from "../types/KalturaDistributionProvider";
import { DistributionProfileListAction } from "../types/DistributionProfileListAction";
import { KalturaDistributionProfileListResponse } from "../types/KalturaDistributionProfileListResponse";
import { KalturaDistributionProfile } from "../types/KalturaDistributionProfile";
import { EntryDistributionListAction } from "../types/EntryDistributionListAction";
import { KalturaEntryDistributionListResponse } from "../types/KalturaEntryDistributionListResponse";
import { KalturaEntryDistribution } from "../types/KalturaEntryDistribution";
import { getClient } from "./utils";
import { LoggerSettings, LogLevels } from "../kaltura-logger";

describe(`service "Distribution" tests`, () => {
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

  test("distribution provider list", (done) => {
    kalturaClient.request(new DistributionProviderListAction())
      .then(
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
      .then(
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
      .then(
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
