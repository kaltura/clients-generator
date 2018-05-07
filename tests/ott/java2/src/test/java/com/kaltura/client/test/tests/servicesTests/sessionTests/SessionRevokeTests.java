package com.kaltura.client.test.tests.servicesTests.sessionTests;

import com.kaltura.client.Client;
import com.kaltura.client.services.OttUserService;
import com.kaltura.client.services.SessionService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.HouseholdUtils;

import com.kaltura.client.types.Household;
import com.kaltura.client.types.OTTUser;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static org.assertj.core.api.Assertions.assertThat;
import static com.kaltura.client.services.SessionService.*;
import static com.kaltura.client.services.OttUserService.*;

public class SessionRevokeTests extends BaseTest {

    public static Client client;

    @BeforeClass
    private void revoke_tests_before_class() {
    }


    @Description("/session/action/revoke - 2 different kss")
    @Test
    private void RevokeKs() {
        Household household = HouseholdUtils.createHousehold(2, 2, false);
        String udid = HouseholdUtils.getDevicesListFromHouseHold(household).get(0).getUdid();
        String masterUserKs = HouseholdUtils.getHouseholdMasterUserKs(household, null);
        String masterUserKs2 = HouseholdUtils.getHouseholdMasterUserKs(household, udid);

        try {
            Thread.sleep(2000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        // Revoke all sessions for specific user

        RevokeSessionBuilder revokeSessionBuilder = SessionService.revoke();
        revokeSessionBuilder.setKs(masterUserKs);
        Response<Boolean> booleanResponse = executor.executeSync(revokeSessionBuilder);

        assertThat(booleanResponse.results).isTrue();

        try {
            Thread.sleep(2000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        // Verify ks is expired

        GetOttUserBuilder getOttUserBuilder = OttUserService.get();
        getOttUserBuilder.setKs(masterUserKs);
        Response<OTTUser> ottUserResponse = executor.executeSync(getOttUserBuilder);
        assertThat(ottUserResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500016).getCode());

        // Verify ks2 is expired

        GetOttUserBuilder getOttUserBuilder2 = OttUserService.get();
        getOttUserBuilder2.setKs(masterUserKs2);
        Response<OTTUser> ottUserResponse2 = executor.executeSync(getOttUserBuilder);
        assertThat(ottUserResponse2.error.getCode()).isEqualTo(getAPIExceptionFromList(500016).getCode());
    }

}