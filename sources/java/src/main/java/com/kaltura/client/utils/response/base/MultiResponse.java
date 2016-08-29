package com.kaltura.client.utils.response.base;

import com.kaltura.client.types.KalturaAPIException;
import com.kaltura.client.utils.response.ResponseType;

import java.util.ArrayList;

/**
 * Created by tehilarozin on 09/08/2016.
 */
public  class MultiResponse extends ArrayList<ResponseType> {

    public MultiResponse(){
        super();
    }

    public MultiResponse(ResponseType response){
        super();
        add(response);
    }

    public boolean failedOn(int i){
        return get(i) instanceof KalturaAPIException;
    }

    public <T extends ResponseType> T getData(int i){
         return i >= 0 && size() > i ? (T) get(i) : null;
    }
}
