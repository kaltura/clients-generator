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
// Copyright (C) 2006-2021  Kaltura Inc.
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
	public class Coupon : ObjectBase
	{
		#region Constants
		public const string COUPONS_GROUP = "couponsGroup";
		public const string STATUS = "status";
		public const string TOTAL_USES = "totalUses";
		public const string LEFT_USES = "leftUses";
		public const string COUPON_CODE = "couponCode";
		#endregion

		#region Private Fields
		private CouponsGroup _CouponsGroup;
		private CouponStatus _Status = null;
		private int _TotalUses = Int32.MinValue;
		private int _LeftUses = Int32.MinValue;
		private string _CouponCode = null;
		#endregion

		#region Properties
		[JsonProperty]
		public CouponsGroup CouponsGroup
		{
			get { return _CouponsGroup; }
			private set 
			{ 
				_CouponsGroup = value;
				OnPropertyChanged("CouponsGroup");
			}
		}
		[JsonProperty]
		public CouponStatus Status
		{
			get { return _Status; }
			private set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
		public int TotalUses
		{
			get { return _TotalUses; }
			private set 
			{ 
				_TotalUses = value;
				OnPropertyChanged("TotalUses");
			}
		}
		[JsonProperty]
		public int LeftUses
		{
			get { return _LeftUses; }
			private set 
			{ 
				_LeftUses = value;
				OnPropertyChanged("LeftUses");
			}
		}
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
		#endregion

		#region CTor
		public Coupon()
		{
		}

		public Coupon(JToken node) : base(node)
		{
			if(node["couponsGroup"] != null)
			{
				this._CouponsGroup = ObjectFactory.Create<CouponsGroup>(node["couponsGroup"]);
			}
			if(node["status"] != null)
			{
				this._Status = (CouponStatus)StringEnum.Parse(typeof(CouponStatus), node["status"].Value<string>());
			}
			if(node["totalUses"] != null)
			{
				this._TotalUses = ParseInt(node["totalUses"].Value<string>());
			}
			if(node["leftUses"] != null)
			{
				this._LeftUses = ParseInt(node["leftUses"].Value<string>());
			}
			if(node["couponCode"] != null)
			{
				this._CouponCode = node["couponCode"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCoupon");
			kparams.AddIfNotNull("couponsGroup", this._CouponsGroup);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("totalUses", this._TotalUses);
			kparams.AddIfNotNull("leftUses", this._LeftUses);
			kparams.AddIfNotNull("couponCode", this._CouponCode);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case COUPONS_GROUP:
					return "CouponsGroup";
				case STATUS:
					return "Status";
				case TOTAL_USES:
					return "TotalUses";
				case LEFT_USES:
					return "LeftUses";
				case COUPON_CODE:
					return "CouponCode";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

