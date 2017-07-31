
import Quick
import Nimble
import KalturaClient

class PluginsTest: BaseTest {
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        client = Client(config)
        
        describe("metadata") {
            
            beforeEach {
                waitUntil(timeout: 500) { done in
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
            it("add profile") {
                let metadataProfile = MetadataProfile()
                metadataProfile.metadataObjectType = MetadataObjectType.ENTRY
                metadataProfile.name = "Test - \(BaseTest.uniqueTag)"
                
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<MetadataProfile> = MetadataProfileService.add(metadataProfile: metadataProfile, xsdData: "<xml></xml>")
                    requestBuilder.set(completion: {(createdMetadataProfile: MetadataProfile?, error: ApiException?) in
                        
                        expect(error).to(beNil())
                        
                        expect(createdMetadataProfile).notTo(beNil())
                        expect(createdMetadataProfile?.id).notTo(beNil())
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
    }
}
