package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.Client;
import com.kaltura.client.test.servicesImpl.OttUserServiceImpl;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.LoginSession;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static org.assertj.core.api.Assertions.assertThat;

public class AnonymousLoginTests extends BaseTest {

    private Client client;

    @BeforeClass
    private void ottUser_anonymousLogin_tests_setup() {
        client = getClient(null);
    }

    @Description("ottUser/action/anonymousLogin - anonymousLogin")
    @Test()
    private void anonymousLogin() {
        Response<LoginSession> loginSessionResponse = OttUserServiceImpl.anonymousLogin(client, partnerId, null);

        assertThat(loginSessionResponse.error).isNull();
        assertThat(loginSessionResponse.results.getKs()).isNotNull();
    }

    @Description("ottUser/action/anonymousLogin - anonymousLogin with wrong partnerId - error 500006")
    @Test()
    private void anonymousLogin_with_wrong_partnerId() {
        Response<LoginSession> loginSessionResponse = OttUserServiceImpl.anonymousLogin(client, partnerId + 1, null);

        assertThat(loginSessionResponse.results).isNull();
        assertThat(loginSessionResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500006).getCode());
    }

}
