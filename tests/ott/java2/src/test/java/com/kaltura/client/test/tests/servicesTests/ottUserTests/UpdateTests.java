package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.test.tests.BaseTest;
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

import static com.kaltura.client.services.OttUserService.*;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;


public class UpdateTests extends BaseTest {

    private OTTUser user;
    private String originalUserEmail;

    private Response<OTTUser> ottUserResponse;

    @BeforeClass
    private void ottUser_update_tests_setup() {
        // register user
        ottUserResponse = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword));
        user = ottUserResponse.results;
        originalUserEmail = user.getEmail();
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/update - update")
    @Test
    private void updateTest() {
        // login user
        Response<LoginResponse> loginResponse = executor.executeSync(login(partnerId, user.getUsername(), defaultUserPassword));
        String userKs = loginResponse.results.getLoginSession().getKs();

        // update user info
        String newUserInfo = "abc";
        user.setFirstName(newUserInfo);
        user.setLastName(newUserInfo);

        ottUserResponse = executor.executeSync(update(user).setKs(userKs));
        assertThat(ottUserResponse.error).isNull();

        // get user after update
        ottUserResponse = executor.executeSync(get().setKs(userKs));
        user = ottUserResponse.results;

        // assert user new info
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

        // update user info
        String newUserInfo = "def";
        user.setFirstName(newUserInfo);
        user.setLastName(newUserInfo);
//        user.setAffiliateCode(null);

        ottUserResponse = executor.executeSync(update(user).setKs(getAdministratorKs()));
        assertThat(ottUserResponse.error).isNull();

        // get user after update
        ottUserResponse =  executor.executeSync(get().setKs(getAdministratorKs()).setUserId(Integer.valueOf(user.getId())));
        user = ottUserResponse.results;

        // assert user new info
        assertThat(ottUserResponse.error).isNull();
        assertThat(user.getFirstName()).isEqualTo(newUserInfo);
        assertThat(user.getLastName()).isEqualTo(newUserInfo);
        assertThat(user.getEmail()).isEqualTo(originalUserEmail);
    }

    @AfterClass
    private void ottUser_update_tests_tearDown() {
        executor.executeSync(delete().setKs(getAdministratorKs()).setUserId(Integer.valueOf(user.getId())));
    }
}
