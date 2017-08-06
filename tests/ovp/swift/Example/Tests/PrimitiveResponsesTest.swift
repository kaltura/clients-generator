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

class PrimitiveResponsesTest: BaseTest {
    
    override func spec() {
        let config: ConnectionConfiguration = ConnectionConfiguration()
        client = Client(config)
        
        describe("primitive responses") {
            
            beforeEach {
                waitUntil(timeout: 500) { done in
                    self.login() { error in
                        expect(error).to(beNil())
                        done()
                    }
                }
            }
            
            it("int") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<Int> = SystemService.getTime()
                    requestBuilder.set(completion: {(time: Int?, error: ApiException?) in
                        
                        expect(error).to(beNil())
                        
                        expect(time).notTo(beNil())
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
            
            it("string") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<String> = SystemService.getVersion()
                    requestBuilder.set(completion: {(version: String?, error: ApiException?) in
                        
                        expect(error).to(beNil())
                        
                        expect(version).notTo(beNil())
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
            
            it("boolean") {
                waitUntil(timeout: 500) { done in
                    let requestBuilder:RequestBuilder<Bool> = SystemService.ping()
                    requestBuilder.set(completion: {(ok: Bool?, error: ApiException?) in
                        
                        expect(error).to(beNil())
                        
                        expect(ok).notTo(beNil())
                        
                        done()
                    })
                    self.executor.send(request: requestBuilder.build(self.client!))
                }
            }
        }
    }
}
