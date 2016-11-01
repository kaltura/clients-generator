package com.app;

import com.kaltura.client.*;
import com.kaltura.client.enums.*;
import com.kaltura.client.services.KalturaAssetService;
import com.kaltura.client.services.KalturaFavoriteService;
import com.kaltura.client.services.KalturaHouseholdDeviceService;
import com.kaltura.client.services.KalturaOttUserService;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.request.KalturaMultiRequestBuilder;
import com.kaltura.client.utils.request.KalturaRequestBuilder;
import com.kaltura.client.utils.request.ActionsQueue;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;
import com.kaltura.client.utils.response.base.MultiResponse;
import com.kaltura.client.utils.response.base.ResponseParser;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.Semaphore;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

/**
 * Created by tehilarozin on 20/07/2016.
 *
 * mixed tests - tests were moved to junit tests classes - should not be copied to test project
 */
public class CLibTest {

    static final int PartnerId = 198;
    public static final String ENDPOINT = "http://54.154.20.171:8080/v4_0/";
    public static final String TAG = "CLibTest";
    public static final String UDID = "69f03435-3214-3a5c-9cf7-6bdbc813e8cf";
    //public static final String MediaId = "440018"; //avatar2
    public static final String MediaId = "442468"; //avatar2
    public static final String MediaId2 = "258656"; //frozen
    private static IKalturaLogger logger = KalturaLogger.getLogger(TAG);

    static KalturaClient kalturaClient;
    static ActionsQueue actionsQueue;

    public static void main(String[] args) {

        try {
            //this point in the app is after the configuration were loaded from the DMS
            KalturaRequestConfiguration cConfig = new KalturaRequestConfiguration();
            cConfig.setEndpoint(ENDPOINT);
            cConfig.setAcceptGzipEncoding(false);

            // clientTag defaults to lib generation date, clientApiVersion is taken from the generation xml input file
            kalturaClient = new KalturaClient(cConfig); // clientTag and clientApiVersion are auto set here
            kalturaClient.setPartnerId(PartnerId);

            actionsQueue = new APIOkRequestsExecutor();//AppInjector.getActionsQueue();

            testMultiThreading();

            //runMockTest();

            //testCancelRequest(actionsQueue, kalturaClient);

            //testLoginFlow(actionsQueue, kalturaClient);

            //testMultiRequest(actionsQueue, kalturaClient);

            //testGetList(actionsQueue, kalturaClient);

            //actionsQueue.clearActions();


            //logger.debug("actionsQueue " + (actionsQueue.isEmpty() ? "is empty" : "has pending actions"));

            /*try {
                    new APIOkRequestsExecutor(kConfig).execute(multiActionRequest);FI9816P
                } catch (IOException e) {
                    e.printStackTrace();
                }
                System.exit(0);*/

        } catch (Exception e) {
            e.printStackTrace();
        }


        while (true) {
            try {
                Thread.currentThread().sleep(200000);
            } catch (InterruptedException e) {
            }
        }

    }

    private static void testCancelRequest(ActionsQueue actionsQueue, KalturaClient kalturaClient) {
        logger.info("testCancelRequest");

        String reqId = actionsQueue.queue(KalturaFavoriteService.list().setCompletion(new OnCompletion<GeneralResponse<KalturaFavoriteListResponse>>() {
            @Override
            public void onComplete(GeneralResponse<KalturaFavoriteListResponse> response) {
                if (response.isSuccess()) {
                    logger.error("I should have been canceled");
                    if (response.getResult().getTotalCount() > 0) {
                        logger.debug("favorites objects: " + response.getResult().getObjects().size());
                    }
                }
            }
        }).build(kalturaClient));
        actionsQueue.cancelAction(reqId);
    }

