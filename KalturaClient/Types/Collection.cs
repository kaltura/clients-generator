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
		public const string PRICE = "price";
		public const string DISCOUNT_MODULE = "discountModule";
		public const string NAME = "name";
		public const string MULTILINGUAL_NAME = "multilingualName";
		public const string DESCRIPTION = "description";
		public const string MULTILINGUAL_DESCRIPTION = "multilingualDescription";
		public const string PRICE_PLAN_IDS = "pricePlanIds";
		public const string COUPONS_GROUPS = "couponsGroups";
		public const string EXTERNAL_ID = "externalId";
		#endregion

		#region Private Fields
		private string _Id = null;
		private IList<BaseChannel> _Channels;
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		private PriceDetails _Price;
		private DiscountModule _DiscountModule;
		private string _Name = null;
		private MultilingualString _MultilingualName;
		private string _Description = null;
		private MultilingualString _MultilingualDescription;
		private string _PricePlanIds = null;
		private IList<CouponsGroup> _CouponsGroups;
		private string _ExternalId = null;
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
		public string PricePlanIds
		{
			get { return _PricePlanIds; }
			set 
			{ 
				_PricePlanIds = value;
				OnPropertyChanged("PricePlanIds");
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
						this._MultilingualName = ObjectFactory.Create<MultilingualString>(propertyNode);
						continue;
					case "description":
						this._Description = propertyNode.InnerText;
						continue;
					case "multilingualDescription":
						this._MultilingualDescription = ObjectFactory.Create<MultilingualString>(propertyNode);
						continue;
					case "pricePlanIds":
						this._PricePlanIds = propertyNode.InnerText;
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
			kparams.AddIfNotNull("price", this._Price);
			kparams.AddIfNotNull("discountModule", this._DiscountModule);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("multilingualName", this._MultilingualName);
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("multilingualDescription", this._MultilingualDescription);
			kparams.AddIfNotNull("pricePlanIds", this._PricePlanIds);
			kparams.AddIfNotNull("couponsGroups", this._CouponsGroups);
			kparams.AddIfNotNull("externalId", this._ExternalId);
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
				case PRICE_PLAN_IDS:
					return "PricePlanIds";
				case COUPONS_GROUPS:
					return "CouponsGroups";
				case EXTERNAL_ID:
					return "ExternalId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

