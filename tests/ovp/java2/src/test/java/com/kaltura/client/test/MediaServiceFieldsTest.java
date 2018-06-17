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
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CountDownLatch;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.ContainerFormat;
import com.kaltura.client.enums.SiteRestrictionType;
import com.kaltura.client.services.AccessControlService;
import com.kaltura.client.services.AccessControlService.AddAccessControlBuilder;
import com.kaltura.client.services.AccessControlService.DeleteAccessControlBuilder;
import com.kaltura.client.services.AccessControlService.UpdateAccessControlBuilder;
import com.kaltura.client.services.ConversionProfileService;
import com.kaltura.client.services.ConversionProfileService.AddConversionProfileBuilder;
import com.kaltura.client.services.ConversionProfileService.DeleteConversionProfileBuilder;
import com.kaltura.client.services.ConversionProfileService.UpdateConversionProfileBuilder;
import com.kaltura.client.services.ThumbParamsService;
import com.kaltura.client.services.ThumbParamsService.AddThumbParamsBuilder;
import com.kaltura.client.services.ThumbParamsService.DeleteThumbParamsBuilder;
import com.kaltura.client.services.ThumbParamsService.GetThumbParamsBuilder;
import com.kaltura.client.services.ThumbParamsService.UpdateThumbParamsBuilder;
import com.kaltura.client.types.AccessControl;
import com.kaltura.client.types.BaseRestriction;
import com.kaltura.client.types.ConversionProfile;
import com.kaltura.client.types.CountryRestriction;
import com.kaltura.client.types.SiteRestriction;
import com.kaltura.client.types.ThumbParams;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

public class MediaServiceFieldsTest extends BaseTest {

