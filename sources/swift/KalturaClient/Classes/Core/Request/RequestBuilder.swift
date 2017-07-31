//
//  RestRequestBuilder.swift
//  Pods
//
//  Created by Admin on 13/11/2016.
//
//

import UIKit
		
public class RequestBuilder<T: Any>: RequestBuilderData {
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
        var bodyData: Data? = nil
        do{
            bodyData = try JSONSerialization.data(withJSONObject: params)
        } catch {
        }
        
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
    
    internal func getUrlTail() -> String {
        return "/service/" + service! + "/action/" + action!
    }
    
    internal func onComplete(_ response: Response) -> Void {
        guard completion != nil else {
            return
        }
        
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
        
        completion!(result, exception)
    }
}
