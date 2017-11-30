import { KalturaBrowserHttpClient } from "../kaltura-clients/kaltura-browser-http-client";
import { BaseEntryListAction } from "../types/BaseEntryListAction";
import { UserLoginByLoginIdAction } from "../types/UserLoginByLoginIdAction";
import { KalturaDetachedResponseProfile } from "../types/KalturaDetachedResponseProfile";
import { KalturaBaseEntryFilter } from "../types/KalturaBaseEntryFilter";
import { KalturaSearchOperator } from "../types/KalturaSearchOperator";
import { KalturaNullableBoolean } from "../types/KalturaNullableBoolean";
import { AppTokenAddAction } from "../types/AppTokenAddAction";
import { KalturaAppToken } from "../types/KalturaAppToken";
import { KalturaSearchOperatorType } from "../types/KalturaSearchOperatorType";
import { KalturaContentDistributionSearchItem } from "../types/KalturaContentDistributionSearchItem";
import { UserGetAction } from "../types/UserGetAction";
import { KalturaBaseEntryListResponse } from "../types/KalturaBaseEntryListResponse";
import { KalturaPlaylist } from "../types/KalturaPlaylist";
import { PartnerGetAction } from "../types/PartnerGetAction";
import { KalturaPlaylistType } from "../types/KalturaPlaylistType";
import { KalturaEntryReplacementStatus } from "../types/KalturaEntryReplacementStatus";
import { KalturaMediaEntryFilterForPlaylist } from "../types/KalturaMediaEntryFilterForPlaylist";
import { KalturaAPIException } from "../kaltura-api-exception";
import { KalturaAppTokenHashType } from "../types/KalturaAppTokenHashType";
import { KalturaMediaEntry } from "../types/KalturaMediaEntry";
import { getClient, escapeRegExp } from "./utils";
import { LoggerSettings, LogLevels } from "../kaltura-logger";
import { KalturaFilterPager } from "../types/KalturaFilterPager";

