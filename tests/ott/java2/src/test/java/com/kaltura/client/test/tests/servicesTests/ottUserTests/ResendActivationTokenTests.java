package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.services.OttUserService.*;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class ResendActivationTokenTests extends BaseTest {

    private OTTUser user;

    @BeforeClass
    private void ottUser_resendActivationToken_tests_setup() {
        // register user
        user = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword)).results;

        // login user
        user = executor.executeSync(login(partnerId, user.getUsername(), defaultUserPassword)).results.getUser();
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/resendActivationToken - resendActivationToken")
    @Test(enabled = false)
    private void resendActivationTokenTest() {
        Response<Boolean> booleanResponse = executor.executeSync(resendActivationToken(partnerId, user.getUsername()));

        assertThat(booleanResponse.error).isNull();
        assertThat(booleanResponse.results.booleanValue()).isTrue();

        // TODO: 4/1/2018 can't be completely tested until we verify emails
    }
}
