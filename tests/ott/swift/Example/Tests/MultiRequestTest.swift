//
//  MultiRequestTest.swift
//  KalturaClient
//
//  Created by Rivka Peleg on 31/07/2017.
//  Copyright © 2017 CocoaPods. All rights reserved.
//

import Foundation


import Quick
import Nimble
import KalturaOttClient

class MultiRequestTest: QuickSpec {
    var client: Client?

    private var executor: RequestExecutor = USRExecutor.shared
    
    override func spec() {
        
        self.client = TConfig.client
        
        describe("Load asset in multi request") {
            
                it("needs to get an asset") {
                    waitUntil(timeout: 500) { done in
                        self.loginAndCreateAsset() { asset, error in
                            expect(error).to(beNil())
                            let resultAsset: Asset? = asset
                            expect(resultAsset).notTo(beNil())
                            expect(resultAsset?.id).notTo(beNil())
                            
                            if resultAsset != nil {
                                print(asset!)
                            }
                            done()
                        }
                    }
                }
                
            }
        }
    
    
    private func loginAndCreateAsset(created: @escaping (_ asset: Asset?, _ error: ApiException?) -> Void) {
        
        let requestBuilderLogin = OttUserService.anonymousLogin(partnerId: TConfig.partnerId)
        let requestBuilderGetAsset = AssetService.get(id: TConfig.assetId, assetReferenceType: AssetReferenceType.MEDIA)
        
        requestBuilderGetAsset.set { (asset, exception) in
            created(asset, exception)
        }
        
         let multiRequest: MultiRequestBuilder = requestBuilderLogin.add(request: requestBuilderGetAsset)
            .link(tokenFromRespose: requestBuilderLogin.responseTokenizer.ks, tokenToRequest: requestBuilderGetAsset.requestTokenizer.ks)
        
        executor.send(request: multiRequest.build(self.client!))
    }
}
