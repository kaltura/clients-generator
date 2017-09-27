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
	public class Country : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string CODE = "code";
		public const string MAIN_LANGUAGE_CODE = "mainLanguageCode";
		public const string LANGUAGES_CODE = "languagesCode";
		public const string CURRENCY = "currency";
		public const string CURRENCY_SIGN = "currencySign";
		public const string VAT_PERCENT = "vatPercent";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _Name = null;
		private string _Code = null;
		private string _MainLanguageCode = null;
		private string _LanguagesCode = null;
		private string _Currency = null;
		private string _CurrencySign = null;
		private float _VatPercent = Single.MinValue;
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
		public string Code
		{
			get { return _Code; }
			set 
			{ 
				_Code = value;
				OnPropertyChanged("Code");
			}
		}
		public string MainLanguageCode
		{
			get { return _MainLanguageCode; }
			set 
			{ 
				_MainLanguageCode = value;
				OnPropertyChanged("MainLanguageCode");
			}
		}
		public string LanguagesCode
		{
			get { return _LanguagesCode; }
			set 
			{ 
				_LanguagesCode = value;
				OnPropertyChanged("LanguagesCode");
			}
		}
		public string Currency
		{
			get { return _Currency; }
			set 
			{ 
				_Currency = value;
				OnPropertyChanged("Currency");
			}
		}
		public string CurrencySign
		{
			get { return _CurrencySign; }
			set 
			{ 
				_CurrencySign = value;
				OnPropertyChanged("CurrencySign");
			}
		}
		public float VatPercent
		{
			get { return _VatPercent; }
			set 
			{ 
				_VatPercent = value;
				OnPropertyChanged("VatPercent");
			}
		}
		#endregion

		#region CTor
		public Country()
		{
		}

		public Country(XmlElement node) : base(node)
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
					case "code":
						this._Code = propertyNode.InnerText;
						continue;
					case "mainLanguageCode":
						this._MainLanguageCode = propertyNode.InnerText;
						continue;
					case "languagesCode":
						this._LanguagesCode = propertyNode.InnerText;
						continue;
					case "currency":
						this._Currency = propertyNode.InnerText;
						continue;
					case "currencySign":
						this._CurrencySign = propertyNode.InnerText;
						continue;
					case "vatPercent":
						this._VatPercent = ParseFloat(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaCountry");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("code", this._Code);
			kparams.AddIfNotNull("mainLanguageCode", this._MainLanguageCode);
			kparams.AddIfNotNull("languagesCode", this._LanguagesCode);
			kparams.AddIfNotNull("currency", this._Currency);
			kparams.AddIfNotNull("currencySign", this._CurrencySign);
			kparams.AddIfNotNull("vatPercent", this._VatPercent);
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
				case CODE:
					return "Code";
				case MAIN_LANGUAGE_CODE:
					return "MainLanguageCode";
				case LANGUAGES_CODE:
					return "LanguagesCode";
				case CURRENCY:
					return "Currency";
				case CURRENCY_SIGN:
					return "CurrencySign";
				case VAT_PERCENT:
					return "VatPercent";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

