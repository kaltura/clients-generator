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
import KalturaClient

class MultiRequestTest: QuickSpec {
    var client: Client?
    var partnerId: Int = 198
    var domainURL = "http://api-preprod.ott.kaltura.com/v4_5"
    var assetId = "485241"
    
    private var executor: RequestExecutor = USRExecutor.shared
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        config.endPoint = URL(string:domainURL)!
        self.client = Client(config)
        
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
        
        let requestBuilderLogin: RequestBuilder<LoginSession> = OttUserService.anonymousLogin(partnerId: self.partnerId)
        let requestBuilderGetAsset: RequestBuilder<Asset> = AssetService.get(id: assetId, assetReferenceType: AssetReferenceType.MEDIA)
        requestBuilderGetAsset.setBody(key: "ks", value: "{1:result:ks}")
        
        requestBuilderGetAsset.set { (asset, exception) in
            created(asset, exception)
        }
        
        let multiRequest: MultiRequestBuilder = MultiRequestBuilder()
        multiRequest.add(request: requestBuilderLogin).add(request: requestBuilderGetAsset)
        executor.send(request: multiRequest.build(self.client!))
    }
}
