
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

    describe("multi-request", function() {
    	var entry = {
    		mediaType: 1, // KalturaMediaType.VIDEO
    		name: 'test'
    	};

    	var uploadToken = {
    	};

    	var mediaResource = {
    		objectType: 'KalturaUploadedFileTokenResource',
			token: '{2:result:id}'
    	};

		var filename = './DemoVideo.mp4';
		
    	jasmine.DEFAULT_TIMEOUT_INTERVAL = 60000;
    	it('create entry', function(done) {
    		KalturaMediaService.add(entry)
    		.add(KalturaUploadTokenService.add(uploadToken))
    		.add(KalturaMediaService.addContent('{1:result:id}', mediaResource))
//    		Karma doesn't support creating <input type=file>
//    		.add(KalturaUploadTokenService.upload('{2:result:id}', filename))
    		.completion(function(success, results) {
        		expect(success).toBe(true);
        		
    			entry = results[0];
        		expect(entry).not.toBe(null);
        		expect(entry.id).not.toBe(null);
        		expect(entry.status.toString()).toBe('7'); // KalturaEntryStatus.NO_CONTENT

    			uploadToken = results[1];
        		expect(uploadToken).not.toBe(null);
        		expect(uploadToken.id).not.toBe(null);
        		expect(uploadToken.status).toBe(0); // KalturaUploadTokenStatus.PENDING

    			entry = results[2];
        		expect(entry.status.toString()).toBe('0'); // KalturaEntryStatus.IMPORT

//        		Karma doesn't support creating <input type=file>
//    			uploadToken = results[3];
//        		expect(uploadToken).not.toBe(null);
//        		expect(uploadToken.id).not.toBe(null);
//        		expect(uploadToken.fileSize).toBeGreaterThan(0);
//        		expect(uploadToken.status).toBe(3); // KalturaUploadTokenStatus.CLOSED
        		
        		done();
    		})
    		.execute(client);
    	});
    });
});
