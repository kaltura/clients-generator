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
import java.util.Collections;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import junit.framework.TestCase;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.Client;
import com.kaltura.client.Configuration;
import com.kaltura.client.ILogger;
import com.kaltura.client.Logger;
import com.kaltura.client.enums.EntryStatus;
import com.kaltura.client.enums.MediaType;
import com.kaltura.client.enums.SessionType;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.services.UploadTokenService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.types.UploadToken;
import com.kaltura.client.types.UploadedFileTokenResource;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.request.RequestElement;
import com.kaltura.client.utils.response.OnCompletion;

public class BaseTest extends TestCase {
	protected TestConfig testConfig;
	
	protected Configuration kalturaConfig = new Configuration();
	protected Client client;
	
	// keeps track of test vids we upload so they can be cleaned up at the end
	protected List<String> testIds = Collections.synchronizedList(new ArrayList<String>());

	protected boolean doCleanup = true;
	private ExecutorService executor;

	private static ILogger logger = Logger.getLogger(BaseTest.class);

	@SuppressWarnings("serial")
	protected class CompletionException extends Exception {
		
		public CompletionException(String message) {
			super(message);
		}
		
		@Override
		public String toString() {
			return getMessage();
		}
	}
	
	protected class Completion{
		
		@SuppressWarnings("rawtypes")
		private CompletableFuture future;
		
		@SuppressWarnings("rawtypes")
		public Completion() {
			future = new CompletableFuture();
		}
		
		public void run(Runnable runnable) throws InterruptedException, ExecutionException {
			executor.submit(runnable);
			Exception error = (Exception) future.get();

			if(error != null) {
				throw new AssertionError(error);
			}
		}
		
		@SuppressWarnings("unchecked")
		public void complete() {
			future.complete(null);
		}
		
		@SuppressWarnings("unchecked")
		public void fail(Exception e) {
			future.complete(new CompletionException(e.getMessage()));
			throw new RuntimeException(e);
		}
		
		@SuppressWarnings("unchecked")
		public void fail(String message) {
			future.complete(new CompletionException(message));
			throw new RuntimeException(message);
		}
		
		public void assertTrue(boolean condition) {
			this.assertTrue(condition, null);
		}
		
		public void assertTrue(boolean condition, String message) {
			if(!condition) {
				this.fail(message);
			}
		}
		
		public void assertNull(Exception exception) {
			if(exception == null) {
				return;
			}
			this.assertTrue(false, exception.getMessage());
		}
		
		public void assertNull(Object object) {
			this.assertNull(object, null);
		}
		
		public void assertNull(Object object, String message) {
			this.assertTrue(object == null, message);
		}
		
		public void assertNotNull(Object object, String message) {
			this.assertTrue(object != null, message);
		}
		
		public void assertNotNull(Object object) {
			this.assertNotNull(object, null);
		}

		public void assertEquals(int expected, int actual) {
			this.assertTrue(expected == actual, actual + " is different than " + expected);
		}

		public void assertEquals(long expected, long actual) {
			this.assertTrue(expected == actual, actual + " is different than " + expected);
		}

		public void assertEquals(Object expected, Object actual) {
			this.assertTrue(expected.equals(actual), actual + " is different than " + expected);
		}
	}
	
	@Override
	protected void setUp() throws Exception {
		super.setUp();
		
		testConfig = new TestConfig();
		
		// Create client
		this.kalturaConfig.setEndpoint(testConfig.getServiceUrl());
		this.client = new Client(this.kalturaConfig);
		
		executor = Executors.newScheduledThreadPool(1);
	}
	
	@Override
	protected void tearDown() throws Exception {
		super.tearDown();
		
		if (!doCleanup) return;
		
		if (logger.isEnabled())
			logger.info("Cleaning up test entries after test");
		
		for (final String id : this.testIds) {
			if (logger.isEnabled()) {
				logger.info("Deleting " + id);
			}

			RequestBuilder<Void> requestBuilder = MediaService.delete(id)
			.setCompletion(new OnCompletion<Void>() {

				@Override
				public void onComplete(Void response, APIException error) {
					assertNull(error);
				}
			});
			APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
		}
	}
	
	
	public void startUserSession() throws Exception{
		startSession(SessionType.USER);
	}
	
