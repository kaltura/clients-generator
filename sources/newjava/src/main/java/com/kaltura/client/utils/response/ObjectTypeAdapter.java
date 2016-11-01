package com.kaltura.client.utils.response;

import com.google.gson.*;
import com.kaltura.client.utils.ObjectClassFactory;

import java.lang.reflect.Type;

/**
 * Created by tehilarozin on 24/07/2016.
 */
public class ObjectTypeAdapter implements JsonDeserializer<ResponseType> {

    @Override
    public ResponseType deserialize(JsonElement json, Type typeOfT, JsonDeserializationContext context) throws JsonParseException {

        try {

            if(json.isJsonObject()){
                JsonObject jsonObject = json.getAsJsonObject();

                if(jsonObject.has("objectType")) {

                    // parsing with reflection through constructors
                    String objectType = jsonObject.getAsJsonPrimitive("objectType").getAsString();
                    //!! due to Server bug on multirequest:
                    if(objectType.equals("KalturaAssetInfo")){
                        objectType = "KalturaMediaAsset"; //-> can be KalturaProgramAsset in case of type EPG
                    }
                    return ObjectClassFactory.getResponseObject(objectType, jsonObject);

                    /*-> the Gson auto parse didn't work when parsing sub classes that recognized with parent class type
                    if(jsonObject.get("objectType").getAsString().equals("done")){
                        return new Gson().fromJson(jsonObject,  typeOfT);
                    } else {*/
                    // completely auto parsing with Gson.
                    /*Class<?> clz = ObjectClassFactory.getResponseClassType(jsonObject.getAsJsonPrimitive("objectType").getAsString());
                    jsonObject.addProperty("objectType", "done");
                    ResponseType responseType = (ResponseType) *//*new Gson()*//**//*GsonParser.createGson().fromJson*//*context.deserialize(jsonObject, clz);
                    //ResponseType responseType = (ResponseType) new Gson().fromJson(jsonObject, clz);
                    return responseType;*/

                } else { // in case json do not have the "objectType" property, parse next the element inside it
                    return deserialize(jsonObject.entrySet().iterator().next().getValue(), typeOfT, context );
                }
            }

        } catch (Exception e) {
            e.printStackTrace();
        }

        return null;
    }
}
