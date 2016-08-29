package com.kaltura.client.utils.request;


import com.kaltura.client.KalturaClientBase;
import com.kaltura.client.utils.response.base.GeneralResponse;

/**
 * Created by tehilarozin on 08/08/2016.
 */
public interface ActionsQueue {

    String queue(ActionElement action, KalturaClientBase kalturaClientBase);

    GeneralResponse execute(ActionElement action, KalturaClientBase kalturaClientBase);

    void cancelAction(String actionId);

    void clearActions();

    boolean isEmpty();
}
