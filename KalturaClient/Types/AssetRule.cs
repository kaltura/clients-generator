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
	public class AssetRule : AssetRuleBase
	{
		#region Constants
		public const string CONDITIONS = "conditions";
		public const string ACTIONS = "actions";
		public const string STATUS = "status";
		#endregion

		#region Private Fields
		private IList<Condition> _Conditions;
		private IList<AssetRuleAction> _Actions;
		private AssetRuleStatus _Status = null;
		#endregion

		#region Properties
		[JsonProperty]
		public IList<Condition> Conditions
		{
			get { return _Conditions; }
			set 
			{ 
				_Conditions = value;
				OnPropertyChanged("Conditions");
			}
		}
		[JsonProperty]
		public IList<AssetRuleAction> Actions
		{
			get { return _Actions; }
			set 
			{ 
				_Actions = value;
				OnPropertyChanged("Actions");
			}
		}
		[JsonProperty]
		public AssetRuleStatus Status
		{
			get { return _Status; }
			private set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		#endregion

		#region CTor
		public AssetRule()
		{
		}

		public AssetRule(JToken node) : base(node)
		{
			if(node["conditions"] != null)
			{
				this._Conditions = new List<Condition>();
				foreach(var arrayNode in node["conditions"].Children())
				{
					this._Conditions.Add(ObjectFactory.Create<Condition>(arrayNode));
				}
			}
			if(node["actions"] != null)
			{
				this._Actions = new List<AssetRuleAction>();
				foreach(var arrayNode in node["actions"].Children())
				{
					this._Actions.Add(ObjectFactory.Create<AssetRuleAction>(arrayNode));
				}
			}
			if(node["status"] != null)
			{
				this._Status = (AssetRuleStatus)StringEnum.Parse(typeof(AssetRuleStatus), node["status"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetRule");
			kparams.AddIfNotNull("conditions", this._Conditions);
			kparams.AddIfNotNull("actions", this._Actions);
			kparams.AddIfNotNull("status", this._Status);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CONDITIONS:
					return "Conditions";
				case ACTIONS:
					return "Actions";
				case STATUS:
					return "Status";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

