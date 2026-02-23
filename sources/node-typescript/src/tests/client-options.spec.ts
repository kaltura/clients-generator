import { KalturaClientOptions } from '../kaltura-client-options';

/**
 * Tests for KalturaClientOptions interface
 * Verifies client configuration structure
 */
describe('KalturaClientOptions', () => {
  describe('Required Properties', () => {
    it('should define required clientTag property', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test-client',
        endpointUrl: 'https://www.kaltura.com/api_v3'
      };

      expect(options.clientTag).toBe('test-client');
    });

    it('should define required endpointUrl property', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test-client',
        endpointUrl: 'https://www.kaltura.com/api_v3'
      };

      expect(options.endpointUrl).toBe('https://www.kaltura.com/api_v3');
    });
  });

  describe('Optional Properties', () => {
    it('should support optional chunkFileSize property', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test-client',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileSize: 5242880 // 5MB
      };

      expect(options.chunkFileSize).toBe(5242880);
    });

    it('should support optional chunkFileDisabled property', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test-client',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileDisabled: true
      };

      expect(options.chunkFileDisabled).toBe(true);
    });

    it('should allow omitting optional properties', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test-client',
        endpointUrl: 'https://www.kaltura.com/api_v3'
      };

      expect(options.chunkFileSize).toBeUndefined();
      expect(options.chunkFileDisabled).toBeUndefined();
    });
  });

  describe('Configuration Variations', () => {
    it('should accept various client tags', () => {
      const tags = ['my-app', 'test_app', 'app-v1.0', 'MyApp123'];

      tags.forEach((tag) => {
        const options: KalturaClientOptions = {
          clientTag: tag,
          endpointUrl: 'https://www.kaltura.com/api_v3'
        };

        expect(options.clientTag).toBe(tag);
      });
    });

    it('should accept different endpoint URLs', () => {
      const urls = [
        'https://www.kaltura.com/api_v3',
        'https://cdnapi.kaltura.com/api_v3',
        'https://your-domain.kaltura.com/api_v3',
        'http://localhost:8080/api_v3'
      ];

      urls.forEach((url) => {
        const options: KalturaClientOptions = {
          clientTag: 'test',
          endpointUrl: url
        };

        expect(options.endpointUrl).toBe(url);
      });
    });

    it('should handle various chunk sizes', () => {
      const sizes = [
        1048576, // 1MB
        5242880, // 5MB
        10485760, // 10MB
        52428800 // 50MB
      ];

      sizes.forEach((size) => {
        const options: KalturaClientOptions = {
          clientTag: 'test',
          endpointUrl: 'https://www.kaltura.com/api_v3',
          chunkFileSize: size
        };

        expect(options.chunkFileSize).toBe(size);
      });
    });
  });

  describe('Chunk Upload Configuration', () => {
    it('should enable chunked uploads with size', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileSize: 10485760,
        chunkFileDisabled: false
      };

      expect(options.chunkFileSize).toBe(10485760);
      expect(options.chunkFileDisabled).toBe(false);
    });

    it('should disable chunked uploads', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileDisabled: true
      };

      expect(options.chunkFileDisabled).toBe(true);
    });

    it('should support chunking with custom size', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileSize: 2097152 // 2MB chunks
      };

      expect(options.chunkFileSize).toBe(2097152);
      // chunkFileDisabled not set, should be undefined
      expect(options.chunkFileDisabled).toBeUndefined();
    });
  });

  describe('TypeScript Type Safety', () => {
    it('should enforce required properties at compile time', () => {
      // This test verifies TypeScript compilation
      // If this compiles, the type system is working

      const options: KalturaClientOptions = {
        clientTag: 'test',
        endpointUrl: 'https://www.kaltura.com/api_v3'
      };

      expect(options).toBeDefined();
    });

    it('should allow additional properties with correct types', () => {
      const options: KalturaClientOptions = {
        clientTag: 'test',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileSize: 5242880,
        chunkFileDisabled: false
      };

      // TypeScript ensures these are the correct types
      const size: number | undefined = options.chunkFileSize;
      const disabled: boolean | undefined = options.chunkFileDisabled;

      expect(typeof size).toBe('number');
      expect(typeof disabled).toBe('boolean');
    });
  });

  describe('Configuration Factory Pattern', () => {
    it('should support factory function pattern', () => {
      const createConfig = (
        tag: string,
        url: string
      ): KalturaClientOptions => ({
        clientTag: tag,
        endpointUrl: url
      });

      const config = createConfig('factory-client', 'https://www.kaltura.com/api_v3');

      expect(config.clientTag).toBe('factory-client');
      expect(config.endpointUrl).toBe('https://www.kaltura.com/api_v3');
    });

    it('should support configuration builders', () => {
      class ConfigBuilder {
        private options: Partial<KalturaClientOptions> = {};

        clientTag(tag: string): this {
          this.options.clientTag = tag;
          return this;
        }

        endpointUrl(url: string): this {
          this.options.endpointUrl = url;
          return this;
        }

        chunkFileSize(size: number): this {
          this.options.chunkFileSize = size;
          return this;
        }

        build(): KalturaClientOptions {
          if (!this.options.clientTag || !this.options.endpointUrl) {
            throw new Error('Missing required properties');
          }
          return this.options as KalturaClientOptions;
        }
      }

      const config = new ConfigBuilder()
        .clientTag('builder-client')
        .endpointUrl('https://www.kaltura.com/api_v3')
        .chunkFileSize(5242880)
        .build();

      expect(config.clientTag).toBe('builder-client');
      expect(config.chunkFileSize).toBe(5242880);
    });
  });
});
