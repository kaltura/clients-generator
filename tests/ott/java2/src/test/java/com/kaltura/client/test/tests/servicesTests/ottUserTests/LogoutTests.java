package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.Client;
import com.kaltura.client.test.servicesImpl.OttUserServiceImpl;
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

import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.login;
import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.register;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class LogoutTests extends BaseTest {

    private Client client;
    private OTTUser user;

    private Response<LoginResponse> loginResponse;
    private Response<Boolean> booleanResponse;


    @BeforeClass
    private void ottUser_logout_tests_setup() {
        client = getClient(null);

        Response<OTTUser> ottUserResponse = register(client, partnerId, generateOttUser(), defaultUserPassword);
        user = ottUserResponse.results;
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/logout - logout")
    @Test
    private void logout() {
        loginResponse = login(client, partnerId, user.getUsername(), defaultUserPassword, null, null);
        client.setKs(loginResponse.results.getLoginSession().getKs());

        booleanResponse = OttUserServiceImpl.logout(client);
        assertThat(booleanResponse.error).isNull();
        assertThat(booleanResponse.results.booleanValue()).isTrue();

        Response<OTTUser> ottUserResponse = OttUserServiceImpl.get(client);
        assertThat(ottUserResponse.results).isNull();
        assertThat(ottUserResponse.error.getCode()).isEqualTo(BaseUtils.getAPIExceptionFromList(500016).getCode());
    }

}
