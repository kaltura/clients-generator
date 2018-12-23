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
// Copyright (C) 2006-2018  Kaltura Inc.
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
	public class MonetizationCondition : BaseSegmentCondition
	{
		#region Constants
		public const string MIN_VALUE = "minValue";
		public const string MAX_VALUE = "maxValue";
		public const string DAYS = "days";
		public const string TYPE = "type";
		public const string OPERATOR = "operator";
		#endregion

		#region Private Fields
		private int _MinValue = Int32.MinValue;
		private int _MaxValue = Int32.MinValue;
		private int _Days = Int32.MinValue;
		private MonetizationType _Type = null;
		private MathemticalOperatorType _Operator = null;
		#endregion

		#region Properties
		public int MinValue
		{
			get { return _MinValue; }
			set 
			{ 
				_MinValue = value;
				OnPropertyChanged("MinValue");
			}
		}
		public int MaxValue
		{
			get { return _MaxValue; }
			set 
			{ 
				_MaxValue = value;
				OnPropertyChanged("MaxValue");
			}
		}
		public int Days
		{
			get { return _Days; }
			set 
			{ 
				_Days = value;
				OnPropertyChanged("Days");
			}
		}
		public MonetizationType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		public MathemticalOperatorType Operator
		{
			get { return _Operator; }
			set 
			{ 
				_Operator = value;
				OnPropertyChanged("Operator");
			}
		}
		#endregion

		#region CTor
		public MonetizationCondition()
		{
		}

		public MonetizationCondition(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "minValue":
						this._MinValue = ParseInt(propertyNode.InnerText);
						continue;
					case "maxValue":
						this._MaxValue = ParseInt(propertyNode.InnerText);
						continue;
					case "days":
						this._Days = ParseInt(propertyNode.InnerText);
						continue;
					case "type":
						this._Type = (MonetizationType)StringEnum.Parse(typeof(MonetizationType), propertyNode.InnerText);
						continue;
					case "operator":
						this._Operator = (MathemticalOperatorType)StringEnum.Parse(typeof(MathemticalOperatorType), propertyNode.InnerText);
						continue;
				}
			}
		}

		public MonetizationCondition(IDictionary<string,object> data) : base(data)
		{
			    this._MinValue = data.TryGetValueSafe<int>("minValue");
			    this._MaxValue = data.TryGetValueSafe<int>("maxValue");
			    this._Days = data.TryGetValueSafe<int>("days");
			    this._Type = (MonetizationType)StringEnum.Parse(typeof(MonetizationType), data.TryGetValueSafe<string>("type"));
			    this._Operator = (MathemticalOperatorType)StringEnum.Parse(typeof(MathemticalOperatorType), data.TryGetValueSafe<string>("operator"));
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
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

