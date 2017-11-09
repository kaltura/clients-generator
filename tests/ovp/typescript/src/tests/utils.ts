import * as fs from "fs";
import * as path from "path";
import { TestsConfig } from "./tests-config";
import { KalturaBrowserHttpClient } from "../kaltura-clients/kaltura-browser-http-client";
import { SessionStartAction } from "../types/SessionStartAction";
import { KalturaSessionType } from '../types/KalturaSessionType';

export function getTestFile(): string | Buffer {
  return fs.readFileSync(path.join(__dirname, "DemoVideo.flv"));
}

export function getClient(): Promise<KalturaBrowserHttpClient> {
    const httpConfiguration = {
        endpointUrl: TestsConfig.endpointUrl,
        clientTag: TestsConfig.clientTag
    };

    let client = new KalturaBrowserHttpClient(httpConfiguration);


    return client.request(new SessionStartAction({
        secret: TestsConfig.adminSecret,
        userId: TestsConfig.userName,
        type: KalturaSessionType.admin,
        partnerId: <any>TestsConfig.partnerId * 1
    })).then(ks => {
        client.ks = ks;
        return client;
    });
}