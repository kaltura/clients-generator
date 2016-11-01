package com.kaltura.client.utils;

import com.google.gson.JsonObject;

import java.lang.reflect.InvocationTargetException;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by tehilarozin on 26/07/2016.
 */
public class ObjectClassFactory {

    final static boolean withReflection = true;
    final static boolean withMap = true;

    static Map<String, Class> classes = new HashMap<String, Class>();


    public static <T> T getResponseObject(Class clz, JsonObject json){
        if(withReflection){
            return reflectResponseObject(clz, json);
        }
        return getResponseObject(clz.getName(), json);
    }

    public static  Class<?> getResponseClassType(String className){
        className  = "com.kaltura.client.types."+className;
        Class clz = null;

        if(withMap){
            clz = classes.get(className);
        }
        if(clz == null) {
            clz = ParseUtils.getClass(className);
            if(withMap){
                classes.put(className, clz);
            }
        }
        return clz;
    }

    public static <T> T getResponseObject(String className, JsonObject json){


        if(withReflection){
            return reflectResponseObject(getResponseClassType(className), json);
        }

        className  = "com.kaltura.client.types."+className;
		
        switch (className){
            /*case "responses.generated.SuccessResult":
                return (T) new SuccessResult(json);

            case "responses.generated.MoreInResponse":
                return (T) new MoreInResponse(json);

            case "responses.generated.OtherResult":
                return (T) new OtherResult(json);

            case "responses.generated.TestResult":
                return (T) new TestResult(json);

            case "responses.generated.ErrorResult":
                return (T) new ErrorResult(json);*/

            default:
                return reflectResponseObject(className, json);
        }
    }

    private static <T> T reflectResponseObject(String className, JsonObject json) {
        Class clz = null;

        if(withMap){
            clz = classes.get(className);
        }
        if(clz == null) {
            clz = ParseUtils.getClass(className);
            if(withMap){
                classes.put(className, clz);
            }
        }

        return reflectResponseObject(clz, json);
    }

    private static <T> T reflectResponseObject(Class clz, JsonObject json) {
        try {
            return clz != null ? (T) clz.getConstructor(JsonObject.class).newInstance(json) : null;

        } catch (InstantiationException | IllegalAccessException | InvocationTargetException | NoSuchMethodException e) {
            e.printStackTrace();
        }

        return null;
    }

}
