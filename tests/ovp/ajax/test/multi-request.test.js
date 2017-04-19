
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


describe("Add media", function() {
    describe("Multiple requests", function() {

    	var entry = {
    		mediaType: 1, // KalturaMediaType.VIDEO
    		name: 'test'
    	};

    	var uploadToken = {};

    	var createdEntry;
    	var createdUploadToken;

    	jasmine.DEFAULT_TIMEOUT_INTERVAL = 60000;
    	it('entry created', function(done) {
    		KalturaMediaService.add(entry)
    		.completion(function(success, entry) {
        		expect(success).toBe(true);
        		expect(entry).not.toBe(null);
        		expect(entry.id).not.toBe(null);
        		expect(entry.status.toString()).toBe('7'); // KalturaEntryStatus.NO_CONTENT

        		createdEntry = entry;
        		KalturaUploadTokenService.add(uploadToken)
        		.completion(function(success, uploadToken) {
            		expect(success).toBe(true);
            		expect(uploadToken).not.toBe(null);
            		expect(uploadToken.id).not.toBe(null);
            		expect(uploadToken.status).toBe(0); // KalturaUploadTokenStatus.PENDING

            		createdUploadToken = uploadToken;
            		
            		var mediaResource = {
            			objectType: 'KalturaUploadedFileTokenResource',
            			token: uploadToken.id
                	};
            		
            		KalturaMediaService.addContent(createdEntry.id, mediaResource)
            		.completion(function(success, entry) {
                		expect(success).toBe(true);
                		expect(entry.status.toString()).toBe('0'); // KalturaEntryStatus.IMPORT

                		done();
            		})
            		.execute(client);
        		})
        		.execute(client);
    		})
    		.execute(client)
    	});
    });
    

    describe("Single multi-request", function() {
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

    	jasmine.DEFAULT_TIMEOUT_INTERVAL = 60000;
    	it('entry created', function(done) {
    		KalturaMediaService.add(entry)
    		.add(KalturaUploadTokenService.add(uploadToken))
    		.add(KalturaMediaService.addContent('{1:result:id}', mediaResource))
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
        		
        		done();
    		})
    		.execute(client);
    	});
    });
});
