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
	public class RegistryResponse : ObjectBase
	{
		#region Constants
		public const string ANNOUNCEMENT_ID = "announcementId";
		public const string KEY = "key";
		public const string URL = "url";
		#endregion

		#region Private Fields
		private long _AnnouncementId = long.MinValue;
		private string _Key = null;
		private string _Url = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long AnnouncementId
		{
			get { return _AnnouncementId; }
			set 
			{ 
				_AnnouncementId = value;
				OnPropertyChanged("AnnouncementId");
			}
		}
		[JsonProperty]
		public string Key
		{
			get { return _Key; }
			set 
			{ 
				_Key = value;
				OnPropertyChanged("Key");
			}
		}
		[JsonProperty]
		public string Url
		{
			get { return _Url; }
			set 
			{ 
				_Url = value;
				OnPropertyChanged("Url");
			}
		}
		#endregion

		#region CTor
		public RegistryResponse()
		{
		}

		public RegistryResponse(JToken node) : base(node)
		{
			if(node["announcementId"] != null)
			{
				this._AnnouncementId = ParseLong(node["announcementId"].Value<string>());
			}
			if(node["key"] != null)
			{
				this._Key = node["key"].Value<string>();
			}
			if(node["url"] != null)
			{
				this._Url = node["url"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaRegistryResponse");
			kparams.AddIfNotNull("announcementId", this._AnnouncementId);
			kparams.AddIfNotNull("key", this._Key);
			kparams.AddIfNotNull("url", this._Url);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ANNOUNCEMENT_ID:
					return "AnnouncementId";
				case KEY:
					return "Key";
				case URL:
					return "Url";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

