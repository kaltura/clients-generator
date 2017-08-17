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

import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class OttTestConfig {
	private static Properties properties;
	
	private static final String PARTNER_ID = "partnerId";
	private static final String ENDPOINT = "serviceUrl";
	private static final String USER_NAME = "userName";
	private static final String USER_PASSWORD = "userPassword";
	private static final String MEDIA_ID = "mediaId";

	OttTestConfig() throws IOException{
		if(properties == null){
			InputStream inputStream = getClass().getClassLoader().getResourceAsStream("ott.test.properties");
			properties = new Properties();
			properties.load(inputStream);
		}
	}
	
	int getPartnerId(){
		return Integer.parseInt(properties.getProperty(PARTNER_ID));
	}
	
	String getServiceUrl(){
		return properties.getProperty(ENDPOINT);
	}
	
	String getUserName(){
		return properties.getProperty(USER_NAME);
	}

	String getUserPassword(){
		return properties.getProperty(USER_PASSWORD);
	}

	String getMediaId() {
		return properties.getProperty(MEDIA_ID);
	}
}
