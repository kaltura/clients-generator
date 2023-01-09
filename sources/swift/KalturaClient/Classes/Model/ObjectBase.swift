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

open class ObjectBase {
    
    public class ObjectBaseTokenizer: BaseTokenizedObject {
        public var relatedObjects: DictionaryTokenizedObject<ListResponse.ListResponseTokenizer> {
            get {
                return DictionaryTokenizedObject<ListResponse.ListResponseTokenizer>(self.append("relatedObjects"))
            }
        }
    }
    
    public var dict: [String: Any] = [:]
    
    public var relatedObjects: Dictionary<String, ListResponse>?
    
    public required init() {
    }
    
    internal func populate(_ dict: [String: Any]) throws {
        if let objects = dict["relatedObjects"] as? [String: Any] {
            relatedObjects = try JSONParser.parse(map: objects)
        }
    }
    
    internal func toDictionary() -> [String: Any] {
        dict["objectType"] = "Kaltura\(serverObjectType)"
        return dict
    }
    
    open var serverObjectType: String {
        return "\(type(of: self))"
    }
}

extension Dictionary {
    public func toDictionary() -> [String: [String: Any]] {
        var results: [String: [String: Any]] = [:]
        for key in self.keys {
            if let stringKey = key as? String, let value = self[key] as? ObjectBase {
                results.updateValue(value.toDictionary(), forKey: stringKey)
            }
        }
        return results
    }
}

