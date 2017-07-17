package com.kaltura.client;

import com.kaltura.client.utils.request.ConnectionConfiguration;
import com.kaltura.client.utils.request.RequestElement;
import com.kaltura.client.utils.response.base.ResponseElement;

public interface RequestQueue {

    void setDefaultConfiguration(ConnectionConfiguration config);

    String queue(RequestElement request);

    ResponseElement execute(RequestElement request);

    void cancelRequest(String reqId);

    void clearRequests();

    boolean isEmpty();

    void enableLogs(boolean enable);
}
