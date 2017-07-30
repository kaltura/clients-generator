
import Quick
import Nimble
import KalturaClient

class BaseTest: QuickSpec {
    var client: Client?
    var secret: String = "ed0b955841a5ec218611c4869256aaa4"
    var partnerId: Int = 198
    var domainURL = "http://api-preprod.ott.kaltura.com/v4_5"
    var assetId = "485241"
    
    private var executor: RequestExecutor = USRExecutor.shared
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        config.endPoint = URL(string:domainURL)!
        
        
        
        describe("upload token") {
            beforeEach {
                waitUntil(timeout: 500) { done in
                    self.client = Client(config)
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
            context("get asset") {
                var createdEntry: Asset?
                
                it("create") {
                    waitUntil(timeout: 500) { done in
                        self.createAsset() { entry, error in
                            expect(error).to(beNil())
                            
                            createdEntry = entry
                            expect(createdEntry).notTo(beNil())
                            expect(createdEntry?.id).notTo(beNil())
                            
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
        
        let requestBuilder:RequestBuilder<LoginSession> = OttUserService.anonymousLogin(partnerId: self.partnerId).set { (loginSession:LoginSession?, exception:ApiException?) in
            self.client!.ks = loginSession?.ks
            done(exception)
        }
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    private func createAsset(created: @escaping (_ createdEntry: Asset?, _ error: ApiException?) -> Void) {
        
 
        let requestBuilder:RequestBuilder<Asset> = AssetService.get(id: assetId, assetReferenceType: AssetReferenceType.MEDIA).set { (asset, exception) in
            created(asset, exception)
        }
        
    
        executor.send(request: requestBuilder.build(client!))
    }
}
