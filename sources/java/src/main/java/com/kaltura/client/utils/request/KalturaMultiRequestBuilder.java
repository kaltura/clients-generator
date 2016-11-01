package com.kaltura.client.utils.request;

import com.kaltura.client.KalturaLogger;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;
import com.kaltura.client.utils.response.base.MultiResponse;

import java.util.LinkedHashMap;

/**
 * Created by tehilarozin on 15/08/2016.
 */
public class KalturaMultiRequestBuilder extends BaseRequestBuilder {
    private static final String TAG = "KalturaMultiRequestBuilder";

    /**
     * the service name in the request url: "...service/multirequest"
     */
    public static final String MULTIREQUEST_ACTION = "multirequest";

    /**
     * Holds the requests contained within.
     * will be used in case a completion was not provided on the multirequest itself
     * and the completion for each inner request should be activated
     */
    private LinkedHashMap<String, KalturaRequestBuilder> calls;
    private int lastId = 0;


    public KalturaMultiRequestBuilder() {
        super();
    }

    /**
     * constructs instance and fill it with requests
     * @param requests
     */
    public KalturaMultiRequestBuilder(KalturaRequestBuilder... requests) {
        super();
        add(requests);
    }

    /**
     * adds 1 or more single request to be activated as part of the multrequest
     * @param requests
     * @return
     */
    public KalturaMultiRequestBuilder add(KalturaRequestBuilder... requests) {
        if (calls == null) {
            calls = new LinkedHashMap<>();
        }

        for (KalturaRequestBuilder request : requests) {
            lastId++;
            String reqId = lastId + "";
            request.params.add("service", request.service);
            request.params.add("action", request.action);
            params.add(reqId, request.params);
            calls.put(reqId, request);
            request.setId(reqId);
        }

        return this;
    }

    /**
     * Adds the requests from kalturaMultiRequestBuilder parameter to the end of the current requests list
     * @param kalturaMultiRequestBuilder - another multirequests to copy requests from
     * @return
     */
    public KalturaMultiRequestBuilder add(KalturaMultiRequestBuilder kalturaMultiRequestBuilder) {
        for (String reqId : kalturaMultiRequestBuilder.calls.keySet()) {
            KalturaRequestBuilder request = kalturaMultiRequestBuilder.calls.get(reqId);
            int last = calls.size() + 1;
            calls.put(last + "", request);
            params.add(last + "", request.params);
        }

        return this;
    }

    /**
     * Sets the callback for the Multirequest parsed response.
     *
     * @param onCompletion
     * @return
     */
    public KalturaMultiRequestBuilder setCompletion(OnCompletion onCompletion) {
        this.onCompletion = onCompletion;
        return this;
    }

    /**
     * Calls the provided callback when response is ready.
     * In case the callback was not defined, response will be chopped to response per request
     * within the multi request, and will be passed to the single request completion callback.
     *
     * @param response - MultiResponse wrapped in {@link GeneralResponse}
     */
    @Override
    public void onComplete(final GeneralResponse response) {
        if (onCompletion != null) {
            onCompletion.onComplete(response);

        } else {
            for (String key : calls.keySet()) {
                KalturaRequestBuilder kalturaRequestBuilder = calls.get(key);
                if (!response.isSuccess()) {
                    kalturaRequestBuilder.onComplete(response);
                    return;
                }
//add headers to sub responses.
                GeneralResponse requestResponse = response.newBuilder()
                        .result(((MultiResponse)response.getResult()).getAt(key))
                        .build();

                kalturaRequestBuilder.onComplete(requestResponse);
            }
        }
    }

    /**
     * Returns a formatted string for the linked property's value.
     *
     * @param id - id/number of the request in the multirequest, to which property value is binded (starts from index <strong>1</strong>)
     * @param propertyPath - keys path to the binded response property
     * @return value pattern for the binded property (exp. [2, request number]:result:[user:name, property path]
     */
    private static String formatLink(String id, String propertyPath) {
        return id + ":result:" + propertyPath.replace(".", ":");
    }

    /**
     * @return - The url postfix for Multirequest
     */
    public String getUrlTail() {
        return "service/" + MULTIREQUEST_ACTION;
    }

    public String getAction() {
        return MULTIREQUEST_ACTION;
    }

    /**
     * Binds request param value to another request's response value.
     *
     * @param sourceRequest - the request from which response value should be taken from
     * @param destRequestIdx - the index of the destination request in the multirequet list
     * @param sourceKey - the properties path in the response to the needed value (exp. user.loginSession.ks)
     * @param destKey - the property that will get the result from the source request
     * @return
     */
    public KalturaMultiRequestBuilder link(KalturaRequestBuilder sourceRequest, int destRequestIdx, String sourceKey, String destKey) {
        try {
            return link(sourceRequest, calls.get(calls.keySet().toArray()[destRequestIdx]), sourceKey, destKey);
        } catch (NullPointerException | IndexOutOfBoundsException e) {
            KalturaLogger.getLogger(TAG).error("failed to link requests. ", e);
        }
        return this;
    }

    /**
     * Binds request param value to another request's response value.
     * In request body the value of the destKey parameter will have a formatted
     * string indicating the source request index in the list and the path in the
     * response to bind as value
     *
     * @param sourceRequest - the request from which response value will be taken
     * @param destRequest - the request to bind to it's parameter.
     * @param sourceKey - propeties path in the source request response.
     * @param destKey - the param property to set its value as linked.
     * @return
     */
    public KalturaMultiRequestBuilder link(KalturaRequestBuilder sourceRequest, KalturaRequestBuilder destRequest, String sourceKey, String destKey) {
        destRequest.setParam(destKey, formatLink(sourceRequest.getId(), sourceKey));
        return this;
    }

    @Override
    public String getTag() {
        return MULTIREQUEST_ACTION;
    }
}

