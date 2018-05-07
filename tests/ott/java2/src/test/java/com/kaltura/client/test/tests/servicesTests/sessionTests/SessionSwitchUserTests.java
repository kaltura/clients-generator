package com.kaltura.client.test.tests.servicesTests.sessionTests;

import com.kaltura.client.Client;
import com.kaltura.client.enums.UserState;
import com.kaltura.client.services.OttUserService;
import com.kaltura.client.services.SessionService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.HouseholdUtils;
import com.kaltura.client.test.utils.OttUserUtils;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static org.assertj.core.api.Assertions.assertThat;
import static com.kaltura.client.services.SessionService.*;
import static com.kaltura.client.services.OttUserService.*;

public class SessionSwitchUserTests extends BaseTest {

    //TODO - replace hardcoded user id
    private String UserId = "1543798";
    private String userKs;
    public static Client client;

    @BeforeClass
    private void switchUser_tests_before_class() {
        userKs = OttUserUtils.getKs(Integer.valueOf(UserId), null);
    }

    @Description("/session/action/switchUser")
    @Test
    private void SwitchUser() {

        Household household = HouseholdUtils.createHouseHold(2, 1, false);
        String udid = HouseholdUtils.getDevicesListFromHouseHold(household).get(0).getUdid();
        String masterUserKs = HouseholdUtils.getHouseholdMasterUserKs(household, udid);
        String secondUserId = HouseholdUtils.getRegularUsersListFromHouseHold(household).get(0).getUserId();

        // Invoke session/action/switchUser - second user replace master user in the session

        SwitchUserSessionBuilder switchUserSessionBuilder = SessionService.switchUser(secondUserId);
        switchUserSessionBuilder.setKs(masterUserKs);
        Response<LoginSession> loginSessionResponse = executor.executeSync(switchUserSessionBuilder);

        // Verify new session ks returned
        assertThat(loginSessionResponse.results.getKs()).isNotEmpty();
        String secondUserKs = loginSessionResponse.results.getKs();

        ///----- After User was switched ------

        // Invoke OttUser/action/get - with master user (expired) ks

        OttUserService.GetOttUserBuilder getOttUserBuilder = OttUserService.get();
        getOttUserBuilder.setKs(masterUserKs);
        Response<OTTUser> ottUserResponse = executor.executeSync(getOttUserBuilder);

        // Verify master user ks is now expired (after the switch)
        assertThat(ottUserResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500016).getCode());

        // Invoke OttUser/action/get with second user new ks

        GetOttUserBuilder getOttUserBuilder2 = OttUserService.get();
        getOttUserBuilder2.setKs(secondUserKs);
        Response<OTTUser> ottUserResponse2 = executor.executeSync(getOttUserBuilder);

        // Verify second user id return in the response
        assertThat(ottUserResponse2.results.getId()).isEqualTo(secondUserId);
        assertThat(ottUserResponse2.results.getUserState()).isEqualTo(UserState.OK);


        // Invoke session/action/get

        GetSessionBuilder getSessionBuilder = SessionService.get(secondUserKs);
        getSessionBuilder.setKs(getAdministratorKs());
        Response<Session> getSessionResponse = executor.executeSync(getSessionBuilder);

        // Verify second user id returned in the response
        assertThat(getSessionResponse.results.getUserId()).isEqualTo(secondUserId);
        assertThat(getSessionResponse.results.getUdid()).isEqualTo(udid);
    }

    @Description("/session/action/switchUser - user switch to himself")
    @Test
    private void SwitchUserToHimself() {
        Household household = HouseholdUtils.createHouseHold(2, 1, false);
        String udid = HouseholdUtils.getDevicesListFromHouseHold(household).get(0).getUdid();
        String masterUserKs = HouseholdUtils.getHouseholdMasterUserKs(household, udid);
        String masterUserId = HouseholdUtils.getMasterUserFromHousehold(household).getUserId();

        // Invoke session/action/switchUser - Should return an error (user can't switched to himself

        SwitchUserSessionBuilder switchUserSessionBuilder = SessionService.switchUser(masterUserId);
        switchUserSessionBuilder.setKs(masterUserKs);
        Response<LoginSession> loginSessionResponse = executor.executeSync(switchUserSessionBuilder);

        // Verify exception returned
        assertThat(loginSessionResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(1).getCode());
    }

    @Description("/session/action/switchUser - switch to inactive user")
    @Test
    private void SwitchInactiveUser() {

        //TODO - replace hardcoded user id
        String inactiveUserId = "1543797";
        String UserKs = OttUserUtils.getKs(Integer.valueOf(UserId), null);

        SwitchUserSessionBuilder switchUserSessionBuilder = SessionService.switchUser(inactiveUserId);
        switchUserSessionBuilder.setKs(UserKs);
        Response<LoginSession> loginSessionResponse = executor.executeSync(switchUserSessionBuilder);

        assertThat(loginSessionResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(2016).getCode());
    }

    @Description("/session/action/switchUser - switch to user from another HH")
    @Test
    private void switchToUserFromAnotherHousehold() {

        //TODO - replace hardcoded user id
        String userIdFromHousehold1 = "1543798";
        String Use1rKs = OttUserUtils.getKs(Integer.valueOf(userIdFromHousehold1), null);

        //TODO - replace hardcoded user id
        String userIdFromHousehold2 = "638731";

        SwitchUserSessionBuilder switchUserSessionBuilder = SessionService.switchUser(userIdFromHousehold2);
        switchUserSessionBuilder.setKs(Use1rKs);
        Response<LoginSession> loginSessionResponse = executor.executeSync(switchUserSessionBuilder);

        assertThat(loginSessionResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500055).getCode());
    }


    @Description("/session/action/switchUser - No user id to switch provided")
    @Test
    private void switchToUserWithoutUserId() {

        SwitchUserSessionBuilder switchUserSessionBuilder = SessionService.switchUser(null);
        switchUserSessionBuilder.setKs(userKs);
        Response<LoginSession> loginSessionResponse = executor.executeSync(switchUserSessionBuilder);

        assertThat(loginSessionResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500053).getCode());
    }
}
