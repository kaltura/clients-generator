package com.kaltura.client.utils.request;

import java.util.List;

import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.GsonParser;

public abstract class ArrayRequestBuilder<T> extends RequestBuilder<List<T>> {

	private Class<T> type;

    public ArrayRequestBuilder(Class<T> type, String service, String action) {
        super(null, service, action);
    	this.type = type;
    }

	public Class<?> getType() {
		return type;
	}

    protected Object parse(String response) throws APIException {
    	return GsonParser.parseArray(response, type);
    }
}














