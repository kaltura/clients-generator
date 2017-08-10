package com.kaltura.client.utils.request;

import com.kaltura.client.Files;
import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.GsonParser;

public class ListResponseRequestBuilder<T, U, V> extends LinkedRequest<ListResponse<T>, U, ListResponse.Tokenizer<V>> {

    private Class<T> type;

    public ListResponseRequestBuilder(Class<T> type, String service, String action, Params params, Files files) {
        super(null, service, action, params, files);
    	this.type = type;
    }

    public ListResponseRequestBuilder(Class<T> type, String service, String action, Params params) {
        this(type, service, action, params, null);
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

	@SuppressWarnings("unchecked")
	public ListResponse.Tokenizer<V> getTokenizer() throws APIException {
		if(id == null) {
			throw new APIException(APIException.FailureStep.OnRequest, "Request is not part of multi-request");
		}

    	MultiRequestBuilder.Tokenizer annotation = type.getAnnotation(MultiRequestBuilder.Tokenizer.class);
		return new Request.ListResponseTokenizer<V>((Class<V>)annotation.value(), id + ":result");
    }
}














