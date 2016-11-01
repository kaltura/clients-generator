package com.kaltura.client.utils.request;

import com.kaltura.client.*;
import com.kaltura.client.utils.EncryptionUtils;
import com.kaltura.client.utils.KalturaAPIConstants;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;
import com.kaltura.client.utils.response.base.MultiResponse;

import java.util.HashMap;
import java.util.LinkedHashMap;
import java.util.UUID;

/**
 * Created by tehilarozin on 01/09/2016.
 */
public class RequestBuilder implements RequestElement{

    private static final String TAG = "RequestBuilder";

    public static final String MULTIREQUEST_ACTION = "multirequest";
    public static final String DEFAULT_CONTENT_TYPE = "application/json";


    //RequestElement actionElement; // service+action_name, params >> url, body
  //  ActionBase action;
    private HashMap<String, String> headers;
    private String id;
    private String service;
    private String action;
    private String url;
    private boolean addSignature;

    private KalturaParams params;
    private OnCompletion onCompletion;
    private LinkedHashMap<String, RequestBuilder> requests;
    private ConnectionConfiguration connectionConfig;


    /*public RequestBuilder(ActionBase action) {
        this.action = action;
    }
*/

    @Override
    public String tag() {
        return action;
    }

    public ConnectionConfiguration config() {
        return connectionConfig;
    }

    @Override
    public void onComplete(GeneralResponse response) {
        if(onCompletion!= null){
            onCompletion.onComplete(response);
        }
    }

    private void setId(String id) {
        this.id = id;
    }

    public String getId() {
        return id;
    }

    private void addParam(String key, String value) {
        params.add(key, value);
    }

    private OnCompletion buildCompletion(final OnCompletion onCompletion) {
        if (requests.size() == 1 && onCompletion != null) {
            return onCompletion;

        } else if (requests.size() > 1 && onCompletion != null) {
            return new OnCompletion<GeneralResponse>() {
                @Override
                public void onComplete(GeneralResponse response) {
                    for (String key : requests.keySet()) {
                        RequestBuilder request = requests.get(key);
                        if (!response.isSuccess()) {
                            request.onComplete(response);
                        }

                        GeneralResponse requestResponse = response.newBuilder()
                                .result(((MultiResponse) response.getResult()).getAt(key))
                                .build();

                        request.onComplete(requestResponse);
                    }

                }
            };
        }
        return null;
    }

    @Override
    public String getContentType() {
        return headers != null ? headers.get(KalturaAPIConstants.HeaderContentType) : DEFAULT_CONTENT_TYPE;
    }

    @Override
    public String getMethod() {
        return "POST";
    }

    @Override
    public String getUrl() {
        return url;
    }

    @Override
    public String getBody() {
        return params.toString();
    }

    @Override
    public HashMap<String, String> getHeaders() {
        return headers;
    }

    public Builder newBuilder(){
        return new Builder(this);
    }



    public static class Builder {

        private String service;
        private String action;
        private KalturaParams params;
        private String id = null;
        private HashMap<String, String> headers;
        private boolean addSignature;
        private OnCompletion onCompletion;
        private LinkedHashMap<String, RequestBuilder> calls;
        private int lastId = 0;

        public Builder(){
        }

        public Builder(String service, String action, KalturaParams params){
            this.service = service;
            this.action = action;
            this.params = params;
        }

        public Builder (RequestBuilder request){
            this.action = request.action;
            this.service = request.service;
            this.calls = request.requests;
            this.params = request.params;
            this.headers = request.headers;
            this.onCompletion = request.onCompletion;
            lastId = calls.size()-1;
        }

        /**
         * builds a formatted property value indicates forward of response property value
         * @param id - id/number of the request in the multirequest, to which property value is binded
         * @param propertyPath - keys path to the binded response property
         * @return value pattern for the binded property (exp. [2, request number]:result:[user:name, property path]
         */
        private String formatLink(String id, String propertyPath) {
            return id + ":result:" + propertyPath.replace(".", ":");
        }
        public Builder setAction(String action){
            this.action = action;
            return this;
        }

        public Builder addSignature(){
            this.addSignature = true;
            return this;
        }

        public Builder setHeaders(String ... nameValueHeaders){
            for (int i = 0 ; i < nameValueHeaders.length-1 ; i+=2){
                this.headers.put(nameValueHeaders[i], nameValueHeaders[i+1]);
            }
            return this;
        }



