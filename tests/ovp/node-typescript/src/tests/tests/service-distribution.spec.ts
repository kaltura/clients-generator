import { KalturaClient } from "../kaltura-client-service";
import { DistributionProviderListAction } from "../api/types/DistributionProviderListAction";
import { KalturaDistributionProviderListResponse } from "../api/types/KalturaDistributionProviderListResponse";
import { KalturaDistributionProvider } from "../api/types/KalturaDistributionProvider";
import { DistributionProfileListAction } from "../api/types/DistributionProfileListAction";
import { KalturaDistributionProfileListResponse } from "../api/types/KalturaDistributionProfileListResponse";
import { KalturaDistributionProfile } from "../api/types/KalturaDistributionProfile";
import { EntryDistributionListAction } from "../api/types/EntryDistributionListAction";
import { KalturaEntryDistributionListResponse } from "../api/types/KalturaEntryDistributionListResponse";
import { KalturaEntryDistribution } from "../api/types/KalturaEntryDistribution";
import { getClient } from "./utils";
import { LoggerSettings, LogLevels } from "../api/kaltura-logger";
import { asyncAssert } from "./utils";

describe(`service "Distribution" tests`, () => {
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

	test("distribution provider list", (done) => {
		expect.assertions(2);
		kalturaClient.request(new DistributionProviderListAction())
			.then(
				response => {
					asyncAssert(() => {
						expect(response instanceof KalturaDistributionProviderListResponse).toBeTruthy();
						expect(Array.isArray(response.objects)).toBeTruthy();

					});
					done();
				},
				(error) => {
					done.fail(error);
				});
	});

	test("distribution profile list", (done) => {
		expect.assertions(4);
		kalturaClient.request(new DistributionProfileListAction())
			.then(
				response => {
					asyncAssert(() => {
						expect(response instanceof KalturaDistributionProfileListResponse).toBeTruthy();
						expect(Array.isArray(response.objects)).toBeTruthy();
						expect(response.objects.length).toBeGreaterThan(0);
						expect(response.objects[0] instanceof KalturaDistributionProfile).toBeTruthy();
					});
					done();
				},
				() => {
					done.fail("should not reach this part");
				});
	});

	test("entry distribution list", (done) => {
		expect.assertions(2);
		kalturaClient.request(new EntryDistributionListAction())
			.then(
				response => {
					asyncAssert(() => {
						expect(response instanceof KalturaEntryDistributionListResponse).toBeTruthy();
						expect(Array.isArray(response.objects)).toBeTruthy();
					});
					done();
				},
				() => {
					done.fail("should not reach this part");
				});
	});
});
