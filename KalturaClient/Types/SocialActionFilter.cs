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
	public class SocialActionFilter : Filter
	{
		#region Constants
		public const string ASSET_ID_IN = "assetIdIn";
		public const string ASSET_TYPE_EQUAL = "assetTypeEqual";
		public const string ACTION_TYPE_IN = "actionTypeIn";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _AssetIdIn = null;
		private AssetType _AssetTypeEqual = null;
		private string _ActionTypeIn = null;
		private SocialActionOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string AssetIdIn
		{
			get { return _AssetIdIn; }
			set 
			{ 
				_AssetIdIn = value;
				OnPropertyChanged("AssetIdIn");
			}
		}
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
		public string ActionTypeIn
		{
			get { return _ActionTypeIn; }
			set 
			{ 
				_ActionTypeIn = value;
				OnPropertyChanged("ActionTypeIn");
			}
		}
		[JsonProperty]
		public new SocialActionOrderBy OrderBy
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
		public SocialActionFilter()
		{
		}

		public SocialActionFilter(JToken node) : base(node)
		{
			if(node["assetIdIn"] != null)
			{
				this._AssetIdIn = node["assetIdIn"].Value<string>();
			}
			if(node["assetTypeEqual"] != null)
			{
				this._AssetTypeEqual = (AssetType)StringEnum.Parse(typeof(AssetType), node["assetTypeEqual"].Value<string>());
			}
			if(node["actionTypeIn"] != null)
			{
				this._ActionTypeIn = node["actionTypeIn"].Value<string>();
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (SocialActionOrderBy)StringEnum.Parse(typeof(SocialActionOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSocialActionFilter");
			kparams.AddIfNotNull("assetIdIn", this._AssetIdIn);
			kparams.AddIfNotNull("assetTypeEqual", this._AssetTypeEqual);
			kparams.AddIfNotNull("actionTypeIn", this._ActionTypeIn);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_ID_IN:
					return "AssetIdIn";
				case ASSET_TYPE_EQUAL:
					return "AssetTypeEqual";
				case ACTION_TYPE_IN:
					return "ActionTypeIn";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

