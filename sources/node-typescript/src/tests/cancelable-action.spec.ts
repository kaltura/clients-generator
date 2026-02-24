import { CancelableAction } from '../cancelable-action';

/**
 * Tests for CancelableAction class
 * This is a Promise-like class that supports cancellation
 */
describe('CancelableAction', () => {
  describe('Static Methods', () => {
    it('should create resolved CancelableAction', (done) => {
      const value = 'test-value';
      const action = CancelableAction.resolve(value);

      action.then((result) => {
        expect(result).toBe(value);
        done();
      }, () => {});
    });

    it('should create rejected CancelableAction', (done) => {
      const error = new Error('test-error');
      const action = CancelableAction.reject<string>(error);

      action.then(
        () => done.fail('Should not resolve'),
        (err) => {
          expect(err).toBe(error);
          done();
        }
      );
    });
  });

  describe('Promise-like Behavior', () => {
    it('should resolve with value', (done) => {
      const action = new CancelableAction<number>((resolve) => {
        resolve(42);
      });

      action.then((value) => {
        expect(value).toBe(42);
        done();
      }, () => {});
    });

    it('should reject with error', (done) => {
      const testError = new Error('test-error');
      const action = new CancelableAction<number>((resolve, reject) => {
        reject(testError);
      });

      action.then(
        () => done.fail('Should not resolve'),
        (error) => {
          expect(error).toBe(testError);
          done();
        }
      );
    });

    it('should support async resolution', (done) => {
      const action = new CancelableAction<string>((resolve) => {
        setTimeout(() => resolve('delayed'), 10);
      });

      action.then((value) => {
        expect(value).toBe('delayed');
        done();
      }, () => {});
    });
  });

  describe('Cancellation', () => {
    it('should support cancel function', (done) => {
      let wasCancelled = false;

      const action = new CancelableAction<number>((resolve) => {
        const timeout = setTimeout(() => resolve(42), 100);

        return () => {
          clearTimeout(timeout);
          wasCancelled = true;
        };
      });

      action.cancel();

      setTimeout(() => {
        expect(wasCancelled).toBe(true);
        done();
      }, 50);
    });

    it('should handle cancellation without cancel function', () => {
      const action = new CancelableAction<number>((resolve) => {
        resolve(42);
        // No cancel function returned
      });

      // Should not throw
      expect(() => action.cancel()).not.toThrow();
    });
  });

  describe('Type Safety', () => {
    it('should maintain type information', (done) => {
      const action = CancelableAction.resolve<number>(10);

      action.then((num: number) => {
        expect(typeof num).toBe('number');
        expect(num).toBe(10);
        done();
      }, () => {});
    });

    it('should support generic types', (done) => {
      interface TestData {
        id: number;
        name: string;
      }

      const data: TestData = { id: 1, name: 'test' };
      const action = CancelableAction.resolve<TestData>(data);

      action.then((result) => {
        expect(result.id).toBe(1);
        expect(result.name).toBe('test');
        done();
      }, () => {});
    });
  });

  describe('Edge Cases', () => {
    it('should handle immediate resolution', (done) => {
      const action = new CancelableAction<string>((resolve) => {
        resolve('immediate');
      });

      action.then((value) => {
        expect(value).toBe('immediate');
        done();
      }, () => {});
    });

    it('should handle null values', (done) => {
      const action = CancelableAction.resolve<null>(null);

      action.then((value) => {
        expect(value).toBeNull();
        done();
      }, () => {});
    });

    it('should handle undefined values', (done) => {
      const action = CancelableAction.resolve<undefined>(undefined);

      action.then((value) => {
        expect(value).toBeUndefined();
        done();
      }, () => {});
    });
  });
});
