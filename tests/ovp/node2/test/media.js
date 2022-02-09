
const fs = require('fs');
const expect = require("chai").expect;
const kaltura = require('../KalturaClient');

const testConfig = JSON.parse(fs.readFileSync('./config.json', 'utf8'));
const {secret, partnerId, serviceUrl} = testConfig;

let config = new kaltura.Configuration();
config.serviceUrl = serviceUrl;

// Uncomment if proxy is needed
/*
const proxyUrl = new URL('http://127.0.0.1:3128');
proxyUrl.username = 'user';
proxyUrl.password = 'pass';
    
config.proxy = proxyUrl.toString();
*/

const client = new kaltura.Client(config);


describe("Start session", () => {
    describe("User KS", () => {
    	let userId = null;
    	let type = kaltura.enums.SessionType.USER;
    	let expiry = null;
    	let privileges = null;

    	it('not null', (done) => {
    		kaltura.services.session.start(secret, userId, type, partnerId, expiry, privileges)
        	.completion((success, ks) => {
        		expect(success).to.equal(true);
        		expect(ks).to.not.be.a('null');
        		client.setKs(ks);
        		done();
        	})
        	.execute(client);
        });
    });
});

describe("Add media", () => {
    describe("Multiple requests", () => {

    	let entry = new kaltura.objects.MediaEntry({
    		mediaType: kaltura.enums.MediaType.VIDEO,
    		name: 'test עברית عربيه हिन्दू'
    	});

    	let uploadToken = new kaltura.objects.UploadToken({
    	});

    	let createdEntry;
    	let createdUploadToken;

    	it('entry created', (done) => {
    		kaltura.services.media.add(entry)
    		.execute(client)
    		.then((entry) => {
        		expect(entry).to.not.be.a('null');
        		expect(entry.id).to.not.be.a('null');
        		expect(entry.status.toString()).to.equal(kaltura.enums.EntryStatus.NO_CONTENT);

        		createdEntry = entry;
        		return kaltura.services.uploadToken.add(uploadToken)
        		.execute(client);
    		})
    		.then((uploadToken) => {
        		expect(uploadToken).to.not.be.a('null');
        		expect(uploadToken.id).to.not.be.a('null');
        		expect(uploadToken.status).to.equal(kaltura.enums.UploadTokenStatus.PENDING);

        		createdUploadToken = uploadToken;
        		
        		let mediaResource = new kaltura.objects.UploadedFileTokenResource({
        			token: uploadToken.id
            	});
        		
        		return kaltura.services.media.addContent(createdEntry.id, mediaResource)
        		.execute(client);
    		})
    		.then((entry) => {
        		expect(entry.status.toString()).to.equal(kaltura.enums.EntryStatus.IMPORT);

        		let filePath = './test/DemoVideo.mp4';
        		return kaltura.services.uploadToken.upload(createdUploadToken.id, filePath)
        		.execute(client);
    		})
    		.then((uploadToken) => {
        		expect(uploadToken.status).to.equal(kaltura.enums.UploadTokenStatus.CLOSED);
        		done();
    		});
    	});
    });
    

    describe("Single multi-request", () => {
    	let entry = new kaltura.objects.MediaEntry({
    		mediaType: kaltura.enums.MediaType.VIDEO,
    		name: 'test'
    	});

    	let uploadToken = new kaltura.objects.UploadToken({
    	});

		let mediaResource = new kaltura.objects.UploadedFileTokenResource({
			token: '{2:result:id}'
    	});
		
		let filePath = './test/DemoVideo.mp4';

    	it('entry created', (done) => {
    		kaltura.services.media.add(entry)
    		.add(kaltura.services.uploadToken.add(uploadToken))
    		.add(kaltura.services.media.addContent('{1:result:id}', mediaResource))
    		.add(kaltura.services.uploadToken.upload('{2:result:id}', filePath))
    		.execute(client)
    		.then((results) => {
    			
    			entry = results[0];
        		expect(entry).to.not.be.a('null');
        		expect(entry.id).to.not.be.a('null');
        		expect(entry.status.toString()).to.equal(kaltura.enums.EntryStatus.NO_CONTENT);

    			uploadToken = results[1];
        		expect(uploadToken).to.not.be.a('null');
        		expect(uploadToken.id).to.not.be.a('null');
        		expect(uploadToken.status).to.equal(kaltura.enums.UploadTokenStatus.PENDING);

    			entry = results[2];
        		expect(entry.status.toString()).to.equal(kaltura.enums.EntryStatus.IMPORT);

    			uploadToken = results[3];
        		expect(uploadToken.status).to.equal(kaltura.enums.UploadTokenStatus.CLOSED);
        		
        		done();
    		});
    	});
    });

	describe("from buffers", () => {
		const filePath = './test/DemoVideo.mp4';
		const uploadToken = new kaltura.objects.UploadToken();
		let createdUploadToken;
		it('creates file', (done) => {
			kaltura.services.uploadToken.add(uploadToken)
				.execute(client)
				.then(token => {
					createdUploadToken = token;
					return kaltura.services.uploadToken.upload(createdUploadToken.id, Buffer.alloc(0), false, false, 0)
						.execute(client);
				})
				.then((uploadToken) => {
					expect(uploadToken.status).to.equal(kaltura.enums.UploadTokenStatus.PARTIAL_UPLOAD);
					const mediaEntry = new kaltura.objects.MediaEntry();
					mediaEntry.mediaType = kaltura.enums.MediaType.VIDEO;
					return kaltura.services.media.add(mediaEntry)
						.execute(client);
				})
				.then(entry => {
					const resource = new kaltura.objects.UploadedFileTokenResource();
					resource.token = createdUploadToken.id;
					return kaltura.services.media.addContent(entry.id, resource).execute(client);
				})
				.then(() => {
					return new Promise((resolve, reject) => {
						const uploads = [];
						const stats = fs.statSync(filePath);
						const rs = fs.createReadStream(filePath);
						let resumeAt = 0;
						rs.on('data', (data) => {
							const p = kaltura.services.uploadToken.upload(createdUploadToken.id, data, true, rs.bytesRead === stats.size, resumeAt)
								.execute(client);
							uploads.push(p);
							resumeAt += data.length;
						});
						rs.once('close', () => resolve(Promise.all(uploads)));
						rs.once('error', reject);
					})
				})
				.then(uploadTokens => {
					const lastUpload = uploadTokens.pop();
					uploadTokens.forEach((token) => expect(token.status).to.equal(kaltura.enums.UploadTokenStatus.PARTIAL_UPLOAD));
					expect(lastUpload.status).to.equal(kaltura.enums.UploadTokenStatus.CLOSED);
					done();
				})
				.catch((err) => done(err));
		});
	});
});
