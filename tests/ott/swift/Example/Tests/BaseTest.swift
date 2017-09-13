
import Quick
import Nimble
import KalturaOttClient

class BaseTest: QuickSpec {
    var client: Client?
    
    private var executor: RequestExecutor = USRExecutor.shared
    
    override func spec() {
        
        describe("Load asset") {
            
            beforeEach {
                waitUntil(timeout: 500) { done in
                    self.client = TConfig.client
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
            context("when user in anonymouse mode") {
                var createdEntry: Asset?
                
                it("needs to get an asset") {
                    waitUntil(timeout: 500) { done in
                        self.createAsset() { entry, error in
                            expect(error).to(beNil())
                            
                            createdEntry = entry
                            expect(createdEntry).notTo(beNil())
                            expect(createdEntry?.id).notTo(beNil())
                            
                            expect(createdEntry?.images?.last?.id).notTo(beNil())
                            if createdEntry != nil {
                                print(createdEntry!)
                            }
                            done()
                        }
                    }
                }

            }
        }
    }
    
    private func login(done: @escaping (_ error: ApiException?) -> Void) {
        
        let requestBuilder = OttUserService.anonymousLogin(partnerId: TConfig.partnerId).set { (loginSession:LoginSession?, exception:ApiException?) in
            self.client!.ks = loginSession?.ks
            done(exception)
        }
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    private func createAsset(created: @escaping (_ createdEntry: Asset?, _ error: ApiException?) -> Void) {
        
 
        let requestBuilder = AssetService.get(id: TConfig.assetId, assetReferenceType: AssetReferenceType.MEDIA).set { (asset, exception) in
            created(asset, exception)
        }
        
    
        executor.send(request: requestBuilder.build(client!))
    }
}
