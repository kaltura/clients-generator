package com.kaltura.client.utils;

import com.google.gson.JsonObject;

import java.lang.reflect.InvocationTargetException;
import java.util.HashMap;
import java.util.Map;

import static com.kaltura.client.utils.ObjectClassFactory.withMap;

/**
 * Created by tehilarozin on 26/07/2016.
 */
public class APIClassesFactory {

    static Map<String, Class> classes = new HashMap<String, Class>();


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
