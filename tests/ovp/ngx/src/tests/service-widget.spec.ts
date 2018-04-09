import {WidgetListAction} from "../api/types/WidgetListAction";
import {KalturaWidgetListResponse} from "../api/types/KalturaWidgetListResponse";
import {getClient} from "./utils";
import {LoggerSettings, LogLevels} from "../api/kaltura-logger";
import {KalturaClient} from "../kaltura-client.service";

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
    kalturaClient.request(new WidgetListAction())
      .subscribe(
        response => {
          expect(response instanceof KalturaWidgetListResponse).toBeTruthy();
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
  });
});
