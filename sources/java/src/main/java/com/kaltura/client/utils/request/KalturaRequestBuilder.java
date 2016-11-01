package com.kaltura.client.utils.request;

import com.kaltura.client.KalturaParams;
import com.kaltura.client.utils.response.OnCompletion;


/**
 * Created by tehilarozin on 14/08/2016.
 */

public class KalturaRequestBuilder extends BaseRequestBuilder {

    String service;
    String action;

    public KalturaRequestBuilder() {
    }

    public KalturaRequestBuilder(String service, String action, KalturaParams params) {
        super(params);
        this.service = service;
        this.action = action;
    }

    /**
     * builds a formatted property value indicates forward of response property value
     *
     * @param id           - id/number of the request in the multirequest, to which property value is binded
     * @param propertyPath - keys path to the binded response property
     * @return value pattern for the binded property (exp. [2, request number]:result:[user:name, property path]
     */
    private String formatLink(String id, String propertyPath) {
        return id + ":result:" + propertyPath.replace(".", ":");
    }


    //@Override
    public String getAction() {
        return action;
    }


    //@Override
    public KalturaParams getParams() {
        return params;
    }

    public String getService() {
        return service;
    }

    public KalturaMultiRequestBuilder add(KalturaRequestBuilder another) {
        try {
            return new KalturaMultiRequestBuilder(this, another);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return new KalturaMultiRequestBuilder();
    }

    public String getUrlTail() {
        StringBuilder urlBuilder = new StringBuilder("service/").append(service);
        if (!action.equals("")) {
            urlBuilder.append("/action/").append(action);
        }

        return urlBuilder.toString();
    }

    public KalturaRequestBuilder setCompletion(OnCompletion onCompletion) {
        this.onCompletion = onCompletion;
        return this;
    }

    public KalturaRequestBuilder setParam(String key, Object value) {
        if (params != null) {
            params.put(key, value);
        }
        return this;
    }

    public KalturaRequestBuilder setId(String id) {
        this.id = id;
        return this;
    }

    @Override
    public String getTag() {
        return action;
    }
}














