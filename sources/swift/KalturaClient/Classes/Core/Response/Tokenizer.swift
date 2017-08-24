//
//  Tokenizer.swift
//  Pods
//
//  Created by Rivka Peleg on 10/08/2017.
//
//

import UIKit

public class BaseTokenizedObject {
    
    internal var requestId: Int = 0
    internal var tokens = [String]()
    
    public required init(requestId: Int) {
        self.requestId = requestId
    }
    
    public convenience required init(_ object:BaseTokenizedObject) {
        self.init(requestId: object.requestId)
        self.tokens.append(contentsOf: object.tokens)
    }
    
    public var result: String { get {
        var token =  "{\(requestId + 1):result"
        if tokens.count > 0 {
            token.append(":" + tokens.joined(separator: ":"))
        }
        token.append("}")
        return token
        }
    }
    
    @discardableResult
    internal func append(_ string: String) -> BaseTokenizedObject {
        let tokenizedObject = BaseTokenizedObject(self)
        tokenizedObject.tokens.append(string)
        return tokenizedObject
    }
    
}


public class ArrayTokenizedObject<T: BaseTokenizedObject>: BaseTokenizedObject {
    
    public subscript(index: Int) -> T {
        return T.self.init(self.append("\(index)"))
    }
    
}

public class DictionaryTokenizedObject<T: BaseTokenizedObject>: BaseTokenizedObject {
    
    public subscript(key: String) -> T {
        return T.self.init(self.append("\(key)"))
    }
    
}

