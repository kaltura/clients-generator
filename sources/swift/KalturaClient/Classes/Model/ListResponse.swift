//
//  ListResponse.swift
//  Pods
//
//  Created by Jonathan Kanarek on 17/07/2017.
//
//

import Foundation

open class ListResponse<T: ObjectBase> : ObjectBase {
    public var objects: Array<T>?
    public var totalCount: Int?
    
    internal override func populate(_ dict: [String: Any]) throws {
        try super.populate(dict);
        // set members values:
        if dict["totalCount"] != nil {
            totalCount = dict["totalCount"] as? Int
        }
        if dict["objects"] != nil {
            objects = try JSONParser.parse(array: dict["objects"] as! [Any])
        }
    }
}
