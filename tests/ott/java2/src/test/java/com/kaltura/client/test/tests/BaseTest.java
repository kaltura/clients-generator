package com.kaltura.client.test.tests;

import com.kaltura.client.Client;
import com.kaltura.client.Configuration;
import com.kaltura.client.services.OttUserService;
import com.kaltura.client.test.TestAPIOkRequestsExecutor;
import com.kaltura.client.test.utils.DBUtils;
import com.kaltura.client.test.utils.IngestUtils;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.response.base.Response;
import org.testng.annotations.BeforeSuite;

import java.util.List;
import java.util.Optional;
import java.util.concurrent.TimeUnit;

import static com.kaltura.client.services.OttUserService.login;
import static com.kaltura.client.test.IngestConstants.CURRENCY_EUR;
import static com.kaltura.client.test.IngestConstants.FIVE_MINUTES_PERIOD;
import static com.kaltura.client.test.Properties.*;
import static com.kaltura.client.test.utils.HouseholdUtils.createHousehold;
import static com.kaltura.client.test.utils.HouseholdUtils.getUsersListFromHouseHold;
import static com.kaltura.client.test.utils.OttUserUtils.getUserById;
import static org.awaitility.Awaitility.setDefaultTimeout;

public class BaseTest {

    public static Client client;
    public static Configuration config;
    public static TestAPIOkRequestsExecutor executor = TestAPIOkRequestsExecutor.getExecutor();

    private static Response<LoginResponse> loginResponse;


    // shared common params
    public static int partnerId;
    public static String defaultUserPassword;

    // shared ks's
    private static String administratorKs, operatorKs, managerKs, anonymousKs;

    // shared VOD
    private static MediaAsset mediaAsset;

    // shared files
    private static MediaFile webMediaFile;
    private static MediaFile mobileMediaFile;

    // shared MPP
    private static Subscription fiveMinRenewableSubscription;

    /*================================================================================
    testing shared params list - used as a helper common params across tests

    int partnerId
    String defaultUserPassword

    String administratorKs
    String operatorKs
    String managerKs
    String anonymousKs

    MediaAsset mediaAsset

    MediaFile webMediaFile
    MediaFile mobileMediaFile

    Subscription fiveMinRenewableSubscription

    Household sharedHousehold
    HouseholdUser sharedMasterUser
    HouseholdUser sharedUser
    String sharedMasterUserKs
    String sharedUserKs
    ================================================================================*/


    @BeforeSuite
    public void base_test_before_suite() {
        // set configuration
        config = new Configuration();
        config.setEndpoint(getProperty(API_BASE_URL) + "/" + getProperty(API_VERSION));
        config.setAcceptGzipEncoding(false);

        // set client
        client = new Client(config);

        // set default awaitility timeout
        setDefaultTimeout(30, TimeUnit.SECONDS);

        // set shared common params
        partnerId = Integer.parseInt(getProperty(PARTNER_ID));
        defaultUserPassword = getProperty(DEFAULT_USER_PASSWORD);
    }

    // getters for shared params
    public static String getAdministratorKs() {
        if (administratorKs == null) {
            String[] userInfo = DBUtils.getUserDataByRole("Administrator").split(":");
            loginResponse = executor.executeSync(login(partnerId, userInfo[0], userInfo[1],
                    null,null));
            administratorKs = loginResponse.results.getLoginSession().getKs();
        }
        return administratorKs;
    }

    public static String getOperatorKs() {
        if (operatorKs == null) {
            String[] userInfo = DBUtils.getUserDataByRole("Operator").split(":");
            loginResponse = executor.executeSync(login(partnerId, userInfo[0], userInfo[1],
                    null,null));
            operatorKs = loginResponse.results.getLoginSession().getKs();
        }
        return operatorKs;
    }

    public static String getManagerKs() {
        if (managerKs == null) {
            String[] userInfo = DBUtils.getUserDataByRole("Manager").split(":");
            loginResponse = executor.executeSync(login(partnerId, userInfo[0], userInfo[1],
                    null,null));
            managerKs = loginResponse.results.getLoginSession().getKs();
        }
        return managerKs;
    }

    public static String getAnonymousKs() {
        if (anonymousKs == null) {
            Response<LoginSession> loginSession = executor.executeSync(OttUserService.anonymousLogin(partnerId));
            anonymousKs = loginSession.results.getKs();
        }
        return anonymousKs;
    }

