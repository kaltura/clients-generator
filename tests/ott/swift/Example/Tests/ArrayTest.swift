//
//  ArrayTest.swift
//  KalturaClient
//
//  Created by Rivka Peleg on 03/08/2017.
//  Copyright © 2017 CocoaPods. All rights reserved.
//

import Foundation
import Quick
import Nimble
import KalturaClient


class arrayTest: QuickSpec {
    var client: Client?
    var secret: String = "ed0b955841a5ec218611c4869256aaa4"
    var partnerId: Int = 198
    var userName = "srivkas@gmail.com"
    var password = "123456"
    var domainURL = "http://api-preprod.ott.kaltura.com/v4_5"
    var assetId = "485241"
    
    
    private var executor: RequestExecutor = USRExecutor.shared
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        config.endPoint = URL(string:domainURL)!
        
        describe("Add action and then delete action with logged user") {
            
            beforeEach {
                waitUntil(timeout: 500) { done in
                    self.client = Client(config)
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
            context("User is logged in") {
                it("needs to add social action and then delete it") {
                    waitUntil(timeout: 500) { done in
                        
                        self.addSocialActions(completed: { (socialActionid, error) in
                            expect(error).to(beNil())
                            expect(socialActionid).notTo(beNil())
                            
                            if let id = socialActionid {
                                self.deleteSocialActions(socialActionId: id, deleted: { (status:[NetworkActionStatus]?, error:ApiException?) in
                                    expect(status?.count).to(beGreaterThan(0))
                                    expect(status?.last).notTo(beNil())
                                    done()
                                })
                            }
                        })
                        
                    }
                }
                
            }
        }
    }
    
    private func login(done: @escaping (_ error: ApiException?) -> Void) {
        
        let requestBuilder = OttUserService.login(partnerId: self.partnerId, username: self.userName, password: self.password).set { (response:LoginResponse?, error: ApiException?) in
            self.client?.ks = response?.loginSession?.ks
            done(error)
        }
        executor.send(request: requestBuilder.build(client!))
    }
    
    
    private func addSocialActions(completed: @escaping (_ socialActionsId: String?, _ error: ApiException?) -> Void) {
        
        let socialAction = SocialAction()
        socialAction.actionType = SocialActionType.LIKE
        socialAction.assetId = Int64(self.assetId)
        socialAction.assetType = AssetType.MEDIA

        let requestBuilder = SocialActionService.add(socialAction: socialAction).set { (response:UserSocialActionResponse?, error:ApiException?) in
            completed(response?.socialAction?.id, error)
        }
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    private func deleteSocialActions(socialActionId: String, deleted: @escaping (_ actionStatus: [NetworkActionStatus]? , _ error: ApiException?) -> Void) {
        
        let requestBuilder = SocialActionService.delete(id: socialActionId).set { (response:Array<NetworkActionStatus>?, error:ApiException?) in
            deleted(response, error)
        }
        
        executor.send(request: requestBuilder.build(client!))
    }
}

