//
//  MultiRequestWithArray.swift
//  KalturaClient
//
//  Created by Rivka Peleg on 07/08/2017.
//  Copyright © 2017 CocoaPods. All rights reserved.
//




import Foundation
import Quick
import Nimble
import KalturaClient


class MultiRequestWithArray: QuickSpec {
    var client: Client?
    
    private var executor: RequestExecutor = USRExecutor.shared
    
    override func spec() {
        
        describe("Add action and then delete action with logged user") {
            
            beforeEach {
                waitUntil(timeout: 500) { done in
                    self.client = TConfig.client
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
            context("User is logged in") {
                it("needs to add social action and then delete it") {
                    waitUntil(timeout: 500) { done in
                        
                        self.addAndDeleteSocialActions(completed: { (status:[NetworkActionStatus]?, error:ApiException?) in
                            expect(status?.count).to(beGreaterThan(0))
                            expect(status?.last).notTo(beNil())
                            done()
                            
                            
                        })
                        
                    }
                }
                
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
        executor.send(request: requestBuilder.build(client!))
    }
    
    
    private func addAndDeleteSocialActions(completed: @escaping (_ status:[NetworkActionStatus]?, _ error: ApiException?) -> Void) {
        
        let socialAction = SocialAction()
        socialAction.actionType = SocialActionType.LIKE
        socialAction.assetId = Int64(TConfig.assetId)
        socialAction.assetType = AssetType.MEDIA
        
        let addSocialActionRB = SocialActionService.add(socialAction: socialAction)
        let deleteSocialActionRB  = SocialActionService.delete(id: "")
        deleteSocialActionRB.set { (result:Array<NetworkActionStatus>?, error:ApiException?) in
            completed(result, error)
        }
        
        let mrb = addSocialActionRB
            .add(request: deleteSocialActionRB)
            .link(tokenFromRespose: addSocialActionRB.responseTokenizer.socialAction().id,
                  tokenToRequest: deleteSocialActionRB.requestTokenizer.id)
        
        executor.send(request: mrb.build(client!))
    }
    
    
}
