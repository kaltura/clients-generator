package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.LoginResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.Test;

import static com.kaltura.client.services.OttUserService.*;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class UpdateLoginDataTests extends BaseTest {

    private Response<LoginResponse> loginResponse;

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/updateLoginData - updateLoginData")
    @Test
    private void updateLoginDataTest() {
        // register user
        OTTUser user = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword)).results;

        // login user
        loginResponse = executor.executeSync(login(partnerId, user.getUsername(), defaultUserPassword));
        String userKs = loginResponse.results.getLoginSession().getKs();

        // update user login data
        String userNewPassword = defaultUserPassword + 1;
        UpdateLoginDataOttUserBuilder updateLoginDataOttUserBuilder = updateLoginData(user.getUsername(), defaultUserPassword, userNewPassword)
            .setKs(userKs);
        Response<Boolean> booleanResponse = executor.executeSync(updateLoginDataOttUserBuilder);

        assertThat(booleanResponse.error).isNull();
        assertThat(booleanResponse.results.booleanValue()).isTrue();

        // try login with old password
        loginResponse = executor.executeSync(login(partnerId, user.getUsername(), defaultUserPassword));

        assertThat(loginResponse.results).isNull();
        assertThat(loginResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(1011).getCode());

        // try login with new password
        loginResponse = executor.executeSync(login(partnerId, user.getUsername(), userNewPassword));

        assertThat(loginResponse.error).isNull();
        assertThat(loginResponse.results.getLoginSession().getKs()).isNotNull();
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/updateLoginData - updateLoginData with administratorKs")
    @Test
    private void updateLoginData_with_administratorKs() {
        // register user
        OTTUser user = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword)).results;

        // login user
        loginResponse = executor.executeSync(login(partnerId, user.getUsername(), defaultUserPassword));

        // update usser login data
        String userNewPassword = defaultUserPassword + 2;
        UpdateLoginDataOttUserBuilder updateLoginDataOttUserBuilder = updateLoginData(user.getUsername(), defaultUserPassword, userNewPassword)
                .setKs(getAdministratorKs());
        Response<Boolean> booleanResponse = executor.executeSync(updateLoginDataOttUserBuilder);

        assertThat(booleanResponse.error).isNull();
        assertThat(booleanResponse.results.booleanValue()).isTrue();

        // try login with old password
        loginResponse = executor.executeSync(login(partnerId, user.getUsername(), defaultUserPassword));

        assertThat(loginResponse.results).isNull();
        assertThat(loginResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(1011).getCode());

        // try login with new password
        loginResponse = executor.executeSync(login(partnerId, user.getUsername(), userNewPassword));

        assertThat(loginResponse.error).isNull();
        assertThat(loginResponse.results.getLoginSession().getKs()).isNotNull();
    }
}
