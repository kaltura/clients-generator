import { setupZoneTestEnv } from 'jest-preset-angular/setup-env/zone';

setupZoneTestEnv();

// Set default timeout for async operations
jest.setTimeout(60000);
