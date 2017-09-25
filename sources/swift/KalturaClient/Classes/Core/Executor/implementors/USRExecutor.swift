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
        logger.debug("Request [\(r.requestId)] url: \(r.url) JSON: \(jsonString)")
        
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

            self.remove(id: r.requestId)
        
            DispatchQueue.main.async {
                if let error = error as NSError? {
                    if error.code == NSURLErrorCancelled {
                        // cancel3ed
                        logger.debug("request has been canceled")
                    } else {
                        let result = Result<Any>(data: nil, error: ApiClientException(message: error.localizedDescription, code: ApiClientException.ErrorCode.httpError))
                        r.completion(result)
                        // some other error
                    }
                    return
                }
                
                if let d = data {
                    let jsonString: String = String(bytes: d, encoding: String.Encoding.utf8)!
                    
                    
                    var logMessage = "Response [\(r.requestId)] \nJSON: \n\(jsonString) \n"
                    if let httpUrlResponse = response as? HTTPURLResponse {
                        logMessage.append("Headers: \n\(httpUrlResponse.allHeaderFields) \n")
                    }
                    logger.debug(logMessage)

                    do {
                        let json = try JSONParser.parse(data: d)
                        let result = Result<Any>(data: json, error:nil)
                        r.completion(result)
                    } catch let error {
                        let result = Result<Any>(data: nil, error: error as? ApiException)
                        r.completion(result)
                        
                    }
                } else {
                    let result = Result<Any>(data: nil, error:nil)
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
    
    
    public func cancel(id requestId: String) {
        let taskID = self.taskIdByRequestID[requestId]
        let taskIndex = self.tasks.index { (
            task) -> Bool in
            return task.taskIdentifier == taskID
        }
        
        if let index = taskIndex {
            let task = self.tasks[index]
            task.cancel()
        }
    }
    
    public func remove(id requestId: String) {
        let taskID = self.taskIdByRequestID[requestId]
        let taskIndex = self.tasks.index { (
            task) -> Bool in
            return task.taskIdentifier == taskID
        }
        
        if let index = taskIndex {
            self.taskIdByRequestID.removeValue(forKey: requestId)
            self.tasks.remove(at: index)
        }
    }
    
    public func cancel(request:Request){
        self.cancel(id: request.requestId)
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
