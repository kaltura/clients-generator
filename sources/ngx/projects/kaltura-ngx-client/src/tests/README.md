# Kaltura NGX Client Tests

This directory contains unit tests for the Kaltura Angular client library.

## Running Tests

### Prerequisites
Ensure all dependencies are installed:
```bash
cd sources/ngx
npm install
```

### Run All Tests
```bash
npm test
```

### Run Tests in Watch Mode
```bash
npm run test:watch
```

### Run Tests for CI
```bash
npm run test:ci
```

## Test Structure

- **setup-jest.ts** - Jest test environment configuration
- **kaltura-client.module.spec.ts** - Tests for the main Angular module
- **kaltura-client-options.spec.ts** - Tests for client configuration options

## Writing Tests

Tests follow Angular testing best practices:
- Use Jest as the test runner
- Use Angular TestBed for component/module testing
- Place test files next to the source files they test (*.spec.ts)

## Test Coverage

The tests verify:
- Module can be imported and configured correctly
- Module throws error when imported twice (preventing double initialization)
- Client options interface works as expected
- Integration with Angular HttpClientModule
- Dependency injection tokens are properly defined
