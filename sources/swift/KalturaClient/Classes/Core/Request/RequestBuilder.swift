//
//  RestRequestBuilder.swift
//  Pods
//
//  Created by Admin on 13/11/2016.
//
//

import UIKit


public protocol RequestBuilderProtocol {
        
    var params: [String: Any] { get set }
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
    
}

public class RequestBuilder<T: Any>: RequestBuilderData, RequestBuilderProtocol {
    
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
        
        return RequestElement(requestId: self.requestId, method:self.method , url: url, dataBody: bodyData, headers: self.headers, timeout: self.timeout, completion: self.onComplete, configuration: client.configuration)
    }
    
    public func getUrlTail() -> String {
        return "/service/" + service! + "/action/" + action!
    }
    
    public func onComplete(_ response: Response) -> Void {
        
        let parsedResult = self.parse(response)
        
        if let block = completion {
            block(parsedResult.data as? T, parsedResult.exception)
        }
    }
    
    
    public func parse(_ response: Response) -> (data:Any?,exception: ApiException?)  {
        
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
