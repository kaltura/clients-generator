package com.kaltura.client.utils.request;

import com.kaltura.client.Files;
import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.GsonParser;

public class ListResponseRequestBuilder<T> extends RequestBuilder<ListResponse<T>> {

	private Class<T> type;

    public ListResponseRequestBuilder(Class<T> type, String service, String action, Params params) {
        super(service, action, params);
    	this.type = type;
    }

    public ListResponseRequestBuilder(Class<T> type, String service, String action, Params params, Files files) {
        super(service, action, params, files);
    	this.type = type;
    }

	public Class<?> getType() {
		return type;
	}

    protected Object parse(String response) throws APIException {
    	return GsonParser.parseListResponse(response, type);
    }
}














