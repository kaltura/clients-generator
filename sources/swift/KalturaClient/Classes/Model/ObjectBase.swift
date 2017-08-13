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

public class ObjectBaseTokenizer: BaseTokenizedObject {
    
    public var relatedObjects: DictionaryTokenizedObject<ListResponseTokenizer> {
        get {
            return DictionaryTokenizedObject<ListResponseTokenizer>(self.append("relatedObjects"))
        }
    }
    
}

open class ObjectBase {
    
    public var relatedObjects: Dictionary<String, ListResponse>?
    
    public required init() {
    }
    
    internal func populate(_ dict: [String: Any]) throws {
        if dict["relatedObjects"] != nil {
            relatedObjects = try JSONParser.parse(map: dict["relatedObjects"] as! [String: Any])
        }
    }
    
    public func toDictionary() -> [String: Any] {
        let dict: [String: Any] = ["objectType": "Kaltura\(type(of: self))"]
        return dict
    }
}

