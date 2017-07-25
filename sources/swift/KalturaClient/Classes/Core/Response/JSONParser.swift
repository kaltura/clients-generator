
import SwiftyJSON

public class JSONParser{
    
    enum error: Error {
        case typeNotFound
        case invalidJsonObject
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
        print("Init object \(T.self)")
        let obj: T = T.self.init()
        try obj.populate(object)
        return obj
    }
    
    internal static func parse(data: Data) throws -> JSON {
        do{
            let json = try JSONSerialization.jsonObject(with: data, options: JSONSerialization.ReadingOptions.allowFragments)
            return JSON(json)
        }
        catch {
            throw ApiClientException(message: "Failed to deserialize JSON", code: ApiClientException.ErrorCode.invalidJson)
        }
    }
    
    internal static func parse<T>(json: JSON) throws -> T? {
        
        if let dict = json.dictionaryObject, dict["objectType"] as? String == "KalturaAPIException" {
            throw try parse(object: dict) as ApiException
        }

        if let _ = T.self as? String.Type {
            return json.string as? T
        }
        
        if let _ = T.self as? ObjectBase.Type {
            if let dict = json.dictionaryObject {
                print("Init object aaa \(T.self)")

                //let ret: MediaEntry? = try parse(object: dict)
                return try parse(object: dict) as? T
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
