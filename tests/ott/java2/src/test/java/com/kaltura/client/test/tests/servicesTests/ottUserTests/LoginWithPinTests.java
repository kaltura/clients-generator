package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.LoginResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.types.UserLoginPin;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.services.OttUserService.*;
import static com.kaltura.client.services.UserLoginPinService.AddUserLoginPinBuilder;
import static com.kaltura.client.services.UserLoginPinService.add;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class LoginWithPinTests extends BaseTest {

    private OTTUser user;

    private Response<LoginResponse> loginResponse;
    private Response<UserLoginPin> userLoginPinResponse;

    private final String SECRET = "secret";

    @BeforeClass
    private void ottUser_login_tests_setup() {
        // register user
        Response<OTTUser> ottUserResponse = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword));
        user = ottUserResponse.results;
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/loginWithPin - loginWithPin with secret")
    @Test
    private void loginWithPin_with_secret() throws InterruptedException {
        // add pin
        AddUserLoginPinBuilder addUserLoginPinBuilder = add(SECRET)
                .setKs(getAdministratorKs())
                .setUserId(Integer.valueOf(user.getId()));
        userLoginPinResponse = executor.executeSync(addUserLoginPinBuilder);

        // login with pin
        String pin = userLoginPinResponse.results.getPinCode();
        LoginWithPinOttUserBuilder loginWithPinOttUserBuilder = loginWithPin(partnerId, pin, null, SECRET);
        loginResponse = executor.executeSync(loginWithPinOttUserBuilder);

        // assert
        assertThat(loginResponse.error).isNull();
        assertThat(loginResponse.results.getLoginSession()).isNotNull();
        assertThat(loginResponse.results.getUser().getUsername()).isEqualTo(user.getUsername());
    }

    @Severity(SeverityLevel.NORMAL)
    @Description("ottUser/action/loginWithPin - loginWithPin with wrong secret - error 2008")
    @Test
    private void loginWithPin_with_wrong_secret() {
        // add pin
        AddUserLoginPinBuilder addUserLoginPinBuilder = add(SECRET)
                .setKs(getAdministratorKs())
                .setUserId(Integer.valueOf(user.getId()));
        userLoginPinResponse = executor.executeSync(addUserLoginPinBuilder);

        // login with pin and wrong secret
        String wrongSecret = SECRET + 1;
        String pin = userLoginPinResponse.results.getPinCode();
        LoginWithPinOttUserBuilder loginWithPinOttUserBuilder = loginWithPin(partnerId, pin, null, wrongSecret);
        loginResponse = executor.executeSync(loginWithPinOttUserBuilder);

        // assert error 2008 is return
        assertThat(loginResponse.results).isNull();
        assertThat(loginResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(2008).getCode());
    }

    @Severity(SeverityLevel.NORMAL)
    @Description("ottUser/action/loginWithPin - loginWithPin with expired pinCode - error 2004")
    @Test(groups = "slow")
    private void loginWithPin_with_expired_pinCode() {
        // add pin
        AddUserLoginPinBuilder addUserLoginPinBuilder = add(SECRET)
                .setKs(getAdministratorKs())
                .setUserId(Integer.valueOf(user.getId()));
        userLoginPinResponse = executor.executeSync(addUserLoginPinBuilder);

        // login with expired pin
        String pin = userLoginPinResponse.results.getPinCode();
        // sleep for 1.5 minutes
        try { Thread.sleep(120000); } catch (InterruptedException e) { e.printStackTrace(); }
        LoginWithPinOttUserBuilder loginWithPinOttUserBuilder = loginWithPin(partnerId, pin, null, SECRET);
        loginResponse = executor.executeSync(loginWithPinOttUserBuilder);

        assertThat(loginResponse.results).isNull();
        assertThat(loginResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(2004).getCode());
    }
}
