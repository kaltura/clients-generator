
import Quick
import Nimble
import KalturaClient

class ExceptionsTest: BaseTest {
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        client = Client(config)
        
        describe("list response") {
            it("asset get") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<ListResponse<FlavorAsset>> = FlavorAssetService.list()
                    requestBuilder.set(completion: {(list: ListResponse<FlavorAsset>?, error: ApiException?) in
                        
                        expect(list).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "PROPERTY_VALIDATION_CANNOT_BE_NULL"
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
        
        describe("object response") {
            it("media get") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<MediaEntry> = MediaService.get(entryId: "bad-id")
                    requestBuilder.set(completion: {(getEntry: MediaEntry?, error: ApiException?) in
                        
                        expect(getEntry).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "ENTRY_ID_NOT_FOUND"
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
        
        describe("empty response") {
            it("media delete") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<Void> = MediaService.delete(entryId: "bad-id")
                    requestBuilder.set(completion: {(void: Void?, error: ApiException?) in
                        
                        expect(void).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "INVALID_KS"
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }

        
        describe("array response") {
            it("flavorAsset getByEntryId") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<[FlavorAsset]> = FlavorAssetService.getByEntryId(entryId: "bad-id")
                    requestBuilder.set(completion: {(assets: [FlavorAsset]?, error: ApiException?) in
                        
                        expect(assets).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "ENTRY_ID_NOT_FOUND"
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
        
        describe("primitive response") {
            
            it("login") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<String> = SessionService.start(secret: "bad-secret", userId: nil, type: SessionType.ADMIN, partnerId: self.partnerId)
                    
                    requestBuilder.set(completion: {(ks: String?, error: ApiException?) in
                        
                        expect(ks).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "START_SESSION_ERROR"
                        
                        done()
                    })
                    
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
    }
}
