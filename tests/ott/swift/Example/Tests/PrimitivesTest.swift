//
//  PrimitivesTest.swift
//  KalturaClient
//
//  Created by Rivka Peleg on 02/08/2017.
//  Copyright © 2017 CocoaPods. All rights reserved.
//

import Quick
import Nimble
import KalturaClient

class PrimitivesTest: QuickSpec {
    var client: Client?
    var secret: String = "ed0b955841a5ec218611c4869256aaa4"
    var partnerId: Int = 198
    var domainURL = "http://api-preprod.ott.kaltura.com/v4_5"
    var assetId = "485241"
    
    private var executor: RequestExecutor = USRExecutor.shared
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        config.endPoint = URL(string:domainURL)!
        
        describe("check Primitives response") {
            
            beforeEach {
                waitUntil(timeout: 500) { done in
                    self.client = Client(config)
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
            context("when user in anonymouse mode") {
                it("needs to return bool value") {
                    waitUntil(timeout: 500) { done in
                        
                        self.ping(completed: { (result:Bool?, exc:ApiException?) in
                            expect(exc).to(beNil())
                            expect(result).notTo(beNil())
                            done()
                        })
                        
                    }
                }
                
                it("needs to return string value", closure: {
                    
                    waitUntil(timeout: 500) { done in
                        
                        self.ping(completed: { (result:Bool?, exc:ApiException?) in
                            expect(exc).to(beNil())
                            expect(result).notTo(beNil())
                            done()
                        })
                        
                    }
                })
                
                
                it("needs to return Int value", closure: {
                    
                    waitUntil(timeout: 500) { done in
                        
                        self.ping(completed: { (result:Bool?, exc:ApiException?) in
                            expect(exc).to(beNil())
                            expect(result).notTo(beNil())
                            done()
                        })
                        
                    }
                })

                
                
                
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
    
    private func getTime(completed: @escaping (_ result: Int64?, _ error: ApiException?) -> Void) {
        
        let requestBuilder = SystemService.getTime().set { (result:Int64?, error: ApiException?) in
             completed(result, error)
        }

        executor.send(request: requestBuilder.build(client!))
    }
    
    private func ping(completed: @escaping (_ result: Bool?, _ error: ApiException?) -> Void) {
        
        let requestBuilder = SystemService.ping().set { (result:Bool?, error: ApiException?) in
            completed(result, error)
        }
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    private func getVersion(completed: @escaping (_ result: String?, _ error: ApiException?) -> Void) {
        
        let requestBuilder = SystemService.getVersion().set { (result: String?, error:ApiException?) in
            completed(result, error)
        }
        executor.send(request: requestBuilder.build(client!))
    }
}
