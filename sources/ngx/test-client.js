/**
 * Simple Node.js test script for Kaltura NGX Client
 * This demonstrates the basic API calls you would make with the NGX client
 *
 * Usage:
 * 1. First build the NGX library: npm run build
 * 2. Update the configuration below with your credentials
 * 3. Run: node test-client.js
 */

const https = require('https');
const crypto = require('crypto');

// ========== CONFIGURATION ==========
const CONFIG = {
    serviceUrl: 'https://www.kaltura.com/api_v3/service',
    partnerId: 0, // Replace with your partner ID
    adminSecret: '', // Replace with your admin secret
    userId: 'testuser@example.com',
    clientTag: 'nodejs-test-client',
    expiry: 86400 // Session expiry in seconds (24 hours)
};

// ========== HELPER FUNCTIONS ==========

/**
 * Generate SHA1 hash for KS generation
 */
function sha1(str) {
    return crypto.createHash('sha1').update(str).digest('hex');
}

/**
 * Generate a Kaltura Session (KS)
 * This mimics what SessionStartAction does in the NGX client
 */
function generateKS(partnerId, adminSecret, userId, type, expiry) {
    const fields = [partnerId, '', expiry, type, crypto.randomBytes(16).toString('hex'), userId];
    const str = fields.join(';');
    const hash = sha1(adminSecret + str);
    const ks = hash + '|' + str;
    return Buffer.from(ks).toString('base64').replace(/\+/g, '-').replace(/\//g, '_');
}

/**
 * Make a Kaltura API call
 */
function kalturaApiCall(service, action, params) {
    return new Promise((resolve, reject) => {
        const data = new URLSearchParams({
            format: 1, // JSON format
            clientTag: CONFIG.clientTag,
            apiVersion: '3.3.0',
            ...params
        }).toString();

        const options = {
            hostname: 'www.kaltura.com',
            path: `/api_v3/service/${service}/action/${action}`,
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Content-Length': data.length
            }
        };

        const req = https.request(options, (res) => {
            let body = '';
            res.on('data', (chunk) => body += chunk);
            res.on('end', () => {
                try {
                    const response = JSON.parse(body);
                    if (response.objectType && response.objectType.includes('Exception')) {
                        reject(new Error(response.message || 'API Error'));
                    } else {
                        resolve(response);
                    }
                } catch (error) {
                    reject(new Error('Failed to parse response: ' + body));
                }
            });
        });

        req.on('error', reject);
        req.write(data);
        req.end();
    });
}

// ========== TEST FUNCTIONS ==========

/**
 * Test 1: Start Session (equivalent to SessionStartAction in NGX)
 */
async function testSessionStart() {
    console.log('\n📝 Test 1: Starting Kaltura Session...');
    console.log('=' .repeat(60));

    try {
        const response = await kalturaApiCall('session', 'start', {
            secret: CONFIG.adminSecret,
            userId: CONFIG.userId,
            type: 2, // Admin session
            partnerId: CONFIG.partnerId,
            expiry: CONFIG.expiry
        });

        console.log('✅ Session started successfully!');
        console.log('KS:', response.substring(0, 50) + '...');
        return response; // This is the KS (Kaltura Session token)
    } catch (error) {
        console.error('❌ Session start failed:', error.message);
        throw error;
    }
}

/**
 * Test 2: List Media Entries (equivalent to MediaListAction in NGX)
 */
async function testListMedia(ks) {
    console.log('\n🎬 Test 2: Listing Media Entries...');
    console.log('=' .repeat(60));

    try {
        const response = await kalturaApiCall('media', 'list', {
            ks: ks,
            'filter:objectType': 'KalturaMediaEntryFilter',
            'pager:objectType': 'KalturaFilterPager',
            'pager:pageSize': 5
        });

        console.log('✅ Media list retrieved successfully!');
        console.log('Total Count:', response.totalCount);
        console.log('\nFirst few entries:');

        if (response.objects && response.objects.length > 0) {
            response.objects.forEach((entry, index) => {
                console.log(`  ${index + 1}. ${entry.name || 'Untitled'} (ID: ${entry.id})`);
                console.log(`     Type: ${entry.mediaType}, Status: ${entry.status}`);
            });
        } else {
            console.log('  No media entries found');
        }

        return response;
    } catch (error) {
        console.error('❌ List media failed:', error.message);
        throw error;
    }
}

/**
 * Test 3: Get Partner Info (equivalent to PartnerGetAction in NGX)
 */
async function testGetPartner(ks) {
    console.log('\n🏢 Test 3: Getting Partner Information...');
    console.log('=' .repeat(60));

    try {
        const response = await kalturaApiCall('partner', 'get', {
            ks: ks,
            id: CONFIG.partnerId
        });

        console.log('✅ Partner info retrieved successfully!');
        console.log('Partner Name:', response.name);
        console.log('Partner ID:', response.id);
        console.log('Partner Status:', response.status);
        console.log('Admin Email:', response.adminEmail || 'N/A');

        return response;
    } catch (error) {
        console.error('❌ Get partner failed:', error.message);
        throw error;
    }
}