    private static void testMultiRequest(final ActionsQueue actionsQueue, final KalturaClient kalturaClient) {
        logger.info("testMultiRequest\n");

        KalturaRequestBuilder loginReq = KalturaOttUserService.login(PartnerId, "albert@gmail.com", "123456").setCompletion(new OnCompletion<GeneralResponse<KalturaLoginResponse>>() {
            @Override
            public void onComplete(GeneralResponse<KalturaLoginResponse> response) {
                logger.debug("onComplete login request [" + response.getRequestId() + "] :\n" + response.toString());
                if (response.isSuccess()) {
                    kalturaClient.setKs(response.getResult().getLoginSession().getKs());

                    String reqId = actionsQueue.queue(KalturaAssetService.get(MediaId, KalturaAssetReferenceType.MEDIA).setCompletion(new OnCompletion<GeneralResponse<KalturaAsset>>() {
                        @Override
                        public void onComplete(GeneralResponse<KalturaAsset> response) {
                            if (response.isSuccess()) {
                                logger.debug("onComplete request get asset [" + response.getRequestId() + "] " + response.getResult().getName() + " info\n" + response.getResult().toParams().toString());
                            }
                            //pingEnd();
                        }
                    }).build(kalturaClient));
                } else {
                    logger.error("Failed on testMultiRequest: loginReq failed");
                }

            }
        });

        KalturaRequestBuilder userInfoReq = KalturaOttUserService.get().setCompletion(new OnCompletion() {
            @Override
            public void onComplete(Object response) {
                logger.debug("onComplete request get user info:\n" + response.toString());
                //pingEnd();
            }
        });

        KalturaMultiRequestBuilder kalturaMultiRequestBuilder = new KalturaMultiRequestBuilder(loginReq, userInfoReq).link(loginReq, userInfoReq, "loginSession.ks", "ks")
                .setCompletion(
                        new OnCompletion<GeneralResponse<MultiResponse>>() {
                            @Override
                            public void onComplete(GeneralResponse<MultiResponse> response) {
                                logger.debug("onComplete multirequest ["+response.getRequestId()+"] - one total completion  \n" + response.toString());
                            }
                        }).add(new KalturaMultiRequestBuilder(KalturaAssetService.get(MediaId2, KalturaAssetReferenceType.MEDIA).setParam("ks", "1:result:loginSession:ks")));

        //logger.debug("creating multirequest separate completions: login(->assetInfo) + userInfo");
        String mreqId = actionsQueue.queue(kalturaMultiRequestBuilder.build(kalturaClient));
        logger.debug("creating multirequest [" + mreqId + "] one completion: login + userInfo + assetInfo");

    }

    private static void testLoginFlow(final ActionsQueue actionsQueue, final KalturaClient kalturaClient) throws KalturaAPIException {
        logger.info("testLoginFlow\n\n");

        final KalturaRequestBuilder login = KalturaOttUserService.login(kalturaClient.getPartnerId(), "albert@gmail.com", "123456", null, UDID)
                .setCompletion(new OnCompletion<GeneralResponse<KalturaLoginResponse>>() {
                    @Override
                    public void onComplete(GeneralResponse<KalturaLoginResponse> response) {
                        if (response.isSuccess()) {
                            // once set will be inserted automatically in all further requests.
                            kalturaClient.setKs(response.getResult().getLoginSession().getKs());

                            KalturaOTTUser user = response.getResult().getUser();
                            logger.debug("Hello " + user.getFirstName() + " " + user.getLastName() + ", username: " + user.getUsername() + ", ");
                            if (user.getSuspentionState() == KalturaHouseholdSuspentionState.SUSPENDED) {
                                logger.warn("user is suspended!");
                            } else if (user.getUserState() == KalturaUserState.USER_NOT_ACTIVATED) {
                                logger.warn("user was not activated!");
                            } else {
                                actionsQueue.queue(KalturaHouseholdDeviceService.get().setCompletion(new OnCompletion<GeneralResponse<KalturaHouseholdDevice>>() {
                                    @Override
                                    public void onComplete(GeneralResponse<KalturaHouseholdDevice> response) {
                                        if (response.isSuccess()) {
                                            if (response.getResult().getStatus() == KalturaDeviceStatus.NOT_ACTIVATED) {
                                                logger.warn("device was not activated!");
                                            }
                                        }
                                    }
                                }).build(kalturaClient));
                            }
                            testGetList(actionsQueue, kalturaClient);

                        }

                    }
                });
        String id = actionsQueue.queue(login.build(kalturaClient));
        logger.debug("queued request call for login(->householdDevice): " + id);

        /*KalturaHousehold household = new KalturaHousehold();
        household.setName("MyTestedHouse");
        household.setDevicesLimit(50);
        household.setUsersLimit(3);

        KalturaHouseholdService.add(household).setCompletion(new OnCompletion<GeneralResponse<KalturaHousehold>>() {
            @Override
            public void onComplete(GeneralResponse<KalturaHousehold> response) {
                if(response.isSuccess()){

                }
            }
        });*/

        // final KalturaRequestBuilder refreshToken = KalturaHouseholdService.


    }


