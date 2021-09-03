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
		[JsonProperty]
		public int Id
		{
			get { return _Id; }
			private set 
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
		public Price Price
		{
			get { return _Price; }
			private set 
			{ 
				_Price = value;
				OnPropertyChanged("Price");
			}
		}
		[JsonProperty]
		public IList<Price> MultiCurrencyPrice
		{
			get { return _MultiCurrencyPrice; }
			set 
			{ 
				_MultiCurrencyPrice = value;
				OnPropertyChanged("MultiCurrencyPrice");
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
		#endregion

		#region CTor
		public PriceDetails()
		{
		}

		public PriceDetails(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["price"] != null)
			{
				this._Price = ObjectFactory.Create<Price>(node["price"]);
			}
			if(node["multiCurrencyPrice"] != null)
			{
				this._MultiCurrencyPrice = new List<Price>();
				foreach(var arrayNode in node["multiCurrencyPrice"].Children())
				{
					this._MultiCurrencyPrice.Add(ObjectFactory.Create<Price>(arrayNode));
				}
			}
			if(node["descriptions"] != null)
			{
				this._Descriptions = new List<TranslationToken>();
				foreach(var arrayNode in node["descriptions"].Children())
				{
					this._Descriptions.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
				}
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

