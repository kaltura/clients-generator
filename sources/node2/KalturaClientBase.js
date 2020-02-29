/*jshint quotmark:true*/
// ===================================================================================================
//													 _	__		 _ _
//													| |/ /__ _| | |_ _	_ _ _ __ _
//													| ' </ _` | |	_| || | '_/ _` |
//													|_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platfroms allow them to do with
// text.
//
// Copyright (C) 2006-2011	Kaltura Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,re
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.	See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.	If not, see <http://www.gnu.org/licenses/>.
//
// @ignore
// ===================================================================================================

const md5 = require('md5');
const fs = require('fs');
const path = require('path');
const http = require('http');
const https = require('https');
const request = require('request');

const kaltura = require('./KalturaRequestData');


function cloneObject(src) {
	return JSON.parse(JSON.stringify(src));
}

function copyObject(src, dest) {
	for (let key in src) {
		switch (typeof (src[key])) {
			case 'function':
				break;

			case 'object':
				dest[key] = cloneObject(src[key]);
				break;

			default:
				dest[key] = src[key];
				break;
		}
	}
}

/**
 * Sorts an array by key, maintaining key to data correlations. This is useful
 * mainly for associative arrays.
 * 
 * @param arr
 *            The array to sort.
 * @return The sorted array.
 */
function ksort(arr) {
	let sArr = [];
	let tArr = [];
	let n = 0;
	for (i in arr)
		tArr[n++] = i + ' |' + arr[i];
	tArr = tArr.sort();
	for (let i = 0; i < tArr.length; i++) {
		let x = tArr[i].split(' |');
		sArr[x[0]] = x[1];
	}
	return sArr;
}

/**
 * Implement to get Kaltura Client logs
 * 
 */
class ILogger {
	log(msg) {
		if (console && console.log) {
			console.log(msg);
		}
	}

	error(msg) {
		if (console && console.error) {
			console.error(msg);
		}
	}

	debug(msg) {
		this.log(msg);
	}
}

/**
 * Kaltura configuration object
 */
class Configuration {

	constructor() {
		this.logger = new ILogger();
		this.serviceUrl = 'http://www.kaltura.com';
		this.serviceBase = '/api_v3/service';
		this.timeout = 30000;
		this.agentOptions = null;
	}

	/**
	 * Set logger to get kaltura client debug logs.
	 * 
	 * @param ILogger log
	 */
	setLogger(log) {
		this.logger = log;
	}

	/**
	 * Gets the logger (Internal client use)
	 * 
	 * @return ILogger
	 */
	getLogger() {
		return this.logger;
	}
}

/**
 * Kaltura client constructor
 * 
 */
class ClientBase extends kaltura.RequestData {

	/**
	 * @param Configuration config
	 */
	constructor(config) {
		super();
		this.shouldLog = false;
		this.setConfig(config);
	}

	/**
	 * getter for the referenced configuration object.
	 * 
	 * @return Configuration
	 */
	getConfig() {
		return this.config;
	}

	/**
	 * @param Configuration config setter for the referenced configuration object.
	 */
	setConfig(config) {
		this.config = config;
		if (config.getLogger() instanceof ILogger) {
			this.shouldLog = true;
		}
		const options = {
			timeout: config.timeout,
			uri: config.serviceUrl,
		};
		if (config.agentOptions) {
			const httpInterface = options.uri.startsWith('https') ? https : http;
			options.agent = new httpInterface.Agent(config.agentOptions);
		}
		if (config.proxy) {
			options.proxy = config.proxy;
		}
		this.request = request.defaults(options);
	}

	/**
	 * return a new multi-request builder
	 */
	startMultiRequest() {
		return new MultiRequestBuilder();
	}

	/**
	 * @param string msg client logging utility.
	 */
	log(msg) {
		if (this.shouldLog)
			this.config.getLogger().log(msg);
	}

	/**
	 * @param string msg client logging utility.
	 */
	debug(msg) {
		if (this.shouldLog)
			this.config.getLogger().debug(msg);
	}
}

