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
	public class ProductPrice : ObjectBase
	{
		#region Constants
		public const string PRODUCT_ID = "productId";
		public const string PRODUCT_TYPE = "productType";
		public const string PRICE = "price";
		public const string PURCHASE_STATUS = "purchaseStatus";
		#endregion

		#region Private Fields
		private string _ProductId = null;
		private TransactionType _ProductType = null;
		private Price _Price;
		private PurchaseStatus _PurchaseStatus = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string ProductId
		{
			get { return _ProductId; }
			set 
			{ 
				_ProductId = value;
				OnPropertyChanged("ProductId");
			}
		}
		[JsonProperty]
		public TransactionType ProductType
		{
			get { return _ProductType; }
			set 
			{ 
				_ProductType = value;
				OnPropertyChanged("ProductType");
			}
		}
		[JsonProperty]
		public Price Price
		{
			get { return _Price; }
			set 
			{ 
				_Price = value;
				OnPropertyChanged("Price");
			}
		}
		[JsonProperty]
		public PurchaseStatus PurchaseStatus
		{
			get { return _PurchaseStatus; }
			set 
			{ 
				_PurchaseStatus = value;
				OnPropertyChanged("PurchaseStatus");
			}
		}
		#endregion

		#region CTor
		public ProductPrice()
		{
		}

		public ProductPrice(JToken node) : base(node)
		{
			if(node["productId"] != null)
			{
				this._ProductId = node["productId"].Value<string>();
			}
			if(node["productType"] != null)
			{
				this._ProductType = (TransactionType)StringEnum.Parse(typeof(TransactionType), node["productType"].Value<string>());
			}
			if(node["price"] != null)
			{
				this._Price = ObjectFactory.Create<Price>(node["price"]);
			}
			if(node["purchaseStatus"] != null)
			{
				this._PurchaseStatus = (PurchaseStatus)StringEnum.Parse(typeof(PurchaseStatus), node["purchaseStatus"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaProductPrice");
			kparams.AddIfNotNull("productId", this._ProductId);
			kparams.AddIfNotNull("productType", this._ProductType);
			kparams.AddIfNotNull("price", this._Price);
			kparams.AddIfNotNull("purchaseStatus", this._PurchaseStatus);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PRODUCT_ID:
					return "ProductId";
				case PRODUCT_TYPE:
					return "ProductType";
				case PRICE:
					return "Price";
				case PURCHASE_STATUS:
					return "PurchaseStatus";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

