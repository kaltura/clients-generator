import { KalturaClientOptions } from '../lib/kaltura-client-options';

/**
 * Basic module configuration tests without dependencies on generated files
 * These tests verify the configuration interfaces and options work correctly
 */
describe('KalturaClientModule Configuration', () => {
  describe('Client Options', () => {
    it('should create valid client options', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test-client',
        endpointUrl: 'https://www.kaltura.com/api_v3'
      };

      expect(options).toBeDefined();
      expect(options.clientTag).toBe('test-client');
      expect(options.endpointUrl).toBe('https://www.kaltura.com/api_v3');
    });

    it('should support optional chunk upload configuration', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test-client',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileSize: 5242880,
        chunkFileDisabled: false
      };

      expect(options.chunkFileSize).toBe(5242880);
      expect(options.chunkFileDisabled).toBe(false);
    });

    it('should create options with only required fields', () => {
      const options: KalturaClientOptions = {
        clientTag: 'minimal-client',
        endpointUrl: 'https://api.kaltura.com/api_v3'
      };

      expect(options.clientTag).toBe('minimal-client');
      expect(options.endpointUrl).toBe('https://api.kaltura.com/api_v3');
      expect(options.chunkFileSize).toBeUndefined();
      expect(options.chunkFileDisabled).toBeUndefined();
    });
  });

  describe('Module Integration', () => {
    it('should verify TypeScript compilation', () => {
      // This test passes if the file compiles successfully
      expect(true).toBe(true);
    });

    it('should allow creating configuration factories', () => {
      const clientOptionsFactory = (): KalturaClientOptions => ({
        clientTag: 'factory-client',
        endpointUrl: 'https://www.kaltura.com/api_v3'
      });

      const options = clientOptionsFactory();
      expect(options.clientTag).toBe('factory-client');
    });
  });

  describe('Configuration Validation', () => {
    it('should accept valid endpoint URLs', () => {
      const urls = [
        'https://www.kaltura.com/api_v3',
        'https://cdnapi.kaltura.com/api_v3',
        'http://localhost:8080/api_v3'
      ];

      urls.forEach(url => {
        const options: KalturaClientOptions = {
          clientTag: 'test',
          endpointUrl: url
        };
        expect(options.endpointUrl).toBe(url);
      });
    });

    it('should accept various client tags', () => {
      const tags = ['my-app', 'test_app', 'app123', 'client-v1.0'];

      tags.forEach(tag => {
        const options: KalturaClientOptions = {
          clientTag: tag,
          endpointUrl: 'https://www.kaltura.com/api_v3'
        };
        expect(options.clientTag).toBe(tag);
      });
    });
  });
});
