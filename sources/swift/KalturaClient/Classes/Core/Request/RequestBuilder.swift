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


public protocol RequestBuilderProtocol {
        
    var params: [String: Any] { get set }
    var files: [String: RequestFile] { get set }
    var requestId: String { get set }
    var method: RequestMethod? { get set }
    var headers: [String:String]? { get set }
    var timeout: Double { get set }
    var urlParams: [String: String]? { get set }
    var service: String? { get set }
    var action: String? { get set }
    
    
    @discardableResult
    func set(method: RequestMethod?) -> Self
    
    @discardableResult
    func set(params:[String: Any]) -> Self
    
    @discardableResult
    func set(headers: [String: String]?) -> Self
    
    @discardableResult
    func add(headerKey:String, headerValue:String) -> Self
    
    @discardableResult
    func setBody(key: String, value:Any?) -> Self
    
    @discardableResult
    func setParam(key: String, value:String) -> Self
    
    func build(_ client: Client) -> Request
    
    func getUrlTail() -> String
    
    func onComplete(_ response: Response) -> Void
    
    func parse(_ response: Response) -> (data:Any?,exception: ApiException?)
    
    func complete(data:Any?, exception: ApiException?)
    
}

public class RequestBuilder<T: Any>: RequestBuilderData, RequestBuilderProtocol {
    public var files: [String: RequestFile] = [:]
    
    public lazy var requestId: String = {
        return UUID().uuidString
    }()
    
    public var method: RequestMethod? = nil
    public var headers: [String:String]? = nil
    public var timeout: Double = 3
    public var completion: ((_ response: T?, _ error: ApiException?) -> Void)?
    public var urlParams: [String: String]? = nil

    public var service: String?
    public var action: String?
    
    public required override init() {
        super.init()
        
        add(headerKey: "Content-Type", headerValue: "application/json")
        add(headerKey: "Accept", headerValue: "application/json")
        set(method: .post)
    }
    
    public convenience init(service: String, action: String) {
        self.init()
        
        self.service = service
        self.action = action
    }
    
    @discardableResult
    public func set(method: RequestMethod?) -> Self{
        self.method = method
        return self
    }
    
    @discardableResult
    public func set(params:[String: Any]) -> Self{
        self.params = params
        return self
    }
    
    @discardableResult
    public func set(headers: [String: String]?) -> Self{
        self.headers = headers
        return self
    }
    
    @discardableResult
    public func set(completion: @escaping (_ response: T?, _ error: ApiException?) -> Void) -> Self{
        self.completion = completion
        return self
    }
    
    @discardableResult
    public func add(headerKey:String, headerValue:String) -> Self {
        
        if (self.headers == nil){
            self.headers = [String:String]()
        }
        
        self.headers![headerKey]  = headerValue
        return self
    }
    
    public func add(request: RequestBuilderProtocol) -> MultiRequestBuilder {
        
        let multiRequestBuilder = MultiRequestBuilder()
        multiRequestBuilder.add(request: self)
        multiRequestBuilder.add(request: request)
        
        return multiRequestBuilder
    }
    
    @discardableResult
    public func setFile(key: String, value:RequestFile?) -> Self {
        
        guard value != nil else {
            return self
        }
        
        self.files[key] = value
        return self
    }
    
    @discardableResult
    public func setParam(key: String, value:String) -> Self {
        
        if self.urlParams != nil {
            self.urlParams![key] = value
        } else {
            self.urlParams = [key:value]
        }
        return self
    }
    
    public func build(_ client: Client) -> Request {
        
        params["format"] = 1 // JSON
        client.applyParams(self)
        let bodyData = self.buildParamsAsData(params: self.params)
        var url: URL = client.configuration.endPoint
        let urlComponents = NSURLComponents()
        urlComponents.host = url.host
        urlComponents.scheme = url.scheme
        urlComponents.path = url.path + "/api_v3" + self.getUrlTail()
        
        if let params = self.urlParams, params.count > 0 {
                        var queryItems = [URLQueryItem]()
            for (key, value) in params {
                queryItems.append(URLQueryItem(name: key, value: value))
            }
            
            urlComponents.queryItems = queryItems
        }
        url = urlComponents.url!
        
        return RequestElement(requestId: self.requestId, method:self.method , url: url, dataBody: bodyData, files: files, headers: self.headers, timeout: self.timeout, completion: self.onComplete, configuration: client.configuration)
    }
    
    public func getUrlTail() -> String {
        return "/service/" + service! + "/action/" + action!
    }
    
    public func onComplete(_ response: Response) -> Void {
        
        let parsedResult = self.parse(response)
        
        complete(data: parsedResult.data, exception: parsedResult.exception)
    }
    
    public func complete(data:Any?, exception: ApiException?)  {
        if let block = completion {
            block(data as? T, exception)
        }
    }
    
    public func parse(_ response: Response) -> (data:Any?, exception: ApiException?)  {
        
        var result: T? = nil
        var exception: ApiException? = nil
        
        if response.error == nil {
            do{
                result = try JSONParser.parse(json: response.data!)
            }
            catch let error {
                exception = error as? ApiException
            }
        }
        else {
            exception = response.error
        }
        
        
        return (result, exception)
    }
    
    public func buildParamsAsData(params: [String:Any]) -> Data? {
    
        var bodyData: Data? = nil
        do{
            bodyData = try JSONSerialization.data(withJSONObject: params)
        } catch {
            
        }
        
        return bodyData
    }
    
    
    
}
