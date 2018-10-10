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
import com.kaltura.client.enums.EnumAsInt;
import com.kaltura.client.enums.EnumAsString;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.ObjectBase;

import java.io.Serializable;
import java.util.*;

/**
 * Helper class that provides a collection of Kaltura parameters (key-value
 * pairs).
 *
 * @author jpotts
 *
 */
@SuppressWarnings("serial")
public class Params extends LinkedHashMap<String, Object> implements Serializable  {

	private static Gson gson = new Gson();
	
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
			if (value instanceof Params) {
				str.append(((Params) value).toQueryString(key));
			} else {
				str.append(key);
				str.append("=");
				str.append(value);
			}
		}

		return str.toString();
	}

	public void add(String key, Integer value) {
		if (value == null || value == ParamsValueDefaults.KALTURA_UNDEF_INT) {
			return;
		}

		if (value == ParamsValueDefaults.KALTURA_NULL_INT) {
			putNull(key);
			return;
		}

		if(key != null) {
			put(key, value);
		}
	}

	public void add(String key, Long value) {
		if (value == null || value == ParamsValueDefaults.KALTURA_UNDEF_LONG) {
			return;
		}
		if (value == ParamsValueDefaults.KALTURA_NULL_LONG) {
			putNull(key);
			return;
		}

		if(key != null) {
			put(key, value);
		}
	}

	public void add(String key, Double value) {
		if (value == null || value == ParamsValueDefaults.KALTURA_UNDEF_DOUBLE) {
			return;
		}
		if (value == ParamsValueDefaults.KALTURA_NULL_DOUBLE) {
			putNull(key);
			return;
		}

		if(key != null) {
			put(key, value);
		}
	}

	public void add(String key, Boolean value) {
		if (value == null) {
			return;
		}
		
		put(key, value);
	}


	public void add(String key, String value) {
		if (value == null) {
			return;
		}

		if (value.equals(ParamsValueDefaults.KALTURA_NULL_STRING)) {
			putNull(key);
			return;
		}

		if(key != null) {
			put(key, value);
		}
	}

	public void add(String key, ObjectBase object) {
		if (object == null || key == null)
			return;


		put(key, object.toParams());
	}

	public <T extends ObjectBase> void add(String key, List<T> array) {
		if (array == null)
			return;

		if (array.isEmpty()) {
			Params emptyParams = new Params();
			emptyParams.put("-", "");
			put(key, emptyParams);

		} else if(array.get(0) instanceof ObjectBase) {
			List<Params> list = new ArrayList<Params>();
			for(ObjectBase item : array) {
				list.add(item.toParams());
			}
			put(key, list);
		}
		else {
			List<String> list = new ArrayList<String>();
			for(ObjectBase item : array) {
				list.add(item.toString());
			}
			put(key, list);
		}
	}

	public void link(String destKey, String requestId, String sourceKey) {
		String source = "{" + requestId + ":result:" + sourceKey.replace(".", ":") + "}";
		Deque<String> destinationKeys = new LinkedList<String>(Arrays.asList(destKey.split("\\.")));
		link(destinationKeys, source);
    }

	@SuppressWarnings({ "unchecked", "rawtypes" })
	protected void link(Deque<String> destinationKeys, String source) {
		String destination = destinationKeys.pollFirst();
		if(destinationKeys.size() == 0) {
			put(destination, source);
		}
		else if(destinationKeys.getFirst().matches("^\\d+$")) {
			int index = Integer.valueOf(destinationKeys.pollFirst());
			List destinationList;
			if(containsKey(destination) && get(destination) instanceof List) {
				destinationList = (List) get(destination);
			}
			else {
				destinationList = new ArrayList();
			}

			if(destinationKeys.size() == 0) {
				destinationList.set(index, source);
			}
			else {
				Params destinationParams;
				if(destinationList.size() > index && destinationList.get(index) instanceof Params) {
					destinationParams = (Params) destinationList.get(index);
				}
				else {
					destinationParams = new Params();
					destinationList.add(destinationParams);
				}
				destinationParams.link(destinationKeys, source);
			}
		}
		else {
			Params destinationParams = null;
			if(containsKey(destination) && get(destination) instanceof Params) {
				destinationParams = (Params) get(destination);
			}
			else {
				destinationParams = new Params();
				put(destination, destinationParams);
			}
			destinationParams.link(destinationKeys, source);
		}
	}

	@SuppressWarnings("unchecked")
	public <T extends ObjectBase> void add(String key, Map<String, T>  map) {
		if (map == null)
			return;

		if (map.isEmpty()) {
			Params emptyParams = new Params();
			emptyParams.put("-", "");
			put(key, emptyParams);

		} 
		else {
			Map<String, Params> items = new HashMap<String, Params>();
			for(String subKey : map.keySet()) {
				items.put(subKey, map.get(subKey).toParams());
			}
			
			if (containsKey(key) && get(key) instanceof Map) {
				Map<String, Params> existingKeys = (Map<String, Params>) get(key);
				existingKeys.putAll(items);
			} else {
				put(key, items);
			}			
		}		
	}

	public Iterable<String> getKeys() {
		return keySet();
	}

	public void add(String key, Params params) {
		put(key, params);
	}

	protected void putNull(String key) {
		put(key + "__null", "");
	}
	
	/**
	 * Pay attention - this function does not support setting value to null.
	 * 
	 * @param key param name
	 * @param value param value
	 */
	public void add(String key, EnumAsString value) {
		if (value == null)
			return;

		add(key, value.getValue());
	}

	/**
	 * Pay attention - this function does not support setting value to null.
	 * 
	 * @param key param name
	 * @param value param value
	 */
	public void add(String key, EnumAsInt value)  {
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

	public Params getParams(String key) throws APIException {
		if (!containsKey(key))
			return null;

		Object value = get(key);
		
		if (value instanceof Params)
			return (Params) value;

		throw new APIException("Key value [" + key
				+ "] is not instance of Params");
	}

	@Override
	public String toString() {
		return gson.toJson(this);
	}
}
