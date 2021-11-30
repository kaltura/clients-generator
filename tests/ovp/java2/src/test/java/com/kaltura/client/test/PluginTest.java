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

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.MetadataObjectType;
import com.kaltura.client.services.MetadataProfileService;
import com.kaltura.client.services.MetadataProfileService.AddMetadataProfileBuilder;
import com.kaltura.client.services.MetadataProfileService.DeleteMetadataProfileBuilder;
import com.kaltura.client.services.MetadataProfileService.UpdateMetadataProfileBuilder;
import com.kaltura.client.types.MetadataProfile;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

public class PluginTest extends BaseTest {

	public void testPlugin() throws Exception {
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		final String testString = "Test profile: " + getName();

		MetadataProfile profileAdd = new MetadataProfile();
		profileAdd.setMetadataObjectType(MetadataObjectType.ENTRY);
		profileAdd.setName(getName());

		AddMetadataProfileBuilder requestBuilder = MetadataProfileService.add(profileAdd, "<xml></xml>")
		.setCompletion(new OnCompletion<Response<MetadataProfile>>() {
			
			@Override
			public void onComplete(Response<MetadataProfile> result) {
				assertNull(result.error);
				MetadataProfile profileAdded = result.results;
				
				assertNotNull(profileAdded.getId());
				
				MetadataProfile profileUpdate = new MetadataProfile();
				profileUpdate.setName(testString);

				UpdateMetadataProfileBuilder requestBuilder = MetadataProfileService.update(profileAdded.getId(), profileUpdate)
				.setCompletion(new OnCompletion<Response<MetadataProfile>>() {
					
					@Override
					public void onComplete(Response<MetadataProfile> result) {
						assertNull(result.error);
						MetadataProfile profileUpdated = result.results;
						
						assertEquals(testString, profileUpdated.getName());
						
						DeleteMetadataProfileBuilder requestBuilder = MetadataProfileService.delete(profileUpdated.getId());
						APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
						
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
