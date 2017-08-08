package com.kaltura.client.utils.request;

import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;


/**
 * Created by tehilarozin on 14/08/2016.
 */

public abstract class RequestBuilder<T> extends BaseRequestBuilder<T> {

	protected Integer index = null;
	
    String service;
    String action;

    public RequestBuilder(Class<T> type, String service, String action) {
        super(type);
        this.service = service;
        this.action = action;
    }
    
    public abstract Object getTokenizer() throws APIException; 
    
    protected String getAction() {
        return action;
    }

    protected Params getParams() {
        return params;
    }

    protected String getService() {
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

    protected String getUrlTail() {
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

    protected RequestBuilder<T> setId(String id) {
        this.id = id;
        return this;
    }

    protected RequestBuilder<T> setIndex(int index) {
        this.index = index;
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














