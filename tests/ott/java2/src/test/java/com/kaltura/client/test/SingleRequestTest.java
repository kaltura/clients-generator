package com.kaltura.client.test;

import com.app.DataFactory;
import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.services.FavoriteService;
import com.kaltura.client.services.OttUserService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.Favorite;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.LoginResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;

import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutionException;

import org.junit.Test;

/**
 * Created by tehilarozin on 11/09/2016.
 */
public class SingleRequestTest extends TestCommon{

    @Test
    /**
     * synced requests: activates login request and if succeeded, activating another request to fetch user data
     */
    public void testRequestWithinRequest() throws InterruptedException, ExecutionException{
        logger.info("testLogin");

        final CountDownLatch doneSignal = new CountDownLatch(1);
        DataFactory.UserLogin userLogin = DataFactory.getUser();
        
        RequestBuilder<LoginResponse> requestBuilder = OttUserService.login(PartnerId, userLogin.username, userLogin.password)
        .setCompletion(new OnCompletion<LoginResponse>() {
			
			@Override
			public void onComplete(LoginResponse loginResponse, APIException error) {
				assertNull(error);

                client.setKs(loginResponse.getLoginSession().getKs());

                OTTUser ottUser = loginResponse.getUser();
                logger.debug("Hello " + ottUser.getFirstName() + " " + ottUser.getLastName() + ", username: " + ottUser.getUsername() + ", ");

                logger.debug("fetching user info: ");
                RequestBuilder<OTTUser> requestBuilder = OttUserService.get()
                .setCompletion(new OnCompletion<OTTUser>() {
					
					@Override
					public void onComplete(OTTUser response, APIException error) {
						assertNull(error);
						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
		doneSignal.await();
    }

    @Test
    public void testList() throws InterruptedException, ExecutionException {
        logger.info("testCancelRequest");

        final CountDownLatch doneSignal = new CountDownLatch(1);
        DataFactory.UserLogin userLogin = DataFactory.getUser();
        
        RequestBuilder<LoginResponse> requestBuilder = OttUserService.login(PartnerId, userLogin.username, userLogin.password)
        .setCompletion(new OnCompletion<LoginResponse>() {
			
			@Override
			public void onComplete(LoginResponse loginResponse, APIException error) {
				assertNull(error);

                client.setKs(loginResponse.getLoginSession().getKs());

		        RequestBuilder<ListResponse<Favorite>> requestBuilder = FavoriteService.list()
                .setCompletion(new OnCompletion<ListResponse<Favorite>>() {
        			
        			@Override
        			public void onComplete(ListResponse<Favorite> response, APIException error) {
						assertNull(error);
						doneSignal.countDown();
        			}
        		});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
		doneSignal.await();
    }
}
