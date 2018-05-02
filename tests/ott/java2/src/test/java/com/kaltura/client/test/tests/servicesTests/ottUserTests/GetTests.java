package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.Client;
import com.kaltura.client.test.servicesImpl.OttUserServiceImpl;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.LoginResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.login;
import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.register;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class GetTests extends BaseTest {

    private Client client;
    private OTTUser user;

    private Response<LoginResponse> loginResponse;
    private Response<OTTUser> ottUserResponse;

    @BeforeClass
    private void ottUser_login_tests_setup() {
        client = getClient(null);
        ottUserResponse = register(client, partnerId, generateOttUser(), defaultUserPassword);
        user = ottUserResponse.results;

        loginResponse = login(client, partnerId, user.getUsername(), defaultUserPassword, null, null);
        user = loginResponse.results.getUser();
        client.setKs(loginResponse.results.getLoginSession().getKs());
    }

    // get tests
    @Description("ottUser/action/get - get")
    @Test
    private void get() {
        ottUserResponse = OttUserServiceImpl.get(client);
        assertThat(loginResponse.error).isNull();
        assertThat(ottUserResponse.results).isEqualToIgnoringGivenFields(user, "userState", "userType");
    }
}
