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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class Configurations : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string PARTNER_ID = "partnerId";
		public const string CONFIGURATION_GROUP_ID = "configurationGroupId";
		public const string APP_NAME = "appName";
		public const string CLIENT_VERSION = "clientVersion";
		public const string PLATFORM = "platform";
		public const string EXTERNAL_PUSH_ID = "externalPushId";
		public const string IS_FORCE_UPDATE = "isForceUpdate";
		public const string CONTENT = "content";
		#endregion

		#region Private Fields
		private string _Id = null;
		private int _PartnerId = Int32.MinValue;
		private string _ConfigurationGroupId = null;
		private string _AppName = null;
		private string _ClientVersion = null;
		private Platform _Platform = null;
		private string _ExternalPushId = null;
		private bool? _IsForceUpdate = null;
		private string _Content = null;
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
		public string ConfigurationGroupId
		{
			get { return _ConfigurationGroupId; }
			set 
			{ 
				_ConfigurationGroupId = value;
				OnPropertyChanged("ConfigurationGroupId");
			}
		}
		[JsonProperty]
		public string AppName
		{
			get { return _AppName; }
			set 
			{ 
				_AppName = value;
				OnPropertyChanged("AppName");
			}
		}
		[JsonProperty]
		public string ClientVersion
		{
			get { return _ClientVersion; }
			set 
			{ 
				_ClientVersion = value;
				OnPropertyChanged("ClientVersion");
			}
		}
		[JsonProperty]
		public Platform Platform
		{
			get { return _Platform; }
			set 
			{ 
				_Platform = value;
				OnPropertyChanged("Platform");
			}
		}
		[JsonProperty]
		public string ExternalPushId
		{
			get { return _ExternalPushId; }
			set 
			{ 
				_ExternalPushId = value;
				OnPropertyChanged("ExternalPushId");
			}
		}
		[JsonProperty]
		public bool? IsForceUpdate
		{
			get { return _IsForceUpdate; }
			set 
			{ 
				_IsForceUpdate = value;
				OnPropertyChanged("IsForceUpdate");
			}
		}
		[JsonProperty]
		public string Content
		{
			get { return _Content; }
			set 
			{ 
				_Content = value;
				OnPropertyChanged("Content");
			}
		}
		#endregion

		#region CTor
		public Configurations()
		{
		}

		public Configurations(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["partnerId"] != null)
			{
				this._PartnerId = ParseInt(node["partnerId"].Value<string>());
			}
			if(node["configurationGroupId"] != null)
			{
				this._ConfigurationGroupId = node["configurationGroupId"].Value<string>();
			}
			if(node["appName"] != null)
			{
				this._AppName = node["appName"].Value<string>();
			}
			if(node["clientVersion"] != null)
			{
				this._ClientVersion = node["clientVersion"].Value<string>();
			}
			if(node["platform"] != null)
			{
				this._Platform = (Platform)StringEnum.Parse(typeof(Platform), node["platform"].Value<string>());
			}
			if(node["externalPushId"] != null)
			{
				this._ExternalPushId = node["externalPushId"].Value<string>();
			}
			if(node["isForceUpdate"] != null)
			{
				this._IsForceUpdate = ParseBool(node["isForceUpdate"].Value<string>());
			}
			if(node["content"] != null)
			{
				this._Content = node["content"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaConfigurations");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("partnerId", this._PartnerId);
			kparams.AddIfNotNull("configurationGroupId", this._ConfigurationGroupId);
			kparams.AddIfNotNull("appName", this._AppName);
			kparams.AddIfNotNull("clientVersion", this._ClientVersion);
			kparams.AddIfNotNull("platform", this._Platform);
			kparams.AddIfNotNull("externalPushId", this._ExternalPushId);
			kparams.AddIfNotNull("isForceUpdate", this._IsForceUpdate);
			kparams.AddIfNotNull("content", this._Content);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case PARTNER_ID:
					return "PartnerId";
				case CONFIGURATION_GROUP_ID:
					return "ConfigurationGroupId";
				case APP_NAME:
					return "AppName";
				case CLIENT_VERSION:
					return "ClientVersion";
				case PLATFORM:
					return "Platform";
				case EXTERNAL_PUSH_ID:
					return "ExternalPushId";
				case IS_FORCE_UPDATE:
					return "IsForceUpdate";
				case CONTENT:
					return "Content";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

