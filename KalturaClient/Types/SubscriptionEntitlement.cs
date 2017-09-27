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
		#endregion

		#region Private Fields
		private long _NextRenewalDate = long.MinValue;
		private bool? _IsRenewableForPurchase = null;
		private bool? _IsRenewable = null;
		private bool? _IsInGracePeriod = null;
		private int _PaymentGatewayId = Int32.MinValue;
		private int _PaymentMethodId = Int32.MinValue;
		private long _ScheduledSubscriptionId = long.MinValue;
		#endregion

		#region Properties
		public long NextRenewalDate
		{
			get { return _NextRenewalDate; }
		}
		public bool? IsRenewableForPurchase
		{
			get { return _IsRenewableForPurchase; }
		}
		public bool? IsRenewable
		{
			get { return _IsRenewable; }
		}
		public bool? IsInGracePeriod
		{
			get { return _IsInGracePeriod; }
		}
		public int PaymentGatewayId
		{
			get { return _PaymentGatewayId; }
			set 
			{ 
				_PaymentGatewayId = value;
				OnPropertyChanged("PaymentGatewayId");
			}
		}
		public int PaymentMethodId
		{
			get { return _PaymentMethodId; }
			set 
			{ 
				_PaymentMethodId = value;
				OnPropertyChanged("PaymentMethodId");
			}
		}
		public long ScheduledSubscriptionId
		{
			get { return _ScheduledSubscriptionId; }
			set 
			{ 
				_ScheduledSubscriptionId = value;
				OnPropertyChanged("ScheduledSubscriptionId");
			}
		}
		#endregion

		#region CTor
		public SubscriptionEntitlement()
		{
		}

		public SubscriptionEntitlement(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "nextRenewalDate":
						this._NextRenewalDate = ParseLong(propertyNode.InnerText);
						continue;
					case "isRenewableForPurchase":
						this._IsRenewableForPurchase = ParseBool(propertyNode.InnerText);
						continue;
					case "isRenewable":
						this._IsRenewable = ParseBool(propertyNode.InnerText);
						continue;
					case "isInGracePeriod":
						this._IsInGracePeriod = ParseBool(propertyNode.InnerText);
						continue;
					case "paymentGatewayId":
						this._PaymentGatewayId = ParseInt(propertyNode.InnerText);
						continue;
					case "paymentMethodId":
						this._PaymentMethodId = ParseInt(propertyNode.InnerText);
						continue;
					case "scheduledSubscriptionId":
						this._ScheduledSubscriptionId = ParseLong(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaSubscriptionEntitlement");
			kparams.AddIfNotNull("nextRenewalDate", this._NextRenewalDate);
			kparams.AddIfNotNull("isRenewableForPurchase", this._IsRenewableForPurchase);
			kparams.AddIfNotNull("isRenewable", this._IsRenewable);
			kparams.AddIfNotNull("isInGracePeriod", this._IsInGracePeriod);
			kparams.AddIfNotNull("paymentGatewayId", this._PaymentGatewayId);
			kparams.AddIfNotNull("paymentMethodId", this._PaymentMethodId);
			kparams.AddIfNotNull("scheduledSubscriptionId", this._ScheduledSubscriptionId);
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
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

