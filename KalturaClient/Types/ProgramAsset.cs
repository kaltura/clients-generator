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
	public class ProgramAsset : Asset
	{
		#region Constants
		public const string EPG_CHANNEL_ID = "epgChannelId";
		public const string EPG_ID = "epgId";
		public const string RELATED_MEDIA_ID = "relatedMediaId";
		public const string CRID = "crid";
		public const string LINEAR_ASSET_ID = "linearAssetId";
		public const string ENABLE_CDVR = "enableCdvr";
		public const string ENABLE_CATCH_UP = "enableCatchUp";
		public const string ENABLE_START_OVER = "enableStartOver";
		public const string ENABLE_TRICK_PLAY = "enableTrickPlay";
		#endregion

		#region Private Fields
		private long _EpgChannelId = long.MinValue;
		private string _EpgId = null;
		private long _RelatedMediaId = long.MinValue;
		private string _Crid = null;
		private long _LinearAssetId = long.MinValue;
		private bool? _EnableCdvr = null;
		private bool? _EnableCatchUp = null;
		private bool? _EnableStartOver = null;
		private bool? _EnableTrickPlay = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long EpgChannelId
		{
			get { return _EpgChannelId; }
			private set 
			{ 
				_EpgChannelId = value;
				OnPropertyChanged("EpgChannelId");
			}
		}
		[JsonProperty]
		public string EpgId
		{
			get { return _EpgId; }
			private set 
			{ 
				_EpgId = value;
				OnPropertyChanged("EpgId");
			}
		}
		[JsonProperty]
		public long RelatedMediaId
		{
			get { return _RelatedMediaId; }
			set 
			{ 
				_RelatedMediaId = value;
				OnPropertyChanged("RelatedMediaId");
			}
		}
		[JsonProperty]
		public string Crid
		{
			get { return _Crid; }
			set 
			{ 
				_Crid = value;
				OnPropertyChanged("Crid");
			}
		}
		[JsonProperty]
		public long LinearAssetId
		{
			get { return _LinearAssetId; }
			set 
			{ 
				_LinearAssetId = value;
				OnPropertyChanged("LinearAssetId");
			}
		}
		[JsonProperty]
		public bool? EnableCdvr
		{
			get { return _EnableCdvr; }
			set 
			{ 
				_EnableCdvr = value;
				OnPropertyChanged("EnableCdvr");
			}
		}
		[JsonProperty]
		public bool? EnableCatchUp
		{
			get { return _EnableCatchUp; }
			set 
			{ 
				_EnableCatchUp = value;
				OnPropertyChanged("EnableCatchUp");
			}
		}
		[JsonProperty]
		public bool? EnableStartOver
		{
			get { return _EnableStartOver; }
			set 
			{ 
				_EnableStartOver = value;
				OnPropertyChanged("EnableStartOver");
			}
		}
		[JsonProperty]
		public bool? EnableTrickPlay
		{
			get { return _EnableTrickPlay; }
			set 
			{ 
				_EnableTrickPlay = value;
				OnPropertyChanged("EnableTrickPlay");
			}
		}
		#endregion

		#region CTor
		public ProgramAsset()
		{
		}

		public ProgramAsset(JToken node) : base(node)
		{
			if(node["epgChannelId"] != null)
			{
				this._EpgChannelId = ParseLong(node["epgChannelId"].Value<string>());
			}
			if(node["epgId"] != null)
			{
				this._EpgId = node["epgId"].Value<string>();
			}
			if(node["relatedMediaId"] != null)
			{
				this._RelatedMediaId = ParseLong(node["relatedMediaId"].Value<string>());
			}
			if(node["crid"] != null)
			{
				this._Crid = node["crid"].Value<string>();
			}
			if(node["linearAssetId"] != null)
			{
				this._LinearAssetId = ParseLong(node["linearAssetId"].Value<string>());
			}
			if(node["enableCdvr"] != null)
			{
				this._EnableCdvr = ParseBool(node["enableCdvr"].Value<string>());
			}
			if(node["enableCatchUp"] != null)
			{
				this._EnableCatchUp = ParseBool(node["enableCatchUp"].Value<string>());
			}
			if(node["enableStartOver"] != null)
			{
				this._EnableStartOver = ParseBool(node["enableStartOver"].Value<string>());
			}
			if(node["enableTrickPlay"] != null)
			{
				this._EnableTrickPlay = ParseBool(node["enableTrickPlay"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaProgramAsset");
			kparams.AddIfNotNull("epgChannelId", this._EpgChannelId);
			kparams.AddIfNotNull("epgId", this._EpgId);
			kparams.AddIfNotNull("relatedMediaId", this._RelatedMediaId);
			kparams.AddIfNotNull("crid", this._Crid);
			kparams.AddIfNotNull("linearAssetId", this._LinearAssetId);
			kparams.AddIfNotNull("enableCdvr", this._EnableCdvr);
			kparams.AddIfNotNull("enableCatchUp", this._EnableCatchUp);
			kparams.AddIfNotNull("enableStartOver", this._EnableStartOver);
			kparams.AddIfNotNull("enableTrickPlay", this._EnableTrickPlay);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case EPG_CHANNEL_ID:
					return "EpgChannelId";
				case EPG_ID:
					return "EpgId";
				case RELATED_MEDIA_ID:
					return "RelatedMediaId";
				case CRID:
					return "Crid";
				case LINEAR_ASSET_ID:
					return "LinearAssetId";
				case ENABLE_CDVR:
					return "EnableCdvr";
				case ENABLE_CATCH_UP:
					return "EnableCatchUp";
				case ENABLE_START_OVER:
					return "EnableStartOver";
				case ENABLE_TRICK_PLAY:
					return "EnableTrickPlay";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

