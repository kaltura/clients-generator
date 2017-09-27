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
// Copyright (C) 2006-2017  Kaltura Inc.
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
	public class PlaybackContextOptions : ObjectBase
	{
		#region Constants
		public const string MEDIA_PROTOCOL = "mediaProtocol";
		public const string STREAMER_TYPE = "streamerType";
		public const string ASSET_FILE_IDS = "assetFileIds";
		public const string CONTEXT = "context";
		#endregion

		#region Private Fields
		private string _MediaProtocol = null;
		private string _StreamerType = null;
		private string _AssetFileIds = null;
		private PlaybackContextType _Context = null;
		#endregion

		#region Properties
		public string MediaProtocol
		{
			get { return _MediaProtocol; }
			set 
			{ 
				_MediaProtocol = value;
				OnPropertyChanged("MediaProtocol");
			}
		}
		public string StreamerType
		{
			get { return _StreamerType; }
			set 
			{ 
				_StreamerType = value;
				OnPropertyChanged("StreamerType");
			}
		}
		public string AssetFileIds
		{
			get { return _AssetFileIds; }
			set 
			{ 
				_AssetFileIds = value;
				OnPropertyChanged("AssetFileIds");
			}
		}
		public PlaybackContextType Context
		{
			get { return _Context; }
			set 
			{ 
				_Context = value;
				OnPropertyChanged("Context");
			}
		}
		#endregion

		#region CTor
		public PlaybackContextOptions()
		{
		}

		public PlaybackContextOptions(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "mediaProtocol":
						this._MediaProtocol = propertyNode.InnerText;
						continue;
					case "streamerType":
						this._StreamerType = propertyNode.InnerText;
						continue;
					case "assetFileIds":
						this._AssetFileIds = propertyNode.InnerText;
						continue;
					case "context":
						this._Context = (PlaybackContextType)StringEnum.Parse(typeof(PlaybackContextType), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaPlaybackContextOptions");
			kparams.AddIfNotNull("mediaProtocol", this._MediaProtocol);
			kparams.AddIfNotNull("streamerType", this._StreamerType);
			kparams.AddIfNotNull("assetFileIds", this._AssetFileIds);
			kparams.AddIfNotNull("context", this._Context);
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
				case CONTEXT:
					return "Context";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

