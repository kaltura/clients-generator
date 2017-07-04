package com.kaltura.client.utils.request;


import com.kaltura.client.Files;
import com.kaltura.client.utils.response.base.ResponseElement;

import java.util.HashMap;

/**
 * Created by tehilarozin on 09/08/2016.
 */
public interface RequestElement {

    String getContentType();

    String getMethod();

    String getUrl();

    String getBody();

    String getTag();

    Files getFiles();

    HashMap<String, String> getHeaders();

    String getId();

    ConnectionConfiguration config();

    void onComplete(ResponseElement responseElement);
}