    public static MediaAsset getSharedMediaAsset() {
        if (mediaAsset == null) {
            mediaAsset = IngestUtils.ingestVOD(Optional.empty(), Optional.empty(), true, Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty(),
                    Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty());
        }
        return mediaAsset;
    }

    public static MediaFile getSharedWebMediaFile() {
        if (webMediaFile == null) {
            if (getProperty(WEB_FILE_TYPE).equals(getSharedMediaAsset().getMediaFiles().get(0).getType())) {
                webMediaFile = mediaAsset.getMediaFiles().get(0);
            } else {
                webMediaFile = mediaAsset.getMediaFiles().get(1);
            }
        }
        return webMediaFile;
    }

    public static MediaFile getSharedMobileMediaFile() {
        if (mobileMediaFile == null) {
            if (getProperty(MOBILE_FILE_TYPE).equals(getSharedMediaAsset().getMediaFiles().get(0).getType())) {
                mobileMediaFile = mediaAsset.getMediaFiles().get(0);
            } else {
                mobileMediaFile = mediaAsset.getMediaFiles().get(1);
            }
        }
        return mobileMediaFile;
    }

    public static Subscription get5MinRenewableSubscription() {
        if (fiveMinRenewableSubscription == null) {
            PricePlan pricePlan = IngestUtils.ingestPP(Optional.empty(), Optional.empty(), Optional.empty(),
                    Optional.of(FIVE_MINUTES_PERIOD), Optional.of(FIVE_MINUTES_PERIOD), Optional.empty(),
                    Optional.of(getProperty(PRICE_CODE_AMOUNT)), Optional.of(CURRENCY_EUR), Optional.of(""),
                    Optional.of(true), Optional.of(3));
            fiveMinRenewableSubscription = IngestUtils.ingestMPP(Optional.empty(), Optional.empty(), Optional.empty(),
                    Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty(),
                    Optional.of(true), Optional.empty(), Optional.of(pricePlan.getName()), Optional.empty(), Optional.empty(), Optional.empty(),
                    Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty());
        }
        return fiveMinRenewableSubscription;
    }

    // shared household
    public static class SharedHousehold {

        private static Household sharedHousehold;
        private static HouseholdUser sharedMasterUser, sharedUser;
        private static String sharedMasterUserKs, sharedUserKs;


        public static Household getSharedHousehold() {
            int numOfUsers = 2;
            int numOfDevices = 2;

            if (sharedHousehold == null) {
                sharedHousehold = createHousehold(numOfUsers, numOfDevices, true);
                List<HouseholdUser> sharedHouseholdUsers = getUsersListFromHouseHold(sharedHousehold);
                for (HouseholdUser user : sharedHouseholdUsers) {
                    if (user.getIsMaster() != null && user.getIsMaster()) {
                        sharedMasterUser = user;
                    }
                    // TODO: ask Alon if we have cases when commented part should be there? What tests related to that logic?
                    if (user.getIsMaster() == null/* && user.getIsDefault() == null*/) {
                        sharedUser = user;
                    }
                }

                String sharedMasterUserName = getUserById(Integer.parseInt(sharedMasterUser.getUserId())).getUsername();
                loginResponse = executor.executeSync(login(partnerId, sharedMasterUserName, defaultUserPassword,null,null));
                sharedMasterUserKs = loginResponse.results.getLoginSession().getKs();

                String sharedUserName = getUserById(Integer.parseInt(sharedUser.getUserId())).getUsername();
                loginResponse = executor.executeSync(login(partnerId, sharedUserName, defaultUserPassword,null,null));
                sharedUserKs = loginResponse.results.getLoginSession().getKs();
            }
            return sharedHousehold;
        }

        public static String getSharedMasterUserKs() {
            if (sharedHousehold == null) getSharedHousehold();
            return sharedMasterUserKs;
        }

        public static String getSharedUserKs() {
            if (sharedHousehold == null) getSharedHousehold();
            return sharedUserKs;
        }

        public static HouseholdUser getSharedMasterUser() {
            if (sharedHousehold == null) getSharedHousehold();
            return sharedMasterUser;
        }

        public static HouseholdUser getSharedUser() {
            if (sharedHousehold == null) getSharedHousehold();
            return sharedUser;
        }
    }
}
