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
import com.kaltura.client.services.ConversionProfileService;
import com.kaltura.client.services.ThumbParamsService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.AccessControl;
import com.kaltura.client.types.BaseRestriction;
import com.kaltura.client.types.ConversionProfile;
import com.kaltura.client.types.CountryRestriction;
import com.kaltura.client.types.SiteRestriction;
import com.kaltura.client.types.ThumbParams;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;

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
		final String testString = "Kaltura test string";
		final int testInt = 42;
		final Boolean testEnumAsInt = false;
		final ContainerFormat testEnumAsString = ContainerFormat.ISMV;

		ThumbParams paramsAdd = new ThumbParams();
		paramsAdd.setName(testString);
		paramsAdd.setDescription(testString);
		paramsAdd.setDensity(testInt);
		paramsAdd.setIsSystemDefault(testEnumAsInt);
		paramsAdd.setFormat(testEnumAsString);

		RequestBuilder<ThumbParams> requestBuilder = ThumbParamsService.add(paramsAdd)
		.setCompletion(new OnCompletion<ThumbParams>() {
			
			@Override
			public void onComplete(final ThumbParams paramsAdded, APIException error) {
				assertNull(error);

				assertEquals(testString, paramsAdded.getDescription());
				assertEquals(testInt, (int) paramsAdded.getDensity());
				assertEquals(testEnumAsInt, paramsAdded.getIsSystemDefault());
				assertEquals(testEnumAsString, paramsAdded.getFormat());

				// Null value not passed
				ThumbParams paramsUpdate = new ThumbParams();
				paramsUpdate.setDescription(null);
				paramsUpdate.setDensity(Integer.MIN_VALUE);
				paramsUpdate.setIsSystemDefault(null);
				paramsUpdate.setFormat(null);

				RequestBuilder<ThumbParams> requestBuilder = ThumbParamsService.update(paramsAdded.getId(), paramsUpdate)
				.setCompletion(new OnCompletion<ThumbParams>() {
					
					@Override
					public void onComplete(ThumbParams paramsUpdated, APIException error) {
						assertNull(error);

						RequestBuilder<ThumbParams> requestBuilder = ThumbParamsService.get(paramsAdded.getId())
						.setCompletion(new OnCompletion<ThumbParams>() {
							
							@Override
							public void onComplete(ThumbParams paramsGot, APIException error) {
								assertNull(error);
						
								assertEquals(testString, paramsGot.getDescription());
								assertEquals(testInt, (int) paramsGot.getDensity());
								assertEquals(testEnumAsInt, paramsGot.getIsSystemDefault());
								assertEquals(testEnumAsString, paramsGot.getFormat());

								RequestBuilder<Void> requestBuilder = ThumbParamsService.delete(paramsAdded.getId());
								APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));

								doneSignal.countDown();
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
	 * Tests that when we ask to set parameters to null, we indeed set them to null
	 * The parameter types that are tested : String
	 * @throws IOException 
	 */
	public void testSetFieldsToNullString() throws Exception {

		startAdminSession();

		final String testString = "Kaltura test string";

        final CountDownLatch doneSignal = new CountDownLatch(1);
		ThumbParams paramsAdd = new ThumbParams();
		paramsAdd.setName(testString);
		paramsAdd.setDescription(testString);

		// Regular update works
		RequestBuilder<ThumbParams> requestBuilder = ThumbParamsService.add(paramsAdd)
		.setCompletion(new OnCompletion<ThumbParams>() {
			
			@Override
			public void onComplete(final ThumbParams paramsAdded, APIException error) {
				assertNull(error);

				assertEquals(testString, paramsAdded.getDescription());

				// Set to null
				ThumbParams paramsUpdate = new ThumbParams();
				paramsUpdate.setDescription("__null_string__");

				RequestBuilder<ThumbParams> requestBuilder = ThumbParamsService.update(paramsAdded.getId(), paramsUpdate)
				.setCompletion(new OnCompletion<ThumbParams>() {
					
					@Override
					public void onComplete(ThumbParams paramsUpdated, APIException error) {
						assertNull(error);
				
						assertNull(paramsUpdated.getDescription());

						RequestBuilder<Void> requestBuilder = ThumbParamsService.delete(paramsAdded.getId());
						APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));

						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
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
		profileAdd.setName("Kaltura test string");
		profileAdd.setFlavorParamsIds("0");
		profileAdd.setStorageProfileId(testInt);

		// Regular update works
		RequestBuilder<ConversionProfile> requestBuilder = ConversionProfileService.add(profileAdd)
		.setCompletion(new OnCompletion<ConversionProfile>() {
			
			@Override
			public void onComplete(ConversionProfile profileAdded, APIException error) {
				assertNull(error);
				
				assertEquals(testInt, (int) profileAdded.getStorageProfileId());
				
				// Set to null
				ConversionProfile profileUpdate = new ConversionProfile();
				profileUpdate.setStorageProfileId(Integer.MAX_VALUE);
		
				RequestBuilder<ConversionProfile> requestBuilder = ConversionProfileService.update(profileAdded.getId(), profileUpdate)
				.setCompletion(new OnCompletion<ConversionProfile>() {
					
					@Override
					public void onComplete(ConversionProfile profileUpdated, APIException error) {
						assertNull(error);
						
						assertTrue(profileUpdated.getStorageProfileId() == null);
				
						RequestBuilder<Void> requestBuilder = ConversionProfileService.delete(profileUpdated.getId());
						APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));

						doneSignal.countDown();
					}
				});
				APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
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
		accessControlAdd.setName("test access control");
		accessControlAdd.setRestrictions(restrictions);
		
		RequestBuilder<AccessControl> requestBuilder = AccessControlService.add(accessControlAdd)
		.setCompletion(new OnCompletion<AccessControl>() {
			
			@Override
			public void onComplete(AccessControl accessControlAdded, APIException error) {
				assertNull(error);
				
				assertNotNull(accessControlAdded.getRestrictions());
				assertEquals(2, accessControlAdded.getRestrictions().size());
				
				// Test null update - shouldn't update
				AccessControl accessControlUpdate = new AccessControl();
				accessControlUpdate.setName("updated access control");
				accessControlUpdate.setRestrictions(null); 

				RequestBuilder<AccessControl> requestBuilder = AccessControlService.update(accessControlAdded.getId(), accessControlUpdate)
				.setCompletion(new OnCompletion<AccessControl>() {
					
					@Override
					public void onComplete(AccessControl accessControlUpdated, APIException error) {
						assertNull(error);
		
						assertEquals(2, accessControlUpdated.getRestrictions().size());
						
						// Test update Empty array - should update
						AccessControl accessControlUpdateAgain = new AccessControl();
						accessControlUpdateAgain.setName("reset access control");
						accessControlUpdateAgain.setRestrictions(new ArrayList<BaseRestriction>()); 

						RequestBuilder<AccessControl> requestBuilder = AccessControlService.update(accessControlUpdated.getId(), accessControlUpdateAgain)
						.setCompletion(new OnCompletion<AccessControl>() {
							
							@Override
							public void onComplete(AccessControl accessControlUpdatedAgain, APIException error) {
								assertNull(error);
						
								assertEquals(0, accessControlUpdatedAgain.getRestrictions().size());
						
								// Delete entry
								RequestBuilder<Void> requestBuilder = AccessControlService.delete(accessControlUpdatedAgain.getId());
								APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(client));
								
								doneSignal.countDown();
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

}
