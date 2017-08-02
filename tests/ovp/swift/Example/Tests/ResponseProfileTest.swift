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
// Copyright (C) 2006-2017  Kaltura Inc.
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

/**
 * This class was generated using exec.php
 * against an XML schema provided by Kaltura.
 *
 * MANUAL CHANGES TO THIS CLASS WILL BE OVERWRITTEN.
 */

import Quick
import Nimble
import KalturaClient

class ResponseProfileTest: BaseTest {
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        client = Client(config)
        
        describe("response profile") {
            
            beforeEach {
                waitUntil(timeout: 500) { done in
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
            it("create and use") {
                
                var xsd = "<xsd:schema xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n"
                xsd += "  <xsd:element name=\"metadata\">\n"
                xsd += "    <xsd:complexType>\n"
                xsd += "      <xsd:sequence>\n"
                xsd += "        <xsd:element name=\"Choice\" minOccurs=\"0\" maxOccurs=\"1\">\n"
                xsd += "          <xsd:annotation>\n"
                xsd += "        <xsd:documentation></xsd:documentation>\n"
                xsd += "            <xsd:appinfo>\n"
                xsd += "              <label>Example choice</label>\n"
                xsd += "              <key>choice</key>\n"
                xsd += "              <searchable>true</searchable>\n"
                xsd += "              <description>Example choice</description>\n"
                xsd += "            </xsd:appinfo>\n"
                xsd += "          </xsd:annotation>\n"
                xsd += "          <xsd:simpleType>\n"
                xsd += "            <xsd:restriction base=\"listType\">\n"
                xsd += "              <xsd:enumeration value=\"on\" />\n"
                xsd += "              <xsd:enumeration value=\"off\" />\n"
                xsd += "            </xsd:restriction>\n"
                xsd += "          </xsd:simpleType>\n"
                xsd += "        </xsd:element>\n"
                xsd += "        <xsd:element name=\"FreeText\" minOccurs=\"0\" maxOccurs=\"1\" type=\"textType\">\n"
                xsd += "          <xsd:annotation>\n"
                xsd += "            <xsd:documentation></xsd:documentation>\n"
                xsd += "            <xsd:appinfo>\n"
                xsd += "              <label>Free text</label>\n"
                xsd += "              <key>freeText</key>\n"
                xsd += "              <searchable>true</searchable>\n"
                xsd += "              <description>Free text</description>\n"
                xsd += "            </xsd:appinfo>\n"
                xsd += "          </xsd:annotation>\n"
                xsd += "        </xsd:element>\n"
                xsd += "      </xsd:sequence>\n"
                xsd += "    </xsd:complexType>\n"
                xsd += "  </xsd:element>\n"
                xsd += "  <xsd:complexType name=\"textType\">\n"
                xsd += "    <xsd:simpleContent>\n"
                xsd += "      <xsd:extension base=\"xsd:string\" />\n"
                xsd += "    </xsd:simpleContent>\n"
                xsd += "  </xsd:complexType>\n"
                xsd += "  <xsd:complexType name=\"objectType\">\n"
                xsd += "    <xsd:simpleContent>\n"
                xsd += "      <xsd:extension base=\"xsd:string\" />\n"
                xsd += "    </xsd:simpleContent>\n"
                xsd += "  </xsd:complexType>\n"
                xsd += "  <xsd:simpleType name=\"listType\">\n"
                xsd += "    <xsd:restriction base=\"xsd:string\" />\n"
                xsd += "  </xsd:simpleType>\n"
                xsd += "</xsd:schema>"

                let xml = "<metadata><Choice>on</Choice><FreeText>example text: \(BaseTest.uniqueTag)</FreeText></metadata>";

                let entry = MediaEntry()
                entry.mediaType = MediaType.VIDEO
                entry.name = "Test - \(BaseTest.uniqueTag)"
                let entryRequest = MediaService.add(entry: entry)
                
                let category = Category()
                category.name = "Test - \(BaseTest.uniqueTag)"
                let categoryRequest = CategoryService.add(category: category)
                
                let categoryMetadataProfile = MetadataProfile()
                categoryMetadataProfile.metadataObjectType = MetadataObjectType.CATEGORY
                categoryMetadataProfile.name = "Test - \(BaseTest.uniqueTag)"
                let categoryMetadataProfileRequest = MetadataProfileService.add(metadataProfile: categoryMetadataProfile, xsdData: xsd)
                
                let metadataMapping = ResponseProfileMapping();
                metadataMapping.filterProperty = "objectIdEqual";
                metadataMapping.parentProperty = "categoryId";
                
                let categoryEntryMapping = ResponseProfileMapping()
                categoryEntryMapping.filterProperty = "entryIdEqual"
                categoryEntryMapping.parentProperty = "id"
                
                waitUntil(timeout: 5000) { done in
                    entryRequest.set(completion: {(createdEntry: MediaEntry?, error: ApiException?) in
                        
                        expect(error).to(beNil())
                        
                        expect(createdEntry).notTo(beNil())
                        expect(createdEntry?.id).notTo(beNil())
                        
                        let getEntryRequest = MediaService.get(entryId: (createdEntry?.id)!)

                        categoryRequest.set(completion: {(createdCategory: KalturaClient.Category?, error: ApiException?) in
                            
                            expect(error).to(beNil())
                            
                            expect(createdCategory).notTo(beNil())
                            expect(createdCategory?.id).notTo(beNil())
                            
                            let categoryEntry = CategoryEntry()
                            categoryEntry.entryId = createdEntry?.id
                            categoryEntry.categoryId = createdCategory?.id
                            
                            let categoryEntryRequest = CategoryEntryService.add(categoryEntry: categoryEntry)
                            
                            categoryEntryRequest.set(completion: {(createdCategoryEntry: CategoryEntry?, error: ApiException?) in
                                
                                expect(error).to(beNil())
                                
                                expect(createdCategoryEntry).notTo(beNil())

                                categoryMetadataProfileRequest.set(completion: {(createdMetadataProfile: MetadataProfile?, error: ApiException?) in
                                    
                                    expect(error).to(beNil())
                                    
                                    expect(createdMetadataProfile).notTo(beNil())
                                    expect(createdMetadataProfile?.id).notTo(beNil())
                                    
                                    let metadataFilter = MetadataFilter()
                                    metadataFilter.metadataObjectTypeEqual = MetadataObjectType.CATEGORY
                                    metadataFilter.metadataProfileIdEqual = createdMetadataProfile?.id
                                    
                                    let metadataResponseProfile = DetachedResponseProfile()
                                    metadataResponseProfile.name = "metadata"
                                    metadataResponseProfile.filter = metadataFilter
                                    metadataResponseProfile.mappings = [metadataMapping]
                                    
                                    let categoryEntryResponseProfile = DetachedResponseProfile()
                                    categoryEntryResponseProfile.name = "categoryEntry"
                                    categoryEntryResponseProfile.relatedProfiles = [metadataResponseProfile]
                                    categoryEntryResponseProfile.filter = CategoryEntryFilter()
                                    categoryEntryResponseProfile.mappings = [categoryEntryMapping]
                                    
                                    let responseProfile = ResponseProfile()
                                    responseProfile.name = "Test-\(BaseTest.uniqueTag)"
                                    responseProfile.systemName = responseProfile.name
                                    responseProfile.relatedProfiles = [categoryEntryResponseProfile]
                                    
                                    let responseProfileRequest = ResponseProfileService.add(addResponseProfile: responseProfile)
                                    
                                    let metadataRequest = MetadataService.add(metadataProfileId: (createdMetadataProfile?.id)!, objectType: MetadataObjectType.CATEGORY, objectId: "\((createdCategory?.id)!)", xmlData: xml)
                                    
                                    metadataRequest.set(completion: {(createdMetadata: Metadata?, error: ApiException?) in
                                        
                                        expect(error).to(beNil())
                                        
                                        expect(createdMetadata).notTo(beNil())
                                        expect(createdMetadata?.id).notTo(beNil())
                                        
                                        responseProfileRequest.set(completion: {(createdResponseProfile: ResponseProfile?, error: ApiException?) in
                                            
                                            expect(error).to(beNil())
                                            
                                            expect(createdResponseProfile).notTo(beNil())
                                            expect(createdResponseProfile?.id).notTo(beNil())
                                            
                                            let responseProfileHolder = ResponseProfileHolder()
                                            responseProfileHolder.id = createdResponseProfile?.id
                                            
                                            getEntryRequest.responseProfile = responseProfileHolder
                                            getEntryRequest.set(completion: {(getEntry: MediaEntry?, error: ApiException?) in
                                                
                                                expect(error).to(beNil())
                                                
                                                expect(getEntry).notTo(beNil())
                                                expect(getEntry?.id).notTo(beNil())
                                                expect(getEntry?.relatedObjects).notTo(beNil())
                                                expect(getEntry?.relatedObjects?.keys).to(contain(categoryEntryResponseProfile.name!))

                                                let categoryEntryList = getEntry?.relatedObjects?[categoryEntryResponseProfile.name!] as? CategoryEntryListResponse
                                                expect(categoryEntryList?.totalCount) == 1

                                                let getCategoryEntry = categoryEntryList?.objects?[0]
                                                expect(getCategoryEntry).notTo(beNil())
                                                expect(getCategoryEntry?.createdAt) == createdCategoryEntry!.createdAt!
                                                expect(getCategoryEntry?.relatedObjects).notTo(beNil())
                                                expect(getCategoryEntry?.relatedObjects?.keys).to(contain(metadataResponseProfile.name!))
                                                
                                                let metadataList = getCategoryEntry?.relatedObjects?[metadataResponseProfile.name!] as? MetadataListResponse
                                                expect(metadataList?.totalCount) == 1
                                                
                                                let getMetadata = metadataList?.objects![0]
                                                expect(getMetadata?.id) == createdMetadata!.id
                                                expect(getMetadata?.xml) == xml

                                                done()
                                            })
                                            self.executor.send(request: getEntryRequest.build(self.client!))
                                        })
                                        self.executor.send(request: responseProfileRequest.build(self.client!))
                                    })
                                    self.executor.send(request: metadataRequest.build(self.client!))
                                })
                                self.executor.send(request: categoryMetadataProfileRequest.build(self.client!))
                            })
                            self.executor.send(request: categoryEntryRequest.build(self.client!))
                        })
                        self.executor.send(request: categoryRequest.build(self.client!))
                    })
                    self.executor.send(request: entryRequest.build(self.client!))
                }
            }
        }
    }
}
