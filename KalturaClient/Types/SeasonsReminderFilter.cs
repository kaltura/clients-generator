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
	public class SeasonsReminderFilter : ReminderFilter
	{
		#region Constants
		public const string SERIES_ID_EQUAL = "seriesIdEqual";
		public const string SEASON_NUMBER_IN = "seasonNumberIn";
		public const string EPG_CHANNEL_ID_EQUAL = "epgChannelIdEqual";
		#endregion

		#region Private Fields
		private string _SeriesIdEqual = null;
		private string _SeasonNumberIn = null;
		private long _EpgChannelIdEqual = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public string SeriesIdEqual
		{
			get { return _SeriesIdEqual; }
			set 
			{ 
				_SeriesIdEqual = value;
				OnPropertyChanged("SeriesIdEqual");
			}
		}
		[JsonProperty]
		public string SeasonNumberIn
		{
			get { return _SeasonNumberIn; }
			set 
			{ 
				_SeasonNumberIn = value;
				OnPropertyChanged("SeasonNumberIn");
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
		#endregion

		#region CTor
		public SeasonsReminderFilter()
		{
		}

		public SeasonsReminderFilter(JToken node) : base(node)
		{
			if(node["seriesIdEqual"] != null)
			{
				this._SeriesIdEqual = node["seriesIdEqual"].Value<string>();
			}
			if(node["seasonNumberIn"] != null)
			{
				this._SeasonNumberIn = node["seasonNumberIn"].Value<string>();
			}
			if(node["epgChannelIdEqual"] != null)
			{
				this._EpgChannelIdEqual = ParseLong(node["epgChannelIdEqual"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSeasonsReminderFilter");
			kparams.AddIfNotNull("seriesIdEqual", this._SeriesIdEqual);
			kparams.AddIfNotNull("seasonNumberIn", this._SeasonNumberIn);
			kparams.AddIfNotNull("epgChannelIdEqual", this._EpgChannelIdEqual);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SERIES_ID_EQUAL:
					return "SeriesIdEqual";
				case SEASON_NUMBER_IN:
					return "SeasonNumberIn";
				case EPG_CHANNEL_ID_EQUAL:
					return "EpgChannelIdEqual";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

