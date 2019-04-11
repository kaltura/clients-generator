import {BaseEntryListAction} from "../lib/api/types/BaseEntryListAction";
import {UserLoginByLoginIdAction} from "../lib/api/types/UserLoginByLoginIdAction";
import {KalturaDetachedResponseProfile} from "../lib/api/types/KalturaDetachedResponseProfile";
import {KalturaBaseEntryFilter} from "../lib/api/types/KalturaBaseEntryFilter";
import {KalturaSearchOperator} from "../lib/api/types/KalturaSearchOperator";
import {KalturaNullableBoolean} from "../lib/api/types/KalturaNullableBoolean";
import {AppTokenAddAction} from "../lib/api/types/AppTokenAddAction";
import {KalturaAppToken} from "../lib/api/types/KalturaAppToken";
import {KalturaSearchOperatorType} from "../lib/api/types/KalturaSearchOperatorType";
import {KalturaContentDistributionSearchItem} from "../lib/api/types/KalturaContentDistributionSearchItem";
import {UserGetAction} from "../lib/api/types/UserGetAction";
import {PlaylistListAction} from "../lib/api/types/PlaylistListAction";
import {KalturaResponseType} from "../lib/api/types/KalturaResponseType";
import {KalturaBaseEntryListResponse} from "../lib/api/types/KalturaBaseEntryListResponse";
import {KalturaPlaylist} from "../lib/api/types/KalturaPlaylist";
import {PartnerGetAction} from "../lib/api/types/PartnerGetAction";
import {KalturaPlaylistType} from "../lib/api/types/KalturaPlaylistType";
import {KalturaEntryReplacementStatus} from "../lib/api/types/KalturaEntryReplacementStatus";
import {KalturaMediaEntryFilterForPlaylist} from "../lib/api/types/KalturaMediaEntryFilterForPlaylist";
import {KalturaAPIException} from "../lib/api/kaltura-api-exception";
import {KalturaAppTokenHashType} from "../lib/api/types/KalturaAppTokenHashType";
import {KalturaMediaEntryFilter} from "../lib/api/types/KalturaMediaEntryFilter";
import {KalturaMediaEntry} from "../lib/api/types/KalturaMediaEntry";
import { asyncAssert, escapeRegExp, getClient } from "./utils";
import {LoggerSettings, LogLevels} from "../lib/api/kaltura-logger";
import {KalturaFilterPager} from "../lib/api/types/KalturaFilterPager";
import {KalturaClient} from "../lib/kaltura-client.service";
import {TestsConfig} from './tests-config';

describe("Kaltura server API request", () => {
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

    describe("Kaltura request with specific format type", () => {
        test("handle response format 1 (json)", (done) => {
            // example of assignment by setParameters function (support chaining)
            const listAction: BaseEntryListAction = new BaseEntryListAction(
                {
                    filter: new KalturaBaseEntryFilter().setData(filter => {
                        filter.statusIn = "2";
                    })
                });
            expect.assertions(2);
            kalturaClient.request(listAction, KalturaResponseType.responseTypeJson).subscribe(
                (response) => {
                    asyncAssert(() => {
                        expect(typeof response === 'string').toBeTruthy();
                        expect(JSON.parse(response)).toBeDefined();
                    });
                    done();
                },
                (error) => {
                    done.fail(error);
                }
            );
        });

        test("handle response format 2 (xml)", (done) => {
            // example of assignment by setParameters function (support chaining)
            const listAction: BaseEntryListAction = new BaseEntryListAction(
                {
                    filter: new KalturaBaseEntryFilter().setData(filter => {
                        filter.statusIn = "2";
                    })
                });
            expect.assertions(2);
            kalturaClient.request(listAction, KalturaResponseType.responseTypeXml).subscribe(
                (response) => {
                    asyncAssert(() => {
                        expect(typeof response === 'string').toBeTruthy();
                        expect(response.indexOf('<?xml ')).toBe(0);
                    });
                    done();
                },
                (error) => {
                    done.fail(error);
                }
            );
        });
    });
});
