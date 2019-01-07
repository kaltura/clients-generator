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
	public class SeriesReminderFilter : ReminderFilter
	{
		#region Constants
		public const string SERIES_ID_IN = "seriesIdIn";
		public const string EPG_CHANNEL_ID_EQUAL = "epgChannelIdEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _SeriesIdIn = null;
		private long _EpgChannelIdEqual = long.MinValue;
		private SeriesReminderOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string SeriesIdIn
		{
			get { return _SeriesIdIn; }
			set 
			{ 
				_SeriesIdIn = value;
				OnPropertyChanged("SeriesIdIn");
			}
		}
		[JsonProperty]
		public long EpgChannelIdEqual
		{
			get { return _EpgChannelIdEqual; }
			set 
			{ 
				_EpgChannelIdEqual = value;
				OnPropertyChanged("EpgChannelIdEqual");
			}
		}
		[JsonProperty]
		public new SeriesReminderOrderBy OrderBy
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
		public SeriesReminderFilter()
		{
		}

		public SeriesReminderFilter(JToken node) : base(node)
		{
			if(node["seriesIdIn"] != null)
			{
				this._SeriesIdIn = node["seriesIdIn"].Value<string>();
			}
			if(node["epgChannelIdEqual"] != null)
			{
				this._EpgChannelIdEqual = ParseLong(node["epgChannelIdEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (SeriesReminderOrderBy)StringEnum.Parse(typeof(SeriesReminderOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSeriesReminderFilter");
			kparams.AddIfNotNull("seriesIdIn", this._SeriesIdIn);
			kparams.AddIfNotNull("epgChannelIdEqual", this._EpgChannelIdEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SERIES_ID_IN:
					return "SeriesIdIn";
				case EPG_CHANNEL_ID_EQUAL:
					return "EpgChannelIdEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

