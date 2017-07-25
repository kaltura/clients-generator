
import Quick
import Nimble
import KalturaClient

class BaseTest: QuickSpec {
    var client: Client?
    var secret: String = "ed0b955841a5ec218611c4869256aaa4"
    var partnerId: Int = 1676801
    
    private var executor: RequestExecutor = USRExecutor.shared
    
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
            
            context("entry") {
                var createdEntry: MediaEntry?
                
                it("create") {
                    waitUntil(timeout: 500) { done in
                        self.createMediaEntry() { entry, error in
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
                                    /*
            it("can do maths") {
                expect(1) == 2
            }

            it("can read") {
                expect("number") == "string"
            }

            it("will eventually fail") {
                expect("time").toEventually( equal("done") )
            }
            
            context("these will pass") {

                it("can do maths") {
                    expect(23) == 23
                }

                it("can read") {
                    expect("🐮") == "🐮"
                }

                it("will eventually pass") {
                    var time = "passing"

                    DispatchQueue.main.async {
                        time = "done"
                    }

                    waitUntil { done in
                        Thread.sleep(forTimeInterval: 0.5)
                        expect(time) == "done"

                        done()
                    }
                }
            }
             */
        }
    }
    
    private func login(done: @escaping (_ error: ApiException?) -> Void) {
        
        let requestBuilder:RequestBuilder<String> = SessionService.start(secret: self.secret, userId: nil, type: SessionType.ADMIN, partnerId: self.partnerId)
        
        requestBuilder.set(completion: {(ks: String?, error: ApiException?) in
            
            self.client!.ks = ks
            done(error)
        })
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    private func createMediaEntry(created: @escaping (_ createdEntry: MediaEntry?, _ error: ApiException?) -> Void) {
        let entry: MediaEntry = MediaEntry()
        entry.mediaType = MediaType.VIDEO
        
        let requestBuilder:RequestBuilder<MediaEntry> = MediaService.add(entry: entry)
        requestBuilder.set(completion: {(createdEntry: MediaEntry?, error: ApiException?) in
            
            created(createdEntry, error)
        })
        
        executor.send(request: requestBuilder.build(client!))
    }
}
