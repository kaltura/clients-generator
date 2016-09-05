package com.kaltura.client.utils.response.base;

import com.kaltura.client.utils.GsonParser;
import com.kaltura.client.utils.request.RequestElement;

/**
 * Created by tehilarozin on 05/09/2016.
 */
public class ResponseParser {

    public static GeneralResponse parse(String response, String contentType, RequestElement action){
        GeneralResponse generalResponse = parseResult(response, contentType);

        if(generalResponse != null) {
            return generalResponse.requestId(action.getId());
        }
        return GeneralResponse.empty().requestId(action.getId());
    }

    private static GeneralResponse parseResult(String result, String format) {
        if(format.equals( "json")) {
            return GsonParser.parseResult(result);
        }
        return null;
    }
}
