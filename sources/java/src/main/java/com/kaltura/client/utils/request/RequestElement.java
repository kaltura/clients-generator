package com.kaltura.client.utils.request;


import com.kaltura.client.ConnectionConfiguration;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;

import java.util.HashMap;

/**
 * Created by tehilarozin on 09/08/2016.
 */
public interface RequestElement extends OnCompletion<GeneralResponse> {

    //String getQuery();

    String getContentType();

    String getMethod();

    String getUrl();

    String getBody();

    String tag();

    HashMap<String, String> getHeaders();

    String getId();

    ConnectionConfiguration config();

   // void onComplete(boolean isSuccess, String response, int code);
}
