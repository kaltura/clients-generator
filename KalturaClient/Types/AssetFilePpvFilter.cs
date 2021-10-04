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
	public class AssetFilePpvFilter : Filter
	{
		#region Constants
		public const string ASSET_ID_EQUAL = "assetIdEqual";
		public const string ASSET_FILE_ID_EQUAL = "assetFileIdEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private long _AssetIdEqual = long.MinValue;
		private long _AssetFileIdEqual = long.MinValue;
		private AssetFilePpvOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long AssetIdEqual
		{
			get { return _AssetIdEqual; }
			set 
			{ 
				_AssetIdEqual = value;
				OnPropertyChanged("AssetIdEqual");
			}
		}
		[JsonProperty]
		public long AssetFileIdEqual
		{
			get { return _AssetFileIdEqual; }
			set 
			{ 
				_AssetFileIdEqual = value;
				OnPropertyChanged("AssetFileIdEqual");
			}
		}
		[JsonProperty]
		public new AssetFilePpvOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		#endregion

		#region CTor
		public AssetFilePpvFilter()
		{
		}

		public AssetFilePpvFilter(JToken node) : base(node)
		{
			if(node["assetIdEqual"] != null)
			{
				this._AssetIdEqual = ParseLong(node["assetIdEqual"].Value<string>());
			}
			if(node["assetFileIdEqual"] != null)
			{
				this._AssetFileIdEqual = ParseLong(node["assetFileIdEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (AssetFilePpvOrderBy)StringEnum.Parse(typeof(AssetFilePpvOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetFilePpvFilter");
			kparams.AddIfNotNull("assetIdEqual", this._AssetIdEqual);
			kparams.AddIfNotNull("assetFileIdEqual", this._AssetFileIdEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_ID_EQUAL:
					return "AssetIdEqual";
				case ASSET_FILE_ID_EQUAL:
					return "AssetFileIdEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

