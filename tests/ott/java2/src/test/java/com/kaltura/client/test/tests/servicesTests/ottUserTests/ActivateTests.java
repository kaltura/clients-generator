package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.Client;
import com.kaltura.client.enums.UserState;
import com.kaltura.client.services.OttUserService;
import com.kaltura.client.test.TestAPIOkRequestsExecutor;
import com.kaltura.client.test.servicesImpl.OttUserServiceImpl;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.DBUtils;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.services.OttUserService.*;
import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.login;
import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.register;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class ActivateTests extends BaseTest {

    private Client client;
    private OTTUser user;
    private Response<OTTUser> ottUserResponse;

    @BeforeClass
    private void ottUser_activate_tests_setup() {
        client = getClient(null);

        ottUserResponse = register(client, partnerId, generateOttUser(), defaultUserPassword);
        user = ottUserResponse.results;

        login(client, partnerId, user.getUsername(), defaultUserPassword, null, null);
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/activate - activate")
    @Test
    private void activate() {
        String activationToken = DBUtils.getActivationToken(user.getUsername());

        ActivateOttUserBuilder activateOttUserBuilder = OttUserService.activate(partnerId, user.getUsername(), activationToken);
//        activateOttUserBuilder.setKs("");
//        activateOttUserBuilder.setUserId(1);

        ottUserResponse = executor.executeSync(activateOttUserBuilder);
//        ottUserResponse = executor.executeSync(OttUserService.activate(partnerId, user.getUsername(), activationToken));

        assertThat(ottUserResponse.error).isNull();
        assertThat(ottUserResponse.results.getUserState()).isEqualTo(UserState.OK);
    }

    @Severity(SeverityLevel.MINOR)
    @Description("ottUser/action/activate - activate twice with the same token")
    @Test
    private void activate_with_sa() {
        String activationToken = DBUtils.getActivationToken(user.getUsername());


        ottUserResponse = OttUserServiceImpl.activate(client, partnerId, user.getUsername(), activationToken);
        assertThat(ottUserResponse.error).isNull();
        assertThat(ottUserResponse.results.getUserState()).isEqualTo(UserState.OK);


    }


}
