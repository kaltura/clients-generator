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
    //private String action;
    private double executionTime;
    protected T result;

    private GeneralResponse(){}

    private GeneralResponse(T result) {
        this.result = result;
    }

    /*private GeneralResponse(JsonObject jsonObject) {
        if(jsonObject == null){
            return;
        }

        executionTime = ParseUtils.parseDouble(jsonObject, KalturaAPIConstants.PropertyExecutionTime);
        result = jsonObject.has(KalturaAPIConstants.PropertyResult) ? parseResponse(jsonObject) : null;
    }*/


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

    /*public void setResult(T result) {
        this.result = result;
    }*/

    /*public String getAction() {
        return action;
    }

    private GeneralResponse<T> setAction(String action) {
        this.action = action;
        return this;
    }*/

    public Builder newBuilder(){
        return new Builder(this);
    }

    @Override
    public String toString() {
        return "GeneralResponse: executionTime = "+ executionTime+", "+super.toString();
    }

    /*public static GeneralResponse getResponse(JsonObject jsonObject) {
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
    }*/

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

    /*protected MultiResponse parseResponse(JsonObject jsonObject) {
        return ParseUtils.parseObject(jsonObject.get(KalturaAPIConstants.PropertyResult), MultiResponse.class);
    }
*/
    public static class Builder{
        Object result;
        String requestId;
       // String action;
        double executionTime;

        public Builder(){}

        private Builder(Builder builder) {
            this.result = builder.result;
            this.requestId = builder.requestId;
            //this.action = builder.action;
            this.executionTime = builder.executionTime;
        }

        private Builder(GeneralResponse response){
            this.result = response.getResult();
            this.requestId = response.getRequestId();
            //this.action = response.getAction();
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

        /*public GeneralResponse fromResult(String action, Object result){
            if(result instanceof MultiResponse){
                return new GeneralResponseList(action, (MultiResponse)result);

            } else if(result instanceof ResponseType){
                return new GeneralSingleResponse(action, (ResponseType) result);

            } else { // empty result (null) will be considered as ok response ("true")
                return new GeneralPrimitiveResponse(action, result == null ? KalturaAPIConstants.ResultOk : (String)result);
            }
        }*/

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

        /*public Builder action(String action){
            this.action = action;
            return this;
        }*/

        public GeneralResponse build(){
            return new GeneralResponse<Object>(result).requestId(requestId)/*.setAction(action)*/.executionTime(executionTime);
        }

        public Builder newBuilder(){
            return new Builder(this);
        }
    }
}
