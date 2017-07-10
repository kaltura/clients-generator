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

import java.io.IOException;
import java.util.concurrent.CountDownLatch;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;

public class SessionServiceTest extends BaseTest {

	/**
	 * Test Open / close Session
	 * @throws IOException 
	 */
	public void testSession() throws Exception {

		startUserSession();
		assertNotNull(client.getSessionId());

        final CountDownLatch doneSignal = new CountDownLatch(1);
		RequestBuilder<ListResponse<MediaEntry>> requestBuilder = MediaService.list()
		.setCompletion(new OnCompletion<ListResponse<MediaEntry>>() {
			
			@Override
			public void onComplete(ListResponse<MediaEntry> response, APIException error) {
				assertNull(error);
				assertNotNull(response);

				// Close session
				client.setSessionId(null);;

				RequestBuilder<ListResponse<MediaEntry>> requestBuilder = MediaService.list()
				.setCompletion(new OnCompletion<ListResponse<MediaEntry>>() {
					
					@Override
					public void onComplete(ListResponse<MediaEntry> response, APIException error) {
						assertNotNull(error);
						assertNull(response);

						
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
