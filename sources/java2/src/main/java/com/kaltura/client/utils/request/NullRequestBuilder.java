package com.kaltura.client.utils.request;

import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.response.base.Response;

public class NullRequestBuilder<U> extends LinkedRequest<Void, U, Void> {

    public NullRequestBuilder(String service, String action, Params params) {
        super(Void.class, service, action, params);
    }

    @Override
    public void onComplete(Response<Void> response) {
        super.onComplete(response.results(null));
    }

	@Override
	public Void getTokenizer() throws APIException {
		throw new APIException(APIException.FailureStep.OnRequest, "Null response can not be used as multi-request token");
	}
}














