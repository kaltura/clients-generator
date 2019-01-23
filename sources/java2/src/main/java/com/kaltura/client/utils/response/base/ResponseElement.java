package com.kaltura.client.utils.response.base;

import com.kaltura.client.utils.ErrorElement;
import okhttp3.Headers;

/**
 * Created by tehilarozin on 06/09/2016.
 */
public interface ResponseElement {

    int getCode();

    String getResponse();

    boolean isSuccess();

    String getRequestId();

    ErrorElement getError();

    Headers getHeaders();
}
