import * as fs from "fs";
import * as path from "path";
import { TestsConfig } from "../api/tests/tests-config";
import { SessionStartAction } from "../api/types/SessionStartAction";
import { KalturaSessionType } from "../api/types/KalturaSessionType";
import { KalturaClient } from '../kaltura-client.service';
import { KalturaClientConfiguration } from '../kaltura-client-configuration.service';

export function getTestFile(): string | Buffer {
  return fs.readFileSync(path.join(__dirname, "DemoVideo.flv"));
}

export function escapeRegExp(s) {
    return s.replace(/[-\/\\^$*+?.()|[\]{}]/g, "\$&");
}

    export function getClient(): KalturaClient {
    const configuration = new KalturaClientConfiguration();
    configuration.endpointUrl = TestsConfig.endpointUrl;
    configuration.clientTag = TestsConfig.clientTag;

    let client = new KalturaClient(null, configuration);

    return client;
    // TODO implement
    // return client.request(new SessionStartAction({
    //     secret: TestsConfig.adminSecret,
    //     userId: TestsConfig.userName,
    //     type: KalturaSessionType.admin,
    //     partnerId: <any>TestsConfig.partnerId * 1
    // })).then(ks => {
    //     client.ks = ks;
    //     return client;
    // },
    //     error => {
    //         console.error(`failed to create session with the following error 'SessionStartAction'`);
    //         throw error;
    //     });
}