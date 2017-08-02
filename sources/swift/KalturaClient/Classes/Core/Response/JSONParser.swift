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

import SwiftyJSON

internal class JSONParser{
    
    private static var kalturaRegex:NSRegularExpression?
    
    private static func getKalturaRegex() -> NSRegularExpression? {
        if kalturaRegex == nil {
            do{
                kalturaRegex = try NSRegularExpression(pattern: "^Kaltura")
            }
            catch {
            }
        }
        return kalturaRegex
    }
    
    private static func getObjectType(_ objectType: String) -> ObjectBase.Type? {
        if let regex = getKalturaRegex() {
            let className = regex.stringByReplacingMatches(in: objectType, options: NSRegularExpression.MatchingOptions(), range: NSMakeRange(0, 10), withTemplate: "")
            
            let fullClassName = "KalturaClient.\(className)"
            let classType = NSClassFromString(fullClassName) as? ObjectBase.Type
            return classType
        }
        
        return nil
    }
    
    public static func parse<T>(array: [Any]) throws -> [T] where T: ObjectBase {
        var ret: [T] = []
        for item in array {
            ret.append(try parse(object: item as! [String: Any]))
        }
        return ret
    }
    
    public static func parse<T>(map: [String: Any]) throws -> [String: T] where T: ObjectBase {
        var ret: [String: T] = [:]
        
        for (key, item) in map {
            ret[key] = try parse(object: item as! [String: Any])
        }
        return ret
    }
    
    public static func parse<T>(object: [String: Any]) throws -> T where T: ObjectBase {
        return try parse(object: object, type: T.self)
    }
    
    public static func parse<T>(object: [String: Any], type: ObjectBase.Type) throws -> T where T: ObjectBase {
        
        var classType: ObjectBase.Type = type
        if let objectType = object["objectType"] as? String {
            classType = getObjectType(objectType) ?? type
        }
        else {
            if let result = object["result"] as? [String: Any] {
                return try self.parse(object: result, type: type)
            }
            else if let error = object["error"] as? [String: Any] {
                throw try parse(object: error) as ApiException
            }
        }

        
        let obj: ObjectBase = classType.init()
        try obj.populate(object)
        
        return obj as! T
    }
    
    public static func parse(data: Data) throws -> JSON {
        do{
            let json = try JSONSerialization.jsonObject(with: data, options: JSONSerialization.ReadingOptions.allowFragments)
            return JSON(json)
        }
        catch {
            throw ApiClientException(message: "Failed to deserialize JSON", code: ApiClientException.ErrorCode.invalidJson)
        }
    }
    
    public static func parse<T>(json: JSON) throws -> T? {
        
        if let dict = json.dictionaryObject, dict["objectType"] as? String == "KalturaAPIException" {
            throw try parse(object: dict) as ApiException
        }
        
        if let _ = T.self as? String.Type {
            return json.string as? T
        }
        
        if let _ = T.self as? Int.Type {
            return json.int as? T
        }
        
        if let _ = T.self as? Int64.Type {
            return json.int64 as? T
        }
        
        if let _ = T.self as? Bool.Type {
            return json.bool as? T
        }
        
        if let _ = T.self as? Double.Type {
            return json.double as? T
        }
        
        if let _ = T.self as? Void.Type {
            return nil
        }
        
        if let type: ObjectBase.Type = T.self as? ObjectBase.Type {
            if let dict = json.dictionaryObject {
                return try parse(object: dict, type: type) as? T
            }
            else {
                throw ApiClientException(message: "JSON is not of object", code: ApiClientException.ErrorCode.invalidJsonObject)
            }
        }
        else {
            throw ApiClientException(message: "Type \(T.self) is not ObjectBase", code: ApiClientException.ErrorCode.typeNotFound)
        }
    }
}
