// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
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
	public class PpvPrice : ProductPrice
	{
		#region Constants
		public const string FILE_ID = "fileId";
		public const string PPV_MODULE_ID = "ppvModuleId";
		public const string IS_SUBSCRIPTION_ONLY = "isSubscriptionOnly";
		public const string SUBSCRIPTION_ID = "subscriptionId";
		public const string COLLECTION_ID = "collectionId";
		public const string PRE_PAID_ID = "prePaidId";
		public const string PPV_DESCRIPTIONS = "ppvDescriptions";
		public const string PURCHASE_USER_ID = "purchaseUserId";
		public const string PURCHASED_MEDIA_FILE_ID = "purchasedMediaFileId";
		public const string RELATED_MEDIA_FILE_IDS = "relatedMediaFileIds";
		public const string START_DATE = "startDate";
		public const string END_DATE = "endDate";
		public const string DISCOUNT_END_DATE = "discountEndDate";
		public const string FIRST_DEVICE_NAME = "firstDeviceName";
		public const string IS_IN_CANCELATION_PERIOD = "isInCancelationPeriod";
		public const string PPV_PRODUCT_CODE = "ppvProductCode";
		#endregion

		#region Private Fields
		private int _FileId = Int32.MinValue;
		private string _PpvModuleId = null;
		private bool? _IsSubscriptionOnly = null;
		private string _SubscriptionId = null;
		private string _CollectionId = null;
		private string _PrePaidId = null;
		private IList<TranslationToken> _PpvDescriptions;
		private string _PurchaseUserId = null;
		private int _PurchasedMediaFileId = Int32.MinValue;
		private IList<IntegerValue> _RelatedMediaFileIds;
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		private long _DiscountEndDate = long.MinValue;
		private string _FirstDeviceName = null;
		private bool? _IsInCancelationPeriod = null;
		private string _PpvProductCode = null;
		#endregion

		#region Properties
		[JsonProperty]
		public int FileId
		{
			get { return _FileId; }
			set 
			{ 
				_FileId = value;
				OnPropertyChanged("FileId");
			}
		}
		[JsonProperty]
		public string PpvModuleId
		{
			get { return _PpvModuleId; }
			set 
			{ 
				_PpvModuleId = value;
				OnPropertyChanged("PpvModuleId");
			}
		}
		[JsonProperty]
		public bool? IsSubscriptionOnly
		{
			get { return _IsSubscriptionOnly; }
			set 
			{ 
				_IsSubscriptionOnly = value;
				OnPropertyChanged("IsSubscriptionOnly");
			}
		}
		[JsonProperty]
		public string SubscriptionId
		{
			get { return _SubscriptionId; }
			set 
			{ 
				_SubscriptionId = value;
				OnPropertyChanged("SubscriptionId");
			}
		}
		[JsonProperty]
		public string CollectionId
		{
			get { return _CollectionId; }
			set 
			{ 
				_CollectionId = value;
				OnPropertyChanged("CollectionId");
			}
		}
		[JsonProperty]
		public string PrePaidId
		{
			get { return _PrePaidId; }
			set 
			{ 
				_PrePaidId = value;
				OnPropertyChanged("PrePaidId");
			}
		}
		[JsonProperty]
		public IList<TranslationToken> PpvDescriptions
		{
			get { return _PpvDescriptions; }
			set 
			{ 
				_PpvDescriptions = value;
				OnPropertyChanged("PpvDescriptions");
			}
		}
		[JsonProperty]
		public string PurchaseUserId
		{
			get { return _PurchaseUserId; }
			set 
			{ 
				_PurchaseUserId = value;
				OnPropertyChanged("PurchaseUserId");
			}
		}
		[JsonProperty]
		public int PurchasedMediaFileId
		{
			get { return _PurchasedMediaFileId; }
			set 
			{ 
				_PurchasedMediaFileId = value;
				OnPropertyChanged("PurchasedMediaFileId");
			}
		}
		[JsonProperty]
		public IList<IntegerValue> RelatedMediaFileIds
		{
			get { return _RelatedMediaFileIds; }
			set 
			{ 
				_RelatedMediaFileIds = value;
				OnPropertyChanged("RelatedMediaFileIds");
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
		public long DiscountEndDate
		{
			get { return _DiscountEndDate; }
			set 
			{ 
				_DiscountEndDate = value;
				OnPropertyChanged("DiscountEndDate");
			}
		}
		[JsonProperty]
		public string FirstDeviceName
		{
			get { return _FirstDeviceName; }
			set 
			{ 
				_FirstDeviceName = value;
				OnPropertyChanged("FirstDeviceName");
			}
		}
		[JsonProperty]
		public bool? IsInCancelationPeriod
		{
			get { return _IsInCancelationPeriod; }
			set 
			{ 
				_IsInCancelationPeriod = value;
				OnPropertyChanged("IsInCancelationPeriod");
			}
		}
		[JsonProperty]
		public string PpvProductCode
		{
			get { return _PpvProductCode; }
			set 
			{ 
				_PpvProductCode = value;
				OnPropertyChanged("PpvProductCode");
			}
		}
		#endregion

		#region CTor
		public PpvPrice()
		{
		}

		public PpvPrice(JToken node) : base(node)
		{
			if(node["fileId"] != null)
			{
				this._FileId = ParseInt(node["fileId"].Value<string>());
			}
			if(node["ppvModuleId"] != null)
			{
				this._PpvModuleId = node["ppvModuleId"].Value<string>();
			}
			if(node["isSubscriptionOnly"] != null)
			{
				this._IsSubscriptionOnly = ParseBool(node["isSubscriptionOnly"].Value<string>());
			}
			if(node["subscriptionId"] != null)
			{
				this._SubscriptionId = node["subscriptionId"].Value<string>();
			}
			if(node["collectionId"] != null)
			{
				this._CollectionId = node["collectionId"].Value<string>();
			}
			if(node["prePaidId"] != null)
			{
				this._PrePaidId = node["prePaidId"].Value<string>();
			}
			if(node["ppvDescriptions"] != null)
			{
				this._PpvDescriptions = new List<TranslationToken>();
				foreach(var arrayNode in node["ppvDescriptions"].Children())
				{
					this._PpvDescriptions.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
				}
			}
			if(node["purchaseUserId"] != null)
			{
				this._PurchaseUserId = node["purchaseUserId"].Value<string>();
			}
			if(node["purchasedMediaFileId"] != null)
			{
				this._PurchasedMediaFileId = ParseInt(node["purchasedMediaFileId"].Value<string>());
			}
			if(node["relatedMediaFileIds"] != null)
			{
				this._RelatedMediaFileIds = new List<IntegerValue>();
				foreach(var arrayNode in node["relatedMediaFileIds"].Children())
				{
					this._RelatedMediaFileIds.Add(ObjectFactory.Create<IntegerValue>(arrayNode));
				}
			}
			if(node["startDate"] != null)
			{
				this._StartDate = ParseLong(node["startDate"].Value<string>());
			}
			if(node["endDate"] != null)
			{
				this._EndDate = ParseLong(node["endDate"].Value<string>());
			}
			if(node["discountEndDate"] != null)
			{
				this._DiscountEndDate = ParseLong(node["discountEndDate"].Value<string>());
			}
			if(node["firstDeviceName"] != null)
			{
				this._FirstDeviceName = node["firstDeviceName"].Value<string>();
			}
			if(node["isInCancelationPeriod"] != null)
			{
				this._IsInCancelationPeriod = ParseBool(node["isInCancelationPeriod"].Value<string>());
			}
			if(node["ppvProductCode"] != null)
			{
				this._PpvProductCode = node["ppvProductCode"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPpvPrice");
			kparams.AddIfNotNull("fileId", this._FileId);
			kparams.AddIfNotNull("ppvModuleId", this._PpvModuleId);
			kparams.AddIfNotNull("isSubscriptionOnly", this._IsSubscriptionOnly);
			kparams.AddIfNotNull("subscriptionId", this._SubscriptionId);
			kparams.AddIfNotNull("collectionId", this._CollectionId);
			kparams.AddIfNotNull("prePaidId", this._PrePaidId);
			kparams.AddIfNotNull("ppvDescriptions", this._PpvDescriptions);
			kparams.AddIfNotNull("purchaseUserId", this._PurchaseUserId);
			kparams.AddIfNotNull("purchasedMediaFileId", this._PurchasedMediaFileId);
			kparams.AddIfNotNull("relatedMediaFileIds", this._RelatedMediaFileIds);
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("discountEndDate", this._DiscountEndDate);
			kparams.AddIfNotNull("firstDeviceName", this._FirstDeviceName);
			kparams.AddIfNotNull("isInCancelationPeriod", this._IsInCancelationPeriod);
			kparams.AddIfNotNull("ppvProductCode", this._PpvProductCode);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case FILE_ID:
					return "FileId";
				case PPV_MODULE_ID:
					return "PpvModuleId";
				case IS_SUBSCRIPTION_ONLY:
					return "IsSubscriptionOnly";
				case SUBSCRIPTION_ID:
					return "SubscriptionId";
				case COLLECTION_ID:
					return "CollectionId";
				case PRE_PAID_ID:
					return "PrePaidId";
				case PPV_DESCRIPTIONS:
					return "PpvDescriptions";
				case PURCHASE_USER_ID:
					return "PurchaseUserId";
				case PURCHASED_MEDIA_FILE_ID:
					return "PurchasedMediaFileId";
				case RELATED_MEDIA_FILE_IDS:
					return "RelatedMediaFileIds";
				case START_DATE:
					return "StartDate";
				case END_DATE:
					return "EndDate";
				case DISCOUNT_END_DATE:
					return "DiscountEndDate";
				case FIRST_DEVICE_NAME:
					return "FirstDeviceName";
				case IS_IN_CANCELATION_PERIOD:
					return "IsInCancelationPeriod";
				case PPV_PRODUCT_CODE:
					return "PpvProductCode";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

