package com.kaltura.client.utils.request;

import com.kaltura.client.Client;
import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.APIConstants;

public abstract class ServeRequestBuilder extends RequestBuilder<String, String, ServeRequestBuilder> {

    public ServeRequestBuilder(String service, String action) {
        super(String.class, service, action);
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
    public RequestElement<String> build(final Client client, boolean addSignature) {
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

	@Override
	public String getTokenizer() throws APIException {
		throw new APIException(APIException.FailureStep.OnRequest, "Served content response can not be used as multi-request token");
	}
}














