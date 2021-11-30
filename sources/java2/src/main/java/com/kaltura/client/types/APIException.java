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

import com.kaltura.client.utils.response.ResponseType;

public class APIException extends Exception implements ResponseType {
	
	private static final long serialVersionUID = 6710104690443289367L;

	public static final String DefaultResponseError = "Failed getting response";

	public enum FailureStep {
		OnRequest("001"),
		OnConfigure("002"),
		OnPass("003"),
		OnResponse("004");

		public String code = "0";

		FailureStep(String code){
			this.code = code;
		}

	};


	private String code = null;
    private String message = null;
    private FailureStep failedOn;

	public APIException() {
		super();
	}

	public APIException(String message) {
		super(message);
		this.message = message;
	}

	public APIException(FailureStep step, String message) {
		super(message);
		this.code = step.code;
		this.failedOn = step;
	}

	public APIException(FailureStep step, String message, String excCode) {
		super(message);
		failedOn = step;
		code = excCode;
	}

	public APIException(Throwable exp) {
		super(exp);
	}

	public APIException(FailureStep step, Throwable exp) {
		super(exp);
		failedOn = step;
		this.code = step.code;
	}

	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

	public String getMessage() {
		return message;
	}

	public void setMessage(String message) {
		this.message = message;
	}

	public FailureStep getFailedOn() {
		return failedOn;
	}

	public void setFailedOn(FailureStep failedOn) {
		this.failedOn = failedOn;
	}
}
