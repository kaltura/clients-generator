// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
// text.
//
// Copyright (C) 2006-2021  Kaltura Inc.
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
	public class ConfigurationGroupDevice : ObjectBase
	{
		#region Constants
		public const string CONFIGURATION_GROUP_ID = "configurationGroupId";
		public const string PARTNER_ID = "partnerId";
		public const string UDID = "udid";
		#endregion

		#region Private Fields
		private string _ConfigurationGroupId = null;
		private int _PartnerId = Int32.MinValue;
		private string _Udid = null;
		#endregion

		#region Properties
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
		public string Udid
		{
			get { return _Udid; }
			set 
			{ 
				_Udid = value;
				OnPropertyChanged("Udid");
			}
		}
		#endregion

		#region CTor
		public ConfigurationGroupDevice()
		{
		}

		public ConfigurationGroupDevice(JToken node) : base(node)
		{
			if(node["configurationGroupId"] != null)
			{
				this._ConfigurationGroupId = node["configurationGroupId"].Value<string>();
			}
			if(node["partnerId"] != null)
			{
				this._PartnerId = ParseInt(node["partnerId"].Value<string>());
			}
			if(node["udid"] != null)
			{
				this._Udid = node["udid"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaConfigurationGroupDevice");
			kparams.AddIfNotNull("configurationGroupId", this._ConfigurationGroupId);
			kparams.AddIfNotNull("partnerId", this._PartnerId);
			kparams.AddIfNotNull("udid", this._Udid);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CONFIGURATION_GROUP_ID:
					return "ConfigurationGroupId";
				case PARTNER_ID:
					return "PartnerId";
				case UDID:
					return "Udid";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

