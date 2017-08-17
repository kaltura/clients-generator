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
import java.util.List;
import java.util.concurrent.CountDownLatch;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.enums.MediaType;
import com.kaltura.client.enums.MetadataObjectType;
import com.kaltura.client.services.BaseEntryService;
import com.kaltura.client.services.BaseEntryService.DeleteBaseEntryBuilder;
import com.kaltura.client.services.CategoryEntryService;
import com.kaltura.client.services.CategoryEntryService.AddCategoryEntryBuilder;
import com.kaltura.client.services.CategoryService;
import com.kaltura.client.services.CategoryService.AddCategoryBuilder;
import com.kaltura.client.services.CategoryService.DeleteCategoryBuilder;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.services.MediaService.AddMediaBuilder;
import com.kaltura.client.services.MediaService.GetMediaBuilder;
import com.kaltura.client.services.MetadataProfileService;
import com.kaltura.client.services.MetadataProfileService.AddMetadataProfileBuilder;
import com.kaltura.client.services.MetadataProfileService.DeleteMetadataProfileBuilder;
import com.kaltura.client.services.MetadataService;
import com.kaltura.client.services.MetadataService.AddMetadataBuilder;
import com.kaltura.client.services.ResponseProfileService;
import com.kaltura.client.services.ResponseProfileService.AddResponseProfileBuilder;
import com.kaltura.client.services.ResponseProfileService.DeleteResponseProfileBuilder;
import com.kaltura.client.types.Category;
import com.kaltura.client.types.CategoryEntry;
import com.kaltura.client.types.CategoryEntryFilter;
import com.kaltura.client.types.DetachedResponseProfile;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.types.Metadata;
import com.kaltura.client.types.MetadataFilter;
import com.kaltura.client.types.MetadataProfile;
import com.kaltura.client.types.ResponseProfile;
import com.kaltura.client.types.ResponseProfileHolder;
import com.kaltura.client.types.ResponseProfileMapping;
import com.kaltura.client.utils.request.MultiRequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

public class ResponseProfileTest extends BaseTest{

