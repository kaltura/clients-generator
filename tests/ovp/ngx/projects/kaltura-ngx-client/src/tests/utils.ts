import * as fs from "fs";
import * as path from "path";
import {TestsConfig} from "./tests-config";
import {SessionStartAction} from "../lib/api/types/SessionStartAction";
import {KalturaSessionType} from "../lib/api/types/KalturaSessionType";
import {KalturaClient} from "../lib/kaltura-client.service";
import {Observable} from "rxjs";
import {TestBed} from "@angular/core/testing";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import { catchError, map } from 'rxjs/operators';

export function getTestFile(): string | Buffer {
  return fs.readFileSync(path.join(__dirname, "DemoVideo.flv"));
}

export function escapeRegExp(s) {
  return s.replace(/[-\/\\^$*+?.()|[\]{}]/g, "\$&");
}

export function asyncAssert(callback) {
  try {
    callback();
  } catch(e) {
    fail(e);
  }
}

export function getClient(): Observable<KalturaClient> {
  TestBed.configureTestingModule({
    imports: [HttpClientModule]
  });

  const httpConfiguration = {
    endpointUrl: TestsConfig.endpointUrl,
    clientTag: TestsConfig.clientTag
  };

  const client = new KalturaClient(TestBed.get(HttpClient), httpConfiguration, null);


  return client.request(new SessionStartAction({
    secret: TestsConfig.adminSecret,
    userId: TestsConfig.userName,
    type: KalturaSessionType.admin,
    partnerId: <any>TestsConfig.partnerId * 1
  })).pipe(
    map(ks => {
      client.setDefaultRequestOptions({ks});
      return client;
    }),
    catchError( error => {
      console.error('failed to create session with the following error "SessionStartAction"');
      throw error;
    }));
}
