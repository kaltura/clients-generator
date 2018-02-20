
const fs = require('fs');
const md5 = require('md5');
const cache = require('node-shared-cache');
const expect = require("chai").expect;
const shortid = require('shortid');
const kaltura = require('../KalturaClient');

const testConfig = JSON.parse(fs.readFileSync('./config.json', 'utf8'));
const {partnerId, serviceUrl} = testConfig;

let config = new kaltura.Configuration();
config.serviceUrl = serviceUrl;

const client = new kaltura.Client(config);

var userId;

const obj = new cache.Cache("test", 524288);
const username = obj.username;
const password = obj.password;

describe("User", () => {
	
	describe("login", () => {
		
		it('returns valid ks', (done) => {
			kaltura.services.ottUser.login(partnerId, username, password)
			.completion((success, response) => {
				const {executionTime, result} = response;
				const loginResponse = result;
				expect(success).to.equal(true);
				console.dir(loginResponse);
				expect(loginResponse).to.not.be.a('null');
				expect(loginResponse.loginSession).to.not.be.a('null');
				expect(loginResponse.loginSession.ks).to.not.be.a('null');
				expect(loginResponse.user).to.not.be.a('null');
				expect(loginResponse.user.id).to.not.be.a('null');
				
				client.setKs(loginResponse.loginSession.ks);
				userId = loginResponse.user.id
				
				done();
			})
			.execute(client);
		});
	});
	
	describe("app-token", () => {
		var appToken = new kaltura.objects.AppToken({
			hashType: kaltura.enums.AppTokenHashType.MD5
		});
		
		it('App-Token created', (done) => {
			
			kaltura.services.appToken.add(appToken)
			.completion((success, response) => {
				const {executionTime, result} = response;
				appToken = result;
				expect(success).to.equal(true);
				console.dir(appToken);
				expect(appToken).to.not.be.a('null');
				expect(appToken.id).to.not.be.a('null');
				expect(appToken.token).to.not.be.a('null');
				expect(appToken.sessionUserId).to.not.be.a('null');
				expect(appToken.sessionUserId).to.equal(userId);
				done();
			})
			.execute(client);
		});
		
		it('KS created', (done) => {
			
			client.setKs(null);
			kaltura.services.ottUser.anonymousLogin(partnerId)
			.completion((success, response) => {
				const {executionTime, result} = response;
				const loginSession = result;
				expect(success).to.equal(true);
				console.dir(loginSession);
				expect(loginSession).to.not.be.a('null');
				expect(loginSession.ks).to.not.be.a('null');
				
				client.setKs(loginSession.ks);
				
				const tokenHash = md5(loginSession.ks + appToken.token);
				kaltura.services.appToken.startSession(appToken.id, tokenHash)
				.completion((success, response) => {
					const {executionTime, result} = response;
					const sessionInfo = result;
					expect(success).to.equal(true);
					console.dir(sessionInfo);
					expect(sessionInfo).to.not.be.a('null');
					expect(sessionInfo.ks).to.not.be.a('null');
					expect(sessionInfo.userId).to.not.be.a('null');
					expect(sessionInfo.userId).to.equal(userId);
					
					client.setKs(sessionInfo.ks);
					
					done();
				})
				.execute(client);
			})
			.execute(client);
			
		});
		
		it('KS valid', (done) => {
			
			kaltura.services.session.get()
			.completion((success, response) => {
				const {executionTime, result} = response;
				const session = result;
				expect(success).to.equal(true);
				console.dir(session);
				expect(session).to.not.be.a('null');
				expect(session.ks).to.not.be.a('null');
				expect(session.userId).to.not.be.a('null');
				expect(session.userId).to.equal(userId);
				
				done();
			})
			.execute(client);
			
		});
		
		it('App-Token deleted', (done) => {
			
			kaltura.services.appToken.deleteAction(appToken.id)
			.completion((success, response) => {
				const {executionTime, result} = response;
				expect(success).to.equal(true);
				expect(result).to.equal(true);
				done();
			})
			.execute(client);
		});
		
		it('KS invalid', (done) => {
			
			kaltura.services.session.get()
			.completion((success, response) => {
				const {executionTime, result} = response;
				expect(success).to.equal(false);
				console.dir(result);
				expect(result).to.not.be.a('null');
				expect(result.error).to.not.be.a('null');
				expect(result.error.code).to.not.be.a('null');
				expect(result.error.code).to.equal('500016');
				
				done();
			})
			.execute(client);
			
		});
	});
});

