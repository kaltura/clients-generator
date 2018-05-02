package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.Client;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.HouseholdUtils;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.List;

import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.list;
import static com.kaltura.client.test.servicesImpl.OttUserServiceImpl.login;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static com.kaltura.client.test.utils.HouseholdUtils.getMasterUserFromHousehold;
import static com.kaltura.client.test.utils.HouseholdUtils.getUsersListFromHouseHold;
import static com.kaltura.client.test.utils.OttUserUtils.getUserById;
import static org.assertj.core.api.Assertions.assertThat;

public class ListTests extends BaseTest {

    private Client client;
    private Household household;
    private Response<ListResponse<OTTUser>> householdUserListResponse;
    private int numberOfUsersInHousehold = 4;

    @BeforeClass
    private void ottUser_list_tests_setup() {
        client = getClient(null);
        household = HouseholdUtils.createHouseHold(numberOfUsersInHousehold, 1, false);
    }

    @Description("ottUser/action/list - list from master ks")
    @Test
    private void list_from_master_ks() {
        HouseholdUser masterUser = getMasterUserFromHousehold(household);

        Response<LoginResponse> loginResponse = login(client, partnerId, getUserById(Integer.parseInt(masterUser.getUserId())).getUsername(),
                defaultUserPassword, null, null);

        client.setKs(loginResponse.results.getLoginSession().getKs());
        householdUserListResponse = list(client, null);
        List<OTTUser> users = householdUserListResponse.results.getObjects();

        assertThat(loginResponse.error).isNull();
        assertThat(users.size()).isEqualTo(numberOfUsersInHousehold + 1);
    }

    @Description("ottUser/action/list - get list with filter using idIn")
    @Test
    private void list_with_filter_idIn() {
        List<HouseholdUser> householdUsers = getUsersListFromHouseHold(household);

        OTTUserFilter filter = new OTTUserFilter();
        String idIn = householdUsers.get(0).getUserId() + "," + householdUsers.get(1).getUserId();
        filter.setIdIn(idIn);

        client.setKs(getAdministratorKs());
        householdUserListResponse = list(client, filter);
        List<OTTUser> users = householdUserListResponse.results.getObjects();

        assertThat(householdUserListResponse.error).isNull();
        assertThat(users.size()).isEqualTo(2);
    }

    @Description("ottUser/action/list - get list with filter using usernameEqual")
    @Test
    private void list_withd_filter_usernameEqual() {
        List<HouseholdUser> householdUsers = getUsersListFromHouseHold(household);

        OTTUserFilter filter = new OTTUserFilter();
        String usernameEqual = getUserById(Integer.valueOf(householdUsers.get(0).getUserId())).getUsername();

        filter.setUsernameEqual(usernameEqual);

        client.setKs(getAdministratorKs());
        householdUserListResponse = list(client, filter);
        List<OTTUser> users = householdUserListResponse.results.getObjects();

        assertThat(householdUserListResponse.error).isNull();
        assertThat(users.size()).isEqualTo(1);
    }

    @Description("ottUser/action/list - get list with not valid filter")
    @Test
    private void list_with_not_valid_filter() {
        List<HouseholdUser> householdUsers = getUsersListFromHouseHold(household);

        OTTUserFilter filter = new OTTUserFilter();
        filter.setIdIn(householdUsers.get(0).getUserId());
        filter.setUsernameEqual(getUserById(Integer.valueOf(householdUsers.get(1).getUserId())).getUsername());

        client.setKs(getAdministratorKs());
        householdUserListResponse = list(client, filter);

        assertThat(householdUserListResponse.results).isNull();
        assertThat(householdUserListResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500038).getCode());
    }
}
