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
	public class EngagementFilter : Filter
	{
		#region Constants
		public const string TYPE_IN = "typeIn";
		public const string SEND_TIME_GREATER_THAN_OR_EQUAL = "sendTimeGreaterThanOrEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _TypeIn = null;
		private long _SendTimeGreaterThanOrEqual = long.MinValue;
		private EngagementOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string TypeIn
		{
			get { return _TypeIn; }
			set 
			{ 
				_TypeIn = value;
				OnPropertyChanged("TypeIn");
			}
		}
		[JsonProperty]
		public long SendTimeGreaterThanOrEqual
		{
			get { return _SendTimeGreaterThanOrEqual; }
			set 
			{ 
				_SendTimeGreaterThanOrEqual = value;
				OnPropertyChanged("SendTimeGreaterThanOrEqual");
			}
		}
		[JsonProperty]
		public new EngagementOrderBy OrderBy
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
		public EngagementFilter()
		{
		}

		public EngagementFilter(JToken node) : base(node)
		{
			if(node["typeIn"] != null)
			{
				this._TypeIn = node["typeIn"].Value<string>();
			}
			if(node["sendTimeGreaterThanOrEqual"] != null)
			{
				this._SendTimeGreaterThanOrEqual = ParseLong(node["sendTimeGreaterThanOrEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (EngagementOrderBy)StringEnum.Parse(typeof(EngagementOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaEngagementFilter");
			kparams.AddIfNotNull("typeIn", this._TypeIn);
			kparams.AddIfNotNull("sendTimeGreaterThanOrEqual", this._SendTimeGreaterThanOrEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case TYPE_IN:
					return "TypeIn";
				case SEND_TIME_GREATER_THAN_OR_EQUAL:
					return "SendTimeGreaterThanOrEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

