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
	public class RollingDeviceRemovalData : ObjectBase
	{
		#region Constants
		public const string ROLLING_DEVICE_REMOVAL_POLICY = "rollingDeviceRemovalPolicy";
		public const string ROLLING_DEVICE_REMOVAL_FAMILY_IDS = "rollingDeviceRemovalFamilyIds";
		#endregion

		#region Private Fields
		private RollingDevicePolicy _RollingDeviceRemovalPolicy = null;
		private string _RollingDeviceRemovalFamilyIds = null;
		#endregion

		#region Properties
		[JsonProperty]
		public RollingDevicePolicy RollingDeviceRemovalPolicy
		{
			get { return _RollingDeviceRemovalPolicy; }
			set 
			{ 
				_RollingDeviceRemovalPolicy = value;
				OnPropertyChanged("RollingDeviceRemovalPolicy");
			}
		}
		[JsonProperty]
		public string RollingDeviceRemovalFamilyIds
		{
			get { return _RollingDeviceRemovalFamilyIds; }
			set 
			{ 
				_RollingDeviceRemovalFamilyIds = value;
				OnPropertyChanged("RollingDeviceRemovalFamilyIds");
			}
		}
		#endregion

		#region CTor
		public RollingDeviceRemovalData()
		{
		}

		public RollingDeviceRemovalData(JToken node) : base(node)
		{
			if(node["rollingDeviceRemovalPolicy"] != null)
			{
				this._RollingDeviceRemovalPolicy = (RollingDevicePolicy)StringEnum.Parse(typeof(RollingDevicePolicy), node["rollingDeviceRemovalPolicy"].Value<string>());
			}
			if(node["rollingDeviceRemovalFamilyIds"] != null)
			{
				this._RollingDeviceRemovalFamilyIds = node["rollingDeviceRemovalFamilyIds"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaRollingDeviceRemovalData");
			kparams.AddIfNotNull("rollingDeviceRemovalPolicy", this._RollingDeviceRemovalPolicy);
			kparams.AddIfNotNull("rollingDeviceRemovalFamilyIds", this._RollingDeviceRemovalFamilyIds);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ROLLING_DEVICE_REMOVAL_POLICY:
					return "RollingDeviceRemovalPolicy";
				case ROLLING_DEVICE_REMOVAL_FAMILY_IDS:
					return "RollingDeviceRemovalFamilyIds";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

