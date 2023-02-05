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
	public class SeriesRecording : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string EPG_ID = "epgId";
		public const string CHANNEL_ID = "channelId";
		public const string SERIES_ID = "seriesId";
		public const string SEASON_NUMBER = "seasonNumber";
		public const string TYPE = "type";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		public const string EXCLUDED_SEASONS = "excludedSeasons";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private long _EpgId = long.MinValue;
		private long _ChannelId = long.MinValue;
		private string _SeriesId = null;
		private int _SeasonNumber = Int32.MinValue;
		private RecordingType _Type = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		private IList<IntegerValue> _ExcludedSeasons;
		#endregion

		#region Properties
		[JsonProperty]
		public long Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public long EpgId
		{
			get { return _EpgId; }
			set 
			{ 
				_EpgId = value;
				OnPropertyChanged("EpgId");
			}
		}
		[JsonProperty]
		public long ChannelId
		{
			get { return _ChannelId; }
			set 
			{ 
				_ChannelId = value;
				OnPropertyChanged("ChannelId");
			}
		}
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
		public int SeasonNumber
		{
			get { return _SeasonNumber; }
			set 
			{ 
				_SeasonNumber = value;
				OnPropertyChanged("SeasonNumber");
			}
		}
		[JsonProperty]
		public RecordingType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		[JsonProperty]
		public long CreateDate
		{
			get { return _CreateDate; }
			private set 
			{ 
				_CreateDate = value;
				OnPropertyChanged("CreateDate");
			}
		}
		[JsonProperty]
		public long UpdateDate
		{
			get { return _UpdateDate; }
			private set 
			{ 
				_UpdateDate = value;
				OnPropertyChanged("UpdateDate");
			}
		}
		[JsonProperty]
		public IList<IntegerValue> ExcludedSeasons
		{
			get { return _ExcludedSeasons; }
			private set 
			{ 
				_ExcludedSeasons = value;
				OnPropertyChanged("ExcludedSeasons");
			}
		}
		#endregion

		#region CTor
		public SeriesRecording()
		{
		}

		public SeriesRecording(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["epgId"] != null)
			{
				this._EpgId = ParseLong(node["epgId"].Value<string>());
			}
			if(node["channelId"] != null)
			{
				this._ChannelId = ParseLong(node["channelId"].Value<string>());
			}
			if(node["seriesId"] != null)
			{
				this._SeriesId = node["seriesId"].Value<string>();
			}
			if(node["seasonNumber"] != null)
			{
				this._SeasonNumber = ParseInt(node["seasonNumber"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = (RecordingType)StringEnum.Parse(typeof(RecordingType), node["type"].Value<string>());
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
			if(node["excludedSeasons"] != null)
			{
				this._ExcludedSeasons = new List<IntegerValue>();
				foreach(var arrayNode in node["excludedSeasons"].Children())
				{
					this._ExcludedSeasons.Add(ObjectFactory.Create<IntegerValue>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSeriesRecording");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("epgId", this._EpgId);
			kparams.AddIfNotNull("channelId", this._ChannelId);
			kparams.AddIfNotNull("seriesId", this._SeriesId);
			kparams.AddIfNotNull("seasonNumber", this._SeasonNumber);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("excludedSeasons", this._ExcludedSeasons);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case EPG_ID:
					return "EpgId";
				case CHANNEL_ID:
					return "ChannelId";
				case SERIES_ID:
					return "SeriesId";
				case SEASON_NUMBER:
					return "SeasonNumber";
				case TYPE:
					return "Type";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				case EXCLUDED_SEASONS:
					return "ExcludedSeasons";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

