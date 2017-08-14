package com.kaltura.client.utils.response.base;

import com.kaltura.client.types.APIException;

/**
 * Created by tehila.rozin on 7/27/17.
 */

public class Response<T> {

    public APIException error;
    public T results;

    public Response(T results, APIException error) {
        this.error = error;
        this.results = results;
    }

    public boolean isSuccess(){
        return error == null;
    }

    public boolean isEmpty(){
        return error != null || results == null;
    }

    public Response<T> results(T results) {
        this.results = results;
        return this;
    }
}