ClientBase.FORMAT_JSON = 1;
ClientBase.FORMAT_XML = 2;
ClientBase.FORMAT_PHP = 3;
ClientBase.FORMAT_JSONP = 9;


class RequestBuilder extends kaltura.VolatileRequestData {

	constructor(service = null, action = null, data = null, files = null) {
		super();

		if (service) {
			this.service = service;
			this.action = action;
			this.data = data;
			this.files = files;
		}

		this.callback = null;
	}

	/**
	 * Sign array of parameters for requests validation (CRC).
	 * 
	 * @param array
	 *            params service action call parameters that will be sent on the
	 *            request.
	 * @return string a hashed signed signature that can identify the sent request
	 *         parameters.
	 */
	signature(params) {
		params = ksort(params);
		let str = '';
		for (let v in params) {
			let k = params[v];
			if (typeof (k) === 'object' || Array.isArray(k))
				k = this.signature(k);

			str += v + k;
		}
		return md5(str);
	}

	/**
	 * send the http request.
	 * 
	 * @return array the results and errors inside an array.
	 */
	doHttpRequest(client) {
		let json = this.getData(true);
		let files = this.getFiles();
		let callback = this.callback;
		let requestUrl = this.getUrl(client);

		let options = {
			uri: requestUrl,
		};
		options.method = 'POST';
		options.headers = {
			'Accept': 'application/json',
			'Content-Type': 'application/json',
		};

		let jsonBody = JSON.stringify(json, (key, value) => (value === null ? undefined : value));
		client.log('URL: ' + requestUrl + ', JSON: ' + jsonBody);

		let body;
		if (files && Object.keys(files).length > 0) {
			let crlf = '\r\n';
			let boundary = '---------------------------' + Math.random();
			let delimiter = crlf + '--' + boundary;
			let postData = [];

			postData.push();
			postData.push(new Buffer(delimiter + crlf + 'Content-Disposition: form-data; name="json"' + crlf + crlf));
			postData.push(new Buffer(jsonBody));

			for (let key in files) {
				if (typeof (files[key]) === 'function') {
					continue;
				}

				let fileName;
				let data;
				if (files[key] instanceof Buffer) {
					fileName = 'chunk';
					data = files[key];
				} else {
					let filePath = files[key];
					fileName = path.basename(filePath);
					data = fs.readFileSync(filePath);
				}

				let headers = ['Content-Disposition: form-data; name="' + key + '"; filename="' + fileName + '"' + crlf, 'Content-Type: application/octet-stream' + crlf];

				postData.push(new Buffer(delimiter + crlf + headers.join('') + crlf));
				postData.push(new Buffer(data));
			}
			postData.push(new Buffer(delimiter + '--'));
			body = Buffer.concat(postData);

			options.headers['Content-Type'] = 'multipart/form-data; boundary=' + boundary;
		}
		else {
			body = jsonBody;
		}

		options.body = body;

		client.request(options, (err, response, data) => {
			if (err) {
				if (callback) {
					callback(false, err);
					return;
				}
				else {
					throw new Error(json.message);
				}
			}
			let sessionId;
			let serverId;
			for (var header in response.headers) {
				if (header === 'x-me') {
					serverId = response.headers[header];
				}
				else if (header === 'x-kaltura-session') {
					sessionId = response.headers[header];
				}
			}
			client.debug('Response server [' + serverId + '] session [' + sessionId + ']: ' + data);

			let json;
			try {
				json = JSON.parse(data);
			} catch (err) {
				json = {
					error: err
				}
			}
			if (json && typeof (json) === 'object' && json.code && json.message) {
				if (callback) {
					callback(false, json);
				}
				else {
					throw new Error(json.message);
				}
			}
			else if (json && typeof (json) === 'object' && json.result && json.result.error && json.result.error.objectType == 'KalturaAPIException') {
				if (callback) {
					callback(false, json);
				}
				else {
					throw new Error(json.message);
				}
			}
			else if (callback) {
				callback(true, json);
			}
		});
	}

