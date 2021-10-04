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
	public class AssetHistoryFilter : Filter
	{
		#region Constants
		public const string TYPE_IN = "typeIn";
		public const string ASSET_ID_IN = "assetIdIn";
		public const string STATUS_EQUAL = "statusEqual";
		public const string DAYS_LESS_THAN_OR_EQUAL = "daysLessThanOrEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _TypeIn = null;
		private string _AssetIdIn = null;
		private WatchStatus _StatusEqual = null;
		private int _DaysLessThanOrEqual = Int32.MinValue;
		private AssetHistoryOrderBy _OrderBy = null;
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
		public string AssetIdIn
		{
			get { return _AssetIdIn; }
			set 
			{ 
				_AssetIdIn = value;
				OnPropertyChanged("AssetIdIn");
			}
		}
		[JsonProperty]
		public WatchStatus StatusEqual
		{
			get { return _StatusEqual; }
			set 
			{ 
				_StatusEqual = value;
				OnPropertyChanged("StatusEqual");
			}
		}
		[JsonProperty]
		public int DaysLessThanOrEqual
		{
			get { return _DaysLessThanOrEqual; }
			set 
			{ 
				_DaysLessThanOrEqual = value;
				OnPropertyChanged("DaysLessThanOrEqual");
			}
		}
		[JsonProperty]
		public new AssetHistoryOrderBy OrderBy
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
		public AssetHistoryFilter()
		{
		}

		public AssetHistoryFilter(JToken node) : base(node)
		{
			if(node["typeIn"] != null)
			{
				this._TypeIn = node["typeIn"].Value<string>();
			}
			if(node["assetIdIn"] != null)
			{
				this._AssetIdIn = node["assetIdIn"].Value<string>();
			}
			if(node["statusEqual"] != null)
			{
				this._StatusEqual = (WatchStatus)StringEnum.Parse(typeof(WatchStatus), node["statusEqual"].Value<string>());
			}
			if(node["daysLessThanOrEqual"] != null)
			{
				this._DaysLessThanOrEqual = ParseInt(node["daysLessThanOrEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (AssetHistoryOrderBy)StringEnum.Parse(typeof(AssetHistoryOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetHistoryFilter");
			kparams.AddIfNotNull("typeIn", this._TypeIn);
			kparams.AddIfNotNull("assetIdIn", this._AssetIdIn);
			kparams.AddIfNotNull("statusEqual", this._StatusEqual);
			kparams.AddIfNotNull("daysLessThanOrEqual", this._DaysLessThanOrEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case TYPE_IN:
					return "TypeIn";
				case ASSET_ID_IN:
					return "AssetIdIn";
				case STATUS_EQUAL:
					return "StatusEqual";
				case DAYS_LESS_THAN_OR_EQUAL:
					return "DaysLessThanOrEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

