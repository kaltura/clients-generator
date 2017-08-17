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
// Copyright (C) 2006-2017  Kaltura Inc.
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

import java.util.List;

import com.google.gson.JsonObject;
import com.google.gson.JsonPrimitive;
import com.kaltura.client.Params;
import com.kaltura.client.utils.GsonParser;
import com.kaltura.client.utils.request.RequestBuilder;


@SuppressWarnings("serial")
public class ListResponse<T> extends ObjectBase {

	public interface Tokenizer<T> {
		String totalCount();
		RequestBuilder.ListTokenizer<T> objects();
	}
	
    private int totalCount = Integer.MIN_VALUE;
    private List<T> objects;

    // totalCount:
    public int getTotalCount(){
        return this.totalCount;
    }
    public void setTotalCount(int totalCount){
        this.totalCount = totalCount;
    }

    // objects:
    public List<T> getObjects(){
        return this.objects;
    }
    public void setObjects(List<T> objects){
        this.objects = objects;
    }


    public ListResponse() {
       super();
    }
    
    @SuppressWarnings("unchecked")
	public ListResponse(JsonObject jsonObject) throws APIException {
        if(jsonObject == null) 
        	return;

        Class<ObjectBase> cls = ObjectBase.class;
        JsonPrimitive objectTypeElement = jsonObject.getAsJsonPrimitive("objectType");
        if(objectTypeElement != null) {
	        String objectType = objectTypeElement.getAsString().replaceAll("ListResponse$", "");
	        cls = GsonParser.getObjectClass(objectType, cls);
        }
        
        // set members values:
        totalCount = GsonParser.parseInt(jsonObject.get("totalCount"));
        objects = (List<T>) GsonParser.parseArray(jsonObject.getAsJsonArray("objects"), cls);
    }

    public Params toParams() {
        Params kparams = super.toParams();
        kparams.add("objectType", "KalturaListResponse");
        return kparams;
    }

}

