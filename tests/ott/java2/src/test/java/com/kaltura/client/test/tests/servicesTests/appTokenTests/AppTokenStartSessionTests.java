package com.kaltura.client.test.tests.servicesTests.appTokenTests;

import com.kaltura.client.Client;
import com.kaltura.client.enums.AppTokenHashType;
import com.kaltura.client.services.AppTokenService;
import com.kaltura.client.services.OttUserService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.AppTokenUtils;
import com.kaltura.client.test.utils.BaseUtils;
import com.kaltura.client.types.AppToken;
import com.kaltura.client.types.LoginSession;
import com.kaltura.client.types.SessionInfo;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.tests.BaseTest.SharedHousehold.*;
import static org.assertj.core.api.Assertions.assertThat;
import static com.kaltura.client.services.OttUserService.*;
import static com.kaltura.client.services.AppTokenService.*;

public class AppTokenStartSessionTests extends BaseTest {

    private AppTokenHashType hashType;
    //TODO - remove hardcoded values
    private String sessionUserId = "1577578";
    private String udid1 = "1234567890";
    private String udid2 = "9876543210";
    private AppToken appToken = new AppToken();
    public static Client client;
    private String sessionPrivileges = "key1:value1,key2:value2";
    private Long expiryDate;
    private String anonymousKs;


    @BeforeClass
    private void add_tests_before_class() {

        // Invoke ottUser/action/anonymousLogin to receive LoginSession object (and anonymous KS)

        AnonymousLoginOttUserBuilder anonymousLoginOttUserBuilder = OttUserService.anonymousLogin(partnerId, udid1);
        anonymousLoginOttUserBuilder.setKs(null);
        Response<LoginSession> loginSessionResponse = executor.executeSync(anonymousLoginOttUserBuilder);

        anonymousKs = loginSessionResponse.results.getKs();
        client.setKs(null);
        expiryDate = BaseUtils.getTimeInEpoch(1);
    }

    @Description("appToken/action/startSession - SHA1")
    @Test
    private void startSessionSha1() {
        hashType = AppTokenHashType.SHA1;
        // Build appToken object
        appToken = AppTokenUtils.addAppToken(sessionUserId, hashType, sessionPrivileges, Math.toIntExact(expiryDate));

        // Invoke AppToken/action/add

        AppTokenService.AddAppTokenBuilder addAppTokenBuilder = AppTokenService.add(appToken);
        addAppTokenBuilder.setKs(getOperatorKs());
        Response<AppToken> appTokenResponse = executor.executeSync(addAppTokenBuilder);

        client.setKs(anonymousKs);

        // Generate new token hash
        String tokenHash = AppTokenUtils.getTokenHash(hashType, anonymousKs, appTokenResponse.results.getToken());

        // Invoke AppToken/action/startSession - with udid1

        StartSessionAppTokenBuilder startSessionAppTokenBuilder = AppTokenService.startSession(appTokenResponse.results.getId(), tokenHash, null,
                Math.toIntExact(expiryDate), udid1);
        startSessionAppTokenBuilder.setKs(anonymousKs);
        Response<SessionInfo> sessionInfoResponse = executor.executeSync(startSessionAppTokenBuilder);

        assertThat(sessionInfoResponse.results.getKs()).isNotEmpty();
        assertThat(sessionInfoResponse.results.getPartnerId()).isEqualTo(partnerId);
        assertThat(sessionInfoResponse.results.getUserId()).isEqualTo(sessionUserId);
        assertThat(sessionInfoResponse.results.getExpiry()).isEqualTo(Math.toIntExact(expiryDate));
        assertThat(sessionInfoResponse.results.getPrivileges()).contains(sessionPrivileges);
        assertThat(sessionInfoResponse.results.getUdid()).isEqualTo(udid1);
        assertThat(sessionInfoResponse.results.getCreateDate()).isNotZero();

        // Invoke AppToken/action/startSession - with udid2

        startSessionAppTokenBuilder = AppTokenService.startSession(appTokenResponse.results.getId(), tokenHash, null,
                Math.toIntExact(expiryDate), udid2);
        startSessionAppTokenBuilder.setKs(anonymousKs);
        sessionInfoResponse = executor.executeSync(startSessionAppTokenBuilder);

        assertThat(sessionInfoResponse.results.getKs()).isNotEmpty();

        // TODO - Add session/action/get request with ks received from startSession API

    }

