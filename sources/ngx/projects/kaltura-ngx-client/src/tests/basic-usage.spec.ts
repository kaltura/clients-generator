import { KalturaClientOptions } from '../lib/kaltura-client-options';

/**
 * Basic usage tests - demonstrates how to configure and use the Kaltura NGX client
 * These tests verify the client can be configured without runtime errors
 */
describe('Basic Usage Examples', () => {
  describe('Client Configuration', () => {
    it('should create valid client options with minimal config', () => {
      const options: KalturaClientOptions = {
        clientTag: 'my-angular-app',
        endpointUrl: 'https://www.kaltura.com/api_v3'
      };

      expect(options.clientTag).toBe('my-angular-app');
      expect(options.endpointUrl).toBe('https://www.kaltura.com/api_v3');
      expect(options.chunkFileSize).toBeUndefined();
      expect(options.chunkFileDisabled).toBeUndefined();
    });

    it('should create client options with file upload configuration', () => {
      const options: KalturaClientOptions = {
        clientTag: 'my-angular-app',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileSize: 10485760, // 10MB chunks
        chunkFileDisabled: false
      };

      expect(options.chunkFileSize).toBe(10485760);
      expect(options.chunkFileDisabled).toBe(false);
    });

    it('should create client options with chunked uploads disabled', () => {
      const options: KalturaClientOptions = {
        clientTag: 'my-angular-app',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileDisabled: true
      };

      expect(options.chunkFileDisabled).toBe(true);
    });

    it('should accept different endpoint URLs', () => {
      const testCases = [
        'https://www.kaltura.com/api_v3',
        'https://cdnapi.kaltura.com/api_v3',
        'https://your-domain.kaltura.com/api_v3',
        'http://localhost:8080/api_v3'
      ];

      testCases.forEach(url => {
        const options: KalturaClientOptions = {
          clientTag: 'test',
          endpointUrl: url
        };
        expect(options.endpointUrl).toBe(url);
      });
    });
  });

  describe('Client Tag Validation', () => {
    it('should accept valid client tags', () => {
      const validTags = [
        'my-app',
        'my_app',
        'myApp123',
        'test-client-v1.0'
      ];

      validTags.forEach(tag => {
        const options: KalturaClientOptions = {
          clientTag: tag,
          endpointUrl: 'https://www.kaltura.com/api_v3'
        };
        expect(options.clientTag).toBe(tag);
      });
    });
  });

  describe('Chunk Upload Configuration', () => {
    it('should accept chunk sizes in bytes', () => {
      const chunkSizes = [
        { size: 1048576, description: '1MB' },
        { size: 5242880, description: '5MB' },
        { size: 10485760, description: '10MB' },
        { size: 52428800, description: '50MB' }
      ];

      chunkSizes.forEach(({ size }) => {
        const options: KalturaClientOptions = {
          clientTag: 'test',
          endpointUrl: 'https://www.kaltura.com/api_v3',
          chunkFileSize: size
        };
        expect(options.chunkFileSize).toBe(size);
      });
    });
  });
});
