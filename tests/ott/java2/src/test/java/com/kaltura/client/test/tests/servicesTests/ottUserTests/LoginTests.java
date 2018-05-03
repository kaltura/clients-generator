package com.kaltura.client.test.tests.servicesTests.ottUserTests;


import com.kaltura.client.services.OttUserService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.LoginResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.services.OttUserService.*;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class LoginTests extends BaseTest {

    private OTTUser user;

    private Response<LoginResponse> loginResponse;

    @BeforeClass
    private void ottUser_login_tests_setup() {
        // register user
        user = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword)).results;
    }

    @Severity(SeverityLevel.CRITICAL)
    @Test(description = "ottUser/action/login - login")
    private void login() {
        loginResponse = OttUserServiceImpl.login(client, partnerId, user.getUsername(), defaultUserPassword, null, null);

        assertThat(loginResponse.error).isNull();
        assertThat(loginResponse.results.getLoginSession()).isNotNull();
        assertThat(loginResponse.results.getUser().getUsername()).isEqualTo(user.getUsername());
    }

    @Severity(SeverityLevel.NORMAL)
    @Description("ottUser/action/login - login with wrong password - error 1011")
    @Test
    private void login_with_wrong_password() {
        loginResponse = OttUserServiceImpl.login(client, partnerId, user.getUsername(), defaultUserPassword + "1", null, null);

        assertThat(loginResponse.results).isNull();
        assertThat(loginResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(1011).getCode());
    }

    @Severity(SeverityLevel.NORMAL)
    @Description("ottUser/action/login - login with wrong username - error 1011")
    @Test
    private void login_with_wrong_username() {
        loginResponse = OttUserServiceImpl.login(client, partnerId, user.getUsername() + "1", defaultUserPassword, null, null);

        assertThat(loginResponse.results).isNull();
        assertThat(loginResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(1011).getCode());
    }

    @Severity(SeverityLevel.NORMAL)
    @Description("ottUser/action/login - login with wrong partnerId - error 500006")
    @Test()
    private void login_with_wrong_partnerId() {
        loginResponse = OttUserServiceImpl.login(client, partnerId + 1, user.getUsername(), defaultUserPassword, null, null);

        assertThat(loginResponse.results).isNull();
        assertThat(loginResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500006).getCode());
    }
}
