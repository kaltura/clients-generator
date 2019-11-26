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
// Copyright (C) 2006-2019  Kaltura Inc.
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
	public class AssetRuleFilter : Filter
	{
		#region Constants
		public const string CONDITIONS_CONTAIN_TYPE = "conditionsContainType";
		public const string ASSET_APPLIED = "assetApplied";
		public const string ACTIONS_CONTAIN_TYPE = "actionsContainType";
		public const string ASSET_RULE_ID_EQUAL = "assetRuleIdEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private RuleConditionType _ConditionsContainType = null;
		private SlimAsset _AssetApplied;
		private RuleActionType _ActionsContainType = null;
		private long _AssetRuleIdEqual = long.MinValue;
		private AssetRuleOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public RuleConditionType ConditionsContainType
		{
			get { return _ConditionsContainType; }
			set 
			{ 
				_ConditionsContainType = value;
				OnPropertyChanged("ConditionsContainType");
			}
		}
		[JsonProperty]
		public SlimAsset AssetApplied
		{
			get { return _AssetApplied; }
			set 
			{ 
				_AssetApplied = value;
				OnPropertyChanged("AssetApplied");
			}
		}
		[JsonProperty]
		public RuleActionType ActionsContainType
		{
			get { return _ActionsContainType; }
			set 
			{ 
				_ActionsContainType = value;
				OnPropertyChanged("ActionsContainType");
			}
		}
		[JsonProperty]
		public long AssetRuleIdEqual
		{
			get { return _AssetRuleIdEqual; }
			set 
			{ 
				_AssetRuleIdEqual = value;
				OnPropertyChanged("AssetRuleIdEqual");
			}
		}
		[JsonProperty]
		public new AssetRuleOrderBy OrderBy
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
		public AssetRuleFilter()
		{
		}

		public AssetRuleFilter(JToken node) : base(node)
		{
			if(node["conditionsContainType"] != null)
			{
				this._ConditionsContainType = (RuleConditionType)StringEnum.Parse(typeof(RuleConditionType), node["conditionsContainType"].Value<string>());
			}
			if(node["assetApplied"] != null)
			{
				this._AssetApplied = ObjectFactory.Create<SlimAsset>(node["assetApplied"]);
			}
			if(node["actionsContainType"] != null)
			{
				this._ActionsContainType = (RuleActionType)StringEnum.Parse(typeof(RuleActionType), node["actionsContainType"].Value<string>());
			}
			if(node["assetRuleIdEqual"] != null)
			{
				this._AssetRuleIdEqual = ParseLong(node["assetRuleIdEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (AssetRuleOrderBy)StringEnum.Parse(typeof(AssetRuleOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetRuleFilter");
			kparams.AddIfNotNull("conditionsContainType", this._ConditionsContainType);
			kparams.AddIfNotNull("assetApplied", this._AssetApplied);
			kparams.AddIfNotNull("actionsContainType", this._ActionsContainType);
			kparams.AddIfNotNull("assetRuleIdEqual", this._AssetRuleIdEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CONDITIONS_CONTAIN_TYPE:
					return "ConditionsContainType";
				case ASSET_APPLIED:
					return "AssetApplied";
				case ACTIONS_CONTAIN_TYPE:
					return "ActionsContainType";
				case ASSET_RULE_ID_EQUAL:
					return "AssetRuleIdEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

