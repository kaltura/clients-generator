package com.kaltura.client.utils;

import com.google.gson.Gson;
import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.google.gson.JsonPrimitive;
import com.google.gson.JsonSyntaxException;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.APIException.FailureStep;
import com.kaltura.client.types.ListResponse;

import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by tehilarozin on 24/07/2016.
 */
public class GsonParser {

	private static final String ResultKey = "result";
	private static final String ObjectTypeKey = "objectType";

    private static Gson gson = new Gson();

    @SuppressWarnings("unchecked")
	public static <T> Class<T> getObjectClass(String objectType, Class<T> defaultClass) {
        String className = "com.kaltura.client.types." + objectType.replaceAll("^Kaltura", "");

		try {
			return (Class<T>) Class.forName(className);
		} catch (ClassNotFoundException e) {
			return defaultClass;
		}
    }

    public static <T> T parseObject(String result, Class<T> clz) throws APIException {
        JsonParser jsonParser = new JsonParser();
        JsonElement jsonElement;
        try{
        	jsonElement = jsonParser.parse(result);
        }
        catch(JsonSyntaxException | IllegalStateException e) {
        	throw new APIException(FailureStep.OnResponse, "Invalid JSON response: " + result);
        }
        
        if(jsonElement.isJsonObject()) {
        	JsonObject jsonObject = jsonElement.getAsJsonObject();
        	if(jsonObject.get(ResultKey) != null && jsonObject.get(ObjectTypeKey) == null) {
        		jsonElement = jsonObject.get(ResultKey);
				if (!jsonElement.isJsonPrimitive()) {
                    jsonObject = jsonElement.getAsJsonObject();
                }
        	}
        	if(jsonObject.get("error") != null && jsonObject.get(ObjectTypeKey) == null) {
        		jsonElement = jsonObject.getAsJsonObject("error");
        	}
        }
    	return parseObject(jsonElement, clz);
    }

    @SuppressWarnings("unchecked")
	public static <T> T parseObject(JsonElement jsonElement, Class<T> clz) throws APIException {
        if(jsonElement.isJsonNull()) {
        	return null;
        }
        
        if(jsonElement.isJsonPrimitive()) {
        	if(clz == String.class) {
        		return (T)jsonElement.getAsString();
        	}
        	if(clz == Integer.class) {
        		return (T)(Integer)jsonElement.getAsInt();
        	}
        	if(clz == Long.class) {
        		return (T)(Long)jsonElement.getAsLong();
        	}
        	if(clz == Boolean.class) {
        		return (T)(Boolean)jsonElement.getAsBoolean();
        	}
        	if(clz == Double.class) {
        		return (T)(Double)jsonElement.getAsDouble();
        	}
        	
        	return (T) (Object) gson.fromJson(jsonElement, Object.class);
        }

        if(jsonElement.isJsonArray()) {
        	return (T) parseArray(jsonElement.getAsJsonArray(), clz);
        }
        
    	return parseObject(jsonElement.getAsJsonObject(), clz);
    }

    public static <T> T parseObject(JsonObject jsonObject, Class<T> clz) throws APIException {
    	if(jsonObject == null)
    	{
    		return null;
    	}

        JsonPrimitive objectTypeElement = jsonObject.getAsJsonPrimitive(ObjectTypeKey);
        if(objectTypeElement == null && jsonObject.has("error")) {
        	throw parseException(jsonObject.getAsJsonObject("error"));
        }
        else if(objectTypeElement != null) {
	        String objectType = objectTypeElement.getAsString();
	        if(objectType.equals("KalturaAPIException")) {
	        	throw parseException(jsonObject);
	        }
	        clz = getObjectClass(objectType, clz);
        }
        if(clz == Void.class) {
        	return null;
        }
        
        try {
        	Constructor<T> constructor = clz.getConstructor(JsonObject.class);
	        return constructor.newInstance(jsonObject);

        } catch (NoSuchMethodException | SecurityException | InstantiationException |
				IllegalAccessException | IllegalArgumentException | InvocationTargetException |
				ClassCastException | IllegalStateException e) {
			throw new APIException(FailureStep.OnResponse, e);
		}
    }

