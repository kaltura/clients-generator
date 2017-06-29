package com.kaltura.client.utils.response.base;

import com.kaltura.client.Logger;
import com.kaltura.client.types.APIException;
import com.kaltura.client.utils.response.ResponseType;

import java.util.ArrayList;
import java.util.Iterator;

/**
 * Created by tehilarozin on 09/08/2016.
 */
public class MultiResponse extends ArrayList<ResponseType> {

    public MultiResponse(){
        super();
    }

    public MultiResponse(ResponseType response){
        super();
        add(response);
    }

    public boolean failedOn(int i) {
        try {
            return get(i) instanceof APIException;
        } catch (IndexOutOfBoundsException e) {
            Logger.getLogger("MultiResponse").error("failedOn: index " + i + " out of range");
            return true;
        }
    }

    public <T extends ResponseType> T getAt(String indexStr) {
        int index = 0;
        try {
            index = Integer.valueOf(indexStr) - 1;

        } catch (NumberFormatException e) {
            e.printStackTrace();
            index = -1;
        }

        return getAt(index);
    }

    public <T extends ResponseType> T getAt(int i){
         return i >= 0 && size() > i ? (T) get(i) : null;
    }

    @Override
    public String toString() {
        StringBuilder builder = new StringBuilder("");
        Iterator<ResponseType> iterator = iterator();
        int i = 1;
        while (iterator.hasNext()) {
            ResponseType responseType = iterator.next();
            if(responseType != null) {
                builder.append("\nresultObject_").append(i++).append(": ").append(responseType.toString());
            }
        }
        return builder.toString();
    }
}
