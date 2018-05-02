package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.Client;
import com.kaltura.client.test.servicesImpl.OttUserServiceImpl;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.types.StringValue;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.register;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class GetEncryptedUserIdTests extends BaseTest {

    private Client client;
    private OTTUser user;

    private Response<StringValue> stringValueResponse;

    @BeforeClass
    private void ottUser_getEncryptedUserId_tests_setup() {
        client = getClient(null);
        Response<OTTUser> ottUserResponse = register(client, partnerId, generateOttUser(), defaultUserPassword);
        user = ottUserResponse.results;
    }

    @Description("ottUser/action/getEncryptedUserId - getEncryptedUserId")
    @Test
    private void getEncryptedUserId() {
        client.setKs(getAdministratorKs());
        client.setUserId(Integer.parseInt(user.getId()));
        stringValueResponse = OttUserServiceImpl.getEncryptedUserId(client);

        assertThat(stringValueResponse.error).isNull();
        assertThat(stringValueResponse.results.getValue()).isNotNull();
    }
}