	public void startAdminSession() throws Exception{
		startSession(SessionType.ADMIN);
	}
	
	protected void startSession(SessionType type) throws Exception {
		
		String sessionId = client.generateSessionV2(testConfig.getAdminSecret(), testConfig.getUserId(), type, testConfig.getPartnerId(), 86400, "");
		if (logger.isEnabled()){
			logger.debug("Session id:" + sessionId);
		}
		
		client.setSessionId(sessionId);
	}
	
	// Entry utils
	
	public void addTestImage(final Completion completion, String name, final OnCompletion<MediaEntry> onCompletion)
	{
		MediaEntry entry = new MediaEntry();
		entry.setName(name);
		entry.setMediaType(MediaType.IMAGE);
		entry.setReferenceId(getUniqueString());
		
		final File fileData;
		final long fileSize;
		try {
			fileData = TestUtils.getTestImageFile();
			fileSize = fileData.length();
		} catch (IOException e) {
			fail(e.getMessage());
			return;
		}
		
		RequestBuilder<MediaEntry> requestBuilder = MediaService.add(entry)
		.setCompletion(new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(final MediaEntry entry, APIException error) {
				completion.assertNull(error);
				
				// Upload token
				UploadToken uploadToken = new UploadToken();
				uploadToken.setFileName(testConfig.getUploadImage());
				uploadToken.setFileSize((double) fileSize);
				RequestBuilder<UploadToken> requestBuilder = UploadTokenService.add(uploadToken)
				.setCompletion(new OnCompletion<UploadToken>() {

					@Override
					public void onComplete(final UploadToken token, APIException error) {
						completion.assertNull(error);
						completion.assertNotNull(token);
						
						// Define content
						UploadedFileTokenResource resource = new UploadedFileTokenResource();
						resource.setToken(token.getId());
						RequestBuilder<MediaEntry> requestBuilder = MediaService.addContent(entry.getId(), resource)
						.setCompletion(new OnCompletion<MediaEntry>() {

							@Override
							public void onComplete(final MediaEntry entry, APIException error) {
								completion.assertNull(error);
								completion.assertNotNull(entry);
								
								// upload
								RequestBuilder<UploadToken> requestBuilder = UploadTokenService.upload(token.getId(), fileData, false)
								.setCompletion(new OnCompletion<UploadToken>() {

									@Override
									public void onComplete(UploadToken token, APIException error) {
										completion.assertNull(error);
										completion.assertNotNull(token);
												
										testIds.add(entry.getId());
										MediaService.get(entry.getId());
										
										onCompletion.onComplete(entry, null);
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
	
	protected String getUniqueString() {
		return UUID.randomUUID().toString();
	}

	public void getProcessedEntry(Completion completion, String id, OnCompletion<MediaEntry> onCompletion) {
		getProcessedEntry(completion, id, false, onCompletion);
	}
	
	private int counter = 0;
	public void getProcessedEntry(final Completion completion, final String id, final Boolean checkReady, final OnCompletion<MediaEntry> onCompletion) {
		final int maxTries = 50;
		final int sleepInterval = 30 * 1000;
		counter = 0;

		RequestBuilder<MediaEntry> requestBuilder = MediaService.get(id);
		final RequestElement requestElement = requestBuilder.build(client);

		requestBuilder.setCompletion(new OnCompletion<MediaEntry>() {
			
			@Override
			public void onComplete(MediaEntry retrievedEntry, APIException error) {
				completion.assertNull(error);
				
				if(checkReady && retrievedEntry.getStatus() != EntryStatus.READY) {

					counter++;

					if (counter >= maxTries) {
						throw new RuntimeException("Max retries (" + maxTries
								+ ") when retrieving entry:" + id);
					} else {
						if (logger.isEnabled())
							logger.info("On try: " + counter + ", clip not ready. waiting "
								+ (sleepInterval / 1000) + " seconds...");
						try {
							Thread.sleep(sleepInterval);
						} catch (InterruptedException ie) {
							throw new RuntimeException("Failed while waiting for entry");
						}
					}

					APIOkRequestsExecutor.getSingleton().queue(requestElement);
				}
				else {
					onCompletion.onComplete(retrievedEntry, error);
				}
			}
		});
		
		APIOkRequestsExecutor.getSingleton().queue(requestElement);
	}
}
