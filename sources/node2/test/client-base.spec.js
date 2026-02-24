const { expect } = require('chai');
const axios = require('axios');
const md5 = require('md5');

/**
 * Basic tests for Kaltura Node2 Client
 * Tests that the client dependencies and environment are working correctly
 */
describe('Kaltura Node2 Client - Environment & Dependencies', function() {

  describe('Required Dependencies', function() {
    it('should have axios installed and working', function() {
      expect(axios).to.exist;
      expect(axios.get).to.be.a('function');
      expect(axios.post).to.be.a('function');
    });

    it('should have md5 installed and working', function() {
      expect(md5).to.exist;
      expect(md5).to.be.a('function');

      const testString = 'test';
      const hash = md5(testString);
      expect(hash).to.equal('098f6bcd4621d373cade4e832627b4f6');
    });

    it('should have path module available', function() {
      const path = require('path');
      expect(path).to.exist;
      expect(path.join).to.be.a('function');
    });
  });

  describe('Node.js Version Check', function() {
    it('should be running Node.js >= 20.20.0', function() {
      const nodeVersion = process.version;
      const versionMatch = nodeVersion.match(/^v(\d+)\.(\d+)\.(\d+)/);

      expect(versionMatch).to.exist;

      const major = parseInt(versionMatch[1]);
      const minor = parseInt(versionMatch[2]);
      const patch = parseInt(versionMatch[3]);

      // Check if version is >= 20.20.0
      const isValidVersion =
        major > 20 ||
        (major === 20 && minor > 20) ||
        (major === 20 && minor === 20 && patch >= 0);

      expect(isValidVersion).to.be.true;
      console.log(`      ✓ Running on Node.js ${nodeVersion}`);
    });
  });

  describe('Client Configuration Structure', function() {
    it('should support basic configuration object', function() {
      const config = {
        serviceUrl: 'https://www.kaltura.com/api_v3',
        partnerId: 12345,
        ks: null,
        clientTag: 'node2-test-client',
        requestTimeout: 30000
      };

      expect(config.serviceUrl).to.equal('https://www.kaltura.com/api_v3');
      expect(config.partnerId).to.equal(12345);
      expect(config.clientTag).to.equal('node2-test-client');
      expect(config.requestTimeout).to.equal(30000);
    });

    it('should handle different partner IDs', function() {
      const partnerIds = [12345, 67890, 11111, 99999];

      partnerIds.forEach(partnerId => {
        const config = {
          serviceUrl: 'https://www.kaltura.com/api_v3',
          partnerId: partnerId
        };
        expect(config.partnerId).to.equal(partnerId);
      });
    });

    it('should support various service URLs', function() {
      const urls = [
        'https://www.kaltura.com/api_v3',
        'https://cdnapi.kaltura.com/api_v3',
        'https://your-domain.kaltura.com/api_v3',
        'http://localhost:8080/api_v3'
      ];

      urls.forEach(url => {
        const config = {
          serviceUrl: url,
          partnerId: 12345
        };
        expect(config.serviceUrl).to.equal(url);
      });
    });
  });

  describe('MD5 Hashing (used for request signing)', function() {
    it('should generate consistent MD5 hashes', function() {
      const testCases = [
        { input: '', expected: 'd41d8cd98f00b204e9800998ecf8427e' },
        { input: 'kaltura', expected: '5daf6415e506534a6b4810391b66d528' },
        { input: 'test123', expected: 'cc03e747a6afbbcbf8be7668acfebee5' }
      ];

      testCases.forEach(({ input, expected }) => {
        const hash = md5(input);
        expect(hash).to.equal(expected);
      });
    });

    it('should handle Unicode strings', function() {
      const unicodeString = 'Hello 世界';
      const hash = md5(unicodeString);

      expect(hash).to.be.a('string');
      expect(hash).to.have.lengthOf(32);
    });
  });

  describe('HTTP Client (Axios)', function() {
    it('should be able to create axios instance', function() {
      const instance = axios.create({
        timeout: 30000,
        headers: {
          'User-Agent': 'Kaltura-Node2-Client'
        }
      });

      expect(instance).to.exist;
      expect(instance.defaults.timeout).to.equal(30000);
    });

    it('should support timeout configuration', function() {
      const timeouts = [10000, 30000, 60000, 120000];

      timeouts.forEach(timeout => {
        const instance = axios.create({ timeout });
        expect(instance.defaults.timeout).to.equal(timeout);
      });
    });

    it('should support custom headers', function() {
      const headers = {
        'User-Agent': 'Kaltura-Test-Client',
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      };

      const instance = axios.create({ headers });

      expect(instance.defaults.headers).to.include(headers);
    });
  });

  describe('Request Data Serialization', function() {
    it('should serialize simple objects to JSON', function() {
      const data = {
        service: 'session',
        action: 'start',
        secret: 'test-secret',
        userId: 'test-user',
        type: 2,
        partnerId: 12345
      };

      const serialized = JSON.stringify(data);
      const deserialized = JSON.parse(serialized);

      expect(deserialized).to.deep.equal(data);
    });

    it('should handle nested objects', function() {
      const data = {
        filter: {
          objectType: 'KalturaMediaEntryFilter',
          nameLike: 'test',
          tagsLike: 'video'
        },
        pager: {
          pageSize: 30,
          pageIndex: 1
        }
      };

      const serialized = JSON.stringify(data);
      const deserialized = JSON.parse(serialized);

      expect(deserialized).to.deep.equal(data);
    });

    it('should handle arrays', function() {
      const data = {
        entryIds: ['1_abc123', '1_def456', '1_ghi789'],
        tags: ['video', 'tutorial', 'demo']
      };

      const serialized = JSON.stringify(data);
      const deserialized = JSON.parse(serialized);

      expect(deserialized.entryIds).to.have.lengthOf(3);
      expect(deserialized.tags).to.include('video');
    });
  });

  describe('Client Integration Readiness', function() {
    it('should have all core modules for HTTP communication', function() {
      expect(axios).to.exist;
      expect(axios.create).to.be.a('function');
      expect(axios.interceptors).to.exist;
    });

    it('should have crypto utilities for request signing', function() {
      expect(md5).to.exist;

      // Test basic signature generation
      const params = 'action=start&partnerId=12345&secret=test';
      const signature = md5(params);

      expect(signature).to.be.a('string');
      expect(signature).to.have.lengthOf(32);
    });

    it('should support Promise-based async operations', function(done) {
      const testPromise = new Promise((resolve) => {
        setTimeout(() => resolve('success'), 10);
      });

      testPromise.then(result => {
        expect(result).to.equal('success');
        done();
      });
    });

    it('should support async/await pattern', async function() {
      const asyncFunction = async () => {
        return 'async-result';
      };

      const result = await asyncFunction();
      expect(result).to.equal('async-result');
    });
  });
});
