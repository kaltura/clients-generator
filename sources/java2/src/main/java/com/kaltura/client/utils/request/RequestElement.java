package com.kaltura.client.utils.request;


import com.kaltura.client.Files;
import com.kaltura.client.utils.response.base.Response;
import com.kaltura.client.utils.response.base.ResponseElement;

import java.util.HashMap;


public interface RequestElement<T> {

    String getContentType();

    String getMethod();

    String getUrl();

    String getBody();

    String getTag();

    Files getFiles();

    HashMap<String, String> getHeaders();

    ConnectionConfiguration config();

    Response<T> parseResponse(ResponseElement responseElement);

    void onComplete(Response<T> response);
}
