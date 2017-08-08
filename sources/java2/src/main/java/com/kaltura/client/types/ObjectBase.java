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

import java.io.Serializable;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Map;

import com.google.gson.JsonObject;
import com.kaltura.client.Params;
import com.kaltura.client.utils.GsonParser;
import com.kaltura.client.utils.response.ResponseType;

@SuppressWarnings("serial")
public class ObjectBase implements Serializable, ResponseType {

	public static class MultiRequestTokens {
		protected String prefix;
		
		public MultiRequestTokens(String prefix) {
			this.prefix = prefix;
		}
		
		protected String tokenize(String propertyName) {
			return String.format("{%s:%s}", prefix, propertyName);
		}
	}
	
	public static class BaseMultiRequestTokens<T extends ObjectBase.MultiRequestTokens> extends MultiRequestTokens {
		private Class<? extends ObjectBase> type;
		
		public BaseMultiRequestTokens(String prefix, Class<? extends ObjectBase> type) {
			super(prefix);
			this.type = type;
		}

		@SuppressWarnings("unchecked")		
		protected T get(String key) throws APIException {
			try {
				Method getter = type.getDeclaredMethod("getMultiRequestTokens", String.class);
				return (T) getter.invoke(null, prefix + ":" + key);
			} catch (NoSuchMethodException | SecurityException | IllegalAccessException | IllegalArgumentException | InvocationTargetException e) {
				throw new APIException(APIException.FailureStep.OnRequest, "Could not get multi-request token");
			}
		}

		@SuppressWarnings("unchecked")
		protected <U extends ObjectBase, V extends T> V get(String key, Class<U> parentType, Class<V> returnType) throws APIException {
			try {
				Method getter = parentType.getDeclaredMethod("getMultiRequestTokens", String.class);
				return (V) getter.invoke(null, prefix + ":" + key);
			} catch (NoSuchMethodException | SecurityException | IllegalAccessException | IllegalArgumentException | InvocationTargetException e) {
				throw new APIException(APIException.FailureStep.OnRequest, "Could not get multi-request token");
			}
		}
	}
	
	public static class MapMultiRequestTokens<T extends ObjectBase.MultiRequestTokens> extends BaseMultiRequestTokens<T> {
		
		public MapMultiRequestTokens(String prefix, Class<? extends ObjectBase> type) {
			super(prefix, type);
		}

		public T get(String key) throws APIException {
			return super.get(key);
		}

		public <U extends ObjectBase, V extends T> V get(String key, Class<U> parentType, Class<V> returnType) throws APIException {
			return super.get(key, parentType, returnType);
		}
	}

	public static class ListMultiRequestTokens<T extends ObjectBase.MultiRequestTokens> extends MapMultiRequestTokens<T> {
		
		public ListMultiRequestTokens(String prefix, Class<? extends ObjectBase> type) {
			super(prefix, type);
		}
		
		public T get(int index) throws APIException {
			return super.get("" + index);
		}
		
		public <U extends ObjectBase, V extends T> V get(int index, Class<U> parentType, Class<V> returnType) throws APIException {
			return super.get("" + index, parentType, returnType);
		}
	}
	

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
        relatedObjects = GsonParser.parseMap(jsonObject.getAsJsonObject("relatedObjects"), ListResponse.class);
    }
    
	public Params toParams() {
		return new Params();
	}
    
	protected static MultiRequestTokens getMultiRequestTokens(String prefix) {
		return new MultiRequestTokens(prefix);
	}
}
