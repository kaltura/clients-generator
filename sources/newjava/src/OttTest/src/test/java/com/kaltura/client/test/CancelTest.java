package com.kaltura.client.test;

import com.kaltura.client.services.KalturaFavoriteService;
import com.kaltura.client.services.KalturaOttUserService;
import com.kaltura.client.types.KalturaFavoriteListResponse;
import com.kaltura.client.types.KalturaLoginResponse;
import com.kaltura.client.utils.request.RequestElement;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;
import org.junit.Test;

/**
 * Created by tehilarozin on 11/09/2016.
 */
public class CancelTest extends TestCommon {
    int counter = 0;

    @Test
    public void testCancel() {
        logger.info("testCancelRequest");
        RequestElement requestElement = KalturaOttUserService.login(kalturaClient.getPartnerId(), "albert@gmail.com", "123456", null, UDID)
                .setCompletion(new OnCompletion<GeneralResponse<KalturaLoginResponse>>() {
                    @Override
                    public void onComplete(GeneralResponse<KalturaLoginResponse> response) {
                        if (response.isSuccess()) {
                            // once set will be inserted automatically in all further requests.
                            kalturaClient.setKs(response.getResult().getLoginSession().getKs());

                            String reqId = actionsQueue.queue(KalturaFavoriteService.list()
                                    .setCompletion(new OnCompletion<GeneralResponse<KalturaFavoriteListResponse>>() {
                                        @Override
                                        public void onComplete(GeneralResponse<KalturaFavoriteListResponse> response) {
                                            counter++;
                                            fail();
                                            if (response.isSuccess()) {
                                                logger.error("I should have been canceled");
                                                if (response.getResult().getTotalCount() > 0) {
                                                    logger.debug("favorites objects: " + response.getResult().getObjects().size());
                                                }
                                            }
                                        }
                                    }).build(kalturaClient));
                            actionsQueue.cancelAction(reqId);// canceled the queued request
                            assertTrue(actionsQueue.isEmpty());
                            try {
                                Thread.sleep(5000L);
                            } catch (InterruptedException e) {
                                e.printStackTrace();
                            }
                            assertTrue(counter==0);
                            resume();
                        }
                    }
                }).build(kalturaClient);

        actionsQueue.queue(requestElement);
        wait(1);
    }
}
