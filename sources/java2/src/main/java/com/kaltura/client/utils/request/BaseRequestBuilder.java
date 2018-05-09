package com.kaltura.client.utils.request;

import com.kaltura.client.Client;
import com.kaltura.client.Configuration;
import com.kaltura.client.FileHolder;
import com.kaltura.client.Files;
import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.APIConstants;
import com.kaltura.client.utils.EncryptionUtils;
import com.kaltura.client.utils.GsonParser;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;
import com.kaltura.client.utils.response.base.ResponseElement;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by tehilarozin on 14/08/2016.
 */
public abstract class BaseRequestBuilder<ReturnedType, SelfType> extends RequestBuilderData<SelfType> implements RequestElement<ReturnedType> {

	protected Class<ReturnedType> type;
    protected String url;
    protected Files files = null;
    protected HashMap<String, String> headers;
    private ConnectionConfiguration connectionConfig;

    /**
     * callback for the parsed response.
     */
    protected OnCompletion<Response<ReturnedType>> onCompletion;

    protected BaseRequestBuilder(Class<ReturnedType> type) {
    	super();
    	this.type = type;
    }

    protected abstract String getUrlTail();

    public abstract String getTag();

	public Class<?> getType() {
		return type;
	}

    @Override
    public String getMethod() {
        return "POST";
    }

    @Override
    public String getBody() {
        return params.toString();
    }

	protected Params getParams() {
        return params;
    }
	
    public void setParams(Map<String, Object> objParams) {
        params.putAll(objParams); // !! null params should be checked - should not appear in request body or be presented as empty string.
	}

    public BaseRequestBuilder<ReturnedType, SelfType> setFile(String key, FileHolder value) {
        if (files != null) {
            files.add(key, value);
        }
        return this;
    }

    @Override
    public HashMap<String, String> getHeaders() {
        return headers;
    }

    @Override
    public Files getFiles() {
        return files;
    }

    public void setHeaders(HashMap<String, String> headers) {
        this.headers = headers;
    }

    public void setHeaders(String ... nameValueHeaders){
        for (int i = 0 ; i < nameValueHeaders.length-1 ; i+=2){
            this.headers.put(nameValueHeaders[i], nameValueHeaders[i+1]);
        }
    }

    @Override
    public String getContentType() {
        return headers != null ? headers.get(APIConstants.HeaderContentType) : APIConstants.DefaultContentType;
    }

    @Override
    public String getUrl() {
        return url;
    }

    @Override
    public ConnectionConfiguration config() {
        return connectionConfig;
    }

    /**
     * Builds the final list of parameters including the default params and the configured params.
     *
     * @param configurations client configurations
     * @param addSignature add signature
     * @return Params
     */
    protected Params prepareParams(Client configurations, boolean addSignature) {

        if(params == null){
            params = new Params();
        }

        // add default params:
        //params.add("format", configurations.getConnectionConfiguration().getServiceResponseTypeFormat());
        params.add("ignoreNull", true);
        if(configurations != null) {
            params.putAll(configurations.getClientConfiguration());
            params.putAll(configurations.getRequestConfiguration());
        }
        if (addSignature) {
            params.add("kalsig", EncryptionUtils.encryptMD5(params.toString()));
        }
        return params;
    }

    private void prepareUrl(String endPoint) {
        if (url == null) {
            StringBuilder urlBuilder = new StringBuilder(endPoint.replaceAll("/$", ""))
            .append("/")
            .append(APIConstants.UrlApiVersion);

            urlBuilder.append(getUrlTail());
            url = urlBuilder.toString();
        }
    }

    public RequestElement<ReturnedType> build(final Client client) {
        return build(client, false);
    }

    @SuppressWarnings("unchecked")
	@Override
    final public Response<ReturnedType> parseResponse(ResponseElement response) {
        ReturnedType result = null;
        APIException error = null;

        if (!response.isSuccess()) {
            error = generateErrorResponse(response);
        } else {
            try {
                result = (ReturnedType) parse(response.getResponse());
            } catch (APIException e) {
                error = e;
            }
        }

        return new Response<ReturnedType>(result, error);
    }

    @Override
    public void onComplete(Response<ReturnedType> response) {
        if(onCompletion != null) {
            onCompletion.onComplete((Response<ReturnedType>) response);
        }
    }

    protected Object parse(String response) throws APIException {
    	if(response.length() == 0 || response.toLowerCase().equals("null")) {
    		return null;
    	}
    	return GsonParser.parseObject(response, type);
    }
    
    protected APIException generateErrorResponse(ResponseElement response) {
    	APIException exception = new APIException(response.getError().getMessage());
    	exception.setCode(String.valueOf(response.getError().getCode()));
    	return exception;
    }
    
    public RequestElement<ReturnedType> build(final Client client, boolean addSignature) {
        connectionConfig = client != null ? client.getConnectionConfiguration() : Configuration.getDefaults();

        prepareParams(client, addSignature);
        prepareHeaders(connectionConfig);
        prepareUrl(connectionConfig.getEndpoint());

        return this;
    }

    protected void prepareHeaders(ConnectionConfiguration config) {
        if (headers == null) {
            headers = new HashMap<String, String>();
        }
        addDefaultHeaders();

        if (!headers.containsKey(APIConstants.HeaderAcceptEncoding) && config.getAcceptGzipEncoding()) {
            headers.put(APIConstants.HeaderAcceptEncoding, APIConstants.HeaderEncodingGzip);
        }
    }

    private void addDefaultHeaders() {
        if(!headers.containsKey(APIConstants.HeaderAccept)) {
            headers.put(APIConstants.HeaderAccept, "application/json");
        }
        if(!headers.containsKey(APIConstants.HeaderAcceptCharset)) {
            headers.put(APIConstants.HeaderAcceptCharset, "utf-8,ISO-8859-1;q=0.7,*;q=0.5");
        }
    }


}