	/**
	 * Tests that when we set values to their matching "NULL" their value isn't passed to the server.
	 * The parameter types that are tested : 
	 * String, int, EnumAsInt, EnumAsString.
	 * @throws IOException 
	 */
	public void testSetFieldValueShouldNotPass() throws Exception {

		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		final String testString = "Kaltura test: " + getName();
		final int testInt = 42;
		final ContainerFormat testEnumAsString = ContainerFormat.ISMV;

		ThumbParams paramsAdd = new ThumbParams();
		paramsAdd.setName(testString);
		paramsAdd.setDescription(testString);
		paramsAdd.setDensity(testInt);
		paramsAdd.setFormat(testEnumAsString);

		AddThumbParamsBuilder requestBuilder = ThumbParamsService.add(paramsAdd)
		.setCompletion(new OnCompletion<Response<ThumbParams>>() {
			
			@Override
			public void onComplete(Response<ThumbParams> result) {
				assertNull(result.error);
				final ThumbParams paramsAdded = result.results;

				assertEquals(testString, paramsAdded.getDescription());
				assertEquals(testInt, (int) paramsAdded.getDensity());
				assertEquals(testEnumAsString, paramsAdded.getFormat());

				// Null value not passed
				ThumbParams paramsUpdate = new ThumbParams();
				paramsUpdate.setDescription(null);
				paramsUpdate.setDensity(Integer.MIN_VALUE);
				paramsUpdate.setFormat(null);

				UpdateThumbParamsBuilder requestBuilder = ThumbParamsService.update(paramsAdded.getId(), paramsUpdate)
				.setCompletion(new OnCompletion<Response<ThumbParams>>() {
					
					@Override
					public void onComplete(Response<ThumbParams> result) {
						assertNull(result.error);

						GetThumbParamsBuilder requestBuilder = ThumbParamsService.get(paramsAdded.getId())
						.setCompletion(new OnCompletion<Response<ThumbParams>>() {
							
							@Override
							public void onComplete(Response<ThumbParams> result) {
								assertNull(result.error);
								ThumbParams paramsGot = result.results;
						
								assertEquals(testString, paramsGot.getDescription());
								assertEquals(testInt, (int) paramsGot.getDensity());
								assertEquals(testEnumAsString, paramsGot.getFormat());

								DeleteThumbParamsBuilder requestBuilder = ThumbParamsService.delete(paramsAdded.getId());
								APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));

								doneSignal.countDown();
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
	 * Tests that when we ask to set parameters to null, we indeed set them to null
	 * The parameter types that are tested : String
	 * @throws IOException 
	 */
	public void testSetFieldsToNullString() throws Exception {

		startAdminSession();

		final String testString = "Kaltura test: " + getName();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		ThumbParams paramsAdd = new ThumbParams();
		paramsAdd.setName(testString);
		paramsAdd.setDescription(testString);

		// Regular update works
		AddThumbParamsBuilder requestBuilder = ThumbParamsService.add(paramsAdd)
		.setCompletion(new OnCompletion<Response<ThumbParams>>() {
			
			@Override
			public void onComplete(Response<ThumbParams> result) {
				assertNull(result.error);
				final ThumbParams paramsAdded = result.results;

				assertEquals(testString, paramsAdded.getDescription());

				// Set to null
				ThumbParams paramsUpdate = new ThumbParams();
				paramsUpdate.setDescription("__null_string__");

				UpdateThumbParamsBuilder requestBuilder = ThumbParamsService.update(paramsAdded.getId(), paramsUpdate)
				.setCompletion(new OnCompletion<Response<ThumbParams>>() {
					
					@Override
					public void onComplete(Response<ThumbParams> result) {
						assertNull(result.error);
						ThumbParams paramsUpdated = result.results;
				
						assertNull(paramsUpdated.getDescription());

						DeleteThumbParamsBuilder requestBuilder = ThumbParamsService.delete(paramsAdded.getId());
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
	
	/**
	 * Tests that when we ask to set parameters to null, we indeed set them to null
	 * The parameter types that are tested : int
	 * @throws IOException 
	 */
	public void testSetFieldsToNullInt() throws Exception {

		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		final int testInt = 42;

		ConversionProfile profileAdd = new ConversionProfile();
		profileAdd.setName("Kaltura test: " + getName());
		profileAdd.setFlavorParamsIds("0");
		profileAdd.setStorageProfileId(testInt);

		// Regular update works
		AddConversionProfileBuilder requestBuilder = ConversionProfileService.add(profileAdd)
		.setCompletion(new OnCompletion<Response<ConversionProfile>>() {
			
			@Override
			public void onComplete(Response<ConversionProfile> result) {
				assertNull(result.error);
				ConversionProfile profileAdded = result.results;
				
				assertEquals(testInt, (int) profileAdded.getStorageProfileId());
				
				// Set to null
				ConversionProfile profileUpdate = new ConversionProfile();
				profileUpdate.setStorageProfileId(Integer.MAX_VALUE);
		
				UpdateConversionProfileBuilder requestBuilder = ConversionProfileService.update(profileAdded.getId(), profileUpdate)
				.setCompletion(new OnCompletion<Response<ConversionProfile>>() {
					
					@Override
					public void onComplete(Response<ConversionProfile> result) {
						assertNull(result.error);
						ConversionProfile profileUpdated = result.results;
						
						assertTrue(profileUpdated.getStorageProfileId() == null);
				
						DeleteConversionProfileBuilder requestBuilder = ConversionProfileService.delete(profileUpdated.getId());
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
	
	/**
	 * Tests that array update is working - 
	 * tests empty array, Null array & full array.
	 */
	public void testArrayConversion() throws Exception {

		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
		SiteRestriction resA = new SiteRestriction();
		resA.setSiteRestrictionType(SiteRestrictionType.RESTRICT_SITE_LIST);
		resA.setSiteList("ResA");
		CountryRestriction resB = new CountryRestriction();
		resB.setCountryList("IllegalCountry");
		
		List<BaseRestriction> restrictions = new ArrayList<BaseRestriction>();
		restrictions.add(resA);
		restrictions.add(resB);
		
		AccessControl accessControlAdd = new AccessControl();
		accessControlAdd.setName("Test access control: " + getName());
		accessControlAdd.setRestrictions(restrictions);
		
		AddAccessControlBuilder requestBuilder = AccessControlService.add(accessControlAdd)
		.setCompletion(new OnCompletion<Response<AccessControl>>() {
			
			@Override
			public void onComplete(Response<AccessControl> result) {
				assertNull(result.error);
				AccessControl accessControlAdded = result.results;
				
				assertNotNull(accessControlAdded.getRestrictions());
				assertEquals(2, accessControlAdded.getRestrictions().size());
				
				// Test null update - shouldn't update
				AccessControl accessControlUpdate = new AccessControl();
				accessControlUpdate.setName("Updated access control: " + getName());
				accessControlUpdate.setRestrictions(null); 

				UpdateAccessControlBuilder requestBuilder = AccessControlService.update(accessControlAdded.getId(), accessControlUpdate)
				.setCompletion(new OnCompletion<Response<AccessControl>>() {
					
					@Override
					public void onComplete(Response<AccessControl> result) {
						assertNull(result.error);
						AccessControl accessControlUpdated = result.results;
		
						assertEquals(2, accessControlUpdated.getRestrictions().size());
						
						// Test update Empty array - should update
						AccessControl accessControlUpdateAgain = new AccessControl();
						accessControlUpdateAgain.setName("Reset access control: " + getName());
						accessControlUpdateAgain.setRestrictions(new ArrayList<BaseRestriction>()); 

						UpdateAccessControlBuilder requestBuilder = AccessControlService.update(accessControlUpdated.getId(), accessControlUpdateAgain)
						.setCompletion(new OnCompletion<Response<AccessControl>>() {
							
							@Override
							public void onComplete(Response<AccessControl> result) {
								assertNull(result.error);
								AccessControl accessControlUpdatedAgain = result.results;
						
								assertEquals(0, accessControlUpdatedAgain.getRestrictions().size());
						
								// Delete entry
								DeleteAccessControlBuilder requestBuilder = AccessControlService.delete(accessControlUpdatedAgain.getId());
								APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
								
								doneSignal.countDown();
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

}
