import {WidgetListAction} from "../lib/api/types/WidgetListAction";
import {KalturaWidgetListResponse} from "../lib/api/types/KalturaWidgetListResponse";
import { asyncAssert, getClient } from "./utils";
import {LoggerSettings, LogLevels} from "../lib/api/kaltura-logger";
import {KalturaClient} from "../lib/kaltura-client.service";

describe(`service "Widget" tests`, () => {
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

  test("widgets list", (done) => {
    expect.assertions(1);
    kalturaClient.request(new WidgetListAction())
      .subscribe(
        response => {
          asyncAssert(() => {
            expect(response instanceof KalturaWidgetListResponse).toBeTruthy();
          });
          done();
        },
        (error) => {
          done.fail(error);
        }
      );
  });
});
