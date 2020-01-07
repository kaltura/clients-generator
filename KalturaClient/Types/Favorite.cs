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
	public class Favorite : ObjectBase
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string EXTRA_DATA = "extraData";
		public const string CREATE_DATE = "createDate";
		#endregion

		#region Private Fields
		private long _AssetId = long.MinValue;
		private string _ExtraData = null;
		private long _CreateDate = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public long AssetId
		{
			get { return _AssetId; }
			set 
			{ 
				_AssetId = value;
				OnPropertyChanged("AssetId");
			}
		}
		[JsonProperty]
		public string ExtraData
		{
			get { return _ExtraData; }
			set 
			{ 
				_ExtraData = value;
				OnPropertyChanged("ExtraData");
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
		#endregion

		#region CTor
		public Favorite()
		{
		}

		public Favorite(JToken node) : base(node)
		{
			if(node["assetId"] != null)
			{
				this._AssetId = ParseLong(node["assetId"].Value<string>());
			}
			if(node["extraData"] != null)
			{
				this._ExtraData = node["extraData"].Value<string>();
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaFavorite");
			kparams.AddIfNotNull("assetId", this._AssetId);
			kparams.AddIfNotNull("extraData", this._ExtraData);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_ID:
					return "AssetId";
				case EXTRA_DATA:
					return "ExtraData";
				case CREATE_DATE:
					return "CreateDate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

