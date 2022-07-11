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

public class MultiRequestBuilder: ArrayRequestBuilder<Any?, BaseTokenizedObject, BaseTokenizedObject> {
    
    var requests = [RequestBuilderProtocol]()
    
    @discardableResult
    public override func add(request: RequestBuilderProtocol) -> Self  {
        var subRequest = request
        subRequest.index = requests.count
        self.requests.append(request)
        return self
    }
    
    public override func getUrlTail() -> String {
        return "/service/multirequest"
    }

    
    override public func build(_ client: Client) -> Request {
        
        for (index, request)  in self.requests.enumerated() {
            let indexKey = String(index+1)
            request.setParam(key: "action", value: request.action)
            request.setParam(key: "service", value: request.service)
            setParam(key: indexKey, value: request.params)
            for (key, requestFile) in request.files {
                setFile(key: "\(indexKey):\(key)", value: requestFile)
            }
        }
        
        return super.build(client)
    }
    
    public override func onComplete(_ response: Result<Any>) -> Void {

        // calling on complete of each request
        var allResponse: [Any] = []
        if let dict = response.data as? [String: Any], let responses = dict["result"] as? [Any] {
            allResponse = responses
        }
        else if let responses = response.data as? [Any]{
            allResponse = responses
        }
        
        guard response.data != nil && response.error == nil && allResponse.count == self.requests.count else {
            if let block = completion {
                block(nil, response.error)
            }
            
            return;
        }
        
        var allParsedResponse = [Any?]()
        for (index, request) in self.requests.enumerated() {
            let singelResponse = allResponse[index]
            let response = Result<Any>(data: singelResponse, error: response.error)
            let parsed = request.parse(response)
            request.complete(data: parsed.data, exception: parsed.exception)
            allParsedResponse.append(parsed.exception ?? parsed.data ?? nil)
        }
        
        if let block = completion {
            block(allParsedResponse, response.error)
        }
        
        return
    }
    
    internal override func buildParamsAsData(params: [String: Any]) -> Data?
    {
        return self.params.sortedJsonData()
    }
    
    private func link(params: [String: Any], keys: [String], token: String) -> [String: Any] {
        var result = params
        var keysArray = keys
        let key = keysArray.removeFirst()
        if keysArray.count > 0 {
            if params[key] is [String : Any] {
                result[key] = link(params: params[key] as! [String : Any], keys: keysArray, token: token)
            }
            else {
                result[key] = link(params: [String : Any](), keys: keysArray, token: token)
            }
        }
        else {
            result[key] = token
        }
        return result
    }
    
    //[user,childrebs,1,name]
    public func link(tokenFromRespose: BaseTokenizedObject, tokenToRequest: BaseTokenizedObject) -> Self{
        
        var request = self.requests[tokenToRequest.requestId]
        let params = request.params
        request.params = link(params: params, keys: tokenToRequest.tokens, token: tokenFromRespose.result)
        
        return self
    }
}



extension Dictionary where Key == String {
    
    func sortedJsonData() -> Data? {
        
        if isEmpty {
            return "{}".data(using: .utf8)
        }
        
        var result = "{"
        
        let sortedKeys = self.keys.sorted { $0.localizedStandardCompare($1) == .orderedAscending }
        
        for key in sortedKeys {
            
            let jsonObject = self[key]!
            var jsonObjectString: String? = nil
            if (JSONSerialization.isValidJSONObject(jsonObject)) {
                do {
                    let jsonData = try JSONSerialization.data(withJSONObject: jsonObject)
                    jsonObjectString = String(data: jsonData, encoding: String.Encoding.utf8)
                } catch {
                    
                }
            } else if let object = jsonObject as? String {
                jsonObjectString = "\"\(object)\""
            } else if let object = jsonObject as? Int {
                jsonObjectString = String(object)
            }
            
            if let value = jsonObjectString  {
                result.append("\"\(key)\":")
                result.append(value)
                result.append(",")
            }
        }
        
        // Remove the trailing comma
        result.removeLast()
        result.append("}")
        
        return result.data(using: String.Encoding.utf8)
    }
}




