//
//  MapTest.swift
//  KalturaClient
//
//  Created by Jonathan Kanarek on 26/09/2017.
//  Copyright © 2017 CocoaPods. All rights reserved.
//

import Foundation
import Quick
import Nimble
import KalturaOttClient


class mapTest: QuickSpec {
    var client: Client = TConfig.client!
    
    private var executor: RequestExecutor = USRExecutor.shared
    
    override func spec() {
        
        let password = "password"
        
        context("User") {
            it("create") {
                waitUntil(timeout: 500) { done in
                    
                    let value1 = StringValue()
                    value1.value = "aaa"
                    
                    let value2 = StringValue()
                    value2.value = "bbb"
                    
                    let ottUserToCreate = OTTUser()
                    ottUserToCreate.username = self.randomString(length: 20)
                    ottUserToCreate.dynamicData = ["aaa": value1, "bbb": value2]
                    
                    let requestBuilder = OttUserService.register(partnerId: TConfig.partnerId, user: ottUserToCreate, password: password)
                        .set {(response:OTTUser?, error: ApiException?) in
                            expect(error).to(beNil())
                            done()
                    }
                    self.executor.send(request: requestBuilder.build(self.client))
                }
            }
        }
    }
    
    private func randomString(length: Int) -> String {
        let allowedChars = "abcdefghijklmnopqrstuvwxyz0123456789"
        let allowedCharsCount = UInt32(allowedChars.characters.count)
        var randomString = ""
        
        for _ in 0..<length {
            let randomNum = Int(arc4random_uniform(allowedCharsCount))
            let randomIndex = allowedChars.index(allowedChars.startIndex, offsetBy: randomNum)
            let newCharacter = allowedChars[randomIndex]
            randomString += String(newCharacter)
        }
        
        return randomString
    }
}

