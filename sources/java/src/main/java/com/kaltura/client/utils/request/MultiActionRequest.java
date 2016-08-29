package com.kaltura.client.utils.request;

import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.ResponseType;
import com.kaltura.client.utils.response.base.GeneralResponse;
import com.kaltura.client.utils.response.base.GeneralSingleResponse;
import com.kaltura.client.utils.response.base.MultiResponse;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by tehilarozin on 15/08/2016.
 */
public class MultiActionRequest extends ActionBase<MultiResponse> {
    public static final String MULTIREQUEST_ACTION = "multirequest";

    /* re1 + re1
    *  req2 + req1
    *  re2 + re2*/

    Map<String, ActionRequest> calls;

    int id = 0;

    public MultiActionRequest() {
        super();
    }

    public MultiActionRequest(ActionRequest... requests) /*throws KalturaAPIException*/ {
        super();
        add(requests);
    }

    public  MultiActionRequest add(ActionRequest... requests) /*throws KalturaAPIException*/ {
        if (calls == null) {
            calls = new HashMap<>();
        }

        for (ActionRequest request : requests) {
            id++;
            String reqId = id+"";
            request.params.add("service", request.service);
            request.params.add("action", request.action);
            params.add(reqId, request.params);
            //files.add(reqId, request.files);
            calls.put(reqId, request);
            request.setId(reqId);
        }

        return this;
    }

    public MultiActionRequest add(MultiActionRequest multiActionRequest) /*throws KalturaAPIException*/ {
        for(String reqId : multiActionRequest.calls.keySet() ){
            ActionRequest request = multiActionRequest.calls.get(reqId);
            int last = calls.size() + 1;
            calls.put(last+"", request);
            params.add(last+"", request.params);
        }


        //params.add(multiActionRequest.params);
        //calls.putAll(multiActionRequest.calls);

        return this;
    }

    public MultiActionRequest setCompletion(OnCompletion onCompletion) {
        this.onCompletion = onCompletion;
        return this;
    }


    @Override
    public void onComplete(GeneralResponse<MultiResponse> response) {
        if(onCompletion != null){
            onCompletion.onComplete(response);
        } else {
            for (String key : calls.keySet()) {
                ActionRequest actionRequest = calls.get(key);
                actionRequest.onComplete((GeneralResponse<ResponseType>) getResponse(response, key));
            }
        }
    }

    /**
     * returns the matching result according to the request key from the response list.
     *
     * @param response - {@link MultiResponse} to get results from
     * @param key - the key id of the request
     * @return - response by request id
     */
    private Object getResponse(GeneralResponse<MultiResponse> response, String key) {
        // in case the response failed return it as it retrieved
        if(!response.isSuccess()) {
            return response;
        }

        int index = 0;
        try {
            index = Integer.valueOf(key) - 1;

        } catch (NumberFormatException e) {
            e.printStackTrace();
        }

        GeneralSingleResponse singleResponse = new GeneralSingleResponse();

        MultiResponse result = response.getResult();
        if (index >= result.size()) {
            index = result.size() - 1;
        }
        singleResponse.setResult(response.getResult().get(index));
        return singleResponse;
    }


    public String getUrlTail() {
        return "service/"+MULTIREQUEST_ACTION;
    }

    @Override
    public String getAction() {
        return MULTIREQUEST_ACTION;
    }

    public MultiActionRequest link(ActionRequest sourceRequest, ActionRequest destRequest, String sourceKey, String destKey) {
        destRequest.setParam(destKey, path(sourceRequest.getId(), sourceKey));
        return this;
    }
}

