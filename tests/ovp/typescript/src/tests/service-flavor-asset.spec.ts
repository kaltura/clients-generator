import { KalturaClient } from "../kaltura-client-service";
import { FlavorAssetListAction } from "../api/types/FlavorAssetListAction";
import { BaseEntryListAction } from "../api/types/BaseEntryListAction";
import { KalturaFlavorAssetListResponse } from "../api/types/KalturaFlavorAssetListResponse";
import { KalturaMediaEntryFilter } from "../api/types/KalturaMediaEntryFilter";
import { KalturaFlavorAsset } from "../api/types/KalturaFlavorAsset";
import { KalturaFlavorAssetFilter } from "../api/types/KalturaFlavorAssetFilter";
import { getClient } from "./utils";
import { LoggerSettings, LogLevels } from "../api/kaltura-logger";
import { asyncAssert } from "./utils";
import { KalturaResponse } from '../api';

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

		expect.assertions(4);
		kalturaClient.multiRequest(
			[
				new BaseEntryListAction({
					filter: new KalturaMediaEntryFilter({
						flavorParamsIdsMatchOr: '0'
					})
				}),
				new FlavorAssetListAction(
					{
						filter: new KalturaFlavorAssetFilter(
							{
								entryIdEqual: ''
							}
						).setDependency(['entryIdEqual',0,'objects:0:id'])
					}
				)
			])
			.then(
				responses => {
					const response: KalturaResponse<KalturaFlavorAssetListResponse> = responses[1];
					asyncAssert(() => {
						expect(response.result instanceof KalturaFlavorAssetListResponse).toBeTruthy();
						if (response.result instanceof KalturaFlavorAssetListResponse) {
							expect(Array.isArray(response.result.objects)).toBeTruthy();
							expect(response.result.objects.length).toBeGreaterThan(0);
							expect(response.result.objects[0] instanceof KalturaFlavorAsset).toBeTruthy();
						}
					});
					done();
				},
				(error) => {
					done.fail(error);
				}
			);
	});
});
