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
		public int Id
		{
			get { return _Id; }
		}
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		public string Description
		{
			get { return _Description; }
			set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		public bool? Status
		{
			get { return _Status; }
			set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		public long CreateDate
		{
			get { return _CreateDate; }
		}
		public long UpdateDate
		{
			get { return _UpdateDate; }
		}
		public bool? IsTrailer
		{
			get { return _IsTrailer; }
			set 
			{ 
				_IsTrailer = value;
				OnPropertyChanged("IsTrailer");
			}
		}
		public MediaFileStreamerType StreamerType
		{
			get { return _StreamerType; }
			set 
			{ 
				_StreamerType = value;
				OnPropertyChanged("StreamerType");
			}
		}
		public int DrmProfileId
		{
			get { return _DrmProfileId; }
			set 
			{ 
				_DrmProfileId = value;
				OnPropertyChanged("DrmProfileId");
			}
		}
		public MediaFileTypeQuality Quality
		{
			get { return _Quality; }
			set 
			{ 
				_Quality = value;
				OnPropertyChanged("Quality");
			}
		}
		public string VideoCodecs
		{
			get { return _VideoCodecs; }
			set 
			{ 
				_VideoCodecs = value;
				OnPropertyChanged("VideoCodecs");
			}
		}
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

		public MediaFileType(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "description":
						this._Description = propertyNode.InnerText;
						continue;
					case "status":
						this._Status = ParseBool(propertyNode.InnerText);
						continue;
					case "createDate":
						this._CreateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "updateDate":
						this._UpdateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "isTrailer":
						this._IsTrailer = ParseBool(propertyNode.InnerText);
						continue;
					case "streamerType":
						this._StreamerType = (MediaFileStreamerType)StringEnum.Parse(typeof(MediaFileStreamerType), propertyNode.InnerText);
						continue;
					case "drmProfileId":
						this._DrmProfileId = ParseInt(propertyNode.InnerText);
						continue;
					case "quality":
						this._Quality = (MediaFileTypeQuality)StringEnum.Parse(typeof(MediaFileTypeQuality), propertyNode.InnerText);
						continue;
					case "videoCodecs":
						this._VideoCodecs = propertyNode.InnerText;
						continue;
					case "audioCodecs":
						this._AudioCodecs = propertyNode.InnerText;
						continue;
				}
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

