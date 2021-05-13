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
	public class CatalogPartnerConfig : PartnerConfiguration
	{
		#region Constants
		public const string SINGLE_MULTILINGUAL_MODE = "singleMultilingualMode";
		public const string CATEGORY_MANAGEMENT = "categoryManagement";
		public const string EPG_MULTILINGUAL_FALLBACK_SUPPORT = "epgMultilingualFallbackSupport";
		#endregion

		#region Private Fields
		private bool? _SingleMultilingualMode = null;
		private CategoryManagement _CategoryManagement;
		private bool? _EpgMultilingualFallbackSupport = null;
		#endregion

		#region Properties
		[JsonProperty]
		public bool? SingleMultilingualMode
		{
			get { return _SingleMultilingualMode; }
			set 
			{ 
				_SingleMultilingualMode = value;
				OnPropertyChanged("SingleMultilingualMode");
			}
		}
		[JsonProperty]
		public CategoryManagement CategoryManagement
		{
			get { return _CategoryManagement; }
			set 
			{ 
				_CategoryManagement = value;
				OnPropertyChanged("CategoryManagement");
			}
		}
		[JsonProperty]
		public bool? EpgMultilingualFallbackSupport
		{
			get { return _EpgMultilingualFallbackSupport; }
			set 
			{ 
				_EpgMultilingualFallbackSupport = value;
				OnPropertyChanged("EpgMultilingualFallbackSupport");
			}
		}
		#endregion

		#region CTor
		public CatalogPartnerConfig()
		{
		}

		public CatalogPartnerConfig(JToken node) : base(node)
		{
			if(node["singleMultilingualMode"] != null)
			{
				this._SingleMultilingualMode = ParseBool(node["singleMultilingualMode"].Value<string>());
			}
			if(node["categoryManagement"] != null)
			{
				this._CategoryManagement = ObjectFactory.Create<CategoryManagement>(node["categoryManagement"]);
			}
			if(node["epgMultilingualFallbackSupport"] != null)
			{
				this._EpgMultilingualFallbackSupport = ParseBool(node["epgMultilingualFallbackSupport"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCatalogPartnerConfig");
			kparams.AddIfNotNull("singleMultilingualMode", this._SingleMultilingualMode);
			kparams.AddIfNotNull("categoryManagement", this._CategoryManagement);
			kparams.AddIfNotNull("epgMultilingualFallbackSupport", this._EpgMultilingualFallbackSupport);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SINGLE_MULTILINGUAL_MODE:
					return "SingleMultilingualMode";
				case CATEGORY_MANAGEMENT:
					return "CategoryManagement";
				case EPG_MULTILINGUAL_FALLBACK_SUPPORT:
					return "EpgMultilingualFallbackSupport";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

