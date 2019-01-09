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
	public class ChannelOrder : ObjectBase
	{
		#region Constants
		public const string DYNAMIC_ORDER_BY = "dynamicOrderBy";
		public const string ORDER_BY = "orderBy";
		public const string PERIOD = "period";
		#endregion

		#region Private Fields
		private DynamicOrderBy _DynamicOrderBy;
		private ChannelOrderBy _OrderBy = null;
		private int _Period = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public DynamicOrderBy DynamicOrderBy
		{
			get { return _DynamicOrderBy; }
			set 
			{ 
				_DynamicOrderBy = value;
				OnPropertyChanged("DynamicOrderBy");
			}
		}
		[JsonProperty]
		public ChannelOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		[JsonProperty]
		public int Period
		{
			get { return _Period; }
			set 
			{ 
				_Period = value;
				OnPropertyChanged("Period");
			}
		}
		#endregion

		#region CTor
		public ChannelOrder()
		{
		}

		public ChannelOrder(JToken node) : base(node)
		{
			if(node["dynamicOrderBy"] != null)
			{
				this._DynamicOrderBy = ObjectFactory.Create<DynamicOrderBy>(node["dynamicOrderBy"]);
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (ChannelOrderBy)StringEnum.Parse(typeof(ChannelOrderBy), node["orderBy"].Value<string>());
			}
			if(node["period"] != null)
			{
				this._Period = ParseInt(node["period"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaChannelOrder");
			kparams.AddIfNotNull("dynamicOrderBy", this._DynamicOrderBy);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			kparams.AddIfNotNull("period", this._Period);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case DYNAMIC_ORDER_BY:
					return "DynamicOrderBy";
				case ORDER_BY:
					return "OrderBy";
				case PERIOD:
					return "Period";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

