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
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;


/**
 * Helper class that provides a collection of Kaltura parameters (key-value
 * pairs).
 *
 * @author jpotts
 *
 */
public class KalturaParamsJSON extends JSONObject implements Serializable  {

	public String toQueryString() throws KalturaAPIException {
		return toQueryString(null);
	}

	public String toQueryString(String prefix) throws KalturaAPIException {

		StringBuffer str = new StringBuffer();
		Object value;

		for (String key : getKeys()) {
			if (str.length() > 0) {
				str.append("&");
			}

			try {
				value = get(key);
			} catch (JSONException e) {
				throw new KalturaAPIException(e.getMessage());
			}

			if (prefix != null) {
				key = prefix + "[" + key + "]";
			}
			if (value instanceof KalturaParamsJSON) {
				str.append(((KalturaParamsJSON) value).toQueryString(key));
			} else {
				str.append(key);
				str.append("=");
				str.append(value);
			}
		}

		return str.toString();
	}

	public void add(String key, int value) throws KalturaAPIException {
		if (value == KalturaParamsValueDefaults.KALTURA_UNDEF_INT) {
			return;
		}

		if (value == KalturaParamsValueDefaults.KALTURA_NULL_INT) {
			putNull(key);
			return;
		}

		try {
			put(key, value);
		} catch (JSONException e) {
			throw new KalturaAPIException(e.getMessage());
		}
	}

	public void add(String key, long value) throws KalturaAPIException {
		if (value == KalturaParamsValueDefaults.KALTURA_UNDEF_LONG) {
			return;
		}
		if (value == KalturaParamsValueDefaults.KALTURA_NULL_LONG) {
			putNull(key);
			return;
		}

		try {
			put(key, value);
		} catch (JSONException e) {
			throw new KalturaAPIException(e.getMessage());
		}
	}

	public void add(String key, double value) throws KalturaAPIException {
		if (value == KalturaParamsValueDefaults.KALTURA_UNDEF_DOUBLE) {
			return;
		}
		if (value == KalturaParamsValueDefaults.KALTURA_NULL_DOUBLE) {
			putNull(key);
			return;
		}

		try {
			put(key, value);
		} catch (JSONException e) {
			throw new KalturaAPIException(e.getMessage());
		}
	}

	public void add(String key, String value) throws KalturaAPIException {
		if (value == null) {
			return;
		}

		if (value.equals(KalturaParamsValueDefaults.KALTURA_NULL_STRING)) {
			putNull(key);
			return;
		}

		try {
			put(key, value);
		} catch (JSONException e) {
			throw new KalturaAPIException(e.getMessage());
		}
	}

	public void add(String key, KalturaObjectBase object)
			throws KalturaAPIException {
		if (object == null)
			return;

		try {
			put(key, object.toParams());
		} catch (JSONException e) {
			throw new KalturaAPIException(e.getMessage());
		}
	}

	public <T extends KalturaObjectBase> void add(String key, ArrayList<T> array)
			throws KalturaAPIException {
		if (array == null)
			return;

		if (array.isEmpty()) {
			KalturaParamsJSON emptyParams = new KalturaParamsJSON();
			try {
				emptyParams.put("-", "");
				put(key, emptyParams);
			} catch (JSONException e) {
				throw new KalturaAPIException(e.getMessage());
			}
		} else {
			JSONArray arrayParams = new JSONArray();
			for (KalturaObjectBase baseObj : array) {
				arrayParams.put(baseObj.toParams());
			}
			try {
				put(key, arrayParams);
			} catch (JSONException e) {
				throw new KalturaAPIException(e.getMessage());
			}
		}
	}

	public <T extends KalturaObjectBase> void add(String key,
												  HashMap<String, T> map) throws KalturaAPIException {
		if (map == null)
			return;

		if (map.isEmpty()) {
			KalturaParams emptyParams = new KalturaParams();
			try {
				emptyParams.put("-", "");
				put(key, emptyParams);
			} catch (JSONException e) {
				throw new KalturaAPIException(e.getMessage());
			}
		} else {
			KalturaParams mapParams = new KalturaParams();
			for (String itemKey : map.keySet()) {
				KalturaObjectBase baseObj = map.get(itemKey);
				mapParams.add(itemKey, baseObj);
			}
			try {
				put(key, mapParams);
			} catch (JSONException e) {
				throw new KalturaAPIException(e.getMessage());
			}
		}
	}

	public <T extends KalturaObjectBase> void add(String key,
												  KalturaParamsJSON params) throws KalturaAPIException {
		try {
			if (params instanceof KalturaParamsJSON && has(key)
					&& get(key) instanceof HashMap) {
				KalturaParamsJSON existingParams = (KalturaParamsJSON) get(key);
				existingParams.putAll(params);
			} else {
				put(key, params);
			}
		} catch (JSONException e) {
			throw new KalturaAPIException(e.getMessage());
		}
	}

	public Iterable<String> getKeys() {
		return new Iterable<String>() {
			@SuppressWarnings("unchecked")
			public Iterator<String> iterator() {
				return (Iterator<String>) keys();
			}
		};
	}

	private void putAll(KalturaParamsJSON params) throws KalturaAPIException {
		for (String key : params.getKeys()) {

			try {
				put(key, params.get(key));
			} catch (JSONException e) {
				throw new KalturaAPIException(e.getMessage());
			}
		}
	}

	public void add(KalturaParamsJSON objectProperties) throws KalturaAPIException {
		putAll(objectProperties);
	}

	protected void putNull(String key) throws KalturaAPIException {
		try {
			put(key + "__null", "");
		} catch (JSONException e) {
			throw new KalturaAPIException(e.getMessage());
		}
	}

	/**
	 * Pay attention - this function does not check if the value is null.
	 * neither it supports setting value to null.
	 */
	public void add(String key, boolean value) throws KalturaAPIException {
		try {
			put(key, value);
		} catch (JSONException e) {
			throw new KalturaAPIException(e.getMessage());
		}
	}

	/**
	 * Pay attention - this function does not support setting value to null.
	 */
	public void add(String key, KalturaEnumAsString value)
			throws KalturaAPIException {
		if (value == null)
			return;

		add(key, value.getValue());
	}

	/**
	 * Pay attention - this function does not support setting value to null.
	 */
	public void add(String key, KalturaEnumAsInt value)
			throws KalturaAPIException {
		if (value == null)
			return;

		add(key, value.getValue());
	}

	public boolean containsKey(String key) {
		return has(key);
	}

	public void clear() {
		for (Object key : getKeys()) {
			remove((String) key);
		}
	}

	public KalturaParamsJSON getParams(String key) throws KalturaAPIException {
		if (!has(key))
			return null;

		Object value;
		try {
			value = get(key);
		} catch (JSONException e) {
			throw new KalturaAPIException(e.getMessage());
		}
		if (value instanceof KalturaParamsJSON)
			return (KalturaParamsJSON) value;

		throw new KalturaAPIException("Key value [" + key
				+ "] is not instance of KalturaParams");
	}

	@Override
	public String toString() {
		return new Gson().toJson(this, Map.class);
	}

}
