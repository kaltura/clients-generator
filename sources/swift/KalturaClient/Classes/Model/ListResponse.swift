//
//  ListResponse.swift
//  Pods
//
//  Created by Jonathan Kanarek on 17/07/2017.
//
//

import Foundation

open class ListResponse<T: ObjectBase> {
    var objects: Array<T>?
    var totalCount: Int?
}
