package com.kaltura.client.utils.request;

import com.kaltura.client.KalturaFile;
import com.kaltura.client.KalturaFiles;
import com.kaltura.client.KalturaParams;


/**
 * Created by tehilarozin on 14/08/2016.
 */
public class KalturaFileRequestBuilder extends KalturaRequestBuilder {

    KalturaFiles files;

    public KalturaFileRequestBuilder(String service, String action, KalturaParams params, KalturaFiles files) {
        super(service, action, params);

        this.files = files;
    }

    //TODO add method that does something with those files.


    public String getUrlTail() {
        StringBuilder urlBuilder = new StringBuilder("service/").append(service);
        if (!action.equals("")) {
            urlBuilder.append("/action/").append(action);
        }

        return urlBuilder.toString();
    }




    public KalturaFileRequestBuilder setFile(String key, KalturaFile value) {
        if (files != null) {
            files.add(key, value);
        }
        return this;
    }


}














