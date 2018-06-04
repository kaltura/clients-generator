package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.test.tests.BaseTest;
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

public class ResetPasswordTests extends BaseTest {

    private OTTUser user;

    @BeforeClass
    private void ottUser_resetPassword_tests_setup() {
        // register user
        Response<OTTUser> ottUserResponse = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword));
        user = ottUserResponse.results;
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/resetPassword - resetPassword")
    @Test(enabled = false)
    private void resetPasswordTest() {
        // reset user password
        ResetPasswordOttUserBuilder resetPasswordOttUserBuilder = resetPassword(partnerId, user.getUsername())
                .setKs(getAdministratorKs());
        Response<Boolean> booleanResponse = executor.executeSync(resetPasswordOttUserBuilder);

        // assert success
        assertThat(booleanResponse.error).isNull();
        assertThat(booleanResponse.results.booleanValue()).isTrue();

        // TODO: 4/1/2018 finsih the test after bug BEO-4884 will be fixed
    }
}
