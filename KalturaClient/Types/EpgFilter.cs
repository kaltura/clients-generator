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
	public class EpgFilter : Filter
	{
		#region Constants
		public const string DATE_EQUAL = "dateEqual";
		public const string LIVE_ASSET_ID_EQUAL = "liveAssetIdEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private long _DateEqual = long.MinValue;
		private long _LiveAssetIdEqual = long.MinValue;
		private EpgOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long DateEqual
		{
			get { return _DateEqual; }
			set 
			{ 
				_DateEqual = value;
				OnPropertyChanged("DateEqual");
			}
		}
		[JsonProperty]
		public long LiveAssetIdEqual
		{
			get { return _LiveAssetIdEqual; }
			set 
			{ 
				_LiveAssetIdEqual = value;
				OnPropertyChanged("LiveAssetIdEqual");
			}
		}
		[JsonProperty]
		public new EpgOrderBy OrderBy
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
		public EpgFilter()
		{
		}

		public EpgFilter(JToken node) : base(node)
		{
			if(node["dateEqual"] != null)
			{
				this._DateEqual = ParseLong(node["dateEqual"].Value<string>());
			}
			if(node["liveAssetIdEqual"] != null)
			{
				this._LiveAssetIdEqual = ParseLong(node["liveAssetIdEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (EpgOrderBy)StringEnum.Parse(typeof(EpgOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaEpgFilter");
			kparams.AddIfNotNull("dateEqual", this._DateEqual);
			kparams.AddIfNotNull("liveAssetIdEqual", this._LiveAssetIdEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case DATE_EQUAL:
					return "DateEqual";
				case LIVE_ASSET_ID_EQUAL:
					return "LiveAssetIdEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

