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
	public class PlaybackContextOptions : ObjectBase
	{
		#region Constants
		public const string MEDIA_PROTOCOL = "mediaProtocol";
		public const string STREAMER_TYPE = "streamerType";
		public const string ASSET_FILE_IDS = "assetFileIds";
		public const string ADAPTER_DATA = "adapterData";
		public const string CONTEXT = "context";
		public const string URL_TYPE = "urlType";
		#endregion

		#region Private Fields
		private string _MediaProtocol = null;
		private string _StreamerType = null;
		private string _AssetFileIds = null;
		private IDictionary<string, StringValue> _AdapterData;
		private PlaybackContextType _Context = null;
		private UrlType _UrlType = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string MediaProtocol
		{
			get { return _MediaProtocol; }
			set 
			{ 
				_MediaProtocol = value;
				OnPropertyChanged("MediaProtocol");
			}
		}
		[JsonProperty]
		public string StreamerType
		{
			get { return _StreamerType; }
			set 
			{ 
				_StreamerType = value;
				OnPropertyChanged("StreamerType");
			}
		}
		[JsonProperty]
		public string AssetFileIds
		{
			get { return _AssetFileIds; }
			set 
			{ 
				_AssetFileIds = value;
				OnPropertyChanged("AssetFileIds");
			}
		}
		[JsonProperty]
		public IDictionary<string, StringValue> AdapterData
		{
			get { return _AdapterData; }
			set 
			{ 
				_AdapterData = value;
				OnPropertyChanged("AdapterData");
			}
		}
		[JsonProperty]
		public PlaybackContextType Context
		{
			get { return _Context; }
			set 
			{ 
				_Context = value;
				OnPropertyChanged("Context");
			}
		}
		[JsonProperty]
		public UrlType UrlType
		{
			get { return _UrlType; }
			set 
			{ 
				_UrlType = value;
				OnPropertyChanged("UrlType");
			}
		}
		#endregion

		#region CTor
		public PlaybackContextOptions()
		{
		}

		public PlaybackContextOptions(JToken node) : base(node)
		{
			if(node["mediaProtocol"] != null)
			{
				this._MediaProtocol = node["mediaProtocol"].Value<string>();
			}
			if(node["streamerType"] != null)
			{
				this._StreamerType = node["streamerType"].Value<string>();
			}
			if(node["assetFileIds"] != null)
			{
				this._AssetFileIds = node["assetFileIds"].Value<string>();
			}
			if(node["adapterData"] != null)
			{
				{
					string key;
					this._AdapterData = new Dictionary<string, StringValue>();
					foreach(var arrayNode in node["adapterData"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._AdapterData[key] = ObjectFactory.Create<StringValue>(arrayNode.Value);
					}
				}
			}
			if(node["context"] != null)
			{
				this._Context = (PlaybackContextType)StringEnum.Parse(typeof(PlaybackContextType), node["context"].Value<string>());
			}
			if(node["urlType"] != null)
			{
				this._UrlType = (UrlType)StringEnum.Parse(typeof(UrlType), node["urlType"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPlaybackContextOptions");
			kparams.AddIfNotNull("mediaProtocol", this._MediaProtocol);
			kparams.AddIfNotNull("streamerType", this._StreamerType);
			kparams.AddIfNotNull("assetFileIds", this._AssetFileIds);
			kparams.AddIfNotNull("adapterData", this._AdapterData);
			kparams.AddIfNotNull("context", this._Context);
			kparams.AddIfNotNull("urlType", this._UrlType);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case MEDIA_PROTOCOL:
					return "MediaProtocol";
				case STREAMER_TYPE:
					return "StreamerType";
				case ASSET_FILE_IDS:
					return "AssetFileIds";
				case ADAPTER_DATA:
					return "AdapterData";
				case CONTEXT:
					return "Context";
				case URL_TYPE:
					return "UrlType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

