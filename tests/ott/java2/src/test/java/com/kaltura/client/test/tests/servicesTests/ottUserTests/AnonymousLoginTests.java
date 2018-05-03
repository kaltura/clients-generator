package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.LoginSession;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.Test;

import static com.kaltura.client.services.OttUserService.anonymousLogin;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static org.assertj.core.api.Assertions.assertThat;


public class AnonymousLoginTests extends BaseTest {

    private Response<LoginSession> loginSessionResponse;

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/anonymousLogin - anonymousLogin")
    @Test()
    private void anonymousLoginTest() {
        loginSessionResponse = executor.executeSync(anonymousLogin(partnerId));

        assertThat(loginSessionResponse.error).isNull();
        assertThat(loginSessionResponse.results.getKs()).isNotNull();
    }

    @Severity(SeverityLevel.MINOR)
    @Description("ottUser/action/anonymousLogin - anonymousLogin with wrong partnerId - error 500006")
    @Test()
    private void anonymousLogin_with_wrong_partnerId() {
        int fakePartnerId = 1;
        loginSessionResponse = executor.executeSync(anonymousLogin(fakePartnerId));

        assertThat(loginSessionResponse.results).isNull();
        assertThat(loginSessionResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500006).getCode());
    }

}
