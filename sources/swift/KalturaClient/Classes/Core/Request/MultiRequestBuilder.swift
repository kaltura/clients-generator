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



public class MultiRequestBuilder: ArrayRequestBuilder<Any> {
    
    
    var requests = [RequestBuilderProtocol]()
    
    @discardableResult
    public override func add(request: RequestBuilderProtocol) -> Self  {
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




