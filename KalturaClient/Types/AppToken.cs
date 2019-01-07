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
// Copyright (C) 2006-2019  Kaltura Inc.
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
	public class AppToken : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string EXPIRY = "expiry";
		public const string PARTNER_ID = "partnerId";
		public const string SESSION_DURATION = "sessionDuration";
		public const string HASH_TYPE = "hashType";
		public const string SESSION_PRIVILEGES = "sessionPrivileges";
		public const string TOKEN = "token";
		public const string SESSION_USER_ID = "sessionUserId";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		#endregion

		#region Private Fields
		private string _Id = null;
		private int _Expiry = Int32.MinValue;
		private int _PartnerId = Int32.MinValue;
		private int _SessionDuration = Int32.MinValue;
		private AppTokenHashType _HashType = null;
		private string _SessionPrivileges = null;
		private string _Token = null;
		private string _SessionUserId = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public string Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public int Expiry
		{
			get { return _Expiry; }
			set 
			{ 
				_Expiry = value;
				OnPropertyChanged("Expiry");
			}
		}
		[JsonProperty]
		public int PartnerId
		{
			get { return _PartnerId; }
			private set 
			{ 
				_PartnerId = value;
				OnPropertyChanged("PartnerId");
			}
		}
		[JsonProperty]
		public int SessionDuration
		{
			get { return _SessionDuration; }
			set 
			{ 
				_SessionDuration = value;
				OnPropertyChanged("SessionDuration");
			}
		}
		[JsonProperty]
		public AppTokenHashType HashType
		{
			get { return _HashType; }
			set 
			{ 
				_HashType = value;
				OnPropertyChanged("HashType");
			}
		}
		[JsonProperty]
		public string SessionPrivileges
		{
			get { return _SessionPrivileges; }
			set 
			{ 
				_SessionPrivileges = value;
				OnPropertyChanged("SessionPrivileges");
			}
		}
		[JsonProperty]
		public string Token
		{
			get { return _Token; }
			private set 
			{ 
				_Token = value;
				OnPropertyChanged("Token");
			}
		}
		[JsonProperty]
		public string SessionUserId
		{
			get { return _SessionUserId; }
			set 
			{ 
				_SessionUserId = value;
				OnPropertyChanged("SessionUserId");
			}
		}
		[JsonProperty]
		public long CreateDate
		{
			get { return _CreateDate; }
			private set 
			{ 
				_CreateDate = value;
				OnPropertyChanged("CreateDate");
			}
		}
		[JsonProperty]
		public long UpdateDate
		{
			get { return _UpdateDate; }
			private set 
			{ 
				_UpdateDate = value;
				OnPropertyChanged("UpdateDate");
			}
		}
		#endregion

		#region CTor
		public AppToken()
		{
		}

		public AppToken(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["expiry"] != null)
			{
				this._Expiry = ParseInt(node["expiry"].Value<string>());
			}
			if(node["partnerId"] != null)
			{
				this._PartnerId = ParseInt(node["partnerId"].Value<string>());
			}
			if(node["sessionDuration"] != null)
			{
				this._SessionDuration = ParseInt(node["sessionDuration"].Value<string>());
			}
			if(node["hashType"] != null)
			{
				this._HashType = (AppTokenHashType)StringEnum.Parse(typeof(AppTokenHashType), node["hashType"].Value<string>());
			}
			if(node["sessionPrivileges"] != null)
			{
				this._SessionPrivileges = node["sessionPrivileges"].Value<string>();
			}
			if(node["token"] != null)
			{
				this._Token = node["token"].Value<string>();
			}
			if(node["sessionUserId"] != null)
			{
				this._SessionUserId = node["sessionUserId"].Value<string>();
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAppToken");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("expiry", this._Expiry);
			kparams.AddIfNotNull("partnerId", this._PartnerId);
			kparams.AddIfNotNull("sessionDuration", this._SessionDuration);
			kparams.AddIfNotNull("hashType", this._HashType);
			kparams.AddIfNotNull("sessionPrivileges", this._SessionPrivileges);
			kparams.AddIfNotNull("token", this._Token);
			kparams.AddIfNotNull("sessionUserId", this._SessionUserId);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case EXPIRY:
					return "Expiry";
				case PARTNER_ID:
					return "PartnerId";
				case SESSION_DURATION:
					return "SessionDuration";
				case HASH_TYPE:
					return "HashType";
				case SESSION_PRIVILEGES:
					return "SessionPrivileges";
				case TOKEN:
					return "Token";
				case SESSION_USER_ID:
					return "SessionUserId";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

