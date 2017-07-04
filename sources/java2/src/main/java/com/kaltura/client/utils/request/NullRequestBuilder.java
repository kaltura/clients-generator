package com.kaltura.client.utils.request;

import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;

public class NullRequestBuilder extends RequestBuilder<Void> {

    public NullRequestBuilder() {
    	super(Void.class);
    }

    public NullRequestBuilder(String service, String action, Params params) {
        super(Void.class, service, action, params);
    }
    
    @Override
    protected void complete(Object result, APIException error) {
        if(onCompletion != null) {
        	onCompletion.onComplete(null, error);
        }
    }
}














