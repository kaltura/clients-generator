package com.kaltura.client.utils.response.base;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.kaltura.client.types.KalturaAPIException;
import com.kaltura.client.utils.KalturaAPIConstants;
import com.kaltura.client.utils.ParseUtils;

/**
 * Created by tehilarozin on 25/07/2016.
 */
public abstract class GeneralResponse<T> {

    private String requestId;
    private String action;
    private double executionTime;
    protected T result;

    public GeneralResponse(){}

    public GeneralResponse(String action, T result) {
        this.action = action;
        this.result = result;
    }

    public GeneralResponse(JsonObject jsonObject) {
        if(jsonObject == null){
            return;
        }

        executionTime = ParseUtils.parseDouble(jsonObject, KalturaAPIConstants.PropertyExecutionTime);
        result = jsonObject.has(KalturaAPIConstants.PropertyResult) ? parseResponse(jsonObject) : null;
    }

    protected abstract T parseResponse(JsonObject jsonObject);

    public double getExecutionTime() {
        return executionTime;
    }

    public void setExecutionTime(double executionTime) {
        this.executionTime = executionTime;
    }

    public T getResult() {
        return result;
    }

    public void setResult(T result) {
        this.result = result;
    }

    public String getAction() {
        return action;
    }

    public void setAction(String action) {
        this.action = action;
    }

    @Override
    public String toString() {
        return "GeneralResponse: executionTime = "+ executionTime;
    }

    public static GeneralResponse getResponse(JsonObject jsonObject) {
        if(jsonObject.has(KalturaAPIConstants.PropertyResult)){
            JsonElement result = jsonObject.get(KalturaAPIConstants.PropertyResult);
            if(result.isJsonArray()){
                return new GeneralResponseList(jsonObject);
            } else if(result.isJsonObject()){
                return new GeneralSingleResponse(jsonObject);
            }
            return new GeneralPrimitiveResponse(jsonObject);
        }
        return null;
    }

    public boolean isSuccess() {
        return !(result instanceof KalturaAPIException);
    }

    public GeneralResponse setRequestId(String requestId) {
        this.requestId = requestId;
        return this;
    }

    public String getRequestId() {
        return requestId;
    }
}
