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

import java.io.InputStream;
import java.io.FileNotFoundException;
import java.io.IOException;

import com.kaltura.client.types.APIException;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.Client;
import com.kaltura.client.Configuration;
import com.kaltura.client.enums.EntryStatus;
import com.kaltura.client.enums.MediaType;
import com.kaltura.client.enums.SessionType;
import com.kaltura.client.services.BaseEntryService;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.services.PartnerService;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.types.Partner;
import com.kaltura.client.types.UploadToken;
import com.kaltura.client.types.UploadedFileTokenResource;
import com.kaltura.client.utils.request.MultiRequestBuilder;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.MultiResponse;
import com.kaltura.client.utils.response.base.ResponseElement;
import com.kaltura.client.test.TestConfig;
import com.kaltura.client.test.TestUtils;

public class Kaltura {
	
	private static final int WAIT_BETWEEN_TESTS = 30000;
	protected static TestConfig testConfig;
	static public Client client;
	
	public static void main(String[] args) throws IOException {

		if(testConfig == null){
			testConfig = new TestConfig();
		}
		
		try {

			get();
			list();
//			multiRequest();
//			MediaEntry entry = addEmptyEntry();
//			uploadMediaFileAndAttachToEmptyEntry(entry);
//			testIfEntryIsReadyForPublish(entry);
//			// cleanup the sample by deleting the entry:
//			deleteEntry(entry);
			System.out.println("Sample code finished successfully.");
			
		} catch (APIException e) {
			System.out.println("Example failed.");
			e.printStackTrace();
		}
	}
	
	/**
	 * Helper function to create the Kaltura client object once and then reuse a static instance.
	 * @return a singleton of <code>Client</code> used in this case 
	 * @throws APIException if failed to generate session
	 */
	private static Client getClient() throws APIException
	{
		if (client != null) {
			return client;
		}
		
		// Set Constants
		int partnerId = testConfig.getPartnerId();
		String adminSecret = testConfig.getAdminSecret();
		String userId = testConfig.getUserId();
		
		// Generate configuration
		Configuration config = new Configuration();
		config.setEndpoint(testConfig.getServiceUrl());
		
		try {
			// Create the client and open session
			client = new Client(config);
			String ks = client.generateSession(adminSecret, userId, SessionType.ADMIN, partnerId);
			client.setSessionId(ks);
		} catch(Exception ex) {
			client = null;
			throw new APIException("Failed to generate session");
		}
		
		System.out.println("Generated KS locally: [" + client.getSessionId() + "]");
		return client;
	}
	
	/** 
	 * lists all media in the account.
	 */
	private static void get() throws APIException {

		RequestBuilder<MediaEntry> requestBuilder = MediaService.get("1_mbz8wa9f")
		.setCompletion(new OnCompletion<MediaEntry>() {

			@Override
			public void onComplete(MediaEntry entry, APIException error) {
				if(error != null) {
					System.err.println(error);
				}
				else {
					System.out.println("Got entry [" + entry.getId() + "]: " + entry.getName());
				}
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(getClient()));
	}
	
	/** 
	 * lists all media in the account.
	 */
	private static void list() throws APIException {

		RequestBuilder<ListResponse<MediaEntry>> requestBuilder = MediaService.list()
		.setCompletion(new OnCompletion<ListResponse<MediaEntry>>() {

			@Override
			public void onComplete(ListResponse<MediaEntry> list, APIException error) {
				if(error != null) {
					System.err.println(error);
				}
				if (list.getTotalCount() > 0) {
					System.out.println("The account contains " + list.getTotalCount() + " entries.");
					for (MediaEntry entry : list.getObjects()) {
						System.out.println("\t \"" + entry.getName() + "\"");
					}
				} else {
					System.out.println("This account doesn't have any entries in it.");
				}
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(getClient()));
	}

	/**
	 * shows how to chain requests together to call a multi-request type where several requests are called in a single request.
	 */
	private static void multiRequest() throws APIException {
		MultiRequestBuilder requestBuilder = BaseEntryService.count()
		.add(PartnerService.getInfo())
		.add(PartnerService.getUsage(2017))
		.setCompletion(new OnCompletion<MultiResponse>() {

			@Override
			public void onComplete(MultiResponse response, APIException error) {
				Partner partner = (Partner) response.get(1);
				System.out.println("Got Admin User email: " + partner.getAdminEmail());
			}
		});
		APIOkRequestsExecutor.getSingleton().queue(requestBuilder.build(getClient()));

	}
	
//	/** 
//	 * creates an empty media entry and assigns basic metadata to it.
//	 * @return the generated <code>MediaEntry</code>
//	 * @throws APIException
//	 */
//	private static MediaEntry addEmptyEntry() throws APIException {
//		System.out.println("Creating an empty Kaltura Entry (without actual media binary attached)...");
//		MediaEntry entry = new MediaEntry();
//		entry.getName() = "An Empty Kaltura Entry Test";
//		entry.getMediaType() = MediaType.VIDEO;
//		MediaEntry newEntry = MediaService().add(entry);
//		System.out.println("The id of our new Video Entry is: " + newEntry.getId());
//		return newEntry;
//	}
//	
//	/**
//	 *  uploads a video file to Kaltura and assigns it to a given Media Entry object
//	 */
//	private static void uploadMediaFileAndAttachToEmptyEntry(MediaEntry entry) throws APIException
//	{
//			Client client = getKalturaClient();			
//			System.out.println("Uploading a video file...");
//			
//			// upload upload token
//			UploadToken upToken = UploadTokenService.add();
//			UploadedFileTokenResource fileTokenResource = new UploadedFileTokenResource();
//			
//			// Connect to media entry and update name
//			fileTokenResource.setToken(upToken.getId());
//			entry = client.getMediaService().addContent(entry.getId(), fileTokenResource);
//			
//			// Upload actual data
//			try
//			{
//				InputStream fileData = TestUtils.getTestVideo();
//				int fileSize = fileData.available();
//
//				UploadTokenService.upload(upToken.getId(), fileData, testConfig.getUploadVideo(), fileSize);
//				
//				System.out.println("Uploaded a new Video file to entry: " + entry.getId());
//			}
//			catch (FileNotFoundException e)
//			{
//				System.out.println("Failed to open test video file");
//			}
//			catch (IOException e)
//			{
//				System.out.println("Failed to read test video file");
//			}
//	}
//	
//	/** 
//	 * periodically calls the Kaltura API to check that a given video entry has finished transcoding and is ready for playback. 
//	 * @param entry The <code>MediaEntry</code> we want to test
//	 */
//	private static void testIfEntryIsReadyForPublish(MediaEntry entry)
//			throws APIException {
//
//		System.out.println("Testing if Media Entry has finished processing and ready to be published...");
//		while (true) {
//			MediaEntry retrievedEntry = MediaService.get(entry.id);
//			if (retrievedEntry.status == EntryStatus.READY) {
//				break;
//			}
//			System.out.println("Media not ready yet. Waiting 30 seconds.");
//			try {
//				Thread.sleep(WAIT_BETWEEN_TESTS);
//			} catch (InterruptedException ie) {
//			}
//		}
//		System.out.println("Entry id: " + entry.id + " is now ready to be published and played.");
//	}
//
//	/** 
//	 * deletes a given entry
//	 * @param entry the <code>MediaEntry</code> we want to delete
//	 * @throws APIException
//	 */
//	private static void deleteEntry(MediaEntry entry)
//			throws APIException {
//		System.out.println("Deleting entry id: " + entry.id);
//		MediaService.delete(entry.id);
//	}
}
