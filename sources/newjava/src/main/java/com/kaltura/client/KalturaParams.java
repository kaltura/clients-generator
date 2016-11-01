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
package com.kaltura.client;

import com.google.gson.Gson;
import com.kaltura.client.enums.KalturaEnumAsInt;
import com.kaltura.client.enums.KalturaEnumAsString;
import com.kaltura.client.types.KalturaAPIException;
import com.kaltura.client.types.KalturaObjectBase;
import com.kaltura.client.utils.request.KalturaRequestBuilder;
import org.json.JSONArray;

import java.io.Serializable;
import java.util.*;

/**
 * Helper class that provides a collection of Kaltura parameters (key-value
 * pairs).
 *
 * @author jpotts
 *
 */
public class KalturaParams extends LinkedHashMap<String, Object> implements Serializable  {

	public String toQueryString() {
		return toQueryString(null);
	}

	public String toQueryString(String prefix) {

		StringBuffer str = new StringBuffer();
		Object value;

		for (String key : getKeys()) {
			if(!containsKey(key)){
				continue;
			}

			if (str.length() > 0) {
				str.append("&");
			}

			value = get(key);

			if (prefix != null) {
				key = prefix + "[" + key + "]";
			}
			if (value instanceof KalturaParams) {
				str.append(((KalturaParams) value).toQueryString(key));
			} else {
				str.append(key);
				str.append("=");
				str.append(value);
			}
		}

		return str.toString();
	}

	public void add(String key, int value) {
		if (value == KalturaParamsValueDefaults.KALTURA_UNDEF_INT) {
			return;
		}

		if (value == KalturaParamsValueDefaults.KALTURA_NULL_INT) {
			putNull(key);
			return;
		}

		if(key != null) {
			put(key, value);
		}
	}

	public void add(String key, long value) {
		if (value == KalturaParamsValueDefaults.KALTURA_UNDEF_LONG) {
			return;
		}
		if (value == KalturaParamsValueDefaults.KALTURA_NULL_LONG) {
			putNull(key);
			return;
		}

		if(key != null) {
			put(key, value);
		}
	}

	public void add(String key, double value) {
		if (value == KalturaParamsValueDefaults.KALTURA_UNDEF_DOUBLE) {
			return;
		}
		if (value == KalturaParamsValueDefaults.KALTURA_NULL_DOUBLE) {
			putNull(key);
			return;
		}

		if(key != null) {
			put(key, value);
		}
	}

	public void add(String key, String value) {
		if (value == null) {
			return;
		}

		if (value.equals(KalturaParamsValueDefaults.KALTURA_NULL_STRING)) {
			putNull(key);
			return;
		}

		if(key != null) {
			put(key, value);
		}
	}

	public void add(String key, KalturaObjectBase object) /*throws KalturaAPIException*/{
		if (object == null || key == null)
			return;


		put(key, object.toParams());
	}

	public <T extends KalturaObjectBase> void add(String key, ArrayList<T> array)
			/*throws KalturaAPIException*/ {
		if (array == null)
			return;

		if (array.isEmpty()) {
			KalturaParams emptyParams = new KalturaParams();
			emptyParams.put("-", "");
			put(key, emptyParams);

		} else {
			JSONArray arrayParams = new JSONArray();
			for (KalturaObjectBase baseObj : array) {
				arrayParams.put(baseObj.toParams());
			}

			put(key, arrayParams);
		}
	}

	/*public <T extends KalturaObjectBase> void add(String key,
												  HashMap<String, T> map) throws KalturaAPIException {
		if (map == null)
			return;

		if (map.isEmpty()) {
			KalturaParams emptyParams = new KalturaParams();
			//try {
				emptyParams.put("-", "");
				put(key, emptyParams);
			*//*} catch (JSONException e) {
				throw new KalturaAPIException(e.getMessage());
			}*//*
		} else {
			KalturaParams mapParams = new KalturaParams();
			for (String itemKey : map.keySet()) {
				KalturaObjectBase baseObj = map.get(itemKey);
				mapParams.add(itemKey, baseObj);
			}
			//try {
				put(key, mapParams);
			*//*} catch (JSONException e) {
				throw new KalturaAPIException(e.getMessage());
			}*//*
		}
	}
*/
	public <T extends KalturaObjectBase> void add(String key, Map<String, T>  params) {
			if (containsKey(key) && get(key) instanceof HashMap) {
				KalturaParams existingParams = (KalturaParams) get(key);
				existingParams.putAll(params);
			} else {
				put(key, params);
			}
	}

	public Iterable<String> getKeys() {
		return keySet();
	}

	/*private void putAll(KalturaParams params) {
		for (String key : params.getKeys()) {
			put(key, params.get(key));
		}
	}*/

	public void add(String key, KalturaParams params) {
		put(key, params);
	}

	protected void putNull(String key) {
		put(key + "__null", "");
	}

	/**
	 * Pay attention - this function does not check if the value is null.
	 * neither it supports setting value to null.
	 */
	public void add(String key, boolean value) {
		put(key, value);
	}

	/**
	 * Pay attention - this function does not support setting value to null.
	 */
	public void add(String key, KalturaEnumAsString value) {
		if (value == null)
			return;

		add(key, value.getValue());
	}

	/**
	 * Pay attention - this function does not support setting value to null.
	 */
	public void add(String key, KalturaEnumAsInt value)
			/*throws KalturaAPIException*/ {
		if (value == null)
			return;

		add(key, value.getValue());
	}

	/*public boolean containsKey(String key) {
		return containsKey(key);
	}*/

	public void clear() {
		for (Object key : getKeys()) {
			remove((String) key);
		}
	}

	public KalturaParams getParams(String key) throws KalturaAPIException {
		if (!containsKey(key))
			return null;

		Object value = get(key);
		
		if (value instanceof KalturaParams)
			return (KalturaParams) value;

		throw new KalturaAPIException("Key value [" + key
				+ "] is not instance of KalturaParams");
	}

	@Override
	public String toString() {
		return new Gson().toJson(this, Map.class);
	}

	public void add(String key, KalturaRequestBuilder request) {
		put(key, request);
	}
}
