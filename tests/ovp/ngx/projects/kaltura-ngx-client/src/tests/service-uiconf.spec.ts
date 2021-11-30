import {UiConfListAction} from "../lib/api/types/UiConfListAction";
import {KalturaUiConfListResponse} from "../lib/api/types/KalturaUiConfListResponse";
import {KalturaUiConf} from "../lib/api/types/KalturaUiConf";
import {KalturaUiConfFilter} from "../lib/api/types/KalturaUiConfFilter";
import {KalturaUiConfObjType} from "../lib/api/types/KalturaUiConfObjType";
import {UiConfListTemplatesAction} from "../lib/api/types/UiConfListTemplatesAction";
import { asyncAssert, getClient } from "./utils";
import {LoggerSettings, LogLevels} from "../lib/api/kaltura-logger";
import {KalturaClient} from "../lib/kaltura-client.service";

describe(`service "UIConf" tests`, () => {
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

  test("uiconf list", (done) => {
    expect.assertions(3);
    kalturaClient.request(new UiConfListAction())
      .subscribe(
        response => {
          asyncAssert(() => {
            expect(response instanceof KalturaUiConfListResponse).toBeTruthy();
            expect(Array.isArray(response.objects)).toBeTruthy();
            expect(response.objects.every(obj => obj instanceof KalturaUiConf)).toBeTruthy();
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
    const filter = new KalturaUiConfFilter({objTypeIn: players.join(",")});
    expect.assertions(1);
    kalturaClient.request(new UiConfListAction(filter))
      .subscribe(
        response => {
          asyncAssert(() => {
            expect(response.objects.every(obj => players.indexOf(Number(obj.objType)) !== -1)).toBeTruthy();
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

    expect.assertions(2);
    kalturaClient.request(new UiConfListAction(filter))
      .subscribe(
        response => {
          asyncAssert(() => {
            expect(response.objects).toBeDefined();
            expect(response.objects.length).toBeGreaterThan(0);
            const match = /isPlaylist="(.*?)"/g.exec(response.objects[0].confFile);
            if (match) {
              expect(["true", "multi"].indexOf(match[1]) !== -1).toBeTruthy();
            }
          });
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
  });

  test("uiconf list templates", (done) => {
    expect.assertions(3);
    kalturaClient.request(new UiConfListTemplatesAction())
      .subscribe(
        response => {
          asyncAssert(() => {
            expect(response instanceof KalturaUiConfListResponse).toBeTruthy();
            expect(Array.isArray(response.objects)).toBeTruthy();
            expect(response.objects.every(obj => obj instanceof KalturaUiConf)).toBeTruthy();
          });
          done();
        },
        (error) => {
          done.fail(error);
        }
      );
  });
});
