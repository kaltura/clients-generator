package com.kaltura.client.utils.request;

import com.kaltura.client.Files;
import com.kaltura.client.Params;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;


public class LinkedRequest<T, U, V> extends Request<T, V> {

	public static enum NoPrimitiveArguments{
		
	}
	
    public LinkedRequest(Class<T> type, String service, String action, Params params, Files files) {
        super(type, service, action, params, files);
    }

    public LinkedRequest(Class<T> type, String service, String action, Params params) {
        this(type, service, action, params, null);
    }

    public LinkedRequest<T, U, V> link(U destKey, String token) {
        params.add(destKey.toString(), token);
        return this;
    }

    public LinkedRequest<T, U, V> setCompletion(OnCompletion<Response<T>> onCompletion) {
        super.setCompletion(onCompletion);
        return this;
    }
}