	sign() {
		let signature = this.signature(this.data);
		this.data.kalsig = signature;
	}

	getUrl(client) {
		let requestUrl = client.config.serviceUrl + client.config.serviceBase;
		requestUrl += '/' + this.service + '/action/' + this.action;

		return requestUrl;
	}

	getFiles() {
		return this.files;
	}

	getData(sign) {
		this.data.format = ClientBase.FORMAT_JSON;
		copyObject(this.requestData, this.data);

		if (sign) {
			this.sign();
		}

		return this.data;
	}

	execute(client, callback) {
		copyObject(client.requestData, this.requestData);

		if (callback) {
			this.completion(callback);
		}

		if (this.callback === null) {
			let This = this;
			return new Promise((resolve, reject) => {
				This.completion((success, result) => {
					if (success) {
						resolve(result);
					}
					else {
						reject(result);
					}
				});
				This.doHttpRequest(client);
			});
		}
		else {
			this.doHttpRequest(client);
			return this;
		}
	}

	completion(callback) {
		this.callback = callback;
		return this;
	}

	add(requestBuilder) {
		let multiRequestBuilder = new MultiRequestBuilder();
		multiRequestBuilder.add(this);
		multiRequestBuilder.add(requestBuilder);
		return multiRequestBuilder;
	}
}

class MultiRequestBuilder extends RequestBuilder {

	constructor(service, action, data, files) {
		super();

		this.requests = [];
		this.generalCallback = null;

		let This = this;
		this.callback = function (success, results) {
			if (!success)
				throw new Error(results);

			for (let i = 0; i < This.requests.length; i++) {
				if (This.requests[i].callback) {
					if (results[i] && typeof (results[i]) == 'object'
						&& results[i].code && results[i].message)
						This.requests[i].callback(false, results[i]);
					else
						This.requests[i].callback(true, results[i]);
				}
			}

			if (This.generalCallback) {
				if (results && typeof (results) == 'object' && results.code
					&& results.message)
					This.generalCallback(false, results)
				else
					This.generalCallback(true, results)
			}
		};
	}

	completion(callback) {
		this.generalCallback = callback;

		return this;
	}

	add(requestBuilder) {
		this.requests.push(requestBuilder);
		return this;
	}

	getUrl(client) {
		let requestUrl = client.config.serviceUrl + client.config.serviceBase;
		requestUrl += '/multirequest';

		return requestUrl;
	}

	getData() {
		this.data = {
			format: ClientBase.FORMAT_JSON
		}

		for (let i = 0; i < this.requests.length; i++) {
			this.data[i] = this.requests[i].getData();
			this.data[i].service = this.requests[i].service;
			this.data[i].action = this.requests[i].action;
		}

		copyObject(this.requestData, this.data);

		this.sign();
		return this.data;
	}

	getFiles() {
		this.files = {};

		for (let i = 0; i < this.requests.length; i++) {
			let requestFiles = this.requests[i].getFiles();
			if (requestFiles) {
				for (let key in requestFiles) {
					if (typeof (requestFiles[key]) !== 'function') {
						this.files[i + ':' + key] = requestFiles[key];
					}
				}
			}
		}

		return this.files;
	}

	execute(client, callback) {
		if (callback) {
			this.completion(callback);
		}

		if (this.generalCallback === null) {
			let This = this;
			return new Promise((resolve, reject) => {
				This.completion((success, result) => {
					if (success) {
						resolve(result);
					}
					else {
						reject(result);
					}
				});
				return super.execute(client);
			});
		}
		else {
			return super.execute(client, callback);
		}
	}
}

class BaseObject {

	constructor(object) {
		if (object) {
			copyObject(object, this);
		}
	}
}

module.exports = {
	ILogger: ILogger,
	Configuration: Configuration,
	ClientBase: ClientBase,
	RequestBuilder: RequestBuilder,
	MultiRequestBuilder: MultiRequestBuilder,
	BaseObject: BaseObject
};
