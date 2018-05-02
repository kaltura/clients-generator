package com.kaltura.client.test.utils;

import com.kaltura.client.Client;
import com.kaltura.client.test.servicesImpl.SessionServiceImpl;
import com.kaltura.client.types.Session;
import com.kaltura.client.utils.response.base.Response;

import static com.kaltura.client.test.tests.BaseTest.getAdministratorKs;
import static com.kaltura.client.test.tests.BaseTest.getClient;

public class SessionUtils extends BaseUtils {


    // Return user id according to the ks provided
    public static String getUserIdByKs(String ks) {
        Client client = getClient(getAdministratorKs());
        Response<Session> getSessionResponse = SessionServiceImpl.get(client,ks);
        return getSessionResponse.results.getUserId();
    }
}
