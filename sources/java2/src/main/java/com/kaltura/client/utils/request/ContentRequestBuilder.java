package com.kaltura.client.utils.request;

import java.util.HashMap;
import java.util.Map;

import com.kaltura.client.Client;
import com.kaltura.client.Configuration;
import com.kaltura.client.Files;
import com.kaltura.client.Params;
import com.kaltura.client.enums.ResponseType;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

public class ContentRequestBuilder extends RequestBuilder<String, String, ContentRequestBuilder> {

	private RequestBuilder<?, ?, ?> requestBuilder;
	private ResponseType format;
	
    public ContentRequestBuilder(RequestBuilder<?, ?, ?> requestBuilder, ResponseType format) {
        super(String.class, requestBuilder.getService(), requestBuilder.getAction());
        this.requestBuilder = requestBuilder;
        this.format = format;
    }
	
    @Override
    protected Params prepareParams(Client client, boolean addSignature) {
    	params = requestBuilder.prepareParams(client, addSignature);
    	params.put("format", format.getValue());
    	connectionConfig = client != null ? client.getConnectionConfiguration() : Configuration.getDefaults();
    	requestBuilder.prepareHeaders(connectionConfig);
    	requestBuilder.prepareUrl(connectionConfig.getEndpoint());
    	return params;
    }
    
	@Override
    protected Object parse(String response) throws APIException {
    	return response;
    }

	@Override
    public MultiRequestBuilder add(RequestBuilder<?, ?, ?> another) throws APIException {
    	throw new APIException("Multi-request is not supported using custom format"); 
    }

	@Override
    protected Params getParams() {
        return requestBuilder.getParams();
    }

	@Override
    protected String getUrlTail() {
        return requestBuilder.getUrlTail();
    }

	@Override
	protected ContentRequestBuilder link(String destKey, String requestId, String sourceKey) {
		requestBuilder.link(destKey, requestId, sourceKey);
		return this;
    }

	@Override
	protected ContentRequestBuilder setId(String id) {
		requestBuilder.setId(id);
        return super.setId(id);
    }

	@Override
    protected String getId() {
        return requestBuilder.getId();
    }

	@Override
	public ContentRequestBuilder setCompletion(OnCompletion<Response<String>> onCompletion) {
        this.onCompletion = onCompletion;
        return this;
    }	

	@Override	
    public void setParams(Map<String, Object> objParams) {
        requestBuilder.setParams(objParams);
	}

    @Override
    public HashMap<String, String> getHeaders() {
    	return requestBuilder.getHeaders();
    }

    @Override
    public Files getFiles() {
    	return requestBuilder.getFiles();
    }

    public void setHeaders(HashMap<String, String> headers) {
    	requestBuilder.setHeaders(headers);
    }

    public void setHeaders(String ... nameValueHeaders){
    	requestBuilder.setHeaders(nameValueHeaders);
    }

    @Override
    public String getContentType() {
    	return requestBuilder.getContentType();
    }

    @Override
    public String getUrl() {
    	return requestBuilder.getUrl() + "?format=" + format.getValue();
    }

    @Override
    public ConnectionConfiguration config() {
    	return requestBuilder.config();
    }
}














