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

@objc public class SessionManager: NSObject {
    
    public enum SessionManagerError: Error{
        case failedToGetKS
        case failedToGetLoginResponse
        case failedToRefreshKS
        case failedToBuildRefreshRequest
        case invalidRefreshCallResponse
        case noRefreshTokenOrTokenToRefresh
        case failedToParseResponse
        case ksExpired
    }
    
    @objc public var partnerId: Int
    
    private var executor: RequestExecutor
    
    private var ks: String? = nil
    private var tokenExpiration: Date?

    private var username: String?
    private var password: String?
    
    private let defaultSessionExpiry = TimeInterval(24*60*60)
    
    private var client:Client
    
    public init(client: Client, partnerId: Int, executor: RequestExecutor?) {
        self.client = client
        self.partnerId = partnerId
        
        if let exe  = executor {
            self.executor = exe
        } else {
            self.executor = USRExecutor.shared
        }
    }
    
    @objc public convenience init(client: Client, partnerId: Int) {
        self.init(client: client, partnerId: partnerId, executor: nil)
    }
    
    @available(*, deprecated, message: "Use init(serverURL:partnerId:executor:)")
    public convenience init(client: Client, version: String, partnerId: Int, executor: RequestExecutor?) {
        self.init(client: client, partnerId: partnerId, executor: executor)
    }
    
    public func loadKS(completion: @escaping (String?, Error?) -> Void){
        if let ks = self.ks, self.tokenExpiration?.compare(Date()) == ComparisonResult.orderedDescending {
                completion(ks, nil)
        } else {
            
            self.ks = nil
            if let username = self.username,
                let password = self.password {
                
                self.startSession(username: username,
                                  password: password, completion: { (e:Error?) in
                                    self.ensureKSAfterRefresh(e: e, completion: completion)
                })
            }
            else {
                
                self.startAnonymousSession(completion: { (e:Error?) in
                    self.ensureKSAfterRefresh(e: e, completion: completion)
                })
            }
        }
    }
    
    
    func ensureKSAfterRefresh(e:Error?,completion: @escaping (String?, Error?) -> Void) -> Void {
        if let ks = self.ks {
            completion(ks, nil)
        } else if let error = e {
            completion(nil, error)
        } else {
            completion(nil, SessionManagerError.ksExpired)
        }
    }
    
    public func startAnonymousSession(completion:@escaping (_ error: Error?) -> Void) -> Void {
        /*
        let loginRequestBuilder = SessionService.startWidgetSession(widgetId: "_\(self.partnerId)")
            .set(completion: { (startWidgetSessionResponse: StartWidgetSessionResponse?, error: ApiException?) in
                
//                if let data = r.data {
//                    var result: OVPBaseObject? = nil
//                    do {
//                        result = try OVPResponseParser.parse(data:data)
//                        if let widgetSession = result as? OVPStartWidgetSessionResponse {
//                            self.ks = widgetSession.ks
//                            self.tokenExpiration = Date(timeIntervalSinceNow:self.defaultSessionExpiry )
//                            completion(nil)
//                            
//                        }else{
//                            completion(SessionManagerError.failedToGetKS)
//                        }
//                        
//                    }catch{
//                        completion(error)
//                    }
//                }else{
//                    completion(SessionManagerError.failedToGetLoginResponse)
//                }
            })
        
        let request = loginRequestBuilder.build(client)
        self.executor.send(request: request)
 */
    }
    
    public func startSession(username: String, password: String, completion: @escaping (_ error: Error?) -> Void) -> Void {
        /*
        self.username = username
        self.password = password
        
        let loginRequestBuilder = UserService.loginByLoginId(
                                                                loginId: username,
                                                                password: password,
                                                                partnerId: self.partnerId)
        
        let sessionGetRequest = SessionService.get(session:"{1:result}")
        let mrb = MultiRequestBuilder()
            .add(request: loginRequestBuilder)
            .add(request: sessionGetRequest)
            .set(completion: { (responses:[Any]?, err: ApiException?) in
                
//                if let data = r.data
//                {
//                    
//                        guard   let arrayResult = data as? [Any],
//                                arrayResult.count == 2
//                        else {
//                             completion(SessionManagerError.failedToParseResponse)
//                            return
//                        }
//                        
//                        let sessionInfo = OVPKalturaSessionInfo(json: arrayResult[1])
//                        self.ks = arrayResult[0] as? String
//                        self.tokenExpiration = sessionInfo?.expiry
//                        completion(nil)
//                    
//                    
//                } else {
//                    completion(SessionManagerError.failedToGetLoginResponse)
//                }
            })
            
            
        let request = mrb.build(client)
        self.executor.send(request: request)
        */
    }
}
