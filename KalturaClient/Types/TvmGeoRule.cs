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
	public class TvmGeoRule : TvmRule
	{
		#region Constants
		public const string ONLY_OR_BUT = "onlyOrBut";
		public const string COUNTRY_IDS = "countryIds";
		public const string PROXY_RULE_ID = "proxyRuleId";
		public const string PROXY_RULE_NAME = "proxyRuleName";
		public const string PROXY_LEVEL_ID = "proxyLevelId";
		public const string PROXY_LEVEL_NAME = "proxyLevelName";
		#endregion

		#region Private Fields
		private bool? _OnlyOrBut = null;
		private string _CountryIds = null;
		private int _ProxyRuleId = Int32.MinValue;
		private string _ProxyRuleName = null;
		private int _ProxyLevelId = Int32.MinValue;
		private string _ProxyLevelName = null;
		#endregion

		#region Properties
		[JsonProperty]
		public bool? OnlyOrBut
		{
			get { return _OnlyOrBut; }
			set 
			{ 
				_OnlyOrBut = value;
				OnPropertyChanged("OnlyOrBut");
			}
		}
		[JsonProperty]
		public string CountryIds
		{
			get { return _CountryIds; }
			set 
			{ 
				_CountryIds = value;
				OnPropertyChanged("CountryIds");
			}
		}
		[JsonProperty]
		public int ProxyRuleId
		{
			get { return _ProxyRuleId; }
			set 
			{ 
				_ProxyRuleId = value;
				OnPropertyChanged("ProxyRuleId");
			}
		}
		[JsonProperty]
		public string ProxyRuleName
		{
			get { return _ProxyRuleName; }
			set 
			{ 
				_ProxyRuleName = value;
				OnPropertyChanged("ProxyRuleName");
			}
		}
		[JsonProperty]
		public int ProxyLevelId
		{
			get { return _ProxyLevelId; }
			set 
			{ 
				_ProxyLevelId = value;
				OnPropertyChanged("ProxyLevelId");
			}
		}
		[JsonProperty]
		public string ProxyLevelName
		{
			get { return _ProxyLevelName; }
			set 
			{ 
				_ProxyLevelName = value;
				OnPropertyChanged("ProxyLevelName");
			}
		}
		#endregion

		#region CTor
		public TvmGeoRule()
		{
		}

		public TvmGeoRule(JToken node) : base(node)
		{
			if(node["onlyOrBut"] != null)
			{
				this._OnlyOrBut = ParseBool(node["onlyOrBut"].Value<string>());
			}
			if(node["countryIds"] != null)
			{
				this._CountryIds = node["countryIds"].Value<string>();
			}
			if(node["proxyRuleId"] != null)
			{
				this._ProxyRuleId = ParseInt(node["proxyRuleId"].Value<string>());
			}
			if(node["proxyRuleName"] != null)
			{
				this._ProxyRuleName = node["proxyRuleName"].Value<string>();
			}
			if(node["proxyLevelId"] != null)
			{
				this._ProxyLevelId = ParseInt(node["proxyLevelId"].Value<string>());
			}
			if(node["proxyLevelName"] != null)
			{
				this._ProxyLevelName = node["proxyLevelName"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaTvmGeoRule");
			kparams.AddIfNotNull("onlyOrBut", this._OnlyOrBut);
			kparams.AddIfNotNull("countryIds", this._CountryIds);
			kparams.AddIfNotNull("proxyRuleId", this._ProxyRuleId);
			kparams.AddIfNotNull("proxyRuleName", this._ProxyRuleName);
			kparams.AddIfNotNull("proxyLevelId", this._ProxyLevelId);
			kparams.AddIfNotNull("proxyLevelName", this._ProxyLevelName);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ONLY_OR_BUT:
					return "OnlyOrBut";
				case COUNTRY_IDS:
					return "CountryIds";
				case PROXY_RULE_ID:
					return "ProxyRuleId";
				case PROXY_RULE_NAME:
					return "ProxyRuleName";
				case PROXY_LEVEL_ID:
					return "ProxyLevelId";
				case PROXY_LEVEL_NAME:
					return "ProxyLevelName";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

