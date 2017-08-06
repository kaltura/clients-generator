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

class MultirequestTest: BaseTest {
    var entryIds: [String] = []
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        client = Client(config)
        
        describe("multirequest") {
            
            afterEach {
                for entryId in self.entryIds {
                    waitUntil(timeout: 500) { done in
                        self.deleteEntry(entryId: entryId) { error in
                            expect(error).to(beNil())
                            done()
                        }
                    }
                }
                self.entryIds = []
            }
            
            let fileName = "DemoVideo"
            let fileType = "flv"
            
            let testBundle = Bundle(for: type(of: self))
            let filePath = testBundle.path(forResource: fileName, ofType: fileType)
            
            var fileElement: RequestFile?
            var fileSize: Double?
            
            do {
                let fileURL = testBundle.url(forResource: fileName, withExtension: fileType)
                fileElement = try RequestFileElement(url: fileURL!)
                
                let attr = try FileManager.default.attributesOfItem(atPath: filePath!)
                fileSize = attr[FileAttributeKey.size] as? Double
            } catch {
                fail("\(error)")
                return
            }
            
            it("login") {
                waitUntil(timeout: 500) { done in
                    
                    let entry = MediaEntry()
                    entry.name = "Multirequest login Test - \(BaseTest.uniqueTag)"
                    entry.mediaType = MediaType.IMAGE
                    entry.referenceId = BaseTest.uniqueTag
                    
                    let entryRequestBuilder = MediaService.add(entry: entry);
                    entryRequestBuilder.ks = "{2:result}"
                    
                    let requestBuilder = SystemService.ping()
                        .add(request: SessionService.start(secret: self.secret!, userId: nil, type: SessionType.ADMIN, partnerId: self.partnerId))
                        .add(request: entryRequestBuilder)
                        .set(completion: {(response: Array<Any?>?, error: ApiException?) in
                            
                            expect(error).to(beNil())
                            
                            expect(response).notTo(beNil())
                            expect(response?.count) == 3
                            
                            // 0
                            let ping = response?[0] as? Bool
                            expect(ping) == true
                            
                            // 2
                            let createdEntry = response?[2] as? MediaEntry
                            expect(createdEntry?.id).notTo(beNil())
                            expect(createdEntry?.status) == EntryStatus.NO_CONTENT
                            
                            self.entryIds.append((createdEntry?.id)!)
                            
                            done()
                        })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
            
            it("file upload") {
                waitUntil(timeout: 500) { done in
                    self.login() { error in
                        expect(error).to(beNil())
                        
                        let entry = MediaEntry()
                        entry.name = "Multirequest upload Test - \(BaseTest.uniqueTag)"
                        entry.mediaType = MediaType.IMAGE
                        entry.referenceId = BaseTest.uniqueTag
                        
                        let uploadToken = UploadToken()
                        uploadToken.fileName = fileElement?.name
                        uploadToken.fileSize = fileSize
                        
                        // 4. Add Content (Object : String, Object)
                        let resource = UploadedFileTokenResource()
                        resource.token = "{3:result:id}"
                        
                        
                        let requestBuilder = SystemService.ping()
                            .add(request:MediaService.add(entry: entry))
                            .add(request:UploadTokenService.add(uploadToken: uploadToken))
                            .add(request:MediaService.addContent(entryId: "{2:result:id}", resource: resource))
                            .add(request:UploadTokenService.upload(uploadTokenId: "{3:result:id}", fileData: fileElement!, resume: false))
                            .set(completion: {(response: Array<Any?>?, error: ApiException?) in
                                
                                expect(error).to(beNil())
                                
                                expect(response).notTo(beNil())
                                expect(response?.count) == 5
                                
                                // 0
                                let ping = response?[0] as? Bool
                                expect(ping) == true
                                
                                // 1
                                let createdEntry = response?[1] as? MediaEntry
                                expect(createdEntry?.id).notTo(beNil())
                                expect(createdEntry?.status) == EntryStatus.NO_CONTENT
                                
                                self.entryIds.append((createdEntry?.id)!)
                                
                                // 2
                                let token = response?[2] as? UploadToken
                                expect(token?.id).notTo(beNil())
                                expect(token?.status) == UploadTokenStatus.PENDING
                                
                                // 3
                                let contentEntry = response?[3] as? MediaEntry
                                expect(contentEntry?.status) == EntryStatus.IMPORT
                                
                                // 4
                                let closedToken = response?[4] as? UploadToken
                                expect(closedToken?.status) == UploadTokenStatus.CLOSED
                                
                                done()
                            })
                        self.executor.send(request: requestBuilder.build(self.client!))
                    }
                }
            }
            
            
            it("multi completions") {
                waitUntil(timeout: 500) { done in
                    self.login() { error in
                        expect(error).to(beNil())
                        
                        let entry = MediaEntry()
                        entry.name = "Multirequest completions Test - \(BaseTest.uniqueTag)"
                        entry.mediaType = MediaType.IMAGE
                        entry.referenceId = BaseTest.uniqueTag
                        
                        let uploadToken = UploadToken()
                        uploadToken.fileName = fileElement?.name
                        uploadToken.fileSize = fileSize
                        
                        // 4. Add Content (Object : String, Object)
                        let resource = UploadedFileTokenResource()
                        resource.token = "{3:result:id}"
                        
                        var completions = 0
                        
                        let pingRequestBuilder = SystemService.ping()
                            .set(completion: { (ping: Bool?, error: ApiException?) in
                                expect(error).to(beNil())
                                expect(ping) == true
                                completions += 1
                            })
                        
                        let mediaAddRequestBuilder = MediaService.add(entry: entry)
                            .set(completion: { (createdEntry: MediaEntry?, error: ApiException?) in
                                expect(error).to(beNil())
                                expect(createdEntry?.id).notTo(beNil())
                                expect(createdEntry?.status) == EntryStatus.NO_CONTENT
                                
                                self.entryIds.append((createdEntry?.id)!)
                                
                                completions += 1
                            })
                        
                        let uploadTokenAddRequestBuilder = UploadTokenService.add(uploadToken: uploadToken)
                            .set(completion: { (token: UploadToken?, error: ApiException?) in
                                expect(error).to(beNil())
                                
                                expect(token?.id).notTo(beNil())
                                expect(token?.status) == UploadTokenStatus.PENDING
                                
                                completions += 1
                            })
                        
                        let mediaAddContentRequestBuilder = MediaService.addContent(entryId: "{2:result:id}", resource: resource)
                            .set(completion: { (contentEntry: MediaEntry?, error: ApiException?) in
                                expect(error).to(beNil())
                                
                                expect(contentEntry?.status) == EntryStatus.IMPORT
                                
                                completions += 1
                            })
                        
                        let uploadTokenUploadRequestBuilder = UploadTokenService.upload(uploadTokenId: "{3:result:id}", fileData: fileElement!, resume: false)
                            .set(completion: { (closedToken: UploadToken?, error: ApiException?) in
                                expect(error).to(beNil())
                                
                                expect(closedToken?.status) == UploadTokenStatus.CLOSED
                                
                                completions += 1
                            })
                        
                        let requestBuilder = pingRequestBuilder
                            .add(request:mediaAddRequestBuilder)
                            .add(request:uploadTokenAddRequestBuilder)
                            .add(request:mediaAddContentRequestBuilder)
                            .add(request:uploadTokenUploadRequestBuilder)
                            .set(completion: {(response: Array<Any?>?, error: ApiException?) in
                                
                                expect(error).to(beNil())
                                
                                expect(response).notTo(beNil())
                                expect(response?.count) == completions
                                
                                done()
                            })
                        self.executor.send(request: requestBuilder.build(self.client!))
                    }
                }
            }
        }
    }
}
