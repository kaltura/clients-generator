//
//  Request.swift
//  Pods
//
//  Created by Admin on 10/11/2016.
//
//

import UIKit

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

public protocol Request {
    
    var requestId: String { get }
    var method: RequestMethod? { get }
    var url: URL { get }
    var dataBody: Data? { get }
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
    public var headers: [String:String]?
    public var timeout: Double
    public var completion: CompletionClosures
    public var configuration: ConnectionConfiguration
}






