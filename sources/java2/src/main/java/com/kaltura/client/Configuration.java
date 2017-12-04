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

import com.kaltura.client.utils.request.ConnectionConfiguration;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;

/**
 * This class holds information needed by the Kaltura client to establish a session.
 * 
 * @author jpotts
 *
 */
public class Configuration implements Serializable, ConnectionConfiguration {

	private static final long serialVersionUID = 2096581946429839651L;
	
	public final static String EndPoint = "endpoint";
	public final static String ConnectTimeout = "connectTimeout";
	public final static String ReadTimeout = "readTimeout";
	public final static String WriteTimeout = "writeTimeout";
	public final static String MaxRetry = "maxRetry";
	public final static String AcceptGzipEncoding = "acceptGzipEncoding";
	public final static String ResponseTypeFormat = "responseTypeFormat";

	/*private String endpoint = "";
	private int connectTimeout = 20000;
	private int readTimeout = connectTimeout;
	private int writeTimeout = 30000;
	private int maxRequestRetry = 3;
    private KalturaServiceResponseTypeFormat serviceFormat = KalturaServiceResponseTypeFormat.RESPONSE_TYPE_JSON;
	private Map<String, String> params;*/

	private Map<String, Object> params;

	public static ConnectionConfiguration getDefaults(){
		return new Configuration();
	}

	public Configuration(){
		initDefaults();
	}

	public Configuration(Map<String, Object> config){
		initDefaults();
		params.putAll(config);
	}

	public Configuration(ConnectionConfiguration config) {
		params = new HashMap<String, Object>();
		params.put(ConnectTimeout, config.getConnectTimeout());
		params.put(ReadTimeout, config.getReadTimeout());
		params.put(WriteTimeout, config.getWriteTimeout());
		params.put(MaxRetry, config.getMaxRetry(2));
		params.put(AcceptGzipEncoding, config.getAcceptGzipEncoding());
		params.put(ResponseTypeFormat, config.getTypeFormat());
		params.put(EndPoint, config.getEndpoint());
	}

	private void initDefaults() {
		params = new HashMap<String, Object>();
		params.put(ConnectTimeout, 20000);
		params.put(ReadTimeout, 20000);
		params.put(WriteTimeout, 30000);
		params.put(MaxRetry, 3);
		params.put(AcceptGzipEncoding, false);
		params.put(ResponseTypeFormat, ServiceResponseTypeFormat.RESPONSE_TYPE_JSON.getValue());
		params.put(EndPoint, "http://www.kaltura.com/");
	}


	public String getEndpoint() {
		return (String) params.get(EndPoint);
	}

	@Override
	public int getTypeFormat() {
		return (int) params.get(ResponseTypeFormat);
	}

	public void setEndpoint(String endpoint) {
		this.params.put(EndPoint, endpoint);
	}

	public Map<String, Object> getParams() {
		return params;
	}

	public void setParams(Map<String, Object> params) {
		this.params = params;
	}

	public void setParam(String key, Object value){
		params.put(key, value);
	}

	public Object getParam(String key){
		return params.get(key);
	}

	public Object getParam(String key, Object defaultValue){
		return params.containsKey(key) ? params.get(key) : defaultValue;
	}

	public int getConnectTimeout() {
		return (int) params.get(ConnectTimeout);
	}

	public void setConnectTimeout(int connectTimeout) {
		params.put(ConnectTimeout, connectTimeout);
	}

	public int getReadTimeout() {
		return (int) params.get(ReadTimeout);
	}

	public void setReadTimeout(int readTimeout) {
		params.put(ReadTimeout, readTimeout);
	}

	public int getWriteTimeout() {
		return (int) params.get(WriteTimeout);
	}

	public void setWriteTimeout(int writeTimeout) {
		params.put(WriteTimeout, writeTimeout);
	}

	/**
	 * Return whether to accept GZIP encoding, that is, whether to
	 * send the HTTP "Accept-Encoding" header with "gzip" as value.
	 */
	public boolean getAcceptGzipEncoding(){
		return (boolean) params.get(AcceptGzipEncoding);
	}

	/**
	 * Set whether to accept GZIP encoding, that is, whether to
	 * send the HTTP "Accept-Encoding" header with "gzip" as value.
	 * <p>Default is "true". Turn this flag off if you do not want
	 * GZIP response compression even if enabled on the HTTP server.
	 * 
	 * @param accept accept or not
	 */
	public void setAcceptGzipEncoding(boolean accept){
		params.put(AcceptGzipEncoding, accept);
	}

	public void setServiceResponseTypeFormat(ServiceResponseTypeFormat format) {
		params.put(ResponseTypeFormat, format.getValue());
	}

	public ServiceResponseTypeFormat getServiceResponseTypeFormat() {
		return ServiceResponseTypeFormat.get((int) params.get(ResponseTypeFormat));
	}

	public void setMaxRetry(int retry) {
		params.put(MaxRetry, retry);
	}

	public int getMaxRetry(int defaultVal) {
		return params.containsKey(MaxRetry) ? (int) params.get(MaxRetry) : defaultVal;
	}
}
