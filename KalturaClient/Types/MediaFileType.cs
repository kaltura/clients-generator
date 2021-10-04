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
	public class MediaFileType : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string DESCRIPTION = "description";
		public const string STATUS = "status";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		public const string IS_TRAILER = "isTrailer";
		public const string STREAMER_TYPE = "streamerType";
		public const string DRM_PROFILE_ID = "drmProfileId";
		public const string QUALITY = "quality";
		public const string VIDEO_CODECS = "videoCodecs";
		public const string AUDIO_CODECS = "audioCodecs";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _Name = null;
		private string _Description = null;
		private bool? _Status = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		private bool? _IsTrailer = null;
		private MediaFileStreamerType _StreamerType = null;
		private int _DrmProfileId = Int32.MinValue;
		private MediaFileTypeQuality _Quality = null;
		private string _VideoCodecs = null;
		private string _AudioCodecs = null;
		#endregion

		#region Properties
		[JsonProperty]
		public int Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public string Description
		{
			get { return _Description; }
			set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		[JsonProperty]
		public bool? Status
		{
			get { return _Status; }
			set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
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
		public bool? IsTrailer
		{
			get { return _IsTrailer; }
			set 
			{ 
				_IsTrailer = value;
				OnPropertyChanged("IsTrailer");
			}
		}
		[JsonProperty]
		public MediaFileStreamerType StreamerType
		{
			get { return _StreamerType; }
			set 
			{ 
				_StreamerType = value;
				OnPropertyChanged("StreamerType");
			}
		}
		[JsonProperty]
		public int DrmProfileId
		{
			get { return _DrmProfileId; }
			set 
			{ 
				_DrmProfileId = value;
				OnPropertyChanged("DrmProfileId");
			}
		}
		[JsonProperty]
		public MediaFileTypeQuality Quality
		{
			get { return _Quality; }
			set 
			{ 
				_Quality = value;
				OnPropertyChanged("Quality");
			}
		}
		[JsonProperty]
		public string VideoCodecs
		{
			get { return _VideoCodecs; }
			set 
			{ 
				_VideoCodecs = value;
				OnPropertyChanged("VideoCodecs");
			}
		}
		[JsonProperty]
		public string AudioCodecs
		{
			get { return _AudioCodecs; }
			set 
			{ 
				_AudioCodecs = value;
				OnPropertyChanged("AudioCodecs");
			}
		}
		#endregion

		#region CTor
		public MediaFileType()
		{
		}

		public MediaFileType(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["description"] != null)
			{
				this._Description = node["description"].Value<string>();
			}
			if(node["status"] != null)
			{
				this._Status = ParseBool(node["status"].Value<string>());
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
			if(node["isTrailer"] != null)
			{
				this._IsTrailer = ParseBool(node["isTrailer"].Value<string>());
			}
			if(node["streamerType"] != null)
			{
				this._StreamerType = (MediaFileStreamerType)StringEnum.Parse(typeof(MediaFileStreamerType), node["streamerType"].Value<string>());
			}
			if(node["drmProfileId"] != null)
			{
				this._DrmProfileId = ParseInt(node["drmProfileId"].Value<string>());
			}
			if(node["quality"] != null)
			{
				this._Quality = (MediaFileTypeQuality)StringEnum.Parse(typeof(MediaFileTypeQuality), node["quality"].Value<string>());
			}
			if(node["videoCodecs"] != null)
			{
				this._VideoCodecs = node["videoCodecs"].Value<string>();
			}
			if(node["audioCodecs"] != null)
			{
				this._AudioCodecs = node["audioCodecs"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaMediaFileType");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("isTrailer", this._IsTrailer);
			kparams.AddIfNotNull("streamerType", this._StreamerType);
			kparams.AddIfNotNull("drmProfileId", this._DrmProfileId);
			kparams.AddIfNotNull("quality", this._Quality);
			kparams.AddIfNotNull("videoCodecs", this._VideoCodecs);
			kparams.AddIfNotNull("audioCodecs", this._AudioCodecs);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case NAME:
					return "Name";
				case DESCRIPTION:
					return "Description";
				case STATUS:
					return "Status";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				case IS_TRAILER:
					return "IsTrailer";
				case STREAMER_TYPE:
					return "StreamerType";
				case DRM_PROFILE_ID:
					return "DrmProfileId";
				case QUALITY:
					return "Quality";
				case VIDEO_CODECS:
					return "VideoCodecs";
				case AUDIO_CODECS:
					return "AudioCodecs";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

