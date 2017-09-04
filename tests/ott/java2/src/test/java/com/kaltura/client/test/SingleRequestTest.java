
package com.kaltura.client.test;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.services.FavoriteService;
import com.kaltura.client.services.FavoriteService.ListFavoriteBuilder;
import com.kaltura.client.services.OttUserService;
import com.kaltura.client.services.OttUserService.GetOttUserBuilder;
import com.kaltura.client.services.OttUserService.LoginOttUserBuilder;
import com.kaltura.client.types.Favorite;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.LoginResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.ApiCompletion;
import com.kaltura.client.utils.response.base.Response;

import org.awaitility.Awaitility;
import org.junit.Test;

import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;

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

        final AtomicBoolean done = new AtomicBoolean(false);
        
        LoginOttUserBuilder requestBuilder = OttUserService.login(testConfig.getPartnerId(), testConfig.getUserName(), testConfig.getUserPassword())
        .setCompletion(new ApiCompletion<LoginResponse>() {

			@Override
			public void onComplete(Response<LoginResponse> result) {

				assertNotNull(result);
				assertNull(result.error);

                client.setKs(result.results.getLoginSession().getKs());

                OTTUser ottUser = result.results.getUser();
                logger.debug("Hello " + ottUser.getFirstName() + " " + ottUser.getLastName() + ", username: " + ottUser.getUsername() + ", ");

                logger.debug("fetching user info: ");
                GetOttUserBuilder requestBuilder = OttUserService.get()
                .setCompletion(new ApiCompletion<OTTUser>() {

					@Override
					public void onComplete(Response<OTTUser> result) {

						assertNotNull(result);
						assertNull(result.error);
						done.set(true);
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		Awaitility.await().atMost(20, TimeUnit.SECONDS).untilTrue(done);

		assertTrue(done.get());
    }

    @Test
    public void testList() throws InterruptedException, ExecutionException {
        logger.info("testCancelRequest");

		final AtomicBoolean done = new AtomicBoolean(false);

        LoginOttUserBuilder requestBuilder = OttUserService.login(testConfig.getPartnerId(),
				testConfig.getUserName(), testConfig.getUserPassword())
        .setCompletion(new ApiCompletion<LoginResponse>() {

			@Override
			public void onComplete(Response<LoginResponse> result) {

				assertNotNull(result);
				assertNull(result.error);

                client.setKs(result.results.getLoginSession().getKs());

		        ListFavoriteBuilder requestBuilder = FavoriteService.list()
                .setCompletion(new ApiCompletion<ListResponse<Favorite>>() {

					@Override
					public void onComplete(Response<ListResponse<Favorite>> result) {

						assertNotNull(result);
						assertNull(result.error);
						done.set(true);
        			}
        		});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		Awaitility.await().atMost(20, TimeUnit.SECONDS).untilTrue(done);

		assertTrue(done.get());
    }
}
