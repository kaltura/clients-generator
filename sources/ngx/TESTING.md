# NGX Client Testing Guide

## Overview
This guide explains how to test the Kaltura Angular (NGX) client after the Node.js security update.

## Security Update Applied
All Node.js client packages have been updated to require Node.js >= 20.20.0 to address the async_hooks vulnerability (CVE affecting Node.js 8.x through 25.x below patched versions).

## Prerequisites

### 1. Install Node.js 20.20.0 or higher
```bash
# Check your Node.js version
node --version

# Should show v20.20.0 or higher
# If not, install/upgrade Node.js from https://nodejs.org/
```

### 2. Install Dependencies
```bash
cd sources/ngx
npm install
```

Note: If you encounter errors about Node.js version, ensure you're using Node.js >= 20.20.0.

## Running Tests

### Run All Tests
```bash
cd sources/ngx
npm test
```

### Run Tests in Watch Mode (for development)
```bash
npm run test:watch
```

### Run Tests for CI/CD
```bash
npm run test:ci
```

### Run Specific Test File
```bash
npx jest projects/kaltura-ngx-client/src/tests/kaltura-client.module.spec.ts
```

## Test Suite Overview

### 1. Module Tests (`kaltura-client.module.spec.ts`)
Tests the main `KalturaClientModule` Angular module:
- Module can be created
- Module prevents double initialization
- Module can be configured with `forRoot()`
- Module integrates with Angular TestBed

### 2. Client Options Tests (`kaltura-client-options.spec.ts`)
Tests the client configuration interface:
- Required properties (clientTag, endpointUrl)
- Optional properties (chunkFileSize, chunkFileDisabled)
- Injection token is properly defined

### 3. Basic Usage Tests (`basic-usage.spec.ts`)
Demonstrates common usage patterns:
- Minimal configuration
- File upload configuration
- Different endpoint URLs
- Chunk upload settings

## Expected Test Results

When all tests pass, you should see output similar to:
```
PASS  projects/kaltura-ngx-client/src/tests/kaltura-client.module.spec.ts
PASS  projects/kaltura-ngx-client/src/tests/kaltura-client-options.spec.ts
PASS  projects/kaltura-ngx-client/src/tests/basic-usage.spec.ts

Test Suites: 3 passed, 3 total
Tests:       15+ passed, 15+ total
```

## Troubleshooting

### Error: "Unsupported engine"
**Cause**: Node.js version is below 20.20.0

**Solution**: Upgrade Node.js to version 20.20.0 or higher

### Error: "Cannot find module '@angular/core'"
**Cause**: Dependencies not installed

**Solution**:
```bash
cd sources/ngx
npm install
```

### Error: "setupTestFrameworkScriptFile" not found
**Cause**: This is a deprecation warning in newer Jest versions (can be ignored)

**Note**: Tests will still run successfully. For future updates, consider migrating to `setupFilesAfterEnv` in package.json.

### Tests fail with "zone.js" errors
**Cause**: Missing zone.js setup

**Solution**: Ensure `setup-jest.ts` is properly configured (it should be already)

## Test Coverage

Current test coverage includes:
- ✅ Module initialization and configuration
- ✅ Client options interface validation
- ✅ Dependency injection tokens
- ✅ Angular TestBed integration
- ✅ Basic usage patterns

## Next Steps

### Adding More Tests
To add more tests, create new `*.spec.ts` files in the `tests/` directory or next to the source files you want to test.

### Integration Tests
For full integration tests with actual Kaltura API calls, see the test examples in `/tests/ovp/` directory.

## Continuous Integration

The tests are configured to run automatically in CI/CD pipelines. Ensure your CI environment uses Node.js >= 20.20.0.

Example GitHub Actions configuration:
```yaml
- uses: actions/setup-node@v4
  with:
    node-version: '20.20.0'
```

## Questions or Issues?

If you encounter issues with the tests, check:
1. Node.js version is >= 20.20.0
2. All dependencies are installed (`npm install`)
3. No conflicting global packages
4. Jest configuration in package.json is correct