    @Description("appToken/action/startSession - SHA256")
    @Test
    private void startSessionSha256() {
        hashType = AppTokenHashType.SHA256;
        // Build appToken object
        appToken = AppTokenUtils.addAppToken(sessionUserId, hashType, sessionPrivileges, Math.toIntExact(expiryDate));

        // Invoke AppToken/action/add
        client.setKs(getOperatorKs());

        AppTokenService.AddAppTokenBuilder addAppTokenBuilder = AppTokenService.add(appToken);
        addAppTokenBuilder.setKs(getOperatorKs());
        Response<AppToken> appTokenResponse = executor.executeSync(addAppTokenBuilder);

        client.setKs(anonymousKs);

        // Generate new token hash
        String tokenHash = AppTokenUtils.getTokenHash(hashType, anonymousKs, appTokenResponse.results.getToken());

        // // Invoke AppToken/action/startSession - with udid1

        StartSessionAppTokenBuilder startSessionAppTokenBuilder = AppTokenService.startSession(appTokenResponse.results.getId(), tokenHash, null,
                Math.toIntExact(expiryDate), udid1);
        startSessionAppTokenBuilder.setKs(anonymousKs);
        Response<SessionInfo> sessionInfoResponse = executor.executeSync(startSessionAppTokenBuilder);

        assertThat(sessionInfoResponse.results.getKs()).isNotEmpty();
        assertThat(sessionInfoResponse.results.getPartnerId()).isEqualTo(partnerId);
        assertThat(sessionInfoResponse.results.getUserId()).isEqualTo(sessionUserId);
        assertThat(sessionInfoResponse.results.getExpiry()).isEqualTo(Math.toIntExact(expiryDate));
        assertThat(sessionInfoResponse.results.getPrivileges()).contains(sessionPrivileges);
        assertThat(sessionInfoResponse.results.getUdid()).isEqualTo(udid1);
        assertThat(sessionInfoResponse.results.getCreateDate()).isNotZero();

        // Invoke AppToken/action/startSession - with udid2

        startSessionAppTokenBuilder = AppTokenService.startSession(appTokenResponse.results.getId(), tokenHash, null,
                Math.toIntExact(expiryDate), udid2);
        startSessionAppTokenBuilder.setKs(anonymousKs);
        sessionInfoResponse = executor.executeSync(startSessionAppTokenBuilder);

        assertThat(sessionInfoResponse.results.getKs()).isNotEmpty();

        // TODO - Add session/action/get request with ks received from startSession API
    }

    @Description("appToken/action/startSession - token id with default expiry date (according to the value in group_203 CB document" +
            "OPEN BEO-4980")
    @Test
    private void startSessionDefaultExpiryDate() {
        int expiryDate = 0;
        getSharedHousehold();
        hashType = AppTokenHashType.SHA1;
        appToken = AppTokenUtils.addAppToken(null, hashType, null, expiryDate);

        AppTokenService.AddAppTokenBuilder addAppTokenBuilder = AppTokenService.add(appToken);
        addAppTokenBuilder.setKs(getSharedMasterUserKs());
        Response<AppToken> appTokenResponse = executor.executeSync(addAppTokenBuilder);

        String tokenHash = AppTokenUtils.getTokenHash(hashType, anonymousKs, appTokenResponse.results.getToken());

        StartSessionAppTokenBuilder startSessionAppTokenBuilder = AppTokenService.startSession(appTokenResponse.results.getId(), tokenHash, null,
                Math.toIntExact(expiryDate), udid1);
        startSessionAppTokenBuilder.setKs(anonymousKs);
        Response<SessionInfo> sessionInfoResponse = executor.executeSync(startSessionAppTokenBuilder);

        assertThat(sessionInfoResponse.results.getKs()).isNotEmpty();
    }
}
