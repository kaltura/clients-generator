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
// Copyright (C) 2006-2020  Kaltura Inc.
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
	public class MonetizationCondition : BaseSegmentCondition
	{
		#region Constants
		public const string MIN_VALUE = "minValue";
		public const string MAX_VALUE = "maxValue";
		public const string DAYS = "days";
		public const string TYPE = "type";
		public const string OPERATOR = "operator";
		public const string BUSINESS_MODULE_ID_IN = "businessModuleIdIn";
		#endregion

		#region Private Fields
		private int _MinValue = Int32.MinValue;
		private int _MaxValue = Int32.MinValue;
		private int _Days = Int32.MinValue;
		private MonetizationType _Type = null;
		private MathemticalOperatorType _Operator = null;
		private string _BusinessModuleIdIn = null;
		#endregion

		#region Properties
		[JsonProperty]
		public int MinValue
		{
			get { return _MinValue; }
			set 
			{ 
				_MinValue = value;
				OnPropertyChanged("MinValue");
			}
		}
		[JsonProperty]
		public int MaxValue
		{
			get { return _MaxValue; }
			set 
			{ 
				_MaxValue = value;
				OnPropertyChanged("MaxValue");
			}
		}
		[JsonProperty]
		public int Days
		{
			get { return _Days; }
			set 
			{ 
				_Days = value;
				OnPropertyChanged("Days");
			}
		}
		[JsonProperty]
		public MonetizationType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		[JsonProperty]
		public MathemticalOperatorType Operator
		{
			get { return _Operator; }
			set 
			{ 
				_Operator = value;
				OnPropertyChanged("Operator");
			}
		}
		[JsonProperty]
		public string BusinessModuleIdIn
		{
			get { return _BusinessModuleIdIn; }
			set 
			{ 
				_BusinessModuleIdIn = value;
				OnPropertyChanged("BusinessModuleIdIn");
			}
		}
		#endregion

		#region CTor
		public MonetizationCondition()
		{
		}

		public MonetizationCondition(JToken node) : base(node)
		{
			if(node["minValue"] != null)
			{
				this._MinValue = ParseInt(node["minValue"].Value<string>());
			}
			if(node["maxValue"] != null)
			{
				this._MaxValue = ParseInt(node["maxValue"].Value<string>());
			}
			if(node["days"] != null)
			{
				this._Days = ParseInt(node["days"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = (MonetizationType)StringEnum.Parse(typeof(MonetizationType), node["type"].Value<string>());
			}
			if(node["operator"] != null)
			{
				this._Operator = (MathemticalOperatorType)StringEnum.Parse(typeof(MathemticalOperatorType), node["operator"].Value<string>());
			}
			if(node["businessModuleIdIn"] != null)
			{
				this._BusinessModuleIdIn = node["businessModuleIdIn"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaMonetizationCondition");
			kparams.AddIfNotNull("minValue", this._MinValue);
			kparams.AddIfNotNull("maxValue", this._MaxValue);
			kparams.AddIfNotNull("days", this._Days);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("operator", this._Operator);
			kparams.AddIfNotNull("businessModuleIdIn", this._BusinessModuleIdIn);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case MIN_VALUE:
					return "MinValue";
				case MAX_VALUE:
					return "MaxValue";
				case DAYS:
					return "Days";
				case TYPE:
					return "Type";
				case OPERATOR:
					return "Operator";
				case BUSINESS_MODULE_ID_IN:
					return "BusinessModuleIdIn";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

