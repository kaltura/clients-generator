package com.kaltura.client.utils.response;

import com.kaltura.client.types.APIException;

/**
 * Created by tehilarozin on 21/07/2016.
 */
public interface OnCompletion<T> {
    void onComplete(T response, APIException error);
}
