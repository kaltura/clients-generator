package com.kaltura.client.utils.request;

import com.kaltura.client.types.APIException;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.GsonParser;

public abstract class ListResponseRequestBuilder<T> extends RequestBuilder<ListResponse<T>> {

    private Class<T> type;

    public ListResponseRequestBuilder(Class<T> type, String service, String action) {
        super(null, service, action);
    	this.type = type;
    }

	public Class<?> getType() {
        return ListResponse.class;
	}

	public Class<T> getRawType(){
        return type;
    }

    protected Object parse(String response) throws APIException {
    	return GsonParser.parseListResponse(response, type);
    }
}














