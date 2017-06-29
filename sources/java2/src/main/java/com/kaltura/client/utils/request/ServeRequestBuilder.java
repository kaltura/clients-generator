package com.kaltura.client.utils.request;

import com.kaltura.client.Client;
import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.response.base.ResponseElement;

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

		url = client.getConnectionConfiguration().getEndpoint() + "api_v3";
		url += "/service/" + service + "/action/" + action;
		url += "?" + kParams.toQueryString();
		
		return this;
    }
	
	@Override
    public void onComplete(ResponseElement response) {
        if(onCompletion != null) {
        	onCompletion.onComplete(response.getResponse(), null);
        }
    }
}














