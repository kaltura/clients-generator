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
	public class UnifiedPaymentRenewal : ObjectBase
	{
		#region Constants
		public const string PRICE = "price";
		public const string DATE = "date";
		public const string UNIFIED_PAYMENT_ID = "unifiedPaymentId";
		public const string ENTITLEMENTS = "entitlements";
		public const string USER_ID = "userId";
		#endregion

		#region Private Fields
		private Price _Price;
		private long _Date = long.MinValue;
		private long _UnifiedPaymentId = long.MinValue;
		private IList<EntitlementRenewalBase> _Entitlements;
		private long _UserId = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public Price Price
		{
			get { return _Price; }
			set 
			{ 
				_Price = value;
				OnPropertyChanged("Price");
			}
		}
		[JsonProperty]
		public long Date
		{
			get { return _Date; }
			set 
			{ 
				_Date = value;
				OnPropertyChanged("Date");
			}
		}
		[JsonProperty]
		public long UnifiedPaymentId
		{
			get { return _UnifiedPaymentId; }
			set 
			{ 
				_UnifiedPaymentId = value;
				OnPropertyChanged("UnifiedPaymentId");
			}
		}
		[JsonProperty]
		public IList<EntitlementRenewalBase> Entitlements
		{
			get { return _Entitlements; }
			set 
			{ 
				_Entitlements = value;
				OnPropertyChanged("Entitlements");
			}
		}
		[JsonProperty]
		public long UserId
		{
			get { return _UserId; }
			set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		#endregion

		#region CTor
		public UnifiedPaymentRenewal()
		{
		}

		public UnifiedPaymentRenewal(JToken node) : base(node)
		{
			if(node["price"] != null)
			{
				this._Price = ObjectFactory.Create<Price>(node["price"]);
			}
			if(node["date"] != null)
			{
				this._Date = ParseLong(node["date"].Value<string>());
			}
			if(node["unifiedPaymentId"] != null)
			{
				this._UnifiedPaymentId = ParseLong(node["unifiedPaymentId"].Value<string>());
			}
			if(node["entitlements"] != null)
			{
				this._Entitlements = new List<EntitlementRenewalBase>();
				foreach(var arrayNode in node["entitlements"].Children())
				{
					this._Entitlements.Add(ObjectFactory.Create<EntitlementRenewalBase>(arrayNode));
				}
			}
			if(node["userId"] != null)
			{
				this._UserId = ParseLong(node["userId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaUnifiedPaymentRenewal");
			kparams.AddIfNotNull("price", this._Price);
			kparams.AddIfNotNull("date", this._Date);
			kparams.AddIfNotNull("unifiedPaymentId", this._UnifiedPaymentId);
			kparams.AddIfNotNull("entitlements", this._Entitlements);
			kparams.AddIfNotNull("userId", this._UserId);
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
				case UNIFIED_PAYMENT_ID:
					return "UnifiedPaymentId";
				case ENTITLEMENTS:
					return "Entitlements";
				case USER_ID:
					return "UserId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

