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

internal class JSONParser{
    
    private static var kalturaRegex:NSRegularExpression?
    private static var moduleName:String?
    
    // handling reflection:
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
    
    private static func getModuleName() -> String {
        if moduleName == nil {
            let className = NSStringFromClass(self) as NSString
            moduleName = className.components(separatedBy: ".").first!
        }
        
        return moduleName!
    }
    
    private static func getObjectType(_ objectType: String) -> ObjectBase.Type? {
        if let regex = getKalturaRegex() {
            let className = regex.stringByReplacingMatches(in: objectType, options: NSRegularExpression.MatchingOptions(), range: NSMakeRange(0, 10), withTemplate: "")
            
            let fullClassName = "\(getModuleName()).\(className)"
            let classType = NSClassFromString(fullClassName) as? ObjectBase.Type
            return classType
        }
        
        return nil
    }
    
    //MARK -- parse methods:
    
    // parse array of objects
    internal static func parse<T>(array: [Any]) throws -> [T] where T: ObjectBase {
        var ret: [T] = []
        for item in array {
            ret.append(try parse(object: item as! [String: Any]))
        }
        return ret
    }

    // I think this is useless
    internal static func parse<T>(array: [Any], type: Array<ObjectBase>.Type) throws -> T? {
        var ret = type.init()
        for item in array {
            ret.append(try parse(object: item as! [String: Any]))
        }
        return ret as? T
    }
    
    // parse map of objects
    internal static func parse<T>(map: [String: Any]) throws -> [String: T] where T: ObjectBase {
        var ret: [String: T] = [:]
        
        for (key, item) in map {
            ret[key] = try parse(object: item as! [String: Any])
        }
        return ret
    }
    
    // parse API Exception
    internal static func parseException(object: [String: Any]) -> ApiException {
        let message = object["message"] as? String
        let code = object["code"] as? String
        
        var exceptionArgs: [ApiExceptionArg]?
        
        if let args = object["args"] as? [[String: Any]] {
            do {
                exceptionArgs = try self.parse(array: args) as [ApiExceptionArg]
            } catch {
                return ApiClientException(message: message, code: code)
            }
        }
        return ApiClientException(message: message, code: code, args: exceptionArgs)
    }
    
    // parse dictinoary of object
    internal static func parse<T>(object: [String: Any]) throws -> T where T: ObjectBase {
        return try parse(object: object, type: T.self)
    }
    
    // parse response
    internal static func parse<T>(object: [String: Any], type: ObjectBase.Type) throws -> T where T: ObjectBase {
        
        var classType: ObjectBase.Type = type
        if let objectType = object["objectType"] as? String {
            classType = getObjectType(objectType) ?? type
        }
        else {
            if let result = object["result"] as? [String: Any] {
                return try self.parse(object: result, type: type)
            }
            else if let error = object["error"] as? [String: Any] {
                throw self.parseException(object: error)
            }
        }

        
        let obj: ObjectBase = classType.init()
        try obj.populate(object)
        
        return obj as! T
    }
    
    internal static func parse(data: Data) throws -> Any {
        do{
            let json = try JSONSerialization.jsonObject(with: data, options: JSONSerialization.ReadingOptions.allowFragments)
            return json
        }
        catch {
            throw ApiClientException(message: "Failed to deserialize JSON",
                                     code: ApiClientException.ErrorCode.invalidJson.rawValue)
        }
    }
    
    internal static func parse(primitive: Any, type: Any) throws -> Any? {
        if let str = primitive as? String {
            return str
        }else if type is Int64.Type {
            return primitive as? Int64
        }else if type is Int.Type {
            return primitive as? Int
        }else if let bool = primitive as? Bool {
            return bool
        }else if let double = primitive as? Double {
            return double
        }
        else if primitive is Void {
            return nil
        }
        else if let dict = primitive as? [String: Any]{
            if let result = dict["result"] {
                return try self.parse(primitive: result, type: type)
            }
            else if let error = dict["error"] as? [String: Any] {
                throw self.parseException(object: error)
            }
        }
        
        throw ApiClientException(message: "Type not found",
                                 code: ApiClientException.ErrorCode.typeNotFound.rawValue)
    }
    
    
    
    internal static func parse<T>(array: Any) throws -> [T]? {
        if let dict = array as? [String: Any] {
            if dict["objectType"] as? String == "KalturaAPIException" {
                throw self.parseException(object: dict)
            }
            
            if let result = dict["result"] {
                return try parse(array: result)
            }
            else if let error = dict["error"] {
                return try parse(array: error)
            }else{
                throw ApiClientException(message: "JSON is not valid object",
                                         code: ApiClientException.ErrorCode.invalidJsonObject.rawValue)
            }
        }
        else if let arr = array as? [Any] {
            return try parse(array: arr) as? [T]
        }
        else{
            throw ApiClientException(message: "JSON is not of object",
                                     code: ApiClientException.ErrorCode.invalidJsonObject.rawValue)
        }
        
        
    }
    internal static func parse<T>(json: Any) throws -> T? {
        
        if let dict = json as? [String: Any], dict["objectType"] as? String == "KalturaAPIException" {
            throw self.parseException(object: dict)
        }
        if let type: ObjectBase.Type = T.self as? ObjectBase.Type {
            if let dict = json as? [String: Any] {
                return try parse(object: dict, type: type) as? T
            }
            else {
                throw ApiClientException(message: "JSON is not of object",
                                         code: ApiClientException.ErrorCode.invalidJsonObject.rawValue)
            }
        }
        else if let _ = T.self as? Void.Type {
            if let dict = json as? [String: Any],
                let result = dict["result"] as? [String: Any],
                let error = result["error"] as? [String: Any],
                error["objectType"] as? String == "KalturaAPIException" {
                throw self.parseException(object: dict)
            }

            return nil
        }
        else {
            return try self.parse(primitive: json, type: T.self) as? T
        }
    }
}
