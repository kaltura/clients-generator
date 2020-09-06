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
	public class TransactionHistoryFilter : Filter
	{
		#region Constants
		public const string ENTITY_REFERENCE_EQUAL = "entityReferenceEqual";
		public const string START_DATE_GREATER_THAN_OR_EQUAL = "startDateGreaterThanOrEqual";
		public const string END_DATE_LESS_THAN_OR_EQUAL = "endDateLessThanOrEqual";
		public const string ENTITLEMENT_ID_EQUAL = "entitlementIdEqual";
		public const string EXTERNAL_ID_EQUAL = "externalIdEqual";
		public const string BILLING_ITEMS_TYPE_EQUAL = "billingItemsTypeEqual";
		public const string BILLING_ACTION_EQUAL = "billingActionEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private EntityReferenceBy _EntityReferenceEqual = null;
		private int _StartDateGreaterThanOrEqual = Int32.MinValue;
		private int _EndDateLessThanOrEqual = Int32.MinValue;
		private long _EntitlementIdEqual = long.MinValue;
		private string _ExternalIdEqual = null;
		private BillingItemsType _BillingItemsTypeEqual = null;
		private BillingAction _BillingActionEqual = null;
		private TransactionHistoryOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public EntityReferenceBy EntityReferenceEqual
		{
			get { return _EntityReferenceEqual; }
			set 
			{ 
				_EntityReferenceEqual = value;
				OnPropertyChanged("EntityReferenceEqual");
			}
		}
		[JsonProperty]
		public int StartDateGreaterThanOrEqual
		{
			get { return _StartDateGreaterThanOrEqual; }
			set 
			{ 
				_StartDateGreaterThanOrEqual = value;
				OnPropertyChanged("StartDateGreaterThanOrEqual");
			}
		}
		[JsonProperty]
		public int EndDateLessThanOrEqual
		{
			get { return _EndDateLessThanOrEqual; }
			set 
			{ 
				_EndDateLessThanOrEqual = value;
				OnPropertyChanged("EndDateLessThanOrEqual");
			}
		}
		[JsonProperty]
		public long EntitlementIdEqual
		{
			get { return _EntitlementIdEqual; }
			set 
			{ 
				_EntitlementIdEqual = value;
				OnPropertyChanged("EntitlementIdEqual");
			}
		}
		[JsonProperty]
		public string ExternalIdEqual
		{
			get { return _ExternalIdEqual; }
			set 
			{ 
				_ExternalIdEqual = value;
				OnPropertyChanged("ExternalIdEqual");
			}
		}
		[JsonProperty]
		public BillingItemsType BillingItemsTypeEqual
		{
			get { return _BillingItemsTypeEqual; }
			set 
			{ 
				_BillingItemsTypeEqual = value;
				OnPropertyChanged("BillingItemsTypeEqual");
			}
		}
		[JsonProperty]
		public BillingAction BillingActionEqual
		{
			get { return _BillingActionEqual; }
			set 
			{ 
				_BillingActionEqual = value;
				OnPropertyChanged("BillingActionEqual");
			}
		}
		[JsonProperty]
		public new TransactionHistoryOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		#endregion

		#region CTor
		public TransactionHistoryFilter()
		{
		}

		public TransactionHistoryFilter(JToken node) : base(node)
		{
			if(node["entityReferenceEqual"] != null)
			{
				this._EntityReferenceEqual = (EntityReferenceBy)StringEnum.Parse(typeof(EntityReferenceBy), node["entityReferenceEqual"].Value<string>());
			}
			if(node["startDateGreaterThanOrEqual"] != null)
			{
				this._StartDateGreaterThanOrEqual = ParseInt(node["startDateGreaterThanOrEqual"].Value<string>());
			}
			if(node["endDateLessThanOrEqual"] != null)
			{
				this._EndDateLessThanOrEqual = ParseInt(node["endDateLessThanOrEqual"].Value<string>());
			}
			if(node["entitlementIdEqual"] != null)
			{
				this._EntitlementIdEqual = ParseLong(node["entitlementIdEqual"].Value<string>());
			}
			if(node["externalIdEqual"] != null)
			{
				this._ExternalIdEqual = node["externalIdEqual"].Value<string>();
			}
			if(node["billingItemsTypeEqual"] != null)
			{
				this._BillingItemsTypeEqual = (BillingItemsType)StringEnum.Parse(typeof(BillingItemsType), node["billingItemsTypeEqual"].Value<string>());
			}
			if(node["billingActionEqual"] != null)
			{
				this._BillingActionEqual = (BillingAction)StringEnum.Parse(typeof(BillingAction), node["billingActionEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (TransactionHistoryOrderBy)StringEnum.Parse(typeof(TransactionHistoryOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaTransactionHistoryFilter");
			kparams.AddIfNotNull("entityReferenceEqual", this._EntityReferenceEqual);
			kparams.AddIfNotNull("startDateGreaterThanOrEqual", this._StartDateGreaterThanOrEqual);
			kparams.AddIfNotNull("endDateLessThanOrEqual", this._EndDateLessThanOrEqual);
			kparams.AddIfNotNull("entitlementIdEqual", this._EntitlementIdEqual);
			kparams.AddIfNotNull("externalIdEqual", this._ExternalIdEqual);
			kparams.AddIfNotNull("billingItemsTypeEqual", this._BillingItemsTypeEqual);
			kparams.AddIfNotNull("billingActionEqual", this._BillingActionEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ENTITY_REFERENCE_EQUAL:
					return "EntityReferenceEqual";
				case START_DATE_GREATER_THAN_OR_EQUAL:
					return "StartDateGreaterThanOrEqual";
				case END_DATE_LESS_THAN_OR_EQUAL:
					return "EndDateLessThanOrEqual";
				case ENTITLEMENT_ID_EQUAL:
					return "EntitlementIdEqual";
				case EXTERNAL_ID_EQUAL:
					return "ExternalIdEqual";
				case BILLING_ITEMS_TYPE_EQUAL:
					return "BillingItemsTypeEqual";
				case BILLING_ACTION_EQUAL:
					return "BillingActionEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

