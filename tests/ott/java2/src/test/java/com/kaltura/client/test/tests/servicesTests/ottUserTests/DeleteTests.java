package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.Client;
import com.kaltura.client.test.servicesImpl.HouseholdServiceImpl;
import com.kaltura.client.test.servicesImpl.OttUserServiceImpl;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.HouseholdUtils;
import com.kaltura.client.types.Household;
import com.kaltura.client.types.HouseholdUser;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.get;
import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.register;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static com.kaltura.client.test.utils.HouseholdUtils.getDefaultUserFromHousehold;
import static com.kaltura.client.test.utils.HouseholdUtils.getMasterUserFromHousehold;
import static com.kaltura.client.test.utils.OttUserUtils.generateOttUser;
import static org.assertj.core.api.Assertions.assertThat;

public class DeleteTests extends BaseTest {

    private Client client;
    private Household household;
    private Response<Boolean> booleanResponse;

    @BeforeClass
    private void ottUser_delete_tests_setup() {
        client = getClient(getAdministratorKs());
        household = HouseholdUtils.createHouseHold(2, 0, false);
    }

    @Description("ottUser/action/delete - delete")
    @Test
    private void delete() {
        Response<OTTUser> ottUserResponse = register(client, partnerId, generateOttUser(), defaultUserPassword);
        OTTUser user = ottUserResponse.results;

        client.setUserId(Integer.valueOf(user.getId()));
        Response<Boolean> booleanResponse = OttUserServiceImpl.delete(client);
        boolean result = booleanResponse.results;
        assertThat(result).isTrue();

        ottUserResponse = get(client);
        assertThat(ottUserResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500004).getCode());
        assertThat(ottUserResponse.results).isNull();
    }

    @Description("ottUser/action/delete - delete master user: error 2031")
    @Test(enabled = true)
    private void delete_master_user() {
        HouseholdUser masterUser = getMasterUserFromHousehold(household);

        client.setUserId(Integer.valueOf(masterUser.getUserId()));
        booleanResponse = OttUserServiceImpl.delete(client);

        assertThat(booleanResponse.results).isNull();
        assertThat(booleanResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(2031).getCode());
    }
    
    @Description("ottUser/action/delete - delete default user: error 2030")
    @Test(enabled = true)
    private void delete_default_user() {
        HouseholdUser defaultUser = getDefaultUserFromHousehold(household);

        client.setUserId(Integer.valueOf(defaultUser.getUserId()));
        booleanResponse = OttUserServiceImpl.delete(client);

        assertThat(booleanResponse.results).isNull();
        assertThat(booleanResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(2030).getCode());
    }

    @AfterClass
    private void ottUser_delete_tests_tearDown() {
        HouseholdServiceImpl.delete(client, Math.toIntExact(household.getId()));
    }

}
