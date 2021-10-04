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
	public class TvmRuleFilter : Filter
	{
		#region Constants
		public const string RULE_TYPE_EQUAL = "ruleTypeEqual";
		public const string NAME_EQUAL = "nameEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private TvmRuleType _RuleTypeEqual = null;
		private string _NameEqual = null;
		private TvmRuleOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public TvmRuleType RuleTypeEqual
		{
			get { return _RuleTypeEqual; }
			set 
			{ 
				_RuleTypeEqual = value;
				OnPropertyChanged("RuleTypeEqual");
			}
		}
		[JsonProperty]
		public string NameEqual
		{
			get { return _NameEqual; }
			set 
			{ 
				_NameEqual = value;
				OnPropertyChanged("NameEqual");
			}
		}
		[JsonProperty]
		public new TvmRuleOrderBy OrderBy
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
		public TvmRuleFilter()
		{
		}

		public TvmRuleFilter(JToken node) : base(node)
		{
			if(node["ruleTypeEqual"] != null)
			{
				this._RuleTypeEqual = (TvmRuleType)StringEnum.Parse(typeof(TvmRuleType), node["ruleTypeEqual"].Value<string>());
			}
			if(node["nameEqual"] != null)
			{
				this._NameEqual = node["nameEqual"].Value<string>();
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (TvmRuleOrderBy)StringEnum.Parse(typeof(TvmRuleOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaTvmRuleFilter");
			kparams.AddIfNotNull("ruleTypeEqual", this._RuleTypeEqual);
			kparams.AddIfNotNull("nameEqual", this._NameEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case RULE_TYPE_EQUAL:
					return "RuleTypeEqual";
				case NAME_EQUAL:
					return "NameEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

