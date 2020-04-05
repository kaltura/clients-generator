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
// Copyright (C) 2006-2020  Kaltura Inc.
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
using System;
using System.Xml;
using System.Collections.Generic;
using Kaltura.Enums;
using Kaltura.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class Iot : CrudObject
	{
		#region Constants
		public const string UDID = "udid";
		public const string ACCESS_KEY = "accessKey";
		public const string ACCESS_SECRET_KEY = "accessSecretKey";
		public const string USERNAME = "username";
		public const string USER_PASSWORD = "userPassword";
		public const string IDENTITY_ID = "identityId";
		public const string THING_ARN = "thingArn";
		public const string THING_ID = "thingId";
		public const string PRINCIPAL = "principal";
		public const string END_POINT = "endPoint";
		public const string EXTENDED_END_POINT = "extendedEndPoint";
		public const string IDENTITY_POOL_ID = "identityPoolId";
		#endregion

		#region Private Fields
		private string _Udid = null;
		private string _AccessKey = null;
		private string _AccessSecretKey = null;
		private string _Username = null;
		private string _UserPassword = null;
		private string _IdentityId = null;
		private string _ThingArn = null;
		private string _ThingId = null;
		private string _Principal = null;
		private string _EndPoint = null;
		private string _ExtendedEndPoint = null;
		private string _IdentityPoolId = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string Udid
		{
			get { return _Udid; }
			set 
			{ 
				_Udid = value;
				OnPropertyChanged("Udid");
			}
		}
		[JsonProperty]
		public string AccessKey
		{
			get { return _AccessKey; }
			set 
			{ 
				_AccessKey = value;
				OnPropertyChanged("AccessKey");
			}
		}
		[JsonProperty]
		public string AccessSecretKey
		{
			get { return _AccessSecretKey; }
			set 
			{ 
				_AccessSecretKey = value;
				OnPropertyChanged("AccessSecretKey");
			}
		}
		[JsonProperty]
		public string Username
		{
			get { return _Username; }
			set 
			{ 
				_Username = value;
				OnPropertyChanged("Username");
			}
		}
		[JsonProperty]
		public string UserPassword
		{
			get { return _UserPassword; }
			set 
			{ 
				_UserPassword = value;
				OnPropertyChanged("UserPassword");
			}
		}
		[JsonProperty]
		public string IdentityId
		{
			get { return _IdentityId; }
			set 
			{ 
				_IdentityId = value;
				OnPropertyChanged("IdentityId");
			}
		}
		[JsonProperty]
		public string ThingArn
		{
			get { return _ThingArn; }
			set 
			{ 
				_ThingArn = value;
				OnPropertyChanged("ThingArn");
			}
		}
		[JsonProperty]
		public string ThingId
		{
			get { return _ThingId; }
			set 
			{ 
				_ThingId = value;
				OnPropertyChanged("ThingId");
			}
		}
		[JsonProperty]
		public string Principal
		{
			get { return _Principal; }
			set 
			{ 
				_Principal = value;
				OnPropertyChanged("Principal");
			}
		}
		[JsonProperty]
		public string EndPoint
		{
			get { return _EndPoint; }
			set 
			{ 
				_EndPoint = value;
				OnPropertyChanged("EndPoint");
			}
		}
		[JsonProperty]
		public string ExtendedEndPoint
		{
			get { return _ExtendedEndPoint; }
			set 
			{ 
				_ExtendedEndPoint = value;
				OnPropertyChanged("ExtendedEndPoint");
			}
		}
		[JsonProperty]
		public string IdentityPoolId
		{
			get { return _IdentityPoolId; }
			set 
			{ 
				_IdentityPoolId = value;
				OnPropertyChanged("IdentityPoolId");
			}
		}
		#endregion

		#region CTor
		public Iot()
		{
		}

		public Iot(JToken node) : base(node)
		{
			if(node["udid"] != null)
			{
				this._Udid = node["udid"].Value<string>();
			}
			if(node["accessKey"] != null)
			{
				this._AccessKey = node["accessKey"].Value<string>();
			}
			if(node["accessSecretKey"] != null)
			{
				this._AccessSecretKey = node["accessSecretKey"].Value<string>();
			}
			if(node["username"] != null)
			{
				this._Username = node["username"].Value<string>();
			}
			if(node["userPassword"] != null)
			{
				this._UserPassword = node["userPassword"].Value<string>();
			}
			if(node["identityId"] != null)
			{
				this._IdentityId = node["identityId"].Value<string>();
			}
			if(node["thingArn"] != null)
			{
				this._ThingArn = node["thingArn"].Value<string>();
			}
			if(node["thingId"] != null)
			{
				this._ThingId = node["thingId"].Value<string>();
			}
			if(node["principal"] != null)
			{
				this._Principal = node["principal"].Value<string>();
			}
			if(node["endPoint"] != null)
			{
				this._EndPoint = node["endPoint"].Value<string>();
			}
			if(node["extendedEndPoint"] != null)
			{
				this._ExtendedEndPoint = node["extendedEndPoint"].Value<string>();
			}
			if(node["identityPoolId"] != null)
			{
				this._IdentityPoolId = node["identityPoolId"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaIot");
			kparams.AddIfNotNull("udid", this._Udid);
			kparams.AddIfNotNull("accessKey", this._AccessKey);
			kparams.AddIfNotNull("accessSecretKey", this._AccessSecretKey);
			kparams.AddIfNotNull("username", this._Username);
			kparams.AddIfNotNull("userPassword", this._UserPassword);
			kparams.AddIfNotNull("identityId", this._IdentityId);
			kparams.AddIfNotNull("thingArn", this._ThingArn);
			kparams.AddIfNotNull("thingId", this._ThingId);
			kparams.AddIfNotNull("principal", this._Principal);
			kparams.AddIfNotNull("endPoint", this._EndPoint);
			kparams.AddIfNotNull("extendedEndPoint", this._ExtendedEndPoint);
			kparams.AddIfNotNull("identityPoolId", this._IdentityPoolId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case UDID:
					return "Udid";
				case ACCESS_KEY:
					return "AccessKey";
				case ACCESS_SECRET_KEY:
					return "AccessSecretKey";
				case USERNAME:
					return "Username";
				case USER_PASSWORD:
					return "UserPassword";
				case IDENTITY_ID:
					return "IdentityId";
				case THING_ARN:
					return "ThingArn";
				case THING_ID:
					return "ThingId";
				case PRINCIPAL:
					return "Principal";
				case END_POINT:
					return "EndPoint";
				case EXTENDED_END_POINT:
					return "ExtendedEndPoint";
				case IDENTITY_POOL_ID:
					return "IdentityPoolId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