	public void testEntryCategoriesAndMetadata() throws Exception {
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
        
		String xsd = "<xsd:schema xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n";
		xsd += "	<xsd:element name=\"metadata\">\n";
		xsd += "		<xsd:complexType>\n";
		xsd += "			<xsd:sequence>\n";
		xsd += "				<xsd:element name=\"Choice\" minOccurs=\"0\" maxOccurs=\"1\">\n";
		xsd += "					<xsd:annotation>\n";
		xsd += "						<xsd:documentation></xsd:documentation>\n";
		xsd += "						<xsd:appinfo>\n";
		xsd += "							<label>Example choice</label>\n";
		xsd += "							<key>choice</key>\n";
		xsd += "							<searchable>true</searchable>\n";
		xsd += "							<description>Example choice</description>\n";
		xsd += "						</xsd:appinfo>\n";
		xsd += "					</xsd:annotation>\n";
		xsd += "					<xsd:simpleType>\n";
		xsd += "						<xsd:restriction base=\"listType\">\n";
		xsd += "							<xsd:enumeration value=\"on\" />\n";
		xsd += "							<xsd:enumeration value=\"off\" />\n";
		xsd += "						</xsd:restriction>\n";
		xsd += "					</xsd:simpleType>\n";
		xsd += "				</xsd:element>\n";
		xsd += "				<xsd:element name=\"FreeText\" minOccurs=\"0\" maxOccurs=\"1\" type=\"textType\">\n";
		xsd += "					<xsd:annotation>\n";
		xsd += "						<xsd:documentation></xsd:documentation>\n";
		xsd += "						<xsd:appinfo>\n";
		xsd += "							<label>Free text</label>\n";
		xsd += "							<key>freeText</key>\n";
		xsd += "							<searchable>true</searchable>\n";
		xsd += "							<description>Free text</description>\n";
		xsd += "						</xsd:appinfo>\n";
		xsd += "					</xsd:annotation>\n";
		xsd += "				</xsd:element>\n";
		xsd += "			</xsd:sequence>\n";
		xsd += "		</xsd:complexType>\n";
		xsd += "	</xsd:element>\n";
		xsd += "	<xsd:complexType name=\"textType\">\n";
		xsd += "		<xsd:simpleContent>\n";
		xsd += "			<xsd:extension base=\"xsd:string\" />\n";
		xsd += "		</xsd:simpleContent>\n";
		xsd += "	</xsd:complexType>\n";
		xsd += "	<xsd:complexType name=\"objectType\">\n";
		xsd += "		<xsd:simpleContent>\n";
		xsd += "			<xsd:extension base=\"xsd:string\" />\n";
		xsd += "		</xsd:simpleContent>\n";
		xsd += "	</xsd:complexType>\n";
		xsd += "	<xsd:simpleType name=\"listType\">\n";
		xsd += "		<xsd:restriction base=\"xsd:string\" />\n";
		xsd += "	</xsd:simpleType>\n";
		xsd += "</xsd:schema>";
		
		final String xml = "<metadata><Choice>on</Choice><FreeText>example text: " + getName() + "</FreeText></metadata>";

		
		AddMediaBuilder entryRequest = createEntry();
		AddCategoryBuilder categoryRequest = createCategory();
		AddMetadataProfileBuilder categoryMetadataProfileRequest = createMetadataProfile(MetadataObjectType.CATEGORY, xsd);

		MetadataFilter metadataFilter = new MetadataFilter();
		metadataFilter.setMetadataObjectTypeEqual(MetadataObjectType.CATEGORY);
		//metadataFilter.setMetadataProfileIdEqual("{3:result:id}"); - responseProfile.relatedProfiles.0.relatedProfiles.0.filter.metadataProfileIdEqual

		ResponseProfileMapping metadataMapping = new ResponseProfileMapping();
		metadataMapping.setFilterProperty("objectIdEqual");
		metadataMapping.setParentProperty("categoryId");
		
		List<ResponseProfileMapping> metadataMappings = new ArrayList<ResponseProfileMapping>();
		metadataMappings.add(metadataMapping);

		final DetachedResponseProfile metadataResponseProfile = new DetachedResponseProfile();
		metadataResponseProfile.setName("metadata");
		metadataResponseProfile.setFilter(metadataFilter);
		metadataResponseProfile.setMappings(metadataMappings);
		
		List<DetachedResponseProfile> categoryEntryRelatedProfiles = new ArrayList<DetachedResponseProfile>();
		categoryEntryRelatedProfiles.add(metadataResponseProfile);

		CategoryEntryFilter categoryEntryFilter = new CategoryEntryFilter();
		
		ResponseProfileMapping categoryEntryMapping = new ResponseProfileMapping();
		categoryEntryMapping.setFilterProperty("entryIdEqual");
		categoryEntryMapping.setParentProperty("id");
		
		List<ResponseProfileMapping> categoryEntryMappings = new ArrayList<ResponseProfileMapping>();
		categoryEntryMappings.add(categoryEntryMapping);
		
		final DetachedResponseProfile categoryEntryResponseProfile = new DetachedResponseProfile();
		categoryEntryResponseProfile.setName("categoryEntry");
		categoryEntryResponseProfile.setRelatedProfiles(categoryEntryRelatedProfiles);
		categoryEntryResponseProfile.setFilter(categoryEntryFilter);
		categoryEntryResponseProfile.setMappings(categoryEntryMappings);
		
		List<DetachedResponseProfile> entryRelatedProfiles = new ArrayList<DetachedResponseProfile>();
		entryRelatedProfiles.add(categoryEntryResponseProfile);
		
		ResponseProfile responseProfile = new ResponseProfile();
		responseProfile.setName("rp" + System.currentTimeMillis());
		responseProfile.setSystemName(responseProfile.getName());
		responseProfile.setRelatedProfiles(entryRelatedProfiles);
		
		AddResponseProfileBuilder responseProfileRequest = ResponseProfileService.add(responseProfile);

		CategoryEntry categoryEntry = new CategoryEntry();
		categoryEntry.setEntryId("{1:result:id}");
//				categoryEntry.setCategoryId("{2:result:id}");
		
		AddCategoryEntryBuilder categoryEntryRequest = CategoryEntryService.add(categoryEntry);

		AddMetadataBuilder metadataRequest = MetadataService.add(Integer.MIN_VALUE, MetadataObjectType.CATEGORY, "{2:result:id}", xml);

		GetMediaBuilder getEntryRequest = MediaService.get("{1:result:id}");

		ResponseProfileHolder responseProfileHolder = new ResponseProfileHolder();
//				responseProfileHolder.setId("{4:result:id}");

		getEntryRequest.setResponseProfile(responseProfileHolder);
		
		MultiRequestBuilder multiRequestBuilder = new MultiRequestBuilder(entryRequest, categoryRequest, categoryMetadataProfileRequest, responseProfileRequest, categoryEntryRequest, metadataRequest, getEntryRequest);
		multiRequestBuilder.link(categoryMetadataProfileRequest, responseProfileRequest, "id", "addResponseProfile.relatedProfiles.0.relatedProfiles.0.filter.metadataProfileIdEqual");
		multiRequestBuilder.link(categoryRequest, categoryEntryRequest, "id", "categoryEntry.categoryId");
		multiRequestBuilder.link(categoryMetadataProfileRequest, metadataRequest, "id", "metadataProfileId");
		multiRequestBuilder.link(responseProfileRequest, getEntryRequest, "id", "responseProfile.id");
		
		multiRequestBuilder.setCompletion(new OnCompletion<Response<List<Object>>>() {
			
			@Override
			public void onComplete(Response<List<Object>> result) {
				List<Object> response = result.results;
				
				MediaEntry entry = (MediaEntry) response.get(0);

				Category category = (Category) response.get(1);

				MetadataProfile categoryMetadataProfile = (MetadataProfile) response.get(2);
				
				ResponseProfile responseProfile = (ResponseProfile) response.get(3);
				assertNotNull(responseProfile.getId());
				assertNotNull(responseProfile.getRelatedProfiles());
				assertEquals(1, responseProfile.getRelatedProfiles().size());

				CategoryEntry categoryEntry = (CategoryEntry) response.get(4);

				Metadata categoryMetadata = (Metadata) response.get(5);
				
				MediaEntry getEntry = (MediaEntry) response.get(6);
				assertEquals(getEntry.getId(), entry.getId());
				
				assertNotNull(getEntry.getRelatedObjects());
				assertTrue(getEntry.getRelatedObjects().containsKey(categoryEntryResponseProfile.getName()));
				
				@SuppressWarnings("unchecked")
				ListResponse<CategoryEntry> categoryEntryList = (ListResponse<CategoryEntry>) getEntry.getRelatedObjects().get(categoryEntryResponseProfile.getName());
				assertEquals(1, categoryEntryList.getTotalCount());
				
				CategoryEntry getCategoryEntry = categoryEntryList.getObjects().get(0);
				assertEquals(getCategoryEntry.getCreatedAt(), categoryEntry.getCreatedAt());
		
				assertNotNull(getCategoryEntry.getRelatedObjects());
				assertTrue(getCategoryEntry.getRelatedObjects().containsKey(metadataResponseProfile.getName()));
				
				@SuppressWarnings("unchecked")
				ListResponse<Metadata> metadataList = (ListResponse<Metadata>) getCategoryEntry.getRelatedObjects().get(metadataResponseProfile.getName());
				assertEquals(1, metadataList.getTotalCount());
				Metadata getMetadata = metadataList.getObjects().get(0);
				assertEquals(categoryMetadata.getId(), getMetadata.getId());
				assertEquals(xml, getMetadata.getXml());
			
				try {
					if(responseProfile != null && responseProfile.getId() > 0)
						deleteResponseProfile(responseProfile.getId());
			
					if(entry != null && entry.getId() != null)
						deleteEntry(entry.getId());
			
					if(category != null && category.getId() > 0)
						deleteCategory(category.getId());
			
					if(categoryMetadataProfile != null && categoryMetadataProfile.getId() > 0)
						deleteMetadataProfile(categoryMetadataProfile.getId());
				}
				catch(Exception e) {
					fail(e.getMessage());
				}
				
				doneSignal.countDown();
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(multiRequestBuilder.build(client));
		doneSignal.await();
	}

	public void testEntryCategoriesAndMetadataWithTokenizers() throws Exception {
		startAdminSession();

        final CountDownLatch doneSignal = new CountDownLatch(1);
        
		String xsd = "<xsd:schema xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n";
		xsd += "	<xsd:element name=\"metadata\">\n";
		xsd += "		<xsd:complexType>\n";
		xsd += "			<xsd:sequence>\n";
		xsd += "				<xsd:element name=\"Choice\" minOccurs=\"0\" maxOccurs=\"1\">\n";
		xsd += "					<xsd:annotation>\n";
		xsd += "						<xsd:documentation></xsd:documentation>\n";
		xsd += "						<xsd:appinfo>\n";
		xsd += "							<label>Example choice</label>\n";
		xsd += "							<key>choice</key>\n";
		xsd += "							<searchable>true</searchable>\n";
		xsd += "							<description>Example choice</description>\n";
		xsd += "						</xsd:appinfo>\n";
		xsd += "					</xsd:annotation>\n";
		xsd += "					<xsd:simpleType>\n";
		xsd += "						<xsd:restriction base=\"listType\">\n";
		xsd += "							<xsd:enumeration value=\"on\" />\n";
		xsd += "							<xsd:enumeration value=\"off\" />\n";
		xsd += "						</xsd:restriction>\n";
		xsd += "					</xsd:simpleType>\n";
		xsd += "				</xsd:element>\n";
		xsd += "				<xsd:element name=\"FreeText\" minOccurs=\"0\" maxOccurs=\"1\" type=\"textType\">\n";
		xsd += "					<xsd:annotation>\n";
		xsd += "						<xsd:documentation></xsd:documentation>\n";
		xsd += "						<xsd:appinfo>\n";
		xsd += "							<label>Free text</label>\n";
		xsd += "							<key>freeText</key>\n";
		xsd += "							<searchable>true</searchable>\n";
		xsd += "							<description>Free text</description>\n";
		xsd += "						</xsd:appinfo>\n";
		xsd += "					</xsd:annotation>\n";
		xsd += "				</xsd:element>\n";
		xsd += "			</xsd:sequence>\n";
		xsd += "		</xsd:complexType>\n";
		xsd += "	</xsd:element>\n";
		xsd += "	<xsd:complexType name=\"textType\">\n";
		xsd += "		<xsd:simpleContent>\n";
		xsd += "			<xsd:extension base=\"xsd:string\" />\n";
		xsd += "		</xsd:simpleContent>\n";
		xsd += "	</xsd:complexType>\n";
		xsd += "	<xsd:complexType name=\"objectType\">\n";
		xsd += "		<xsd:simpleContent>\n";
		xsd += "			<xsd:extension base=\"xsd:string\" />\n";
		xsd += "		</xsd:simpleContent>\n";
		xsd += "	</xsd:complexType>\n";
		xsd += "	<xsd:simpleType name=\"listType\">\n";
		xsd += "		<xsd:restriction base=\"xsd:string\" />\n";
		xsd += "	</xsd:simpleType>\n";
		xsd += "</xsd:schema>";
		
		final String xml = "<metadata><Choice>on</Choice><FreeText>example text: " + getName() + "</FreeText></metadata>";

		
		AddMediaBuilder entryRequest = createEntry();
		AddCategoryBuilder categoryRequest = createCategory();
		AddMetadataProfileBuilder categoryMetadataProfileRequest = createMetadataProfile(MetadataObjectType.CATEGORY, xsd);

		MultiRequestBuilder multiRequestBuilder = new MultiRequestBuilder(entryRequest, categoryRequest, categoryMetadataProfileRequest);
		
		MetadataFilter metadataFilter = new MetadataFilter();
		metadataFilter.setMetadataObjectTypeEqual(MetadataObjectType.CATEGORY);
		metadataFilter.metadataProfileIdEqual(categoryMetadataProfileRequest.getTokenizer().id());

		ResponseProfileMapping metadataMapping = new ResponseProfileMapping();
		metadataMapping.setFilterProperty("objectIdEqual");
		metadataMapping.setParentProperty("categoryId");
		
		List<ResponseProfileMapping> metadataMappings = new ArrayList<ResponseProfileMapping>();
		metadataMappings.add(metadataMapping);

		final DetachedResponseProfile metadataResponseProfile = new DetachedResponseProfile();
		metadataResponseProfile.setName("metadata");
		metadataResponseProfile.setFilter(metadataFilter);
		metadataResponseProfile.setMappings(metadataMappings);
		
		List<DetachedResponseProfile> categoryEntryRelatedProfiles = new ArrayList<DetachedResponseProfile>();
		categoryEntryRelatedProfiles.add(metadataResponseProfile);

		CategoryEntryFilter categoryEntryFilter = new CategoryEntryFilter();
		
		ResponseProfileMapping categoryEntryMapping = new ResponseProfileMapping();
		categoryEntryMapping.setFilterProperty("entryIdEqual");
		categoryEntryMapping.setParentProperty("id");
		
		List<ResponseProfileMapping> categoryEntryMappings = new ArrayList<ResponseProfileMapping>();
		categoryEntryMappings.add(categoryEntryMapping);
		
		final DetachedResponseProfile categoryEntryResponseProfile = new DetachedResponseProfile();
		categoryEntryResponseProfile.setName("categoryEntry");
		categoryEntryResponseProfile.setRelatedProfiles(categoryEntryRelatedProfiles);
		categoryEntryResponseProfile.setFilter(categoryEntryFilter);
		categoryEntryResponseProfile.setMappings(categoryEntryMappings);
		
		List<DetachedResponseProfile> entryRelatedProfiles = new ArrayList<DetachedResponseProfile>();
		entryRelatedProfiles.add(categoryEntryResponseProfile);
		
		ResponseProfile responseProfile = new ResponseProfile();
		responseProfile.setName("rp" + System.currentTimeMillis());
		responseProfile.setSystemName(responseProfile.getName());
		responseProfile.setRelatedProfiles(entryRelatedProfiles);
		
		AddResponseProfileBuilder responseProfileRequest = ResponseProfileService.add(responseProfile);
		multiRequestBuilder.add(responseProfileRequest);

		CategoryEntry categoryEntry = new CategoryEntry();
		categoryEntry.setEntryId(entryRequest.getTokenizer().id());
		categoryEntry.categoryId(categoryRequest.getTokenizer().id());
		
		AddCategoryEntryBuilder categoryEntryRequest = CategoryEntryService.add(categoryEntry);
		multiRequestBuilder.add(categoryEntryRequest);

		AddMetadataBuilder metadataRequest = MetadataService.add(Integer.MIN_VALUE, MetadataObjectType.CATEGORY, "{2:result:id}", xml);
		metadataRequest.metadataProfileId(categoryMetadataProfileRequest.getTokenizer().id());
		multiRequestBuilder.add(metadataRequest);

		GetMediaBuilder getEntryRequest = MediaService.get(entryRequest.getTokenizer().id());
		multiRequestBuilder.add(getEntryRequest);

		ResponseProfileHolder responseProfileHolder = new ResponseProfileHolder();
		responseProfileHolder.id(responseProfileRequest.getTokenizer().id());

		getEntryRequest.setResponseProfile(responseProfileHolder);
		
		multiRequestBuilder.setCompletion(new OnCompletion<Response<List<Object>>>() {
			
			@Override
			public void onComplete(Response<List<Object>> result) {
				assertNull(result.error);
				
				List<Object> response = result.results;
				
				MediaEntry entry = (MediaEntry) response.get(0);

				Category category = (Category) response.get(1);

				MetadataProfile categoryMetadataProfile = (MetadataProfile) response.get(2);
				
				ResponseProfile responseProfile = (ResponseProfile) response.get(3);
				assertNotNull(responseProfile.getId());
				assertNotNull(responseProfile.getRelatedProfiles());
				assertEquals(1, responseProfile.getRelatedProfiles().size());

				CategoryEntry categoryEntry = (CategoryEntry) response.get(4);

				Metadata categoryMetadata = (Metadata) response.get(5);
				
				MediaEntry getEntry = (MediaEntry) response.get(6);
				assertEquals(getEntry.getId(), entry.getId());
				
				assertNotNull(getEntry.getRelatedObjects());
				assertTrue(getEntry.getRelatedObjects().containsKey(categoryEntryResponseProfile.getName()));
				
				@SuppressWarnings("unchecked")
				ListResponse<CategoryEntry> categoryEntryList = (ListResponse<CategoryEntry>) getEntry.getRelatedObjects().get(categoryEntryResponseProfile.getName());
				assertEquals(1, categoryEntryList.getTotalCount());
				
				CategoryEntry getCategoryEntry = categoryEntryList.getObjects().get(0);
				assertEquals(getCategoryEntry.getCreatedAt(), categoryEntry.getCreatedAt());
		
				assertNotNull(getCategoryEntry.getRelatedObjects());
				assertTrue(getCategoryEntry.getRelatedObjects().containsKey(metadataResponseProfile.getName()));
				
				@SuppressWarnings("unchecked")
				ListResponse<Metadata> metadataList = (ListResponse<Metadata>) getCategoryEntry.getRelatedObjects().get(metadataResponseProfile.getName());
				assertEquals(1, metadataList.getTotalCount());
				Metadata getMetadata = metadataList.getObjects().get(0);
				assertEquals(categoryMetadata.getId(), getMetadata.getId());
				assertEquals(xml, getMetadata.getXml());
			
				try {
					if(responseProfile != null && responseProfile.getId() > 0)
						deleteResponseProfile(responseProfile.getId());
			
					if(entry != null && entry.getId() != null)
						deleteEntry(entry.getId());
			
					if(category != null && category.getId() > 0)
						deleteCategory(category.getId());
			
					if(categoryMetadataProfile != null && categoryMetadataProfile.getId() > 0)
						deleteMetadataProfile(categoryMetadataProfile.getId());
				}
				catch(Exception e) {
					fail(e.getMessage());
				}
				
				doneSignal.countDown();
			}
		});
		APIOkRequestsExecutor.getExecutor().queue(multiRequestBuilder.build(client));
		doneSignal.await();
	}
	
	protected AddMetadataProfileBuilder createMetadataProfile(MetadataObjectType objectType, String xsdData) {
		MetadataProfile metadataProfile = new MetadataProfile();
		metadataProfile.setMetadataObjectType(objectType);
		metadataProfile.setName("mp" + System.currentTimeMillis());
		
		return MetadataProfileService.add(metadataProfile, xsdData);
	}

	protected CategoryEntryService.AddCategoryEntryBuilder addEntryToCategory(String entryId, int categoryId) throws Exception {
		CategoryEntry categoryEntry = new CategoryEntry();
		categoryEntry.setEntryId(entryId);
		categoryEntry.setCategoryId(categoryId);
		
		return CategoryEntryService.add(categoryEntry);
	}

	protected AddMediaBuilder createEntry() {
		MediaEntry entry = new MediaEntry();
		entry.setMediaType(MediaType.VIDEO);
		
		return MediaService.add(entry);
	}

	protected AddCategoryBuilder createCategory() {
		Category category = new Category();
		category.setName("c" + System.currentTimeMillis());
		
		return CategoryService.add(category);
	}

	protected void deleteCategory(int id) throws Exception {
		startAdminSession();
		DeleteCategoryBuilder requestBuilder = CategoryService.delete(id);
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
	}

	protected void deleteEntry(String id) throws Exception {
		startAdminSession();
		DeleteBaseEntryBuilder requestBuilder = BaseEntryService.delete(id);
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
	}

	protected void deleteResponseProfile(int id) throws Exception {
		startAdminSession();
		DeleteResponseProfileBuilder requestBuilder = ResponseProfileService.delete(id);
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
	}

	protected void deleteMetadataProfile(int id) throws Exception {
		startAdminSession();
		DeleteMetadataProfileBuilder requestBuilder = MetadataProfileService.delete(id);
		APIOkRequestsExecutor.getExecutor().queue(requestBuilder.build(client));
	}
	
}