    public static List<?> parseArray(String result, Class<?>[] types) throws APIException {
        JsonParser jsonParser = new JsonParser();
        JsonElement jsonElement;
        try{
        	jsonElement = jsonParser.parse(result);
        }
        catch(JsonSyntaxException | IllegalStateException e) {
        	throw new APIException(FailureStep.OnResponse, "Invalid JSON response: " + result);
        }

        if(jsonElement.isJsonObject()) {
        	JsonObject jsonObject = jsonElement.getAsJsonObject();
        	if(jsonObject.get(ResultKey) != null && jsonObject.get(ObjectTypeKey) == null) {
        		jsonElement = jsonObject.get(ResultKey);
        	}
        	if(jsonObject.get("error") != null && jsonObject.get(ObjectTypeKey) == null) {
        		jsonElement = jsonObject.getAsJsonObject("error");
        	}
        }
        
        if(jsonElement.isJsonObject()) {
        	JsonObject jsonObject = jsonElement.getAsJsonObject();
	        String objectType = jsonObject.getAsJsonPrimitive(ObjectTypeKey).getAsString();
	        if(objectType.equals("KalturaAPIException")) {
	        	throw parseException(jsonObject);
	        }
        }
        else if(jsonElement.isJsonArray()) {
        	return parseArray(jsonElement.getAsJsonArray(), types);
        }

       	throw new APIException(FailureStep.OnResponse, "Invalid JSON response type, expected array: " + result);
    }

    public static List<?> parseArray(JsonArray jsonArray, Class<?>[] types) throws APIException {
    	if(jsonArray == null)
    	{
    		return null;
    	}
    	
    	List<Object> array = new ArrayList<Object>();
    	int index = 0;
    	for(JsonElement jsonElement : jsonArray) {
    		try{
    			array.add(parseObject(jsonElement, types[index++]));
    		}
    		catch(APIException e) {
    			array.add(e);
    		}
    	}
    	
    	return array;
    }

    public static <T> List<T> parseArray(String result, Class<T> clz) throws APIException {
        JsonParser jsonParser = new JsonParser();
        JsonElement jsonElement;
        try{
        	jsonElement = jsonParser.parse(result);
        }
        catch(JsonSyntaxException | IllegalStateException e) {
        	throw new APIException(FailureStep.OnResponse, "Invalid JSON response: " + result);
        }

        if(jsonElement.isJsonObject()) {
			JsonObject jsonObject = jsonElement.getAsJsonObject();
			if (jsonObject.has(ResultKey)){
				jsonElement = jsonObject.getAsJsonArray(ResultKey);
			}
		}

        if(jsonElement.isJsonObject()) {
        	JsonObject jsonObject = jsonElement.getAsJsonObject();
	        String objectType = jsonObject.getAsJsonPrimitive(ObjectTypeKey).getAsString();
	        if(objectType.equals("KalturaAPIException")) {
	        	throw parseException(jsonObject);
	        }
        }
        else if(jsonElement.isJsonArray()) {
        	return parseArray(jsonElement.getAsJsonArray(), clz);
        }

       	throw new APIException(FailureStep.OnResponse, "Invalid JSON response type, expected array of " + clz.getName() + ": " + result);
    }

    public static <T> List<T> parseArray(JsonArray jsonArray, Class<T> clz) throws APIException {
    	if(jsonArray == null)
    	{
    		return null;
    	}
    	
    	List<T> array = new ArrayList<T>();
    	for(JsonElement jsonElement : jsonArray) {
	        array.add(parseObject(jsonElement, clz));
    	}
    	
    	return array;
    }

