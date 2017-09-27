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
	public class Coupon : ObjectBase
	{
		#region Constants
		public const string COUPONS_GROUP = "couponsGroup";
		public const string STATUS = "status";
		#endregion

		#region Private Fields
		private CouponsGroup _CouponsGroup;
		private CouponStatus _Status = null;
		#endregion

		#region Properties
		public CouponsGroup CouponsGroup
		{
			get { return _CouponsGroup; }
		}
		public CouponStatus Status
		{
			get { return _Status; }
		}
		#endregion

		#region CTor
		public Coupon()
		{
		}

		public Coupon(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "couponsGroup":
						this._CouponsGroup = ObjectFactory.Create<CouponsGroup>(propertyNode);
						continue;
					case "status":
						this._Status = (CouponStatus)StringEnum.Parse(typeof(CouponStatus), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaCoupon");
			kparams.AddIfNotNull("couponsGroup", this._CouponsGroup);
			kparams.AddIfNotNull("status", this._Status);
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
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

