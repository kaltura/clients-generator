package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.services.OttUserService.register;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class RegisterTests extends BaseTest {

    private OTTUser user;
    private Response<OTTUser> ottUserResponse;

    @BeforeClass
    private void ottUser_login_tests_setup() {
        user = generateOttUser();
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/register - register")
    @Test
    private void registerTest() {
        ottUserResponse = executor.executeSync(register(partnerId, user, defaultUserPassword));

        assertThat(ottUserResponse.error).isNull();
        assertThat(ottUserResponse.results.getUsername()).isEqualTo(user.getUsername());
        // TODO: 3/28/2018 add relevant assertions
    }

    // TODO: 3/29/2018 add relevant scenarios
}
