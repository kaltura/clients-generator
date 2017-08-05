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

class BaseTest: QuickSpec {
    var client: Client?
    var secret: String = "@ADMIN_SECRET@"
    var partnerId: Int = @PARTNER_ID@
    static var uniqueTag: String = uniqueString()
    
    public var executor: RequestExecutor = USRExecutor.shared
    
    static func uniqueString() -> String {
        let uuid = UUID().uuidString
        return uuid.substring(to: uuid.index(uuid.startIndex, offsetBy: 8))
    }
    
    func login(done: @escaping (_ error: ApiException?) -> Void) {
        
        let requestBuilder:RequestBuilder<String> = SessionService.start(secret: self.secret, userId: nil, type: SessionType.ADMIN, partnerId: self.partnerId)
        
        requestBuilder.set(completion: {(ks: String?, error: ApiException?) in
            
            self.client!.ks = ks
            done(error)
        })
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    func deleteEntry(entryId: String, done: @escaping (_ error: ApiException?) -> Void) {
        
        let requestBuilder:RequestBuilder<Void> = MediaService.delete(entryId: entryId)
        
        requestBuilder.set(completion: {(void: Void?, error: ApiException?) in
            done(error)
        })
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    func createMediaEntry(created: @escaping (_ createdEntry: MediaEntry?, _ error: ApiException?) -> Void) {
        let entry: MediaEntry = MediaEntry()
        entry.mediaType = MediaType.VIDEO
        entry.tags = BaseTest.uniqueTag
        
        let requestBuilder:RequestBuilder<MediaEntry> = MediaService.add(entry: entry)
        requestBuilder.set(completion: {(createdEntry: MediaEntry?, error: ApiException?) in
            
            created(createdEntry, error)
        })
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    func createMediaEntries(count: Int, created: @escaping (_ createdEntries: [MediaEntry]) -> Void) {
        var entries: [MediaEntry] = []
        for _ in 1...count {
            self.createMediaEntry() { entry, error in
                entries.append(entry!)
                if entries.count == count {
                    created(entries)
                }
            }
        }
    }
}