    public static void testGetList(ActionsQueue actionsQueue, KalturaClient kalturaClient) {
        String reqId = actionsQueue.queue(KalturaAssetService.list().setCompletion(new OnCompletion<GeneralResponse<KalturaAssetListResponse>>() {
            @Override
            public void onComplete(GeneralResponse<KalturaAssetListResponse> response) {
                if (response.isSuccess()) {
                    logger.debug("onComplete request list assets [" + response.getRequestId() + "] " + response.getResult().getTotalCount() + " count\n" + response.getResult().toParams().toString());
                }
                //pingEnd();
            }
        }).build(kalturaClient));
    }


    public static void testMultiThreading(){
        final ScheduledExecutorService threadPool = Executors.newScheduledThreadPool(10);
        ((KalturaRequestConfiguration)kalturaClient.getConnectionConfiguration()).setMaxRetry(0);
        try {
             Thread x = new Thread(new Runnable() {
                @Override
                public void run() {
                    for (int i = 0; i <= 40; i++) {
                        final RequestThread thread = new RequestThread(kalturaClient);//.delay(Math.abs(ThreadLocalRandom.current().nextLong(1000, 6000) - i));
                        thread.setName("thread_a_" + i);
                        long delay = 0;//ThreadLocalRandom.current().nextLong(0, 2000);
                        logger.info("run thread: thread_a_" + i+", delay = "+delay);
                        thread.run();
                        //threadPool.schedule(thread, delay, TimeUnit.MILLISECONDS);
                    }
                }
            });
            Thread y = new Thread(new Runnable() {
                @Override
                public void run() {
                    for (int i = 0; i <= 90; i++) {
                        final RequestThread thread = new RequestThread(kalturaClient);//.delay(Math.abs(ThreadLocalRandom.current().nextLong(1000, 6000) - i));
                        thread.setName("thread_b_" + i);
                        long delay = 0;//ThreadLocalRandom.current().nextLong(0, 2000);
                        logger.info("run thread: thread_b_" + i+", delay = "+delay);
                        thread.run();
                        //threadPool.schedule(thread, delay, TimeUnit.MILLISECONDS);
                    }
                }
            });

            y.run();
            x.run();
        } finally {
            //threadPool.shutdownNow();
        }

    }



    public static void runMockTest() {

        java.lang.String mockFile = "/Users/tehilarozin/assetListRes.txt";
        ResponseParser.parseResponseFile(mockFile, new OnCompletion<GeneralResponse<KalturaAssetListResponse>>() {
            @Override
            public void onComplete(GeneralResponse<KalturaAssetListResponse> response) {
                if (response.isSuccess()) {
                    logger.debug("onComplete request list assets [" + response.getRequestId() + "] " + response.getResult().getTotalCount() + " count\n" + response.getResult().toParams().toString());
                }
            }
        });
    }

    static int countPings = 0;

    private static void pingEnd() {
        countPings++;
        if (countPings >= 2) {
            System.exit(0);
        }
    }


