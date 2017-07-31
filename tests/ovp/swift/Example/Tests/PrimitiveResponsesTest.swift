
import Quick
import Nimble
import KalturaClient

class PrimitiveResponsesTest: BaseTest {
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        client = Client(config)
        
        describe("primitive responses") {
            
            beforeEach {
                waitUntil(timeout: 500) { done in
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
            it("int") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<Int> = SystemService.getTime()
                    requestBuilder.set(completion: {(time: Int?, error: ApiException?) in
                        
                        expect(error).to(beNil())
                        
                        expect(time).notTo(beNil())
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
            
            it("string") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<String> = SystemService.getVersion()
                    requestBuilder.set(completion: {(version: String?, error: ApiException?) in
                        
                        expect(error).to(beNil())
                        
                        expect(version).notTo(beNil())
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
            
            it("boolean") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<Bool> = SystemService.ping()
                    requestBuilder.set(completion: {(ok: Bool?, error: ApiException?) in
                        
                        expect(error).to(beNil())
                        
                        expect(ok).notTo(beNil())
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
    }
}
