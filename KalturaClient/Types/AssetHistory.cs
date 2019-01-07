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
	public class AssetHistory : ObjectBase
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string ASSET_TYPE = "assetType";
		public const string POSITION = "position";
		public const string DURATION = "duration";
		public const string WATCHED_DATE = "watchedDate";
		public const string FINISHED_WATCHING = "finishedWatching";
		#endregion

		#region Private Fields
		private long _AssetId = long.MinValue;
		private AssetType _AssetType = null;
		private int _Position = Int32.MinValue;
		private int _Duration = Int32.MinValue;
		private long _WatchedDate = long.MinValue;
		private bool? _FinishedWatching = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long AssetId
		{
			get { return _AssetId; }
			private set 
			{ 
				_AssetId = value;
				OnPropertyChanged("AssetId");
			}
		}
		[JsonProperty]
		public AssetType AssetType
		{
			get { return _AssetType; }
			private set 
			{ 
				_AssetType = value;
				OnPropertyChanged("AssetType");
			}
		}
		[JsonProperty]
		public int Position
		{
			get { return _Position; }
			private set 
			{ 
				_Position = value;
				OnPropertyChanged("Position");
			}
		}
		[JsonProperty]
		public int Duration
		{
			get { return _Duration; }
			private set 
			{ 
				_Duration = value;
				OnPropertyChanged("Duration");
			}
		}
		[JsonProperty]
		public long WatchedDate
		{
			get { return _WatchedDate; }
			private set 
			{ 
				_WatchedDate = value;
				OnPropertyChanged("WatchedDate");
			}
		}
		[JsonProperty]
		public bool? FinishedWatching
		{
			get { return _FinishedWatching; }
			private set 
			{ 
				_FinishedWatching = value;
				OnPropertyChanged("FinishedWatching");
			}
		}
		#endregion

		#region CTor
		public AssetHistory()
		{
		}

		public AssetHistory(JToken node) : base(node)
		{
			if(node["assetId"] != null)
			{
				this._AssetId = ParseLong(node["assetId"].Value<string>());
			}
			if(node["assetType"] != null)
			{
				this._AssetType = (AssetType)StringEnum.Parse(typeof(AssetType), node["assetType"].Value<string>());
			}
			if(node["position"] != null)
			{
				this._Position = ParseInt(node["position"].Value<string>());
			}
			if(node["duration"] != null)
			{
				this._Duration = ParseInt(node["duration"].Value<string>());
			}
			if(node["watchedDate"] != null)
			{
				this._WatchedDate = ParseLong(node["watchedDate"].Value<string>());
			}
			if(node["finishedWatching"] != null)
			{
				this._FinishedWatching = ParseBool(node["finishedWatching"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetHistory");
			kparams.AddIfNotNull("assetId", this._AssetId);
			kparams.AddIfNotNull("assetType", this._AssetType);
			kparams.AddIfNotNull("position", this._Position);
			kparams.AddIfNotNull("duration", this._Duration);
			kparams.AddIfNotNull("watchedDate", this._WatchedDate);
			kparams.AddIfNotNull("finishedWatching", this._FinishedWatching);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_ID:
					return "AssetId";
				case ASSET_TYPE:
					return "AssetType";
				case POSITION:
					return "Position";
				case DURATION:
					return "Duration";
				case WATCHED_DATE:
					return "WatchedDate";
				case FINISHED_WATCHING:
					return "FinishedWatching";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