/**
 * Test 4: List Categories (equivalent to CategoryListAction in NGX)
 */
async function testListCategories(ks) {
    console.log('\n📁 Test 4: Listing Categories...');
    console.log('=' .repeat(60));

    try {
        const response = await kalturaApiCall('category', 'list', {
            ks: ks,
            'filter:objectType': 'KalturaCategoryFilter',
            'pager:objectType': 'KalturaFilterPager',
            'pager:pageSize': 5
        });

        console.log('✅ Category list retrieved successfully!');
        console.log('Total Count:', response.totalCount);
        console.log('\nFirst few categories:');

        if (response.objects && response.objects.length > 0) {
            response.objects.forEach((category, index) => {
                console.log(`  ${index + 1}. ${category.name} (ID: ${category.id})`);
                console.log(`     Full Name: ${category.fullName || 'N/A'}`);
            });
        } else {
            console.log('  No categories found');
        }

        return response;
    } catch (error) {
        console.error('❌ List categories failed:', error.message);
        throw error;
    }
}

/**
 * Display NGX Client equivalent code
 */
function showNGXEquivalent() {
    console.log('\n' + '=' .repeat(60));
    console.log('📦 KALTURA NGX CLIENT EQUIVALENT CODE');
    console.log('=' .repeat(60));
    console.log(`
// 1. Install and import the NGX client in your Angular app
import { KalturaClient } from 'kaltura-ngx-client';
import { SessionStartAction } from 'kaltura-ngx-client';
import { MediaListAction } from 'kaltura-ngx-client';
import { PartnerGetAction } from 'kaltura-ngx-client';
import { CategoryListAction } from 'kaltura-ngx-client';
import { KalturaSessionType } from 'kaltura-ngx-client';
import { KalturaMediaEntryFilter } from 'kaltura-ngx-client';
import { KalturaCategoryFilter } from 'kaltura-ngx-client';
import { KalturaFilterPager } from 'kaltura-ngx-client';

// 2. Inject HttpClient and create KalturaClient instance
constructor(private http: HttpClient) {
  this.client = new KalturaClient(http, {
    endpointUrl: '${CONFIG.serviceUrl}',
    clientTag: '${CONFIG.clientTag}'
  }, null);
}

// 3. Start a session
this.client.request(new SessionStartAction({
  secret: '${CONFIG.adminSecret ? CONFIG.adminSecret.substring(0, 4) + '...' : 'YOUR_ADMIN_SECRET'}',
  userId: '${CONFIG.userId}',
  type: KalturaSessionType.admin,
  partnerId: ${CONFIG.partnerId}
})).subscribe(
  ks => {
    console.log('Session started:', ks);
    this.client.setDefaultRequestOptions({ ks });

    // 4. Now you can make API calls

    // List media entries
    this.client.request(new MediaListAction({
      filter: new KalturaMediaEntryFilter(),
      pager: new KalturaFilterPager({ pageSize: 5 })
    })).subscribe(
      response => console.log('Media:', response),
      error => console.error('Error:', error)
    );

    // Get partner info
    this.client.request(new PartnerGetAction({
      id: ${CONFIG.partnerId}
    })).subscribe(
      partner => console.log('Partner:', partner),
      error => console.error('Error:', error)
    );

    // List categories
    this.client.request(new CategoryListAction({
      filter: new KalturaCategoryFilter(),
      pager: new KalturaFilterPager({ pageSize: 5 })
    })).subscribe(
      response => console.log('Categories:', response),
      error => console.error('Error:', error)
    );
  },
  error => console.error('Session start error:', error)
);
    `);
}

// ========== MAIN EXECUTION ==========

async function runTests() {
    console.log('\n🎥 KALTURA NGX CLIENT TEST SUITE');
    console.log('=' .repeat(60));

    // Validate configuration
    if (!CONFIG.partnerId || !CONFIG.adminSecret) {
        console.error('\n❌ ERROR: Please configure your Partner ID and Admin Secret');
        console.error('Edit the CONFIG object in this file and add your credentials.\n');
        showNGXEquivalent();
        return;
    }

    try {
        // Run all tests
        const ks = await testSessionStart();
        await testListMedia(ks);
        await testGetPartner(ks);
        await testListCategories(ks);

        console.log('\n' + '=' .repeat(60));
        console.log('✅ ALL TESTS COMPLETED SUCCESSFULLY!');
        console.log('=' .repeat(60));

        showNGXEquivalent();

    } catch (error) {
        console.error('\n❌ TEST SUITE FAILED:', error.message);
        console.error('\nPlease verify:');
        console.error('1. Your Partner ID is correct');
        console.error('2. Your Admin Secret is correct');
        console.error('3. You have network connectivity to Kaltura API');
        console.error('4. Your partner account is active\n');
    }
}

// Run the tests
if (require.main === module) {
    runTests();
}

module.exports = {
    kalturaApiCall,
    testSessionStart,
    testListMedia,
    testGetPartner,
    testListCategories
};
