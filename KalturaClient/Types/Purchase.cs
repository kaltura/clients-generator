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
	public class Purchase : PurchaseBase
	{
		#region Constants
		public const string CURRENCY = "currency";
		public const string PRICE = "price";
		public const string PAYMENT_METHOD_ID = "paymentMethodId";
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string COUPON = "coupon";
		#endregion

		#region Private Fields
		private string _Currency = null;
		private float _Price = Single.MinValue;
		private int _PaymentMethodId = Int32.MinValue;
		private int _PaymentGatewayId = Int32.MinValue;
		private string _Coupon = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string Currency
		{
			get { return _Currency; }
			set 
			{ 
				_Currency = value;
				OnPropertyChanged("Currency");
			}
		}
		[JsonProperty]
		public float Price
		{
			get { return _Price; }
			set 
			{ 
				_Price = value;
				OnPropertyChanged("Price");
			}
		}
		[JsonProperty]
		public int PaymentMethodId
		{
			get { return _PaymentMethodId; }
			set 
			{ 
				_PaymentMethodId = value;
				OnPropertyChanged("PaymentMethodId");
			}
		}
		[JsonProperty]
		public int PaymentGatewayId
		{
			get { return _PaymentGatewayId; }
			set 
			{ 
				_PaymentGatewayId = value;
				OnPropertyChanged("PaymentGatewayId");
			}
		}
		[JsonProperty]
		public string Coupon
		{
			get { return _Coupon; }
			set 
			{ 
				_Coupon = value;
				OnPropertyChanged("Coupon");
			}
		}
		#endregion

		#region CTor
		public Purchase()
		{
		}

		public Purchase(JToken node) : base(node)
		{
			if(node["currency"] != null)
			{
				this._Currency = node["currency"].Value<string>();
			}
			if(node["price"] != null)
			{
				this._Price = ParseFloat(node["price"].Value<string>());
			}
			if(node["paymentMethodId"] != null)
			{
				this._PaymentMethodId = ParseInt(node["paymentMethodId"].Value<string>());
			}
			if(node["paymentGatewayId"] != null)
			{
				this._PaymentGatewayId = ParseInt(node["paymentGatewayId"].Value<string>());
			}
			if(node["coupon"] != null)
			{
				this._Coupon = node["coupon"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPurchase");
			kparams.AddIfNotNull("currency", this._Currency);
			kparams.AddIfNotNull("price", this._Price);
			kparams.AddIfNotNull("paymentMethodId", this._PaymentMethodId);
			kparams.AddIfNotNull("paymentGatewayId", this._PaymentGatewayId);
			kparams.AddIfNotNull("coupon", this._Coupon);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CURRENCY:
					return "Currency";
				case PRICE:
					return "Price";
				case PAYMENT_METHOD_ID:
					return "PaymentMethodId";
				case PAYMENT_GATEWAY_ID:
					return "PaymentGatewayId";
				case COUPON:
					return "Coupon";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

