// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platfroms allow them to do with
// text.
//
// Copyright (C) 2006-2011  Kaltura Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// @ignore
// ===================================================================================================
package com.kaltura.client.test;

import java.io.File;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.atomic.AtomicInteger;

import com.kaltura.client.types.APIException;
import com.kaltura.client.types.BaseEntry;
import com.kaltura.client.types.DrmPlaybackPluginData;
import com.kaltura.client.utils.request.MultiRequestBuilder;
import com.kaltura.client.utils.request.NullRequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;
import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.EntryStatus;
import com.kaltura.client.enums.MediaType;
import com.kaltura.client.enums.UploadTokenStatus;
import com.kaltura.client.services.BaseEntryService;
import com.kaltura.client.services.BaseEntryService.GetContextDataBaseEntryBuilder;
import com.kaltura.client.services.BaseEntryService.ListBaseEntryBuilder;
import com.kaltura.client.services.FlavorAssetService;
import com.kaltura.client.services.FlavorAssetService.GetByEntryIdFlavorAssetBuilder;
import com.kaltura.client.services.FlavorAssetService.GetFlavorAssetsWithParamsFlavorAssetBuilder;
import com.kaltura.client.services.LiveStreamService;
import com.kaltura.client.services.LiveStreamService.GetLiveStreamBuilder;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.services.MediaService.AddMediaBuilder;
import com.kaltura.client.services.MediaService.GetMediaBuilder;
import com.kaltura.client.services.MediaService.UpdateMediaBuilder;
import com.kaltura.client.services.PlaylistService;
import com.kaltura.client.services.SystemService;
import com.kaltura.client.services.SystemService.PingSystemBuilder;
import com.kaltura.client.services.UploadTokenService;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.types.MediaEntryFilterForPlaylist;
import com.kaltura.client.types.UploadToken;
import com.kaltura.client.types.UploadedFileTokenResource;


public class MultiRequestTest extends BaseTest{

