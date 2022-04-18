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

public class ApiException : ObjectBase, Error, @unchecked Sendable {
    // These properties were changed to read-only when `@unchecked Sendable` was added for
    // Xcode 13.3. If you make them publicly writable or alter their values within the class
    // you will need to do so with thread-safety in mind.
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
