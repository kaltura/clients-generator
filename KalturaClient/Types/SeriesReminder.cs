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
	public class SeriesReminder : Reminder
	{
		#region Constants
		public const string SERIES_ID = "seriesId";
		public const string SEASON_NUMBER = "seasonNumber";
		public const string EPG_CHANNEL_ID = "epgChannelId";
		#endregion

		#region Private Fields
		private string _SeriesId = null;
		private long _SeasonNumber = long.MinValue;
		private long _EpgChannelId = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public string SeriesId
		{
			get { return _SeriesId; }
			set 
			{ 
				_SeriesId = value;
				OnPropertyChanged("SeriesId");
			}
		}
		[JsonProperty]
		public long SeasonNumber
		{
			get { return _SeasonNumber; }
			set 
			{ 
				_SeasonNumber = value;
				OnPropertyChanged("SeasonNumber");
			}
		}
		[JsonProperty]
		public long EpgChannelId
		{
			get { return _EpgChannelId; }
			set 
			{ 
				_EpgChannelId = value;
				OnPropertyChanged("EpgChannelId");
			}
		}
		#endregion

		#region CTor
		public SeriesReminder()
		{
		}

		public SeriesReminder(JToken node) : base(node)
		{
			if(node["seriesId"] != null)
			{
				this._SeriesId = node["seriesId"].Value<string>();
			}
			if(node["seasonNumber"] != null)
			{
				this._SeasonNumber = ParseLong(node["seasonNumber"].Value<string>());
			}
			if(node["epgChannelId"] != null)
			{
				this._EpgChannelId = ParseLong(node["epgChannelId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSeriesReminder");
			kparams.AddIfNotNull("seriesId", this._SeriesId);
			kparams.AddIfNotNull("seasonNumber", this._SeasonNumber);
			kparams.AddIfNotNull("epgChannelId", this._EpgChannelId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SERIES_ID:
					return "SeriesId";
				case SEASON_NUMBER:
					return "SeasonNumber";
				case EPG_CHANNEL_ID:
					return "EpgChannelId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

