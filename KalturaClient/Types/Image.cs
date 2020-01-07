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
	public class Image : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string VERSION = "version";
		public const string IMAGE_TYPE_ID = "imageTypeId";
		public const string IMAGE_OBJECT_ID = "imageObjectId";
		public const string IMAGE_OBJECT_TYPE = "imageObjectType";
		public const string STATUS = "status";
		public const string URL = "url";
		public const string CONTENT_ID = "contentId";
		public const string IS_DEFAULT = "isDefault";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Version = null;
		private long _ImageTypeId = long.MinValue;
		private long _ImageObjectId = long.MinValue;
		private ImageObjectType _ImageObjectType = null;
		private ImageStatus _Status = null;
		private string _Url = null;
		private string _ContentId = null;
		private bool? _IsDefault = null;
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
		public string Version
		{
			get { return _Version; }
			private set 
			{ 
				_Version = value;
				OnPropertyChanged("Version");
			}
		}
		[JsonProperty]
		public long ImageTypeId
		{
			get { return _ImageTypeId; }
			set 
			{ 
				_ImageTypeId = value;
				OnPropertyChanged("ImageTypeId");
			}
		}
		[JsonProperty]
		public long ImageObjectId
		{
			get { return _ImageObjectId; }
			set 
			{ 
				_ImageObjectId = value;
				OnPropertyChanged("ImageObjectId");
			}
		}
		[JsonProperty]
		public ImageObjectType ImageObjectType
		{
			get { return _ImageObjectType; }
			set 
			{ 
				_ImageObjectType = value;
				OnPropertyChanged("ImageObjectType");
			}
		}
		[JsonProperty]
		public ImageStatus Status
		{
			get { return _Status; }
			private set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
		public string Url
		{
			get { return _Url; }
			private set 
			{ 
				_Url = value;
				OnPropertyChanged("Url");
			}
		}
		[JsonProperty]
		public string ContentId
		{
			get { return _ContentId; }
			private set 
			{ 
				_ContentId = value;
				OnPropertyChanged("ContentId");
			}
		}
		[JsonProperty]
		public bool? IsDefault
		{
			get { return _IsDefault; }
			private set 
			{ 
				_IsDefault = value;
				OnPropertyChanged("IsDefault");
			}
		}
		#endregion

		#region CTor
		public Image()
		{
		}

		public Image(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["version"] != null)
			{
				this._Version = node["version"].Value<string>();
			}
			if(node["imageTypeId"] != null)
			{
				this._ImageTypeId = ParseLong(node["imageTypeId"].Value<string>());
			}
			if(node["imageObjectId"] != null)
			{
				this._ImageObjectId = ParseLong(node["imageObjectId"].Value<string>());
			}
			if(node["imageObjectType"] != null)
			{
				this._ImageObjectType = (ImageObjectType)StringEnum.Parse(typeof(ImageObjectType), node["imageObjectType"].Value<string>());
			}
			if(node["status"] != null)
			{
				this._Status = (ImageStatus)StringEnum.Parse(typeof(ImageStatus), node["status"].Value<string>());
			}
			if(node["url"] != null)
			{
				this._Url = node["url"].Value<string>();
			}
			if(node["contentId"] != null)
			{
				this._ContentId = node["contentId"].Value<string>();
			}
			if(node["isDefault"] != null)
			{
				this._IsDefault = ParseBool(node["isDefault"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaImage");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("version", this._Version);
			kparams.AddIfNotNull("imageTypeId", this._ImageTypeId);
			kparams.AddIfNotNull("imageObjectId", this._ImageObjectId);
			kparams.AddIfNotNull("imageObjectType", this._ImageObjectType);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("url", this._Url);
			kparams.AddIfNotNull("contentId", this._ContentId);
			kparams.AddIfNotNull("isDefault", this._IsDefault);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case VERSION:
					return "Version";
				case IMAGE_TYPE_ID:
					return "ImageTypeId";
				case IMAGE_OBJECT_ID:
					return "ImageObjectId";
				case IMAGE_OBJECT_TYPE:
					return "ImageObjectType";
				case STATUS:
					return "Status";
				case URL:
					return "Url";
				case CONTENT_ID:
					return "ContentId";
				case IS_DEFAULT:
					return "IsDefault";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

