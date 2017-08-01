package com.kaltura.client.utils.request;

import com.kaltura.client.Files;
import com.kaltura.client.Logger;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.GsonParser;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;

/**
 * Created by tehilarozin on 15/08/2016.
 */
public class MultiRequestBuilder extends BaseRequestBuilder<List<Object>> {
    private static final String TAG = "MultiRequestBuilder";

    /**
     * the service name in the request url: "...service/multirequest"
     */
    public static final String MULTIREQUEST_ACTION = "multirequest";

    /**
     * Holds the requests contained within.
     * will be used in case a completion was not provided on the multirequest itself
     * and the completion for each inner request should be activated
     */
    private LinkedHashMap<String, RequestBuilder<?>> requests = new LinkedHashMap<>();
    private int lastId = 0;


    public MultiRequestBuilder() {
        super();
    }

    /**
     * constructs instance and fill it with requests
     * @param requests
     */
    public MultiRequestBuilder(RequestBuilder<?>... requests) {
        this();
        add(requests);
    }

    public MultiRequestBuilder setCompletion(OnCompletion<Response<List<Object>>> onCompletion) {
        this.onCompletion = onCompletion;
        return this;
    }

    public MultiRequestBuilder setId(String id) {
        this.id = id;
        return this;
    }

    /**
     * adds 1 or more single request to be activated as part of the multrequest
     * @param requests
     * @return
     */
    public MultiRequestBuilder add(RequestBuilder<?>... requests) {
        for (RequestBuilder<?> request : requests) {
        	add(request);
        }

        return this;
    }

    public MultiRequestBuilder add(RequestBuilder<?> request) {
        lastId++;
        String reqId = lastId + "";
        request.params.add("service", request.service);
        request.params.add("action", request.action);
        params.add(reqId, request.params);
        if(request.files != null) {
        	if(files == null) {
        		files = new Files();
        	}
        	files.add(reqId, request.files);
        }
        requests.put(reqId, request);
        request.setId(reqId);

        return this;
    }

    /**
     * Adds the requests from kalturaMultiRequestBuilder parameter to the end of the current requests list
     * @param multiRequestBuilder - another multirequests to copy requests from
     * @return
     */
    public MultiRequestBuilder add(MultiRequestBuilder multiRequestBuilder) {
        for (String reqId : multiRequestBuilder.requests.keySet()) {
            RequestBuilder<?> request = multiRequestBuilder.requests.get(reqId);
            int last = requests.size() + 1;
            requests.put(last + "", request);
            params.add(last + "", request.params);
        }

        return this;
    }

    @SuppressWarnings({ "unchecked", "rawtypes" })
    @Override
    public void onComplete(Response<List<Object>> response) {
        List<Object> results = null;
        if(response != null) {
            results = (List<Object>) response.results;
            int index = 0;
            for(RequestBuilder<?> request : requests.values()) {
                Object item = results.get(index++);
                if(item instanceof APIException) {
                    request.onComplete(new Response(null, (APIException) item));
                } else {
                    request.onComplete(new Response(item, null));
                }
            }
        }

        super.onComplete(response);
    }

    @Override
    protected Object parse(String response) throws APIException {
        List<Class<?>> list = new ArrayList<Class<?>>();
        for(RequestBuilder<?> call : requests.values()) {
            list.add(call.getType());
        }
    	return GsonParser.parseArray(response, list.toArray(new Class[requests.size()]));
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
    public MultiRequestBuilder link(RequestBuilder<?> sourceRequest, int destRequestIdx, String sourceKey, String destKey) {
        try {
            return link(sourceRequest, requests.get(requests.keySet().toArray()[destRequestIdx]), sourceKey, destKey);
        } catch (NullPointerException | IndexOutOfBoundsException e) {
            Logger.getLogger(TAG).error("failed to link requests. ", e);
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
    public MultiRequestBuilder link(RequestBuilder<?> sourceRequest, RequestBuilder<?> destRequest, String sourceKey, String destKey) {
        destRequest.link(destKey, sourceRequest.getId(), sourceKey);
        return this;
    }

    @Override
    public String getTag() {
        return MULTIREQUEST_ACTION;
    }

 }

