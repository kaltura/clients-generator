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
	public class AssetStructMetaFilter : Filter
	{
		#region Constants
		public const string ASSET_STRUCT_ID_EQUAL = "assetStructIdEqual";
		public const string META_ID_EQUAL = "metaIdEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private long _AssetStructIdEqual = long.MinValue;
		private long _MetaIdEqual = long.MinValue;
		private AssetStructMetaOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long AssetStructIdEqual
		{
			get { return _AssetStructIdEqual; }
			set 
			{ 
				_AssetStructIdEqual = value;
				OnPropertyChanged("AssetStructIdEqual");
			}
		}
		[JsonProperty]
		public long MetaIdEqual
		{
			get { return _MetaIdEqual; }
			set 
			{ 
				_MetaIdEqual = value;
				OnPropertyChanged("MetaIdEqual");
			}
		}
		[JsonProperty]
		public new AssetStructMetaOrderBy OrderBy
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
		public AssetStructMetaFilter()
		{
		}

		public AssetStructMetaFilter(JToken node) : base(node)
		{
			if(node["assetStructIdEqual"] != null)
			{
				this._AssetStructIdEqual = ParseLong(node["assetStructIdEqual"].Value<string>());
			}
			if(node["metaIdEqual"] != null)
			{
				this._MetaIdEqual = ParseLong(node["metaIdEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (AssetStructMetaOrderBy)StringEnum.Parse(typeof(AssetStructMetaOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetStructMetaFilter");
			kparams.AddIfNotNull("assetStructIdEqual", this._AssetStructIdEqual);
			kparams.AddIfNotNull("metaIdEqual", this._MetaIdEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_STRUCT_ID_EQUAL:
					return "AssetStructIdEqual";
				case META_ID_EQUAL:
					return "MetaIdEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

