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
import com.kaltura.client.ILogger;
import com.kaltura.client.Logger;
import com.kaltura.client.enums.EntryStatus;
import com.kaltura.client.enums.EntryType;
import com.kaltura.client.enums.MediaType;
import com.kaltura.client.enums.ModerationFlagType;
import com.kaltura.client.services.DataService;
import com.kaltura.client.services.FlavorAssetService;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.services.PlaylistService;
import com.kaltura.client.services.UploadTokenService;
import com.kaltura.client.types.APIException;
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
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;

public class MediaServiceTest extends BaseTest {

	private ILogger logger = Logger.getLogger(MediaServiceTest.class);
	
	/**
	 * Tests the following : 
	 * Media Service -
	 *  - add From Url
	 * @throws Exception 
	 */
	public void testAddFromUrl() throws Exception {
		if (logger.isEnabled()) 
			logger.info("Test Add From URL");
        
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
        
		final String name = "test (" + new Date() + ")";
		
		addClipFromUrl(name, new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry addedEntry, APIException error) {
				assertNull(error);
				
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

		RequestBuilder<MediaEntry> requestBuilder = MediaService.addFromUrl(entry, testConfig.getTestUrl())
		.setCompletion(new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry response, APIException error) {
				assertNull(error);

				if(response != null)
					testIds.add(response.getId());
				
				onCompletion.onComplete(response, error);
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
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
		
		if (logger.isEnabled())
			logger.info("Test upload token add");
		
		final File fileData = TestUtils.getTestVideoFile();
		final long fileSize = fileData.length();
		final String uniqueTag = "test_" + getUniqueString();

		final MediaEntryFilter filter = new MediaEntryFilter();
		filter.setTagsLike(uniqueTag);
		
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		RequestBuilder<Integer> requestBuilder = MediaService.count(filter)
		.setCompletion(new OnCompletion<Integer>() {

			@Override
			public void onComplete(Integer response, APIException error) {
				assertNull(error);
				
				final int size = response;
				

				// Create entry
				MediaEntry entry = new MediaEntry();
				entry.setName("test (" + new Date() + ")");
				entry.setType(EntryType.MEDIA_CLIP);
				entry.setMediaType(MediaType.VIDEO);
				entry.setTags(uniqueTag);
				
				RequestBuilder<MediaEntry> requestBuilder = MediaService.add(entry)
				.setCompletion(new OnCompletion<MediaEntry>() {

					@Override
					public void onComplete(final MediaEntry entry, APIException error) {
						assertNull(error);
						assertNotNull(entry);
						
						testIds.add(entry.getId());
						
						// Create token
						UploadToken uploadToken = new UploadToken();
						uploadToken.setFileName(testConfig.getUploadVideo());
						uploadToken.setFileSize((double) fileSize);

						RequestBuilder<UploadToken> requestBuilder = UploadTokenService.add(uploadToken)
						.setCompletion(new OnCompletion<UploadToken>() {

							@Override
							public void onComplete(final UploadToken token, APIException error) {
								assertNull(error);
								assertNotNull(token);
								
								// Define content
								UploadedFileTokenResource resource = new UploadedFileTokenResource();
								resource.setToken(token.getId());

								RequestBuilder<MediaEntry> requestBuilder = MediaService.addContent(entry.getId(), resource)
								.setCompletion(new OnCompletion<MediaEntry>() {

									@Override
									public void onComplete(final MediaEntry entry, APIException error) {
										assertNull(error);
										assertNotNull(entry);

										// upload
										RequestBuilder<UploadToken> requestBuilder = UploadTokenService.upload(token.getId(), fileData, false)
										.setCompletion(new OnCompletion<UploadToken>() {

											@Override
											public void onComplete(UploadToken uploadToken, APIException error) {
												assertNull(error);
												assertNotNull(uploadToken);
												
												// Test Creation
												getProcessedEntry(entry.getId(), true, new OnCompletion<MediaEntry>() {

													@Override
													public void onComplete(MediaEntry entry, APIException error) {
														assertNull(error);
														assertNotNull(entry);
														
														// Test get flavor asset by entry id.
														RequestBuilder<List<FlavorAsset>> requestBuilder = FlavorAssetService.getByEntryId(entry.getId())
														.setCompletion(new OnCompletion<List<FlavorAsset>>() {

															@Override
															public void onComplete(List<FlavorAsset> listFlavors, APIException error) {
																assertNull(error);
																assertNotNull(listFlavors);
																assertTrue(listFlavors.size() >= 1); // Should contain at least the source
																

																RequestBuilder<Integer> requestBuilder = MediaService.count(filter)
																.setCompletion(new OnCompletion<Integer>() {

																	@Override
																	public void onComplete(Integer response, APIException error) {
																		assertNull(error);
																		
																		int size2 = response;
																		assertTrue(size + 1 == size2);
																		doneSignal.countDown();
																	}
																});
																APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
															}
														});
														APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
													}
												});
											}
										});
										APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
									}
								});
								APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
							}
						});
						APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
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
		if (logger.isEnabled())
			logger.info("Test Update Entry");
		
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		String name = "test (" + new Date() + ")";
		
		addTestImage(name, new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(final MediaEntry addedEntry, APIException error) {
				assertNotNull(addedEntry);
				assertNotNull(addedEntry.getId());
				
				final String name2 = "test (" + new Date() + ")";
				
				MediaEntry updatedEntry = new MediaEntry();
				updatedEntry.setName(name2);			
				
				RequestBuilder<MediaEntry> requestBuilder = MediaService.update(addedEntry.getId(), updatedEntry)
				.setCompletion(new OnCompletion<MediaEntry>() {

					@Override
					public void onComplete(MediaEntry response, APIException error) {
						assertNull(error);

						getProcessedEntry(addedEntry.getId(), new OnCompletion<MediaEntry>() {

							@Override
							public void onComplete(MediaEntry queriedEntry, APIException error) {
								assertEquals(name2, queriedEntry.getName());
								doneSignal.countDown();
							}
						});
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
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
		if (logger.isEnabled())
			logger.info("Starting badGet test");
		
		// look for one we know doesn't exist
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		RequestBuilder<MediaEntry> requestBuilder = MediaService.get("bad-id")
		.setCompletion(new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry response, APIException error) {
				assertNull(response);
				assertNotNull(error);
				doneSignal.countDown();
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	/**
	 * Tests the following : 
	 * Media Service -
	 *  - Get
	 * @throws Exception 
	 */
	public void testGet() throws Exception {
		if (logger.isEnabled())
			logger.info("Starting get test");
		
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		String name = "test (" + new Date() + ")";
		
		addTestImage(name, new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(final MediaEntry addedEntry, APIException error) {
				assertNull(error);

				RequestBuilder<MediaEntry> requestBuilder = MediaService.get(addedEntry.getId())
				.setCompletion(new OnCompletion<MediaEntry>() {

					@Override
					public void onComplete(MediaEntry retrievedEntry, APIException error) {
						assertNull(error);

						assertNotNull(retrievedEntry);
						assertEquals(addedEntry.getId(), retrievedEntry.getId());
						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
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

		if (logger.isEnabled())
			logger.info("Test List");

		final int count = 2;
		
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		OnCompletion<MediaEntry> onCompletion = new OnCompletion<MediaEntry>() {
			List<MediaEntry> entries = new ArrayList<MediaEntry>();

			@Override
			public void onComplete(MediaEntry addedEntry, APIException error) {
				assertNull(error);

				getProcessedEntry(addedEntry.getId(), new OnCompletion<MediaEntry>() {

					@Override
					public void onComplete(MediaEntry readyEntry, APIException error) {
						assertNull(error);
						
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

				RequestBuilder<ListResponse<MediaEntry>> requestBuilder = MediaService.list(filter)
				.setCompletion(new OnCompletion<ListResponse<MediaEntry>>() {

					@Override
					public void onComplete(ListResponse<MediaEntry> listResponse, APIException error) {
						assertNull(error);

						assertEquals(listResponse.getTotalCount(), count);

						for(final MediaEntry entry : entries) {
							long foundItems = count(listResponse, entry);
							assertEquals((long) 1, foundItems);
						}
						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		};
		
		for(int i = 0; i < count; i++) {
			// add test clips
			String name = "test one (" + new Date() + ")";
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
		
		if (logger.isEnabled())
			logger.info("Starting moderation test");
		
		final String FLAG_COMMENTS = "This is a test flag";
		final String name = "test (" + new Date() + ")";
		
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		addTestImage(name, new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry addedEntry, APIException error) {

				if (logger.isEnabled())
					logger.info("Entry added: " + addedEntry.getId());
				
				//wait for the newly-added clip to process
				getProcessedEntry(addedEntry.getId(), new OnCompletion<MediaEntry>() {

					@Override
					public void onComplete(final MediaEntry addedEntry, APIException error) {
						
						// flag the clip
						ModerationFlag flag = new ModerationFlag();
						flag.setFlaggedEntryId(addedEntry.getId());
						flag.setFlagType(ModerationFlagType.SPAM_COMMERCIALS);
						flag.setComments(FLAG_COMMENTS);
						RequestBuilder<Void> requestBuilder = MediaService.flag(flag)
						.setCompletion(new OnCompletion<Void>() {

							@Override
							public void onComplete(Void response, APIException error) {
								assertNull(error);

								// get the list of flags for this entry
								RequestBuilder<ListResponse<ModerationFlag>> requestBuilder = MediaService.listFlags(addedEntry.getId())
								.setCompletion(new OnCompletion<ListResponse<ModerationFlag>>() {

									@Override
									public void onComplete(ListResponse<ModerationFlag> flagList, APIException error) {
										assertNull(error);

										assertEquals(flagList.getTotalCount(), 1);

										// check that the flag we put in is the flag we got back
										ModerationFlag retFlag = flagList.getObjects().get(0);						
										assertEquals(retFlag.getFlagType(), ModerationFlagType.SPAM_COMMERCIALS);
										assertEquals(retFlag.getComments(), FLAG_COMMENTS);
										
										doneSignal.countDown();
									}
								});
								APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
							}
						});
						APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
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
		if (logger.isEnabled())
			logger.info("Starting delete test");
		
		startUserSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		String name = "test (" + new Date() + ")";
		
		// First delete - should succeed
		addTestImage(name, new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry addedEntry, APIException error) {
				assertNotNull(addedEntry);
				final String idToDelete = addedEntry.getId();
				
				// calling this makes the test wait for processing to complete
				// if you call delete while it is processing, the delete doesn't happen
				getProcessedEntry(idToDelete, new OnCompletion<MediaEntry>() {

					@Override
					public void onComplete(MediaEntry response, APIException error) {
						assertNull(error);
						
						RequestBuilder<Void> requestBuilder = MediaService.delete(idToDelete)
						.setCompletion(new OnCompletion<Void>() {

							@Override
							public void onComplete(Void response, APIException error) {
								assertNull(error);

								// Get deleted - should fail
								RequestBuilder<MediaEntry> requestBuilder = MediaService.get(idToDelete)
								.setCompletion(new OnCompletion<MediaEntry>() {

									@Override
									public void onComplete(MediaEntry response, APIException error) {
										assertNotNull("Exception expected as the entry should be already delete", error);
										doneSignal.countDown();
									}
								});
								APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
								
								// we whacked this one, so let's not keep track of it		
								testIds.remove(idToDelete);
							}
						});
						APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
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
		if (logger.isEnabled())
			logger.info("Starting delete test");

		startUserSession();

		final File fileData = TestUtils.getTestVideoFile();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		RequestBuilder<String> requestBuilder = MediaService.upload(fileData)
		.setCompletion(new OnCompletion<String>() {

			@Override
			public void onComplete(String result, APIException error) {
				assertNull(error);

				if (logger.isEnabled())
					logger.debug("After upload, result:" + result);		
				
				String name = "test (" + new Date() + ")";
				MediaEntry entry = new MediaEntry();
				entry.setName(name);
				entry.setType(EntryType.MEDIA_CLIP);
				entry.setMediaType(MediaType.VIDEO);
				
				RequestBuilder<MediaEntry> requestBuilder = MediaService.addFromUploadedFile(entry, result)
				.setCompletion(new OnCompletion<MediaEntry>() {

					@Override
					public void onComplete(MediaEntry entry, APIException error) {
						assertNull(error);
						assertNotNull(entry);
						assertNotNull(entry.getId());
						
						testIds.add(entry.getId());

						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	public void testPlaylist() throws Exception {
		if (logger.isEnabled())	
			logger.info("Starting test playlist execute from filters");
	
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		// Create entry
		addTestImage("test (" + new Date() + ")", new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry entry, APIException error) {
				assertNull(error);
				
				// generate filter
				MediaEntryFilterForPlaylist filter = new MediaEntryFilterForPlaylist();
				filter.setReferenceIdEqual(entry.getReferenceId());
				List<MediaEntryFilterForPlaylist> filters = new ArrayList<MediaEntryFilterForPlaylist>();
				filters.add(filter);
				RequestBuilder<List<BaseEntry>> requestBuilder = PlaylistService.executeFromFilters(filters, 5)
				.setCompletion(new OnCompletion<List<BaseEntry>>() {

					@Override
					public void onComplete(List<BaseEntry> list, APIException error) {
						assertNull(error);
						assertNotNull(list);
						assertEquals(1, list.size());
						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
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
		dataEntry.setName("test (" + new Date() + ")");
		dataEntry.setDataContent(test);
		RequestBuilder<DataEntry> requestBuilder = DataService.add(dataEntry)
		.setCompletion(new OnCompletion<DataEntry>() {

			@Override
			public void onComplete(DataEntry addedDataEntry, APIException error) {
				assertNull(error);

				// serve
				RequestBuilder<String> requestBuilder = DataService.serve(addedDataEntry.getId())
				.setCompletion(new OnCompletion<String>() {

					@Override
					public void onComplete(String response, APIException error) {
						assertEquals(test, response);
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
