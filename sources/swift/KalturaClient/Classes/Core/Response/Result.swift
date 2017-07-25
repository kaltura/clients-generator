//
//  Result.swift
//  Pods
//
//  Created by Admin on 08/11/2016.
//
//

import UIKit
import SwiftyJSON


public class Result<T>: NSObject {
    
    public var data: JSON? = nil
    public var error: ApiException? = nil
    
    public init(data:JSON?, error:ApiException?) {
        self.data = data
        self.error = error
    }
    
    public convenience init(data: JSON) {
        self.init(data: data, error: nil)
    }

    public convenience init(error: ApiException) {
        self.init(data: nil, error: error)
    }
}
