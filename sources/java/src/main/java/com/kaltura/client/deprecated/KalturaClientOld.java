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
// Copyright (C) 2006-2016  Kaltura Inc.
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
import com.kaltura.client.services.KalturaAnnouncementService;
import com.kaltura.client.services.KalturaAppTokenService;
import com.kaltura.client.services.KalturaHouseholdUserService;
import com.kaltura.client.services.KalturaOttUserService;

/**
 * This class was generated using exec.php
 * against an XML schema provided by Kaltura.
 * 
 * MANUAL CHANGES TO THIS CLASS WILL BE OVERWRITTEN.
 */

@SuppressWarnings("serial")
public class KalturaClientOld extends KalturaClientBase {
	
	public KalturaClientOld(ConnectionConfiguration config) {
		super(config);
		
		this.setClientTag("java:16-08-18");
		this.setApiVersion("3.6.287.27685");
	}
	
	protected KalturaAnnouncementService announcementService;
	public KalturaAnnouncementService getAnnouncementService() {
		if(this.announcementService == null)
			this.announcementService = new KalturaAnnouncementService(this);
	
		return this.announcementService;
	}
	
	protected KalturaAppTokenService appTokenService;
	public KalturaAppTokenService getAppTokenService() {
		if(this.appTokenService == null)
			this.appTokenService = new KalturaAppTokenService(this);
	
		return this.appTokenService;
	}
	
	protected KalturaHouseholdUserService householdUserService;
	public KalturaHouseholdUserService getHouseholdUserService() {
		if(this.householdUserService == null)
			this.householdUserService = new KalturaHouseholdUserService(this);
	
		return this.householdUserService;
	}
	
	protected KalturaOttUserService ottUserService;
	public KalturaOttUserService getOttUserService() {
		if(this.ottUserService == null)
			this.ottUserService = new KalturaOttUserService(this);
	
		return this.ottUserService;
	}
	
	/**
	 * @param String $clientTag
	 */
	public void setClientTag(String clientTag){
		this.clientConfiguration.put("clientTag", clientTag);
	}
	
	/**
	 * @return String
	 */
	public String getClientTag(){
		if(this.clientConfiguration.containsKey("clientTag")){
			return (String) this.clientConfiguration.get("clientTag");
		}
		
		return null;
	}
	
	/**
	 * @param String $apiVersion
	 */
	public void setApiVersion(String apiVersion){
		this.clientConfiguration.put("apiVersion", apiVersion);
	}
	
	/**
	 * @return String
	 */
	public String getApiVersion(){
		if(this.clientConfiguration.containsKey("apiVersion")){
			return (String) this.clientConfiguration.get("apiVersion");
		}
		
		return null;
	}
	
	/**
	 * Impersonated partner id
	 * 
	 * @param Integer $partnerId
	 */
	public void setPartnerId(Integer partnerId){
		this.requestConfiguration.put("partnerId", partnerId);
	}
	
	/**
	 * Impersonated partner id
	 * 
	 * @return Integer
	 */
	public Integer getPartnerId(){
		if(this.requestConfiguration.containsKey("partnerId")){
			return (Integer) this.requestConfiguration.get("partnerId");
		}
		
		return null;
	}
	
	/**
	 * Kaltura API session
	 * 
	 * @param String $ks
	 */
	public void setKs(String ks){
		this.requestConfiguration.put("ks", ks);
	}
	
	/**
	 * Kaltura API session
	 * 
	 * @return String
	 */
	public String getKs(){
		if(this.requestConfiguration.containsKey("ks")){
			return (String) this.requestConfiguration.get("ks");
		}
		
		return null;
	}
	
	/**
	 * Kaltura API session
	 * 
	 * @param String $sessionId
	 */
	public void setSessionId(String sessionId){
		this.requestConfiguration.put("ks", sessionId);
	}
	
	/**
	 * Kaltura API session
	 * 
	 * @return String
	 */
	public String getSessionId(){
		if(this.requestConfiguration.containsKey("ks")){
			return (String) this.requestConfiguration.get("ks");
		}
		
		return null;
	}
	
	/**
	 * Clear all volatile configuration parameters
	 */
	protected void resetRequest(){
	}
}
