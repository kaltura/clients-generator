package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.Client;
import com.kaltura.client.test.servicesImpl.OttUserServiceImpl;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.login;
import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.register;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class ResendActivationTokenTests extends BaseTest {

    private Client client;
    private OTTUser user;

    @BeforeClass
    private void ottUser_resendActivationToken_tests_setup() {
        client = getClient(null);
        user = generateOttUser();

        register(client, partnerId, user, defaultUserPassword);
        login(client, partnerId, user.getUsername(), defaultUserPassword, null, null);
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/resendActivationToken - resendActivationToken")
    @Test(enabled = false)
    private void resendActivationToken() {
        Response<Boolean> booleanResponse = OttUserServiceImpl.resendActivationToken(client, partnerId, user.getUsername());
        assertThat(booleanResponse.error).isNull();
        assertThat(booleanResponse.results.booleanValue()).isTrue();

        // TODO: 4/1/2018 can't be completely tests until we verify emails
    }
}
