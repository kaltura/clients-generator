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
	public class ConfigurationGroupDeviceFilter : Filter
	{
		#region Constants
		public const string CONFIGURATION_GROUP_ID_EQUAL = "configurationGroupIdEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _ConfigurationGroupIdEqual = null;
		private ConfigurationGroupDeviceOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string ConfigurationGroupIdEqual
		{
			get { return _ConfigurationGroupIdEqual; }
			set 
			{ 
				_ConfigurationGroupIdEqual = value;
				OnPropertyChanged("ConfigurationGroupIdEqual");
			}
		}
		[JsonProperty]
		public new ConfigurationGroupDeviceOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		#endregion

		#region CTor
		public ConfigurationGroupDeviceFilter()
		{
		}

		public ConfigurationGroupDeviceFilter(JToken node) : base(node)
		{
			if(node["configurationGroupIdEqual"] != null)
			{
				this._ConfigurationGroupIdEqual = node["configurationGroupIdEqual"].Value<string>();
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (ConfigurationGroupDeviceOrderBy)StringEnum.Parse(typeof(ConfigurationGroupDeviceOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaConfigurationGroupDeviceFilter");
			kparams.AddIfNotNull("configurationGroupIdEqual", this._ConfigurationGroupIdEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CONFIGURATION_GROUP_ID_EQUAL:
					return "ConfigurationGroupIdEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

