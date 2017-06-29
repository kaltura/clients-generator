package com.kaltura.client.utils.response.base;

import com.kaltura.client.utils.ErrorElement;

/**
 * Created by tehilarozin on 06/09/2016.
 *
 * the response object passed from the requests executor to the completion callback for further parsing.
 */
public class ExecutedResponse implements ResponseElement {

    String requestId;
    int code = -1;
    String response = "";
    boolean isSuccess = false;
    String contentType;
    ErrorElement errorElement = null;


    public ExecutedResponse requestId(String id) {
        this.requestId = id;
        return this;
    }

    public ExecutedResponse code(int code) {
        this.code = code;
        return this;
    }

    public ExecutedResponse response(String response) {
        this.response = response;
        return this;
    }

    public ExecutedResponse success(boolean success) {
        this.isSuccess = success;
        return this;
    }

    public ExecutedResponse contentType(String contentType) {
        this.contentType = contentType;
        return this;
    }

    public ExecutedResponse errorElement(ErrorElement errorElement) {
        this.errorElement = errorElement;
        return this;
    }

    @Override
    public int getCode() {
        return code;
    }

    @Override
    public String getResponse() {
        return response;
    }

    @Override
    public boolean isSuccess() {
        return isSuccess;
    }

    @Override
    public String getRequestId() {
        return requestId;
    }

	@Override
	public ErrorElement getError() {
		return errorElement;
	}

}

