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
	public class Collection : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string CHANNELS = "channels";
		public const string START_DATE = "startDate";
		public const string END_DATE = "endDate";
		public const string DISCOUNT_MODULE = "discountModule";
		public const string NAME = "name";
		public const string MULTILINGUAL_NAME = "multilingualName";
		public const string DESCRIPTION = "description";
		public const string MULTILINGUAL_DESCRIPTION = "multilingualDescription";
		public const string USAGE_MODULE = "usageModule";
		public const string COUPONS_GROUPS = "couponsGroups";
		public const string EXTERNAL_ID = "externalId";
		public const string PRODUCT_CODES = "productCodes";
		public const string PRICE_DETAILS_ID = "priceDetailsId";
		#endregion

		#region Private Fields
		private string _Id = null;
		private IList<BaseChannel> _Channels;
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		private DiscountModule _DiscountModule;
		private string _Name = null;
		private IList<TranslationToken> _MultilingualName;
		private string _Description = null;
		private IList<TranslationToken> _MultilingualDescription;
		private UsageModule _UsageModule;
		private IList<CouponsGroup> _CouponsGroups;
		private string _ExternalId = null;
		private IList<ProductCode> _ProductCodes;
		private long _PriceDetailsId = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public string Id
		{
			get { return _Id; }
			set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public IList<BaseChannel> Channels
		{
			get { return _Channels; }
			set 
			{ 
				_Channels = value;
				OnPropertyChanged("Channels");
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
		public DiscountModule DiscountModule
		{
			get { return _DiscountModule; }
			set 
			{ 
				_DiscountModule = value;
				OnPropertyChanged("DiscountModule");
			}
		}
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			private set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public IList<TranslationToken> MultilingualName
		{
			get { return _MultilingualName; }
			set 
			{ 
				_MultilingualName = value;
				OnPropertyChanged("MultilingualName");
			}
		}
		[JsonProperty]
		public string Description
		{
			get { return _Description; }
			private set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		[JsonProperty]
		public IList<TranslationToken> MultilingualDescription
		{
			get { return _MultilingualDescription; }
			set 
			{ 
				_MultilingualDescription = value;
				OnPropertyChanged("MultilingualDescription");
			}
		}
		[JsonProperty]
		public UsageModule UsageModule
		{
			get { return _UsageModule; }
			set 
			{ 
				_UsageModule = value;
				OnPropertyChanged("UsageModule");
			}
		}
		[JsonProperty]
		public IList<CouponsGroup> CouponsGroups
		{
			get { return _CouponsGroups; }
			set 
			{ 
				_CouponsGroups = value;
				OnPropertyChanged("CouponsGroups");
			}
		}
		[JsonProperty]
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		[JsonProperty]
		public IList<ProductCode> ProductCodes
		{
			get { return _ProductCodes; }
			set 
			{ 
				_ProductCodes = value;
				OnPropertyChanged("ProductCodes");
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
		public Collection()
		{
		}

		public Collection(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["channels"] != null)
			{
				this._Channels = new List<BaseChannel>();
				foreach(var arrayNode in node["channels"].Children())
				{
					this._Channels.Add(ObjectFactory.Create<BaseChannel>(arrayNode));
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
			if(node["discountModule"] != null)
			{
				this._DiscountModule = ObjectFactory.Create<DiscountModule>(node["discountModule"]);
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["multilingualName"] != null)
			{
				this._MultilingualName = new List<TranslationToken>();
				foreach(var arrayNode in node["multilingualName"].Children())
				{
					this._MultilingualName.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
				}
			}
			if(node["description"] != null)
			{
				this._Description = node["description"].Value<string>();
			}
			if(node["multilingualDescription"] != null)
			{
				this._MultilingualDescription = new List<TranslationToken>();
				foreach(var arrayNode in node["multilingualDescription"].Children())
				{
					this._MultilingualDescription.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
				}
			}
			if(node["usageModule"] != null)
			{
				this._UsageModule = ObjectFactory.Create<UsageModule>(node["usageModule"]);
			}
			if(node["couponsGroups"] != null)
			{
				this._CouponsGroups = new List<CouponsGroup>();
				foreach(var arrayNode in node["couponsGroups"].Children())
				{
					this._CouponsGroups.Add(ObjectFactory.Create<CouponsGroup>(arrayNode));
				}
			}
			if(node["externalId"] != null)
			{
				this._ExternalId = node["externalId"].Value<string>();
			}
			if(node["productCodes"] != null)
			{
				this._ProductCodes = new List<ProductCode>();
				foreach(var arrayNode in node["productCodes"].Children())
				{
					this._ProductCodes.Add(ObjectFactory.Create<ProductCode>(arrayNode));
				}
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
				kparams.AddReplace("objectType", "KalturaCollection");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("channels", this._Channels);
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("discountModule", this._DiscountModule);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("multilingualName", this._MultilingualName);
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("multilingualDescription", this._MultilingualDescription);
			kparams.AddIfNotNull("usageModule", this._UsageModule);
			kparams.AddIfNotNull("couponsGroups", this._CouponsGroups);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("productCodes", this._ProductCodes);
			kparams.AddIfNotNull("priceDetailsId", this._PriceDetailsId);
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
				case USAGE_MODULE:
					return "UsageModule";
				case COUPONS_GROUPS:
					return "CouponsGroups";
				case EXTERNAL_ID:
					return "ExternalId";
				case PRODUCT_CODES:
					return "ProductCodes";
				case PRICE_DETAILS_ID:
					return "PriceDetailsId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

