package com.kaltura.client.utils.request;

import com.kaltura.client.Client;
import com.kaltura.client.Files;
import com.kaltura.client.Params;
import com.kaltura.client.utils.response.OnCompletion;


/**
 * Created by tehilarozin on 14/08/2016.
 */

public class RequestBuilder<T> extends BaseRequestBuilder<T> {

    String service;
    String action;

    public RequestBuilder(Class<T> type) {
    	super(type);
    }

    public RequestBuilder(Class<T> type, String service, String action, Params params, Files files) {
        super(type, params, files);
        this.service = service;
        this.action = action;
    }

    public RequestBuilder(Class<T> type, String service, String action, Params params) {
        super(type, params);
        this.service = service;
        this.action = action;
    }

    public RequestBuilder(String service, String action, Params params, Files files) {
        super(params, files);
        this.service = service;
        this.action = action;
    }

    public RequestBuilder(String service, String action, Params params) {
        super(params);
        this.service = service;
        this.action = action;
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

    public RequestBuilder<T> setCompletion(OnCompletion<T> onCompletion) {
        this.onCompletion = onCompletion;
        return this;
    }

    public RequestBuilder<T> setParam(String key, Object value) {
        if (params != null) {
            params.put(key, value);
        }
        return this;
    }

    public RequestBuilder<T> setId(String id) {
        this.id = id;
        return this;
    }

    @Override
    public String getTag() {
        return action;
    }
}














