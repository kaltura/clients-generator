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

namespace Kaltura.Types
{
	public class EntitlementRenewal : ObjectBase
	{
		#region Constants
		public const string PRICE = "price";
		public const string DATE = "date";
		public const string PURCHASE_ID = "purchaseId";
		public const string SUBSCRIPTION_ID = "subscriptionId";
		#endregion

		#region Private Fields
		private Price _Price;
		private long _Date = long.MinValue;
		private long _PurchaseId = long.MinValue;
		private long _SubscriptionId = long.MinValue;
		#endregion

		#region Properties
		public Price Price
		{
			get { return _Price; }
			set 
			{ 
				_Price = value;
				OnPropertyChanged("Price");
			}
		}
		public long Date
		{
			get { return _Date; }
			set 
			{ 
				_Date = value;
				OnPropertyChanged("Date");
			}
		}
		public long PurchaseId
		{
			get { return _PurchaseId; }
			set 
			{ 
				_PurchaseId = value;
				OnPropertyChanged("PurchaseId");
			}
		}
		public long SubscriptionId
		{
			get { return _SubscriptionId; }
			set 
			{ 
				_SubscriptionId = value;
				OnPropertyChanged("SubscriptionId");
			}
		}
		#endregion

		#region CTor
		public EntitlementRenewal()
		{
		}

		public EntitlementRenewal(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "price":
						this._Price = ObjectFactory.Create<Price>(propertyNode);
						continue;
					case "date":
						this._Date = ParseLong(propertyNode.InnerText);
						continue;
					case "purchaseId":
						this._PurchaseId = ParseLong(propertyNode.InnerText);
						continue;
					case "subscriptionId":
						this._SubscriptionId = ParseLong(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaEntitlementRenewal");
			kparams.AddIfNotNull("price", this._Price);
			kparams.AddIfNotNull("date", this._Date);
			kparams.AddIfNotNull("purchaseId", this._PurchaseId);
			kparams.AddIfNotNull("subscriptionId", this._SubscriptionId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PRICE:
					return "Price";
				case DATE:
					return "Date";
				case PURCHASE_ID:
					return "PurchaseId";
				case SUBSCRIPTION_ID:
					return "SubscriptionId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

