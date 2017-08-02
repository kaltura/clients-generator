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


