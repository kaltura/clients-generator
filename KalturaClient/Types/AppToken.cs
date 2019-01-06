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
// Copyright (C) 2006-2018  Kaltura Inc.
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
		public string Id
		{
			get { return _Id; }
		}
		public int Expiry
		{
			get { return _Expiry; }
			set 
			{ 
				_Expiry = value;
				OnPropertyChanged("Expiry");
			}
		}
		public int PartnerId
		{
			get { return _PartnerId; }
		}
		public int SessionDuration
		{
			get { return _SessionDuration; }
			set 
			{ 
				_SessionDuration = value;
				OnPropertyChanged("SessionDuration");
			}
		}
		public AppTokenHashType HashType
		{
			get { return _HashType; }
			set 
			{ 
				_HashType = value;
				OnPropertyChanged("HashType");
			}
		}
		public string SessionPrivileges
		{
			get { return _SessionPrivileges; }
			set 
			{ 
				_SessionPrivileges = value;
				OnPropertyChanged("SessionPrivileges");
			}
		}
		public string Token
		{
			get { return _Token; }
		}
		public string SessionUserId
		{
			get { return _SessionUserId; }
			set 
			{ 
				_SessionUserId = value;
				OnPropertyChanged("SessionUserId");
			}
		}
		public long CreateDate
		{
			get { return _CreateDate; }
		}
		public long UpdateDate
		{
			get { return _UpdateDate; }
		}
		#endregion

		#region CTor
		public AppToken()
		{
		}

		public AppToken(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "expiry":
						this._Expiry = ParseInt(propertyNode.InnerText);
						continue;
					case "partnerId":
						this._PartnerId = ParseInt(propertyNode.InnerText);
						continue;
					case "sessionDuration":
						this._SessionDuration = ParseInt(propertyNode.InnerText);
						continue;
					case "hashType":
						this._HashType = (AppTokenHashType)StringEnum.Parse(typeof(AppTokenHashType), propertyNode.InnerText);
						continue;
					case "sessionPrivileges":
						this._SessionPrivileges = propertyNode.InnerText;
						continue;
					case "token":
						this._Token = propertyNode.InnerText;
						continue;
					case "sessionUserId":
						this._SessionUserId = propertyNode.InnerText;
						continue;
					case "createDate":
						this._CreateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "updateDate":
						this._UpdateDate = ParseLong(propertyNode.InnerText);
						continue;
				}
			}
		}

		public AppToken(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<string>("id");
			    this._Expiry = data.TryGetValueSafe<int>("expiry");
			    this._PartnerId = data.TryGetValueSafe<int>("partnerId");
			    this._SessionDuration = data.TryGetValueSafe<int>("sessionDuration");
			    this._HashType = (AppTokenHashType)StringEnum.Parse(typeof(AppTokenHashType), data.TryGetValueSafe<string>("hashType"));
			    this._SessionPrivileges = data.TryGetValueSafe<string>("sessionPrivileges");
			    this._Token = data.TryGetValueSafe<string>("token");
			    this._SessionUserId = data.TryGetValueSafe<string>("sessionUserId");
			    this._CreateDate = data.TryGetValueSafe<long>("createDate");
			    this._UpdateDate = data.TryGetValueSafe<long>("updateDate");
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

