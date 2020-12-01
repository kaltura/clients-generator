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
	public class CouponEntitlementDiscountDetails : EntitlementDiscountDetails
	{
		#region Constants
		public const string COUPON_CODE = "couponCode";
		public const string ENDLESS_COUPON = "endlessCoupon";
		#endregion

		#region Private Fields
		private string _CouponCode = null;
		private bool? _EndlessCoupon = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string CouponCode
		{
			get { return _CouponCode; }
			private set 
			{ 
				_CouponCode = value;
				OnPropertyChanged("CouponCode");
			}
		}
		[JsonProperty]
		public bool? EndlessCoupon
		{
			get { return _EndlessCoupon; }
			private set 
			{ 
				_EndlessCoupon = value;
				OnPropertyChanged("EndlessCoupon");
			}
		}
		#endregion

		#region CTor
		public CouponEntitlementDiscountDetails()
		{
		}

		public CouponEntitlementDiscountDetails(JToken node) : base(node)
		{
			if(node["couponCode"] != null)
			{
				this._CouponCode = node["couponCode"].Value<string>();
			}
			if(node["endlessCoupon"] != null)
			{
				this._EndlessCoupon = ParseBool(node["endlessCoupon"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCouponEntitlementDiscountDetails");
			kparams.AddIfNotNull("couponCode", this._CouponCode);
			kparams.AddIfNotNull("endlessCoupon", this._EndlessCoupon);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case COUPON_CODE:
					return "CouponCode";
				case ENDLESS_COUPON:
					return "EndlessCoupon";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

