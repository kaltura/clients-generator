package com.kaltura.client.utils.request;

import com.kaltura.client.Files;
import com.kaltura.client.Logger;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.GsonParser;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;


public class MultiRequestBuilder extends BaseRequestBuilder<List<Object>, MultiRequestBuilder> {
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
    private LinkedHashMap<String, RequestBuilder<?, ?, ?>> requests = new LinkedHashMap<>();
    private int lastId = 0;

    @Target(value={ElementType.TYPE})
    @Retention(RetentionPolicy.RUNTIME)
    public @interface Tokenizer {
        Class<?> value();
    }


    public MultiRequestBuilder() {
        super(null);
    }

    /**
     * constructs instance and fill it with requests
     * @param requests requests list
     */
    public MultiRequestBuilder(RequestBuilder<?, ?, ?>... requests) {
        this();
        add(requests);
    }

    public MultiRequestBuilder setCompletion(OnCompletion<Response<List<Object>>> onCompletion) {
        this.onCompletion = onCompletion;
        return this;
    }

    /**
     * adds 1 or more single request to be activated as part of the multrequest
     * @param requests requests list
     * @return MultiRequestBuilder
     */
    public MultiRequestBuilder add(RequestBuilder<?, ?, ?>... requests) {
        for (RequestBuilder<?, ?, ?> request : requests) {
        	add(request);
        }

        return this;
    }

    public MultiRequestBuilder add(RequestBuilder<?, ?, ?> request) {
        lastId++;
        String reqId = lastId + "";
        request.params.add("service", request.getService());
        request.params.add("action", request.getAction());
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
     * @param multiRequestBuilder another multirequests to copy requests from
     * @return MultiRequestBuilder
     */
    public MultiRequestBuilder add(MultiRequestBuilder multiRequestBuilder) {
        for (String reqId : multiRequestBuilder.requests.keySet()) {
        	RequestBuilder<?, ?, ?> request = multiRequestBuilder.requests.get(reqId);
            int last = requests.size() + 1;
            requests.put(last + "", request);
            params.add(last + "", request.params);
        }

        return this;
    }

    @SuppressWarnings({ "unchecked", "rawtypes" })
    @Override
    public void onComplete(Response<List<Object>> response) {

        if(response != null) {
            APIException topError = response.error;

            int index = 0;

            for(RequestBuilder<?, ?, ?> request : requests.values()) {
                if(request.onCompletion != null) {
                    if (topError != null) {
                        request.onComplete(new Response(null, topError));

                    } else {
                        Object item = response.results.get(index++);
                        APIException error = null;

                        if (item instanceof APIException) {
                            error = (APIException) item;
                            item = null;
                        }

                        request.onComplete(new Response(item, error));
                    }
                }
            }
        }

        super.onComplete(response);
    }

    @Override
    protected Object parse(String response) throws APIException {
        List<Class<?>> list = new ArrayList<Class<?>>();
        for(RequestBuilder<?, ?, ?> call : requests.values()) {
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
     * @param sourceRequestIdx the index of the request from which response value should be taken from
     * @param destRequestIdx the index of the destination request in the multirequest list
     * @param sourceKey the properties path in the response to the needed value (exp. user.loginSession.ks)
     * @param destKey the property that will get the result from the source request
     * @return MultiRequestBuilder
     */
    public MultiRequestBuilder link(int sourceRequestIdx, int destRequestIdx, String sourceKey, String destKey) {
        try {
            return link(getRequestAt(sourceRequestIdx), getRequestAt(destRequestIdx), sourceKey, destKey);

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
     * @param sourceRequest the request from which response value will be taken
     * @param destRequest the request to bind to it's parameter.
     * @param sourceKey propeties path in the source request response.
     * @param destKey the param property to set its value as linked.
     * @return MultiRequestBuilder
     */
    public MultiRequestBuilder link(RequestBuilder<?, ?, ?> sourceRequest, RequestBuilder<?, ?, ?> destRequest, String sourceKey, String destKey) {
        if(sourceRequest == null || destRequest == null){
            throw new NullPointerException("link requests can't be null");
        }
        destRequest.link(destKey, sourceRequest.getId(), sourceKey);
        return this;
    }

    @Override
    public String getTag() {
        return MULTIREQUEST_ACTION;
    }

    @SuppressWarnings("rawtypes")
	private RequestBuilder getRequestAt(int index) throws IndexOutOfBoundsException{
        Object[] requestsKeys = requests.keySet().toArray();
        return requests.get(requestsKeys[index]);
    }

 }

