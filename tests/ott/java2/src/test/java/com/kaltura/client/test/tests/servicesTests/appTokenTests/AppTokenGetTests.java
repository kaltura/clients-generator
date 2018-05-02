package com.kaltura.client.test.tests.servicesTests.appTokenTests;

import com.kaltura.client.Client;
import com.kaltura.client.enums.AppTokenHashType;
import com.kaltura.client.services.AppTokenService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.AppTokenUtils;
import com.kaltura.client.test.utils.BaseUtils;
import com.kaltura.client.types.AppToken;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.tests.BaseTest.SharedHousehold.getSharedUser;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static org.assertj.core.api.Assertions.assertThat;

public class AppTokenGetTests extends BaseTest {

    private String sessionUserId;
    private String sessionPrivileges = "key1:value1,key2:value2";
    private AppTokenHashType hashType;
    public static Client client;
    private Long expiryDate;
    private int offSetInMinutes = 1;
    private int sessionDuration = 86400;
    private AppToken appToken;

    @BeforeClass
    private void get_tests_before_class() {
        hashType = AppTokenHashType.SHA1;
        sessionUserId =  getSharedUser().getUserId();

        expiryDate = BaseUtils.getTimeInEpoch(offSetInMinutes);
    }

    @Description("AppToken/action/get")
    @Test

    private void getAppToken() {
        appToken = AppTokenUtils.addAppToken(sessionUserId, hashType, sessionPrivileges, Math.toIntExact(expiryDate));

        AppTokenService.AddAppTokenBuilder addAppTokenBuilder = AppTokenService.add(appToken);
        addAppTokenBuilder.setKs(getOperatorKs());
        Response<AppToken> addAppTokenResponse = executor.executeSync(addAppTokenBuilder);

        AppTokenService.GetAppTokenBuilder getAppTokenBuilder = AppTokenService.get(addAppTokenResponse.results.getId());
        getAppTokenBuilder.setKs(getOperatorKs());
        Response<AppToken> getAppTokenResponse = executor.executeSync(getAppTokenBuilder);

        assertThat(getAppTokenResponse.results.getId()).isEqualTo(addAppTokenResponse.results.getId());
        assertThat(getAppTokenResponse.results.getExpiry()).isEqualTo(Math.toIntExact(expiryDate));
        assertThat(getAppTokenResponse.results.getPartnerId()).isEqualTo(partnerId);
        assertThat(getAppTokenResponse.results.getSessionDuration()).isEqualTo(sessionDuration);
        assertThat(getAppTokenResponse.results.getHashType()).isEqualTo(hashType);
        assertThat(getAppTokenResponse.results.getSessionPrivileges()).isEqualTo(sessionPrivileges);
        assertThat(getAppTokenResponse.results.getToken()).isEqualTo(addAppTokenResponse.results.getToken());
        assertThat(getAppTokenResponse.results.getSessionUserId()).isEqualTo(sessionUserId);
    }

    @Description("AppToken/action/get")
    @Test
    private void getAppTokenWithInvalidId() {

        AppTokenService.GetAppTokenBuilder getAppTokenBuilder = AppTokenService.get("1234");
        getAppTokenBuilder.setKs(getOperatorKs());
        Response<AppToken> getAppTokenResponse = executor.executeSync(getAppTokenBuilder);

        assertThat(getAppTokenResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500055).getCode());
    }


}
