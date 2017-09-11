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
import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.concurrent.CountDownLatch;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.EntryStatus;
import com.kaltura.client.enums.EntryType;
import com.kaltura.client.enums.MediaType;
import com.kaltura.client.enums.ModerationFlagType;
import com.kaltura.client.services.DataService;
import com.kaltura.client.services.DataService.AddDataBuilder;
import com.kaltura.client.services.FlavorAssetService;
import com.kaltura.client.services.FlavorAssetService.GetByEntryIdFlavorAssetBuilder;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.services.MediaService.AddContentMediaBuilder;
import com.kaltura.client.services.MediaService.AddFromUploadedFileMediaBuilder;
import com.kaltura.client.services.MediaService.AddFromUrlMediaBuilder;
import com.kaltura.client.services.MediaService.AddMediaBuilder;
import com.kaltura.client.services.MediaService.CountMediaBuilder;
import com.kaltura.client.services.MediaService.GetMediaBuilder;
import com.kaltura.client.services.MediaService.ListFlagsMediaBuilder;
import com.kaltura.client.services.MediaService.ListMediaBuilder;
import com.kaltura.client.services.MediaService.UpdateMediaBuilder;
import com.kaltura.client.services.MediaService.UploadMediaBuilder;
import com.kaltura.client.services.PlaylistService;
import com.kaltura.client.services.PlaylistService.ExecuteFromFiltersPlaylistBuilder;
import com.kaltura.client.services.UploadTokenService;
import com.kaltura.client.services.UploadTokenService.AddUploadTokenBuilder;
import com.kaltura.client.services.UploadTokenService.UploadUploadTokenBuilder;
import com.kaltura.client.types.BaseEntry;
import com.kaltura.client.types.DataEntry;
import com.kaltura.client.types.FlavorAsset;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.types.MediaEntryFilter;
import com.kaltura.client.types.MediaEntryFilterForPlaylist;
import com.kaltura.client.types.ModerationFlag;
import com.kaltura.client.types.UploadToken;
import com.kaltura.client.types.UploadedFileTokenResource;
import com.kaltura.client.utils.request.NullRequestBuilder;
import com.kaltura.client.utils.request.ServeRequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

public class MediaServiceTest extends BaseTest {

