// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platfroms allow them to do with
// text.
//
// Copyright (C) 2006-2017  Kaltura Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// @ignore
// ===================================================================================================

/**
 * This class was generated using exec.php
 * against an XML schema provided by Kaltura.
 *
 * MANUAL CHANGES TO THIS CLASS WILL BE OVERWRITTEN.
 */

import Quick
import Nimble
import KalturaClient

class ExceptionsTest: BaseTest {
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        if let endPoint = URL(string: serverUrl) {
            config.endPoint = endPoint
        }
        client = Client(config)
        
        describe("list response") {
            it("asset get") {
                waitUntil(timeout: .seconds(15)) { done in
                    let requestBuilder = FlavorAssetService.list()
                    requestBuilder.set(completion: {(list: FlavorAssetListResponse?, error: ApiException?) in
                        
                        expect(list).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "PROPERTY_VALIDATION_CANNOT_BE_NULL"
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
        
        describe("object response") {
            it("media get") {
                waitUntil(timeout: .seconds(15)) { done in
                    let requestBuilder = MediaService.get(entryId: "bad-id")
                    requestBuilder.set(completion: {(getEntry: MediaEntry?, error: ApiException?) in
                        
                        expect(getEntry).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "ENTRY_ID_NOT_FOUND"
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
        
        describe("empty response") {
            it("media delete") {
                waitUntil(timeout: .seconds(15)) { done in
                    let requestBuilder = MediaService.delete(entryId: "bad-id")
                    requestBuilder.set(completion: {(void: Void?, error: ApiException?) in
                        
                        expect(void).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "INVALID_KS"
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }

        
        describe("array response") {
            it("flavorAsset getByEntryId") {
                waitUntil(timeout: .seconds(15)) { done in
                    let requestBuilder = FlavorAssetService.getByEntryId(entryId: "bad-id")
                    requestBuilder.set(completion: {(assets: [FlavorAsset]?, error: ApiException?) in
                        
                        expect(assets).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "ENTRY_ID_NOT_FOUND"
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
        
        describe("primitive response") {
            
            it("login") {
                waitUntil(timeout: .seconds(15)) { done in
                    let requestBuilder = SessionService.start(secret: "bad-secret", userId: nil, type: SessionType.ADMIN, partnerId: self.partnerId)
                    
                    requestBuilder.set(completion: {(ks: String?, error: ApiException?) in
                        
                        expect(ks).to(beNil())
                        
                        expect(error).notTo(beNil())
                        expect(error?.code).notTo(beNil())
                        expect(error?.code) == "START_SESSION_ERROR"
                        
                        done()
                    })
                    
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
    }
}
