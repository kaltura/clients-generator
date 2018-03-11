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
	public class DeviceReport : Report
	{
		#region Constants
		public const string PARTNER_ID = "partnerId";
		public const string CONFIGURATION_GROUP_ID = "configurationGroupId";
		public const string UDID = "udid";
		public const string PUSH_PARAMETERS = "pushParameters";
		public const string VERSION_NUMBER = "versionNumber";
		public const string VERSION_PLATFORM = "versionPlatform";
		public const string VERSION_APP_NAME = "versionAppName";
		public const string LAST_ACCESS_IP = "lastAccessIP";
		public const string LAST_ACCESS_DATE = "lastAccessDate";
		public const string USER_AGENT = "userAgent";
		public const string OPERATION_SYSTEM = "operationSystem";
		#endregion

		#region Private Fields
		private int _PartnerId = Int32.MinValue;
		private string _ConfigurationGroupId = null;
		private string _Udid = null;
		private PushParams _PushParameters;
		private string _VersionNumber = null;
		private Platform _VersionPlatform = null;
		private string _VersionAppName = null;
		private string _LastAccessIP = null;
		private long _LastAccessDate = long.MinValue;
		private string _UserAgent = null;
		private string _OperationSystem = null;
		#endregion

		#region Properties
		public int PartnerId
		{
			get { return _PartnerId; }
			set 
			{ 
				_PartnerId = value;
				OnPropertyChanged("PartnerId");
			}
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
		public string Udid
		{
			get { return _Udid; }
			set 
			{ 
				_Udid = value;
				OnPropertyChanged("Udid");
			}
		}
		public PushParams PushParameters
		{
			get { return _PushParameters; }
			set 
			{ 
				_PushParameters = value;
				OnPropertyChanged("PushParameters");
			}
		}
		public string VersionNumber
		{
			get { return _VersionNumber; }
			set 
			{ 
				_VersionNumber = value;
				OnPropertyChanged("VersionNumber");
			}
		}
		public Platform VersionPlatform
		{
			get { return _VersionPlatform; }
			set 
			{ 
				_VersionPlatform = value;
				OnPropertyChanged("VersionPlatform");
			}
		}
		public string VersionAppName
		{
			get { return _VersionAppName; }
			set 
			{ 
				_VersionAppName = value;
				OnPropertyChanged("VersionAppName");
			}
		}
		public string LastAccessIP
		{
			get { return _LastAccessIP; }
			set 
			{ 
				_LastAccessIP = value;
				OnPropertyChanged("LastAccessIP");
			}
		}
		public long LastAccessDate
		{
			get { return _LastAccessDate; }
			set 
			{ 
				_LastAccessDate = value;
				OnPropertyChanged("LastAccessDate");
			}
		}
		public string UserAgent
		{
			get { return _UserAgent; }
			set 
			{ 
				_UserAgent = value;
				OnPropertyChanged("UserAgent");
			}
		}
		public string OperationSystem
		{
			get { return _OperationSystem; }
			set 
			{ 
				_OperationSystem = value;
				OnPropertyChanged("OperationSystem");
			}
		}
		#endregion

		#region CTor
		public DeviceReport()
		{
		}

		public DeviceReport(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "partnerId":
						this._PartnerId = ParseInt(propertyNode.InnerText);
						continue;
					case "configurationGroupId":
						this._ConfigurationGroupId = propertyNode.InnerText;
						continue;
					case "udid":
						this._Udid = propertyNode.InnerText;
						continue;
					case "pushParameters":
						this._PushParameters = ObjectFactory.Create<PushParams>(propertyNode);
						continue;
					case "versionNumber":
						this._VersionNumber = propertyNode.InnerText;
						continue;
					case "versionPlatform":
						this._VersionPlatform = (Platform)StringEnum.Parse(typeof(Platform), propertyNode.InnerText);
						continue;
					case "versionAppName":
						this._VersionAppName = propertyNode.InnerText;
						continue;
					case "lastAccessIP":
						this._LastAccessIP = propertyNode.InnerText;
						continue;
					case "lastAccessDate":
						this._LastAccessDate = ParseLong(propertyNode.InnerText);
						continue;
					case "userAgent":
						this._UserAgent = propertyNode.InnerText;
						continue;
					case "operationSystem":
						this._OperationSystem = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaDeviceReport");
			kparams.AddIfNotNull("partnerId", this._PartnerId);
			kparams.AddIfNotNull("configurationGroupId", this._ConfigurationGroupId);
			kparams.AddIfNotNull("udid", this._Udid);
			kparams.AddIfNotNull("pushParameters", this._PushParameters);
			kparams.AddIfNotNull("versionNumber", this._VersionNumber);
			kparams.AddIfNotNull("versionPlatform", this._VersionPlatform);
			kparams.AddIfNotNull("versionAppName", this._VersionAppName);
			kparams.AddIfNotNull("lastAccessIP", this._LastAccessIP);
			kparams.AddIfNotNull("lastAccessDate", this._LastAccessDate);
			kparams.AddIfNotNull("userAgent", this._UserAgent);
			kparams.AddIfNotNull("operationSystem", this._OperationSystem);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PARTNER_ID:
					return "PartnerId";
				case CONFIGURATION_GROUP_ID:
					return "ConfigurationGroupId";
				case UDID:
					return "Udid";
				case PUSH_PARAMETERS:
					return "PushParameters";
				case VERSION_NUMBER:
					return "VersionNumber";
				case VERSION_PLATFORM:
					return "VersionPlatform";
				case VERSION_APP_NAME:
					return "VersionAppName";
				case LAST_ACCESS_IP:
					return "LastAccessIP";
				case LAST_ACCESS_DATE:
					return "LastAccessDate";
				case USER_AGENT:
					return "UserAgent";
				case OPERATION_SYSTEM:
					return "OperationSystem";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

