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
		public const string TIME_ZONE_ID = "timeZoneId";
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
		private string _TimeZoneId = null;
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
		public string Code
		{
			get { return _Code; }
			set 
			{ 
				_Code = value;
				OnPropertyChanged("Code");
			}
		}
		[JsonProperty]
		public string MainLanguageCode
		{
			get { return _MainLanguageCode; }
			set 
			{ 
				_MainLanguageCode = value;
				OnPropertyChanged("MainLanguageCode");
			}
		}
		[JsonProperty]
		public string LanguagesCode
		{
			get { return _LanguagesCode; }
			set 
			{ 
				_LanguagesCode = value;
				OnPropertyChanged("LanguagesCode");
			}
		}
		[JsonProperty]
		public string Currency
		{
			get { return _Currency; }
			set 
			{ 
				_Currency = value;
				OnPropertyChanged("Currency");
			}
		}
		[JsonProperty]
		public string CurrencySign
		{
			get { return _CurrencySign; }
			set 
			{ 
				_CurrencySign = value;
				OnPropertyChanged("CurrencySign");
			}
		}
		[JsonProperty]
		public float VatPercent
		{
			get { return _VatPercent; }
			set 
			{ 
				_VatPercent = value;
				OnPropertyChanged("VatPercent");
			}
		}
		[JsonProperty]
		public string TimeZoneId
		{
			get { return _TimeZoneId; }
			set 
			{ 
				_TimeZoneId = value;
				OnPropertyChanged("TimeZoneId");
			}
		}
		#endregion

		#region CTor
		public Country()
		{
		}

		public Country(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["code"] != null)
			{
				this._Code = node["code"].Value<string>();
			}
			if(node["mainLanguageCode"] != null)
			{
				this._MainLanguageCode = node["mainLanguageCode"].Value<string>();
			}
			if(node["languagesCode"] != null)
			{
				this._LanguagesCode = node["languagesCode"].Value<string>();
			}
			if(node["currency"] != null)
			{
				this._Currency = node["currency"].Value<string>();
			}
			if(node["currencySign"] != null)
			{
				this._CurrencySign = node["currencySign"].Value<string>();
			}
			if(node["vatPercent"] != null)
			{
				this._VatPercent = ParseFloat(node["vatPercent"].Value<string>());
			}
			if(node["timeZoneId"] != null)
			{
				this._TimeZoneId = node["timeZoneId"].Value<string>();
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
			kparams.AddIfNotNull("timeZoneId", this._TimeZoneId);
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
				case TIME_ZONE_ID:
					return "TimeZoneId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

