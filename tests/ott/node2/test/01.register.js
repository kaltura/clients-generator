
const fs = require('fs');
const cache = require('node-shared-cache');
const expect = require("chai").expect;
const shortid = require('shortid');
const kaltura = require('../KalturaClient');

const testConfig = JSON.parse(fs.readFileSync('./config.json', 'utf8'));
const {partnerId, serviceUrl} = testConfig;

let config = new kaltura.Configuration();
config.serviceUrl = serviceUrl;

const client = new kaltura.Client(config);

const username = shortid.generate();
const password = shortid.generate();

cache.release("test");

var obj = new cache.Cache("test", 524288);
obj.username = username;
obj.password = password;

describe("User", () => {
	
    describe("register", () => {
    	const user = new kaltura.objects.OTTUser({
    		username: username,
    		firstName: shortid.generate(),
    		lastName: shortid.generate(),
    		email: shortid.generate() + "@test.com"
    	});

    	it('creates user', (done) => {
    		kaltura.services.ottUser.register(partnerId, user, password)
        	.completion((success, response) => {
				const {executionTime, result} = response;
				const user = result;
        		expect(success).to.equal(true);
        		expect(user).to.not.be.a('null');
        		expect(user.id).to.not.be.a('null');
        		done();
        	})
        	.execute(client);
        });
    });
	
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
        		client.setKs(loginResponse.loginSession.ks);
        		done();
        	})
        	.execute(client);
        });
    });
	
    describe("household", () => {
    	const household = new kaltura.objects.Household({
    		name: shortid.generate(),
    		description: shortid.generate(),
    		externalId: shortid.generate()
    	});

    	it('created', (done) => {
    		kaltura.services.household.add(household)
        	.completion((success, response) => {
				const {executionTime, result} = response;
				const household = result;
        		expect(success).to.equal(true);
        		expect(household).to.not.be.a('null');
        		expect(household.id).to.not.be.a('null');
				
        		done();
        	})
        	.execute(client);
        });
		
    	it('logged in as master', (done) => {
    		kaltura.services.ottUser.login(partnerId, username, password)
        	.completion((success, response) => {
				const {executionTime, result} = response;
				const loginResponse = result;
        		expect(success).to.equal(true);
        		expect(loginResponse).to.not.be.a('null');
        		expect(loginResponse.loginSession).to.not.be.a('null');
        		expect(loginResponse.loginSession.ks).to.not.be.a('null');
        		client.setKs(loginResponse.loginSession.ks);
        		done();
        	})
        	.execute(client);
        });
    });
});
