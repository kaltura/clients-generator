//
//  RestMultiRequestBuilder.swift
//  Pods
//
//  Created by Admin on 13/11/2016.
//
//

import UIKit
import SwiftyJSON

public class MultiRequestBuilder: ArrayRequestBuilder<Any> {
    
    var requests: [RequestBuilder<Any>] = [RequestBuilder<Any>]()
    
    @discardableResult
    public func add<T>(request:RequestBuilder<T>) -> Self {
        self.requests.append(request as Any as! RequestBuilder<Any>)
        return self
    }
    
    internal override func getUrlTail() -> String {
        return "/service/multirequest"
    }

    
    override public func build(_ client: Client) -> Request {
        
        for (index, request)  in self.requests.enumerated() {
            request.setBody(key: "action", value: request.action)
            request.setBody(key: "service", value: request.service)
            setBody(key: String(index+1), value: request.params)
        }
        
        return super.build(client)
    }
    
//    func kalturaMultiRequestData() -> Data? {
//        
//        
//        let prefix = "{"
//        let suffix = "}"
//        var data = prefix.data(using: String.Encoding.utf8)
//        
//        for  index in 1...self.requests.count {
//            let requestBody = self.jsonBody?[String(index)].rawString(String.Encoding.utf8, options: JSONSerialization.WritingOptions())?.trimmingCharacters(in: CharacterSet.whitespacesAndNewlines)
//            let requestBodyData = requestBody?.data(using: String.Encoding.utf8)
//            data?.append("\"\(index)\":".data(using: String.Encoding.utf8)!)
//            data?.append(requestBodyData!)
//            data?.append(",".data(using: String.Encoding.utf8)!)
//            _ = self.jsonBody?.dictionaryObject?.removeValue(forKey: String(index))
//        }
//        
//        if let jsonBody = self.jsonBody{
//            let remainingJsonAsString: String? = jsonBody.rawString(String.Encoding.utf8, options: JSONSerialization.WritingOptions())
//            if let jsonString = remainingJsonAsString{
//                var jsonWithoutLastChar = String(jsonString.characters.dropLast())
//                
//                jsonWithoutLastChar = String(jsonWithoutLastChar.characters.dropFirst())
//                data?.append((jsonWithoutLastChar.data(using: String.Encoding.utf8))!)
//            }
//        }
//        
//        data?.append(suffix.data(using: String.Encoding.utf8)!)
//        
//        return data
//    }
}


