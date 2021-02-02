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
	public class PlaybackSource : MediaFile
	{
		#region Constants
		public const string FORMAT = "format";
		public const string PROTOCOLS = "protocols";
		public const string DRM = "drm";
		public const string IS_TOKENIZED = "isTokenized";
		#endregion

		#region Private Fields
		private string _Format = null;
		private string _Protocols = null;
		private IList<DrmPlaybackPluginData> _Drm;
		private bool? _IsTokenized = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string Format
		{
			get { return _Format; }
			set 
			{ 
				_Format = value;
				OnPropertyChanged("Format");
			}
		}
		[JsonProperty]
		public string Protocols
		{
			get { return _Protocols; }
			set 
			{ 
				_Protocols = value;
				OnPropertyChanged("Protocols");
			}
		}
		[JsonProperty]
		public IList<DrmPlaybackPluginData> Drm
		{
			get { return _Drm; }
			set 
			{ 
				_Drm = value;
				OnPropertyChanged("Drm");
			}
		}
		[JsonProperty]
		public bool? IsTokenized
		{
			get { return _IsTokenized; }
			set 
			{ 
				_IsTokenized = value;
				OnPropertyChanged("IsTokenized");
			}
		}
		#endregion

		#region CTor
		public PlaybackSource()
		{
		}

		public PlaybackSource(JToken node) : base(node)
		{
			if(node["format"] != null)
			{
				this._Format = node["format"].Value<string>();
			}
			if(node["protocols"] != null)
			{
				this._Protocols = node["protocols"].Value<string>();
			}
			if(node["drm"] != null)
			{
				this._Drm = new List<DrmPlaybackPluginData>();
				foreach(var arrayNode in node["drm"].Children())
				{
					this._Drm.Add(ObjectFactory.Create<DrmPlaybackPluginData>(arrayNode));
				}
			}
			if(node["isTokenized"] != null)
			{
				this._IsTokenized = ParseBool(node["isTokenized"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPlaybackSource");
			kparams.AddIfNotNull("format", this._Format);
			kparams.AddIfNotNull("protocols", this._Protocols);
			kparams.AddIfNotNull("drm", this._Drm);
			kparams.AddIfNotNull("isTokenized", this._IsTokenized);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case FORMAT:
					return "Format";
				case PROTOCOLS:
					return "Protocols";
				case DRM:
					return "Drm";
				case IS_TOKENIZED:
					return "IsTokenized";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

