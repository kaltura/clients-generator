import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { KalturaClient } from '../lib/kaltura-client.service';
import { KalturaClientModule } from '../lib/kaltura-client.module';
import { KalturaClientOptions, KALTURA_CLIENT_OPTIONS } from '../lib/kaltura-client-options';
import { KalturaClientException } from '../lib/api/kaltura-client-exception';
import { KalturaObjectBase, KalturaObjectMetadata } from '../lib/api/kaltura-object-base';
import { KalturaRequest } from '../lib/api/kaltura-request';
import { KalturaRequestOptions } from '../lib/api/kaltura-request-options';

// Mock test config for unit tests (no actual server needed)
const TestConfig: KalturaClientOptions = {
  endpointUrl: 'https://www.kaltura.com',
  clientTag: 'ngx-modern-tests',
};

// Concrete implementation of KalturaObjectBase for testing
class TestKalturaObject extends KalturaObjectBase {
  constructor(data?: any) {
    super(data);
  }

  protected _getMetadata(): KalturaObjectMetadata {
    const result = super._getMetadata();
    Object.assign(result.properties, {
      objectType: { type: 'c', default: 'TestKalturaObject' },
    });
    return result;
  }
}

// Simple mock request for testing
class MockKalturaRequest extends KalturaRequest<any> {
  constructor(data?: any) {
    super(data, { responseType: 'o', responseSubType: 'TestKalturaObject', responseConstructor: TestKalturaObject });
  }

  protected _getMetadata(): KalturaObjectMetadata {
    const result = super._getMetadata();
    Object.assign(result.properties, {
      service: { type: 'c', default: 'mock' },
      action: { type: 'c', default: 'test' },
    });
    return result;
  }
}

describe('KalturaClient', () => {
  let client: KalturaClient;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        KalturaClientModule.forRoot(() => TestConfig)
      ],
      providers: []
    });

    client = TestBed.inject(KalturaClient);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(client).toBeTruthy();
  });

  it('should have setOptions method', () => {
    expect(typeof client.setOptions).toBe('function');
  });

  it('should have appendOptions method', () => {
    expect(typeof client.appendOptions).toBe('function');
  });

  it('should have setDefaultRequestOptions method', () => {
    expect(typeof client.setDefaultRequestOptions).toBe('function');
  });

  it('should have appendDefaultRequestOptions method', () => {
    expect(typeof client.appendDefaultRequestOptions).toBe('function');
  });

  it('should throw KalturaClientException when setOptions is called with null', () => {
    expect(() => client.setOptions(null as any)).toThrow(KalturaClientException);
  });

  it('should throw KalturaClientException when appendOptions is called with null', () => {
    expect(() => client.appendOptions(null as any)).toThrow(KalturaClientException);
  });

  it('should throw KalturaClientException when setDefaultRequestOptions is called with null', () => {
    expect(() => client.setDefaultRequestOptions(null as any)).toThrow(KalturaClientException);
  });

  it('should throw KalturaClientException when appendDefaultRequestOptions is called with null', () => {
    expect(() => client.appendDefaultRequestOptions(null as any)).toThrow(KalturaClientException);
  });
});

describe('TestKalturaObject', () => {
  it('should be able to create instance', () => {
    const obj = new TestKalturaObject();
    expect(obj).toBeTruthy();
  });

  it('should support setData method chaining', () => {
    const obj = new TestKalturaObject();
    const result = obj.setData((o) => {
      // Configure object
    });
    expect(result).toBe(obj);
  });

  it('should support allowEmptyArray method chaining', () => {
    const obj = new TestKalturaObject();
    const result = obj.allowEmptyArray('testArray');
    expect(result).toBe(obj);
  });
});

describe('KalturaRequestOptions', () => {
  it('should be able to create instance', () => {
    const options = new KalturaRequestOptions();
    expect(options).toBeTruthy();
  });

  it('should initialize acceptedTypes as empty array', () => {
    const options = new KalturaRequestOptions();
    expect(options.acceptedTypes).toEqual([]);
  });

  it('should accept configuration in constructor', () => {
    const options = new KalturaRequestOptions({
      ks: 'test-ks',
      partnerId: 12345
    });
    expect(options.ks).toBe('test-ks');
    expect(options.partnerId).toBe(12345);
  });
});
