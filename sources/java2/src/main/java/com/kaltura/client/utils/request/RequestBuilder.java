package com.kaltura.client.utils.request;

import com.kaltura.client.Files;
import com.kaltura.client.Params;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;


/**
 * Created by tehilarozin on 14/08/2016.
 */

public class RequestBuilder<T> extends BaseRequestBuilder<T> {

    String service;
    String action;

    public RequestBuilder(Class<T> type, String service, String action, Params params, Files files) {
        super(type, params, files);
        this.service = service;
        this.action = action;
    }

    protected String getUrlTail() {
        StringBuilder urlBuilder = new StringBuilder("service/").append(service);
        if (!action.equals("")) {
            urlBuilder.append("/action/").append(action);
        }

        return urlBuilder.toString();
    }

    public RequestBuilder<T> setCompletion(OnCompletion<Response<T>> onCompletion) {
        this.onCompletion = onCompletion;
        return this;
    }

    @Override
    public String getTag() {
        return action;
    }
}














