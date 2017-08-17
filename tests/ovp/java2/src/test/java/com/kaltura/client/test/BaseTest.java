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
import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import java.util.UUID;

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
import com.kaltura.client.services.MediaService.AddContentMediaBuilder;
import com.kaltura.client.services.MediaService.AddMediaBuilder;
import com.kaltura.client.services.MediaService.GetMediaBuilder;
import com.kaltura.client.services.UploadTokenService;
import com.kaltura.client.services.UploadTokenService.AddUploadTokenBuilder;
import com.kaltura.client.services.UploadTokenService.UploadUploadTokenBuilder;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.types.UploadToken;
import com.kaltura.client.types.UploadedFileTokenResource;
import com.kaltura.client.utils.request.NullRequestBuilder;
import com.kaltura.client.utils.request.RequestElement;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

abstract class BaseTest extends TestCase {
	protected TestConfig testConfig;
	
	protected Configuration kalturaConfig = new Configuration();
	protected Client client;
	
	// keeps track of test vids we upload so they can be cleaned up at the end
	protected List<String> testIds = Collections.synchronizedList(new ArrayList<String>());

	protected boolean doCleanup = true;

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
	
	@Override
	protected void setUp() throws Exception {
		super.setUp();

		if (logger.isEnabled())
			logger.info("Starting test: " + getClass().getName() + "." + getName());
		
		testConfig = new TestConfig();
		
		// Create client
		this.kalturaConfig.setEndpoint(testConfig.getServiceUrl());
		this.client = new Client(this.kalturaConfig);
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

			NullRequestBuilder requestBuilder = MediaService.delete(id)
			.setCompletion(new OnCompletion<Response<Void>>() {

				@Override
				public void onComplete(Response<Void> result) {
					assertNull(result.error);
				}
			});
			APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		}
	}
	
	static public void assertNull(APIException object) {
		if(object != null) {
			throw new AssertionError(object);
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
	
	protected static String join(Collection<?> col) {
		return join(col, ",");
	}
	
	protected static String join(Collection<?> col, String delim) {
	    StringBuilder sb = new StringBuilder();
	    Iterator<?> iter = col.iterator();
	    if (iter.hasNext())
	        sb.append(iter.next().toString());
	    while (iter.hasNext()) {
	        sb.append(delim);
	        sb.append(iter.next().toString());
	    }
	    return sb.toString();
	}
	
	// Entry utils
	
	public void addTestImage(String name, final OnCompletion<MediaEntry> onCompletion)
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
		
		AddMediaBuilder requestBuilder = MediaService.add(entry)
		.setCompletion(new OnCompletion<Response<MediaEntry>>() {

			@Override
			public void onComplete(Response<MediaEntry> result) {
				assertNull(result.error);
				final MediaEntry entry = result.results;
				
				// Upload token
				UploadToken uploadToken = new UploadToken();
				uploadToken.setFileName(testConfig.getUploadImage());
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
								assertNotNull(entry);
								
								// upload
								UploadUploadTokenBuilder requestBuilder = UploadTokenService.upload(token.getId(), fileData, false)
								.setCompletion(new OnCompletion<Response<UploadToken>>() {

									@Override
									public void onComplete(Response<UploadToken> result) {
										assertNull(result.error);
										assertNotNull(token);
												
										testIds.add(entry.getId());
										MediaService.get(entry.getId());
										
										onCompletion.onComplete(entry);
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
	
	protected String getUniqueString() {
		return UUID.randomUUID().toString();
	}

	public void getProcessedEntry(String id, OnCompletion<MediaEntry> onCompletion) {
		getProcessedEntry(id, false, onCompletion);
	}
	
	private int counter = 0;
	public void getProcessedEntry(final String id, final Boolean checkReady, final OnCompletion<MediaEntry> onCompletion) {
		final int maxTries = 50;
		final int sleepInterval = 30 * 1000;
		counter = 0;

		GetMediaBuilder requestBuilder = MediaService.get(id);
		final RequestElement<MediaEntry> requestElement = requestBuilder.build(client);

		requestBuilder.setCompletion(new OnCompletion<Response<MediaEntry>>() {
			
			@Override
			public void onComplete(Response<MediaEntry> result) {
				assertNull(result.error);
				MediaEntry retrievedEntry = result.results;
				
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

					APIOkRequestsExecutor.getExecutor().queue(requestElement);
				}
				else {
					onCompletion.onComplete(retrievedEntry);
				}
			}
		});
		
		APIOkRequestsExecutor.getExecutor().queue(requestElement);
	}
}
