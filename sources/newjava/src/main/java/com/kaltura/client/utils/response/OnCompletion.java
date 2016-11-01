package com.kaltura.client.utils.response;


/**
 * Created by tehilarozin on 21/07/2016.
 */
public interface OnCompletion<R> {
    void onComplete(R response);
}
