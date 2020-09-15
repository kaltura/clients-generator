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
// Copyright (C) 2006-2020  Kaltura Inc.
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
	public class CampaignSearchFilter : CampaignFilter
	{
		#region Constants
		public const string START_DATE_GREATER_THAN_OR_EQUAL = "startDateGreaterThanOrEqual";
		public const string END_DATE_LESS_THAN_OR_EQUAL = "endDateLessThanOrEqual";
		public const string STATE_EQUAL = "stateEqual";
		public const string HAS_PROMOTION = "hasPromotion";
		#endregion

		#region Private Fields
		private long _StartDateGreaterThanOrEqual = long.MinValue;
		private long _EndDateLessThanOrEqual = long.MinValue;
		private ObjectState _StateEqual = null;
		private bool? _HasPromotion = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long StartDateGreaterThanOrEqual
		{
			get { return _StartDateGreaterThanOrEqual; }
			set 
			{ 
				_StartDateGreaterThanOrEqual = value;
				OnPropertyChanged("StartDateGreaterThanOrEqual");
			}
		}
		[JsonProperty]
		public long EndDateLessThanOrEqual
		{
			get { return _EndDateLessThanOrEqual; }
			set 
			{ 
				_EndDateLessThanOrEqual = value;
				OnPropertyChanged("EndDateLessThanOrEqual");
			}
		}
		[JsonProperty]
		public ObjectState StateEqual
		{
			get { return _StateEqual; }
			set 
			{ 
				_StateEqual = value;
				OnPropertyChanged("StateEqual");
			}
		}
		[JsonProperty]
		public bool? HasPromotion
		{
			get { return _HasPromotion; }
			set 
			{ 
				_HasPromotion = value;
				OnPropertyChanged("HasPromotion");
			}
		}
		#endregion

		#region CTor
		public CampaignSearchFilter()
		{
		}

		public CampaignSearchFilter(JToken node) : base(node)
		{
			if(node["startDateGreaterThanOrEqual"] != null)
			{
				this._StartDateGreaterThanOrEqual = ParseLong(node["startDateGreaterThanOrEqual"].Value<string>());
			}
			if(node["endDateLessThanOrEqual"] != null)
			{
				this._EndDateLessThanOrEqual = ParseLong(node["endDateLessThanOrEqual"].Value<string>());
			}
			if(node["stateEqual"] != null)
			{
				this._StateEqual = (ObjectState)StringEnum.Parse(typeof(ObjectState), node["stateEqual"].Value<string>());
			}
			if(node["hasPromotion"] != null)
			{
				this._HasPromotion = ParseBool(node["hasPromotion"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCampaignSearchFilter");
			kparams.AddIfNotNull("startDateGreaterThanOrEqual", this._StartDateGreaterThanOrEqual);
			kparams.AddIfNotNull("endDateLessThanOrEqual", this._EndDateLessThanOrEqual);
			kparams.AddIfNotNull("stateEqual", this._StateEqual);
			kparams.AddIfNotNull("hasPromotion", this._HasPromotion);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case START_DATE_GREATER_THAN_OR_EQUAL:
					return "StartDateGreaterThanOrEqual";
				case END_DATE_LESS_THAN_OR_EQUAL:
					return "EndDateLessThanOrEqual";
				case STATE_EQUAL:
					return "StateEqual";
				case HAS_PROMOTION:
					return "HasPromotion";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

