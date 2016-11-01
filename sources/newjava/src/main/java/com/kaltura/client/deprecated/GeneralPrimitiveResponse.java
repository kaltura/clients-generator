package com.kaltura.client.utils.response.base;

import com.google.gson.JsonObject;
import com.kaltura.client.utils.KalturaAPIConstants;
import com.kaltura.client.utils.ParseUtils;

/**
 * Created by tehilarozin on 28/07/2016.
 */
@Deprecated
public class GeneralPrimitiveResponse extends GeneralResponse<String> {

    public GeneralPrimitiveResponse(JsonObject jsonObject) {
        super(jsonObject);
    }

    @Override
    protected String parseResponse(JsonObject jsonObject) {
        return ParseUtils.parseString(jsonObject, KalturaAPIConstants.PropertyResult);
    }

    @Override
    public String toString() {
        return "GeneralPrimitiveResponse: " + super.toString() + ", result = "+ result;
    }
}
