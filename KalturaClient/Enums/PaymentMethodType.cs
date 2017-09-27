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
namespace Kaltura.Enums
{
	public sealed class PaymentMethodType : StringEnum
	{
		public static readonly PaymentMethodType UNKNOWN = new PaymentMethodType("unknown");
		public static readonly PaymentMethodType CREDIT_CARD = new PaymentMethodType("credit_card");
		public static readonly PaymentMethodType SMS = new PaymentMethodType("sms");
		public static readonly PaymentMethodType PAY_PAL = new PaymentMethodType("pay_pal");
		public static readonly PaymentMethodType DEBIT_CARD = new PaymentMethodType("debit_card");
		public static readonly PaymentMethodType IDEAL = new PaymentMethodType("ideal");
		public static readonly PaymentMethodType INCASO = new PaymentMethodType("incaso");
		public static readonly PaymentMethodType GIFT = new PaymentMethodType("gift");
		public static readonly PaymentMethodType VISA = new PaymentMethodType("visa");
		public static readonly PaymentMethodType MASTER_CARD = new PaymentMethodType("master_card");
		public static readonly PaymentMethodType IN_APP = new PaymentMethodType("in_app");
		public static readonly PaymentMethodType M1 = new PaymentMethodType("m1");
		public static readonly PaymentMethodType CHANGE_SUBSCRIPTION = new PaymentMethodType("change_subscription");
		public static readonly PaymentMethodType OFFLINE = new PaymentMethodType("offline");

		private PaymentMethodType(string name) : base(name) { }
	}
}