    static class RequestThread extends Thread {
        KalturaClient client;
        volatile Lock lock = new ReentrantLock();
        Semaphore semaphore = new Semaphore(1);
        private long delay = 0;
static int countRefs;
        RequestThread(KalturaClient client){
            this.client = client;
        }

        RequestThread delay(long milliDelay){
            this.delay = milliDelay;
            return this;
        }

        @Override
        public void run() {
            try {

                final DataFactory.UserLogin userLogin = DataFactory.getUser();
                logger.info(RequestThread.this.getName() + ": start login for "+userLogin.username);
                //logger.debug("delay login for "+delay+"milli. "+this.getName());

               // synchronized (semaphore){/*Thread.currentThread()*/semaphore.wait(delay);}
                //logger.debug("delay login for "+delay+"milli. "+this.getName());
                //semaphore.acquire();// lock.lockInterruptibly();

                /*final KalturaRequestBuilder login = KalturaOttUserService.login(kalturaClient.getPartnerId(), userLogin.username, userLogin.password, null, UDID)
                        .setCompletion(new OnCompletion<GeneralResponse<KalturaLoginResponse>>() {
                            @Override
                            public void onComplete(GeneralResponse<KalturaLoginResponse> response) {
                                if (response.isSuccess()) {
                                    logger.info(RequestThread.this.getName() + ": login request success for user "+response.getResult().getUser().getFirstName());
                                    kalturaClient.setKs(response.getResult().getLoginSession().getKs());

                                    testGetList(actionsQueue, client);

                                } else {
                                    logger.warn(RequestThread.this.getName() + ": login request failed for user "+userLogin.username);
                                }
                              //  semaphore.release(); //lock.unlock();
                            }
                        });*/
                //logger.info(RequestThread.this.getName() + " queue request");

                //actionsQueue.queue(login.build(client));


                KalturaRequestBuilder loginReq = KalturaOttUserService.login(kalturaClient.getPartnerId(), userLogin.username, userLogin.password).setCompletion(new OnCompletion<GeneralResponse<KalturaLoginResponse>>() {
                    @Override
                    public void onComplete(GeneralResponse<KalturaLoginResponse> response) {
                        //logger.debug("onComplete login request [" + response.getRequestId() + "] :\n" + response.toString());
                        if (response.isSuccess()) {

                            kalturaClient.setKs(response.getResult().getLoginSession().getKs());
                            logger.info(RequestThread.this.getName() + ": login request success for user "+response.getResult().getUser().getFirstName()+", countRefs= "+countRefs);



                            String reqId = actionsQueue.queue(KalturaAssetService.get(MediaId, KalturaAssetReferenceType.MEDIA).setParam("type", KalturaAssetType.MEDIA.getValue()).setCompletion(new OnCompletion<GeneralResponse<KalturaAsset>>() {
                                @Override
                                public void onComplete(GeneralResponse<KalturaAsset> response) {
                                    if (response.isSuccess()) {
                                        logger.debug(RequestThread.this.getName() + ": success request get asset [" + response.getRequestId() + "] " + response.getResult().getName() + " info\n" + response.getResult().toParams().toString());
                                    }
                                    //pingEnd();
                                }
                            }).add(KalturaOttUserService.logout().setCompletion(new OnCompletion<GeneralResponse<String>>() {
                                @Override
                                public void onComplete(GeneralResponse<String> response) {
                                    countRefs++;
                                    if(response.isSuccess()){
                                        logger.info(RequestThread.this.getName() + ": logged out from user ");
                                    }

                                }
                            })).build(client));
                        } else {
                            countRefs++;
                            logger.warn(RequestThread.this.getName() + ": login request failed for user "+userLogin.username);
                        }

                    }
                });
                actionsQueue.queue(loginReq.build(client));



                //logger.info(RequestThread.this.getName() + "end");

               // semaphore.acquire(); //lock.lock();
            } /*catch ( InterruptedException e) {
                e.printStackTrace();
            }*/ finally {
                //Thread.currentThread().interrupt();
            }
        }
    }

}

