package com.kaltura.client.utils.response.base;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.kaltura.client.types.KalturaAPIException;
import com.kaltura.client.utils.GsonParser;
import com.kaltura.client.utils.ParseUtils;
import com.kaltura.client.utils.request.KalturaMultiRequestBuilder;
import com.kaltura.client.utils.request.RequestElement;
import com.kaltura.client.utils.response.OnCompletion;

import java.io.FileReader;
import java.io.IOException;

/**
 * Created by tehilarozin on 05/09/2016.
 */
public class ResponseParser {


    public static void parse(RequestElement request, ResponseElement responseElement, OnCompletion<GeneralResponse> completion){
        GeneralResponse generalResponse = null;

        if(!responseElement.isSuccess()) {
            generalResponse = generateErrorResponse(responseElement, request instanceof KalturaMultiRequestBuilder);
        } else {
            generalResponse = parseResult(responseElement.getResponse(), responseElement.getContentType());
        }
        if(generalResponse == null) {
            generalResponse = GeneralResponse.empty();
        }

        generalResponse.requestId(responseElement.getRequestId());

        if(completion != null) {
            completion.onComplete(generalResponse);
        }
    }

    private static GeneralResponse generateErrorResponse(ResponseElement response, boolean isMultiRequest) {

        KalturaAPIException.FailureStep failureStep = response.getCode() == -1 ?
                KalturaAPIException.FailureStep.OnRequest :
                KalturaAPIException.FailureStep.OnResponse;
        String code = KalturaAPIException.FailureStep.OnRequest.equals(failureStep) ? failureStep.code : response.getCode() + "";

        String message = response.getResponse() == null ? KalturaAPIException.DefaultResponseError : response.getResponse();
        KalturaAPIException result = new KalturaAPIException(failureStep, message, code);

        return new GeneralResponse.Builder()
                .result(isMultiRequest ? new MultiResponse(result) : result)
                .build();
    }

    private static GeneralResponse parseResult(String result, String format) {
        if(format.equals( "json")) {
            return GsonParser.parseResult(result);
        }
        return null;
    }



    public static <T> void parseResponseFile(String responseFile, OnCompletion<GeneralResponse<T>> completion) {
        JsonParser parser = new JsonParser();
        try {
            JsonElement element = (JsonObject) parser.parse(new FileReader(responseFile));
            GeneralResponse response = ParseUtils.parseResult(element.toString(), "json");

            if (completion != null) {
                completion.onComplete(response);
            }
        }catch (IOException e){
            if (completion != null) {
                completion.onComplete(generateErrorResponse(new ExecutedResponse().response(e.getMessage()).contentType("json"), true));
            }
        }
    }

}
