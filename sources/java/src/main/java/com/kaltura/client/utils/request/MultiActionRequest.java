package com.kaltura.client.utils.request;

import com.kaltura.client.KalturaLogger;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;
import com.kaltura.client.utils.response.base.MultiResponse;

import java.util.LinkedHashMap;

/**
 * Created by tehilarozin on 15/08/2016.
 */
public class MultiActionRequest extends ActionBase {
    public static final String MULTIREQUEST_ACTION = "multirequest";
    private static final String TAG = "MultiActionRequest";

    /* re1 + re1
    *  req2 + req1
    *  re2 + re2*/

    LinkedHashMap<String, ActionRequest> calls;

    int lastId = 0;

    public MultiActionRequest() {
        super();
    }

    public MultiActionRequest(ActionRequest... requests) {
        super();
        add(requests);
    }

    public MultiActionRequest add(ActionRequest... requests) {
        if (calls == null) {
            calls = new LinkedHashMap<>();
        }

        for (ActionRequest request : requests) {
            lastId++;
            String reqId = lastId + "";
            request.params.add("service", request.service);
            request.params.add("action", request.action);
            params.add(reqId, request.params);
            //files.add(reqId, request.files);
            calls.put(reqId, request);
            request.setId(reqId);
        }

        return this;
    }

    public MultiActionRequest add(MultiActionRequest multiActionRequest) {
        for (String reqId : multiActionRequest.calls.keySet()) {
            ActionRequest request = multiActionRequest.calls.get(reqId);
            int last = calls.size() + 1;
            calls.put(last + "", request);
            params.add(last + "", request.params);
        }

        return this;
    }

    public MultiActionRequest setCompletion(OnCompletion onCompletion) {
        this.onCompletion = onCompletion;
        return this;
    }


    /**
     * Pass response of multiAction request to the completion block.
     * in case multirequest has a completion callback the full response will be passed to it.
     * otherwise response will be chopped to response per request within the multi request
     * and will be passed to its completion block.
     *
     * @param response - MultiResponse wrapped in {@link GeneralResponse}
     */
   // @Override
    public void onComplete(final GeneralResponse response) {
        if (onCompletion != null) {
            onCompletion.onComplete(response);

        } else {
            for (String key : calls.keySet()) {
                ActionRequest actionRequest = calls.get(key);
                if (!response.isSuccess()) {
                    actionRequest.onComplete(response);
                    return;
                }

                GeneralResponse requestResponse = response.newBuilder()
                        .result(((MultiResponse)response.getResult()).getAt(key))
                        .build();

                actionRequest.onComplete(requestResponse);
            }
        }
    }



    public String getUrlTail() {
        return "service/" + MULTIREQUEST_ACTION;
    }

    //@Override
    public String getAction() {
        return MULTIREQUEST_ACTION;
    }

    public MultiActionRequest link(ActionRequest sourceRequest, int destRequestIdx, String sourceKey, String destKey) {
        try {
            return link(sourceRequest, calls.get(calls.keySet().toArray()[destRequestIdx]), sourceKey, destKey);
        } catch (NullPointerException | IndexOutOfBoundsException e) {
            KalturaLogger.getLogger(TAG).error("failed to link requests. ", e);
        }
        return this;
    }

    public MultiActionRequest link(ActionRequest sourceRequest, ActionRequest destRequest, String sourceKey, String destKey) {
        destRequest.setParam(destKey, path(sourceRequest.getId(), sourceKey));
        return this;
    }

    @Override
    protected String getTag() {
        return MULTIREQUEST_ACTION;
    }
}

