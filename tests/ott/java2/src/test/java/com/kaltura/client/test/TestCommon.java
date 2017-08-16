import com.kaltura.client.Client;
import com.kaltura.client.Configuration;
import com.kaltura.client.ILogger;
import com.kaltura.client.Logger;

import junit.framework.TestCase;

public abstract class TestCommon extends TestCase {

    protected static ILogger logger = Logger.getLogger("java-test");

    Client client;
    OttTestConfig testConfig;


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

       /* if (logger.isEnabled())
            logger.info("Starting test: " + getClass().getName() + "." + getName());*/

        testConfig = new OttTestConfig();

        Configuration config = new Configuration();
        config.setEndpoint(testConfig.getServiceUrl());
        config.setAcceptGzipEncoding(false);

        client = new Client(config);

    }

}
