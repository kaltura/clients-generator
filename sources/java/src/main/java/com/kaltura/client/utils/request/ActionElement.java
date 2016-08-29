package com.kaltura.client.utils.request;


import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;

/**
 * Created by tehilarozin on 09/08/2016.
 */
public interface ActionElement<T> extends OnCompletion<GeneralResponse<T>> {

    void setParams(Object params);

    Object getParams();

    String getMethod();

    String getUrlTail();

    String getBody();

    String getAction();

}
