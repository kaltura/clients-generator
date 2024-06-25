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
import java.net.Proxy;

/**
 * This class holds information needed by the Kaltura client to establish a session.
 * 
 * @author jpotts
 *
 */
public class Configuration implements Serializable, ConnectionConfiguration {

	private static final long serialVersionUID = 2096581946429839651L;
	
	public final static String Endpoint = "endpoint";
	public final static String PROXY = "proxy";
	public final static String PROXY_PORT = "proxyPort";
	public final static String PROXY_TYPE = "proxyType";
	public final static String PROXY_USERNAME = "proxyUsername";
	public final static String PROXY_PASSWORD = "proxyPassword";
	public final static String ConnectTimeout = "connectTimeout";
	public final static String ReadTimeout = "readTimeout";
	public final static String WriteTimeout = "writeTimeout";
	public final static String MaxRetry = "maxRetry";
	public final static String AcceptGzipEncoding = "acceptGzipEncoding";
	public final static String ResponseTypeFormat = "responseTypeFormat";
	public final static String IgnoreSslDomainVerification = "ignoreSslDomainVerification";

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
		params.put(IgnoreSslDomainVerification, config.getIgnoreSslDomainVerification());
		params.put(Endpoint, config.getEndpoint());
		params.put(PROXY_TYPE, Proxy.Type.HTTP);
	}

	private void initDefaults() {
		params = new HashMap<String, Object>();
		params.put(ConnectTimeout, 20000);
		params.put(ReadTimeout, 20000);
		params.put(WriteTimeout, 30000);
		params.put(MaxRetry, 3);
		params.put(AcceptGzipEncoding, false);
		params.put(IgnoreSslDomainVerification, false);
		params.put(Endpoint, "http://www.kaltura.com/");
		params.put(PROXY_TYPE, Proxy.Type.HTTP);
	}


	public String getEndpoint() {
		return (String) params.get(Endpoint);
	}

	public String getProxy() {
		return (String) params.get(PROXY);
	}
	public int getProxyPort() {
		try {
			return Integer.parseInt(params.get(PROXY_PORT).toString());	
		} catch (ClassCastException e) {
			return 0;
		}
		
	}

	public Proxy.Type getProxyType(){
		return (Proxy.Type) params.get(PROXY_TYPE);
	}
	public String getProxyUsername(){
		return (String) params.get(PROXY_USERNAME);
	}
	public String getProxyPassword(){
		return (String) params.get(PROXY_PASSWORD);
	}


	@Override
	public boolean getIgnoreSslDomainVerification() {
		return (boolean) params.get(IgnoreSslDomainVerification);
	}

	public void setEndpoint(String endpoint) {
		this.params.put(endpoint, endpoint);
	}

	public void setProxy(String proxy) {
		params.put(PROXY, proxy);
		System.setProperty("http_proxy",proxy);
	}

	public void setProxyPort(int proxyPort) {
		params.put(PROXY_PORT, String.valueOf(proxyPort));
		System.setProperty("http_proxy_port",String.valueOf(proxyPort));
	}

	public void setProxyType(String proxyType) {
		
		if("sock".equalsIgnoreCase(proxyType) || "https".equalsIgnoreCase(proxyType)){
			params.put(PROXY_TYPE, Proxy.Type.SOCKS);
			System.setProperty("http_proxy_type",Proxy.Type.SOCKS.toString());
		}else if("http".equalsIgnoreCase(proxyType)){
			params.put(PROXY_TYPE, Proxy.Type.HTTP);
			System.setProperty("http_proxy_type",Proxy.Type.HTTP.toString());
		}
		
	}
	
	public void setProxyUsername(String proxyUsername) {
		params.put(PROXY_USERNAME, proxyUsername);
		System.setProperty("http_proxy_username",String.valueOf(proxyUsername));
	}

	public void setProxyPassword(String proxyPassword) {
		params.put(PROXY_PASSWORD, proxyPassword);
		System.setProperty("http_proxy_password",String.valueOf(proxyPassword));
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

	public void setIgnoreSslDomainVerification(boolean ignore) {
		params.put(IgnoreSslDomainVerification, ignore);
	}

	public void setMaxRetry(int retry) {
		params.put(MaxRetry, retry);
	}

	public int getMaxRetry(int defaultVal) {
		return params.containsKey(MaxRetry) ? (int) params.get(MaxRetry) : defaultVal;
	}
}