        /*
        * kalturaClientBase.connectionConfiguration.getAcceptGzipEncoding())
        * Headers.of(KalturaAPIConstants.HeaderAccept, "application/json",
                KalturaAPIConstants.HeaderAcceptCharset, "utf-8,ISO-8859-1;q=0.7,*;q=0.5");
if(acceptEncoding) {
            headers.newBuilder().set(KalturaAPIConstants.HeaderAcceptEncoding, KalturaAPIConstants.HeaderEncodingGzip);
        }
        * */

        public Builder setHeaders(HashMap<String, String> headers){
            this.headers.putAll(headers);
            return this;
        }

        public Builder add(RequestBuilder... requests) {
            if (calls == null) {
                calls = new LinkedHashMap<>();

                /*if(action != null){
                    RequestBuilder first = new RequestBuilder().
                }*/
            }

            for (RequestBuilder request : requests) {
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

        public Builder addParam(String key, String value){
            if(params == null){
                params = new KalturaParams();
            }
            params.add(key, value);
            return this;
        }

        private String generateId(String action) {
            return UUID.randomUUID().toString()+"::"+action;
        }

        public Builder setCompletion(OnCompletion onCompletion){
            this.onCompletion = onCompletion;
            return this;
        }

        private KalturaParams prepareParams(KalturaClientBase configurations) {

            if(params == null){
                params = new KalturaParams();
            }

            // add default params:
            //params.add("format", configurations.getConnectionConfiguration().getServiceResponseTypeFormat());
            params.add("ignoreNull", true);
            if(configurations != null) {
                params.putAll(configurations.getClientConfiguration());
                params.putAll(configurations.getRequestConfiguration());
            }
            if (this.addSignature) {
                params.add("kalsig", EncryptionUtils.encryptMD5(params.toString()));
            }
            return params;
        }

/*        public OnCompletion getCompletion() {
            new OnCompletion<GeneralResponse>() {
                @Override
                public void onComplete(GeneralResponse response) {
                    if()
                }
            }
            if(calls.size() <= 1) {
                return onCompletion;

            } else {
                for (String key : calls.keySet()) {
                    RequestBuilder request = calls.get(key);
                    if (!response.isSuccess()) {
                        request.onComplete(response);
                    }

                    GeneralResponse requestResponse = response.newBuilder()
                            .result(((MultiResponse)response.getResult()).getAt(key))
                            .build();

                    request.onComplete(requestResponse);
                }
            }


            return onCompletion;
        }*/

        public Builder link(RequestBuilder sourceRequest, int destRequestIdx, String sourceKey, String destKey) {
            try {
                return link(sourceRequest, calls.get(calls.keySet().toArray()[destRequestIdx]), sourceKey, destKey);
            } catch (NullPointerException | IndexOutOfBoundsException e) {
                KalturaLogger.getLogger(TAG).error("failed to link requests. ", e);
            }
            return this;
        }

        public Builder link(RequestBuilder sourceRequest, RequestBuilder destRequest, String sourceKey, String destKey) {
            destRequest.addParam(destKey, formatLink(sourceRequest.getId(), sourceKey));
            return this;
        }

        public String setUrl(String endPoint) {
            StringBuilder urlBuilder = new StringBuilder(endPoint)
                    .append(KalturaAPIConstants.UrlApiVersion);

            if (service == null || service.equals("")) {
                urlBuilder.append(params.toQueryString());

            } else {
                urlBuilder.append("service/").append(service);
                if (!action.equals("")) {
                    urlBuilder.append("/action/").append(action);
                }
            }

            return urlBuilder.toString();
        }

        public RequestBuilder build(KalturaClient client){
            boolean hasMany = calls.size() > 1;
            RequestBuilder request = new RequestBuilder();
            request.action = hasMany ? "" : this.action;
            request.service = hasMany ? MULTIREQUEST_ACTION : this.service;
           // request.id = generateId(hasMany ? request.service : request.action);
            request.params = this.params;// prepareParams(client);
            request.connectionConfig = client == null ? ConnectionConfiguration.getDefaults() : client.getConnectionConfiguration();
            request.requests = this.calls;
            request.url = setUrl(client == null ? "" : client.getConnectionConfiguration().getEndpoint());
            request.onCompletion = this.onCompletion;

            return request;
        }

    }

    /*public RequestBuilder build(KalturaClient client){
        request.action = hasMany ? "" : this.action;
        request.service = hasMany ? MULTIREQUEST_ACTION : this.service;
        // request.id = generateId(hasMany ? request.service : request.action);
        request.params = prepareParams(client);
        request.connectionConfig = client == null ? ConnectionConfiguration.getDefaults() : client.getConnectionConfiguration();
        request.requests = this.calls;
        request.url = setUrl(client == null ? "" : client.getConnectionConfiguration().getEndpoint());
        request.buildCompletion(this.onCompletion);
    }*/



}
