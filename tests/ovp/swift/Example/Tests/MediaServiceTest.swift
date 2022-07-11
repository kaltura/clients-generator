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

class MediaServiceTest: BaseTest {
    var entryIds: [String] = []
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        if let endPoint = URL(string: serverUrl) {
            config.endPoint = endPoint
        }
        client = Client(config)
        
        describe("upload token") {
            
            beforeEach {
                waitUntil(timeout: .seconds(15)) { done in
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
            afterEach {
                for entryId in self.entryIds {
                    waitUntil(timeout: .seconds(15)) { done in
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

            it("upload request file") {
                
                do {
                    let fileURL = testBundle.url(forResource: fileName, withExtension: fileType)
                    let fileData = try Data(contentsOf: fileURL!);
                    fileElement = RequestFileElement(data: fileData, name: "\(fileName).\(fileType)", mimeType: "video/x-flv")
                    
                    let attr = try FileManager.default.attributesOfItem(atPath: filePath!)
                    fileSize = attr[FileAttributeKey.size] as? Double
                } catch {
                    fail("\(error)")
                    return
                }
                
                waitUntil(timeout: .seconds(15)) { done in
                    self.createEntry(fileElement: fileElement!, fileSize: fileSize!) { createdEntry in
                        done()
                    }
                }
            }
            
            
            it("upload url") {
                
                do {
                    let fileURL = testBundle.url(forResource: fileName, withExtension: fileType)
                    fileElement = try RequestFileElement(url: fileURL!)
                    
                    let attr = try FileManager.default.attributesOfItem(atPath: filePath!)
                    fileSize = attr[FileAttributeKey.size] as? Double
                } catch {
                    fail("\(error)")
                    return
                }
                
                waitUntil(timeout: .seconds(15)) { done in
                    self.createEntry(fileElement: fileElement!, fileSize: fileSize!) { createdEntry in
                        done()
                    }
                }
            }
            
            
            it("update") {
                
                let name: String = UUID().uuidString
                
                waitUntil(timeout: .seconds(15)) { done in
                    self.createMediaEntry(prefix: "Media Update Test") { entry, error in
                        expect(error).to(beNil())
                        
                        expect(entry).notTo(beNil())
                        expect(entry?.id).notTo(beNil())
                        
                        self.entryIds.append((entry?.id)!)
                        
                        let updateEntry: MediaEntry = MediaEntry()
                        updateEntry.name = name
                        
                        let mediaUpdateRequestBuilder = MediaService.update(entryId: (entry?.id)!, mediaEntry: updateEntry)
                        mediaUpdateRequestBuilder.set(completion: {(updatedEntry: MediaEntry?, error: ApiException?) in
                            
                            expect(error).to(beNil())
                            
                            expect(updatedEntry).notTo(beNil())
                            expect(updatedEntry?.id).notTo(beNil())
                            expect(updatedEntry?.name) == name
                            
                            done()
                        })
                        self.executor.send(request: mediaUpdateRequestBuilder.build(self.client!))
                    }
                }
            }
            
            
            it("get") {
                
                waitUntil(timeout: .seconds(15)) { done in
                    self.createMediaEntry(prefix: "Media Get Test") { entry, error in
                        expect(error).to(beNil())
                        
                        expect(entry).notTo(beNil())
                        expect(entry?.id).notTo(beNil())
                        
                        self.entryIds.append((entry?.id)!)
                        
                        let mediaGetRequestBuilder = MediaService.get(entryId: (entry?.id)!)
                        mediaGetRequestBuilder.set(completion: {(getEntry: MediaEntry?, error: ApiException?) in
                            
                            expect(error).to(beNil())
                            
                            expect(getEntry).notTo(beNil())
                            expect(getEntry?.id).notTo(beNil())
                            expect(getEntry?.id) == entry?.id
                            
                            done()
                        })
                        self.executor.send(request: mediaGetRequestBuilder.build(self.client!))
                    }
                }
            }
            
            
            it("bad get") {
                
                waitUntil(timeout: .seconds(15)) { done in
                    let mediaGetRequestBuilder = MediaService.get(entryId: "bad-id")
                    mediaGetRequestBuilder.set(completion: {(getEntry: MediaEntry?, error: ApiException?) in
                        
                        expect(getEntry).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "ENTRY_ID_NOT_FOUND"
                        
                        done()
                    })
                    self.executor.send(request: mediaGetRequestBuilder.build(self.client!))
                }
            }
            
            
            it("list") {
                
                let count: Int = 3
                let filter = MediaEntryFilter()
                filter.tagsMultiLikeOr = BaseTest.uniqueTag
                filter.statusIn = EntryStatus.NO_CONTENT.rawValue
                
                waitUntil(timeout: .seconds(15)) { done in
                    self.createMediaEntries(prefix: "Media List Test", count: count) { createdEntries in
                    
                        let mediaListRequestBuilder = MediaService.list(filter: filter)
                        mediaListRequestBuilder.set(completion: {(list: MediaListResponse?, error: ApiException?) in
                            
                            expect(error).to(beNil())
                            
                            expect(list).notTo(beNil())
                            expect(list?.objects?.count) == count
                            expect(list?.totalCount) == count
                            
                            for entry in createdEntries {
                                self.entryIds.append(entry.id!)
                                
                                if let _ = (list?.objects)!.index(where: { $0.id == entry.id }) {
                                    // OK
                                }
                                else {
                                    fail("Created entry not found in returned list")
                                }
                            }
                            
                            done()
                        })
                        self.executor.send(request: mediaListRequestBuilder.build(self.client!))
                    }
                }
            }
        }
    }
    
    private func createEntry(fileElement: RequestFile, fileSize: Double, _ created: @escaping (_ createdEntry: MediaEntry) -> Void) {
        
        var createdEntry: MediaEntry?
        var createdUploadToken: UploadToken?
        
        self.createMediaEntry(prefix: "Media Upload Test") { entry, error in
            expect(error).to(beNil())
            
            createdEntry = entry
            expect(createdEntry).notTo(beNil())
            expect(createdEntry?.id).notTo(beNil())
            
            self.entryIds.append((createdEntry?.id)!)
            
            let uploadToken: UploadToken = UploadToken()
            uploadToken.fileName = fileElement.name
            uploadToken.fileSize = fileSize
            
            let uploadTokenAddRequestBuilder = UploadTokenService.add(uploadToken: uploadToken)
            uploadTokenAddRequestBuilder.set(completion: {(uploadToken: UploadToken?, error: ApiException?) in
                
                expect(error).to(beNil())
                
                createdUploadToken = uploadToken
                expect(createdUploadToken).notTo(beNil())
                expect(createdUploadToken?.id).notTo(beNil())
                
                
                let resource: UploadedFileTokenResource = UploadedFileTokenResource()
                resource.token = createdUploadToken?.id
                
                let mediaAddContentRequestBuilder = MediaService.addContent(entryId: (createdEntry?.id)!, resource: resource)
                mediaAddContentRequestBuilder.set(completion: {(entry: MediaEntry?, error: ApiException?) in
                    
                    expect(error).to(beNil())
                    
                    let uploadTokenUploadRequestBuilder = UploadTokenService.upload(uploadTokenId: (createdUploadToken?.id)!, fileData: fileElement)
                    uploadTokenUploadRequestBuilder.set(completion: {(uploadToken: UploadToken?, error: ApiException?) in
                        
                        expect(error).to(beNil())
                        
                        expect(uploadToken).notTo(beNil())
                        expect(uploadToken?.status) == UploadTokenStatus.CLOSED
                        
                        created(createdEntry!)
                    })
                    self.executor.send(request: uploadTokenUploadRequestBuilder.build(self.client!))
                })
                self.executor.send(request: mediaAddContentRequestBuilder.build(self.client!))
            })
            self.executor.send(request: uploadTokenAddRequestBuilder.build(self.client!))
        }
    }
}
