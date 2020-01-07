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
namespace Kaltura.Enums
{
	public sealed class PurchaseStatus : StringEnum
	{
		public static readonly PurchaseStatus PPV_PURCHASED = new PurchaseStatus("ppv_purchased");
		public static readonly PurchaseStatus FREE = new PurchaseStatus("free");
		public static readonly PurchaseStatus FOR_PURCHASE_SUBSCRIPTION_ONLY = new PurchaseStatus("for_purchase_subscription_only");
		public static readonly PurchaseStatus SUBSCRIPTION_PURCHASED = new PurchaseStatus("subscription_purchased");
		public static readonly PurchaseStatus FOR_PURCHASE = new PurchaseStatus("for_purchase");
		public static readonly PurchaseStatus SUBSCRIPTION_PURCHASED_WRONG_CURRENCY = new PurchaseStatus("subscription_purchased_wrong_currency");
		public static readonly PurchaseStatus PRE_PAID_PURCHASED = new PurchaseStatus("pre_paid_purchased");
		public static readonly PurchaseStatus GEO_COMMERCE_BLOCKED = new PurchaseStatus("geo_commerce_blocked");
		public static readonly PurchaseStatus ENTITLED_TO_PREVIEW_MODULE = new PurchaseStatus("entitled_to_preview_module");
		public static readonly PurchaseStatus FIRST_DEVICE_LIMITATION = new PurchaseStatus("first_device_limitation");
		public static readonly PurchaseStatus COLLECTION_PURCHASED = new PurchaseStatus("collection_purchased");
		public static readonly PurchaseStatus USER_SUSPENDED = new PurchaseStatus("user_suspended");
		public static readonly PurchaseStatus NOT_FOR_PURCHASE = new PurchaseStatus("not_for_purchase");
		public static readonly PurchaseStatus INVALID_CURRENCY = new PurchaseStatus("invalid_currency");
		public static readonly PurchaseStatus CURRENCY_NOT_DEFINED_ON_PRICE_CODE = new PurchaseStatus("currency_not_defined_on_price_code");

		private PurchaseStatus(string name) : base(name) { }
	}
}
