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
	public class HouseholdSegment : CrudObject
	{
		#region Constants
		public const string SEGMENT_ID = "segmentId";
		public const string HOUSEHOLD_ID = "householdId";
		#endregion

		#region Private Fields
		private long _SegmentId = long.MinValue;
		private long _HouseholdId = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public long SegmentId
		{
			get { return _SegmentId; }
			set 
			{ 
				_SegmentId = value;
				OnPropertyChanged("SegmentId");
			}
		}
		[JsonProperty]
		public long HouseholdId
		{
			get { return _HouseholdId; }
			set 
			{ 
				_HouseholdId = value;
				OnPropertyChanged("HouseholdId");
			}
		}
		#endregion

		#region CTor
		public HouseholdSegment()
		{
		}

		public HouseholdSegment(JToken node) : base(node)
		{
			if(node["segmentId"] != null)
			{
				this._SegmentId = ParseLong(node["segmentId"].Value<string>());
			}
			if(node["householdId"] != null)
			{
				this._HouseholdId = ParseLong(node["householdId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaHouseholdSegment");
			kparams.AddIfNotNull("segmentId", this._SegmentId);
			kparams.AddIfNotNull("householdId", this._HouseholdId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SEGMENT_ID:
					return "SegmentId";
				case HOUSEHOLD_ID:
					return "HouseholdId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

