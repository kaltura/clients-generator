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
		public string Id
		{
			get { return _Id; }
		}
		public int PartnerId
		{
			get { return _PartnerId; }
		}
		public string ConfigurationGroupId
		{
			get { return _ConfigurationGroupId; }
			set 
			{ 
				_ConfigurationGroupId = value;
				OnPropertyChanged("ConfigurationGroupId");
			}
		}
		public string AppName
		{
			get { return _AppName; }
			set 
			{ 
				_AppName = value;
				OnPropertyChanged("AppName");
			}
		}
		public string ClientVersion
		{
			get { return _ClientVersion; }
			set 
			{ 
				_ClientVersion = value;
				OnPropertyChanged("ClientVersion");
			}
		}
		public Platform Platform
		{
			get { return _Platform; }
			set 
			{ 
				_Platform = value;
				OnPropertyChanged("Platform");
			}
		}
		public string ExternalPushId
		{
			get { return _ExternalPushId; }
			set 
			{ 
				_ExternalPushId = value;
				OnPropertyChanged("ExternalPushId");
			}
		}
		public bool? IsForceUpdate
		{
			get { return _IsForceUpdate; }
			set 
			{ 
				_IsForceUpdate = value;
				OnPropertyChanged("IsForceUpdate");
			}
		}
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

		public Configurations(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "partnerId":
						this._PartnerId = ParseInt(propertyNode.InnerText);
						continue;
					case "configurationGroupId":
						this._ConfigurationGroupId = propertyNode.InnerText;
						continue;
					case "appName":
						this._AppName = propertyNode.InnerText;
						continue;
					case "clientVersion":
						this._ClientVersion = propertyNode.InnerText;
						continue;
					case "platform":
						this._Platform = (Platform)StringEnum.Parse(typeof(Platform), propertyNode.InnerText);
						continue;
					case "externalPushId":
						this._ExternalPushId = propertyNode.InnerText;
						continue;
					case "isForceUpdate":
						this._IsForceUpdate = ParseBool(propertyNode.InnerText);
						continue;
					case "content":
						this._Content = propertyNode.InnerText;
						continue;
				}
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

