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
	public class DefaultPlaybackAdapters : ObjectBase
	{
		#region Constants
		public const string MEDIA_ADAPTER_ID = "mediaAdapterId";
		public const string EPG_ADAPTER_ID = "epgAdapterId";
		public const string RECORDING_ADAPTER_ID = "recordingAdapterId";
		#endregion

		#region Private Fields
		private long _MediaAdapterId = long.MinValue;
		private long _EpgAdapterId = long.MinValue;
		private long _RecordingAdapterId = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public long MediaAdapterId
		{
			get { return _MediaAdapterId; }
			set 
			{ 
				_MediaAdapterId = value;
				OnPropertyChanged("MediaAdapterId");
			}
		}
		[JsonProperty]
		public long EpgAdapterId
		{
			get { return _EpgAdapterId; }
			set 
			{ 
				_EpgAdapterId = value;
				OnPropertyChanged("EpgAdapterId");
			}
		}
		[JsonProperty]
		public long RecordingAdapterId
		{
			get { return _RecordingAdapterId; }
			set 
			{ 
				_RecordingAdapterId = value;
				OnPropertyChanged("RecordingAdapterId");
			}
		}
		#endregion

		#region CTor
		public DefaultPlaybackAdapters()
		{
		}

		public DefaultPlaybackAdapters(JToken node) : base(node)
		{
			if(node["mediaAdapterId"] != null)
			{
				this._MediaAdapterId = ParseLong(node["mediaAdapterId"].Value<string>());
			}
			if(node["epgAdapterId"] != null)
			{
				this._EpgAdapterId = ParseLong(node["epgAdapterId"].Value<string>());
			}
			if(node["recordingAdapterId"] != null)
			{
				this._RecordingAdapterId = ParseLong(node["recordingAdapterId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaDefaultPlaybackAdapters");
			kparams.AddIfNotNull("mediaAdapterId", this._MediaAdapterId);
			kparams.AddIfNotNull("epgAdapterId", this._EpgAdapterId);
			kparams.AddIfNotNull("recordingAdapterId", this._RecordingAdapterId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case MEDIA_ADAPTER_ID:
					return "MediaAdapterId";
				case EPG_ADAPTER_ID:
					return "EpgAdapterId";
				case RECORDING_ADAPTER_ID:
					return "RecordingAdapterId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

