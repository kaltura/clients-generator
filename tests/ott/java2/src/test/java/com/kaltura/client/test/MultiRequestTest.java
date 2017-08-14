package com.kaltura.client.test;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.AssetOrderBy;
import com.kaltura.client.enums.AssetReferenceType;
import com.kaltura.client.services.AssetService;
import com.kaltura.client.services.OttUserService;
import com.kaltura.client.types.Asset;
import com.kaltura.client.types.FilterPager;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.LoginResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.types.SearchAssetFilter;
import com.kaltura.client.utils.request.MultiRequestBuilder;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
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

	private AtomicBoolean done;

	public void setUp() throws Exception {
		super.setUp();
		done = new AtomicBoolean(false);
	}

	@Test
    public void testMultiRequest() throws InterruptedException, ExecutionException {
        logger.info("testMultiRequest\n");

        //final CountDownLatch doneSignal = new CountDownLatch(1);

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

    @Test
	public void testListResponse(){
		SearchAssetFilter searchAssetFilter = new SearchAssetFilter();
		searchAssetFilter.setOrderBy(AssetOrderBy.RELEVANCY_DESC.name());
		searchAssetFilter.setTypeIn("420, 423, 421, 0, 422");

		FilterPager filterPager = new FilterPager();
		filterPager.setPageSize(10);
		filterPager.setPageIndex(1);
		final ListResponse<Asset> assets = new ListResponse<>();

		MultiRequestBuilder multiRequestBuilder = OttUserService.login(testConfig.getPartnerId(),
				testConfig.getUserName(), testConfig.getUserPassword())
				.add(AssetService.list(searchAssetFilter, filterPager))
				.link(0, 1, "loginSession.ks", "ks")
				.setCompletion(new OnCompletion<Response<List<Object>>>() {
					@Override
					public void onComplete(Response<List<Object>> result) {
						try {
							assets.setObjects(((ListResponse)result.results.get(1)).getObjects());
						} catch (Exception e) {
							e.printStackTrace();
						}
						done.set(true);
					}
				});

		APIOkRequestsExecutor.getExecutor().queue(multiRequestBuilder.build(client));

		Awaitility.await().atMost(30, TimeUnit.SECONDS).untilTrue(done);

		assertTrue(done.get());
		assertFalse(assets.getObjects().isEmpty());
	}

}
