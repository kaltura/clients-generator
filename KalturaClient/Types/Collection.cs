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
		private MultilingualString _MultilingualName;
		private string _Description = null;
		private MultilingualString _MultilingualDescription;
		private UsageModule _UsageModule;
		private IList<CouponsGroup> _CouponsGroups;
		private string _ExternalId = null;
		private IList<ProductCode> _ProductCodes;
		private long _PriceDetailsId = long.MinValue;
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
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		public MultilingualString MultilingualName
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
			set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		public MultilingualString MultilingualDescription
		{
			get { return _MultilingualDescription; }
			set 
			{ 
				_MultilingualDescription = value;
				OnPropertyChanged("MultilingualDescription");
			}
		}
		public UsageModule UsageModule
		{
			get { return _UsageModule; }
			set 
			{ 
				_UsageModule = value;
				OnPropertyChanged("UsageModule");
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
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
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

		public Collection(XmlElement node) : base(node)
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
					case "discountModule":
						this._DiscountModule = ObjectFactory.Create<DiscountModule>(propertyNode);
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "multilingualName":
						this._MultilingualName = ObjectFactory.Create<MultilingualString>(propertyNode);
						continue;
					case "description":
						this._Description = propertyNode.InnerText;
						continue;
					case "multilingualDescription":
						this._MultilingualDescription = ObjectFactory.Create<MultilingualString>(propertyNode);
						continue;
					case "usageModule":
						this._UsageModule = ObjectFactory.Create<UsageModule>(propertyNode);
						continue;
					case "couponsGroups":
						this._CouponsGroups = new List<CouponsGroup>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._CouponsGroups.Add(ObjectFactory.Create<CouponsGroup>(arrayNode));
						}
						continue;
					case "externalId":
						this._ExternalId = propertyNode.InnerText;
						continue;
					case "productCodes":
						this._ProductCodes = new List<ProductCode>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._ProductCodes.Add(ObjectFactory.Create<ProductCode>(arrayNode));
						}
						continue;
					case "priceDetailsId":
						this._PriceDetailsId = ParseLong(propertyNode.InnerText);
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

