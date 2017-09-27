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
	public class ProductPriceFilter : Filter
	{
		#region Constants
		public const string SUBSCRIPTION_ID_IN = "subscriptionIdIn";
		public const string FILE_ID_IN = "fileIdIn";
		public const string IS_LOWEST = "isLowest";
		public const string COUPON_CODE_EQUAL = "couponCodeEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _SubscriptionIdIn = null;
		private string _FileIdIn = null;
		private bool? _IsLowest = null;
		private string _CouponCodeEqual = null;
		private ProductPriceOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public string SubscriptionIdIn
		{
			get { return _SubscriptionIdIn; }
			set 
			{ 
				_SubscriptionIdIn = value;
				OnPropertyChanged("SubscriptionIdIn");
			}
		}
		public string FileIdIn
		{
			get { return _FileIdIn; }
			set 
			{ 
				_FileIdIn = value;
				OnPropertyChanged("FileIdIn");
			}
		}
		public bool? IsLowest
		{
			get { return _IsLowest; }
			set 
			{ 
				_IsLowest = value;
				OnPropertyChanged("IsLowest");
			}
		}
		public string CouponCodeEqual
		{
			get { return _CouponCodeEqual; }
			set 
			{ 
				_CouponCodeEqual = value;
				OnPropertyChanged("CouponCodeEqual");
			}
		}
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

		public ProductPriceFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "subscriptionIdIn":
						this._SubscriptionIdIn = propertyNode.InnerText;
						continue;
					case "fileIdIn":
						this._FileIdIn = propertyNode.InnerText;
						continue;
					case "isLowest":
						this._IsLowest = ParseBool(propertyNode.InnerText);
						continue;
					case "couponCodeEqual":
						this._CouponCodeEqual = propertyNode.InnerText;
						continue;
					case "orderBy":
						this._OrderBy = (ProductPriceOrderBy)StringEnum.Parse(typeof(ProductPriceOrderBy), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaProductPriceFilter");
			kparams.AddIfNotNull("subscriptionIdIn", this._SubscriptionIdIn);
			kparams.AddIfNotNull("fileIdIn", this._FileIdIn);
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

