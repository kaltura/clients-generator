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
	public class FollowDataBase : ObjectBase
	{
		#region Constants
		public const string ANNOUNCEMENT_ID = "announcementId";
		public const string STATUS = "status";
		public const string TITLE = "title";
		public const string TIMESTAMP = "timestamp";
		public const string FOLLOW_PHRASE = "followPhrase";
		#endregion

		#region Private Fields
		private long _AnnouncementId = long.MinValue;
		private int _Status = Int32.MinValue;
		private string _Title = null;
		private long _Timestamp = long.MinValue;
		private string _FollowPhrase = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long AnnouncementId
		{
			get { return _AnnouncementId; }
			private set 
			{ 
				_AnnouncementId = value;
				OnPropertyChanged("AnnouncementId");
			}
		}
		[JsonProperty]
		public int Status
		{
			get { return _Status; }
			private set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
		public string Title
		{
			get { return _Title; }
			private set 
			{ 
				_Title = value;
				OnPropertyChanged("Title");
			}
		}
		[JsonProperty]
		public long Timestamp
		{
			get { return _Timestamp; }
			private set 
			{ 
				_Timestamp = value;
				OnPropertyChanged("Timestamp");
			}
		}
		[JsonProperty]
		public string FollowPhrase
		{
			get { return _FollowPhrase; }
			private set 
			{ 
				_FollowPhrase = value;
				OnPropertyChanged("FollowPhrase");
			}
		}
		#endregion

		#region CTor
		public FollowDataBase()
		{
		}

		public FollowDataBase(JToken node) : base(node)
		{
			if(node["announcementId"] != null)
			{
				this._AnnouncementId = ParseLong(node["announcementId"].Value<string>());
			}
			if(node["status"] != null)
			{
				this._Status = ParseInt(node["status"].Value<string>());
			}
			if(node["title"] != null)
			{
				this._Title = node["title"].Value<string>();
			}
			if(node["timestamp"] != null)
			{
				this._Timestamp = ParseLong(node["timestamp"].Value<string>());
			}
			if(node["followPhrase"] != null)
			{
				this._FollowPhrase = node["followPhrase"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaFollowDataBase");
			kparams.AddIfNotNull("announcementId", this._AnnouncementId);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("title", this._Title);
			kparams.AddIfNotNull("timestamp", this._Timestamp);
			kparams.AddIfNotNull("followPhrase", this._FollowPhrase);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ANNOUNCEMENT_ID:
					return "AnnouncementId";
				case STATUS:
					return "Status";
				case TITLE:
					return "Title";
				case TIMESTAMP:
					return "Timestamp";
				case FOLLOW_PHRASE:
					return "FollowPhrase";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

