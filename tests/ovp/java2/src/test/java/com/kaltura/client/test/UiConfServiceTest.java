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

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.concurrent.CountDownLatch;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.ILogger;
import com.kaltura.client.Logger;
import com.kaltura.client.enums.UiConfCreationMode;
import com.kaltura.client.services.UiConfService;
import com.kaltura.client.services.UiConfService.AddUiConfBuilder;
import com.kaltura.client.services.UiConfService.DeleteUiConfBuilder;
import com.kaltura.client.services.UiConfService.GetUiConfBuilder;
import com.kaltura.client.types.UiConf;
import com.kaltura.client.utils.request.NullRequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

public class UiConfServiceTest extends BaseTest {
	private ILogger logger = Logger.getLogger(UiConfServiceTest.class);

	// keeps track of test vids we upload so they can be cleaned up at the end
	protected List<Integer> testUiConfIds = new ArrayList<Integer>();
	
	protected void addUiConf(String name, final OnCompletion<Response<UiConf>> onCompletion) {

		UiConf uiConf = new UiConf();
		uiConf.setName(name);
		uiConf.setDescription("Ui conf unit test");
		uiConf.setHeight(373);
		uiConf.setWidth(750);
		uiConf.setCreationMode(UiConfCreationMode.ADVANCED);
		uiConf.setConfFile("NON_EXISTING_CONF_FILE");
		
		// this uiConf won't be editable in the KMC until it gets a config added to it, I think
		
		AddUiConfBuilder requestBuilder = UiConfService.add(uiConf)
		.setCompletion(new OnCompletion<Response<UiConf>>() {
			
			@Override
			public void onComplete(Response<UiConf> result) {
				assertNull(result.error);
				UiConf addedConf = result.results;
				
				assertNotNull(addedConf);
				testUiConfIds.add(addedConf.getId());
				
				onCompletion.onComplete(new Response<UiConf>(addedConf, result.error));
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
	}
	
	public void testAddUiConf() throws Exception {
		startAdminSession();
        final CountDownLatch doneSignal = new CountDownLatch(1);
		String name = getName() + " (" + new Date() + ")";
		addUiConf(name, new OnCompletion<Response<UiConf>>() {

			@Override
			public void onComplete(Response<UiConf> result) {
				assertNull(result.error);
				assertNotNull(result.results);
				doneSignal.countDown();
			}
		});
		doneSignal.await();
	}
	
	public void testGetUiConf() throws Exception {
		startAdminSession();
        final CountDownLatch doneSignal = new CountDownLatch(1);
		String name = getName() + " (" + new Date() + ")";
		addUiConf(name, new OnCompletion<Response<UiConf>>() {

			@Override
			public void onComplete(Response<UiConf> result) {
				assertNull(result.error);
				final UiConf addedConf = result.results;
				assertNotNull(addedConf);
				
				GetUiConfBuilder requestBuilder = UiConfService.get(addedConf.getId())
				.setCompletion(new OnCompletion<Response<UiConf>>() {
					
					@Override
					public void onComplete(Response<UiConf> result) {
						assertNull(result.error);

						assertEquals(result.results.getId(), addedConf.getId());
						
						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		});
		doneSignal.await();
	}
	
	public void testDeleteUiConf() throws Exception {
		startAdminSession();
        final CountDownLatch doneSignal = new CountDownLatch(1);
		String name = getName() + " (" + new Date() + ")";
		addUiConf(name, new OnCompletion<Response<UiConf>>() {

			@Override
			public void onComplete(Response<UiConf> result) {
				assertNull(result.error);
				final UiConf addedConf = result.results;
				
				assertNotNull(addedConf);
				
				NullRequestBuilder requestBuilder = UiConfService.delete(addedConf.getId())
				.setCompletion(new OnCompletion<Response<Void>>() {
					
					@Override
					public void onComplete(Response<Void> result) {
						assertNull(result.error);

						testUiConfIds.remove(addedConf.getId());

						GetUiConfBuilder uiConfRequestBuilder = UiConfService.get(addedConf.getId())
						.setCompletion(new OnCompletion<Response<UiConf>>() {
							
							@Override
							public void onComplete(Response<UiConf> result) {
								assertNull("Getting deleted ui-conf should fail", result.results);
								assertNotNull("Getting deleted ui-conf should fail", result.error);

								doneSignal.countDown();
							}
						});
						APIOkRequestsExecutor.getExecutor().queue(uiConfRequestBuilder.build(client));
					}
				});
				APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
			}
		});
		doneSignal.await();
	}

	@Override
	protected void tearDown() throws Exception {
		
		super.tearDown();
		
		if (!doCleanup) return;
		
		if (logger.isEnabled())
			logger.info("Cleaning up test UI Conf entries after test");
		
		for (Integer id : this.testUiConfIds) {
			if (logger.isEnabled())
				logger.debug("Deleting UI conf " + id);

			DeleteUiConfBuilder requestBuilder = UiConfService.delete(id);
			APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
		} //next id
	}
}
