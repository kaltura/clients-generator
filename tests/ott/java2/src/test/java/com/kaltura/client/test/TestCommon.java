package com.kaltura.client.test;

import com.app.DataFactory;
import com.kaltura.client.Client;
import com.kaltura.client.Configuration;
import com.kaltura.client.ILogger;
import com.kaltura.client.Logger;

import junit.framework.TestCase;

import java.util.concurrent.CompletableFuture;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * Created by tehilarozin on 11/09/2016.
 */
public abstract class TestCommon extends TestCase {

    static final int PartnerId = 198;
    public static final String ENDPOINT = "http://34.249.122.223:8080/v4_4/";
    public static final String UDID = "69f03435-3214-3a5c-9cf7-6bdbc813e8cf";
    public static final String MediaId = "442468"; //avatar2
    public static final String MediaId2 = "258656"; //frozen
    protected static ILogger logger = Logger.getLogger("new-java-test");
    protected static final DataFactory.UserLogin NotExists = new DataFactory.UserLogin("Thomas@gmail.com", "123456");

    Client client;


	private ExecutorService executor;

	@SuppressWarnings("serial")
	protected class CompletionException extends Exception {
		
		public CompletionException(String message) {
			super(message);
		}
		
		@Override
		public String toString() {
			return getMessage();
		}
	}
	
	protected class Completion{
		
		@SuppressWarnings("rawtypes")
		private CompletableFuture future;
		
		@SuppressWarnings("rawtypes")
		public Completion() {
			future = new CompletableFuture();
		}
		
		public void run(Runnable runnable) throws InterruptedException, ExecutionException {
			executor.submit(runnable);
			Exception error = (Exception) future.get();

			if(error != null) {
				throw new AssertionError(error);
			}
		}
		
		@SuppressWarnings("unchecked")
		public void complete() {
			future.complete(null);
		}
		
		@SuppressWarnings("unchecked")
		public void fail(Exception e) {
			future.complete(new CompletionException(e.getMessage()));
			throw new RuntimeException(e);
		}
		
		@SuppressWarnings("unchecked")
		public void fail(String message) {
			future.complete(new CompletionException(message));
			throw new RuntimeException(message);
		}
		
		public void assertTrue(boolean condition) {
			this.assertTrue(condition, null);
		}
		
		public void assertTrue(boolean condition, String message) {
			if(!condition) {
				this.fail(message);
			}
		}
		
		public void assertNull(Exception exception) {
			if(exception == null) {
				return;
			}
			this.assertTrue(false, exception.getMessage());
		}
		
		public void assertNull(Object object) {
			this.assertNull(object, null);
		}
		
		public void assertNull(Object object, String message) {
			this.assertTrue(object == null, message);
		}
		
		public void assertNotNull(Object object, String message) {
			this.assertTrue(object != null, message);
		}
		
		public void assertNotNull(Object object) {
			this.assertNotNull(object, null);
		}

		public void assertEquals(int expected, int actual) {
			this.assertTrue(expected == actual, actual + " is different than " + expected);
		}

		public void assertEquals(long expected, long actual) {
			this.assertTrue(expected == actual, actual + " is different than " + expected);
		}

		public void assertEquals(Object expected, Object actual) {
			this.assertTrue(expected.equals(actual), actual + " is different than " + expected);
		}
	}

    @Override
    protected void setUp() throws Exception {
        super.setUp();

		executor = Executors.newScheduledThreadPool(1);
		
        Configuration config = new Configuration();
        config.setEndpoint(ENDPOINT);
        config.setAcceptGzipEncoding(false);

        client = new Client(config);
    }
}
