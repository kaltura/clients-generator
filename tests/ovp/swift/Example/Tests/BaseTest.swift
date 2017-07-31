
import Quick
import Nimble
import KalturaClient

class BaseTest: QuickSpec {
    var client: Client?
    var secret: String = "ed0b955841a5ec218611c4869256aaa4"
    var partnerId: Int = 1676801
    static var uniqueTag: String = uniqueString()
    
    public var executor: RequestExecutor = USRExecutor.shared
    
    static func uniqueString() -> String {
        let uuid = UUID().uuidString
        return uuid.substring(to: uuid.index(uuid.startIndex, offsetBy: 8))
    }
    
    func login(done: @escaping (_ error: ApiException?) -> Void) {
        
        let requestBuilder:RequestBuilder<String> = SessionService.start(secret: self.secret, userId: nil, type: SessionType.ADMIN, partnerId: self.partnerId)
        
        requestBuilder.set(completion: {(ks: String?, error: ApiException?) in
            
            self.client!.ks = ks
            done(error)
        })
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    func deleteEntry(entryId: String, done: @escaping (_ error: ApiException?) -> Void) {
        
        let requestBuilder:RequestBuilder<Void> = MediaService.delete(entryId: entryId)
        
        requestBuilder.set(completion: {(void: Void?, error: ApiException?) in
            done(error)
        })
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    func createMediaEntry(created: @escaping (_ createdEntry: MediaEntry?, _ error: ApiException?) -> Void) {
        let entry: MediaEntry = MediaEntry()
        entry.mediaType = MediaType.VIDEO
        entry.tags = BaseTest.uniqueTag
        
        let requestBuilder:RequestBuilder<MediaEntry> = MediaService.add(entry: entry)
        requestBuilder.set(completion: {(createdEntry: MediaEntry?, error: ApiException?) in
            
            created(createdEntry, error)
        })
        
        executor.send(request: requestBuilder.build(client!))
    }
    
    func createMediaEntries(count: Int, created: @escaping (_ createdEntries: [MediaEntry]) -> Void) {
        var entries: [MediaEntry] = []
        for _ in 1...count {
            self.createMediaEntry() { entry, error in
                entries.append(entry!)
                if entries.count == count {
                    created(entries)
                }
            }
        }
    }
}
