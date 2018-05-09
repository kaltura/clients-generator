package com.kaltura.client.test.tests.servicesTests.sessionTests;

import com.kaltura.client.Client;
import com.kaltura.client.services.SessionService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.HouseholdUtils;
import com.kaltura.client.test.utils.OttUserUtils;
import com.kaltura.client.test.utils.SessionUtils;
import com.kaltura.client.types.Household;
import com.kaltura.client.types.HouseholdUser;
import com.kaltura.client.types.Session;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.services.SessionService.*;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static org.assertj.core.api.Assertions.assertThat;

public class SessionGetTests extends BaseTest {

    private Client client;

    @BeforeClass
    private void get_tests_before_class() {

    }

    @Description("session/action/get - master user")
    @Test
    private void getMasterUserSession() {
        Household household = HouseholdUtils.createHousehold(2, 1, false);
        HouseholdUser user = HouseholdUtils.getMasterUserFromHousehold(household);
        String udid = HouseholdUtils.getDevicesListFromHouseHold(household).get(0).getUdid();
        String session = OttUserUtils.getKs(Integer.parseInt(user.getUserId()), udid);

        GetSessionBuilder getSessionBuilder = SessionService.get(session);
        getSessionBuilder.setKs(getAdministratorKs());
        Response<Session> getSessionResponse = executor.executeSync(getSessionBuilder);

        assertThat(getSessionResponse.results.getKs()).isEqualTo(session);
        assertThat(getSessionResponse.results.getPartnerId()).isEqualTo(partnerId);
        assertThat(getSessionResponse.results.getUserId()).isEqualTo(user.getUserId());
        assertThat(getSessionResponse.results.getExpiry()).isGreaterThan(Math.toIntExact(System.currentTimeMillis() / 1000));
        assertThat(getSessionResponse.results.getUdid()).isEqualTo(udid);
        assertThat(getSessionResponse.results.getCreateDate()).isGreaterThan(1523954068);
    }

    @Description("session/action/get - Anonymous user")
    @Test
    private void getAnonymousUserSession() {
        String session = BaseTest.getAnonymousKs();

        GetSessionBuilder getSessionBuilder = SessionService.get(session);
        getSessionBuilder.setKs(getAdministratorKs());
        Response<Session> getSessionResponse = executor.executeSync(getSessionBuilder);

        assertThat(getSessionResponse.results.getKs()).isEqualTo(session);
        assertThat(getSessionResponse.results.getUserId()).isEqualTo("0");
        assertThat(getSessionResponse.results.getUdid()).isEqualTo("");
    }

    @Description("session/action/get - operator user")
    @Test
    private void getOperatorUserSession() {
        String session = getOperatorKs();

        GetSessionBuilder getSessionBuilder = SessionService.get(session);
        getSessionBuilder.setKs(getAdministratorKs());
        Response<Session> getSessionResponse = executor.executeSync(getSessionBuilder);

        assertThat(getSessionResponse.results.getKs()).isEqualTo(session);
        assertThat(getSessionResponse.results.getUserId()).isEqualTo(SessionUtils.getUserIdByKs(getOperatorKs()));
        assertThat(getSessionResponse.results.getUdid()).isEqualTo("");
    }

    @Description("session/action/get - invalid ks")
    @Test
    private void getSessionWithInvalidSessionKs() {
        String session = getOperatorKs() + 1;

        GetSessionBuilder getSessionBuilder = SessionService.get(session);
        getSessionBuilder.setKs(getAdministratorKs());
        Response<Session> getSessionResponse = executor.executeSync(getSessionBuilder);

        assertThat(getSessionResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500015).getCode());
    }
}

