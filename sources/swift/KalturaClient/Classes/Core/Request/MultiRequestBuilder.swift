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
    
    
    var requests = [RequestBuilderProtocol]()
    
    @discardableResult
    public func add(request: RequestBuilderProtocol) -> Self  {
        self.requests.append(request)
        return self
    }
    
    public override func getUrlTail() -> String {
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
    
    public override func onComplete(_ response: Response) -> Void {

        // calling on complete of each request
        let allResponse = response.data?["result"].array
        var allParsedResponse = [Any]()
        for (index, request) in self.requests.enumerated() {
            let singelResponse = allResponse?[index]
            let response = Response(data: singelResponse, error: response.error)
            request.onComplete(response)
            let parsed = request.parse(response)
            allParsedResponse.append(parsed.data ?? parsed.exception ?? ApiException())
        }
        
        if let block = completion {
            block(allParsedResponse,response.error)
        }
        
        return
    }
    
    public override func buildParamsAsData(params: [String: Any]) -> Data?
    {
        return self.params.sortedJsonData()
    }
}



extension Dictionary where Key == String {
    
    func sortedJsonData() -> Data? {
        
        var result = ""
        let prefix = "{"
        let suffix = "}"
        
        
        result.append(prefix)
        let sortedKeys = self.keys.sorted()
        for key in sortedKeys {
            
            let jsonObject = self[key]!
            var jsonObjectString: String? = nil
            if ( JSONSerialization.isValidJSONObject(jsonObject)){
                do {
                    let jsonData = try JSONSerialization.data(withJSONObject: jsonObject, options: JSONSerialization.WritingOptions.prettyPrinted)
                    jsonObjectString = String(data: jsonData, encoding: String.Encoding.utf8)
                }catch{
                    
                }
            }else if let object = jsonObject as? String {
                jsonObjectString = "\"\(object)\""
            }else if let object = jsonObject as? Int {
                jsonObjectString = String(object)
            }
            
            
            if let value = jsonObjectString  {
                result.append("\"\(key)\":")
                result.append(value)
                result.append(",")
            }
        }
        
        result = String(result.characters.dropLast())
        result.append(suffix)
        
        let data = result.data(using: String.Encoding.utf8)
        return data
    }
}




