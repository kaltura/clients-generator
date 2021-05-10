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
	public class DiscountDetails : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string MULTI_CURRENCY_DISCOUNT = "multiCurrencyDiscount";
		public const string START_DATE = "startDate";
		public const string END_DATE = "endDate";
		public const string WHEN_ALGO_TIMES = "whenAlgoTimes";
		public const string WHEN_ALGO_TYPE = "whenAlgoType";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _Name = null;
		private IList<Discount> _MultiCurrencyDiscount;
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		private int _WhenAlgoTimes = Int32.MinValue;
		private int _WhenAlgoType = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public int Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public IList<Discount> MultiCurrencyDiscount
		{
			get { return _MultiCurrencyDiscount; }
			set 
			{ 
				_MultiCurrencyDiscount = value;
				OnPropertyChanged("MultiCurrencyDiscount");
			}
		}
		[JsonProperty]
		public long StartDate
		{
			get { return _StartDate; }
			set 
			{ 
				_StartDate = value;
				OnPropertyChanged("StartDate");
			}
		}
		[JsonProperty]
		public long EndDate
		{
			get { return _EndDate; }
			set 
			{ 
				_EndDate = value;
				OnPropertyChanged("EndDate");
			}
		}
		[JsonProperty]
		public int WhenAlgoTimes
		{
			get { return _WhenAlgoTimes; }
			set 
			{ 
				_WhenAlgoTimes = value;
				OnPropertyChanged("WhenAlgoTimes");
			}
		}
		[JsonProperty]
		public int WhenAlgoType
		{
			get { return _WhenAlgoType; }
			set 
			{ 
				_WhenAlgoType = value;
				OnPropertyChanged("WhenAlgoType");
			}
		}
		#endregion

		#region CTor
		public DiscountDetails()
		{
		}

		public DiscountDetails(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["multiCurrencyDiscount"] != null)
			{
				this._MultiCurrencyDiscount = new List<Discount>();
				foreach(var arrayNode in node["multiCurrencyDiscount"].Children())
				{
					this._MultiCurrencyDiscount.Add(ObjectFactory.Create<Discount>(arrayNode));
				}
			}
			if(node["startDate"] != null)
			{
				this._StartDate = ParseLong(node["startDate"].Value<string>());
			}
			if(node["endDate"] != null)
			{
				this._EndDate = ParseLong(node["endDate"].Value<string>());
			}
			if(node["whenAlgoTimes"] != null)
			{
				this._WhenAlgoTimes = ParseInt(node["whenAlgoTimes"].Value<string>());
			}
			if(node["whenAlgoType"] != null)
			{
				this._WhenAlgoType = ParseInt(node["whenAlgoType"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaDiscountDetails");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("multiCurrencyDiscount", this._MultiCurrencyDiscount);
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("whenAlgoTimes", this._WhenAlgoTimes);
			kparams.AddIfNotNull("whenAlgoType", this._WhenAlgoType);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case NAME:
					return "Name";
				case MULTI_CURRENCY_DISCOUNT:
					return "MultiCurrencyDiscount";
				case START_DATE:
					return "StartDate";
				case END_DATE:
					return "EndDate";
				case WHEN_ALGO_TIMES:
					return "WhenAlgoTimes";
				case WHEN_ALGO_TYPE:
					return "WhenAlgoType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

