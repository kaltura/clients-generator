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
	public class SocialCommentFilter : Filter
	{
		#region Constants
		public const string ASSET_ID_EQUAL = "assetIdEqual";
		public const string ASSET_TYPE_EQUAL = "assetTypeEqual";
		public const string SOCIAL_PLATFORM_EQUAL = "socialPlatformEqual";
		public const string CREATE_DATE_GREATER_THAN = "createDateGreaterThan";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private long _AssetIdEqual = long.MinValue;
		private AssetType _AssetTypeEqual = null;
		private SocialPlatform _SocialPlatformEqual = null;
		private long _CreateDateGreaterThan = long.MinValue;
		private SocialCommentOrderBy _OrderBy = null;
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
		public SocialPlatform SocialPlatformEqual
		{
			get { return _SocialPlatformEqual; }
			set 
			{ 
				_SocialPlatformEqual = value;
				OnPropertyChanged("SocialPlatformEqual");
			}
		}
		[JsonProperty]
		public long CreateDateGreaterThan
		{
			get { return _CreateDateGreaterThan; }
			set 
			{ 
				_CreateDateGreaterThan = value;
				OnPropertyChanged("CreateDateGreaterThan");
			}
		}
		[JsonProperty]
		public new SocialCommentOrderBy OrderBy
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
		public SocialCommentFilter()
		{
		}

		public SocialCommentFilter(JToken node) : base(node)
		{
			if(node["assetIdEqual"] != null)
			{
				this._AssetIdEqual = ParseLong(node["assetIdEqual"].Value<string>());
			}
			if(node["assetTypeEqual"] != null)
			{
				this._AssetTypeEqual = (AssetType)StringEnum.Parse(typeof(AssetType), node["assetTypeEqual"].Value<string>());
			}
			if(node["socialPlatformEqual"] != null)
			{
				this._SocialPlatformEqual = (SocialPlatform)StringEnum.Parse(typeof(SocialPlatform), node["socialPlatformEqual"].Value<string>());
			}
			if(node["createDateGreaterThan"] != null)
			{
				this._CreateDateGreaterThan = ParseLong(node["createDateGreaterThan"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (SocialCommentOrderBy)StringEnum.Parse(typeof(SocialCommentOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSocialCommentFilter");
			kparams.AddIfNotNull("assetIdEqual", this._AssetIdEqual);
			kparams.AddIfNotNull("assetTypeEqual", this._AssetTypeEqual);
			kparams.AddIfNotNull("socialPlatformEqual", this._SocialPlatformEqual);
			kparams.AddIfNotNull("createDateGreaterThan", this._CreateDateGreaterThan);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_ID_EQUAL:
					return "AssetIdEqual";
				case ASSET_TYPE_EQUAL:
					return "AssetTypeEqual";
				case SOCIAL_PLATFORM_EQUAL:
					return "SocialPlatformEqual";
				case CREATE_DATE_GREATER_THAN:
					return "CreateDateGreaterThan";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

