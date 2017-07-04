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
import java.util.concurrent.atomic.AtomicInteger;

import com.kaltura.client.types.APIException;
import com.kaltura.client.types.BaseEntry;
import com.kaltura.client.utils.request.MultiRequestBuilder;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.EntryStatus;
import com.kaltura.client.enums.MediaType;
import com.kaltura.client.enums.UploadTokenStatus;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.services.PlaylistService;
import com.kaltura.client.services.SystemService;
import com.kaltura.client.services.UploadTokenService;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.types.MediaEntryFilterForPlaylist;
import com.kaltura.client.types.UploadToken;
import com.kaltura.client.types.UploadedFileTokenResource;


public class MultiRequestTest extends BaseTest{

	public void testWithFileUpload() throws Exception {

		final File fileData = TestUtils.getTestImageFile();
		
		startAdminSession();

		final Completion completion = new Completion();
		completion.run(new Runnable() {
			@Override
			public void run() {
				MediaEntry entry = new MediaEntry();
				entry.setName("test (" + new Date() + ")");
				entry.setMediaType(MediaType.IMAGE);
				entry.setReferenceId(getUniqueString());
		
				UploadToken uploadToken = new UploadToken();
				uploadToken.setFileName(testConfig.getUploadImage());
				uploadToken.setFileSize((double) fileData.length());
		
				// 4. Add Content (Object : String, Object)
				UploadedFileTokenResource resource = new UploadedFileTokenResource();
				resource.setToken("{3:result:id}");
				
				RequestBuilder<List<Object>> requestBuilder = SystemService.ping()
				.add(MediaService.add(entry))
				.add(UploadTokenService.add(uploadToken))
				.add(MediaService.addContent("{2:result:id}", resource))
				.add(UploadTokenService.upload("{3:result:id}", fileData, false))
				.setCompletion(new OnCompletion<List<Object>>() {
					
					@Override
					public void onComplete(List<Object> multi, APIException error) {
						completion.assertNull(error);
		
						// 0
						completion.assertNotNull(multi.get(0));
						completion.assertTrue(multi.get(0) instanceof Boolean);
						completion.assertTrue((Boolean) multi.get(0));
						
						// 1
						completion.assertTrue(multi.get(1) instanceof MediaEntry);
						MediaEntry mEntry = (MediaEntry) multi.get(1);
						completion.assertNotNull(mEntry);
						completion.assertNotNull(mEntry.getId());
						completion.assertEquals(EntryStatus.NO_CONTENT, mEntry.getStatus());
						
						testIds.add(mEntry.getId());
						
						// 2
						completion.assertTrue(multi.get(2) instanceof UploadToken);
						UploadToken mToken =(UploadToken) multi.get(2);
						completion.assertNotNull(mToken);
						completion.assertNotNull(mToken.getId());
						completion.assertEquals(UploadTokenStatus.PENDING, mToken.getStatus());
						
						// 3
						completion.assertTrue(multi.get(3) instanceof MediaEntry);
						mEntry = (MediaEntry) multi.get(3);
						completion.assertEquals(EntryStatus.IMPORT, mEntry.getStatus());
						
						// 4
						completion.assertTrue(multi.get(4) instanceof UploadToken);
						mToken =(UploadToken) multi.get(4);
						completion.assertEquals(UploadTokenStatus.CLOSED, mToken.getStatus());
						
						
						// execute from filters (Array: Array, int)
						MediaEntryFilterForPlaylist filter = new MediaEntryFilterForPlaylist();
						filter.setReferenceIdEqual(mEntry.getReferenceId());
						List<MediaEntryFilterForPlaylist> filters = new ArrayList<MediaEntryFilterForPlaylist>();
						filters.add(filter);
						
						MultiRequestBuilder multiRequestBuilder = new MultiRequestBuilder();
						multiRequestBuilder.add(PlaylistService.executeFromFilters(filters, 5));
						multiRequestBuilder.setCompletion(new OnCompletion<List<Object>>() {

							@Override
							public void onComplete(List<Object> multi, APIException error) {
								@SuppressWarnings("unchecked")
								List<BaseEntry> mRes = (List<BaseEntry>)multi.get(0);
								completion.assertNotNull(mRes);
								completion.assertEquals(1, mRes.size());
								
								completion.complete();
							}
						});
						APIOkRequestsExecutor.getSingleton().queue(multiRequestBuilder.build(client));
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
	}

	public void testWithSingleCompletion() throws Exception {

		final String updatedTag = getUniqueString();
		
		startAdminSession();

		final Completion completion = new Completion();
		completion.run(new Runnable() {
			@Override
			public void run() {
				MediaEntry entry = new MediaEntry();
				entry.setName("test (" + new Date() + ")");
				entry.setMediaType(MediaType.IMAGE);
				entry.setReferenceId(getUniqueString());

				MediaEntry updateEntry = new MediaEntry();
				updateEntry.setTags(updatedTag);
		
				RequestBuilder<List<Object>> requestBuilder = SystemService.ping()
				.add(MediaService.add(entry))
				.add(MediaService.update("{2:result:id}", updateEntry))
				.add(MediaService.delete("{2:result:id}"))
				.setCompletion(new OnCompletion<List<Object>>() {
					
					@Override
					public void onComplete(List<Object> multi, APIException error) {
						completion.assertNull(error);
		
						// 0
						completion.assertNotNull(multi.get(0));
						completion.assertTrue(multi.get(0) instanceof Boolean);
						completion.assertTrue((Boolean) multi.get(0));

						// 1
						MediaEntry mEntry = (MediaEntry) multi.get(1);
						completion.assertNotNull(mEntry);
						completion.assertNotNull(mEntry.getId());

						// 2
						mEntry = (MediaEntry) multi.get(2);
						completion.assertEquals(updatedTag, mEntry.getTags());
						
						completion.complete();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
	}

	public void testWithManyCompletions() throws Exception {

		final String updatedTag = getUniqueString();
		
		startAdminSession();

		final Completion completion = new Completion();
		completion.run(new Runnable() {
			@Override
			public void run() {
				final AtomicInteger counter = new AtomicInteger(0);
				
				MediaEntry entry = new MediaEntry();
				entry.setName("test (" + new Date() + ")");
				entry.setMediaType(MediaType.IMAGE);
				entry.setReferenceId(getUniqueString());

				MediaEntry updateEntry = new MediaEntry();
				updateEntry.setTags(updatedTag);

				RequestBuilder<Boolean> systemServicePingRequestBuilder = SystemService.ping()
				.setCompletion(new OnCompletion<Boolean>() {
					
					@Override
					public void onComplete(Boolean response, APIException error) {
						completion.assertNull(error);
						completion.assertTrue(response);
						counter.incrementAndGet();
					}
				});
				
				RequestBuilder<MediaEntry> mediaServiceAddRequestBuilder = MediaService.add(entry)
				.setCompletion(new OnCompletion<MediaEntry>() {
					
					@Override
					public void onComplete(MediaEntry mEntry, APIException error) {
						completion.assertNull(error);
						completion.assertNotNull(mEntry);
						completion.assertNotNull(mEntry.getId());
						counter.incrementAndGet();
					}
				});
				
				RequestBuilder<MediaEntry> mediaServiceUpdateRequestBuilder = MediaService.update("{2:result:id}", updateEntry)
				.setCompletion(new OnCompletion<MediaEntry>() {
					
					@Override
					public void onComplete(MediaEntry mEntry, APIException error) {
						completion.assertNull(error);
						completion.assertNotNull(mEntry);
						completion.assertEquals(updatedTag, mEntry.getTags());
						counter.incrementAndGet();
					}
				});
				
				RequestBuilder<Void> mediaServiceDeleteRequestBuilder = MediaService.delete("{2:result:id}")
				.setCompletion(new OnCompletion<Void>() {
					
					@Override
					public void onComplete(Void response, APIException error) {
						completion.assertNull(error);
						counter.incrementAndGet();
					}
				});
				
				RequestBuilder<List<Object>> requestBuilder = new MultiRequestBuilder(
					systemServicePingRequestBuilder, 
					mediaServiceAddRequestBuilder, 
					mediaServiceUpdateRequestBuilder, 
					mediaServiceDeleteRequestBuilder
				)
				.setCompletion(new OnCompletion<List<Object>>() {
					
					@Override
					public void onComplete(List<Object> multi, APIException error) {
						completion.assertNull(error);
						completion.assertEquals(4, counter.get());
						completion.assertEquals(4, multi.size());
		
						completion.complete();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
	}
	
	
	/**
	 * This function tests that in a case of error in a multi request, the error is parsed correctly
	 * and it doesn't affect the rest of the multi-request.
	 * @throws KalturaAPIException
	 * @throws IOException 
	 */
	public void testWithError() throws Exception {
		
		startAdminSession();

		final Completion completion = new Completion();
		completion.run(new Runnable() {
			@Override
			public void run() {
				RequestBuilder<List<Object>> requestBuilder = SystemService.ping()
				.add(MediaService.get("Illegal String"))
				.add(SystemService.ping())
				.setCompletion(new OnCompletion<List<Object>>() {
		
					@Override
					public void onComplete(List<Object> multi, APIException error) {
						completion.assertNotNull(multi.get(0));
						completion.assertTrue(multi.get(0) instanceof Boolean);
						completion.assertTrue((Boolean) multi.get(0));
						
						completion.assertTrue(multi.get(1) instanceof APIException);

						completion.assertNotNull(multi.get(2));
						completion.assertTrue(multi.get(2) instanceof Boolean);
						completion.assertTrue((Boolean) multi.get(2));

						completion.complete();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
	}
	
	
	/**
	 * This function tests that in a case of error in a multi request, the error is parsed correctly
	 * and it doesn't affect the rest of the multi-request.
	 * @throws KalturaAPIException
	 * @throws IOException 
	 */
	public void testWithErrorWithManyCompletions() throws Exception {
		
		startAdminSession();

		final Completion completion = new Completion();
		completion.run(new Runnable() {
			@Override
			public void run() {
				final AtomicInteger counter = new AtomicInteger(0);
				
				RequestBuilder<Boolean> requestBuilder1 = SystemService.ping()
				.setCompletion(new OnCompletion<Boolean>() {
					
					@Override
					public void onComplete(Boolean response, APIException error) {
						completion.assertNull(error);
						completion.assertTrue(response);
						counter.incrementAndGet();
					}
				});
				
				RequestBuilder<MediaEntry> requestBuilder2 = MediaService.get("Illegal String")
				.setCompletion(new OnCompletion<MediaEntry>() {
					
					@Override
					public void onComplete(MediaEntry response, APIException error) {
						completion.assertNotNull(error);
						completion.assertNull(response);
						counter.incrementAndGet();
					}
				});
				
				RequestBuilder<Boolean> requestBuilder3 = SystemService.ping()
				.setCompletion(new OnCompletion<Boolean>() {
					
					@Override
					public void onComplete(Boolean response, APIException error) {
						completion.assertNull(error);
						completion.assertTrue(response);
						counter.incrementAndGet();
					}
				});
				
				RequestBuilder<List<Object>> requestBuilder = new MultiRequestBuilder(requestBuilder1, requestBuilder2, requestBuilder3)
				.setCompletion(new OnCompletion<List<Object>>() {
		
					@Override
					public void onComplete(List<Object> multi, APIException error) {
						completion.assertNull(error);

						completion.assertEquals(3, counter.get());
						completion.assertEquals(3, multi.size());
						
						completion.complete();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
	}
}
