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
	public class LanguageFilter : Filter
	{
		#region Constants
		public const string CODE_IN = "codeIn";
		public const string EXCLUDE_PARTNER = "excludePartner";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _CodeIn = null;
		private bool? _ExcludePartner = null;
		private LanguageOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string CodeIn
		{
			get { return _CodeIn; }
			set 
			{ 
				_CodeIn = value;
				OnPropertyChanged("CodeIn");
			}
		}
		[JsonProperty]
		public bool? ExcludePartner
		{
			get { return _ExcludePartner; }
			set 
			{ 
				_ExcludePartner = value;
				OnPropertyChanged("ExcludePartner");
			}
		}
		[JsonProperty]
		public new LanguageOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		#endregion

		#region CTor
		public LanguageFilter()
		{
		}

		public LanguageFilter(JToken node) : base(node)
		{
			if(node["codeIn"] != null)
			{
				this._CodeIn = node["codeIn"].Value<string>();
			}
			if(node["excludePartner"] != null)
			{
				this._ExcludePartner = ParseBool(node["excludePartner"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (LanguageOrderBy)StringEnum.Parse(typeof(LanguageOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaLanguageFilter");
			kparams.AddIfNotNull("codeIn", this._CodeIn);
			kparams.AddIfNotNull("excludePartner", this._ExcludePartner);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CODE_IN:
					return "CodeIn";
				case EXCLUDE_PARTNER:
					return "ExcludePartner";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

