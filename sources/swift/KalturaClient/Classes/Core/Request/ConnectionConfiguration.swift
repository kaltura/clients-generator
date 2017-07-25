//
//  RequestConfiguration.swift
//  Pods
//
//  Created by Admin on 10/11/2016.
//
//

import UIKit


var defaultTimeOut = 3.0
var defaultRetryCount = 3

@objc public class ConnectionConfiguration: NSObject {
    
    @objc public var endPoint: URL = URL(string: "https://www.kaltura.com")!
    @objc public var readTimeOut: Double = defaultTimeOut
    @objc public var writeTimeOut: Double = defaultTimeOut
    @objc public var connectTimeOut: Double = defaultTimeOut
    @objc public var retryCount: Int = defaultRetryCount
    @objc public var ignoreLocalCache: Bool = false
}
