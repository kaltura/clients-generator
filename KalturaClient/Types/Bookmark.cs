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
	public class Bookmark : SlimAsset
	{
		#region Constants
		public const string USER_ID = "userId";
		public const string POSITION = "position";
		public const string POSITION_OWNER = "positionOwner";
		public const string FINISHED_WATCHING = "finishedWatching";
		public const string PLAYER_DATA = "playerData";
		public const string PROGRAM_ID = "programId";
		public const string IS_REPORTING_MODE = "isReportingMode";
		#endregion

		#region Private Fields
		private string _UserId = null;
		private int _Position = Int32.MinValue;
		private PositionOwner _PositionOwner = null;
		private bool? _FinishedWatching = null;
		private BookmarkPlayerData _PlayerData;
		private long _ProgramId = long.MinValue;
		private bool? _IsReportingMode = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string UserId
		{
			get { return _UserId; }
			private set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		[JsonProperty]
		public int Position
		{
			get { return _Position; }
			set 
			{ 
				_Position = value;
				OnPropertyChanged("Position");
			}
		}
		[JsonProperty]
		public PositionOwner PositionOwner
		{
			get { return _PositionOwner; }
			private set 
			{ 
				_PositionOwner = value;
				OnPropertyChanged("PositionOwner");
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
		[JsonProperty]
		public BookmarkPlayerData PlayerData
		{
			get { return _PlayerData; }
			set 
			{ 
				_PlayerData = value;
				OnPropertyChanged("PlayerData");
			}
		}
		[JsonProperty]
		public long ProgramId
		{
			get { return _ProgramId; }
			set 
			{ 
				_ProgramId = value;
				OnPropertyChanged("ProgramId");
			}
		}
		[JsonProperty]
		public bool? IsReportingMode
		{
			get { return _IsReportingMode; }
			set 
			{ 
				_IsReportingMode = value;
				OnPropertyChanged("IsReportingMode");
			}
		}
		#endregion

		#region CTor
		public Bookmark()
		{
		}

		public Bookmark(JToken node) : base(node)
		{
			if(node["userId"] != null)
			{
				this._UserId = node["userId"].Value<string>();
			}
			if(node["position"] != null)
			{
				this._Position = ParseInt(node["position"].Value<string>());
			}
			if(node["positionOwner"] != null)
			{
				this._PositionOwner = (PositionOwner)StringEnum.Parse(typeof(PositionOwner), node["positionOwner"].Value<string>());
			}
			if(node["finishedWatching"] != null)
			{
				this._FinishedWatching = ParseBool(node["finishedWatching"].Value<string>());
			}
			if(node["playerData"] != null)
			{
				this._PlayerData = ObjectFactory.Create<BookmarkPlayerData>(node["playerData"]);
			}
			if(node["programId"] != null)
			{
				this._ProgramId = ParseLong(node["programId"].Value<string>());
			}
			if(node["isReportingMode"] != null)
			{
				this._IsReportingMode = ParseBool(node["isReportingMode"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBookmark");
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("position", this._Position);
			kparams.AddIfNotNull("positionOwner", this._PositionOwner);
			kparams.AddIfNotNull("finishedWatching", this._FinishedWatching);
			kparams.AddIfNotNull("playerData", this._PlayerData);
			kparams.AddIfNotNull("programId", this._ProgramId);
			kparams.AddIfNotNull("isReportingMode", this._IsReportingMode);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case USER_ID:
					return "UserId";
				case POSITION:
					return "Position";
				case POSITION_OWNER:
					return "PositionOwner";
				case FINISHED_WATCHING:
					return "FinishedWatching";
				case PLAYER_DATA:
					return "PlayerData";
				case PROGRAM_ID:
					return "ProgramId";
				case IS_REPORTING_MODE:
					return "IsReportingMode";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

