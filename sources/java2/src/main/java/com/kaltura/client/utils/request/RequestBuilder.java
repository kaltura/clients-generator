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

    public RequestBuilder(Class<T> type, String service, String action, Params params) {
        this(type, service, action, params, null);
    }
    
    public RequestBuilder(Class<T> type) {
        this(type, null, null, null);
    }

    public RequestBuilder(String service, String action, Params params, Files files) {
        this(null, service, action, params, files);
    }

    public RequestBuilder(String service, String action, Params params) {
        this(service, action, params, null);
    }

    //@Override
    public String getAction() {
        return action;
    }


    //@Override
    public Params getParams() {
        return params;
    }

    public String getService() {
        return service;
    }

    public MultiRequestBuilder add(RequestBuilder<?> another) {
        try {
            return new MultiRequestBuilder(this, another);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return new MultiRequestBuilder();
    }

    public String getUrlTail() {
        StringBuilder urlBuilder = new StringBuilder("service/").append(service);
        if (!action.equals("")) {
            urlBuilder.append("/action/").append(action);
        }

        return urlBuilder.toString();
    }


    protected RequestBuilder<T> link(String destKey, String requestId, String sourceKey) {
        params.link(destKey, requestId, sourceKey);
        return this;
    }

    public RequestBuilder<T> setId(String id) {
        this.id = id;
        return this;
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














