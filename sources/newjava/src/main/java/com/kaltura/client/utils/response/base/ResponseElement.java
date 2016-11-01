package com.kaltura.client.utils.response.base;

/**
 * Created by tehilarozin on 06/09/2016.
 */
public interface ResponseElement {

    String getContentType();

    int getCode();

    String getResponse();

    boolean isSuccess();

    String getRequestId();

}
