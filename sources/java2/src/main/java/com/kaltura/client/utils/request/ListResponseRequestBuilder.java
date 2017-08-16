package com.kaltura.client.utils.request;

import com.kaltura.client.types.APIException;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.GsonParser;

public abstract class ListResponseRequestBuilder<RS, TK, S> extends RequestBuilder<ListResponse<RS>, ListResponse.Tokenizer<TK>, S> {

    private Class<RS> type;

    public ListResponseRequestBuilder(Class<RS> type, String service, String action) {
        super(null, service, action);
    	this.type = type;
    }

	public Class<?> getType() {
        return ListResponse.class;
	}

	public Class<RS> getRawType(){
        return type;
    }

    protected Object parse(String response) throws APIException {
    	return GsonParser.parseListResponse(response, type);
    }

	@SuppressWarnings("unchecked")
	public ListResponse.Tokenizer<TK> getTokenizer() throws APIException {
		if(id == null) {
			throw new APIException(APIException.FailureStep.OnRequest, "Request is not part of multi-request");
		}

    	MultiRequestBuilder.Tokenizer annotation = type.getAnnotation(MultiRequestBuilder.Tokenizer.class);
		return new RequestBuilder.ListResponseTokenizer<TK>((Class<TK>)annotation.value(), id + ":result");
    }
}