describe("Kaltura server API request", () => {
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
    test("expose request configuration properties as part of each action", () => {
      const listAction: BaseEntryListAction = new BaseEntryListAction();
      expect(listAction).toBeDefined();
      expect(listAction instanceof BaseEntryListAction).toBeTruthy();

      const userLoginByLoginIdAction: UserLoginByLoginIdAction = new UserLoginByLoginIdAction(
        {
          loginId: "a",
          password: "a",
          ks: "valid ks",
          partnerId: 1234,
          responseProfile: new KalturaDetachedResponseProfile().setData(data => {
            data.fields = "fields";
          })
        }
      );
      expect(userLoginByLoginIdAction).toBeDefined();
      expect(userLoginByLoginIdAction instanceof UserLoginByLoginIdAction).toBeTruthy();

      const pojoRequest = <any>userLoginByLoginIdAction.toRequestObject();
      expect(pojoRequest.service).toBe("user");
      expect(pojoRequest.action).toBe("loginByLoginId");
      expect(pojoRequest.ks).toBe("valid ks");
      expect(pojoRequest.partnerId).toBe(1234);
      expect(pojoRequest.responseProfile).toBeDefined();
      expect(pojoRequest.responseProfile.objectType).toBe("KalturaDetachedResponseProfile");
      expect(pojoRequest.responseProfile.fields).toBe("fields");

    });

    test("create a pojo of the request by and emit ", () => {
      const request = new BaseEntryListAction(
        {
          filter: new KalturaBaseEntryFilter().setData(data => {
            data.advancedSearch = new KalturaSearchOperator();
          })
        }
      );

      expect(request.filter).toBeDefined();
      expect(request.filter instanceof KalturaBaseEntryFilter).toBeTruthy();
      expect(request.filter.advancedSearch).toBeDefined();
      expect(request.filter.advancedSearch instanceof KalturaSearchOperator).toBeTruthy();

      const pojoRequest: any = <any>request.toRequestObject();
      expect(pojoRequest).toBeDefined();

      expect(pojoRequest.filter).toBeDefined();
      expect(pojoRequest.filter.objectType).toBe("KalturaBaseEntryFilter");
      expect(pojoRequest.filter instanceof KalturaBaseEntryFilter).toBeFalsy();
      expect(pojoRequest.filter.advancedSearch).toBeDefined();
      expect(pojoRequest.filter.advancedSearch.objectType).toBe("KalturaSearchOperator");
      expect(pojoRequest.filter.advancedSearch instanceof KalturaSearchOperator).toBeFalsy();
    });

    test("ignore undefined/null/empty array values in request", () => {
      const request = new BaseEntryListAction(
        {
          filter: new KalturaBaseEntryFilter(),
          responseProfile: new KalturaDetachedResponseProfile()
        });

      const pojoRequest = <any>request.toRequestObject();
      expect(pojoRequest).toBeDefined();
      expect(pojoRequest.hasOwnProperty("pager")).toBeFalsy();
      expect(pojoRequest.filter).toBeDefined();
      expect(pojoRequest.filter.hasOwnProperty("statusIn")).toBeFalsy();
      expect(pojoRequest.responseProfile).toBeDefined();
      expect(pojoRequest.responseProfile.hasOwnProperty("mappings")).toBeFalsy();
    });

    test("ignore local action properties properties in request", () => {
      const request = new BaseEntryListAction(
        {
          filter: new KalturaBaseEntryFilter()
        });

      const pojoRequest = <any>request.toRequestObject();
      expect(pojoRequest).toBeDefined();
      expect(typeof pojoRequest.objectType).toBe("undefined");
    });


    test("send enum of type int in request", () => {
      const request = new BaseEntryListAction(
        {
          filter: new KalturaBaseEntryFilter().setData(data => {
            data.isRoot = KalturaNullableBoolean.trueValue;
          })
        }
      );

      const pojoRequest: any = request.toRequestObject();
      expect(pojoRequest).toBeDefined();
      expect(pojoRequest.filter.isRoot).toBe(1);
      expect(typeof pojoRequest.filter.isRoot === "number");
    });

    test("send enum of type string in request", () => {
      const request = new AppTokenAddAction(
        {
          appToken: new KalturaAppToken().setData(
            request => {
              request.hashType = KalturaAppTokenHashType.sha1;
            }
          )
        }
      );

      expect(KalturaAppTokenHashType.sha1).toBe(KalturaAppTokenHashType.sha1);

      const pojoRequest: any = request.toRequestObject();
      expect(pojoRequest).toBeDefined();
      expect(pojoRequest.appToken.hashType).toBe(KalturaAppTokenHashType.sha1.toString());
      expect(typeof pojoRequest.appToken.hashType === "string");
    });

    test("send object in request", () => {
      const request = new BaseEntryListAction(
        {
          filter: new KalturaBaseEntryFilter().setData(data => {
            data.statusIn = "2";
          })
        }
      );

      const pojoRequest: any = request.toRequestObject();
      expect(pojoRequest).toBeDefined();
      expect(pojoRequest.filter).toBeDefined();
      expect(pojoRequest.filter instanceof KalturaBaseEntryFilter).toBeFalsy();
      expect(pojoRequest.filter.objectType).toBe("KalturaBaseEntryFilter");

    });

    xtest("send date in request", () => {
      pending("waiting to a server support for dates");
      // const request = new BaseEntryListAction(
      // {
      // filter:
      //     new KalturaBaseEntryFilter().setData(data =>
      //     {
      //         data.createdAtGreaterThanOrEqual = new Date("1980-08-11");
      //     })
      // }
      // );
      //
      // const requestData  : any= request.build();
      // expect(requestData).toBeDefined();
      // expect(requestData.filter).toBeDefined();
      // expect(requestData.filter.createdAtGreaterThanOrEqual).toBe(334800000);
    });

    xtest("send array of simple types in request", () => {
      pending("TBD");
    });

    test("send array of objects in request", () => {
      const request = new BaseEntryListAction(
        {
          filter: new KalturaBaseEntryFilter().setData(data => {
            data.statusIn = "2";
            data.advancedSearch = new KalturaSearchOperator().setData(data => {
              data.type = KalturaSearchOperatorType.searchAnd;
              data.items.push(
                new KalturaSearchOperator(),
                new KalturaSearchOperator().setData(searchOperator => {
                  searchOperator.type = KalturaSearchOperatorType.searchOr;
                  searchOperator.items.push(
                    new KalturaContentDistributionSearchItem().setData(distribution => {
                      distribution.distributionProfileId = 1;
                    }),
                    new KalturaContentDistributionSearchItem().setData(distribution => {
                      distribution.distributionProfileId = 2;
                    })
                  );
                })
              );
            });
          })
        }
      );

      const pojoRequest: any = request.toRequestObject();
      expect(pojoRequest).toBeDefined();

      const requestFilter: any = pojoRequest.filter;
      expect(requestFilter).toBeDefined();
      const requestAvancedSearch: any = requestFilter.advancedSearch;
      expect(requestAvancedSearch.items.length).toBe(2);
      expect(requestAvancedSearch.items[1].items.length).toBe(2);
      expect(requestAvancedSearch.items[1].items[0].distributionProfileId).toBe(1);
      expect(requestAvancedSearch.items[1].items[1].distributionProfileId).toBe(2);
    });

    test("handle default value property of type int correctly", () => {
      const request = new BaseEntryListAction(
        {
          filter: new KalturaBaseEntryFilter()
        }
      );

      let pojoRequest: any = request.toRequestObject();
      expect(pojoRequest).toBeDefined();
      expect(pojoRequest.filter).toBeDefined();
      expect(typeof pojoRequest.filter.partnerIdEqual).toBe("undefined");

      request.filter.partnerIdEqual = 123;
      pojoRequest = request.toRequestObject();
      expect(pojoRequest.filter).toBeDefined();
      expect(pojoRequest.filter.partnerIdEqual).toBe(123);
    });

    test("handle default value property of type string correctly", () => {
      const request = new BaseEntryListAction(
        {
          filter: new KalturaBaseEntryFilter()
        }
      );

      let pojoRequest: any = request.toRequestObject();
      expect(pojoRequest).toBeDefined();
      expect(pojoRequest.filter).toBeDefined();
      expect(typeof pojoRequest.filter.freeText).toBe("undefined");

      request.filter.freeText = "free";
      pojoRequest = request.toRequestObject();
      expect(pojoRequest.filter).toBeDefined();
      expect(pojoRequest.filter.freeText).toBe("free");

      const request2 = new UserLoginByLoginIdAction(
        {
          loginId: "username",
          password: "password"
        });

      const pojoRequest2: any = request2.toRequestObject();
      expect(pojoRequest2).toBeDefined();
      expect(pojoRequest2.privileges).toBe("*");

    });

    test("treat string default value ", () => {
      const request = new UserGetAction();

      expect(request.userId).toBeUndefined();

      let pojoRequest: any = request.toRequestObject();
      expect(pojoRequest).toBeDefined();
      expect(pojoRequest.userId).toBeUndefined();
    });

    test("chain complex request with one statement (nested arrays, inner complex object)", () => {

      const request = new BaseEntryListAction(
        {
          filter: new KalturaBaseEntryFilter().setData(data => {
            data.statusIn = "2";
            data.advancedSearch = new KalturaSearchOperator().setData(data => {
              data.type = KalturaSearchOperatorType.searchAnd;
              data.items.push(
                new KalturaSearchOperator().setData(searchOperator => {
                  searchOperator.type = KalturaSearchOperatorType.searchOr;
                  searchOperator.items.push(
                    new KalturaContentDistributionSearchItem().setData(distribution => {
                      distribution.distributionProfileId = 12333;
                    })
                  );
                })
              );
            });
          })
        }
      );

      const pojoRequest: any = request.toRequestObject();
      expect(pojoRequest).toBeDefined();

      const requestFilter: any = pojoRequest.filter;
      expect(requestFilter).toBeDefined();
      expect(requestFilter instanceof KalturaBaseEntryFilter).toBeFalsy();
      expect(requestFilter.statusIn).toBe("2");
      expect(requestFilter.advancedSearch.objectType).toBe("KalturaSearchOperator");
      expect(requestFilter.advancedSearch.type).toBe(KalturaSearchOperatorType.searchAnd);
      const advancedSearchItem: any = requestFilter.advancedSearch.items["0"];
      expect(advancedSearchItem).toBeDefined();
      expect(advancedSearchItem.type).toBe(KalturaSearchOperatorType.searchOr);
      expect(advancedSearchItem.items).toBeDefined();
      const distributionSearchItem: any = advancedSearchItem.items["0"];
      expect(distributionSearchItem).toBeDefined();
      expect(distributionSearchItem.objectType).toBe("KalturaContentDistributionSearchItem");
      expect(distributionSearchItem.distributionProfileId).toBe(12333);
    });

    test("force required parameters to be provided by constructor", () => {
      const request = new UserLoginByLoginIdAction(
        {
          loginId: "username",
          password: "password"
        });

      const pojoRequest: any = request.toRequestObject();
      expect(pojoRequest).toBeDefined();

      expect(pojoRequest["loginId"]).toBe("username");
      expect(pojoRequest["password"]).toBe("password");
    });


    test("set optional parameters of action request (only!) directly from the action constructor", () => {
      const request = new UserLoginByLoginIdAction({
        loginId: "username",
        password: "password",
        expiry: 1234
      });

      const pojoRequest: any = request.toRequestObject();
      expect(pojoRequest).toBeDefined();
      expect(pojoRequest.expiry).toBe(1234);

      const filter = new KalturaBaseEntryFilter();
      filter.statusIn = "2";
      const request2: BaseEntryListAction = new BaseEntryListAction({ filter: filter });

      const pojoRequest2: any = request2.toRequestObject();

      expect(pojoRequest2).toBeDefined();
      expect(pojoRequest2.filter).toBeDefined();
      expect(pojoRequest2.filter instanceof KalturaBaseEntryFilter).toBeDefined();
      expect(pojoRequest2.filter["statusIn"]).toBe("2");

    });


    test("allow overriding the general request configuration for ks/partnerid for specific request", () => {
      // build request with default ks (not settings ks explicitly)
      const requestWithDefaultKS = new UserLoginByLoginIdAction({
        loginId: "username",
        password: "password"
      });


      const pojoRequest: any = requestWithDefaultKS.toRequestObject();
      expect(pojoRequest).toBeDefined();
      expect(pojoRequest.ks).toBeUndefined();

      // build request with custom ks
      const requestWithCustomKS = new UserLoginByLoginIdAction({
        loginId: "username",
        password: "password",
        ks: "custom request KS"
      });

      const pojoRequest2: any = requestWithCustomKS.toRequestObject();

      expect(pojoRequest2).toBeDefined();
      expect(pojoRequest2.ks).toBe("custom request KS");


    });

    test("support chaining on setCompletion", () => {
      const request = new UserLoginByLoginIdAction({
        loginId: "username",
        password: "password",
        ks: "custom request KS"
      });
      const setCompletionResult = request.setCompletion(() => {
      });

      expect(setCompletionResult instanceof UserLoginByLoginIdAction).toBeTruthy();
    });

    test("expose function that allow setting multiple parameters while chaining", () => {
      const request: UserLoginByLoginIdAction = new UserLoginByLoginIdAction({
        loginId: "username",
        password: "password"
      }).setData(
        (request) => {
          request.expiry = 1;
          request.privileges = "none";
        }
      );

      const pojoRequest: any = request.toRequestObject();


      expect(pojoRequest).toBeDefined();
      expect(pojoRequest.expiry).toBe(1);
      expect(pojoRequest.privileges).toBe("none");
    });
  });

  describe("Invoking kaltura response", () => {
    test("parse action response type", (done) => {
      // example of assignment by setParameters function (support chaining)
      const listAction: BaseEntryListAction = new BaseEntryListAction(
        {
          filter: new KalturaBaseEntryFilter().setData(filter => {
            filter.statusIn = "2";
          })
        });

      kalturaClient.request(listAction).then(
        (response) => {
          expect(response instanceof KalturaBaseEntryListResponse).toBeTruthy();
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    xtest("parse action response type that inherit from expected response type", () => {
      pending("TBD");
    });


    xtest("throw error when provided action response doesnt inherit from expected action response type", () => {
      pending("TBD");
    });

    test("parse object response property", (done) => {
      kalturaClient.request(new BaseEntryListAction()).then(
        (response) => {
          expect(response instanceof KalturaBaseEntryListResponse).toBeTruthy();

          expect(response.objects).toBeDefined();
          const object1 = response.objects[0];
          expect(object1).toBeDefined();

          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    test("parse object response property that inherit from expected property type", (done) => {
      kalturaClient.request(new BaseEntryListAction()).then(
        (response) => {
          expect(response).toBeDefined();
          expect(response.objects).toBeDefined();

          const object0 = response.objects[0];
          expect(object0).toBeDefined();
          expect(object0 instanceof KalturaMediaEntry).toBeTruthy();

          const object4 = response.objects[4];
          expect(object4).toBeDefined();
          expect(object4 instanceof KalturaPlaylist).toBeTruthy();
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    xtest("throw error when provided property value doesnt inherit from expected property type", () => {
      pending("TBD");
    });

    test("parse number response property", (done) => {
      kalturaClient.request(new BaseEntryListAction()).then(
        (response) => {
          expect(response).toBeDefined();
          expect(response.objects).toBeDefined();
          const object0 = response.objects[0];
          expect(object0).toBeDefined();
          expect(object0.accessControlId).toBe(1880531);
          done();
        },
        (error) => {
          fail(error);
        }
      );
    });

    test("parse number response property while provided value is boolean", (done) => {
      kalturaClient.request(new PartnerGetAction({ id: 1931861 })).then(
        (response) => {
          expect(response).toBeDefined();
          expect(response.allowMultiNotification).toBe(0);
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    test("parse number response property while provided value is valid number as string", (done) => {
      kalturaClient.request(new BaseEntryListAction()).then(
        (response) => {
          expect(response).toBeDefined();
          expect(response.objects).toBeDefined();
          const object1 = response.objects[1];
          expect(object1).toBeDefined();
          expect(object1.version).toBe(0);
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    xtest("throw error when response property of type number and the provided value is not a number", () => {
      pending("TBD");
    });

    test("parse string response property", (done) => {
      kalturaClient.request(new BaseEntryListAction()).then(
        (response) => {
          expect(response).toBeDefined();
          expect(response.objects).toBeDefined();
          const object1 = response.objects[1];
          expect(object1).toBeDefined();
          expect(object1.name).toBe("Columbia Business School:Video as a Marketing Tool in Education");
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    test("parse string response property while provided value is of type number", (done) => {
      kalturaClient.request(new PartnerGetAction({ id: 1931861 })).then(
        (response) => {
          expect(response).toBeDefined();
          expect(response.defConversionProfileType).toBe("1001");
          done();
        },
        (error) => {
          fail(error);
        }
      );
    });

    test("parse array response property", (done) => {
      kalturaClient.request(new BaseEntryListAction({
          pager: new KalturaFilterPager({
              pageSize: 30
          })
      })).then(
        (response) => {

          expect(response).toBeDefined();
          expect(response.totalCount).toBeGreaterThan(30);
          expect(response.objects).toBeDefined();
          expect(response.objects.length).toBe(30);
          const object1 = response.objects[1];
          expect(object1).toBeDefined();
          const object23 = response.objects[23];
          expect(object23).toBeDefined();
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    xtest("parse boolean response property", () => {
      pending("TBD");
    });


    test("parse boolean response property while provided value is valid number as string", (done) => {
      kalturaClient.request(new PartnerGetAction({ id: 1931861 })).then(
        (response) => {
          expect(response).toBeDefined();
          expect(response.adultContent).toBe(false);
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    xtest("parse file response property", () => {
      pending("TBD");
    });

    xtest("parse void response property", () => {
      pending("TBD");
    });

    xtest("parse date response property", () => {
      pending("waiting to a server support for dates");
      // kalturaClient.request(new BaseEntryListAction()).then(
      //     (response) =>
      //     {
      //         const kalturaMediaEntry : KalturaMediaEntry = <KalturaMediaEntry>response.objects[0];
      //         expect(kalturaMediaEntry.createdAt instanceof Date).toBeTruthy();// known dates are converted by the api
      //         expect(kalturaMediaEntry.createdAt.getTime() ).toBe((new Date(1450013576 * 1000)).getTime()); // TODO [kmc] response.{typed array}.{DATE VALUE}
      //     },
      //     () =>
      //     {
      //         fail("should not reach this part");
      //     }
      // );
    });

    test("parse enum of type int response property", (done) => {
      kalturaClient.request(new BaseEntryListAction()).then(
        (response) => {
          expect(response).toBeDefined();
          expect(response.objects).toBeDefined();
          const object3: KalturaPlaylist = <KalturaPlaylist>response.objects[4];
          expect(object3).toBeDefined();
          expect(object3.playlistType).toBe(KalturaPlaylistType.dynamic);
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    xtest("parse enum of type string response property when the provided value is of type int", () => {
      kalturaClient.request(new BaseEntryListAction()).then(
        (response) => {
          const kalturaMediaEntry: KalturaMediaEntry = <KalturaMediaEntry>response.objects[0];
          expect(kalturaMediaEntry.replacementStatus).toBe(KalturaEntryReplacementStatus.none);
          expect(kalturaMediaEntry.replacementStatus.equals(KalturaEntryReplacementStatus.none)).toBeTruthy();
        },
        () => {
          fail("should not reach this part");
        }
      );
    });

    xtest("parse enum of type string response property when the provided value is of type string", () => {
      pending("TBD");
    });

    xtest("parse array of simple types response property", () => {
      pending("TBD");
    });

    test("parse array of objects response property", (done) => {
      kalturaClient.request(new BaseEntryListAction(
          {
              pager: new KalturaFilterPager({
                  pageSize: 30
              })
          }
      )).then(
        (response) => {
          expect(response instanceof KalturaBaseEntryListResponse).toBeTruthy();

          // verify length of array and totalCount
            expect(response.totalCount).toBeGreaterThan(30);
            expect(response.objects).toBeDefined();
          expect(response.objects.length).toBe(30);

          // verify item is of the right type
          const kalturaMediaEntry: KalturaMediaEntry = <KalturaMediaEntry>response.objects[0];
          expect(kalturaMediaEntry).toBeDefined();

          const kalturaPlaylist: KalturaPlaylist = <KalturaPlaylist>response.objects[4];
          expect(kalturaPlaylist).toBeDefined();

          // verify array inner item properties are exposed correctly
          expect(kalturaMediaEntry.dataUrl).toMatch(new RegExp(`^${escapeRegExp('https://cdnapisec.kaltura.com/p/1931861/sp/193186100/playManifest/entryId/1_2vp1gp7u/format/url/protocol/http')}[s]?$`)); // simple value
          expect(kalturaMediaEntry.id).toBe("1_2vp1gp7u"); // simple value OF BASE


          // verify nested array is exposed correctly
          const playlistFilterItem: KalturaMediaEntryFilterForPlaylist = kalturaPlaylist.filters[0];
          expect(playlistFilterItem).toBeDefined();
          expect(playlistFilterItem.limit).toBe(200); // // nested array item value

          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });

    test("parse kaltura api exception response", (done) => {
      const listAction: BaseEntryListAction = new BaseEntryListAction();
      listAction.ks = "invalid ks";

      kalturaClient.request(listAction).then(
        (response) => {
          fail(`should not reach this part: ${response}`);
          done();
        },
        (error) => {
          expect(error instanceof KalturaAPIException).toBeTruthy();
          done();
        }
      );

    });

    xtest("reflect network exceptions as kaltura api exception", () => {
      pending("TBD");
    });

    xtest("reflect missing requst argument as kaltura api exception", () => {
      pending("TBD");
    });

    test("process request without setting completion to that request", (done) => {
      kalturaClient.request(new BaseEntryListAction()).then(
        (response) => {
          expect(response instanceof KalturaBaseEntryListResponse).toBeTruthy();
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
    });
  });
});
