import { KalturaClientOptions, KALTURA_CLIENT_OPTIONS } from '../lib/kaltura-client-options';

describe('KalturaClientOptions', () => {
  describe('Interface', () => {
    it('should define required properties', () => {
      const options: KalturaClientOptions = {
        clientTag: 'my-app',
        endpointUrl: 'https://www.kaltura.com/api_v3'
      };

      expect(options.clientTag).toBe('my-app');
      expect(options.endpointUrl).toBe('https://www.kaltura.com/api_v3');
    });

    it('should allow optional chunkFileSize property', () => {
      const options: KalturaClientOptions = {
        clientTag: 'my-app',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileSize: 5242880 // 5MB
      };

      expect(options.chunkFileSize).toBe(5242880);
    });

    it('should allow optional chunkFileDisabled property', () => {
      const options: KalturaClientOptions = {
        clientTag: 'my-app',
        endpointUrl: 'https://www.kaltura.com/api_v3',
        chunkFileDisabled: true
      };

      expect(options.chunkFileDisabled).toBe(true);
    });
  });

  describe('InjectionToken', () => {
    it('should provide KALTURA_CLIENT_OPTIONS token', () => {
      expect(KALTURA_CLIENT_OPTIONS).toBeDefined();
      expect(KALTURA_CLIENT_OPTIONS.toString()).toContain('kaltura client options');
    });
  });
});
