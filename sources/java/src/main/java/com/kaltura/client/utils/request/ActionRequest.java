package com.kaltura.client.utils.request;

import com.kaltura.client.KalturaParams;
import com.kaltura.client.utils.response.ResponseType;
import com.kaltura.client.utils.response.base.GeneralResponse;


/**
 * Created by tehilarozin on 14/08/2016.
 */
public class ActionRequest extends ActionBase<ResponseType> {

    String service;
    String action;
    String id = null;

    public ActionRequest(String service, String action, KalturaParams params) {
        super(params);
        this.service = service;
        this.action = action;
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
    public String getAction() {
        return action;
    }

    @Override
    public void onComplete(GeneralResponse<ResponseType> response) {
        if(onCompletion != null){
            onCompletion.onComplete(response);
        }
    }

    public ActionRequest setParam(String key, Object value) {
        if (params != null) {
            params.put(key, value);
        }
        return this;
    }

    public ActionRequest setId(String id){
        this.id = id;
        return this;
    }

    public String getId() {
        return id;
    }
}














