package com.kaltura.client.utils.request;

import com.kaltura.client.utils.ErrorElement;
import com.kaltura.client.utils.response.base.ResponseElement;

/**
 * Created by tehilarozin on 06/09/2016.
 *
 * the response object passed from the requests executor to the completion callback for further parsing.
 */
public class ExecutedRequest implements ResponseElement {

    String requestId;
    int code = -1;
    String response = null;
    boolean isSuccess = false;
    ErrorElement error = null;

    public ExecutedRequest requestId(String id) {
        this.requestId = id;
        return this;
    }

    public ExecutedRequest code(int code) {
        this.code = code;
        return this;
    }

    public ExecutedRequest response(String response) {
        this.response = response;
        return this;
    }

    public ExecutedRequest success(boolean success) {
        this.isSuccess = success;
        return this;
    }

    public ExecutedRequest error(ErrorElement error) {
        this.error = error;
        this.code = error.getCode();
        this.response = error.getMessage();
        return this;
    }

    public ExecutedRequest error(Exception exception) {
        return error(ErrorElement.fromException(exception));
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
    public ErrorElement getError() {
        return error;
    }

    @Override
    public boolean isSuccess() {
        return isSuccess;
    }

    @Override
    public String getRequestId() {
        return requestId;
    }

}

