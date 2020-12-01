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
	public class StreamingDeviceFilter : Filter
	{
		#region Constants
		public const string ASSET_TYPE_EQUAL = "assetTypeEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private AssetType _AssetTypeEqual = null;
		private StreamingDeviceOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public AssetType AssetTypeEqual
		{
			get { return _AssetTypeEqual; }
			set 
			{ 
				_AssetTypeEqual = value;
				OnPropertyChanged("AssetTypeEqual");
			}
		}
		[JsonProperty]
		public new StreamingDeviceOrderBy OrderBy
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
		public StreamingDeviceFilter()
		{
		}

		public StreamingDeviceFilter(JToken node) : base(node)
		{
			if(node["assetTypeEqual"] != null)
			{
				this._AssetTypeEqual = (AssetType)StringEnum.Parse(typeof(AssetType), node["assetTypeEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (StreamingDeviceOrderBy)StringEnum.Parse(typeof(StreamingDeviceOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaStreamingDeviceFilter");
			kparams.AddIfNotNull("assetTypeEqual", this._AssetTypeEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_TYPE_EQUAL:
					return "AssetTypeEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

