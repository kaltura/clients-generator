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
package com.kaltura.client.utils;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.kaltura.client.IKalturaLogger;
import com.kaltura.client.KalturaLogger;
import com.kaltura.client.types.KalturaAPIException;
import com.kaltura.client.utils.response.base.GeneralResponse;
import org.w3c.dom.Node;

import java.util.ArrayList;
import java.util.HashMap;

public final class ParseUtils {

	private static IKalturaLogger logger = KalturaLogger.getLogger(ParseUtils.class);

	public static Class<?> getClass(String name){
		try {
			return Class.forName(name);
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		}
		return Object.class;
	}

	public static String parseString(String txt) {
		 return txt;
	}

	public static String parseString(JsonObject jsonObject, String property) {
		return GsonParser.parseString(jsonObject, property);
	}

	public static int parseInt(String txt) {
		if (txt.length() != 0) {
			try {
				return Integer.parseInt(txt);
			} catch (NumberFormatException nfe) {
				if (logger.isEnabled())
					logger.warn("Failed to parse [" + txt + "] as int", nfe);
			}
		}
		return 0;
	}

	public static int parseInt(JsonObject jsonObject, String property) {
		return GsonParser.parseInt(jsonObject, property);
	}

	public static long parseBigint(String txt) {
		if (txt.length() != 0) {
			try {
				return Long.parseLong(txt);
			} catch (NumberFormatException nfe) {
				if (logger.isEnabled())
					logger.warn("Failed to parse [" + txt + "] as long", nfe);
			}
		}
		return 0;
	}

	public static long parseLong(JsonObject jsonObject, String property) {
		return GsonParser.parseLong(jsonObject, property);
	}

	public static double parseDouble(String txt) {
		if (txt.length() != 0) {
			try {
				return Double.parseDouble(txt);
			} catch (NumberFormatException nfe) {
				if (logger.isEnabled())
					logger.warn("Failed to parse [" + txt + "] as double", nfe);
			}
		}
		return 0;
	}

	public static double parseDouble(JsonObject jsonObject, String property) {
		return GsonParser.parseDouble(jsonObject, property);
	}

	public static boolean parseBool(String txt) {
		 return !txt.equals("0");
	}

	public static boolean parseBoolean(JsonObject jsonObject, String property) {
		return GsonParser.parseBoolean(jsonObject, property);
	}

	@SuppressWarnings("unchecked")
    public static <T> T parseObject(JsonElement jsonElement, Class clz) {
        return (T) GsonParser.parseObject(jsonElement, clz);
    }

	public static <T> T parseObject(Node node, Class clz) {
		try {
			return (T) XMLParser.parseObject(node, clz);

		} catch (KalturaAPIException e) {
			e.printStackTrace();
			logger.error("failed parsing xml object from "+node.getNodeName());
		}
		return null;
	}

	public static <T> T parseObject(JsonObject jsonObject, String property, Class clz) {
		try {
			return GsonParser.parseObject(jsonObject, property, clz);

		} catch (Exception e) {
			e.printStackTrace();
			logger.error("failed parsing xml object from "+property);
		}

		return null;
	}

	public static <T> HashMap<String, T> parseMap(JsonObject jsonObject, String property, Class clz){
		return GsonParser.parseMap(jsonObject, property, clz);
	}

	public static <T> ArrayList<T> parseArray(JsonObject jsonObject, String property, Class clz){
		return GsonParser.parseArray(jsonObject, property, clz);
	}

	public static <T> HashMap<String, T> parseMap(Node aNode, Class clz){
		try {
			return XMLParser.parseMap(aNode, clz);

		} catch (KalturaAPIException e) {
			e.printStackTrace();
			logger.error("failed parsing xml Map from "+aNode.getNodeName());
		}
		return null;
	}

	public static <T> ArrayList<T> parseArray(Node aNode, Class clz){
		try {
			return XMLParser.parseArray(aNode, clz);

		} catch (KalturaAPIException e) {
			e.printStackTrace();
			logger.error("failed parsing xml Array from "+aNode.getNodeName());
		}
		return null;
	}

	public static String toJson(Object object) {
		return GsonParser.toJson(object);
	}

	public static GeneralResponse parseResult(String result, String format) {
		if(format.equals( "json")) {
			return GsonParser.parseResult(result);
		}
		return null;
	}

	public static String convertToHex(byte[] data) {
		/*check this out:
		* final StringBuilder builder = new StringBuilder();
			for(byte b : in) {
				builder.append(String.format("%02x", b));
			}
			return builder.toString();*/

		StringBuffer buf = new StringBuffer();
		for (int i = 0; i < data.length; i++) {
			int halfbyte = (data[i] >>> 4) & 0x0F;
			int two_halfs = 0;
			do {
				if ((0 <= halfbyte) && (halfbyte <= 9))
					buf.append((char) ('0' + halfbyte));
				else
					buf.append((char) ('a' + (halfbyte - 10)));
				halfbyte = data[i] & 0x0F;
			} while(two_halfs++ < 1);
		}
		return buf.toString();
	}
}
