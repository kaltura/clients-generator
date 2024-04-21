import { KalturaClient } from "../kaltura-client-service";
import { UiConfListAction } from "../api/types/UiConfListAction";
import { KalturaUiConfListResponse } from "../api/types/KalturaUiConfListResponse";
import { KalturaUiConf } from "../api/types/KalturaUiConf";
import { KalturaUiConfFilter } from "../api/types/KalturaUiConfFilter";
import { KalturaUiConfObjType } from "../api/types/KalturaUiConfObjType";
import { UiConfListTemplatesAction } from "../api/types/UiConfListTemplatesAction";
import { getClient } from "./utils";
import { LoggerSettings, LogLevels } from "../api/kaltura-logger";
import { asyncAssert } from "./utils";

describe(`service "UIConf" tests`, () => {
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

  test("uiconf list", (done) => {
	  expect.assertions(4);
    kalturaClient.request(new UiConfListAction())
      .then(
        response => {
	        asyncAssert(() => {
		        expect(response instanceof KalturaUiConfListResponse).toBeTruthy();
		        expect(Array.isArray(response.objects)).toBeTruthy();
		        expect(response.objects.length).toBeGreaterThan(0);
		        expect(response.objects[0] instanceof KalturaUiConf).toBeTruthy();
	        });
          done();
        },
        (error) => {
          done.fail(error);
        }
      );
  });


  // TODO [kmc] investigate response
  xtest("get players", (done) => {
    const players = [KalturaUiConfObjType.player, KalturaUiConfObjType.playerV3, KalturaUiConfObjType.playerSl];
    const filter = new KalturaUiConfFilter({ objTypeIn: players.join(",") });

	  expect.assertions(2);
    kalturaClient.request(new UiConfListAction(filter))
      .then(
        response => {
	        asyncAssert(() => {
		        expect(response.objects.length).toBeGreaterThan(0);
		        expect(players.indexOf(Number(response.objects[0].objType)) !== -1).toBeTruthy();
        });
          done();
        },
        (error) => {
          done.fail(error);
        }
      );
  });

  test("get video players", (done) => {
    const players = [KalturaUiConfObjType.player, KalturaUiConfObjType.playerV3, KalturaUiConfObjType.playerSl];
    const filter = new KalturaUiConfFilter({
      objTypeIn: players.join(","),
      tagsMultiLikeOr: "player"
    });

	  expect.assertions(1);
    kalturaClient.request(new UiConfListAction(filter))
      .then(
        response => {
	        expect(response.objects.length).toBeGreaterThan(0);
          done();
        },
        (error) => {
          done.fail(error);
        }
      );
  });

  test("uiconf list templates", (done) => {
	  expect.assertions(4);
    kalturaClient.request(new UiConfListTemplatesAction())
      .then(
        response => {
	        asyncAssert(() => {
		        expect(response instanceof KalturaUiConfListResponse).toBeTruthy();
		        expect(Array.isArray(response.objects)).toBeTruthy();
		        expect(response.objects.length).toBeGreaterThan(0);
		        expect(response.objects[0] instanceof KalturaUiConf).toBeTruthy();
	        });
          done();
        },
        (error) => {
          done.fail(error);
        }
      );
  });
});
