package com.kaltura.client.test;

import com.app.DataFactory;
import com.kaltura.client.KalturaClient;
import com.kaltura.client.KalturaRequestConfiguration;
import com.kaltura.client.enums.KalturaAssetReferenceType;
import com.kaltura.client.enums.KalturaAssetType;
import com.kaltura.client.services.KalturaAssetService;
import com.kaltura.client.services.KalturaOttUserService;
import com.kaltura.client.types.KalturaAsset;
import com.kaltura.client.types.KalturaLoginResponse;
import com.kaltura.client.utils.request.ActionsQueue;
import com.kaltura.client.utils.request.KalturaRequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;
import org.junit.Test;

import java.lang.ref.WeakReference;
import java.util.concurrent.Semaphore;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

import static com.kaltura.client.test.ThreadSafeTest.RequestThread.countReps;

/**
 * Created by tehilarozin on 12/09/2016.
 */
public class ThreadSafeTest extends TestCommon {

    final static int MAX_FIRST = 1;//90;
    final static int MAX_SECOND = 0;//40;
    private int counter;

    @Override
    protected void setUp() throws Exception {

        super.setUp();
        ((KalturaRequestConfiguration)kalturaClient.getConnectionConfiguration()).setMaxRetry(0);

        counter = 0;
        countReps = 0;

    }

    @Test
    public void testMultiThreadingRequests() {

        try {
            Thread x = new Thread(new Runnable() {
                @Override
                public void run() {
                    for (int i = 0; i <= MAX_FIRST; i++) {
                        final RequestThread thread = new RequestThread(kalturaClient, actionsQueue);//.delay(Math.abs(ThreadLocalRandom.current().nextLong(1000, 6000) - i));
                        thread.setName("thread_a_" + i);
                        long delay = 0;//ThreadLocalRandom.current().nextLong(0, 2000); //we can set a delay to make atcivation timing unpredictable
                        logger.info("run thread: thread_a_" + i + ", delay = " + delay);
                        thread.run();
                    }
                }
            });
            Thread y = new Thread(new Runnable() {
                @Override
                public void run() {
                    for (int i = 0; i <= MAX_SECOND; i++) {
                        final RequestThread thread = new RequestThread(kalturaClient, actionsQueue);//.delay(Math.abs(ThreadLocalRandom.current().nextLong(1000, 6000) - i));
                        thread.setName("thread_b_" + i);
                        long delay = 0;//ThreadLocalRandom.current().nextLong(0, 2000);
                        logger.info("run thread: thread_b_" + i + ", delay = " + delay);
                        thread.run();
                    }
                }
            });

            x.run();
            y.run();

            while (countReps < MAX_SECOND + MAX_FIRST){
                try {
                    Thread.sleep(2000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }


        } catch (Exception e) {
            logger.error(e.getMessage());
        }

        assertTrue(actionsQueue.isEmpty());
        logger.info("countReps = "+countReps);

    }

    static class RequestThread extends Thread {
        KalturaClient client;
        WeakReference<ActionsQueue> actionsQueueRef;
        ActionsQueue actionsQueue;
        volatile Lock lock = new ReentrantLock();
        Semaphore semaphore = new Semaphore(1);
        private long delay = 0;

        static int countReps = 0;

        RequestThread(KalturaClient client, ActionsQueue queue) {
            this.client = client;
            //this.actionsQueueRef = new WeakReference<ActionsQueue>(queue);
            this.actionsQueue = queue;
        }

        RequestThread delay(long milliDelay) {
            this.delay = milliDelay;
            return this;
        }

        @Override
        public void run() {
            try {

                final DataFactory.UserLogin userLogin = DataFactory.getUser();
                logger.info(RequestThread.this.getName() + ": start login for " + userLogin.username);
                //logger.debug("delay login for "+delay+"milli. "+this.getName());

                KalturaRequestBuilder loginReq = KalturaOttUserService.login(client.getPartnerId(), userLogin.username, userLogin.password).setCompletion(new OnCompletion<GeneralResponse<KalturaLoginResponse>>() {
                    @Override
                    public void onComplete(GeneralResponse<KalturaLoginResponse> response) {
                        logger.debug("onComplete login request [" + response.getRequestId() + "] :\n" + response.toString());

                        if (response.isSuccess()) {
                            logger.info(RequestThread.this.getName() + ": login request success for user " + response.getResult().getUser().getFirstName());

                            client.setKs(response.getResult().getLoginSession().getKs());

                            String reqId = actionsQueue.queue(KalturaAssetService.get(MediaId, KalturaAssetReferenceType.MEDIA).setParam("type", KalturaAssetType.MEDIA.getValue()).setCompletion(new OnCompletion<GeneralResponse<KalturaAsset>>() {
                                @Override
                                public void onComplete(GeneralResponse<KalturaAsset> response) {
                                    if (response.isSuccess()) {
                                        logger.debug(RequestThread.this.getName() + ": success request get asset [" + response.getRequestId() + "] " + response.getResult().getName() + " info\n" + response.getResult().toParams().toString());
                                    }
                                }
                            }).add(KalturaOttUserService.logout().setCompletion(new OnCompletion<GeneralResponse<String>>() {
                                @Override
                                public void onComplete(GeneralResponse<String> response) {
                                    if (response.isSuccess()) {
                                        logger.info(RequestThread.this.getName() + ": logged out from user ");
                                    }
                                    countReps++;

                                }
                            })).build(client));
                        } else {
                            logger.warn(RequestThread.this.getName() + ": login request failed for user " + userLogin.username);
                            countReps++;
                        }

                    }
                });
                actionsQueue.queue(loginReq.build(client));

            } catch (Exception ex){
                logger.error(ex.getMessage());
            }
        }
    }

}
