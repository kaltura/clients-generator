package com.kaltura.client.utils.response.base;

import com.google.gson.JsonObject;
import com.kaltura.client.utils.KalturaAPIConstants;
import com.kaltura.client.utils.ParseUtils;
import com.kaltura.client.utils.response.ResponseType;

/**
 * Created by tehilarozin on 25/07/2016.
 */
public class GeneralSingleResponse extends GeneralResponse<ResponseType> {


    public GeneralSingleResponse() {
        super();
    }

    public GeneralSingleResponse(/*SomeRequest request, long code,*/String action, ResponseType result) {
        super(action, result);
    }

    public GeneralSingleResponse(JsonObject jsonObject) {
        super(jsonObject);
    }

    @Override
    protected ResponseType parseResponse(JsonObject jsonObject) {
        return ParseUtils.parseObject(jsonObject.getAsJsonObject(KalturaAPIConstants.PropertyResult), ResponseType.class);
    }

    @Override
    public String toString() {
        return "GeneralSingleResponse: "+ super.toString()+", result = "+ result.toString();
    }
}
