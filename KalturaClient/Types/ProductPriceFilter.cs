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
	public class ProductPriceFilter : Filter
	{
		#region Constants
		public const string SUBSCRIPTION_ID_IN = "subscriptionIdIn";
		public const string FILE_ID_IN = "fileIdIn";
		public const string COLLECTION_ID_IN = "collectionIdIn";
		public const string IS_LOWEST = "isLowest";
		public const string COUPON_CODE_EQUAL = "couponCodeEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _SubscriptionIdIn = null;
		private string _FileIdIn = null;
		private string _CollectionIdIn = null;
		private bool? _IsLowest = null;
		private string _CouponCodeEqual = null;
		private ProductPriceOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string SubscriptionIdIn
		{
			get { return _SubscriptionIdIn; }
			set 
			{ 
				_SubscriptionIdIn = value;
				OnPropertyChanged("SubscriptionIdIn");
			}
		}
		[JsonProperty]
		public string FileIdIn
		{
			get { return _FileIdIn; }
			set 
			{ 
				_FileIdIn = value;
				OnPropertyChanged("FileIdIn");
			}
		}
		[JsonProperty]
		public string CollectionIdIn
		{
			get { return _CollectionIdIn; }
			set 
			{ 
				_CollectionIdIn = value;
				OnPropertyChanged("CollectionIdIn");
			}
		}
		[JsonProperty]
		public bool? IsLowest
		{
			get { return _IsLowest; }
			set 
			{ 
				_IsLowest = value;
				OnPropertyChanged("IsLowest");
			}
		}
		[JsonProperty]
		public string CouponCodeEqual
		{
			get { return _CouponCodeEqual; }
			set 
			{ 
				_CouponCodeEqual = value;
				OnPropertyChanged("CouponCodeEqual");
			}
		}
		[JsonProperty]
		public new ProductPriceOrderBy OrderBy
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
		public ProductPriceFilter()
		{
		}

		public ProductPriceFilter(JToken node) : base(node)
		{
			if(node["subscriptionIdIn"] != null)
			{
				this._SubscriptionIdIn = node["subscriptionIdIn"].Value<string>();
			}
			if(node["fileIdIn"] != null)
			{
				this._FileIdIn = node["fileIdIn"].Value<string>();
			}
			if(node["collectionIdIn"] != null)
			{
				this._CollectionIdIn = node["collectionIdIn"].Value<string>();
			}
			if(node["isLowest"] != null)
			{
				this._IsLowest = ParseBool(node["isLowest"].Value<string>());
			}
			if(node["couponCodeEqual"] != null)
			{
				this._CouponCodeEqual = node["couponCodeEqual"].Value<string>();
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (ProductPriceOrderBy)StringEnum.Parse(typeof(ProductPriceOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaProductPriceFilter");
			kparams.AddIfNotNull("subscriptionIdIn", this._SubscriptionIdIn);
			kparams.AddIfNotNull("fileIdIn", this._FileIdIn);
			kparams.AddIfNotNull("collectionIdIn", this._CollectionIdIn);
			kparams.AddIfNotNull("isLowest", this._IsLowest);
			kparams.AddIfNotNull("couponCodeEqual", this._CouponCodeEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SUBSCRIPTION_ID_IN:
					return "SubscriptionIdIn";
				case FILE_ID_IN:
					return "FileIdIn";
				case COLLECTION_ID_IN:
					return "CollectionIdIn";
				case IS_LOWEST:
					return "IsLowest";
				case COUPON_CODE_EQUAL:
					return "CouponCodeEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

