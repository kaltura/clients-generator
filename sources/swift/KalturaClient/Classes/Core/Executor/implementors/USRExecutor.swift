//
//  URLSessionRequestExecutor.swift
//  Pods
//
//  Created by Admin on 10/11/2016.
//
//

import UIKit
import SwiftyJSON


@objc public class USRExecutor: NSObject, RequestExecutor, URLSessionDelegate {
    
    var tasks: [URLSessionDataTask] = [URLSessionDataTask]()
    var taskIdByRequestID: [String: Int] = [String: Int]()
    
    enum ResponseError: Error {
        case emptyOrIncorrectURL
        case incorrectJSONBody
    }
    
    public static let shared = USRExecutor()
    
    public func send(request r: Request){
        
        var request: URLRequest = URLRequest(url: r.url)
        
        let jsonString: String = String(bytes: r.dataBody!, encoding: String.Encoding.utf8)!
        print("Request [\(r.requestId)] url: \(r.url) JSON: \(jsonString)")
        
        //handle http method
        if let method = r.method {
            request.httpMethod = method.value
        }
        
        // handle headers
        if let headers = r.headers{
            for (headerKey,headerValue) in headers{
                request.setValue(headerValue, forHTTPHeaderField: headerKey)
            }
        }
        
        // handle body
        if !r.files.isEmpty {
            let boundary = "Boundary-\(UUID().uuidString)"
            request.setValue("multipart/form-data; boundary=\(boundary)", forHTTPHeaderField: "Content-Type")
            request.httpBody = buildMultipartData(boundary: boundary, files: r.files, jsonString: jsonString)
        }
        else if let data = r.dataBody {
            request.httpBody = data
        }
        
        let session: URLSession!
        
        if r.configuration.ignoreLocalCache {
            let configuration = URLSessionConfiguration.default
            configuration.requestCachePolicy = NSURLRequest.CachePolicy.reloadIgnoringLocalCacheData
            session = URLSession(configuration: configuration)
        } else {
            session = URLSession.shared
        }
        
        var task: URLSessionDataTask? = nil
        // settings headers:
        task = session.dataTask(with: request) { (data, response, error) in
            
            let index = self.taskIndexForRequest(request: r)
            if let i = index {
               self.tasks.remove(at: i)
            }
        
            DispatchQueue.main.async {
                if let error = error as NSError? {
                    if error.code == NSURLErrorCancelled {
                        // cancel3ed
                    } else {
                        let result = Response(data: nil, error: ApiClientException(message: error.localizedDescription, code: ApiClientException.ErrorCode.httpError))
                        r.completion(result)
                        // some other error
                    }
                    return
                }
                
                if let d = data {
                    let jsonString: String = String(bytes: d, encoding: String.Encoding.utf8)!
                    print("Response [\(r.requestId)] JSON: \(jsonString)")

                    do {
                        let json:JSON = try JSONParser.parse(data: d)
                        let result = Response(data: json, error:nil)
                        r.completion(result)
                    } catch let error {
                        let result = Response(data: nil, error: error as? ApiException)
                        r.completion(result)
                        
                    }
                } else {
                    let result = Response(data: nil, error:nil)
                    r.completion(result)
                }
            }
        }
    
        if let tsk = task{
            self.taskIdByRequestID[r.requestId] = task?.taskIdentifier
            self.tasks.append(tsk)
            tsk.resume()
        }
    }
    
    private func buildMultipartData(boundary: String, files: [String: RequestFile], jsonString: String) -> Data {
        let body = NSMutableData()
        
        let boundaryPrefix = "--\(boundary)\r\n"
        
        body.appendString(boundaryPrefix)
        body.appendString("Content-Disposition: form-data; name=\"json\"\r\n\r\n")
        body.appendString("\(jsonString)\r\n")
        
        for (key, file) in files {
            body.appendString(boundaryPrefix)
            body.appendString("Content-Disposition: form-data; name=\"\(key)\"; filename=\"\(file.name)\"\r\n")
            body.appendString("Content-Type: \(file.mimeType)\r\n\r\n")
            body.append(file.data)
            body.appendString("\r\n")
        }
        body.appendString("--".appending(boundary.appending("--")))
        
        return body as Data
    }
    
    public func cancel(request:Request){
        
        let index = self.taskIndexForRequest(request: request)
        if let i = index {
            let task = self.tasks[i]
            task.cancel()
        }
    }
    
    public func taskIndexForRequest(request:Request) -> Int?{
    
        if let taskId = self.taskIdByRequestID[request.requestId]{
            
            let taskIndex = self.tasks.index(where: { (taskInArray:URLSessionDataTask) -> Bool in
                
                if taskInArray.taskIdentifier == taskId {
                    return true
                }else{
                    return false
                }
            })
            
            if let index = taskIndex{
                return index
            } else {
                return nil
            }
 
        } else {
            return nil
        }
    }
    
    public func clean(){
        
    }
    
    // MARK: URLSessionDelegate
    public func urlSession(_ session: URLSession, didBecomeInvalidWithError error: Error?){
        
    }
    
    public func urlSession(_ session: URLSession, didReceive challenge: URLAuthenticationChallenge, completionHandler: @escaping (URLSession.AuthChallengeDisposition, URLCredential?) -> Swift.Void){
        
    }
    
    public func urlSessionDidFinishEvents(forBackgroundURLSession session: URLSession){
        
    }
}

extension NSMutableData {
    func appendString(_ string: String) {
        let data = string.data(using: String.Encoding.utf8, allowLossyConversion: false)
        append(data!)
    }
}
