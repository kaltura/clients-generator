//
//  MultiRequestWithPrimitives.swift
//  KalturaClient
//
//  Created by Rivka Peleg on 07/08/2017.
//  Copyright © 2017 CocoaPods. All rights reserved.
//


import Quick
import Nimble
import KalturaOttClient

class MultiRequestWithPrimitives: QuickSpec {
    var client: Client?
    var assetId = "485241"
    var userid = "1080046"
    var householdId = 0
    
    private var executor: RequestExecutor = USRExecutor.shared
    
    override func spec() {
    
        describe("check Primitives response") {
            
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
                
                it("needs to return Void value", closure: {
                    waitUntil(timeout: 500) { done in
                        self.sendMultiRequest(getTimeCompleted: { (result:Int64?, error:ApiException?) in
                            expect(error).to(beNil())
                            expect(result).notTo(beNil())
                        }, pingCompleted: { (result:Bool?, error) in
                            expect(error).to(beNil())
                            expect(result).notTo(beNil())
                        }, getVersionCompleted: { (result:String?, error:ApiException?) in
                            expect(error).to(beNil())
                            expect(result).notTo(beNil())
                        }, updatePasswordCompleted: { (result:Void?, error:ApiException?) in
                            expect(error).to(beNil())
                            expect(result).to(beNil())
                        }, whenAllCompleted: { (result:[Any?]?, error:ApiException?) in
                            expect(error).to(beNil())
                            expect(result).notTo(beNil())
                            expect(result?.count).to(beGreaterThan(0))
                            expect(result?.count).to(beLessThan(5))
                            done()
                        })
                    
                    }
                })
            }
        }
    }
    
    private func login(done: @escaping (_ error: ApiException?) -> Void) {
        
        let requestBuilder = OttUserService.login(partnerId: TConfig.partnerId,
                                                  username: TConfig.username,
                                                  password: TConfig.password)
            .set { (response:LoginResponse?, error: ApiException?) in
                    self.client?.ks = response?.loginSession?.ks
            done(error)
            
        }
        requestBuilder.setParam(key: "udid", value: "72958A68-3823-4C67-8A19-ADA920599301")
        executor.send(request: requestBuilder.build(client!))
    }
    
    private func sendMultiRequest(getTimeCompleted: @escaping (_ result: Int64?, _ error: ApiException?) -> Void,
                                                                 pingCompleted: @escaping (_ result: Bool?, _ error: ApiException?) -> Void,
                                                                 getVersionCompleted: @escaping (_ result: String?, _ error: ApiException?) -> Void,
                                                                 updatePasswordCompleted: @escaping (_ result: Void?, _ error: ApiException?) -> Void,
                                                                 whenAllCompleted: @escaping (_ result: [Any?]?, _ error: ApiException?) -> Void ) {
        
        let getTimeRequestBuilder = SystemService.getTime().set { (result:Int64?, error: ApiException?) in
            getTimeCompleted(result, error)
        }
        
        let pingRequestBuilder = SystemService.ping().set { (result:Bool?, error: ApiException?) in
            pingCompleted(result, error)
        }
        
        let getVersionRequestBuilder = SystemService.getVersion().set { (result: String?, error:ApiException?) in
            getVersionCompleted(result, error)
        }
        
//        let updatePasswordRequestBuilder = OttUserService.updatePassword(userId: Int(self.userid)!, password: "123456").set { (result:Void?, error:ApiException?) in
//            updatePasswordCompleted(result, error)
//        }
        
        let mrb = getTimeRequestBuilder.add(request: pingRequestBuilder).add(request: getVersionRequestBuilder)
            //.add(request: updatePasswordRequestBuilder)
        mrb.set { (result:[Any?]?, error:ApiException?) in
            whenAllCompleted(result, error)
        }
        executor.send(request: mrb.build(client!))
    }
    
}

