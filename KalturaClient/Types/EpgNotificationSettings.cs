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
	public class EpgNotificationSettings : ObjectBase
	{
		#region Constants
		public const string ENABLED = "enabled";
		public const string DEVICE_FAMILY_IDS = "deviceFamilyIds";
		public const string LIVE_ASSET_IDS = "liveAssetIds";
		public const string BACKWARD_TIME_RANGE = "backwardTimeRange";
		public const string FORWARD_TIME_RANGE = "forwardTimeRange";
		#endregion

		#region Private Fields
		private bool? _Enabled = null;
		private string _DeviceFamilyIds = null;
		private string _LiveAssetIds = null;
		private int _BackwardTimeRange = Int32.MinValue;
		private int _ForwardTimeRange = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public bool? Enabled
		{
			get { return _Enabled; }
			set 
			{ 
				_Enabled = value;
				OnPropertyChanged("Enabled");
			}
		}
		[JsonProperty]
		public string DeviceFamilyIds
		{
			get { return _DeviceFamilyIds; }
			set 
			{ 
				_DeviceFamilyIds = value;
				OnPropertyChanged("DeviceFamilyIds");
			}
		}
		[JsonProperty]
		public string LiveAssetIds
		{
			get { return _LiveAssetIds; }
			set 
			{ 
				_LiveAssetIds = value;
				OnPropertyChanged("LiveAssetIds");
			}
		}
		[JsonProperty]
		public int BackwardTimeRange
		{
			get { return _BackwardTimeRange; }
			set 
			{ 
				_BackwardTimeRange = value;
				OnPropertyChanged("BackwardTimeRange");
			}
		}
		[JsonProperty]
		public int ForwardTimeRange
		{
			get { return _ForwardTimeRange; }
			set 
			{ 
				_ForwardTimeRange = value;
				OnPropertyChanged("ForwardTimeRange");
			}
		}
		#endregion

		#region CTor
		public EpgNotificationSettings()
		{
		}

		public EpgNotificationSettings(JToken node) : base(node)
		{
			if(node["enabled"] != null)
			{
				this._Enabled = ParseBool(node["enabled"].Value<string>());
			}
			if(node["deviceFamilyIds"] != null)
			{
				this._DeviceFamilyIds = node["deviceFamilyIds"].Value<string>();
			}
			if(node["liveAssetIds"] != null)
			{
				this._LiveAssetIds = node["liveAssetIds"].Value<string>();
			}
			if(node["backwardTimeRange"] != null)
			{
				this._BackwardTimeRange = ParseInt(node["backwardTimeRange"].Value<string>());
			}
			if(node["forwardTimeRange"] != null)
			{
				this._ForwardTimeRange = ParseInt(node["forwardTimeRange"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaEpgNotificationSettings");
			kparams.AddIfNotNull("enabled", this._Enabled);
			kparams.AddIfNotNull("deviceFamilyIds", this._DeviceFamilyIds);
			kparams.AddIfNotNull("liveAssetIds", this._LiveAssetIds);
			kparams.AddIfNotNull("backwardTimeRange", this._BackwardTimeRange);
			kparams.AddIfNotNull("forwardTimeRange", this._ForwardTimeRange);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ENABLED:
					return "Enabled";
				case DEVICE_FAMILY_IDS:
					return "DeviceFamilyIds";
				case LIVE_ASSET_IDS:
					return "LiveAssetIds";
				case BACKWARD_TIME_RANGE:
					return "BackwardTimeRange";
				case FORWARD_TIME_RANGE:
					return "ForwardTimeRange";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

