//
//  File.swift
//  KalturaClient
//
//  Created by Rivka Peleg on 04/09/2017.
//  Copyright © 2017 CocoaPods. All rights reserved.
//

import Foundation
import KalturaOttClient


let TConfig = TestConfig.shared

class TestConfig {
    
    
    static let shared = TestConfig()
    
    var serverURL: String = ""
    var partnerId: Int = 0
    var username: String = ""
    var password: String = ""
    var operatorUsername: String = ""
    var operatorPassword: String = ""
    var assetId: String = ""
    
    
    init() {
        guard let filePath = Bundle(for: TestConfig.self).path(forResource: "TestConfig", ofType: "plist") else {
            return
        }
        
        guard let dict = NSDictionary(contentsOfFile: filePath) else {
            return
        }

        self.serverURL = dict["serverURL"] as! String
        self.partnerId = dict["partnerId"] as! Int
        self.username = dict["username"] as! String
        self.password = dict["password"] as! String
        self.operatorUsername =  dict["operatorUsername"] as! String
        self.operatorPassword =  dict["operatorPassword"] as! String
        self.assetId =  dict["assetId"] as! String
        
    }
    
    var client: Client? {
        get {
            let config = ConnectionConfiguration()
            config.endPoint = URL(string:self.serverURL)!
            return Client(config)
        }
    }
    
}
