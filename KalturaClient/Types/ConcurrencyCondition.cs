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
	public class ConcurrencyCondition : AssetCondition
	{
		#region Constants
		public const string LIMIT = "limit";
		public const string CONCURRENCY_LIMITATION_TYPE = "concurrencyLimitationType";
		#endregion

		#region Private Fields
		private int _Limit = Int32.MinValue;
		private ConcurrencyLimitationType _ConcurrencyLimitationType = null;
		#endregion

		#region Properties
		[JsonProperty]
		public int Limit
		{
			get { return _Limit; }
			set 
			{ 
				_Limit = value;
				OnPropertyChanged("Limit");
			}
		}
		[JsonProperty]
		public ConcurrencyLimitationType ConcurrencyLimitationType
		{
			get { return _ConcurrencyLimitationType; }
			set 
			{ 
				_ConcurrencyLimitationType = value;
				OnPropertyChanged("ConcurrencyLimitationType");
			}
		}
		#endregion

		#region CTor
		public ConcurrencyCondition()
		{
		}

		public ConcurrencyCondition(JToken node) : base(node)
		{
			if(node["limit"] != null)
			{
				this._Limit = ParseInt(node["limit"].Value<string>());
			}
			if(node["concurrencyLimitationType"] != null)
			{
				this._ConcurrencyLimitationType = (ConcurrencyLimitationType)StringEnum.Parse(typeof(ConcurrencyLimitationType), node["concurrencyLimitationType"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaConcurrencyCondition");
			kparams.AddIfNotNull("limit", this._Limit);
			kparams.AddIfNotNull("concurrencyLimitationType", this._ConcurrencyLimitationType);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case LIMIT:
					return "Limit";
				case CONCURRENCY_LIMITATION_TYPE:
					return "ConcurrencyLimitationType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

