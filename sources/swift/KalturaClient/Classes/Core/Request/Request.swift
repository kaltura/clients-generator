//
//  Request.swift
//  Pods
//
//  Created by Admin on 10/11/2016.
//
//

import MobileCoreServices

public typealias CompletionClosures =  (_ response: Response) -> Void

public enum RequestMethod {
    case get
    case post
    
    /// The `RequestMethod` value, for example for get we need "GET" etc.
    var value: String {
        switch self {
        case .get: return "GET"
        case .post: return "POST"
        }
    }
}

public protocol RequestFile {
    var data: Data { get }
    var name: String { get }
    var mimeType: String { get }
}

public struct RequestFileElement: RequestFile {
    public var data: Data
    public var name: String
    public var mimeType: String
    
    public init?(url: URL) throws {
        data = try Data(contentsOf: url);
        name = url.lastPathComponent
        mimeType = RequestFileElement.mimeTypeForPath(path: url.path)
    }

    public init(data: Data, name: String, mimeType: String) {
        self.data = data
        self.name = name
        self.mimeType = mimeType
    }
    
    private static func mimeTypeForPath(path: String) -> String {
        let url = NSURL(fileURLWithPath: path)
        let pathExtension = url.pathExtension
        
        if let uti = UTTypeCreatePreferredIdentifierForTag(kUTTagClassFilenameExtension, pathExtension! as NSString, nil)?.takeRetainedValue() {
            if let mimetype = UTTypeCopyPreferredTagWithClass(uti, kUTTagClassMIMEType)?.takeRetainedValue() {
                return mimetype as String
            }
        }
        return "application/octet-stream"
    }
}

public protocol Request {
    
    var requestId: String { get }
    var method: RequestMethod? { get }
    var url: URL { get }
    var dataBody: Data? { get }
    var files: [String: RequestFile] { get }
    var headers: [String:String]? { get }
    var timeout: Double { get }
    var completion: CompletionClosures { get }
    var configuration: ConnectionConfiguration { get }
}

public struct RequestElement: Request {
    
    public var requestId: String
    public var method: RequestMethod?
    public var url: URL
    public var dataBody: Data?
    public var files: [String: RequestFile]
    public var headers: [String:String]?
    public var timeout: Double
    public var completion: CompletionClosures
    public var configuration: ConnectionConfiguration
}






