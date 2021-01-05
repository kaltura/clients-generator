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
	public class Promotion : ObjectBase
	{
		#region Constants
		public const string DISCOUNT_MODULE_ID = "discountModuleId";
		public const string CONDITIONS = "conditions";
		public const string NUMBER_OF_RECURRING = "numberOfRecurring";
		#endregion

		#region Private Fields
		private long _DiscountModuleId = long.MinValue;
		private IList<Condition> _Conditions;
		private int _NumberOfRecurring = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public long DiscountModuleId
		{
			get { return _DiscountModuleId; }
			set 
			{ 
				_DiscountModuleId = value;
				OnPropertyChanged("DiscountModuleId");
			}
		}
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
		public int NumberOfRecurring
		{
			get { return _NumberOfRecurring; }
			set 
			{ 
				_NumberOfRecurring = value;
				OnPropertyChanged("NumberOfRecurring");
			}
		}
		#endregion

		#region CTor
		public Promotion()
		{
		}

		public Promotion(JToken node) : base(node)
		{
			if(node["discountModuleId"] != null)
			{
				this._DiscountModuleId = ParseLong(node["discountModuleId"].Value<string>());
			}
			if(node["conditions"] != null)
			{
				this._Conditions = new List<Condition>();
				foreach(var arrayNode in node["conditions"].Children())
				{
					this._Conditions.Add(ObjectFactory.Create<Condition>(arrayNode));
				}
			}
			if(node["numberOfRecurring"] != null)
			{
				this._NumberOfRecurring = ParseInt(node["numberOfRecurring"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPromotion");
			kparams.AddIfNotNull("discountModuleId", this._DiscountModuleId);
			kparams.AddIfNotNull("conditions", this._Conditions);
			kparams.AddIfNotNull("numberOfRecurring", this._NumberOfRecurring);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case DISCOUNT_MODULE_ID:
					return "DiscountModuleId";
				case CONDITIONS:
					return "Conditions";
				case NUMBER_OF_RECURRING:
					return "NumberOfRecurring";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

