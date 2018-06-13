import { ThumbAssetServeAction } from "../api/types/ThumbAssetServeAction";
import { KalturaClient } from "../kaltura-client-service";

describe("Kaltura File request", () => {
    test("thumbasset service > serve action", (done) => {

        const thumbRequest = new ThumbAssetServeAction({
            thumbAssetId: '1_ep9epsxy'
        });

        const predefinedClient = new KalturaClient();
        predefinedClient.setDefaultRequestOptions(
            {
                ks: 'YWIyZDAxYWRhZmQ1NzhjMzQ5ZmI3Nzc4MzVhYTJkMGI1NDdhYzA5YnwxNzYzMzIxOzE3NjMzMjE7MTUxMjA1MzA1MzsyOzE1MTE5NjY2NTMuNTk7YWRtaW47ZGlzYWJsZWVudGl0bGVtZW50Ozs'
            }
        );

        predefinedClient.request(thumbRequest)
            .then(
                result => {
                    expect(result).toBeDefined();
                    expect(result.url).toBeDefined();
                    expect(result.url).toBe('https://www.kaltura.com/api_v3/service/thumbasset/action/serve?format=1&apiVersion=@VERSION@&thumbAssetId=1_ep9epsxy&ks=YWIyZDAxYWRhZmQ1NzhjMzQ5ZmI3Nzc4MzVhYTJkMGI1NDdhYzA5YnwxNzYzMzIxOzE3NjMzMjE7MTUxMjA1MzA1MzsyOzE1MTE5NjY2NTMuNTk7YWRtaW47ZGlzYWJsZWVudGl0bGVtZW50Ozs&clientTag=ngxClientTag');

                    done();
                },
                error => {
                    const s = '';
                });

    });

    test("error when sending 'KalturaFileRequest' as part of multi-request", (done) => {

        const thumbRequest: any = new ThumbAssetServeAction({
            thumbAssetId: 'thumbAssetId'
        });


        const config =
            {
                endpointUrl: 'https://www.kaltura.com',
                clientTag: 'ngxClientTag'
            };

        const predefinedClient = new KalturaClient();
        predefinedClient.setDefaultRequestOptions({
            ks: 'YWIyZDAxYWRhZmQ1NzhjMzQ5ZmI3Nzc4MzVhYTJkMGI1NDdhYzA5YnwxNzYzMzIxOzE3NjMzMjE7MTUxMjA1MzA1MzsyOzE1MTE5NjY2NTMuNTk7YWRtaW47ZGlzYWJsZWVudGl0bGVtZW50Ozs'
        });

        predefinedClient.multiRequest([thumbRequest])
            .then(
                result => {
                    fail('got response instead of error');
                },
                error => {
                    expect(error).toBeDefined();
                    expect(error).toBeInstanceOf(Error);
                    expect(error.message).toBe("multi-request not support requests of type 'KalturaFileRequest'");
                    done();
                });

    });
});
