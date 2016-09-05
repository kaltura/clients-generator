package com.kaltura.client.utils.request;

import com.kaltura.client.KalturaParams;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;


/**
 * Created by tehilarozin on 14/08/2016.
 */

public class ActionRequest extends ActionBase {

    String service;
    String action;

    public ActionRequest() {
    }

    public ActionRequest(String service, String action, KalturaParams params) {
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

    public MultiActionRequest add(ActionRequest another) {
        try {
            return new MultiActionRequest(this, another);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return new MultiActionRequest();
    }

    public String getUrlTail() {
        StringBuilder urlBuilder = new StringBuilder("service/").append(service);
        if (!action.equals("")) {
            urlBuilder.append("/action/").append(action);
        }

        return urlBuilder.toString();
    }

    @Override
    public ActionRequest setCompletion(OnCompletion/*<GeneralResponse>*/ onCompletion) {
        this.onCompletion = onCompletion;
        return this;
    }

    @Override
    public void onComplete(GeneralResponse response) {
        if (onCompletion != null) {
            onCompletion.onComplete(response);
        }
    }

    public ActionRequest setParam(String key, Object value) {
        if (params != null) {
            params.put(key, value);
        }
        return this;
    }

    public ActionRequest setId(String id) {
        this.id = id;
        return this;
    }

    @Override
    protected String getTag() {
        return action;
    }
}














