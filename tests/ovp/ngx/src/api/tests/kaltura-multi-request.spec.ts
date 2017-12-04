import { KalturaResponse } from "../kaltura-response";
import { KalturaMultiRequest } from "../kaltura-multi-request";
import { UserLoginByLoginIdAction } from "../types/UserLoginByLoginIdAction";
import { UserGetByLoginIdAction } from "../types/UserGetByLoginIdAction";
import { PermissionListAction } from "../types/PermissionListAction";
import { PartnerGetAction } from "../types/PartnerGetAction";
import { KalturaAPIException } from "../kaltura-api-exception";
import { KalturaMultiResponse } from "../kaltura-multi-response";
import { KalturaUser } from "../types/KalturaUser";
import { KalturaBrowserHttpClient } from "../kaltura-clients/kaltura-browser-http-client";
import { getClient } from "./utils";
import { LoggerSettings, LogLevels } from "../kaltura-logger";

describe("Kaltura server API multi request", () => {
  const fakeUserName = "";
  const fakePassword = "";

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

  afterAll(() => {
    kalturaClient = null;
  });

  describe("Building request", () => {
    test("execute multi request with only ond inner requests", () => {
      const multiRequest = new KalturaMultiRequest(
        new UserLoginByLoginIdAction({
          loginId: fakeUserName,
          password: fakePassword
        })
      );

      const pojoRequest = multiRequest.toRequestObject();

      expect(multiRequest instanceof KalturaMultiRequest).toBeTruthy();
      expect(pojoRequest instanceof KalturaMultiRequest).toBeFalsy();
      expect(pojoRequest["0"]).toBeDefined();
      expect(pojoRequest["1"]).toBeUndefined();
    });

    test("executes multi request with multiple inner requests", () => {
      const multiRequest = new KalturaMultiRequest(
        new UserLoginByLoginIdAction({
          loginId: fakeUserName,
          password: fakePassword
        }),
        new UserGetByLoginIdAction({ loginId: fakeUserName }),
        new PermissionListAction(),
        new PartnerGetAction(12)
      );

      const pojoRequest = multiRequest.toRequestObject();

      expect(pojoRequest["0"]).toBeDefined();
      expect(pojoRequest["1"]).toBeDefined();
      expect(pojoRequest["2"]).toBeDefined();
      expect(pojoRequest["3"]).toBeDefined();
      expect(pojoRequest["4"]).toBeUndefined();
    });

    test("set completion on inner requests (optional)", () => {
      const multiRequest = new KalturaMultiRequest(
        new UserLoginByLoginIdAction({
          loginId: fakeUserName,
          password: fakePassword
        })
          .setCompletion(() => {
          }),
        new UserGetByLoginIdAction({ loginId: fakeUserName }),
        new PermissionListAction()
          .setCompletion(() => {
          }),
        new PartnerGetAction(12)
      );

      expect(multiRequest.requests).toBeDefined();
      expect(multiRequest.requests["0"]["callback"]).toBeDefined();
      expect(multiRequest.requests["1"]["callback"]).toBeUndefined();
      expect(multiRequest.requests["2"]["callback"]).toBeDefined();
      expect(multiRequest.requests["3"]["callback"]).toBeUndefined();
    });

    test("supports kaltura api parameter dependency between inner requests", () => {

      const multiRequest1 = new KalturaMultiRequest(
        new UserLoginByLoginIdAction({
          loginId: fakeUserName,
          password: fakePassword
        }),
        new UserGetByLoginIdAction({ loginId: fakeUserName }).setDependency(["ks", 1, ""]),
        new PartnerGetAction().setDependency(["ks", 1, ""], ["id", 2, "partnerId"])
      );

      const pojoRequest = multiRequest1.toRequestObject();
      expect(pojoRequest).toBeDefined();

      const multiRequest1Request1 = pojoRequest["1"];
      const multiRequest1Request2 = pojoRequest["2"];

      expect(multiRequest1Request1).toBeDefined();
      expect(multiRequest1Request1.ks).toBe("{2:result}");
      expect(multiRequest1Request2).toBeDefined();
      expect(multiRequest1Request2.ks).toBe("{2:result}");
      expect(multiRequest1Request2.id).toBe("{3:result:partnerId}");

      const multiRequest2 = new KalturaMultiRequest(
        new UserLoginByLoginIdAction({
          loginId: fakeUserName,
          password: fakePassword
        }),
        new UserGetByLoginIdAction({ loginId: fakeUserName })
          .setDependency({ property: "ks", request: 1 }),
        new PartnerGetAction(null)
          .setDependency({ property: "ks", request: 1 }, { property: "id", request: 2, targetPath: ["partnerId"] })
      );

      const pojoRequest2 = multiRequest2.toRequestObject();
      expect(pojoRequest2).toBeDefined();

      const multiRequest2request1 = pojoRequest2["1"];
      const multiRequest2Request2 = pojoRequest2["2"];

      expect(multiRequest2request1).toBeDefined();
      expect(multiRequest2request1.ks).toBe("{1:result}");
      expect(multiRequest2Request2).toBeDefined();
      expect(multiRequest2Request2.ks).toBe("{1:result}");
      expect(multiRequest2Request2.id).toBe("{2:result:partnerId}");
    });
  });

  describe("Invoking multi request", () => {
    test("executes multi request set completion on some requests and on the multi request instance", (done) => {
      kalturaClient.multiRequest(new KalturaMultiRequest(
        new UserLoginByLoginIdAction({
          loginId: fakeUserName,
          password: fakePassword
        }),
        new UserGetByLoginIdAction({ loginId: fakeUserName })
          .setDependency(["ks", 1, ""]),
        new PermissionListAction()
          .setDependency(["ks", 1, ""])
          .setCompletion((response) => {
            // TODO [kmc] should add a test failing if this callback wasn"t called
            expect(response).toBeDefined();
            expect(response.result).toBeDefined();
            expect(response.error).toBeUndefined();

          }),
        new PartnerGetAction()
          .setDependency(["ks", 1, ""], ["id", 2, "partnerId"])
      ).setData((request) => {
        request.ks = null; // Override provided ks from the kaltura request configuration
      }).setCompletion(responses => {
        // TODO [kmc] should add a test failing if this callback wasn"t called
        expect(responses.hasErrors()).toBe(false);
      })).then(
        (responses) => {
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    test("handles multi request response with failure on some inner requests", (done) => {
      kalturaClient.multiRequest(new KalturaMultiRequest(
        new UserLoginByLoginIdAction({
          loginId: fakeUserName,
          password: fakePassword
        }),
        new UserGetByLoginIdAction({ loginId: fakeUserName })
          .setDependency(["ks", 1, ""]),
        new PermissionListAction()
          .setCompletion((response) => {
            // TODO [kmc] should add a test failing if this callback wasn"t called
            expect(response).toBeDefined();
            expect(response.result).toBeUndefined();
            expect(response.error).toBeDefined();
          }),
        new PartnerGetAction()
          .setDependency(["ks", 1, ""], ["id", 2, "partnerId"])
      ).setData((request) => {
        request.ks = null; // Override provided ks from the kaltura request configuration
      }).setCompletion(responses => {
        // TODO [kmc] should add a test failing if this callback wasn"t called
        expect(responses.hasErrors()).toBe(true);
      })).then(
        (responses) => {
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });


    test("returns error if server response is not an array or got unexpected number of items in array", () => {
      const request = new KalturaMultiRequest(
        new UserLoginByLoginIdAction({
          loginId: fakeUserName,
          password: fakePassword
        }),
        new UserGetByLoginIdAction({ loginId: fakeUserName })
          .setDependency(["ks", 1, ""])
      );

      expect(request.handleResponse(null).hasErrors()).toBeTruthy();
      expect(request.handleResponse(null).length).toBe(2);
      // TODO [kmc] investigate
      // expect(request.handleResponse(null)[0].error instanceof KalturaAPIException).toBeTruthy();
      // expect(request.handleResponse(null)[0].error.code).toBe("client::response_type_error");

      expect(request.handleResponse([{}]).hasErrors()).toBeTruthy();
      expect(request.handleResponse([{}]).length).toBe(2);
      // TODO [kmc] investigate
      // expect(request.handleResponse([{}])[0].error instanceof KalturaAPIException).toBeTruthy();
      // expect(request.handleResponse([{}])[0].error.code).toBe("client::response_type_error");
    });

    test("exposes a function that return if one or more responses returned with errors.", () => {
      // response with errors
      const response1 = new KalturaMultiResponse([
        new KalturaResponse<any>(null, new KalturaAPIException("12", "222")),
      ]);

      expect(response1).toBeDefined();
      expect(typeof response1.hasErrors).toBe("function");
      expect(response1.hasErrors()).toBe(true);

      // response without errors
      const response2 = new KalturaMultiResponse([
        new KalturaResponse<any>(new KalturaUser(), null),
        new KalturaResponse<any>(new KalturaUser(), null),
      ]);

      expect(response2).toBeDefined();
      expect(typeof response2.hasErrors).toBe("function");
      expect(response2.hasErrors()).toBe(false);
    });
  });
});