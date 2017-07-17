package com.kaltura.client.utils.request;

import com.kaltura.client.Client;
import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.APIConstants;

public class ServeRequestBuilder extends RequestBuilder<String> {

    public ServeRequestBuilder() {
    	super(String.class);
    }

    public ServeRequestBuilder(String service, String action, Params params) {
        super(String.class, service, action, params);
    }

	@Override
    public String getMethod() {
    	return "GET";
    }

    @Override
    public String getBody() {
        return null;
    }

	@Override
    public RequestElement build(final Client client, boolean addSignature) {
		Params kParams = prepareParams(client, true);
		prepareHeaders(client.getConnectionConfiguration());
		String endPoint = client.getConnectionConfiguration().getEndpoint().replaceAll("/$", "");
        StringBuilder urlBuilder = new StringBuilder(endPoint)
        .append("/")
        .append(APIConstants.UrlApiVersion)
        .append("/service/")
        .append(service)
        .append("/action/")
        .append(action)
        .append("?")
        .append(kParams.toQueryString());
        
        url = urlBuilder.toString();
		
		return this;
    }
	
	@Override
    protected Object parse(String response) throws APIException {
    	return response;
    }
}














