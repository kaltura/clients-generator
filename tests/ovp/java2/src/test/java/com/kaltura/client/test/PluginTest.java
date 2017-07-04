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

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.MetadataObjectType;
import com.kaltura.client.services.MetadataProfileService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.MetadataProfile;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;

public class PluginTest extends BaseTest {

	public void testPlugin() throws Exception {
		startAdminSession();

		final Completion completion = new Completion();
		completion.run(new Runnable() {
			@Override
			public void run() {
				final String testString = "TEST PROFILE";

				MetadataProfile profileAdd = new MetadataProfile();
				profileAdd.setMetadataObjectType(MetadataObjectType.ENTRY);
				profileAdd.setName("asdasd");
		
				RequestBuilder<MetadataProfile> requestBuilder = MetadataProfileService.add(profileAdd, "<xml></xml>")
				.setCompletion(new OnCompletion<MetadataProfile>() {
					
					@Override
					public void onComplete(MetadataProfile profileAdded, APIException error) {
						completion.assertNull(error);
						
						completion.assertNotNull(profileAdded.getId());
						
						MetadataProfile profileUpdate = new MetadataProfile();
						profileUpdate.setName(testString);

						RequestBuilder<MetadataProfile> requestBuilder = MetadataProfileService.update(profileAdded.getId(), profileUpdate)
						.setCompletion(new OnCompletion<MetadataProfile>() {
							
							@Override
							public void onComplete(MetadataProfile profileUpdated, APIException error) {
								completion.assertNull(error);
								
								completion.assertEquals(testString, profileUpdated.getName());
								
								RequestBuilder<Void> requestBuilder = MetadataProfileService.delete(profileUpdated.getId());
								APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
								
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

}
