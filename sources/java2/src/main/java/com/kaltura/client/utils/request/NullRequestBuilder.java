package com.kaltura.client.utils.request;

import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.response.base.Response;

public abstract class NullRequestBuilder extends RequestBuilder<Void> {

    public NullRequestBuilder(String service, String action) {
        super(Void.class, service, action);
    }

    @Override
    public void onComplete(Response<Void> response) {
        super.onComplete(response.results(null));
    }

	@Override
	public String getTokenizer() throws APIException {
		throw new APIException(APIException.FailureStep.OnRequest, "Null response can not be used as multi-request token");
	}
}














