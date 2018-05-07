package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.Client;
import com.kaltura.client.test.servicesImpl.OttUserServiceImpl;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.types.OTTUserDynamicData;
import com.kaltura.client.types.StringValue;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.register;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class UpdateDynamicDataTests extends BaseTest {

    private Client client;
    private OTTUser user;

    @BeforeClass
    private void ottUser_updateDynamicData_tests_setup() {
        client = getClient(null);
        Response<OTTUser> ottUserResponse = register(client, partnerId, generateOttUser(), defaultUserPassword);
        user = ottUserResponse.results;
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/updateDynamicData - updateDynamicData")
    @Test
    private void updateDynamicData() {
        // set client
        client.setKs(getAdministratorKs());
        client.setUserId(Integer.valueOf(user.getId()));

        String keyString = "key1";
        String valueString = "value1";

        StringValue value = new StringValue();
        value.setValue(valueString);
        Response<OTTUserDynamicData> ottUserDynamicDataResponse = OttUserServiceImpl.updateDynamicData(client, keyString, value);

        assertThat(ottUserDynamicDataResponse.error).isNull();
        assertThat(ottUserDynamicDataResponse.results.getKey()).isEqualTo(keyString);
        assertThat(ottUserDynamicDataResponse.results.getValue().getValue()).isEqualTo(valueString);
    }
}
