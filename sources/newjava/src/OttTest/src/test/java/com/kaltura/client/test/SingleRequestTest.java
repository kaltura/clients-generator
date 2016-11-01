package com.kaltura.client.test;

import com.app.DataFactory;
import com.kaltura.client.services.KalturaOttUserService;
import com.kaltura.client.types.KalturaLoginResponse;
import com.kaltura.client.types.KalturaOTTUser;
import com.kaltura.client.utils.request.KalturaRequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.ResponseType;
import com.kaltura.client.utils.response.base.GeneralResponse;
import org.junit.Test;

/**
 * Created by tehilarozin on 11/09/2016.
 */
public class SingleRequestTest extends TestCommon{

    @Test
    /**
     * synced requests: activates login request and if succeeded, activating another request to fetch user data
     */
    public void testRequestWithinRequest(){
        logger.info("testLogin");

        DataFactory.UserLogin userLogin = DataFactory.getUser();
        KalturaRequestBuilder loginReq = KalturaOttUserService.login(PartnerId, userLogin.username, userLogin.password)
                .setCompletion(new OnCompletion<GeneralResponse<KalturaLoginResponse>>() {
                    @Override
                    public void onComplete(GeneralResponse<KalturaLoginResponse> response) {
                        logger.debug("onComplete login request [" + response.getRequestId() + "] :\n" + response.toString());
                        if (response.isSuccess()) {
                            kalturaClient.setKs(response.getResult().getLoginSession().getKs());

                            KalturaOTTUser ottUser = response.getResult().getUser();
                            logger.debug("Hello " + ottUser.getFirstName() + " " + ottUser.getLastName() + ", username: " + ottUser.getUsername() + ", ");

                            logger.debug("fetching user info: ");
                            actionsQueue.queue(KalturaOttUserService.get().setCompletion(new OnCompletion() {
                                @Override
                                public void onComplete(Object response) {
                                    logger.debug("onComplete request get user info:\n" + response.toString());
                                    resume();
                                }
                            }).build(kalturaClient));

                        } else {
                            logger.error("Failed on testSingleRequest");
                            assertNotNull(response);
                            assertTrue(response.getResult() instanceof ResponseType);
                            resume();
                        }
                    }
        });
        actionsQueue.queue(loginReq.build(kalturaClient));
        wait(1);
    }
}
