package com.kaltura.client.utils.response.base;

/**
 * Created by tehilarozin on 06/09/2016.
 */
public class ExecutedRequestResponse implements ResponseElement {

    String requestId;
    int code = -1;
    String response = "";
    boolean isSuccess = false;
    String contentType;


    public ExecutedRequestResponse requestId(String id) {
        this.requestId = id;
        return this;
    }

    public ExecutedRequestResponse code(int code) {
        this.code = code;
        return this;
    }

    public ExecutedRequestResponse response(String response) {
        this.response = response;
        return this;
    }

    public ExecutedRequestResponse success(boolean success) {
        this.isSuccess = success;
        return this;
    }

    public ExecutedRequestResponse contentType(String contentType) {
        this.contentType = contentType;
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
    public String getContentType() {
        return contentType;
    }


}

