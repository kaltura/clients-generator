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
	public class SubscriptionEntitlement : Entitlement
	{
		#region Constants
		public const string NEXT_RENEWAL_DATE = "nextRenewalDate";
		public const string IS_RENEWABLE_FOR_PURCHASE = "isRenewableForPurchase";
		public const string IS_RENEWABLE = "isRenewable";
		public const string IS_IN_GRACE_PERIOD = "isInGracePeriod";
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string PAYMENT_METHOD_ID = "paymentMethodId";
		public const string SCHEDULED_SUBSCRIPTION_ID = "scheduledSubscriptionId";
		public const string UNIFIED_PAYMENT_ID = "unifiedPaymentId";
		public const string IS_SUSPENDED = "isSuspended";
		public const string PRICE_DETAILS = "priceDetails";
		#endregion

		#region Private Fields
		private long _NextRenewalDate = long.MinValue;
		private bool? _IsRenewableForPurchase = null;
		private bool? _IsRenewable = null;
		private bool? _IsInGracePeriod = null;
		private int _PaymentGatewayId = Int32.MinValue;
		private int _PaymentMethodId = Int32.MinValue;
		private long _ScheduledSubscriptionId = long.MinValue;
		private long _UnifiedPaymentId = long.MinValue;
		private bool? _IsSuspended = null;
		private EntitlementPriceDetails _PriceDetails;
		#endregion

		#region Properties
		[JsonProperty]
		public long NextRenewalDate
		{
			get { return _NextRenewalDate; }
			private set 
			{ 
				_NextRenewalDate = value;
				OnPropertyChanged("NextRenewalDate");
			}
		}
		[JsonProperty]
		public bool? IsRenewableForPurchase
		{
			get { return _IsRenewableForPurchase; }
			private set 
			{ 
				_IsRenewableForPurchase = value;
				OnPropertyChanged("IsRenewableForPurchase");
			}
		}
		[JsonProperty]
		public bool? IsRenewable
		{
			get { return _IsRenewable; }
			private set 
			{ 
				_IsRenewable = value;
				OnPropertyChanged("IsRenewable");
			}
		}
		[JsonProperty]
		public bool? IsInGracePeriod
		{
			get { return _IsInGracePeriod; }
			private set 
			{ 
				_IsInGracePeriod = value;
				OnPropertyChanged("IsInGracePeriod");
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
		public long ScheduledSubscriptionId
		{
			get { return _ScheduledSubscriptionId; }
			private set 
			{ 
				_ScheduledSubscriptionId = value;
				OnPropertyChanged("ScheduledSubscriptionId");
			}
		}
		[JsonProperty]
		public long UnifiedPaymentId
		{
			get { return _UnifiedPaymentId; }
			private set 
			{ 
				_UnifiedPaymentId = value;
				OnPropertyChanged("UnifiedPaymentId");
			}
		}
		[JsonProperty]
		public bool? IsSuspended
		{
			get { return _IsSuspended; }
			private set 
			{ 
				_IsSuspended = value;
				OnPropertyChanged("IsSuspended");
			}
		}
		[JsonProperty]
		public EntitlementPriceDetails PriceDetails
		{
			get { return _PriceDetails; }
			private set 
			{ 
				_PriceDetails = value;
				OnPropertyChanged("PriceDetails");
			}
		}
		#endregion

		#region CTor
		public SubscriptionEntitlement()
		{
		}

		public SubscriptionEntitlement(JToken node) : base(node)
		{
			if(node["nextRenewalDate"] != null)
			{
				this._NextRenewalDate = ParseLong(node["nextRenewalDate"].Value<string>());
			}
			if(node["isRenewableForPurchase"] != null)
			{
				this._IsRenewableForPurchase = ParseBool(node["isRenewableForPurchase"].Value<string>());
			}
			if(node["isRenewable"] != null)
			{
				this._IsRenewable = ParseBool(node["isRenewable"].Value<string>());
			}
			if(node["isInGracePeriod"] != null)
			{
				this._IsInGracePeriod = ParseBool(node["isInGracePeriod"].Value<string>());
			}
			if(node["paymentGatewayId"] != null)
			{
				this._PaymentGatewayId = ParseInt(node["paymentGatewayId"].Value<string>());
			}
			if(node["paymentMethodId"] != null)
			{
				this._PaymentMethodId = ParseInt(node["paymentMethodId"].Value<string>());
			}
			if(node["scheduledSubscriptionId"] != null)
			{
				this._ScheduledSubscriptionId = ParseLong(node["scheduledSubscriptionId"].Value<string>());
			}
			if(node["unifiedPaymentId"] != null)
			{
				this._UnifiedPaymentId = ParseLong(node["unifiedPaymentId"].Value<string>());
			}
			if(node["isSuspended"] != null)
			{
				this._IsSuspended = ParseBool(node["isSuspended"].Value<string>());
			}
			if(node["priceDetails"] != null)
			{
				this._PriceDetails = ObjectFactory.Create<EntitlementPriceDetails>(node["priceDetails"]);
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSubscriptionEntitlement");
			kparams.AddIfNotNull("nextRenewalDate", this._NextRenewalDate);
			kparams.AddIfNotNull("isRenewableForPurchase", this._IsRenewableForPurchase);
			kparams.AddIfNotNull("isRenewable", this._IsRenewable);
			kparams.AddIfNotNull("isInGracePeriod", this._IsInGracePeriod);
			kparams.AddIfNotNull("paymentGatewayId", this._PaymentGatewayId);
			kparams.AddIfNotNull("paymentMethodId", this._PaymentMethodId);
			kparams.AddIfNotNull("scheduledSubscriptionId", this._ScheduledSubscriptionId);
			kparams.AddIfNotNull("unifiedPaymentId", this._UnifiedPaymentId);
			kparams.AddIfNotNull("isSuspended", this._IsSuspended);
			kparams.AddIfNotNull("priceDetails", this._PriceDetails);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NEXT_RENEWAL_DATE:
					return "NextRenewalDate";
				case IS_RENEWABLE_FOR_PURCHASE:
					return "IsRenewableForPurchase";
				case IS_RENEWABLE:
					return "IsRenewable";
				case IS_IN_GRACE_PERIOD:
					return "IsInGracePeriod";
				case PAYMENT_GATEWAY_ID:
					return "PaymentGatewayId";
				case PAYMENT_METHOD_ID:
					return "PaymentMethodId";
				case SCHEDULED_SUBSCRIPTION_ID:
					return "ScheduledSubscriptionId";
				case UNIFIED_PAYMENT_ID:
					return "UnifiedPaymentId";
				case IS_SUSPENDED:
					return "IsSuspended";
				case PRICE_DETAILS:
					return "PriceDetails";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

