
import Quick
import Nimble
import KalturaClient

class MediaServiceTest: BaseTest {
    var entryIds: [String] = []
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        client = Client(config)
        
        describe("upload token") {
            
            beforeEach {
                waitUntil(timeout: 500) { done in
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
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
                
                waitUntil(timeout: 500) { done in
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
                
                waitUntil(timeout: 500) { done in
                    self.createEntry(fileElement: fileElement!, fileSize: fileSize!) { createdEntry in
                        done()
                    }
                }
            }
            
            
            it("update") {
                
                let name: String = UUID().uuidString
                
                waitUntil(timeout: 500) { done in
                    self.createMediaEntry() { entry, error in
                        expect(error).to(beNil())
                        
                        expect(entry).notTo(beNil())
                        expect(entry?.id).notTo(beNil())
                        
                        self.entryIds.append((entry?.id)!)
                        
                        let updateEntry: MediaEntry = MediaEntry()
                        updateEntry.name = name
                        
                        let mediaUpdateRequestBuilder:RequestBuilder<MediaEntry> = MediaService.update(entryId: (entry?.id)!, mediaEntry: updateEntry)
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
                
                waitUntil(timeout: 500) { done in
                    self.createMediaEntry() { entry, error in
                        expect(error).to(beNil())
                        
                        expect(entry).notTo(beNil())
                        expect(entry?.id).notTo(beNil())
                        
                        self.entryIds.append((entry?.id)!)
                        
                        let mediaGetRequestBuilder:RequestBuilder<MediaEntry> = MediaService.get(entryId: (entry?.id)!)
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
                
                waitUntil(timeout: 500) { done in                        let mediaGetRequestBuilder:RequestBuilder<MediaEntry> = MediaService.get(entryId: "bad-id")
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
                
                waitUntil(timeout: 500) { done in
                    self.createMediaEntries(count: count) { createdEntries in
                    
                        let mediaListRequestBuilder:RequestBuilder<ListResponse<MediaEntry>> = MediaService.list(filter: filter)
                        mediaListRequestBuilder.set(completion: {(list: ListResponse<MediaEntry>?, error: ApiException?) in
                            
                            expect(error).to(beNil())
                            
                            expect(list).notTo(beNil())
                            expect(list?.objects?.count) == count
                            expect(list?.totalCount) == count
                            
                            for entry in createdEntries {
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
        
        self.createMediaEntry() { entry, error in
            expect(error).to(beNil())
            
            createdEntry = entry
            expect(createdEntry).notTo(beNil())
            expect(createdEntry?.id).notTo(beNil())
            
            self.entryIds.append((createdEntry?.id)!)
            
            let uploadToken: UploadToken = UploadToken()
            uploadToken.fileName = fileElement.name
            uploadToken.fileSize = fileSize
            
            let uploadTokenAddRequestBuilder:RequestBuilder<UploadToken> = UploadTokenService.add(uploadToken: uploadToken)
            uploadTokenAddRequestBuilder.set(completion: {(uploadToken: UploadToken?, error: ApiException?) in
                
                expect(error).to(beNil())
                
                createdUploadToken = uploadToken
                expect(createdUploadToken).notTo(beNil())
                expect(createdUploadToken?.id).notTo(beNil())
                
                
                let resource: UploadedFileTokenResource = UploadedFileTokenResource()
                resource.token = createdUploadToken?.id
                
                let mediaAddContentRequestBuilder:RequestBuilder<MediaEntry> = MediaService.addContent(entryId: (createdEntry?.id)!, resource: resource)
                mediaAddContentRequestBuilder.set(completion: {(entry: MediaEntry?, error: ApiException?) in
                    
                    expect(error).to(beNil())
                    
                    let uploadTokenUploadRequestBuilder:RequestBuilder<UploadToken> = UploadTokenService.upload(uploadTokenId: (createdUploadToken?.id)!, fileData: fileElement)
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
