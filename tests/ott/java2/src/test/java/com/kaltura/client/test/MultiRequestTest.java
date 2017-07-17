package com.kaltura.client.test;

import com.app.DataFactory;
import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.AssetReferenceType;
import com.kaltura.client.services.AssetService;
import com.kaltura.client.services.OttUserService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.Asset;
import com.kaltura.client.types.LoginResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.request.MultiRequestBuilder;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;

import java.util.List;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.atomic.AtomicInteger;

import org.junit.Test;

/**
 * Created by tehilarozin on 11/09/2016.
 */
public class MultiRequestTest extends TestCommon {

    @Test
    public void testMultiRequest() throws InterruptedException, ExecutionException {
        logger.info("testMultiRequest\n");

        final CountDownLatch doneSignal = new CountDownLatch(1);
		final AtomicInteger counter = new AtomicInteger(0);
        DataFactory.UserLogin userLogin = DataFactory.getUser();
        
        RequestBuilder<LoginResponse> ottUserLoginRequestBuilder = OttUserService.login(PartnerId, userLogin.username, userLogin.password)
        .setCompletion(new OnCompletion<LoginResponse>() {
			
			@Override
			public void onComplete(LoginResponse loginResponse, APIException error) {
				assertNull(error);
				counter.incrementAndGet();						
			}
		});

        RequestBuilder<Asset> assetGetRequestBuilder = AssetService.get(MediaId, AssetReferenceType.MEDIA)
        .setCompletion(new OnCompletion<Asset>() {
			
			@Override
			public void onComplete(Asset asset, APIException error) {
				assertNull(error);
				counter.incrementAndGet();
			}
		});

        RequestBuilder<OTTUser> ottUserGetRequestBuilder = OttUserService.get()
        .setCompletion(new OnCompletion<OTTUser>() {
			
			@Override
			public void onComplete(OTTUser response, APIException error) {
				assertNull(error);
				counter.incrementAndGet();
			}
		});
        
        MultiRequestBuilder requestBuilder = new MultiRequestBuilder(ottUserLoginRequestBuilder, assetGetRequestBuilder, ottUserGetRequestBuilder);
        requestBuilder.link(ottUserLoginRequestBuilder, assetGetRequestBuilder, "loginSession.ks", "ks");
        requestBuilder.link(ottUserLoginRequestBuilder, ottUserGetRequestBuilder, "loginSession.ks", "ks");
        
        requestBuilder.setCompletion(new OnCompletion<List<Object>>() {
			
			@Override
			public void onComplete(List<Object> multi, APIException error) {
				assertNull(error);
				assertEquals(3, counter.get());
				assertEquals(3, multi.size());

				doneSignal.countDown();
				
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
		doneSignal.await();
    }

}
