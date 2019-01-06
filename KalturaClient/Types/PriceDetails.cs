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
	public class PriceDetails : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string PRICE = "price";
		public const string MULTI_CURRENCY_PRICE = "multiCurrencyPrice";
		public const string DESCRIPTIONS = "descriptions";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _Name = null;
		private Price _Price;
		private IList<Price> _MultiCurrencyPrice;
		private IList<TranslationToken> _Descriptions;
		#endregion

		#region Properties
		public int Id
		{
			get { return _Id; }
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
		public Price Price
		{
			get { return _Price; }
		}
		public IList<Price> MultiCurrencyPrice
		{
			get { return _MultiCurrencyPrice; }
			set 
			{ 
				_MultiCurrencyPrice = value;
				OnPropertyChanged("MultiCurrencyPrice");
			}
		}
		public IList<TranslationToken> Descriptions
		{
			get { return _Descriptions; }
			set 
			{ 
				_Descriptions = value;
				OnPropertyChanged("Descriptions");
			}
		}
		#endregion

		#region CTor
		public PriceDetails()
		{
		}

		public PriceDetails(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "price":
						this._Price = ObjectFactory.Create<Price>(propertyNode);
						continue;
					case "multiCurrencyPrice":
						this._MultiCurrencyPrice = new List<Price>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._MultiCurrencyPrice.Add(ObjectFactory.Create<Price>(arrayNode));
						}
						continue;
					case "descriptions":
						this._Descriptions = new List<TranslationToken>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Descriptions.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
						}
						continue;
				}
			}
		}

		public PriceDetails(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<int>("id");
			    this._Name = data.TryGetValueSafe<string>("name");
			    this._Price = ObjectFactory.Create<Price>(data.TryGetValueSafe<IDictionary<string,object>>("price"));
			    this._MultiCurrencyPrice = new List<Price>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("multiCurrencyPrice", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._MultiCurrencyPrice.Add(ObjectFactory.Create<Price>((IDictionary<string,object>)dataDictionary));
			    }
			    this._Descriptions = new List<TranslationToken>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("descriptions", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._Descriptions.Add(ObjectFactory.Create<TranslationToken>((IDictionary<string,object>)dataDictionary));
			    }
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPriceDetails");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("price", this._Price);
			kparams.AddIfNotNull("multiCurrencyPrice", this._MultiCurrencyPrice);
			kparams.AddIfNotNull("descriptions", this._Descriptions);
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
				case MULTI_CURRENCY_PRICE:
					return "MultiCurrencyPrice";
				case DESCRIPTIONS:
					return "Descriptions";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

