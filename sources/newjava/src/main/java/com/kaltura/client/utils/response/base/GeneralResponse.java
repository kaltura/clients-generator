package com.kaltura.client.utils.response.base;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.kaltura.client.types.KalturaAPIException;
import com.kaltura.client.utils.KalturaAPIConstants;
import com.kaltura.client.utils.ParseUtils;
import com.kaltura.client.utils.response.ResponseType;

/**
 * Created by tehilarozin on 25/07/2016.
 */
public class GeneralResponse<T> {

    private String requestId;
    private double executionTime;
    protected T result;
    //TODO: add headers here

    private GeneralResponse(){}

    private GeneralResponse(T result) {
        this.result = result;
    }

    public static GeneralResponse empty(){
        return new GeneralResponse(null);
    }

    public double getExecutionTime() {
        return executionTime;
    }

    private GeneralResponse<T> executionTime(double executionTime) {
        this.executionTime = executionTime;
        return this;
    }

    public T getResult() {
        return result;
    }

    public Builder newBuilder(){
        return new Builder(this);
    }

    @Override
    public String toString() {
        return "GeneralResponse: executionTime = "+ executionTime+", "+super.toString();
    }

    public boolean isSuccess() {
        return result != null && !(result instanceof KalturaAPIException);
    }

    public GeneralResponse requestId(String requestId) {
        this.requestId = requestId;
        return this;
    }

    public String getRequestId() {
        return requestId;
    }

    public static class Builder{
        private Object result;
        private String requestId;
        private double executionTime;
        public Builder(){}

        private Builder(Builder builder) {
            this.result = builder.result;
            this.requestId = builder.requestId;
            this.executionTime = builder.executionTime;
        }

        private Builder(GeneralResponse response) {
            this.result = response.getResult();
            this.requestId = response.getRequestId();
            this.executionTime = response.getExecutionTime();
        }

        private Builder fromJson(JsonObject jsonObject){
            executionTime = ParseUtils.parseDouble(jsonObject, KalturaAPIConstants.PropertyExecutionTime);

            if(jsonObject.has(KalturaAPIConstants.PropertyResult)){
                JsonElement jsonResult = jsonObject.get(KalturaAPIConstants.PropertyResult);

                if(jsonResult.isJsonArray()){
                    result = ParseUtils.parseObject(jsonObject.get(KalturaAPIConstants.PropertyResult), MultiResponse.class);

                } else if(jsonResult.isJsonObject()){
                    result = ParseUtils.parseObject(jsonObject.getAsJsonObject(KalturaAPIConstants.PropertyResult), ResponseType.class);

                } else {
                    result = ParseUtils.parseString(jsonObject, KalturaAPIConstants.PropertyResult);
                }
            }
            return this;
        }

        public Builder result(JsonObject jsonObject){
            return fromJson(jsonObject);
        }

        public Builder result(Object result){
            this.result = result == null ? KalturaAPIConstants.ResultOk
                    : result instanceof JsonObject ? fromJson((JsonObject) result)
                    : result;
            return this;
        }

        public Builder requestId(String requestId){
            this.requestId = requestId;
            return this;
        }

        public Builder executionTime(double executionTime){
            this.executionTime = executionTime;
            return this;
        }

        public GeneralResponse build(){
            return new GeneralResponse<Object>(result).requestId(requestId)/*.setAction(action)*/.executionTime(executionTime);
        }

     }
}
