package com.kaltura.client;

import com.kaltura.client.utils.request.ConnectionConfiguration;
import com.kaltura.client.utils.request.RequestElement;
import com.kaltura.client.utils.response.base.Response;

public interface RequestQueue {

    void setDefaultConfiguration(ConnectionConfiguration config);

    @SuppressWarnings("rawtypes")
	String queue(RequestElement request);

    @SuppressWarnings("rawtypes")
	Response<?> execute(RequestElement request);

    void cancelRequest(String reqId);

    void clearRequests();

    boolean isEmpty();

    void enableLogs(boolean enable);
}
