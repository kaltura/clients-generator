
import SwiftyJSON

internal class JSONParser{
    
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
        
        if let result = object["result"] as? [String: Any] {
            return try self.parse(object: result, type: type)
        }
        else if let error = object["error"] as? [String: Any] {
            throw try parse(object: error) as ApiException
        }
        
        let obj: ObjectBase = type.init()
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
