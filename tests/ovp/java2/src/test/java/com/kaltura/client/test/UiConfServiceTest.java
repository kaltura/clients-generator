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

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.ILogger;
import com.kaltura.client.Logger;
import com.kaltura.client.enums.UiConfCreationMode;
import com.kaltura.client.services.UiConfService;
import com.kaltura.client.test.BaseTest.Completion;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.UiConf;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;

public class UiConfServiceTest extends BaseTest {
	private ILogger logger = Logger.getLogger(UiConfServiceTest.class);

	// keeps track of test vids we upload so they can be cleaned up at the end
	protected List<Integer> testUiConfIds = new ArrayList<Integer>();
	
	protected void addUiConf(String name, final OnCompletion<UiConf> onCompletion) {

		UiConf uiConf = new UiConf();
		uiConf.setName(name);
		uiConf.setDescription("Ui conf unit test");
		uiConf.setHeight(373);
		uiConf.setWidth(750);
		uiConf.setCreationMode(UiConfCreationMode.ADVANCED);
		uiConf.setConfFile("NON_EXISTING_CONF_FILE");
		
		// this uiConf won't be editable in the KMC until it gets a config added to it, I think
		
		RequestBuilder<UiConf> requestBuilder = UiConfService.add(uiConf)
		.setCompletion(new OnCompletion<UiConf>() {
			
			@Override
			public void onComplete(UiConf addedConf, APIException error) {

				testUiConfIds.add(addedConf.getId());
				
				onCompletion.onComplete(addedConf, error);
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
	}
	
	public void testAddUiConf() throws Exception {
		if (logger.isEnabled())
			logger.info("Starting ui conf add test");

		startAdminSession();
		final Completion completion = new Completion();
		completion.run(new Runnable() {
			@Override
			public void run() {
				String name = "Test UI Conf (" + new Date() + ")";
				addUiConf(name, new OnCompletion<UiConf>() {

					@Override
					public void onComplete(UiConf addedConf, APIException error) {
						completion.assertNull(error);
						completion.assertNotNull(addedConf);
						completion.complete();
					}
				});
			}
		});
	}
	
	public void testGetUiConf() throws Exception {
		if (logger.isEnabled())
			logger.info("Starting ui get test");

		startAdminSession();
		final Completion completion = new Completion();
		completion.run(new Runnable() {
			@Override
			public void run() {
				String name = "Test UI Conf (" + new Date() + ")";
				addUiConf(name, new OnCompletion<UiConf>() {

					@Override
					public void onComplete(final UiConf addedConf, APIException error) {
						completion.assertNull(error);
						completion.assertNotNull(addedConf);
						
						RequestBuilder<UiConf> requestBuilder = UiConfService.get(addedConf.getId())
						.setCompletion(new OnCompletion<UiConf>() {
							
							@Override
							public void onComplete(UiConf retrievedConf, APIException error) {
								completion.assertNull(error);

								completion.assertEquals(retrievedConf.getId(), addedConf.getId());
								
								completion.complete();
							}
						});
						APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
					}
				});
			}
		});
	}
	
	public void testDeleteUiConf() throws Exception {
		if (logger.isEnabled())
			logger.info("Starting ui conf delete test");

		startAdminSession();
		final Completion completion = new Completion();
		completion.run(new Runnable() {
			@Override
			public void run() {
				String name = "Test UI Conf (" + new Date() + ")";
				addUiConf(name, new OnCompletion<UiConf>() {

					@Override
					public void onComplete(final UiConf addedConf, APIException error) {
						completion.assertNull(error);
						completion.assertNotNull(addedConf);
						
						RequestBuilder<Void> requestBuilder = UiConfService.delete(addedConf.getId())
						.setCompletion(new OnCompletion<Void>() {
							
							@Override
							public void onComplete(Void response, APIException error) {
								completion.assertNull(error);

								testUiConfIds.remove(addedConf.getId());

								RequestBuilder<UiConf> requestBuilder = UiConfService.get(addedConf.getId())
								.setCompletion(new OnCompletion<UiConf>() {
									
									@Override
									public void onComplete(UiConf retrievedConf, APIException error) {
										completion.assertNull(retrievedConf, "Getting deleted ui-conf should fail");
										completion.assertNotNull(error, "Getting deleted ui-conf should fail");

										completion.complete();
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

			RequestBuilder<Void> requestBuilder = UiConfService.delete(id);
			APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
		} //next id
	}
}
