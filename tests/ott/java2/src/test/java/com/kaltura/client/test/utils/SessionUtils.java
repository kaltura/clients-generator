package com.kaltura.client.test.utils;

import com.kaltura.client.services.SessionService;
import com.kaltura.client.types.Session;
import com.kaltura.client.utils.response.base.Response;

import static com.kaltura.client.test.tests.BaseTest.executor;
import static com.kaltura.client.test.tests.BaseTest.getAdministratorKs;

public class SessionUtils extends BaseUtils {


    // Return user id according to the ks provided
    public static String getUserIdByKs(String ks) {
        SessionService.GetSessionBuilder getSessionBuilder = SessionService.get(ks);
        getSessionBuilder.setKs(getAdministratorKs());
        Response<Session> getSessionResponse = executor.executeSync(getSessionBuilder);

        return getSessionResponse.results.getUserId();
    }
}
