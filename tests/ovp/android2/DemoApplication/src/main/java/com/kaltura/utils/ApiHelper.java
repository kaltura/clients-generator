package com.kaltura.utils;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.Client;
import com.kaltura.client.Configuration;
import com.kaltura.client.RequestQueue;
import com.kaltura.client.types.Category;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.request.RequestBuilder;

/**
 * Created by jonathan.kanarek on 06/07/2017.
 */

public class ApiHelper {

    private static Client client;

    private static String host;

    private static String cdnHost;

    public static String getHost() {
        return host;
    }

    public static void setHost(String host) {
        ApiHelper.host = host;
    }

    public static String getCdnHost() {
        return cdnHost;
    }

    public static void setCdnHost(String cdnHost) {
        ApiHelper.cdnHost = cdnHost;
    }

    public static Client getClient() {
        if(client == null) {
            Configuration config = new Configuration();
            config.setConnectTimeout(10000);
            config.setEndpoint(host);

            client = new Client(config);
        }
        return client;
    }

    public static RequestQueue getRequestQueue() {
        return APIOkRequestsExecutor.getExecutor();
    }

    public static void execute(RequestBuilder<?> requestBuilder) {
        getRequestQueue().queue(requestBuilder.build(getClient()));
    }
}