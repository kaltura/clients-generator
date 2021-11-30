import { KalturaResponse } from "../lib/api/kaltura-response";
import { KalturaMultiRequest } from "../lib/api/kaltura-multi-request";
import { UserLoginByLoginIdAction } from "../lib/api/types/UserLoginByLoginIdAction";
import { UserGetByLoginIdAction } from "../lib/api/types/UserGetByLoginIdAction";
import { PermissionListAction } from "../lib/api/types/PermissionListAction";
import { PartnerGetAction } from "../lib/api/types/PartnerGetAction";
import { KalturaAPIException } from "../lib/api/kaltura-api-exception";
import { KalturaMultiResponse } from "../lib/api/kaltura-multi-response";
import { KalturaUser } from "../lib/api/types/KalturaUser";
import { asyncAssert, getClient } from "./utils";
import { LoggerSettings, LogLevels } from "../lib/api/kaltura-logger";
import { KalturaClient } from "../lib/kaltura-client.service";

describe("Kaltura server API multi request", () => {
    const fakeUserName = "login";
    const fakePassword = "pass";

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

    describe("Building request", () => {
        test("execute multi request with only ond inner requests", () => {
            const multiRequest = new KalturaMultiRequest(
                new UserLoginByLoginIdAction({
                    loginId: fakeUserName,
                    password: fakePassword
                })
            );

            const pojoRequest = multiRequest.buildRequest(null);

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
                new PartnerGetAction({id: 12})
            );

            const pojoRequest = multiRequest.buildRequest(null);

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
                new PartnerGetAction({ id: 12})
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
                new UserGetByLoginIdAction({ loginId: fakeUserName }).setDependency(["ks", 0, ""]),
                new PartnerGetAction().setDependency(["ks", 1, ""], ["id", 1, "partnerId"])
            );

            const pojoRequest = multiRequest1.buildRequest(null);
            expect(pojoRequest).toBeDefined();

            const multiRequest1Request1 = pojoRequest["1"];
            const multiRequest1Request2 = pojoRequest["2"];

            expect(multiRequest1Request1).toBeDefined();
            expect(multiRequest1Request2).toBeDefined();
            expect(multiRequest1Request2.id).toBe("{2:result:partnerId}");

            const multiRequest2 = new KalturaMultiRequest(
                new UserLoginByLoginIdAction({
                    loginId: fakeUserName,
                    password: fakePassword
                }),
                new UserGetByLoginIdAction({ loginId: fakeUserName }),
            new PartnerGetAction(null)
                .setDependency({ property: "ks", request: 0 }, { property: "id", request: 1, targetPath: ["partnerId"] })
        );

            const pojoRequest2 = multiRequest2.buildRequest(null);
            expect(pojoRequest2).toBeDefined();

            const multiRequest2request1 = pojoRequest2["1"];
            const multiRequest2Request2 = pojoRequest2["2"];

            expect(multiRequest2request1).toBeDefined();
            expect(multiRequest2Request2).toBeDefined();
            expect(multiRequest2Request2.id).toBe("{2:result:partnerId}");
        });
    });

    describe("Invoking multi request", () => {
        test("executes multi request set completion on some requests and on the multi request instance", (done) => {
          expect.assertions(4);
            kalturaClient.multiRequest(new KalturaMultiRequest(
              new PermissionListAction(),
                new PermissionListAction()
                    .setCompletion((response) => {
                      asyncAssert(() => {
                        expect(response).toBeDefined();
                        expect(response.result).toBeDefined();
                        expect(response.error).toBeUndefined();
                      });
                    })
            ).setCompletion(responses => {
              asyncAssert(() => {
                expect(responses.hasErrors()).toBeFalsy();
              });
            })).subscribe(
                (responses) => {
                  done();
                },
                (error) => {
                    done.fail(error);
                }
            );
        });

        test("handles multi request response with failure on some inner requests", (done) => {
          expect.assertions(7);
            kalturaClient.multiRequest(new KalturaMultiRequest(
                new UserLoginByLoginIdAction({
                    loginId: fakeUserName,
                    password: fakePassword
                }).setCompletion((response) => {
                  asyncAssert(() => {
                    expect(response).toBeDefined();
                    expect(response.result).toBeUndefined();
                    expect(response.error).toBeDefined();
                  });
                }),
                new PermissionListAction()
                    .setCompletion((response) => {
                      asyncAssert(() => {
                        expect(response).toBeDefined();
                        expect(response.result).toBeDefined();
                        expect(response.error).toBeUndefined();
                      });
                    }),
            ).setCompletion(responses => {
              asyncAssert(() => {
                expect(responses.hasErrors()).toBe(true);
              });
            })).subscribe(
                (responses) => {
                    done();
                },
                (error) => {
                    done.fail(error);
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
                new KalturaResponse<any>(null, new KalturaAPIException("12", "222",null)),
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