	/**
	 * Tests the following : 
	 * Media Service -
	 *  - add From Url
	 * @throws Exception 
	 */
	public void testAddFromUrl() throws Exception {
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
        
		final String name = getName() + " (" + new Date() + ")";
		
		addClipFromUrl(name, new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry addedEntry) {
				assertNotNull(addedEntry);
				assertNotNull(addedEntry.getId());
				assertEquals(name, addedEntry.getName());
				assertEquals(EntryStatus.IMPORT, addedEntry.getStatus());
				doneSignal.countDown();
			}
		});
		
		doneSignal.await();
	}
	
	public void addClipFromUrl(final String name, final OnCompletion<MediaEntry> onCompletion) {

		MediaEntry entry = new MediaEntry();

		entry.setName(name);
		entry.setType(EntryType.MEDIA_CLIP);
		entry.setMediaType(MediaType.VIDEO);

		AddFromUrlMediaBuilder requestBuilder = MediaService.addFromUrl(entry, testConfig.getTestUrl())
		.setCompletion(new OnCompletion<Response<MediaEntry>>() {

			@Override
			public void onComplete(Response<MediaEntry> result) {
				assertNull(result.error);

				if(result.results != null)
					testIds.add(result.results.getId());
				
				onCompletion.onComplete(result.results);
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
	}
	
	/**
	 * Tests the following : 
	 * Media Service - 
	 * 	- add 
	 *  - add Content
	 *  - count
	 * Upload token - 
	 *  - add
	 *  - upload
	 * Flavor asset - 
	 * 	- get by entry id
	 * @throws Exception 
	 */
	public void testUploadTokenAddGivenFile() throws Exception {
		
		final File fileData = TestUtils.getTestVideoFile();
		final long fileSize = fileData.length();
		final String uniqueTag = "test_" + getUniqueString();

		final MediaEntryFilter filter = new MediaEntryFilter();
		filter.setTagsLike(uniqueTag);
		
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		CountMediaBuilder requestBuilder = MediaService.count(filter)
		.setCompletion(new OnCompletion<Response<Integer>>() {

			@Override
			public void onComplete(Response<Integer> result) {
				assertNull(result.error);
				
				final int size = result.results;
				

				// Create entry
				MediaEntry entry = new MediaEntry();
				entry.setName(getName() + " (" + new Date() + ")");
				entry.setType(EntryType.MEDIA_CLIP);
				entry.setMediaType(MediaType.VIDEO);
				entry.setTags(uniqueTag);
				
				AddMediaBuilder requestBuilder = MediaService.add(entry)
				.setCompletion(new OnCompletion<Response<MediaEntry>>() {

					@Override
					public void onComplete(Response<MediaEntry> result) {
						assertNull(result.error);
						final MediaEntry entry = result.results;
						assertNotNull(entry);
						
						testIds.add(entry.getId());
						
						// Create token
						UploadToken uploadToken = new UploadToken();
						uploadToken.setFileName(testConfig.getUploadVideo());
						uploadToken.setFileSize((double) fileSize);

						AddUploadTokenBuilder requestBuilder = UploadTokenService.add(uploadToken)
						.setCompletion(new OnCompletion<Response<UploadToken>>() {

							@Override
							public void onComplete(Response<UploadToken> result) {
								assertNull(result.error);
								final UploadToken token = result.results;
								
								assertNotNull(token);
								
								// Define content
								UploadedFileTokenResource resource = new UploadedFileTokenResource();
								resource.setToken(token.getId());

								AddContentMediaBuilder requestBuilder = MediaService.addContent(entry.getId(), resource)
								.setCompletion(new OnCompletion<Response<MediaEntry>>() {

									@Override
									public void onComplete(Response<MediaEntry> result) {
										assertNull(result.error);
										final MediaEntry entry = result.results;
										assertNotNull(entry);

										// upload
										UploadUploadTokenBuilder requestBuilder = UploadTokenService.upload(token.getId(), fileData, false)
										.setCompletion(new OnCompletion<Response<UploadToken>>() {

											@Override
											public void onComplete(Response<UploadToken> result) {
												assertNull(result.error);
												UploadToken uploadToken = result.results;
												assertNotNull(uploadToken);
												
												// Test Creation
												getProcessedEntry(entry.getId(), true, new OnCompletion<MediaEntry>() {

													@Override
													public void onComplete(MediaEntry entry) {
														assertNotNull(entry);
														
														// Test get flavor asset by entry id.
														GetByEntryIdFlavorAssetBuilder requestBuilder = FlavorAssetService.getByEntryId(entry.getId())
														.setCompletion(new OnCompletion<Response<List<FlavorAsset>>>() {

															@Override
															public void onComplete(Response<List<FlavorAsset>> result) {
																assertNull(result.error);
																List<FlavorAsset> listFlavors = result.results;
																
																assertNotNull(listFlavors);
																assertTrue(listFlavors.size() >= 1); // Should contain at least the source
																

																CountMediaBuilder requestBuilder = MediaService.count(filter)
																.setCompletion(new OnCompletion<Response<Integer>>() {

																	@Override
																	public void onComplete(Response<Integer> result) {
																		assertNull(result.error);
																		
																		int size2 = result.results;
																		assertTrue(size + 1 == size2);
																		doneSignal.countDown();
																	}
																});
																APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
															}
														});
														APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
													}
												});
											}
										});
										APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
									}
								});
								APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
							}
						});
						APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	/**
	 * Tests the following : 
	 * Media Service -
	 *  - add From Url
	 * http://www.kaltura.org/how-update-supposed-work-api-v3
	 * @throws Exception 
	 */
	public void testUpdate() throws Exception {
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		String name = getName() + " (" + new Date() + ")";
		
		addTestImage(name, new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(final MediaEntry addedEntry) {
				assertNotNull(addedEntry);
				assertNotNull(addedEntry.getId());
				
				final String name2 = getName() + " (" + new Date() + ")";
				
				MediaEntry updatedEntry = new MediaEntry();
				updatedEntry.setName(name2);			
				
				UpdateMediaBuilder requestBuilder = MediaService.update(addedEntry.getId(), updatedEntry)
				.setCompletion(new OnCompletion<Response<MediaEntry>>() {

					@Override
					public void onComplete(Response<MediaEntry> result) {
						assertNull(result.error);

						getProcessedEntry(addedEntry.getId(), new OnCompletion<MediaEntry>() {

							@Override
							public void onComplete(MediaEntry queriedEntry) {
								assertEquals(name2, queriedEntry.getName());
								doneSignal.countDown();
							}
						});
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		});
		doneSignal.await();
	}
	
	/**
	 * Tests the following : 
	 * Media Service -
	 *  - Get
	 * @throws Exception 
	 */
	public void testBadGet() throws Exception {
		// look for one we know doesn't exist
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		GetMediaBuilder requestBuilder = MediaService.get("bad-id")
		.setCompletion(new OnCompletion<Response<MediaEntry>>() {

			@Override
			public void onComplete(Response<MediaEntry> result) {
				assertNull(result.results);
				assertNotNull(result.error);
				doneSignal.countDown();
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	/**
	 * Tests the following : 
	 * Media Service -
	 *  - Get
	 * @throws Exception 
	 */
	public void testGet() throws Exception {
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		String name = getName() + " (" + new Date() + ")";
		
		addTestImage(name, new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(final MediaEntry addedEntry) {
				GetMediaBuilder requestBuilder = MediaService.get(addedEntry.getId())
				.setCompletion(new OnCompletion<Response<MediaEntry>>() {

					@Override
					public void onComplete(Response<MediaEntry> result) {
						assertNull(result.error);
						MediaEntry retrievedEntry = result.results;
						assertNotNull(retrievedEntry);
						assertEquals(addedEntry.getId(), retrievedEntry.getId());
						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		});
		doneSignal.await();
	}

	private long count(ListResponse<MediaEntry> listResponse, final MediaEntry entry) {
		int count = 0;
		for(MediaEntry item : listResponse.getObjects()) {
			if(item.getId().equals(entry.getId())) { 
				count++;
			}
		}
		return count;
	}
	
	/**
	 * Tests the following : 
	 * Media Service -
	 *  - list
	 * @throws Exception 
	 */
	public void testList() throws Exception {
		final int count = 2;
		
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		OnCompletion<MediaEntry> onCompletion = new OnCompletion<MediaEntry>() {
			List<MediaEntry> entries = new ArrayList<MediaEntry>();

			@Override
			public void onComplete(MediaEntry addedEntry) {
				getProcessedEntry(addedEntry.getId(), new OnCompletion<MediaEntry>() {

					@Override
					public void onComplete(MediaEntry readyEntry) {
						entries.add(readyEntry);
						if(entries.size() == count) {
							testFilters();
						}
					}
				});
			}
			
			private void testFilters() {

				List<String> list = new ArrayList<String>();
				for(MediaEntry entry : entries) {
					list.add(entry.getName());
				}
				
				MediaEntryFilter filter = new MediaEntryFilter();
				filter.setNameMultiLikeOr(join(list));
				filter.setStatusIn(EntryStatus.IMPORT.getValue() + "," + EntryStatus.NO_CONTENT.getValue() + "," + EntryStatus.PENDING.getValue() + "," + EntryStatus.PRECONVERT.getValue() + "," + EntryStatus.READY.getValue());

				ListMediaBuilder requestBuilder = MediaService.list(filter)
				.setCompletion(new OnCompletion<Response<ListResponse<MediaEntry>>>() {

					@Override
					public void onComplete(Response<ListResponse<MediaEntry>> result) {
						assertNull(result.error);
						ListResponse<MediaEntry> listResponse = result.results;

						assertEquals(listResponse.getTotalCount(), count);

						for(final MediaEntry entry : entries) {
							long foundItems = count(listResponse, entry);
							assertEquals((long) 1, foundItems);
						}
						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		};
		
		for(int i = 0; i < count; i++) {
			// add test clips
			String name = getName() + " (" + new Date() + ")";
			addTestImage(name, onCompletion);
			
			try {
				Thread.sleep(1000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
		doneSignal.await();
	}
	
	/**
	 * Tests the following : 
	 * Media Service -
	 *  - flag
	 *  - list flags
	 * @throws Exception 
	 */
	public void testModeration() throws Exception {
		
		final String FLAG_COMMENTS = "This is a test flag: " + getName();
		final String name = getName() + " (" + new Date() + ")";
		
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		addTestImage(name, new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry addedEntry) {
				
				//wait for the newly-added clip to process
				getProcessedEntry(addedEntry.getId(), new OnCompletion<MediaEntry>() {

					@Override
					public void onComplete(final MediaEntry addedEntry) {
						
						// flag the clip
						ModerationFlag flag = new ModerationFlag();
						flag.setFlaggedEntryId(addedEntry.getId());
						flag.setFlagType(ModerationFlagType.SPAM_COMMERCIALS);
						flag.setComments(FLAG_COMMENTS);
						NullRequestBuilder requestBuilder = MediaService.flag(flag)
						.setCompletion(new OnCompletion<Response<Void>>() {

							@Override
							public void onComplete(Response<Void> result) {
								assertNull(result.error);

								// get the list of flags for this entry
								ListFlagsMediaBuilder requestBuilder = MediaService.listFlags(addedEntry.getId())
								.setCompletion(new OnCompletion<Response<ListResponse<ModerationFlag>>>() {

									@Override
									public void onComplete(Response<ListResponse<ModerationFlag>> result) {
										assertNull(result.error);
										ListResponse<ModerationFlag> flagList = result.results;

										assertEquals(flagList.getTotalCount(), 1);

										// check that the flag we put in is the flag we got back
										ModerationFlag retFlag = flagList.getObjects().get(0);						
										assertEquals(retFlag.getFlagType(), ModerationFlagType.SPAM_COMMERCIALS);
										assertEquals(retFlag.getComments(), FLAG_COMMENTS);
										
										doneSignal.countDown();
									}
								});
								APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
							}
						});
						APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
					}
				});
			}
		});
		doneSignal.await();
	}
	
	
	/**
	 * Tests the following : 
	 * Media Service -
	 *  - delete
	 * @throws IOException 
	 */
	public void testDelete() throws Exception {
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		String name = getName() + " (" + new Date() + ")";
		
		// First delete - should succeed
		addTestImage(name, new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry addedEntry) {
				assertNotNull(addedEntry);
				final String idToDelete = addedEntry.getId();
				
				// calling this makes the test wait for processing to complete
				// if you call delete while it is processing, the delete doesn't happen
				getProcessedEntry(idToDelete, new OnCompletion<MediaEntry>() {

					@Override
					public void onComplete(MediaEntry result) {
						NullRequestBuilder requestBuilder = MediaService.delete(idToDelete)
						.setCompletion(new OnCompletion<Response<Void>>() {

							@Override
							public void onComplete(Response<Void> result) {
								assertNull(result.error);

								// Get deleted - should fail
								GetMediaBuilder requestBuilder = MediaService.get(idToDelete)
								.setCompletion(new OnCompletion<Response<MediaEntry>>() {

									@Override
									public void onComplete(Response<MediaEntry> result) {
										assertNotNull("Exception expected as the entry should be already delete", result.error);
										doneSignal.countDown();
									}
								});
								APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
								
								// we whacked this one, so let's not keep track of it		
								testIds.remove(idToDelete);
							}
						});
						APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
					}
				});
			}
		});
		doneSignal.await();
	}
	
	/**
	 * Tests the following : 
	 * Media Service -
	 *  - upload
	 *  - add from uploaded file
	 * @throws Exception 
	 */
	public void testUpload() throws Exception {
		startUserSession();

		final File fileData = TestUtils.getTestVideoFile();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		UploadMediaBuilder requestBuilder = MediaService.upload(fileData)
		.setCompletion(new OnCompletion<Response<String>>() {

			@Override
			public void onComplete(Response<String> result) {
				assertNull(result.error);
				
				String name = getName() + " (" + new Date() + ")";
				MediaEntry entry = new MediaEntry();
				entry.setName(name);
				entry.setType(EntryType.MEDIA_CLIP);
				entry.setMediaType(MediaType.VIDEO);
				
				AddFromUploadedFileMediaBuilder requestBuilder = MediaService.addFromUploadedFile(entry, result.results)
				.setCompletion(new OnCompletion<Response<MediaEntry>>() {

					@Override
					public void onComplete(Response<MediaEntry> result) {
						assertNull(result.error);
						MediaEntry entry = result.results;
						
						assertNotNull(entry);
						assertNotNull(entry.getId());
						
						testIds.add(entry.getId());

						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	public void testPlaylist() throws Exception {
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		// Create entry
		addTestImage(getName() + " (" + new Date() + ")", new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry entry) {
				// generate filter
				MediaEntryFilterForPlaylist filter = new MediaEntryFilterForPlaylist();
				filter.setReferenceIdEqual(entry.getReferenceId());
				List<MediaEntryFilterForPlaylist> filters = new ArrayList<MediaEntryFilterForPlaylist>();
				filters.add(filter);
				ExecuteFromFiltersPlaylistBuilder requestBuilder = PlaylistService.executeFromFilters(filters, 5)
				.setCompletion(new OnCompletion<Response<List<BaseEntry>>>() {

					@Override
					public void onComplete(Response<List<BaseEntry>> result) {
						assertNull(result.error);
						assertNotNull(result.results);
						assertEquals(1, result.results.size());
						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		});
		doneSignal.await();
	}
	
	public void testServe() throws Exception {
		final String test = "bla bla bla";
		
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		// Add Entry
		DataEntry dataEntry = new DataEntry();
		dataEntry.setName(getName() + " (" + new Date() + ")");
		dataEntry.setDataContent(test);
		AddDataBuilder requestBuilder = DataService.add(dataEntry)
		.setCompletion(new OnCompletion<Response<DataEntry>>() {

			@Override
			public void onComplete(Response<DataEntry> result) {
				assertNull(result.error);

				// serve
				ServeRequestBuilder requestBuilder = DataService.serve(result.results.getId())
				.setCompletion(new OnCompletion<Response<String>>() {

					@Override
					public void onComplete(Response<String> result) {
						assertEquals(test, result.results);
						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		doneSignal.await();
	}
}
