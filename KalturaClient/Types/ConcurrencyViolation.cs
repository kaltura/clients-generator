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
	public class ConcurrencyViolation : EventObject
	{
		#region Constants
		public const string TIMESTAMP = "timestamp";
		public const string UDID = "udid";
		public const string ASSET_ID = "assetId";
		public const string VIOLATION_RULE = "violationRule";
		public const string HOUSEHOLD_ID = "householdId";
		public const string USER_ID = "userId";
		#endregion

		#region Private Fields
		private long _Timestamp = long.MinValue;
		private string _Udid = null;
		private string _AssetId = null;
		private string _ViolationRule = null;
		private string _HouseholdId = null;
		private string _UserId = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long Timestamp
		{
			get { return _Timestamp; }
			set 
			{ 
				_Timestamp = value;
				OnPropertyChanged("Timestamp");
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
		[JsonProperty]
		public string AssetId
		{
			get { return _AssetId; }
			set 
			{ 
				_AssetId = value;
				OnPropertyChanged("AssetId");
			}
		}
		[JsonProperty]
		public string ViolationRule
		{
			get { return _ViolationRule; }
			set 
			{ 
				_ViolationRule = value;
				OnPropertyChanged("ViolationRule");
			}
		}
		[JsonProperty]
		public string HouseholdId
		{
			get { return _HouseholdId; }
			set 
			{ 
				_HouseholdId = value;
				OnPropertyChanged("HouseholdId");
			}
		}
		[JsonProperty]
		public string UserId
		{
			get { return _UserId; }
			set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		#endregion

		#region CTor
		public ConcurrencyViolation()
		{
		}

		public ConcurrencyViolation(JToken node) : base(node)
		{
			if(node["timestamp"] != null)
			{
				this._Timestamp = ParseLong(node["timestamp"].Value<string>());
			}
			if(node["udid"] != null)
			{
				this._Udid = node["udid"].Value<string>();
			}
			if(node["assetId"] != null)
			{
				this._AssetId = node["assetId"].Value<string>();
			}
			if(node["violationRule"] != null)
			{
				this._ViolationRule = node["violationRule"].Value<string>();
			}
			if(node["householdId"] != null)
			{
				this._HouseholdId = node["householdId"].Value<string>();
			}
			if(node["userId"] != null)
			{
				this._UserId = node["userId"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaConcurrencyViolation");
			kparams.AddIfNotNull("timestamp", this._Timestamp);
			kparams.AddIfNotNull("udid", this._Udid);
			kparams.AddIfNotNull("assetId", this._AssetId);
			kparams.AddIfNotNull("violationRule", this._ViolationRule);
			kparams.AddIfNotNull("householdId", this._HouseholdId);
			kparams.AddIfNotNull("userId", this._UserId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case TIMESTAMP:
					return "Timestamp";
				case UDID:
					return "Udid";
				case ASSET_ID:
					return "AssetId";
				case VIOLATION_RULE:
					return "ViolationRule";
				case HOUSEHOLD_ID:
					return "HouseholdId";
				case USER_ID:
					return "UserId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

