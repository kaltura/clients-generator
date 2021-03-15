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
	public class Compensation : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string SUBSCRIPTION_ID = "subscriptionId";
		public const string COMPENSATION_TYPE = "compensationType";
		public const string AMOUNT = "amount";
		public const string TOTAL_RENEWAL_ITERATIONS = "totalRenewalIterations";
		public const string APPLIED_RENEWAL_ITERATIONS = "appliedRenewalIterations";
		public const string PURCHASE_ID = "purchaseId";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private long _SubscriptionId = long.MinValue;
		private CompensationType _CompensationType = null;
		private float _Amount = decimal.MinValue;
		private int _TotalRenewalIterations = Int32.MinValue;
		private int _AppliedRenewalIterations = Int32.MinValue;
		private int _PurchaseId = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public long Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public long SubscriptionId
		{
			get { return _SubscriptionId; }
			private set 
			{ 
				_SubscriptionId = value;
				OnPropertyChanged("SubscriptionId");
			}
		}
		[JsonProperty]
		public CompensationType CompensationType
		{
			get { return _CompensationType; }
			set 
			{ 
				_CompensationType = value;
				OnPropertyChanged("CompensationType");
			}
		}
		[JsonProperty]
		public float Amount
		{
			get { return _Amount; }
			set 
			{ 
				_Amount = value;
				OnPropertyChanged("Amount");
			}
		}
		[JsonProperty]
		public int TotalRenewalIterations
		{
			get { return _TotalRenewalIterations; }
			set 
			{ 
				_TotalRenewalIterations = value;
				OnPropertyChanged("TotalRenewalIterations");
			}
		}
		[JsonProperty]
		public int AppliedRenewalIterations
		{
			get { return _AppliedRenewalIterations; }
			private set 
			{ 
				_AppliedRenewalIterations = value;
				OnPropertyChanged("AppliedRenewalIterations");
			}
		}
		[JsonProperty]
		public int PurchaseId
		{
			get { return _PurchaseId; }
			set 
			{ 
				_PurchaseId = value;
				OnPropertyChanged("PurchaseId");
			}
		}
		#endregion

		#region CTor
		public Compensation()
		{
		}

		public Compensation(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["subscriptionId"] != null)
			{
				this._SubscriptionId = ParseLong(node["subscriptionId"].Value<string>());
			}
			if(node["compensationType"] != null)
			{
				this._CompensationType = (CompensationType)StringEnum.Parse(typeof(CompensationType), node["compensationType"].Value<string>());
			}
			if(node["amount"] != null)
			{
				this._Amount = ParseFloat(node["amount"].Value<string>());
			}
			if(node["totalRenewalIterations"] != null)
			{
				this._TotalRenewalIterations = ParseInt(node["totalRenewalIterations"].Value<string>());
			}
			if(node["appliedRenewalIterations"] != null)
			{
				this._AppliedRenewalIterations = ParseInt(node["appliedRenewalIterations"].Value<string>());
			}
			if(node["purchaseId"] != null)
			{
				this._PurchaseId = ParseInt(node["purchaseId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCompensation");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("subscriptionId", this._SubscriptionId);
			kparams.AddIfNotNull("compensationType", this._CompensationType);
			kparams.AddIfNotNull("amount", this._Amount);
			kparams.AddIfNotNull("totalRenewalIterations", this._TotalRenewalIterations);
			kparams.AddIfNotNull("appliedRenewalIterations", this._AppliedRenewalIterations);
			kparams.AddIfNotNull("purchaseId", this._PurchaseId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case SUBSCRIPTION_ID:
					return "SubscriptionId";
				case COMPENSATION_TYPE:
					return "CompensationType";
				case AMOUNT:
					return "Amount";
				case TOTAL_RENEWAL_ITERATIONS:
					return "TotalRenewalIterations";
				case APPLIED_RENEWAL_ITERATIONS:
					return "AppliedRenewalIterations";
				case PURCHASE_ID:
					return "PurchaseId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

