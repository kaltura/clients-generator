package com.kaltura.client.test.tests.servicesTests.ottUserTests;

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
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class GetTests extends BaseTest {

    private OTTUser user;
    private Response<LoginResponse> loginResponse;

    @BeforeClass
    private void ottUser_get_tests_setup() {
        // register user
        user = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword)).results;

        // login user
        loginResponse = executor.executeSync(login(partnerId, user.getUsername(), defaultUserPassword));
        user = loginResponse.results.getUser();
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/get - get")
    @Test
    private void getTest() {
        // get user
        GetOttUserBuilder getOttUserBuilder = get()
                .setKs(loginResponse.results.getLoginSession().getKs());
        Response<OTTUser> ottUserResponse = executor.executeSync(getOttUserBuilder);

        assertThat(loginResponse.error).isNull();
        assertThat(ottUserResponse.results).isEqualToIgnoringGivenFields(user, "userState", "userType");
    }
}
