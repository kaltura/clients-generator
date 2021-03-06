// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platfroms allow them to do with
// text.
//
// Copyright (C) 2006-2011  Kaltura Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// @ignore
// ===================================================================================================
package com.kaltura.client.types;

import android.os.Parcel;
import android.os.Parcelable;

import com.google.gson.JsonObject;
import com.kaltura.client.Params;
import com.kaltura.client.utils.GsonParser;
import com.kaltura.client.utils.response.ResponseType;

import java.util.Map;


@SuppressWarnings("serial")
public class ObjectBase implements Parcelable, ResponseType {

    public interface Tokenizer<I> {
    }

    private Params params = null;

    @SuppressWarnings("rawtypes")
    protected Map<String, ListResponse> relatedObjects;

    @SuppressWarnings("rawtypes")
    public Map<String, ListResponse> getRelatedObjects() {
        return relatedObjects;
    }

    @SuppressWarnings("rawtypes")
    public void setRelatedObjects(Map<String, ListResponse> relatedObjects) {
        this.relatedObjects = relatedObjects;
    }

    public ObjectBase() {
    }

    public ObjectBase(JsonObject jsonObject) throws APIException {
        if(jsonObject == null) return;

        // set members values:
        try
        {
            relatedObjects = GsonParser.parseMap(jsonObject.getAsJsonObject("relatedObjects"), ListResponse.class);
        }
        catch (ClassCastException e)
        {
            relatedObjects = GsonParser.parseMap(new JsonObject(), ListResponse.class);
        }
    }

    public void setToken(String key, String token) {
        if(params == null) {
            params = new Params();
        }
        params.add(key, token);
    }

    public Params toParams() {
        if(params == null) {
            params = new Params();
        }
        return params;
    }

    public static final Creator<ObjectBase> CREATOR = new Creator<ObjectBase>() {
        @Override
        public ObjectBase createFromParcel(Parcel in) {
            return new ObjectBase(in);
        }

        @Override
        public ObjectBase[] newArray(int size) {
            return new ObjectBase[size];
        }
    };

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
    }

    protected ObjectBase(Parcel in) {
    }

}
