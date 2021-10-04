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
	public class Ppv : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string PRICE = "price";
		public const string FILE_TYPES = "fileTypes";
		public const string DISCOUNT_MODULE = "discountModule";
		public const string COUPONS_GROUP = "couponsGroup";
		public const string DESCRIPTIONS = "descriptions";
		public const string PRODUCT_CODE = "productCode";
		public const string IS_SUBSCRIPTION_ONLY = "isSubscriptionOnly";
		public const string FIRST_DEVICE_LIMITATION = "firstDeviceLimitation";
		public const string USAGE_MODULE = "usageModule";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Name = null;
		private PriceDetails _Price;
		private IList<IntegerValue> _FileTypes;
		private DiscountModule _DiscountModule;
		private CouponsGroup _CouponsGroup;
		private IList<TranslationToken> _Descriptions;
		private string _ProductCode = null;
		private bool? _IsSubscriptionOnly = null;
		private bool? _FirstDeviceLimitation = null;
		private UsageModule _UsageModule;
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
		public PriceDetails Price
		{
			get { return _Price; }
			set 
			{ 
				_Price = value;
				OnPropertyChanged("Price");
			}
		}
		[JsonProperty]
		public IList<IntegerValue> FileTypes
		{
			get { return _FileTypes; }
			set 
			{ 
				_FileTypes = value;
				OnPropertyChanged("FileTypes");
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
		public CouponsGroup CouponsGroup
		{
			get { return _CouponsGroup; }
			set 
			{ 
				_CouponsGroup = value;
				OnPropertyChanged("CouponsGroup");
			}
		}
		[JsonProperty]
		public IList<TranslationToken> Descriptions
		{
			get { return _Descriptions; }
			set 
			{ 
				_Descriptions = value;
				OnPropertyChanged("Descriptions");
			}
		}
		[JsonProperty]
		public string ProductCode
		{
			get { return _ProductCode; }
			set 
			{ 
				_ProductCode = value;
				OnPropertyChanged("ProductCode");
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
		public bool? FirstDeviceLimitation
		{
			get { return _FirstDeviceLimitation; }
			set 
			{ 
				_FirstDeviceLimitation = value;
				OnPropertyChanged("FirstDeviceLimitation");
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
		#endregion

		#region CTor
		public Ppv()
		{
		}

		public Ppv(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["price"] != null)
			{
				this._Price = ObjectFactory.Create<PriceDetails>(node["price"]);
			}
			if(node["fileTypes"] != null)
			{
				this._FileTypes = new List<IntegerValue>();
				foreach(var arrayNode in node["fileTypes"].Children())
				{
					this._FileTypes.Add(ObjectFactory.Create<IntegerValue>(arrayNode));
				}
			}
			if(node["discountModule"] != null)
			{
				this._DiscountModule = ObjectFactory.Create<DiscountModule>(node["discountModule"]);
			}
			if(node["couponsGroup"] != null)
			{
				this._CouponsGroup = ObjectFactory.Create<CouponsGroup>(node["couponsGroup"]);
			}
			if(node["descriptions"] != null)
			{
				this._Descriptions = new List<TranslationToken>();
				foreach(var arrayNode in node["descriptions"].Children())
				{
					this._Descriptions.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
				}
			}
			if(node["productCode"] != null)
			{
				this._ProductCode = node["productCode"].Value<string>();
			}
			if(node["isSubscriptionOnly"] != null)
			{
				this._IsSubscriptionOnly = ParseBool(node["isSubscriptionOnly"].Value<string>());
			}
			if(node["firstDeviceLimitation"] != null)
			{
				this._FirstDeviceLimitation = ParseBool(node["firstDeviceLimitation"].Value<string>());
			}
			if(node["usageModule"] != null)
			{
				this._UsageModule = ObjectFactory.Create<UsageModule>(node["usageModule"]);
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPpv");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("price", this._Price);
			kparams.AddIfNotNull("fileTypes", this._FileTypes);
			kparams.AddIfNotNull("discountModule", this._DiscountModule);
			kparams.AddIfNotNull("couponsGroup", this._CouponsGroup);
			kparams.AddIfNotNull("descriptions", this._Descriptions);
			kparams.AddIfNotNull("productCode", this._ProductCode);
			kparams.AddIfNotNull("isSubscriptionOnly", this._IsSubscriptionOnly);
			kparams.AddIfNotNull("firstDeviceLimitation", this._FirstDeviceLimitation);
			kparams.AddIfNotNull("usageModule", this._UsageModule);
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
				case PRICE:
					return "Price";
				case FILE_TYPES:
					return "FileTypes";
				case DISCOUNT_MODULE:
					return "DiscountModule";
				case COUPONS_GROUP:
					return "CouponsGroup";
				case DESCRIPTIONS:
					return "Descriptions";
				case PRODUCT_CODE:
					return "ProductCode";
				case IS_SUBSCRIPTION_ONLY:
					return "IsSubscriptionOnly";
				case FIRST_DEVICE_LIMITATION:
					return "FirstDeviceLimitation";
				case USAGE_MODULE:
					return "UsageModule";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

