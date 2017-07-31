package com.kaltura.client;


import android.os.Handler;
import android.os.Looper;

import com.kaltura.client.utils.request.ConnectionConfiguration;
import com.kaltura.client.utils.request.RequestElement;
import com.kaltura.client.utils.response.base.ResponseElement;

/**
 * @hide
 */
public class AndroidAPIRequestsExecutor extends APIOkRequestsExecutor {

    public static final String TAG = "AndroidAPIRequestsExecutor";

    private static APIOkRequestsExecutor mainExecutor;
    private Handler handler = null;


    public static APIOkRequestsExecutor getBackExecutor() {
        if (self == null) {
            self = new AndroidAPIRequestsExecutor();
        }
        return self;
    }

    public static APIOkRequestsExecutor getExecutor() {
        if (mainExecutor == null) {
            mainExecutor = new AndroidAPIRequestsExecutor(new Handler(Looper.getMainLooper()));
        }
        return mainExecutor;
    }

    public AndroidAPIRequestsExecutor() {
        super();
    }

    public AndroidAPIRequestsExecutor(Handler handler){
        super();
        this.handler = handler;
    }

    public AndroidAPIRequestsExecutor(ConnectionConfiguration defaultConfiguration) {
        super(defaultConfiguration);
    }


    @Override
    protected void postCompletion(final RequestElement action, ResponseElement responseElement) {
        final com.kaltura.client.utils.response.base.Response<?> apiResponse = action.parseResponse(responseElement);

        if (handler != null) {
            handler.post(new Runnable() {
                @Override
                public void run() {
                    action.onComplete(apiResponse);
                }
            });
        } else {
            action.onComplete(apiResponse);
        }
    }
}
