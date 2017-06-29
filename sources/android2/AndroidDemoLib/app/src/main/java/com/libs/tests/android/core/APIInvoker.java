package com.libs.tests.android.core;

import android.util.Log;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.ConnectionConfiguration;
import com.kaltura.client.KalturaClient;
import com.kaltura.client.services.KalturaHouseholdDeviceService;
import com.kaltura.client.services.KalturaHouseholdService;
import com.kaltura.client.services.KalturaOttUserService;
import com.kaltura.client.types.KalturaHousehold;
import com.kaltura.client.types.KalturaHouseholdDevice;
import com.kaltura.client.types.KalturaLoginResponse;
import com.kaltura.client.types.KalturaOTTUserFilter;
import com.kaltura.client.utils.request.ActionRequest;
import com.kaltura.client.utils.request.MultiActionRequest;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.ResponseType;
import com.kaltura.client.utils.response.base.GeneralResponse;
import com.kaltura.client.utils.response.base.MultiResponse;
import com.libs.tests.android.LoginActivity;

/**
 * Created by tehilarozin on 30/08/2016.
 */

public class APIInvoker {

    private static APIInvoker self;
    private APIOkRequestsExecutor requestsExecutor;
    private KalturaClient client;

    static {
        self = new APIInvoker();
    }

    public static APIInvoker invoke(){
        return self;
    }

    private APIInvoker(){
        requestsExecutor = new APIOkRequestsExecutor();
        ConnectionConfiguration connectionConfiguration = new ConnectionConfiguration();
        connectionConfiguration.setEndpoint(LoginActivity.ENDPOINT);
        client = new KalturaClient(connectionConfiguration);
        client.setPartnerId(LoginActivity.PartnerId);
    }

    public void login(String username, String password, String uuid, final OnCompletion onCompletion){
        ActionRequest loginReq = KalturaOttUserService.login(client.getPartnerId(), username, password, null, uuid).setCompletion(
                new OnCompletion<GeneralResponse<KalturaLoginResponse>>() {
                    @Override
                    public void onComplete(GeneralResponse<KalturaLoginResponse> response) {
                        if(response.isSuccess()){
                            client.setKs(response.getResult().getLoginSession().getKs());
                        }
                        if(onCompletion != null){
                            onCompletion.onComplete(response);
                        }
                    }
                });
        requestsExecutor.queue(loginReq.build(client));
    }


    public String loginWithInfo(String username, String password, String uuid, final OnCompletion onCompletion){
        ActionRequest loginReq = KalturaOttUserService.login(client.getPartnerId(), username, password, null, uuid).setCompletion(onCompletion);
        ActionRequest deviceInfo = KalturaHouseholdDeviceService.get();
        MultiActionRequest multiActionRequest = loginReq.add(deviceInfo)
                .link(loginReq, deviceInfo, "loginSession.ks", "ks")
                .add(KalturaHouseholdService.get())
                .link(loginReq, 2, "loginSession.ks", "ks")
                .add(KalturaOttUserService.list(new KalturaOTTUserFilter()))
                .link(loginReq, 3, "loginSession.ks", "ks")
                .setCompletion(new OnCompletion<GeneralResponse<MultiResponse>>() {
            @Override
            public void onComplete(GeneralResponse<MultiResponse> response) {
                ResponseType completionResponse = null;

                if(response.isSuccess()) {
                    MultiResponse multiResponse = response.getResult();
                    if (!multiResponse.failedOn(0)) {
                        Log.i("APIInvoker", "hello " + ((KalturaLoginResponse) multiResponse.getAt(0)).getUser().getFirstName() + "!");

                        if (!multiResponse.failedOn(2)) {
                            KalturaHousehold household = multiResponse.getAt(2);
                            Log.i("APIInvoker", "household name:" + household.getName() + ", devices limit = " + household.getDevicesLimit());
                        }
                        if (!multiResponse.failedOn(1)) {
                            KalturaHouseholdDevice householdDevice = multiResponse.getAt(1);
                            Log.i("APIInvoker", "Device status: " + householdDevice.getStatus());
                        }

                        completionResponse = multiResponse.getAt(3);
                    } else {
                        completionResponse = multiResponse.get(0);
                    }

                    if (onCompletion != null) {
                        onCompletion.onComplete(new GeneralResponse.Builder().result(completionResponse).build());
                    }
                }
            }
        });
        return requestsExecutor.queue(multiActionRequest.build(client));
    }

    public void cancel(String reqId) {
        requestsExecutor.cancelAction(reqId);
    }
}
