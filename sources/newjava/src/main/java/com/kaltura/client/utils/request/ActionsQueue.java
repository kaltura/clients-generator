package com.kaltura.client.utils.request;


import com.kaltura.client.utils.response.base.ResponseElement;

/**
 * Created by tehilarozin on 08/08/2016.
 */
public interface ActionsQueue {

    String queue(RequestElement action);

    ResponseElement execute(RequestElement action);

    void cancelAction(String actionId);

    void clearActions();

    boolean isEmpty();
}
