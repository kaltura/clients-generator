package com.kaltura.client.test;

import com.app.DataFactory;
import com.kaltura.client.enums.KalturaAssetReferenceType;
import com.kaltura.client.services.KalturaAssetService;
import com.kaltura.client.services.KalturaOttUserService;
import com.kaltura.client.types.KalturaAsset;
import com.kaltura.client.types.KalturaLoginResponse;
import com.kaltura.client.utils.request.KalturaMultiRequestBuilder;
import com.kaltura.client.utils.request.KalturaRequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;
import com.kaltura.client.utils.response.base.MultiResponse;
import org.junit.Test;

/**
 * Created by tehilarozin on 11/09/2016.
 */
public class MultiRequestTest extends TestCommon {

    @Test
    public void testMultiRequest() {
        logger.info("testMultiRequest\n");

        DataFactory.UserLogin userLogin = DataFactory.getUser();
        KalturaRequestBuilder loginReq = KalturaOttUserService.login(PartnerId, userLogin.username, userLogin.password).setCompletion(new OnCompletion<GeneralResponse<KalturaLoginResponse>>() {
            @Override
            public void onComplete(GeneralResponse<KalturaLoginResponse> response) {
                logger.debug("onComplete login request [" + response.getRequestId() + "] :\n" + response.toString());
                if (response.isSuccess()) {
                    kalturaClient.setKs(response.getResult().getLoginSession().getKs());

                    String reqId = actionsQueue.queue(KalturaAssetService.get(MediaId, KalturaAssetReferenceType.MEDIA).setCompletion(new OnCompletion<GeneralResponse<KalturaAsset>>() {
                        @Override
                        public void onComplete(GeneralResponse<KalturaAsset> response) {
                            assertTrue(response.isSuccess());
                            logger.debug("onComplete request get asset [" + response.getRequestId() + "] " + response.getResult().getName() + " info\n" + response.getResult().toParams().toString());


                        }
                    }).build(kalturaClient));
                } else {
                    logger.error("Failed on testMultiRequest: loginReq failed");
                }

            }
        });

        KalturaRequestBuilder userInfoReq = KalturaOttUserService.get().setCompletion(new OnCompletion() {
            @Override
            public void onComplete(Object response) {
                logger.debug("onComplete request get user info:\n" + response.toString());
            }
        });

        KalturaMultiRequestBuilder kalturaMultiRequestBuilder = new KalturaMultiRequestBuilder(loginReq, userInfoReq)
                .link(loginReq, userInfoReq, "loginSession.ks", "ks")
                .setCompletion(  // will be activated when request returns with response
                        new OnCompletion<GeneralResponse<MultiResponse>>() {
                            @Override
                            public void onComplete(GeneralResponse<MultiResponse> response) {
                                logger.debug("onComplete multirequest ["+response.getRequestId()+"] - one total completion  \n" + response.toString());

                                assertTrue(response.isSuccess());
                                assertNotNull(response.getResult());
                                assertEquals(response.getResult().size(), 3);
                                resume();
                            }
                        })
                // addition of multirequest to another, adds its inner requests.
                .add(new KalturaMultiRequestBuilder(KalturaAssetService.get(MediaId2, KalturaAssetReferenceType.MEDIA)
                           .setParam("ks", "1:result:loginSession:ks")));

        String mReqId = actionsQueue.queue(kalturaMultiRequestBuilder.build(kalturaClient));
        logger.debug("creating multirequest [" + mReqId + "] one completion: login + userInfo + assetInfo");
        assertNotNull(mReqId);
        assertNotSame(mReqId, "");
        wait(1);
    }

}
