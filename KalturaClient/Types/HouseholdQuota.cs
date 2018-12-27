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
	public class HouseholdQuota : ObjectBase
	{
		#region Constants
		public const string HOUSEHOLD_ID = "householdId";
		public const string TOTAL_QUOTA = "totalQuota";
		public const string AVAILABLE_QUOTA = "availableQuota";
		#endregion

		#region Private Fields
		private long _HouseholdId = long.MinValue;
		private int _TotalQuota = Int32.MinValue;
		private int _AvailableQuota = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public long HouseholdId
		{
			get { return _HouseholdId; }
			private set 
			{ 
				_HouseholdId = value;
				OnPropertyChanged("HouseholdId");
			}
		}
		[JsonProperty]
		public int TotalQuota
		{
			get { return _TotalQuota; }
			private set 
			{ 
				_TotalQuota = value;
				OnPropertyChanged("TotalQuota");
			}
		}
		[JsonProperty]
		public int AvailableQuota
		{
			get { return _AvailableQuota; }
			private set 
			{ 
				_AvailableQuota = value;
				OnPropertyChanged("AvailableQuota");
			}
		}
		#endregion

		#region CTor
		public HouseholdQuota()
		{
		}

		public HouseholdQuota(JToken node) : base(node)
		{
			if(node["householdId"] != null)
			{
				this._HouseholdId = ParseLong(node["householdId"].Value<string>());
			}
			if(node["totalQuota"] != null)
			{
				this._TotalQuota = ParseInt(node["totalQuota"].Value<string>());
			}
			if(node["availableQuota"] != null)
			{
				this._AvailableQuota = ParseInt(node["availableQuota"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaHouseholdQuota");
			kparams.AddIfNotNull("householdId", this._HouseholdId);
			kparams.AddIfNotNull("totalQuota", this._TotalQuota);
			kparams.AddIfNotNull("availableQuota", this._AvailableQuota);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case HOUSEHOLD_ID:
					return "HouseholdId";
				case TOTAL_QUOTA:
					return "TotalQuota";
				case AVAILABLE_QUOTA:
					return "AvailableQuota";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

