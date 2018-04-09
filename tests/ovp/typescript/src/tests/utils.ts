import * as fs from "fs";
import * as path from "path";
import { TestsConfig } from "./tests-config";
import { KalturaClient } from "../kaltura-client-service";
import { SessionStartAction } from "../api/types/SessionStartAction";
import { KalturaSessionType } from "../api/types/KalturaSessionType";

export function getTestFile(): string | Buffer {
  return fs.readFileSync(path.join(__dirname, "DemoVideo.flv"));
}

export function escapeRegExp(s) {
    return s.replace(/[-\/\\^$*+?.()|[\]{}]/g, "\$&");
}

export function getClient(): Promise<KalturaClient> {
    const httpConfiguration = {
        endpointUrl: TestsConfig.endpointUrl,
        clientTag: TestsConfig.clientTag
    };

    let client = new KalturaClient(httpConfiguration);


    return client.request(new SessionStartAction({
        secret: TestsConfig.adminSecret,
        userId: TestsConfig.userName,
        type: KalturaSessionType.admin,
        partnerId: <any>TestsConfig.partnerId * 1
    })).then(ks => {
        client.setDefaultRequestOptions({
            ks
        });
        return client;
    },
        error => {
            console.error(`failed to create session with the following error 'SessionStartAction'`);
            throw error;
        });
}