	public void testWithFileUpload() throws Exception {

		final File fileData = TestUtils.getTestImageFile();
		
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		MediaEntry entry = new MediaEntry();
		entry.setName(getName() + " (" + new Date() + ")");
		entry.setMediaType(MediaType.IMAGE);
		entry.setReferenceId(getUniqueString());

		UploadToken uploadToken = new UploadToken();
		uploadToken.setFileName(testConfig.getUploadImage());
		uploadToken.setFileSize((double) fileData.length());

		// 4. Add Content (Object : String, Object)
		UploadedFileTokenResource resource = new UploadedFileTokenResource();
		resource.setToken("{3:result:id}");
		
		MultiRequestBuilder requestBuilder = SystemService.ping()
		.add(MediaService.add(entry))
		.add(UploadTokenService.add(uploadToken))
		.add(MediaService.addContent("{2:result:id}", resource))
		.add(UploadTokenService.upload("{3:result:id}", fileData, false))
		.setCompletion(new OnCompletion<Response<List<Object>>>() {
			
			@Override
			public void onComplete(Response<List<Object>> result) {
				assertNull(result.error);

				// 0
				assertNotNull(result.results.get(0));
				assertTrue(result.results.get(0) instanceof Boolean);
				assertTrue((Boolean) result.results.get(0));
				
				// 1
				assertTrue(result.results.get(1) instanceof MediaEntry);
				MediaEntry mEntry = (MediaEntry) result.results.get(1);
				assertNotNull(mEntry);
				assertNotNull(mEntry.getId());
				assertEquals(EntryStatus.NO_CONTENT, mEntry.getStatus());
				
				testIds.add(mEntry.getId());
				
				// 2
				assertTrue(result.results.get(2) instanceof UploadToken);
				UploadToken mToken =(UploadToken) result.results.get(2);
				assertNotNull(mToken);
				assertNotNull(mToken.getId());
				assertEquals(UploadTokenStatus.PENDING, mToken.getStatus());
				
				// 3
				assertTrue(result.results.get(3) instanceof MediaEntry);
				mEntry = (MediaEntry) result.results.get(3);
				assertEquals(EntryStatus.IMPORT, mEntry.getStatus());
				
				// 4
				assertTrue(result.results.get(4) instanceof UploadToken);
				mToken =(UploadToken) result.results.get(4);
				assertEquals(UploadTokenStatus.CLOSED, mToken.getStatus());

				try {
					Thread.sleep(5000);
				} catch (InterruptedException ie) {
					throw new RuntimeException("Failed while waiting for executeFromFilters");
				}

				// execute from filters (Array: Array, int)
				MediaEntryFilterForPlaylist filter = new MediaEntryFilterForPlaylist();
				filter.setReferenceIdEqual(mEntry.getReferenceId());
				List<MediaEntryFilterForPlaylist> filters = new ArrayList<MediaEntryFilterForPlaylist>();
				filters.add(filter);
				
				MultiRequestBuilder multiRequestBuilder = new MultiRequestBuilder();
				multiRequestBuilder.add(PlaylistService.executeFromFilters(filters, 5));
				multiRequestBuilder.setCompletion(new OnCompletion<Response<List<Object>>>() {

					@Override
					public void onComplete(Response<List<Object>> result) {
						@SuppressWarnings("unchecked")
						List<BaseEntry> mRes = (List<BaseEntry>)result.results.get(0);
						assertNotNull(mRes);
						assertEquals(1, mRes.size());
						
						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(multiRequestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		doneSignal.await();
	}

	public void testWithSingleCompletion() throws Exception {

		final String updatedTag = getUniqueString();
		
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		MediaEntry entry = new MediaEntry();
		entry.setName("test (" + new Date() + ")");
		entry.setMediaType(MediaType.IMAGE);
		entry.setReferenceId(getUniqueString());

		MediaEntry updateEntry = new MediaEntry();
		updateEntry.setTags(updatedTag);

		MultiRequestBuilder requestBuilder = SystemService.ping()
		.add(MediaService.add(entry))
		.add(MediaService.update("{2:result:id}", updateEntry))
		.add(MediaService.delete("{2:result:id}"))
		.setCompletion(new OnCompletion<Response<List<Object>>>() {
			
			@Override
			public void onComplete(Response<List<Object>> result) {
				assertNull(result.error);

				// 0
				assertNotNull(result.results.get(0));
				assertTrue(result.results.get(0) instanceof Boolean);
				assertTrue((Boolean) result.results.get(0));

				// 1
				MediaEntry mEntry = (MediaEntry) result.results.get(1);
				assertNotNull(mEntry);
				assertNotNull(mEntry.getId());

				// 2
				mEntry = (MediaEntry) result.results.get(2);
				assertEquals(updatedTag, mEntry.getTags());
				
				doneSignal.countDown();
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		doneSignal.await();
	}

	public void testWithManyCompletions() throws Exception {

		final String updatedTag = getUniqueString();
		
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		final AtomicInteger counter = new AtomicInteger(0);
		
		MediaEntry entry = new MediaEntry();
		entry.setName(getName() + " (" + new Date() + ")");
		entry.setMediaType(MediaType.IMAGE);
		entry.setReferenceId(getUniqueString());

		MediaEntry updateEntry = new MediaEntry();
		updateEntry.setTags(updatedTag);

		PingSystemBuilder systemServicePingRequestBuilder = SystemService.ping()
		.setCompletion(new OnCompletion<Response<Boolean>>() {
			
			@Override
			public void onComplete(Response<Boolean> result) {
				assertNull(result.error);
				assertTrue(result.results);
				counter.incrementAndGet();
			}
		});
		
		AddMediaBuilder mediaServiceAddRequestBuilder = MediaService.add(entry)
		.setCompletion(new OnCompletion<Response<MediaEntry>>() {
			
			@Override
			public void onComplete(Response<MediaEntry> result) {
				assertNull(result.error);
				assertNotNull(result.results);
				assertNotNull(result.results.getId());
				counter.incrementAndGet();
			}
		});
		
		UpdateMediaBuilder mediaServiceUpdateRequestBuilder = MediaService.update("{2:result:id}", updateEntry)
		.setCompletion(new OnCompletion<Response<MediaEntry>>() {
			
			@Override
			public void onComplete(Response<MediaEntry> result) {
				assertNull(result.error);
				assertNotNull(result.results);
				assertEquals(updatedTag, result.results.getTags());
				counter.incrementAndGet();
			}
		});
		
		NullRequestBuilder mediaServiceDeleteRequestBuilder = MediaService.delete("{2:result:id}")
		.setCompletion(new OnCompletion<Response<Void>>() {
			
			@Override
			public void onComplete(Response<Void> result) {
				assertNull(result.error);
				counter.incrementAndGet();
			}
		});
		
		MultiRequestBuilder requestBuilder = new MultiRequestBuilder(
			systemServicePingRequestBuilder, 
			mediaServiceAddRequestBuilder, 
			mediaServiceUpdateRequestBuilder, 
			mediaServiceDeleteRequestBuilder
		)
		.setCompletion(new OnCompletion<Response<List<Object>>>() {
			
			@Override
			public void onComplete(Response<List<Object>> result) {
				assertNull(result.error);
				assertEquals(4, counter.get());
				assertEquals(4, result.results.size());

				doneSignal.countDown();
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	
	/**
	 * This function tests that in a case of error in a multi request, the error is parsed correctly
	 * and it doesn't affect the rest of the multi-request.
	 * @throws KalturaAPIException
	 * @throws IOException 
	 */
	public void testWithError() throws Exception {
		
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
        MultiRequestBuilder requestBuilder = SystemService.ping()
		.add(MediaService.get("Illegal String"))
		.add(SystemService.ping())
		.setCompletion(new OnCompletion<Response<List<Object>>>() {

			@Override
			public void onComplete(Response<List<Object>> result) {
				assertNotNull(result.results.get(0));
				assertTrue(result.results.get(0) instanceof Boolean);
				assertTrue((Boolean) result.results.get(0));
				
				assertTrue(result.results.get(1) instanceof APIException);

				assertNotNull(result.results.get(2));
				assertTrue(result.results.get(2) instanceof Boolean);
				assertTrue((Boolean) result.results.get(2));

				doneSignal.countDown();
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	
	/**
	 * This function tests that in a case of error in a multi request, the error is parsed correctly
	 * and it doesn't affect the rest of the multi-request.
	 * @throws KalturaAPIException
	 * @throws IOException 
	 */
	public void testWithErrorWithManyCompletions() throws Exception {
		
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		final AtomicInteger counter = new AtomicInteger(0);
		
		PingSystemBuilder requestBuilder1 = SystemService.ping()
		.setCompletion(new OnCompletion<Response<Boolean>>() {
			
			@Override
			public void onComplete(Response<Boolean> result) {
				assertNull(result.error);
				assertTrue(result.results);
				counter.incrementAndGet();
			}
		});
		
		GetMediaBuilder requestBuilder2 = MediaService.get("Illegal String")
		.setCompletion(new OnCompletion<Response<MediaEntry>>() {
			
			@Override
			public void onComplete(Response<MediaEntry> result) {
				assertNotNull(result.error);
				assertNull(result.results);
				counter.incrementAndGet();
			}
		});
		
		PingSystemBuilder requestBuilder3 = SystemService.ping()
		.setCompletion(new OnCompletion<Response<Boolean>>() {
			
			@Override
			public void onComplete(Response<Boolean> result) {
				assertNull(result.error);
				assertTrue(result.results);
				counter.incrementAndGet();
			}
		});
		
		MultiRequestBuilder requestBuilder = new MultiRequestBuilder(requestBuilder1, requestBuilder2, requestBuilder3)
		.setCompletion(new OnCompletion<Response<List<Object>>>() {

			@Override
			public void onComplete(Response<List<Object>> result) {
				assertNull(result.error);

				assertEquals(3, counter.get());
				assertEquals(3, result.results.size());
				
				doneSignal.countDown();
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	public void testTokens() throws Exception {

		PingSystemBuilder systemPingRequestBuilder = SystemService.ping();
		GetMediaBuilder mediaGetRequestBuilder = MediaService.get("whatever");
		GetByEntryIdFlavorAssetBuilder flavorAssetGetByEntryIdRequestBuilder = FlavorAssetService.getByEntryId("whatever");
		GetFlavorAssetsWithParamsFlavorAssetBuilder flavorAssetGetFlavorAssetsWithParamsRequestBuilder = FlavorAssetService.getFlavorAssetsWithParams("whatever");
		GetLiveStreamBuilder liveStreamGetRequestBuilder = LiveStreamService.get("whatever");
		GetContextDataBaseEntryBuilder baseEntryGetContextDataRequestBuilder = BaseEntryService.getContextData("whatever", null);
		ListBaseEntryBuilder baseEntryListRequestBuilder = BaseEntryService.list();
		
		systemPingRequestBuilder
		.add(mediaGetRequestBuilder)
		.add(flavorAssetGetByEntryIdRequestBuilder)
		.add(flavorAssetGetFlavorAssetsWithParamsRequestBuilder)
		.add(liveStreamGetRequestBuilder)
		.add(baseEntryGetContextDataRequestBuilder)
		.add(baseEntryListRequestBuilder);

		assertEquals("{1:result}", systemPingRequestBuilder.getTokenizer());
		assertEquals("{2:result:id}", mediaGetRequestBuilder.getTokenizer().id());
		assertEquals("{3:result:1:id}", flavorAssetGetByEntryIdRequestBuilder.getTokenizer().get(1).id());
		assertEquals("{4:result:0:flavorAsset:id}", flavorAssetGetFlavorAssetsWithParamsRequestBuilder.getTokenizer().get(0).flavorAsset().id());
		assertEquals("{5:result:streams:0:language}", liveStreamGetRequestBuilder.getTokenizer().streams().get(0).language());
		assertEquals("{6:result:pluginData:myKey:scheme}", baseEntryGetContextDataRequestBuilder.getTokenizer().pluginData().get("myKey", DrmPlaybackPluginData.Tokenizer.class).scheme());
		assertEquals("{7:result:objects:1:id}", baseEntryListRequestBuilder.getTokenizer().objects().get(1).id());
	}
}
