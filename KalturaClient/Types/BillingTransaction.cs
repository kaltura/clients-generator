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
// Copyright (C) 2006-2018  Kaltura Inc.
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
	public class BillingTransaction : ObjectBase
	{
		#region Constants
		public const string RECIEPT_CODE = "recieptCode";
		public const string PURCHASED_ITEM_NAME = "purchasedItemName";
		public const string PURCHASED_ITEM_CODE = "purchasedItemCode";
		public const string ITEM_TYPE = "itemType";
		public const string BILLING_ACTION = "billingAction";
		public const string PRICE = "price";
		public const string ACTION_DATE = "actionDate";
		public const string START_DATE = "startDate";
		public const string END_DATE = "endDate";
		public const string PAYMENT_METHOD = "paymentMethod";
		public const string PAYMENT_METHOD_EXTRA_DETAILS = "paymentMethodExtraDetails";
		public const string IS_RECURRING = "isRecurring";
		public const string BILLING_PROVIDER_REF = "billingProviderRef";
		public const string PURCHASE_ID = "purchaseId";
		public const string REMARKS = "remarks";
		public const string BILLING_PRICE_TYPE = "billingPriceType";
		#endregion

		#region Private Fields
		private string _RecieptCode = null;
		private string _PurchasedItemName = null;
		private string _PurchasedItemCode = null;
		private BillingItemsType _ItemType = null;
		private BillingAction _BillingAction = null;
		private Price _Price;
		private long _ActionDate = long.MinValue;
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		private PaymentMethodType _PaymentMethod = null;
		private string _PaymentMethodExtraDetails = null;
		private bool? _IsRecurring = null;
		private int _BillingProviderRef = Int32.MinValue;
		private int _PurchaseId = Int32.MinValue;
		private string _Remarks = null;
		private BillingPriceType _BillingPriceType = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string RecieptCode
		{
			get { return _RecieptCode; }
			private set 
			{ 
				_RecieptCode = value;
				OnPropertyChanged("RecieptCode");
			}
		}
		[JsonProperty]
		public string PurchasedItemName
		{
			get { return _PurchasedItemName; }
			private set 
			{ 
				_PurchasedItemName = value;
				OnPropertyChanged("PurchasedItemName");
			}
		}
		[JsonProperty]
		public string PurchasedItemCode
		{
			get { return _PurchasedItemCode; }
			private set 
			{ 
				_PurchasedItemCode = value;
				OnPropertyChanged("PurchasedItemCode");
			}
		}
		[JsonProperty]
		public BillingItemsType ItemType
		{
			get { return _ItemType; }
			private set 
			{ 
				_ItemType = value;
				OnPropertyChanged("ItemType");
			}
		}
		[JsonProperty]
		public BillingAction BillingAction
		{
			get { return _BillingAction; }
			private set 
			{ 
				_BillingAction = value;
				OnPropertyChanged("BillingAction");
			}
		}
		[JsonProperty]
		public Price Price
		{
			get { return _Price; }
			private set 
			{ 
				_Price = value;
				OnPropertyChanged("Price");
			}
		}
		[JsonProperty]
		public long ActionDate
		{
			get { return _ActionDate; }
			private set 
			{ 
				_ActionDate = value;
				OnPropertyChanged("ActionDate");
			}
		}
		[JsonProperty]
		public long StartDate
		{
			get { return _StartDate; }
			private set 
			{ 
				_StartDate = value;
				OnPropertyChanged("StartDate");
			}
		}
		[JsonProperty]
		public long EndDate
		{
			get { return _EndDate; }
			private set 
			{ 
				_EndDate = value;
				OnPropertyChanged("EndDate");
			}
		}
		[JsonProperty]
		public PaymentMethodType PaymentMethod
		{
			get { return _PaymentMethod; }
			private set 
			{ 
				_PaymentMethod = value;
				OnPropertyChanged("PaymentMethod");
			}
		}
		[JsonProperty]
		public string PaymentMethodExtraDetails
		{
			get { return _PaymentMethodExtraDetails; }
			private set 
			{ 
				_PaymentMethodExtraDetails = value;
				OnPropertyChanged("PaymentMethodExtraDetails");
			}
		}
		[JsonProperty]
		public bool? IsRecurring
		{
			get { return _IsRecurring; }
			private set 
			{ 
				_IsRecurring = value;
				OnPropertyChanged("IsRecurring");
			}
		}
		[JsonProperty]
		public int BillingProviderRef
		{
			get { return _BillingProviderRef; }
			private set 
			{ 
				_BillingProviderRef = value;
				OnPropertyChanged("BillingProviderRef");
			}
		}
		[JsonProperty]
		public int PurchaseId
		{
			get { return _PurchaseId; }
			private set 
			{ 
				_PurchaseId = value;
				OnPropertyChanged("PurchaseId");
			}
		}
		[JsonProperty]
		public string Remarks
		{
			get { return _Remarks; }
			private set 
			{ 
				_Remarks = value;
				OnPropertyChanged("Remarks");
			}
		}
		[JsonProperty]
		public BillingPriceType BillingPriceType
		{
			get { return _BillingPriceType; }
			private set 
			{ 
				_BillingPriceType = value;
				OnPropertyChanged("BillingPriceType");
			}
		}
		#endregion

		#region CTor
		public BillingTransaction()
		{
		}

		public BillingTransaction(JToken node) : base(node)
		{
			if(node["recieptCode"] != null)
			{
				this._RecieptCode = node["recieptCode"].Value<string>();
			}
			if(node["purchasedItemName"] != null)
			{
				this._PurchasedItemName = node["purchasedItemName"].Value<string>();
			}
			if(node["purchasedItemCode"] != null)
			{
				this._PurchasedItemCode = node["purchasedItemCode"].Value<string>();
			}
			if(node["itemType"] != null)
			{
				this._ItemType = (BillingItemsType)StringEnum.Parse(typeof(BillingItemsType), node["itemType"].Value<string>());
			}
			if(node["billingAction"] != null)
			{
				this._BillingAction = (BillingAction)StringEnum.Parse(typeof(BillingAction), node["billingAction"].Value<string>());
			}
			if(node["price"] != null)
			{
				this._Price = ObjectFactory.Create<Price>(node["price"]);
			}
			if(node["actionDate"] != null)
			{
				this._ActionDate = ParseLong(node["actionDate"].Value<string>());
			}
			if(node["startDate"] != null)
			{
				this._StartDate = ParseLong(node["startDate"].Value<string>());
			}
			if(node["endDate"] != null)
			{
				this._EndDate = ParseLong(node["endDate"].Value<string>());
			}
			if(node["paymentMethod"] != null)
			{
				this._PaymentMethod = (PaymentMethodType)StringEnum.Parse(typeof(PaymentMethodType), node["paymentMethod"].Value<string>());
			}
			if(node["paymentMethodExtraDetails"] != null)
			{
				this._PaymentMethodExtraDetails = node["paymentMethodExtraDetails"].Value<string>();
			}
			if(node["isRecurring"] != null)
			{
				this._IsRecurring = ParseBool(node["isRecurring"].Value<string>());
			}
			if(node["billingProviderRef"] != null)
			{
				this._BillingProviderRef = ParseInt(node["billingProviderRef"].Value<string>());
			}
			if(node["purchaseId"] != null)
			{
				this._PurchaseId = ParseInt(node["purchaseId"].Value<string>());
			}
			if(node["remarks"] != null)
			{
				this._Remarks = node["remarks"].Value<string>();
			}
			if(node["billingPriceType"] != null)
			{
				this._BillingPriceType = (BillingPriceType)StringEnum.Parse(typeof(BillingPriceType), node["billingPriceType"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBillingTransaction");
			kparams.AddIfNotNull("recieptCode", this._RecieptCode);
			kparams.AddIfNotNull("purchasedItemName", this._PurchasedItemName);
			kparams.AddIfNotNull("purchasedItemCode", this._PurchasedItemCode);
			kparams.AddIfNotNull("itemType", this._ItemType);
			kparams.AddIfNotNull("billingAction", this._BillingAction);
			kparams.AddIfNotNull("price", this._Price);
			kparams.AddIfNotNull("actionDate", this._ActionDate);
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("paymentMethod", this._PaymentMethod);
			kparams.AddIfNotNull("paymentMethodExtraDetails", this._PaymentMethodExtraDetails);
			kparams.AddIfNotNull("isRecurring", this._IsRecurring);
			kparams.AddIfNotNull("billingProviderRef", this._BillingProviderRef);
			kparams.AddIfNotNull("purchaseId", this._PurchaseId);
			kparams.AddIfNotNull("remarks", this._Remarks);
			kparams.AddIfNotNull("billingPriceType", this._BillingPriceType);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case RECIEPT_CODE:
					return "RecieptCode";
				case PURCHASED_ITEM_NAME:
					return "PurchasedItemName";
				case PURCHASED_ITEM_CODE:
					return "PurchasedItemCode";
				case ITEM_TYPE:
					return "ItemType";
				case BILLING_ACTION:
					return "BillingAction";
				case PRICE:
					return "Price";
				case ACTION_DATE:
					return "ActionDate";
				case START_DATE:
					return "StartDate";
				case END_DATE:
					return "EndDate";
				case PAYMENT_METHOD:
					return "PaymentMethod";
				case PAYMENT_METHOD_EXTRA_DETAILS:
					return "PaymentMethodExtraDetails";
				case IS_RECURRING:
					return "IsRecurring";
				case BILLING_PROVIDER_REF:
					return "BillingProviderRef";
				case PURCHASE_ID:
					return "PurchaseId";
				case REMARKS:
					return "Remarks";
				case BILLING_PRICE_TYPE:
					return "BillingPriceType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

