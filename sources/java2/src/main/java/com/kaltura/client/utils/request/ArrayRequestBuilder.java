package com.kaltura.client.utils.request;

import java.util.List;

import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.GsonParser;

public abstract class ArrayRequestBuilder<T, V> extends RequestBuilder<List<T>, RequestBuilder.ListTokenizer<V>> {

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

    @SuppressWarnings("unchecked")
	public RequestBuilder.ListTokenizer<V> getTokenizer() throws APIException {
		if(id == null) {
			throw new APIException(APIException.FailureStep.OnRequest, "Request is not part of multi-request");
		}

    	MultiRequestBuilder.Tokenizer annotation = type.getAnnotation(MultiRequestBuilder.Tokenizer.class);
		return new RequestBuilder.ListTokenizer<V>((Class<V>)annotation.value(), id + ":result");
    }
}














