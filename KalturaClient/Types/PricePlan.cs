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
	public class PricePlan : UsageModule
	{
		#region Constants
		public const string IS_RENEWABLE = "isRenewable";
		public const string RENEWALS_NUMBER = "renewalsNumber";
		public const string DISCOUNT_ID = "discountId";
		public const string PRICE_DETAILS_ID = "priceDetailsId";
		#endregion

		#region Private Fields
		private bool? _IsRenewable = null;
		private int _RenewalsNumber = Int32.MinValue;
		private long _DiscountId = long.MinValue;
		private long _PriceDetailsId = long.MinValue;
		#endregion

		#region Properties
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
		public int RenewalsNumber
		{
			get { return _RenewalsNumber; }
			private set 
			{ 
				_RenewalsNumber = value;
				OnPropertyChanged("RenewalsNumber");
			}
		}
		[JsonProperty]
		public long DiscountId
		{
			get { return _DiscountId; }
			private set 
			{ 
				_DiscountId = value;
				OnPropertyChanged("DiscountId");
			}
		}
		[JsonProperty]
		public long PriceDetailsId
		{
			get { return _PriceDetailsId; }
			set 
			{ 
				_PriceDetailsId = value;
				OnPropertyChanged("PriceDetailsId");
			}
		}
		#endregion

		#region CTor
		public PricePlan()
		{
		}

		public PricePlan(JToken node) : base(node)
		{
			if(node["isRenewable"] != null)
			{
				this._IsRenewable = ParseBool(node["isRenewable"].Value<string>());
			}
			if(node["renewalsNumber"] != null)
			{
				this._RenewalsNumber = ParseInt(node["renewalsNumber"].Value<string>());
			}
			if(node["discountId"] != null)
			{
				this._DiscountId = ParseLong(node["discountId"].Value<string>());
			}
			if(node["priceDetailsId"] != null)
			{
				this._PriceDetailsId = ParseLong(node["priceDetailsId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPricePlan");
			kparams.AddIfNotNull("isRenewable", this._IsRenewable);
			kparams.AddIfNotNull("renewalsNumber", this._RenewalsNumber);
			kparams.AddIfNotNull("discountId", this._DiscountId);
			kparams.AddIfNotNull("priceDetailsId", this._PriceDetailsId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case IS_RENEWABLE:
					return "IsRenewable";
				case RENEWALS_NUMBER:
					return "RenewalsNumber";
				case DISCOUNT_ID:
					return "DiscountId";
				case PRICE_DETAILS_ID:
					return "PriceDetailsId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

