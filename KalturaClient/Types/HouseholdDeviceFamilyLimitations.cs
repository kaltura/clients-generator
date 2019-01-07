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
	public class HouseholdDeviceFamilyLimitations : DeviceFamilyBase
	{
		#region Constants
		public const string FREQUENCY = "frequency";
		public const string DEVICE_LIMIT = "deviceLimit";
		public const string CONCURRENT_LIMIT = "concurrentLimit";
		#endregion

		#region Private Fields
		private int _Frequency = Int32.MinValue;
		private int _DeviceLimit = Int32.MinValue;
		private int _ConcurrentLimit = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public int Frequency
		{
			get { return _Frequency; }
			set 
			{ 
				_Frequency = value;
				OnPropertyChanged("Frequency");
			}
		}
		[JsonProperty]
		public int DeviceLimit
		{
			get { return _DeviceLimit; }
			set 
			{ 
				_DeviceLimit = value;
				OnPropertyChanged("DeviceLimit");
			}
		}
		[JsonProperty]
		public int ConcurrentLimit
		{
			get { return _ConcurrentLimit; }
			set 
			{ 
				_ConcurrentLimit = value;
				OnPropertyChanged("ConcurrentLimit");
			}
		}
		#endregion

		#region CTor
		public HouseholdDeviceFamilyLimitations()
		{
		}

		public HouseholdDeviceFamilyLimitations(JToken node) : base(node)
		{
			if(node["frequency"] != null)
			{
				this._Frequency = ParseInt(node["frequency"].Value<string>());
			}
			if(node["deviceLimit"] != null)
			{
				this._DeviceLimit = ParseInt(node["deviceLimit"].Value<string>());
			}
			if(node["concurrentLimit"] != null)
			{
				this._ConcurrentLimit = ParseInt(node["concurrentLimit"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaHouseholdDeviceFamilyLimitations");
			kparams.AddIfNotNull("frequency", this._Frequency);
			kparams.AddIfNotNull("deviceLimit", this._DeviceLimit);
			kparams.AddIfNotNull("concurrentLimit", this._ConcurrentLimit);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case FREQUENCY:
					return "Frequency";
				case DEVICE_LIMIT:
					return "DeviceLimit";
				case CONCURRENT_LIMIT:
					return "ConcurrentLimit";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

