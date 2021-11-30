import {FlavorAssetListAction} from "../lib/api/types/FlavorAssetListAction";
import {KalturaFlavorAssetListResponse} from "../lib/api/types/KalturaFlavorAssetListResponse";
import {KalturaFlavorAsset} from "../lib/api/types/KalturaFlavorAsset";
import {BaseEntryListAction} from "../lib/api/types/BaseEntryListAction";
import {KalturaMediaEntryFilter} from "../lib/api/types/KalturaMediaEntryFilter";
import {KalturaFlavorAssetFilter} from "../lib/api/types/KalturaFlavorAssetFilter";
import { asyncAssert, getClient } from "./utils";
import {LoggerSettings, LogLevels} from "../lib/api/kaltura-logger";
import {KalturaResponse} from "../lib/api/kaltura-response";
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
		expect.assertions(3);
		kalturaClient.multiRequest([
			new BaseEntryListAction(
				{
					filter: new KalturaMediaEntryFilter({
						flavorParamsIdsMatchOr: '0'
					})
				}
			),
			new FlavorAssetListAction(
				{
					filter: new KalturaFlavorAssetFilter(
						{
							entryIdEqual: ''
						}
					).setDependency(['entryIdEqual',0,'objects:0:id'])
				}
			)]
		)
			.subscribe(
				responses => {

					const response: KalturaResponse<KalturaFlavorAssetListResponse> = responses[1];
					asyncAssert(() => {
						expect(response.result instanceof KalturaFlavorAssetListResponse).toBeTruthy();
						expect(Array.isArray(response.result.objects)).toBeTruthy();
						expect(response.result.objects.every(obj => obj instanceof KalturaFlavorAsset)).toBeTruthy();
					});
					done();
				},
				(error) => {
					done.fail(error);
				}
			);
	});
});
