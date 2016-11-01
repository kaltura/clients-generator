package com.kaltura.client.utils.response.base;

import com.google.gson.JsonObject;
import com.kaltura.client.utils.KalturaAPIConstants;
import com.kaltura.client.utils.ParseUtils;
import com.kaltura.client.utils.response.ResponseType;


/**
 * Created by tehilarozin on 25/07/2016.
 */
@Deprecated
public class GeneralResponseList extends GeneralResponse<MultiResponse> {

    public GeneralResponseList() {
        super();
    }

    public GeneralResponseList(String action, MultiResponse result) {
        super(action, result);
    }

    public GeneralResponseList(JsonObject jsonObject) {
        super(jsonObject);
    }

    @Override
    protected MultiResponse parseResponse(JsonObject jsonObject) {
        return ParseUtils.parseObject(jsonObject.get(KalturaAPIConstants.PropertyResult), MultiResponse.class);
    }

    @Override
    public String toString() {
        return "GeneralResponseList:, result = "+ buildString();
    }

    private String buildString() {
        StringBuilder builder = new StringBuilder("");

        if(result != null) {
            int i = 0;
            for (ResponseType responseType : result) {
                if(responseType != null) {
                    builder.append("\nresultObject_").append(i).append(": ").append(responseType.toString());
                }
                i++;
            }
        }

        return builder.toString();
    }
}
