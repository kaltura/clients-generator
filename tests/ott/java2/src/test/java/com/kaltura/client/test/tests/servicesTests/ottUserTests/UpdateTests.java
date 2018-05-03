package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.Client;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.Entitlement;
import com.kaltura.client.types.LoginResponse;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Issue;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;


public class UpdateTests extends BaseTest {

    private Client client;
    private OTTUser user;
    private String originalUserEmail;

    private Response<OTTUser> ottUserResponse;

    @BeforeClass
    private void ottUser_update_tests_setup() {
        client = getClient(null);
        // register user
        ottUserResponse = register(client, partnerId, generateOttUser(), defaultUserPassword);
        user = ottUserResponse.results;
        originalUserEmail = user.getEmail();
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/update - update")
    @Test
    private void update() {
        // get self ks
        Response<LoginResponse> loginResponse = OttUserServiceImpl.login(client, partnerId, user.getUsername(), defaultUserPassword, null, null);
        client.setKs(loginResponse.results.getLoginSession().getKs());

        // update
        String newUserInfo = "abc";

        user.setFirstName(newUserInfo);
        user.setLastName(newUserInfo);
        ottUserResponse = OttUserServiceImpl.update(client, user, null);

        assertThat(ottUserResponse.error).isNull();

        // get user after update
        ottUserResponse = OttUserServiceImpl.get(client);
        user = ottUserResponse.results;

        // assert
        assertThat(ottUserResponse.error).isNull();
        assertThat(user.getFirstName()).isEqualTo(newUserInfo);
        assertThat(user.getLastName()).isEqualTo(newUserInfo);
        assertThat(user.getEmail()).isEqualTo(originalUserEmail);
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/update - update with administratorKs")
    @Issue("BEO-4919")
    @Test(enabled = true)
    private void update_with_administratorKs() {

        // update
        String newUserInfo = "def";

        user.setFirstName(newUserInfo);
        user.setLastName(newUserInfo);
//        user.setAffiliateCode(null);

        client.setKs(getAdministratorKs());
        ottUserResponse = OttUserServiceImpl.update(client, user, user.getId());

        assertThat(ottUserResponse.error).isNull();

        // get user after update
        client.setUserId(Integer.valueOf(user.getId()));
        ottUserResponse = OttUserServiceImpl.get(client);
        user = ottUserResponse.results;

        // assert
        assertThat(ottUserResponse.error).isNull();
        assertThat(user.getFirstName()).isEqualTo(newUserInfo);
        assertThat(user.getLastName()).isEqualTo(newUserInfo);
        assertThat(user.getEmail()).isEqualTo(originalUserEmail);
    }

    @AfterClass
    private void ottUser_update_tests_tearDown() {
        OttUserServiceImpl.delete(client);
    }
}
