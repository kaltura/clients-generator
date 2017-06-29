package com.kaltura.client.utils.request;

import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.response.base.ResponseElement;

public class NullRequestBuilder extends RequestBuilder<Void> {

    public NullRequestBuilder() {
    	super(Void.class);
    }

    public NullRequestBuilder(String service, String action, Params params) {
        super(Void.class, service, action, params);
    }

	@Override
    public void onComplete(ResponseElement response) {
        APIException error = null;
        
        if(!response.isSuccess()) {
        	error = generateErrorResponse(response);
        }

        if(onCompletion != null) {
        	onCompletion.onComplete(null, error);
        }
    }
}














