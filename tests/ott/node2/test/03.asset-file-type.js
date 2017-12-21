
const fs = require('fs');
const md5 = require('md5');
const cache = require('node-shared-cache');
const expect = require("chai").expect;
const shortid = require('shortid');
const kaltura = require('../KalturaClient');

const testConfig = JSON.parse(fs.readFileSync('./config.json', 'utf8'));
const {partnerId, serviceUrl, operatorUsername, operatorPassword} = testConfig;

let config = new kaltura.Configuration();
config.serviceUrl = serviceUrl;

const client = new kaltura.Client(config);

describe("Anonymous", () => {
	
	describe("login", () => {
		
		it('returns valid ks', (done) => {
			kaltura.services.ottUser.anonymousLogin(partnerId)
			.completion((success, response) => {
				const {executionTime, result} = response;
				const loginSession = result;
				expect(success).to.equal(true);
				console.dir(loginSession);
				expect(loginSession).to.not.be.a('null');
				expect(loginSession.ks).to.not.be.a('null');
				
				client.setKs(loginSession.ks);
				
				done();
			})
			.execute(client);
		});
		
		it('asset-file-type list', (done) => {
			
			kaltura.services.assetFileType.listAction()
			.completion((success, response) => {
				const {executionTime, result} = response;
				list = result;
				expect(success).to.equal(true);
				console.dir(list);
				expect(list).to.not.be.a('null');
				expect(list.totalCount).to.not.be.a('null');
				expect(list.totalCount).to.be.above(0);
				expect(list.objects).to.not.be.a('null');
				expect(list.objects).to.be.an('array');
				expect(list.objects.length).to.equal(list.totalCount);
				
				done();
			})
			.execute(client);
		});
		
	});
});
	
describe("Operator", () => {
	var assetFileTypeId;
	
	it('Login', (done) => {
		kaltura.services.ottUser.login(partnerId, operatorUsername, operatorPassword)
		.completion((success, response) => {
			const {executionTime, result} = response;
			const loginResponse = result;
			expect(success).to.equal(true);
			console.dir(loginResponse);
			expect(loginResponse).to.not.be.a('null');
			expect(loginResponse.loginSession).to.not.be.a('null');
			expect(loginResponse.loginSession.ks).to.not.be.a('null');
			client.setKs(loginResponse.loginSession.ks);
			done();
		})
		.execute(client);
	});

	const assetFileType = new kaltura.objects.AssetFileType({
		type: kaltura.enums.AssetFileStreamType.FLV,
		description: shortid.generate(),
		isActive: true,
		enableOfflinePlayback: false,
		enableTrailerPlayback: false,
		streamerType: kaltura.enums.AssetFileStreamerType.APPLE_HTTP
	});
	
	it('Asset-File-Type add', (done) => {
		
		kaltura.services.assetFileType.add(assetFileType)
		.completion((success, response) => {
			const {executionTime, result} = response;
			createdAssetFileType = result;
			expect(success).to.equal(true);
			console.dir(createdAssetFileType);
			expect(createdAssetFileType).to.not.be.a('null');
			expect(createdAssetFileType.id).to.not.be.a('null');
			expect(createdAssetFileType.createDate).to.not.be.a('null');
			expect(createdAssetFileType.updateDate).to.not.be.a('null');
			expect(createdAssetFileType.type).to.equal(assetFileType.type);
			expect(createdAssetFileType.description).to.equal(assetFileType.description);
			expect(createdAssetFileType.isActive).to.equal(assetFileType.isActive);
			expect(createdAssetFileType.enableOfflinePlayback).to.equal(assetFileType.enableOfflinePlayback);
			expect(createdAssetFileType.enableTrailerPlayback).to.equal(assetFileType.enableTrailerPlayback);
			expect(createdAssetFileType.streamerType).to.equal(assetFileType.streamerType);
			expect(createdAssetFileType.drmAdapterProfileId).to.equal(assetFileType.drmAdapterProfileId);
			
			assetFileTypeId = createdAssetFileType.id;
			
			done();
		})
		.execute(client);
	});		
	
	it('Asset-File-Type get', (done) => {
		
		kaltura.services.assetFileType.get(assetFileTypeId)
		.completion((success, response) => {
			const {executionTime, result} = response;
			gotAssetFileType = result;
			expect(success).to.equal(true);
			console.dir(gotAssetFileType);
			expect(gotAssetFileType).to.not.be.a('null');
			expect(gotAssetFileType.id).to.not.be.a('null');
			expect(gotAssetFileType.createDate).to.not.be.a('null');
			expect(gotAssetFileType.updateDate).to.not.be.a('null');
			expect(gotAssetFileType.type).to.equal(assetFileType.type);
			expect(gotAssetFileType.description).to.equal(assetFileType.description);
			expect(gotAssetFileType.isActive).to.equal(assetFileType.isActive);
			expect(gotAssetFileType.enableOfflinePlayback).to.equal(assetFileType.enableOfflinePlayback);
			expect(gotAssetFileType.enableTrailerPlayback).to.equal(assetFileType.enableTrailerPlayback);
			expect(gotAssetFileType.streamerType).to.equal(assetFileType.streamerType);
			expect(gotAssetFileType.drmAdapterProfileId).to.equal(assetFileType.drmAdapterProfileId);
			
			done();
		})
		.execute(client);
	});
	
	it('Asset-File-Type update', (done) => {
		
		const updateAssetFileType = new kaltura.objects.AssetFileType({
			description: shortid.generate(),
			isActive: false
		});
		
		kaltura.services.assetFileType.update(assetFileTypeId, updateAssetFileType)
		.completion((success, response) => {
			const {executionTime, result} = response;
			updatedAssetFileType = result;
			expect(success).to.equal(true);
			console.dir(updatedAssetFileType);
			expect(updatedAssetFileType).to.not.be.a('null');
			expect(updatedAssetFileType.updateDate).to.be.above(updatedAssetFileType.createDate);
			expect(updatedAssetFileType.description).to.equal(updateAssetFileType.description);
			expect(updatedAssetFileType.isActive).to.equal(updateAssetFileType.isActive);
			
			done();
		})
		.execute(client);
	});	
	
	it('Asset-File-Type delete', (done) => {
		
		kaltura.services.assetFileType.deleteAction(assetFileTypeId)
		.completion((success, response) => {
			const {executionTime, result} = response;
			gotAssetFileType = result;
			expect(success).to.equal(true);
			
			
			kaltura.services.assetFileType.get(assetFileTypeId)
			.completion((success, response) => {
				const {executionTime, result} = response;
				expect(success).to.equal(false);
				console.dir(result);
				expect(result).to.not.be.a('null');
				expect(result.error).to.not.be.a('null');
				expect(result.error.code).to.not.be.a('null');
				expect(result.error.code).to.equal(3);
				
				done();
			})
			.execute(client);
		})
		.execute(client);
	});
});

