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
// Copyright (C) 2006-2018  Kaltura Inc.
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
	public class Subscription : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string CHANNELS = "channels";
		public const string START_DATE = "startDate";
		public const string END_DATE = "endDate";
		public const string FILE_TYPES = "fileTypes";
		public const string IS_RENEWABLE = "isRenewable";
		public const string RENEWALS_NUMBER = "renewalsNumber";
		public const string IS_INFINITE_RENEWAL = "isInfiniteRenewal";
		public const string PRICE = "price";
		public const string DISCOUNT_MODULE = "discountModule";
		public const string NAME = "name";
		public const string MULTILINGUAL_NAME = "multilingualName";
		public const string DESCRIPTION = "description";
		public const string MULTILINGUAL_DESCRIPTION = "multilingualDescription";
		public const string MEDIA_ID = "mediaId";
		public const string PRORITY_IN_ORDER = "prorityInOrder";
		public const string PRICE_PLAN_IDS = "pricePlanIds";
		public const string PREVIEW_MODULE = "previewModule";
		public const string HOUSEHOLD_LIMITATIONS_ID = "householdLimitationsId";
		public const string GRACE_PERIOD_MINUTES = "gracePeriodMinutes";
		public const string PREMIUM_SERVICES = "premiumServices";
		public const string MAX_VIEWS_NUMBER = "maxViewsNumber";
		public const string VIEW_LIFE_CYCLE = "viewLifeCycle";
		public const string WAIVER_PERIOD = "waiverPeriod";
		public const string IS_WAIVER_ENABLED = "isWaiverEnabled";
		public const string USER_TYPES = "userTypes";
		public const string COUPONS_GROUPS = "couponsGroups";
		public const string PRODUCT_CODES = "productCodes";
		public const string DEPENDENCY_TYPE = "dependencyType";
		public const string EXTERNAL_ID = "externalId";
		public const string IS_CANCELLATION_BLOCKED = "isCancellationBlocked";
		#endregion

		#region Private Fields
		private string _Id = null;
		private IList<BaseChannel> _Channels;
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		private IList<IntegerValue> _FileTypes;
		private bool? _IsRenewable = null;
		private int _RenewalsNumber = Int32.MinValue;
		private bool? _IsInfiniteRenewal = null;
		private PriceDetails _Price;
		private DiscountModule _DiscountModule;
		private string _Name = null;
		private IList<TranslationToken> _MultilingualName;
		private string _Description = null;
		private IList<TranslationToken> _MultilingualDescription;
		private int _MediaId = Int32.MinValue;
		private long _ProrityInOrder = long.MinValue;
		private string _PricePlanIds = null;
		private PreviewModule _PreviewModule;
		private int _HouseholdLimitationsId = Int32.MinValue;
		private int _GracePeriodMinutes = Int32.MinValue;
		private IList<PremiumService> _PremiumServices;
		private int _MaxViewsNumber = Int32.MinValue;
		private int _ViewLifeCycle = Int32.MinValue;
		private int _WaiverPeriod = Int32.MinValue;
		private bool? _IsWaiverEnabled = null;
		private IList<OTTUserType> _UserTypes;
		private IList<CouponsGroup> _CouponsGroups;
		private IList<ProductCode> _ProductCodes;
		private SubscriptionDependencyType _DependencyType = null;
		private string _ExternalId = null;
		private bool? _IsCancellationBlocked = null;
		#endregion

		#region Properties
		public string Id
		{
			get { return _Id; }
			set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		public IList<BaseChannel> Channels
		{
			get { return _Channels; }
			set 
			{ 
				_Channels = value;
				OnPropertyChanged("Channels");
			}
		}
		public long StartDate
		{
			get { return _StartDate; }
			set 
			{ 
				_StartDate = value;
				OnPropertyChanged("StartDate");
			}
		}
		public long EndDate
		{
			get { return _EndDate; }
			set 
			{ 
				_EndDate = value;
				OnPropertyChanged("EndDate");
			}
		}
		public IList<IntegerValue> FileTypes
		{
			get { return _FileTypes; }
			set 
			{ 
				_FileTypes = value;
				OnPropertyChanged("FileTypes");
			}
		}
		public bool? IsRenewable
		{
			get { return _IsRenewable; }
			set 
			{ 
				_IsRenewable = value;
				OnPropertyChanged("IsRenewable");
			}
		}
		public int RenewalsNumber
		{
			get { return _RenewalsNumber; }
			set 
			{ 
				_RenewalsNumber = value;
				OnPropertyChanged("RenewalsNumber");
			}
		}
		public bool? IsInfiniteRenewal
		{
			get { return _IsInfiniteRenewal; }
			set 
			{ 
				_IsInfiniteRenewal = value;
				OnPropertyChanged("IsInfiniteRenewal");
			}
		}
		public PriceDetails Price
		{
			get { return _Price; }
			set 
			{ 
				_Price = value;
				OnPropertyChanged("Price");
			}
		}
		public DiscountModule DiscountModule
		{
			get { return _DiscountModule; }
			set 
			{ 
				_DiscountModule = value;
				OnPropertyChanged("DiscountModule");
			}
		}
		public string Name
		{
			get { return _Name; }
		}
		public IList<TranslationToken> MultilingualName
		{
			get { return _MultilingualName; }
			set 
			{ 
				_MultilingualName = value;
				OnPropertyChanged("MultilingualName");
			}
		}
		public string Description
		{
			get { return _Description; }
		}
		public IList<TranslationToken> MultilingualDescription
		{
			get { return _MultilingualDescription; }
			set 
			{ 
				_MultilingualDescription = value;
				OnPropertyChanged("MultilingualDescription");
			}
		}
		public int MediaId
		{
			get { return _MediaId; }
			set 
			{ 
				_MediaId = value;
				OnPropertyChanged("MediaId");
			}
		}
		public long ProrityInOrder
		{
			get { return _ProrityInOrder; }
			set 
			{ 
				_ProrityInOrder = value;
				OnPropertyChanged("ProrityInOrder");
			}
		}
		public string PricePlanIds
		{
			get { return _PricePlanIds; }
			set 
			{ 
				_PricePlanIds = value;
				OnPropertyChanged("PricePlanIds");
			}
		}
		public PreviewModule PreviewModule
		{
			get { return _PreviewModule; }
			set 
			{ 
				_PreviewModule = value;
				OnPropertyChanged("PreviewModule");
			}
		}
		public int HouseholdLimitationsId
		{
			get { return _HouseholdLimitationsId; }
			set 
			{ 
				_HouseholdLimitationsId = value;
				OnPropertyChanged("HouseholdLimitationsId");
			}
		}
		public int GracePeriodMinutes
		{
			get { return _GracePeriodMinutes; }
			set 
			{ 
				_GracePeriodMinutes = value;
				OnPropertyChanged("GracePeriodMinutes");
			}
		}
		public IList<PremiumService> PremiumServices
		{
			get { return _PremiumServices; }
			set 
			{ 
				_PremiumServices = value;
				OnPropertyChanged("PremiumServices");
			}
		}
		public int MaxViewsNumber
		{
			get { return _MaxViewsNumber; }
			set 
			{ 
				_MaxViewsNumber = value;
				OnPropertyChanged("MaxViewsNumber");
			}
		}
		public int ViewLifeCycle
		{
			get { return _ViewLifeCycle; }
			set 
			{ 
				_ViewLifeCycle = value;
				OnPropertyChanged("ViewLifeCycle");
			}
		}
		public int WaiverPeriod
		{
			get { return _WaiverPeriod; }
			set 
			{ 
				_WaiverPeriod = value;
				OnPropertyChanged("WaiverPeriod");
			}
		}
		public bool? IsWaiverEnabled
		{
			get { return _IsWaiverEnabled; }
			set 
			{ 
				_IsWaiverEnabled = value;
				OnPropertyChanged("IsWaiverEnabled");
			}
		}
		public IList<OTTUserType> UserTypes
		{
			get { return _UserTypes; }
			set 
			{ 
				_UserTypes = value;
				OnPropertyChanged("UserTypes");
			}
		}
		public IList<CouponsGroup> CouponsGroups
		{
			get { return _CouponsGroups; }
			set 
			{ 
				_CouponsGroups = value;
				OnPropertyChanged("CouponsGroups");
			}
		}
		public IList<ProductCode> ProductCodes
		{
			get { return _ProductCodes; }
			set 
			{ 
				_ProductCodes = value;
				OnPropertyChanged("ProductCodes");
			}
		}
		public SubscriptionDependencyType DependencyType
		{
			get { return _DependencyType; }
			set 
			{ 
				_DependencyType = value;
				OnPropertyChanged("DependencyType");
			}
		}
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		public bool? IsCancellationBlocked
		{
			get { return _IsCancellationBlocked; }
			set 
			{ 
				_IsCancellationBlocked = value;
				OnPropertyChanged("IsCancellationBlocked");
			}
		}
		#endregion

		#region CTor
		public Subscription()
		{
		}

		public Subscription(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "channels":
						this._Channels = new List<BaseChannel>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Channels.Add(ObjectFactory.Create<BaseChannel>(arrayNode));
						}
						continue;
					case "startDate":
						this._StartDate = ParseLong(propertyNode.InnerText);
						continue;
					case "endDate":
						this._EndDate = ParseLong(propertyNode.InnerText);
						continue;
					case "fileTypes":
						this._FileTypes = new List<IntegerValue>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._FileTypes.Add(ObjectFactory.Create<IntegerValue>(arrayNode));
						}
						continue;
					case "isRenewable":
						this._IsRenewable = ParseBool(propertyNode.InnerText);
						continue;
					case "renewalsNumber":
						this._RenewalsNumber = ParseInt(propertyNode.InnerText);
						continue;
					case "isInfiniteRenewal":
						this._IsInfiniteRenewal = ParseBool(propertyNode.InnerText);
						continue;
					case "price":
						this._Price = ObjectFactory.Create<PriceDetails>(propertyNode);
						continue;
					case "discountModule":
						this._DiscountModule = ObjectFactory.Create<DiscountModule>(propertyNode);
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "multilingualName":
						this._MultilingualName = new List<TranslationToken>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._MultilingualName.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
						}
						continue;
					case "description":
						this._Description = propertyNode.InnerText;
						continue;
					case "multilingualDescription":
						this._MultilingualDescription = new List<TranslationToken>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._MultilingualDescription.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
						}
						continue;
					case "mediaId":
						this._MediaId = ParseInt(propertyNode.InnerText);
						continue;
					case "prorityInOrder":
						this._ProrityInOrder = ParseLong(propertyNode.InnerText);
						continue;
					case "pricePlanIds":
						this._PricePlanIds = propertyNode.InnerText;
						continue;
					case "previewModule":
						this._PreviewModule = ObjectFactory.Create<PreviewModule>(propertyNode);
						continue;
					case "householdLimitationsId":
						this._HouseholdLimitationsId = ParseInt(propertyNode.InnerText);
						continue;
					case "gracePeriodMinutes":
						this._GracePeriodMinutes = ParseInt(propertyNode.InnerText);
						continue;
					case "premiumServices":
						this._PremiumServices = new List<PremiumService>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._PremiumServices.Add(ObjectFactory.Create<PremiumService>(arrayNode));
						}
						continue;
					case "maxViewsNumber":
						this._MaxViewsNumber = ParseInt(propertyNode.InnerText);
						continue;
					case "viewLifeCycle":
						this._ViewLifeCycle = ParseInt(propertyNode.InnerText);
						continue;
					case "waiverPeriod":
						this._WaiverPeriod = ParseInt(propertyNode.InnerText);
						continue;
					case "isWaiverEnabled":
						this._IsWaiverEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "userTypes":
						this._UserTypes = new List<OTTUserType>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._UserTypes.Add(ObjectFactory.Create<OTTUserType>(arrayNode));
						}
						continue;
					case "couponsGroups":
						this._CouponsGroups = new List<CouponsGroup>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._CouponsGroups.Add(ObjectFactory.Create<CouponsGroup>(arrayNode));
						}
						continue;
					case "productCodes":
						this._ProductCodes = new List<ProductCode>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._ProductCodes.Add(ObjectFactory.Create<ProductCode>(arrayNode));
						}
						continue;
					case "dependencyType":
						this._DependencyType = (SubscriptionDependencyType)StringEnum.Parse(typeof(SubscriptionDependencyType), propertyNode.InnerText);
						continue;
					case "externalId":
						this._ExternalId = propertyNode.InnerText;
						continue;
					case "isCancellationBlocked":
						this._IsCancellationBlocked = ParseBool(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaSubscription");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("channels", this._Channels);
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("fileTypes", this._FileTypes);
			kparams.AddIfNotNull("isRenewable", this._IsRenewable);
			kparams.AddIfNotNull("renewalsNumber", this._RenewalsNumber);
			kparams.AddIfNotNull("isInfiniteRenewal", this._IsInfiniteRenewal);
			kparams.AddIfNotNull("price", this._Price);
			kparams.AddIfNotNull("discountModule", this._DiscountModule);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("multilingualName", this._MultilingualName);
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("multilingualDescription", this._MultilingualDescription);
			kparams.AddIfNotNull("mediaId", this._MediaId);
			kparams.AddIfNotNull("prorityInOrder", this._ProrityInOrder);
			kparams.AddIfNotNull("pricePlanIds", this._PricePlanIds);
			kparams.AddIfNotNull("previewModule", this._PreviewModule);
			kparams.AddIfNotNull("householdLimitationsId", this._HouseholdLimitationsId);
			kparams.AddIfNotNull("gracePeriodMinutes", this._GracePeriodMinutes);
			kparams.AddIfNotNull("premiumServices", this._PremiumServices);
			kparams.AddIfNotNull("maxViewsNumber", this._MaxViewsNumber);
			kparams.AddIfNotNull("viewLifeCycle", this._ViewLifeCycle);
			kparams.AddIfNotNull("waiverPeriod", this._WaiverPeriod);
			kparams.AddIfNotNull("isWaiverEnabled", this._IsWaiverEnabled);
			kparams.AddIfNotNull("userTypes", this._UserTypes);
			kparams.AddIfNotNull("couponsGroups", this._CouponsGroups);
			kparams.AddIfNotNull("productCodes", this._ProductCodes);
			kparams.AddIfNotNull("dependencyType", this._DependencyType);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("isCancellationBlocked", this._IsCancellationBlocked);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case CHANNELS:
					return "Channels";
				case START_DATE:
					return "StartDate";
				case END_DATE:
					return "EndDate";
				case FILE_TYPES:
					return "FileTypes";
				case IS_RENEWABLE:
					return "IsRenewable";
				case RENEWALS_NUMBER:
					return "RenewalsNumber";
				case IS_INFINITE_RENEWAL:
					return "IsInfiniteRenewal";
				case PRICE:
					return "Price";
				case DISCOUNT_MODULE:
					return "DiscountModule";
				case NAME:
					return "Name";
				case MULTILINGUAL_NAME:
					return "MultilingualName";
				case DESCRIPTION:
					return "Description";
				case MULTILINGUAL_DESCRIPTION:
					return "MultilingualDescription";
				case MEDIA_ID:
					return "MediaId";
				case PRORITY_IN_ORDER:
					return "ProrityInOrder";
				case PRICE_PLAN_IDS:
					return "PricePlanIds";
				case PREVIEW_MODULE:
					return "PreviewModule";
				case HOUSEHOLD_LIMITATIONS_ID:
					return "HouseholdLimitationsId";
				case GRACE_PERIOD_MINUTES:
					return "GracePeriodMinutes";
				case PREMIUM_SERVICES:
					return "PremiumServices";
				case MAX_VIEWS_NUMBER:
					return "MaxViewsNumber";
				case VIEW_LIFE_CYCLE:
					return "ViewLifeCycle";
				case WAIVER_PERIOD:
					return "WaiverPeriod";
				case IS_WAIVER_ENABLED:
					return "IsWaiverEnabled";
				case USER_TYPES:
					return "UserTypes";
				case COUPONS_GROUPS:
					return "CouponsGroups";
				case PRODUCT_CODES:
					return "ProductCodes";
				case DEPENDENCY_TYPE:
					return "DependencyType";
				case EXTERNAL_ID:
					return "ExternalId";
				case IS_CANCELLATION_BLOCKED:
					return "IsCancellationBlocked";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