    public static <T> ListResponse<T> parseListResponse(String result, Class<T> clz) throws APIException {
        JsonParser jsonParser = new JsonParser();
        JsonObject jsonObject;
        try{
        	jsonObject = jsonParser.parse(result).getAsJsonObject();
        }
        catch(JsonSyntaxException | IllegalStateException e) {
        	throw new APIException(FailureStep.OnResponse, "Invalid JSON response: " + result);
        }

    	if(jsonObject.get(ResultKey) != null && jsonObject.get(ObjectTypeKey) == null) {
    		jsonObject = jsonObject.getAsJsonObject(ResultKey);
    	}
    	if(jsonObject.get("error") != null && jsonObject.get(ObjectTypeKey) == null) {
    		jsonObject = jsonObject.getAsJsonObject("error");
    	}
        
        String objectType = jsonObject.getAsJsonPrimitive(ObjectTypeKey).getAsString();
        if(objectType.equals("KalturaAPIException")) {
        	throw parseException(jsonObject);
        }
        
        ListResponse<T> listResponse = new ListResponse<T>();
         JsonPrimitive totalCount = jsonObject.getAsJsonPrimitive("totalCount");
        if (null != totalCount) {
            listResponse.setTotalCount(totalCount.getAsInt());
        }
        listResponse.setObjects(parseArray(jsonObject.getAsJsonArray("objects"), clz));

        return listResponse;
    }

    public static APIException parseException(String result) {
        JsonParser jsonParser = new JsonParser();
        JsonElement jsonElement;
        try{
        	jsonElement = jsonParser.parse(result);
        }
        catch(JsonSyntaxException | IllegalStateException e) {
        	return new APIException(FailureStep.OnResponse, "Invalid JSON response: " + result);
        }

        if(jsonElement.isJsonObject()) {
        	return parseException(jsonElement.getAsJsonObject());
        }
        
        return null;
    }

    private static APIException parseException(JsonObject jsonObject) {
        String objectType = jsonObject.getAsJsonPrimitive(ObjectTypeKey).getAsString();
        if(objectType.equals("KalturaAPIException")) {
        	return gson.fromJson(jsonObject, APIException.class);
        }
        
        return null;
    }

    public static String parseString(JsonElement jsonElement) {
    	if(jsonElement == null)
    	{
    		return null;
    	}
    	
    	return jsonElement.getAsString();
    }

    public static Integer parseInt(JsonElement jsonElement) {
    	if(jsonElement == null)
    	{
    		return null;
    	}
    	
    	return jsonElement.getAsInt();
    }

    public static Boolean parseBoolean(JsonElement jsonElement) {
    	if(jsonElement == null)
    	{
    		return null;
    	}
    	
    	return jsonElement.getAsBoolean();
    }

    public static Double parseDouble(JsonElement jsonElement) {
    	if(jsonElement == null)
    	{
    		return null;
    	}
    	
    	return jsonElement.getAsDouble();
    }

    public static Long parseLong(JsonElement jsonElement) {
    	if(jsonElement == null)
    	{
    		return null;
    	}
    	
    	return jsonElement.getAsLong();
    }

    public static <T> Map<String, T> parseMap(JsonObject jsonMap, Class<T> clz) throws APIException {
    	if(jsonMap == null)
    	{
    		return null;
    	}
    	
    	JsonObject jsonObject;
    	String objectType;
    	Map<String, T> map = new HashMap<String, T>();
    	
		for (Map.Entry<String, JsonElement> entry : jsonMap.entrySet()) {
    		jsonObject = entry.getValue().getAsJsonObject();
	        objectType = jsonObject.getAsJsonPrimitive(ObjectTypeKey).getAsString();
	        if(objectType.equals("KalturaAPIException")) {
	        	throw parseException(jsonObject);
	        }
	        clz = getObjectClass(objectType, clz);
			map.put(entry.getKey(), parseObject(jsonObject, clz));
		}
    	
    	return map;
    }
}
