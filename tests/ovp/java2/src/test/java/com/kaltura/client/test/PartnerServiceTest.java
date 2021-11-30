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
import java.util.Date;
import java.util.List;
import java.util.concurrent.CountDownLatch;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.EntryStatus;
import com.kaltura.client.enums.EntryType;
import com.kaltura.client.enums.MediaType;
import com.kaltura.client.enums.ModerationFlagType;
import com.kaltura.client.services.DataService;
import com.kaltura.client.services.DataService.AddDataBuilder;
import com.kaltura.client.services.FlavorAssetService;
import com.kaltura.client.services.FlavorAssetService.GetByEntryIdFlavorAssetBuilder;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.services.MediaService.AddContentMediaBuilder;
import com.kaltura.client.services.MediaService.AddFromUploadedFileMediaBuilder;
import com.kaltura.client.services.MediaService.AddFromUrlMediaBuilder;
import com.kaltura.client.services.MediaService.AddMediaBuilder;
import com.kaltura.client.services.MediaService.CountMediaBuilder;
import com.kaltura.client.services.MediaService.GetMediaBuilder;
import com.kaltura.client.services.MediaService.ListFlagsMediaBuilder;
import com.kaltura.client.services.MediaService.ListMediaBuilder;
import com.kaltura.client.services.MediaService.UpdateMediaBuilder;
import com.kaltura.client.services.MediaService.UploadMediaBuilder;
import com.kaltura.client.services.PlaylistService;
import com.kaltura.client.services.PlaylistService.ExecuteFromFiltersPlaylistBuilder;
import com.kaltura.client.services.UploadTokenService;
import com.kaltura.client.services.UploadTokenService.AddUploadTokenBuilder;
import com.kaltura.client.services.UploadTokenService.UploadUploadTokenBuilder;
import com.kaltura.client.types.BaseEntry;
import com.kaltura.client.types.Partner;
import com.kaltura.client.types.DataEntry;
import com.kaltura.client.types.FlavorAsset;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.types.MediaEntryFilter;
import com.kaltura.client.types.MediaEntryFilterForPlaylist;
import com.kaltura.client.types.ModerationFlag;
import com.kaltura.client.types.UploadToken;
import com.kaltura.client.types.UploadedFileTokenResource;
import com.kaltura.client.utils.request.NullRequestBuilder;
import com.kaltura.client.utils.request.ServeRequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

public class PartnerServiceTest extends BaseTest {

	public void testPartner() throws Exception {
		startAdminSession();

		final CountDownLatch doneSignal = new CountDownLatch(1);
		getTestPartner(new OnCompletion<Partner>() {

			@Override
			public void onComplete(Partner partner) {
				assertEquals(testConfig.getPartnerId(), (int) partner.getId());
				doneSignal.countDown();
			}
		});
		doneSignal.await();
	}
}
