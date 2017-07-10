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

import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutionException;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.Client;
import com.kaltura.client.services.BaseEntryService;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.services.SystemService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.BaseEntry;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.utils.request.ExecutedRequest;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.request.RequestElement;
import com.kaltura.client.utils.response.OnCompletion;

public class ErrorTest extends BaseTest {

	public void testInvalidServiceId() throws InterruptedException, ExecutionException {
		this.kalturaConfig.setEndpoint("http://2.2.2.2");
		this.kalturaConfig.setConnectTimeout(2000);
		
		final Client invalidClient = new Client(this.kalturaConfig);

        final CountDownLatch doneSignal = new CountDownLatch(1);

		RequestBuilder<Boolean> requestBuilder = SystemService.ping()
		.setCompletion(new OnCompletion<Boolean>() {
			
			@Override
			public void onComplete(Boolean response, APIException error) {
				assertNotNull("Ping to invalid end-point should fail", error);
				assertNull("Ping to invalid end-point should fail", response);
				
				doneSignal.countDown();
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(invalidClient));
		doneSignal.await();
	}
	
	public void testInvalidServerDnsName() throws InterruptedException, ExecutionException {
		this.kalturaConfig.setEndpoint("http://www.nonexistingkaltura.com");
		this.kalturaConfig.setConnectTimeout(2000);

		final Client invalidClient = new Client(this.kalturaConfig);

        final CountDownLatch doneSignal = new CountDownLatch(1);
        
		RequestBuilder<Boolean> requestBuilder = SystemService.ping()
		.setCompletion(new OnCompletion<Boolean>() {
			
			@Override
			public void onComplete(Boolean response, APIException error) {
				assertNotNull("Ping to invalid end-point should fail", error);
				assertNull("Ping to invalid end-point should fail", response);
				
				doneSignal.countDown();
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(invalidClient));
		doneSignal.await();
	}
	
	private class ExecutorMock {
		
		String resultToReturn;

		public ExecutorMock(String res) {
			resultToReturn = res;
		}

	    public void queue(RequestElement action) {
            ExecutedRequest responseElement = new ExecutedRequest().response(resultToReturn).success(true);
            action.onComplete(responseElement);
	    }
	}

	/**
	 * Tests case in which JSON format is completely ruined
	 * @throws ExecutionException 
	 * @throws InterruptedException 
	 */
	public void testJsonParsingError() throws InterruptedException, ExecutionException {
		final ExecutorMock mockClient = new ExecutorMock("Invalid JSON");
        final CountDownLatch doneSignal = new CountDownLatch(1);
        
		RequestBuilder<Boolean> requestBuilder = SystemService.ping()
		.setCompletion(new OnCompletion<Boolean>() {
			
			@Override
			public void onComplete(Boolean response, APIException error) {
				assertNotNull("Invalid JSON should fail", error);
				assertNull("Invalid JSON should fail", response);
				
				doneSignal.countDown();
			}
		});
		mockClient.queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	/**
	 * Tests case in which the response has JSON format, but no object type as expected
	 * @throws ExecutionException 
	 * @throws InterruptedException 
	 */
	public void testTagInSimpleType() throws InterruptedException, ExecutionException {
		final ExecutorMock mockClient = new ExecutorMock("{sometag: 1}");
        final CountDownLatch doneSignal = new CountDownLatch(1);
		RequestBuilder<Boolean> requestBuilder = SystemService.ping()
		.setCompletion(new OnCompletion<Boolean>() {
			
			@Override
			public void onComplete(Boolean response, APIException error) {
				assertNotNull("Invalid JSON should fail", error);
				assertNull("Invalid JSON should fail", response);
				
				doneSignal.countDown();
			}
		});
		mockClient.queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	/**
	 * Tests case in which the response has JSON format, but no object
	 * @throws ExecutionException 
	 * @throws InterruptedException 
	 */
	public void testEmptyObjectOrException() throws InterruptedException, ExecutionException {
		final ExecutorMock mockClient = new ExecutorMock("\"bla bla\"");
        final CountDownLatch doneSignal = new CountDownLatch(1);
		RequestBuilder<ListResponse<MediaEntry>> requestBuilder = MediaService.list()
		.setCompletion(new OnCompletion<ListResponse<MediaEntry>>() {
			
			@Override
			public void onComplete(ListResponse<MediaEntry> response, APIException error) {
				assertNotNull("Invalid JSON type should fail", error);
				assertNull("Invalid JSON type should fail", response);
				
				doneSignal.countDown();
			}
		});
		mockClient.queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	public void testUnknownObjectType() throws InterruptedException, ExecutionException  {

		final ExecutorMock mockClient = new ExecutorMock("{objectType: \"UnknownObjectType\"}");
        final CountDownLatch doneSignal = new CountDownLatch(1);
		RequestBuilder<MediaEntry> requestBuilder = MediaService.get("invalid-id")
		.setCompletion(new OnCompletion<MediaEntry>() {
			
			@Override
			public void onComplete(MediaEntry response, APIException error) {
				assertNull(error);
				assertTrue(response instanceof MediaEntry);
				
				doneSignal.countDown();
			}
		});
		mockClient.queue(requestBuilder.build(client));
		doneSignal.await();
	}
	
	public void testArrayOfUknownEntry() throws InterruptedException, ExecutionException {
		String testJson = "{objectType: \"KalturaMediaListResponse\", objects: [" +
				"{objectType: \"NonExistingclass\", id: \"test1\", name: \"test1\"}," +
				"{objectType: \"NonExistingclass\", id: \"test2\", name: \"test2\"}," +
				"{objectType: \"KalturaMediaEntry\", id: \"test3\", name: \"test3\"}" +
				"], totalCount: 3}";

		final ExecutorMock mockClient = new ExecutorMock(testJson);
        final CountDownLatch doneSignal = new CountDownLatch(1);
		RequestBuilder<ListResponse<BaseEntry>> requestBuilder = BaseEntryService.list()
		.setCompletion(new OnCompletion<ListResponse<BaseEntry>>() {
			
			@Override
			public void onComplete(ListResponse<BaseEntry> res, APIException error) {

				assertEquals(3, res.getTotalCount());
				assertEquals(3, res.getObjects().size());
				
				BaseEntry entry1 = res.getObjects().get(0);
				BaseEntry entry2 = res.getObjects().get(1);
				BaseEntry entry3 = res.getObjects().get(2);

				assertTrue(entry1 instanceof BaseEntry);
				assertEquals("test1", entry1.getId());
				assertEquals("test1", entry1.getName());

				assertTrue(entry2 instanceof BaseEntry);
				assertEquals("test2", entry2.getId());
				assertEquals("test2", entry2.getName());

				assertTrue(entry3 instanceof MediaEntry);
				assertEquals("test3", entry3.getId());
				assertEquals("test3", entry3.getName());

				doneSignal.countDown();
			}
		});
		mockClient.queue(requestBuilder.build(client));
		doneSignal.await();
	}
}
