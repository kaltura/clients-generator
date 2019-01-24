package com.kaltura.client.utils.response.base;

import com.kaltura.client.utils.ErrorElement;

import java.util.List;
import java.util.Map;

/**
 * Created by tehilarozin on 06/09/2016.
 */
public interface ResponseElement {

    int getCode();

    String getResponse();

    boolean isSuccess();

    String getRequestId();

    ErrorElement getError();

    Map<String, List<String>> getHeaders();
}
