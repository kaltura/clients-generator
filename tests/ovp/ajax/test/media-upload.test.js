
var config = new KalturaConfiguration();
config.serviceUrl = serviceUrl;
config.setLogger(new IKalturaLogger());

var client = new KalturaClient(config);

describe("Start session", function() {
    describe("User KS", function() {
    	var userId = null;
    	var type = 0; // KalturaSessionType.USER
    	var expiry = null;
    	var privileges = null;

    	it('not null', function(done) {
    		KalturaSessionService.start(secret, userId, type, partnerId, expiry, privileges)
        	.completion(function(success, ks) {
        		expect(success).toBe(true);
        		expect(ks).not.toBe(null);
        		client.setKs(ks);
        		done();
        	})
        	.execute(client);
        });
    });
});


describe("media", function() {
    describe("upload", function() {

    	var entry = {
    		mediaType: 1, // KalturaMediaType.VIDEO
    		name: 'test'
    	};

    	var uploadToken = {};

    	var createdEntry;
    	var createdUploadToken;
    	
    	it('create entry', function(done) {
    		KalturaMediaService.add(entry)
    		.completion(function(success, entry) {
        		expect(success).toBe(true);
        		expect(entry).not.toBe(null);
        		expect(entry.id).not.toBe(null);
        		expect(entry.status.toString()).toBe('7'); // KalturaEntryStatus.NO_CONTENT

        		createdEntry = entry;
        		done();
    		})
    		.execute(client)
        });
        
    	it('create upload-token', function(done) {
    		KalturaUploadTokenService.add(uploadToken)
    		.completion(function(success, uploadToken) {
        		expect(success).toBe(true);
        		expect(uploadToken).not.toBe(null);
        		expect(uploadToken.id).not.toBe(null);
        		expect(uploadToken.status).toBe(0); // KalturaUploadTokenStatus.PENDING

        		createdUploadToken = uploadToken;
        		done();
    		})
    		.execute(client);
        });
        
    	it('add content', function(done) {
    		var mediaResource = {
    			objectType: 'KalturaUploadedFileTokenResource',
    			token: createdUploadToken.id
        	};
    		
    		KalturaMediaService.addContent(createdEntry.id, mediaResource)
    		.completion(function(success, entry) {
        		expect(success).toBe(true);
        		expect(entry.status.toString()).toBe('0'); // KalturaEntryStatus.IMPORT

        		done();
    		})
    		.execute(client);
    	});
        
//    	Karma doesn't support creating <input type=file> 
//    	it('upload file', function(done) {
//    		var filename = './DemoVideo.mp4';
//    		KalturaUploadTokenService.upload(createdUploadToken.id, filename)
//    		.completion(function(success, uploadToken) {
//        		expect(success).toBe(true);
//        		expect(uploadToken).not.toBe(null);
//        		expect(uploadToken.id).not.toBe(null);
//        		expect(uploadToken.fileSize).toBeGreaterThan(0);
//        		expect(uploadToken.status).toBe(3); // KalturaUploadTokenStatus.CLOSED
//
//        		createdUploadToken = uploadToken;
//        		done();
//    		})
//    		.execute(client);
//        });
    });
});
