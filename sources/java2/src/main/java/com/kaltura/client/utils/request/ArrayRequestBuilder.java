package com.kaltura.client.utils.request;

import java.util.List;

import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.GsonParser;

public class ArrayRequestBuilder<T> extends RequestBuilder<List<T>> {

	private Class<T> type;

    public ArrayRequestBuilder(Class<T> type) {
        this(type, null, null, new Params());
    }

    public ArrayRequestBuilder(Class<T> type, String service, String action, Params params) {
        super(service, action, params);
    	this.type = type;
    }

	public Class<?> getType() {
		return type;
	}

    protected Object parse(String response) throws APIException {
    	return GsonParser.parseArray(response, type);
    }
}














