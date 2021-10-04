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
	public class CouponsGroup : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string START_DATE = "startDate";
		public const string END_DATE = "endDate";
		public const string MAX_USES_NUMBER = "maxUsesNumber";
		public const string MAX_USES_NUMBER_ON_RENEWABLE_SUB = "maxUsesNumberOnRenewableSub";
		public const string COUPON_GROUP_TYPE = "couponGroupType";
		public const string MAX_HOUSEHOLD_USES = "maxHouseholdUses";
		public const string DISCOUNT_ID = "discountId";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Name = null;
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		private int _MaxUsesNumber = Int32.MinValue;
		private int _MaxUsesNumberOnRenewableSub = Int32.MinValue;
		private CouponGroupType _CouponGroupType = null;
		private int _MaxHouseholdUses = Int32.MinValue;
		private long _DiscountId = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public string Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public long StartDate
		{
			get { return _StartDate; }
			set 
			{ 
				_StartDate = value;
				OnPropertyChanged("StartDate");
			}
		}
		[JsonProperty]
		public long EndDate
		{
			get { return _EndDate; }
			set 
			{ 
				_EndDate = value;
				OnPropertyChanged("EndDate");
			}
		}
		[JsonProperty]
		public int MaxUsesNumber
		{
			get { return _MaxUsesNumber; }
			set 
			{ 
				_MaxUsesNumber = value;
				OnPropertyChanged("MaxUsesNumber");
			}
		}
		[JsonProperty]
		public int MaxUsesNumberOnRenewableSub
		{
			get { return _MaxUsesNumberOnRenewableSub; }
			set 
			{ 
				_MaxUsesNumberOnRenewableSub = value;
				OnPropertyChanged("MaxUsesNumberOnRenewableSub");
			}
		}
		[JsonProperty]
		public CouponGroupType CouponGroupType
		{
			get { return _CouponGroupType; }
			set 
			{ 
				_CouponGroupType = value;
				OnPropertyChanged("CouponGroupType");
			}
		}
		[JsonProperty]
		public int MaxHouseholdUses
		{
			get { return _MaxHouseholdUses; }
			set 
			{ 
				_MaxHouseholdUses = value;
				OnPropertyChanged("MaxHouseholdUses");
			}
		}
		[JsonProperty]
		public long DiscountId
		{
			get { return _DiscountId; }
			set 
			{ 
				_DiscountId = value;
				OnPropertyChanged("DiscountId");
			}
		}
		#endregion

		#region CTor
		public CouponsGroup()
		{
		}

		public CouponsGroup(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["startDate"] != null)
			{
				this._StartDate = ParseLong(node["startDate"].Value<string>());
			}
			if(node["endDate"] != null)
			{
				this._EndDate = ParseLong(node["endDate"].Value<string>());
			}
			if(node["maxUsesNumber"] != null)
			{
				this._MaxUsesNumber = ParseInt(node["maxUsesNumber"].Value<string>());
			}
			if(node["maxUsesNumberOnRenewableSub"] != null)
			{
				this._MaxUsesNumberOnRenewableSub = ParseInt(node["maxUsesNumberOnRenewableSub"].Value<string>());
			}
			if(node["couponGroupType"] != null)
			{
				this._CouponGroupType = (CouponGroupType)StringEnum.Parse(typeof(CouponGroupType), node["couponGroupType"].Value<string>());
			}
			if(node["maxHouseholdUses"] != null)
			{
				this._MaxHouseholdUses = ParseInt(node["maxHouseholdUses"].Value<string>());
			}
			if(node["discountId"] != null)
			{
				this._DiscountId = ParseLong(node["discountId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCouponsGroup");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("maxUsesNumber", this._MaxUsesNumber);
			kparams.AddIfNotNull("maxUsesNumberOnRenewableSub", this._MaxUsesNumberOnRenewableSub);
			kparams.AddIfNotNull("couponGroupType", this._CouponGroupType);
			kparams.AddIfNotNull("maxHouseholdUses", this._MaxHouseholdUses);
			kparams.AddIfNotNull("discountId", this._DiscountId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case NAME:
					return "Name";
				case START_DATE:
					return "StartDate";
				case END_DATE:
					return "EndDate";
				case MAX_USES_NUMBER:
					return "MaxUsesNumber";
				case MAX_USES_NUMBER_ON_RENEWABLE_SUB:
					return "MaxUsesNumberOnRenewableSub";
				case COUPON_GROUP_TYPE:
					return "CouponGroupType";
				case MAX_HOUSEHOLD_USES:
					return "MaxHouseholdUses";
				case DISCOUNT_ID:
					return "DiscountId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

