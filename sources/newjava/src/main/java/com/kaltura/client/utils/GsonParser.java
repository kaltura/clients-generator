package com.kaltura.client.utils;

import com.google.gson.*;
import com.google.gson.internal.$Gson$Types;
import com.kaltura.client.IKalturaLogger;
import com.kaltura.client.KalturaLogger;
import com.kaltura.client.utils.response.ObjectTypeAdapter;
import com.kaltura.client.utils.response.ResponseType;
import com.kaltura.client.utils.response.base.GeneralResponse;

import java.lang.reflect.ParameterizedType;
import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by tehilarozin on 24/07/2016.
 */
public class GsonParser {

    private static IKalturaLogger logger = KalturaLogger.getLogger(GsonParser.class);

    private static GsonBuilder sGsonBuilder;



    public static <T> T parseObject(String json, Class<T> clz) {
        if (json != null) {
            Gson gson = createGson();
            try {
                return gson.fromJson(json, clz);
            } catch (JsonParseException e) {
                e.printStackTrace();
            }
        }
        return null;
    }

    public static <T> T parseObject(JsonElement json, Class<T> clz) /*throws KalturaAPIException*/{
        if (json != null) {
            Gson gson = createGson();
            try {
                return gson.fromJson(json, clz);

            } catch (JsonParseException e) {
                e.printStackTrace();
                //throw new KalturaAPIException("Gson parsing failed for "+clz.getName()+": "+e.getMessage());
            }
        }
        return null;
    }

    /*public static GeneralResponse parseObject(String json) {
        if (json != null) {
            Gson gson = createGson();
            try {
                return gson.fromJson(json, GeneralResponse.class);
            } catch (JsonParseException e) {
                e.printStackTrace();
            }
        }
        return null;
    }*/

    public synchronized static GsonBuilder getGsonBuilder(){
        if(sGsonBuilder == null){
            sGsonBuilder = new GsonBuilder();
            registerBuilder();
        }

        return sGsonBuilder;
    }

    public static void registerBuilder(){
       // getGsonBuilder().registerTypeAdapter(ResponseType.class, new InterfaceAdapter<ResponseType>());
        getGsonBuilder().registerTypeHierarchyAdapter(ResponseType.class, new ObjectTypeAdapter());
        //getGsonBuilder().registerTypeHierarchyAdapter(ErrorObject.class, new ErrorTypeAdapter());

    }

    public static Gson createGson(){
        return getGsonBuilder().create();
    }

    public static <T> T parseObject(JsonObject jsonObject, String key, Class clz){
        if(jsonObject.has(key)){
            JsonElement valueElement = jsonObject.get(key);
            if(valueElement.isJsonObject()){
                //return (T) parseObject(valueElement, getClass(typeName));

                return ObjectClassFactory.getResponseObject(clz, valueElement.getAsJsonObject());

            }
        }
        return null;
    }

    public static int parseInt(JsonObject jsonObject, String property){
        if(jsonObject.has(property)){
            JsonPrimitive primitive = jsonObject.getAsJsonPrimitive(property);
            if(primitive.isNumber()){
                return primitive.getAsInt();
            } else {
                logger.error("expected int but got non number value");
            }
        }

        return 0;
    }

    public static long parseLong(JsonObject jsonObject, String property){
        if(jsonObject.has(property)){
            JsonPrimitive primitive = jsonObject.getAsJsonPrimitive(property);
            if(primitive.isNumber()){
                return primitive.getAsLong();
            } else {
                logger.error("expected int but got non number value");
            }
        }

        return 0;
    }

    public static double parseDouble(JsonObject jsonObject, String property){
        if(jsonObject.has(property)){
            JsonPrimitive primitive = jsonObject.getAsJsonPrimitive(property);
            if(primitive.isNumber()){
                return primitive.getAsDouble();
            } else {
                logger.error("expected int but got non number value");
            }
        }

        return 0;
    }

    public static boolean parseBoolean(JsonObject jsonObject, String property) {
        if(jsonObject.has(property)) {
            JsonPrimitive primitive = jsonObject.getAsJsonPrimitive(property);
            if(primitive.isBoolean()){
                return primitive.getAsBoolean();
            } else {
                logger.error("expected int but got non number value");
            }
        }

        return false;
    }

    public static String parseString(JsonObject jsonObject, String property){
        if(jsonObject.has(property)){
            JsonPrimitive primitive = jsonObject.getAsJsonPrimitive(property);
            if(primitive.isString()){
                return primitive.getAsString();
            } else {
                logger.error("expected int but got non number value");
            }
        }

        return "";
    }

    public static <T> HashMap<String, T> parseMap(JsonObject jsonObject, String property, Class clz) {
        if(jsonObject.has(property)){
            JsonElement jsonElement = jsonObject.get(property);
            if(jsonElement.isJsonObject()) {
                ParameterizedType type = $Gson$Types.newParameterizedTypeWithOwner(null, HashMap.class, String.class, clz);
                //Type type = ParameterizedTypeImpl.make(HashMap.class, new Type[]{String.class, clz}, null);
                return createGson().fromJson(jsonElement, type);
            }
        }

        return new HashMap<String, T>();
    }

    public static <T> ArrayList<T> parseArray(JsonObject jsonObject, String property, Class clz) {
        if(jsonObject.has(property)){
            JsonElement jsonElement = jsonObject.get(property);
            if(jsonElement.isJsonArray()) {
                ParameterizedType type = $Gson$Types.newParameterizedTypeWithOwner(null, ArrayList.class, clz);
//                Type type = ParameterizedTypeImpl.make(ArrayList.class, new Type[]{clz}, null);
                return createGson().fromJson(jsonObject.get(property), type);
            }
        }

        return new ArrayList<T>();
    }

    public static String toJson(Object object) {
        return createGson().toJson(object);
    }

    public static GeneralResponse parseResult(String result) {
        JsonParser jsonParser = new JsonParser();
        JsonObject jo = (JsonObject) jsonParser.parse(result);
        return new GeneralResponse.Builder().result(jo).build();
    }
}
