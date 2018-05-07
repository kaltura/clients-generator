package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.services.OttUserService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.BaseUtils;
import com.kaltura.client.types.LoginResponse;
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

public class LogoutTests extends BaseTest {

    private OTTUser user;

    private Response<LoginResponse> loginResponse;
    private Response<Boolean> booleanResponse;


    @BeforeClass
    private void ottUser_logout_tests_setup() {
        // register user
        Response<OTTUser> ottUserResponse = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword));
        user = ottUserResponse.results;
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/logout - logout")
    @Test
    private void logout() {
        // login user
        loginResponse = executor.executeSync(login(partnerId, user.getUsername(), defaultUserPassword));
        String userKs = loginResponse.results.getLoginSession().getKs();

        // logout user
        LogoutOttUserBuilder logoutOttUserBuilder = OttUserService.logout().setKs(userKs);
        booleanResponse = executor.executeSync(logoutOttUserBuilder);

        assertThat(booleanResponse.error).isNull();
        assertThat(booleanResponse.results.booleanValue()).isTrue();

        // assert can't get user after logout
        GetOttUserBuilder getOttUserBuilder = OttUserService.get().setKs(userKs);
        Response<OTTUser> ottUserResponse = executor.executeSync(getOttUserBuilder);
        assertThat(ottUserResponse.results).isNull();
        assertThat(ottUserResponse.error.getCode()).isEqualTo(BaseUtils.getAPIExceptionFromList(500016).getCode());
    }

}
