package com.kaltura.client.test.tests.servicesTests.ottUserTests;

import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.HouseholdUtils;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.List;

import static com.kaltura.client.services.OttUserService.*;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static com.kaltura.client.test.utils.BaseUtils.getConcatenatedString;
import static com.kaltura.client.test.utils.HouseholdUtils.getMasterUserFromHousehold;
import static com.kaltura.client.test.utils.HouseholdUtils.getUsersListFromHouseHold;
import static com.kaltura.client.test.utils.OttUserUtils.getUserById;
import static org.assertj.core.api.Assertions.assertThat;

public class ListTests extends BaseTest {

    private Household household;
    private Response<ListResponse<OTTUser>> householdUserListResponse;
    private int numberOfUsersInHousehold = 4;
    private int numberOfDevicesInHousehold = 1;


    @BeforeClass
    private void ottUser_list_tests_setup() {
        household = HouseholdUtils.createHousehold(numberOfUsersInHousehold, numberOfDevicesInHousehold, false);
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/list - list from master ks")
    @Test
    private void list_from_master_ks() {
        // get master user from household
        HouseholdUser masterUser = getMasterUserFromHousehold(household);

        // login master user
        String username = getUserById(Integer.parseInt(masterUser.getUserId())).getUsername();
        LoginOttUserBuilder loginOttUserBuilder = login(partnerId, username, defaultUserPassword);
        Response<LoginResponse> loginResponse = executor.executeSync(loginOttUserBuilder);

        // list household users
        ListOttUserBuilder listOttUserBuilder = list()
                .setKs(loginResponse.results.getLoginSession().getKs());
        householdUserListResponse = executor.executeSync(listOttUserBuilder);
        List<OTTUser> users = householdUserListResponse.results.getObjects();

        // assert users list size
        assertThat(householdUserListResponse.error).isNull();
        assertThat(users.size()).isEqualTo(numberOfUsersInHousehold + 2);
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/list - get list with filter using idIn")
    @Test
    private void list_with_filter_idIn() {
        // get users from household
        List<HouseholdUser> householdUsers = getUsersListFromHouseHold(household);

        // set user filter
        OTTUserFilter ottUserFilter = new OTTUserFilter();
        String idIn = getConcatenatedString(householdUsers.get(0).getUserId(), householdUsers.get(1).getUserId());
        ottUserFilter.setIdIn(idIn);

        // list household users
        ListOttUserBuilder listOttUserBuilder = list(ottUserFilter)
                .setKs(getAdministratorKs());
        householdUserListResponse = executor.executeSync(listOttUserBuilder);
        List<OTTUser> users = householdUserListResponse.results.getObjects();

        // assert users list size
        assertThat(householdUserListResponse.error).isNull();
        assertThat(users.size()).isEqualTo(2);
    }

    @Severity(SeverityLevel.CRITICAL)
    @Description("ottUser/action/list - get list with filter using usernameEqual")
    @Test
    private void list_withd_filter_usernameEqual() {
        // get users from household
        List<HouseholdUser> householdUsers = getUsersListFromHouseHold(household);

        // set user filter
        OTTUserFilter ottUserFilter = new OTTUserFilter();
        String usernameEqual = getUserById(Integer.valueOf(householdUsers.get(0).getUserId())).getUsername();
        ottUserFilter.setUsernameEqual(usernameEqual);

        // list household users
        ListOttUserBuilder listOttUserBuilder = list(ottUserFilter)
                .setKs(getAdministratorKs());
        householdUserListResponse = executor.executeSync(listOttUserBuilder);
        List<OTTUser> users = householdUserListResponse.results.getObjects();

        // assert users list size
        assertThat(householdUserListResponse.error).isNull();
        assertThat(users.size()).isEqualTo(1);
    }

    @Severity(SeverityLevel.NORMAL)
    @Description("ottUser/action/list - get list with not valid filter")
    @Test
    private void list_with_not_valid_filter() {
        // get users from household
        List<HouseholdUser> householdUsers = getUsersListFromHouseHold(household);

        // set user filter
        OTTUserFilter ottUserFilter = new OTTUserFilter();
        ottUserFilter.setIdIn(householdUsers.get(0).getUserId());
        ottUserFilter.setUsernameEqual(getUserById(Integer.valueOf(householdUsers.get(1).getUserId())).getUsername());

        // list household users
        ListOttUserBuilder listOttUserBuilder = list(ottUserFilter)
                .setKs(getAdministratorKs());
        householdUserListResponse = executor.executeSync(listOttUserBuilder);

        // assert error 500038 is return
        assertThat(householdUserListResponse.results).isNull();
        assertThat(householdUserListResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500038).getCode());
    }
}
