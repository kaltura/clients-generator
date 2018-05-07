package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.assertj.core.api.Assertions;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.services.OttUserService.*;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;

public class AddRoleTests extends BaseTest {

    private OTTUser user;

    @BeforeClass
    private void ottUser_addRole_tests_setup() {
        // register user
        user = executor.executeSync(register(partnerId, generateOttUser(), defaultUserPassword)).results;
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/addRole - addRole")
    @Test(enabled = false)
    // TODO: 3/27/2018 finish and fix test 
    private void addRoleTest() {
        int roleId = 3;

        // add role
        AddRoleOttUserBuilder addRoleOttUserBuilder = addRole(roleId)
                .setKs(getAdministratorKs())
                .setUserId(Integer.valueOf(user.getId()));
        Response<Boolean> booleanResponse = executor.executeSync(addRoleOttUserBuilder);
        Assertions.assertThat(booleanResponse.results.booleanValue()).isEqualTo(true);

        // TODO: 3/27/2018 finish and fix test
    }
}
