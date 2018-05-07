package com.kaltura.client.test.tests.servicesTests.ottUserTests;

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

import static com.kaltura.client.services.OttUserService.*;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class UpdateDynamicDataTests extends BaseTest {

    private OTTUser user;

    @BeforeClass
    private void ottUser_updateDynamicData_tests_setup() {
        // register user
        Response<OTTUser> ottUserResponse = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword));
        user = ottUserResponse.results;
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/updateDynamicData - updateDynamicData")
    @Test
    private void updateDynamicDataTest() {
        // set dynamic data
        String keyString = "key1";
        String valueString = "value1";

        StringValue value = new StringValue();
        value.setValue(valueString);

        // update user dynamic data
        UpdateDynamicDataOttUserBuilder updateDynamicDataOttUserBuilder = updateDynamicData(keyString, value)
                .setKs(getAdministratorKs())
                .setUserId(Integer.valueOf(user.getId()));
        Response<OTTUserDynamicData> ottUserDynamicDataResponse = executor.executeSync(updateDynamicDataOttUserBuilder);

        // assert new dynamic data
        assertThat(ottUserDynamicDataResponse.error).isNull();
        assertThat(ottUserDynamicDataResponse.results.getKey()).isEqualTo(keyString);
        assertThat(ottUserDynamicDataResponse.results.getValue().getValue()).isEqualTo(valueString);
    }
}
