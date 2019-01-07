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
	public class MonetizationSource : SegmentSource
	{
		#region Constants
		public const string TYPE = "type";
		public const string OPERATOR = "operator";
		public const string DAYS = "days";
		#endregion

		#region Private Fields
		private MonetizationType _Type = null;
		private MathemticalOperatorType _Operator = null;
		private int _Days = Int32.MinValue;
		#endregion

		#region Properties
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
		public int Days
		{
			get { return _Days; }
			set 
			{ 
				_Days = value;
				OnPropertyChanged("Days");
			}
		}
		#endregion

		#region CTor
		public MonetizationSource()
		{
		}

		public MonetizationSource(JToken node) : base(node)
		{
			if(node["type"] != null)
			{
				this._Type = (MonetizationType)StringEnum.Parse(typeof(MonetizationType), node["type"].Value<string>());
			}
			if(node["operator"] != null)
			{
				this._Operator = (MathemticalOperatorType)StringEnum.Parse(typeof(MathemticalOperatorType), node["operator"].Value<string>());
			}
			if(node["days"] != null)
			{
				this._Days = ParseInt(node["days"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaMonetizationSource");
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("operator", this._Operator);
			kparams.AddIfNotNull("days", this._Days);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case TYPE:
					return "Type";
				case OPERATOR:
					return "Operator";
				case DAYS:
					return "Days";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

