package com.kaltura.client.test.tests.servicesTests.appTokenTests;

import com.kaltura.client.enums.AppTokenHashType;
import com.kaltura.client.services.AppTokenService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.AppTokenUtils;
import com.kaltura.client.types.AppToken;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.services.AppTokenService.*;
import static com.kaltura.client.test.tests.BaseTest.SharedHousehold.getSharedUser;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static org.assertj.core.api.Assertions.assertThat;

public class AppTokenDeleteTests extends BaseTest {
    
    private String sessionUserId;
    private AppToken appToken;

    @BeforeClass
    private void add_tests_before_class() {
        sessionUserId = getSharedUser().getUserId();
        appToken = AppTokenUtils.addAppToken(sessionUserId, AppTokenHashType.SHA1, null, null);
    }

    @Description("appToken/action/delete")
    @Test
    // TODO: 5/3/2018 not clear test name! 
    private void addAppToken() {
        // Add token
        AddAppTokenBuilder addAppTokenBuilder = AppTokenService.add(appToken).setKs(getOperatorKs());
        Response<AppToken> appTokenResponse = executor.executeSync(addAppTokenBuilder);

        assertThat(appTokenResponse.error).isNull();
        assertThat(appTokenResponse.results.getExpiry()).isNull();

        // Delete token
        DeleteAppTokenBuilder deleteAppTokenBuilder = AppTokenService.delete(appTokenResponse.results.getId()).setKs(getOperatorKs());
        Response<Boolean> deleteTokenResponse = executor.executeSync(deleteAppTokenBuilder);

        assertThat(deleteTokenResponse.results).isTrue();

        // Try to delete token using invalid token id
        String invalidTokenId = "1234";
        deleteAppTokenBuilder = AppTokenService.delete(invalidTokenId).setKs(getOperatorKs());
        deleteTokenResponse = executor.executeSync(deleteAppTokenBuilder);

        // TODO: 5/3/2018 split two scenarios into separate tests 
        assertThat(deleteTokenResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500055).getCode());

        // Try to delete token again - exception returned
        deleteAppTokenBuilder = AppTokenService.delete(appTokenResponse.results.getId()).setKs(getOperatorKs());
        deleteTokenResponse = executor.executeSync(deleteAppTokenBuilder);

        assertThat(deleteTokenResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500055).getCode());
    }

}