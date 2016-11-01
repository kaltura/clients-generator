package com.kaltura.client.utils.deprecated;

import com.kaltura.client.types.KalturaAPIException;
import com.kaltura.client.utils.ParseUtils;
import com.kaltura.client.utils.core.response.base.GeneralResponse;
import com.kaltura.client.utils.request.ActionElement;

import java.util.concurrent.Callable;

import static com.sun.tools.javadoc.Main.execute;

/**
 * Created by tehilarozin on 09/08/2016.
 *
 * implements request executioner.
 * open connection with {@link HttpUrlConnector} and execute request
 */
public class HUCActionCaller implements Callable<GeneralResponse> {

    HttpUrlConnector mConnector;

    public HUCActionCaller(ActionElement action){
        mConnector = new HttpUrlConnector(action);
    }


    @Override
    public GeneralResponse call() throws Exception {
        String jsonRes = null;
        if(mConnector!=null) {
            try {
                 jsonRes = mConnector.execute();
            } catch (KalturaAPIException e) {
                jsonRes = ParseUtils.toJson(e);
            }

            if(jsonRes != null && !jsonRes.equals("")){
                GeneralResponse response = ParseUtils.parseResult(jsonRes, mConnector.getActionRequest().getAction());

            }
            return null;
        }
    }
}
