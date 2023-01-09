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

public protocol ApiException: Error {
    var message: String? { get }
    var code: String? { get }
    var args: [ApiExceptionArg]? { get }
}

public struct ApiClientException: ApiException {

    public enum ErrorCode: String {
        case typeNotFound = "TYPE_NOT_FOUND"
        case invalidJson = "INVALID_JSON"
        case invalidJsonObject = "INVALID_JSON_OBJECT"
        case httpError = "HTTP_ERROR"
    }
    
    public let message: String?
    public let code: String?
    public let args: [ApiExceptionArg]?
    
    init(message: String? = nil, code: String? = nil, args: [ApiExceptionArg]? = nil) {
        self.message = message
        self.code = code
        self.args = args
    }
}
