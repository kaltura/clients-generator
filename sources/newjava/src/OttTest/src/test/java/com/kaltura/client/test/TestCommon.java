package com.kaltura.client.test;

import com.app.DataFactory;
import com.kaltura.client.*;
import com.kaltura.client.utils.request.ActionsQueue;
import junit.framework.TestCase;

import java.util.concurrent.CountDownLatch;

/**
 * Created by tehilarozin on 11/09/2016.
 */
public abstract class TestCommon extends TestCase {

    static final int PartnerId = 198;
    public static final String ENDPOINT = "http://52.210.223.65:8080/v4_0/";
    public static final String UDID = "69f03435-3214-3a5c-9cf7-6bdbc813e8cf";
    public static final String MediaId = "442468"; //avatar2
    public static final String MediaId2 = "258656"; //frozen
    protected static IKalturaLogger logger = KalturaLogger.getLogger("new-java-test");
    protected static final DataFactory.UserLogin NotExists = new DataFactory.UserLogin("Thomas@gmail.com", "123456");

    KalturaClient kalturaClient;
    ActionsQueue actionsQueue;
    CountDownLatch countDown;

    @Override
    protected void setUp() throws Exception {
        super.setUp();

        KalturaRequestConfiguration config = new KalturaRequestConfiguration();
        config.setEndpoint(ENDPOINT);
        config.setAcceptGzipEncoding(false);

        // clientTag defaults to lib generation date, clientApiVersion is taken from the generation xml input file
        kalturaClient = new KalturaClient(config); // clientTag and clientApiVersion are auto set here
        kalturaClient.setPartnerId(PartnerId);

        actionsQueue = new APIOkRequestsExecutor();
    }

    @Override
    protected void tearDown() throws Exception {
        super.tearDown();

        actionsQueue.clearActions();
    }

    protected void resume(){
        countDown.countDown();
    }

    protected synchronized void wait(int count){
        countDown = new CountDownLatch(count);
        try {
            countDown.await();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        /*while(waitFor > 0) {
            try {
                Thread.sleep(sleepTime);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }*/

    }
}
