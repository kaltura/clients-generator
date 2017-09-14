//
//  Request.swift
//  Pods
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

import MobileCoreServices

public typealias CompletionClosures =  (_ response: Result<Any>) -> Void

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






