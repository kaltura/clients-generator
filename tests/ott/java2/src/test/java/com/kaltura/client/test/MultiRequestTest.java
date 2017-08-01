package com.kaltura.client.test;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.AssetReferenceType;
import com.kaltura.client.services.AssetService;
import com.kaltura.client.services.OttUserService;
import com.kaltura.client.types.Asset;
import com.kaltura.client.types.LoginResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.request.MultiRequestBuilder;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.base.ApiCompletion;
import com.kaltura.client.utils.response.base.Response;

import org.awaitility.Awaitility;
import org.junit.Test;

import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicInteger;

/**
 * Created by tehilarozin on 11/09/2016.
 */
public class MultiRequestTest extends TestCommon {

    @Test
    public void testMultiRequest() throws InterruptedException, ExecutionException {
        logger.info("testMultiRequest\n");

        //final CountDownLatch doneSignal = new CountDownLatch(1);
        final AtomicBoolean done = new AtomicBoolean(false);

        final AtomicInteger counter = new AtomicInteger(0);
        //DataFactory.UserLogin userLogin = DataFactory.getUser();

        RequestBuilder<LoginResponse> ottUserLoginRequestBuilder = OttUserService.login(testConfig.getPartnerId(),
            testConfig.getUserName(), testConfig.getUserPassword())
            .setCompletion(new ApiCompletion<LoginResponse>() {

                @Override
                public void onComplete(Response<LoginResponse> result) {

                    assertNotNull(result);
                    assertNull(result.error);
                    counter.incrementAndGet();
                }
            });

        RequestBuilder<Asset> assetGetRequestBuilder = AssetService.get(testConfig.getMediaId(), AssetReferenceType.MEDIA)
        .setCompletion(new ApiCompletion<Asset>() {

			@Override
			public void onComplete(Response<Asset> result) {
				assertNotNull(result);
				assertNull(result.error);
				counter.incrementAndGet();
			}
		});

        RequestBuilder<OTTUser> ottUserGetRequestBuilder = OttUserService.get()
        .setCompletion(new ApiCompletion<OTTUser>() {

			@Override
			public void onComplete(Response<OTTUser> result) {

				assertNotNull(result);
				assertNull(result.error);
				counter.incrementAndGet();
			}
		});
        
        MultiRequestBuilder requestBuilder = new MultiRequestBuilder(ottUserLoginRequestBuilder, assetGetRequestBuilder, ottUserGetRequestBuilder);
        requestBuilder.link(ottUserLoginRequestBuilder, assetGetRequestBuilder, "loginSession.ks", "ks");
        requestBuilder.link(ottUserLoginRequestBuilder, ottUserGetRequestBuilder, "loginSession.ks", "ks");
        
        requestBuilder.setCompletion(new ApiCompletion<List<Object>>() {

			@Override
			public void onComplete(Response<List<Object>> result) {
				assertNotNull(result);
				assertNull(result.error);
				assertEquals(3, counter.get());
				assertEquals(3, result.results.size());

				done.set(true);
				
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
        Awaitility.await().atMost(30, TimeUnit.SECONDS).untilTrue(done);

        assertTrue(done.get());
    }

}
