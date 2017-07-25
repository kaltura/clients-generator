//
//  ApiException.swift
//  Pods
//
//  Created by Jonathan Kanarek on 20/07/2017.
//
//
import SwiftyJSON

public class ApiException : ObjectBase, Error{
    public var message: String?
    public var code: String?
    public var args: [ApiExceptionArg]?
    
    public required init() {
        super.init()
    }
    
    public convenience init(message: String, code: String, args: [ApiExceptionArg]) {
        self.init()
        
        self.message = message
        self.code = code
        self.args = args
    }
    
    public convenience init(message: String, code: String) {
        self.init(message: message, code: code, args: [])
    }
    
    internal override func populate(_ dict: [String: Any]) throws {
        try super.populate(dict);
        // set members values:
        message = dict["message"] as? String
        code = dict["code"] as? String
        if let argsDict = dict["args"] as? [String: String] {
            args = []
            for (key, value) in argsDict {
                let arg = ApiExceptionArg()
                arg.name = key
                arg.value = value
            }
        }
    }
}

public class ApiClientException : ApiException {
    
    public enum ErrorCode: String {
        case typeNotFound = "TYPE_NOT_FOUND"
        case invalidJson = "INVALID_JSON"
        case invalidJsonObject = "INVALID_JSON_OBJECT"
        case httpError = "HTTP_ERROR"
    }
    
    public convenience init(message: String, code: ErrorCode) {
        self.init(message: message, code: code.rawValue)
    }
}
