package com.kaltura.client.test;

import com.app.DataFactory;
import com.kaltura.client.Client;
import com.kaltura.client.Configuration;
import com.kaltura.client.ILogger;
import com.kaltura.client.Logger;

import junit.framework.TestCase;

/**
 * Created by tehilarozin on 11/09/2016.
 */
public abstract class TestCommon extends TestCase {

    static final int PartnerId = 100;
    public static final String ENDPOINT = "http://test.com/v4_4/";
    public static final String MediaId = "442468";
    protected static ILogger logger = Logger.getLogger("java-test");

    Client client;

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

	@Override
    protected void setUp() throws Exception {
        super.setUp();
		
        Configuration config = new Configuration();
        config.setEndpoint(ENDPOINT);
        config.setAcceptGzipEncoding(false);

        client = new Client(config);
    }
}
