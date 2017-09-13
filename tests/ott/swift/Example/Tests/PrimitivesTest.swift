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
    var username = "gordon2015001450620646744@mailinator.com"
    var password = "password"
    var userid = "1080046"
    var householdId = 0
    
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
                
                it("needs to return Void value", closure: {
                    waitUntil(timeout: 500) { done in
                        self.updatePassword(completed: { (result:Void?, error:ApiException?) in
                            expect(error).to(beNil())
                            expect(result).to(beNil())
                            done()
                        })
                    }
                })
            }
        }
    }
    
    private func login(done: @escaping (_ error: ApiException?) -> Void) {
        
        let requestBuilder = OttUserService.login(partnerId: self.partnerId, username: self.username, password: self.password).set { (response:LoginResponse?, error: ApiException?) in
            self.client?.ks = response?.loginSession?.ks
            done(error)
            
        }
        requestBuilder.setBody(key: "udid", value: "72958A68-3823-4C67-8A19-ADA920599301")
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
    
    
    private func updatePassword(completed: @escaping (_ result: Void?, _ error: ApiException?) -> Void) {
        
        let getAssetFiles = OttUserService.updatePassword(userId: Int(self.userid)!, password: "123456").set { (result:Void?, error:ApiException?) in
            completed(result, error)
        }
        
        executor.send(request: getAssetFiles.build(client!))
    }
    
    private func getAssetManifest(completed: @escaping (_ result: Void?, _ error: ApiException?) -> Void) {
        
        let getAssetFiles = AssetService.get(id: self.assetId, assetReferenceType: .MEDIA)
        
        getAssetFiles.set { (asset:Asset?, error:ApiException?) in
            let playManifest = AssetFileService.playManifest(partnerId: self.partnerId, assetId: self.assetId, assetType: .MEDIA, assetFileId: Int64((asset?.mediaFiles?.last?.id)!), contextType: .PLAYBACK, ks: self.client?.ks).set(completion: { (result:Void?, error:ApiException?) in
                completed(result, error)
            })
        
            self.executor.send(request: playManifest.build(self.client!))
        }
        
        
        executor.send(request: getAssetFiles.build(client!))
    }
}
