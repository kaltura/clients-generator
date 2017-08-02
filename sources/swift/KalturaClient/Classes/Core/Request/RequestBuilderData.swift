//
//  File.swift
//  Pods
//
//  Created by Jonathan Kanarek on 19/07/2017.
//
//

@objc public class RequestBuilderData: NSObject {
    public var params: [String: Any] = [:]
    
    @discardableResult
    public func setBody(key: String, value:Any?) -> Self {
        
        guard value != nil else {
            return self
        }
        
        var val: Any;
        if value is ObjectBase {
            val = (value as! ObjectBase).toDictionary()
        }
        else {
            val = value!
        }
        self.params[key] = val
        return self
    }
}
