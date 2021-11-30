package com.kaltura.client.utils.request;

import com.kaltura.client.utils.ErrorElement;
import com.kaltura.client.utils.response.base.ResponseElement;

import java.util.List;
import java.util.Map;

public class ExecutedRequest implements ResponseElement {

    String requestId;
    int code = -1;
    String response = null;
    boolean isSuccess = false;
    ErrorElement error = null;
    Map<String, List<String>> headers = null;

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

    public ExecutedRequest headers(Map<String, List<String>> headers) {
        this.headers = headers;
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

    @Override
    public Map<String, List<String>> getHeaders() {
        return headers;
    }
}

