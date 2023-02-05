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
	public class RegionFilter : BaseRegionFilter
	{
		#region Constants
		public const string EXTERNAL_ID_IN = "externalIdIn";
		public const string ID_IN = "idIn";
		public const string PARENT_ID_EQUAL = "parentIdEqual";
		public const string LIVE_ASSET_ID_EQUAL = "liveAssetIdEqual";
		public const string PARENT_ONLY = "parentOnly";
		public const string EXCLUSIVE_LCN = "exclusiveLcn";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _ExternalIdIn = null;
		private string _IdIn = null;
		private int _ParentIdEqual = Int32.MinValue;
		private int _LiveAssetIdEqual = Int32.MinValue;
		private bool? _ParentOnly = null;
		private bool? _ExclusiveLcn = null;
		private RegionOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string ExternalIdIn
		{
			get { return _ExternalIdIn; }
			set 
			{ 
				_ExternalIdIn = value;
				OnPropertyChanged("ExternalIdIn");
			}
		}
		[JsonProperty]
		public string IdIn
		{
			get { return _IdIn; }
			set 
			{ 
				_IdIn = value;
				OnPropertyChanged("IdIn");
			}
		}
		[JsonProperty]
		public int ParentIdEqual
		{
			get { return _ParentIdEqual; }
			set 
			{ 
				_ParentIdEqual = value;
				OnPropertyChanged("ParentIdEqual");
			}
		}
		[JsonProperty]
		public int LiveAssetIdEqual
		{
			get { return _LiveAssetIdEqual; }
			set 
			{ 
				_LiveAssetIdEqual = value;
				OnPropertyChanged("LiveAssetIdEqual");
			}
		}
		[JsonProperty]
		public bool? ParentOnly
		{
			get { return _ParentOnly; }
			set 
			{ 
				_ParentOnly = value;
				OnPropertyChanged("ParentOnly");
			}
		}
		[JsonProperty]
		public bool? ExclusiveLcn
		{
			get { return _ExclusiveLcn; }
			set 
			{ 
				_ExclusiveLcn = value;
				OnPropertyChanged("ExclusiveLcn");
			}
		}
		[JsonProperty]
		public new RegionOrderBy OrderBy
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
		public RegionFilter()
		{
		}

		public RegionFilter(JToken node) : base(node)
		{
			if(node["externalIdIn"] != null)
			{
				this._ExternalIdIn = node["externalIdIn"].Value<string>();
			}
			if(node["idIn"] != null)
			{
				this._IdIn = node["idIn"].Value<string>();
			}
			if(node["parentIdEqual"] != null)
			{
				this._ParentIdEqual = ParseInt(node["parentIdEqual"].Value<string>());
			}
			if(node["liveAssetIdEqual"] != null)
			{
				this._LiveAssetIdEqual = ParseInt(node["liveAssetIdEqual"].Value<string>());
			}
			if(node["parentOnly"] != null)
			{
				this._ParentOnly = ParseBool(node["parentOnly"].Value<string>());
			}
			if(node["exclusiveLcn"] != null)
			{
				this._ExclusiveLcn = ParseBool(node["exclusiveLcn"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (RegionOrderBy)StringEnum.Parse(typeof(RegionOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaRegionFilter");
			kparams.AddIfNotNull("externalIdIn", this._ExternalIdIn);
			kparams.AddIfNotNull("idIn", this._IdIn);
			kparams.AddIfNotNull("parentIdEqual", this._ParentIdEqual);
			kparams.AddIfNotNull("liveAssetIdEqual", this._LiveAssetIdEqual);
			kparams.AddIfNotNull("parentOnly", this._ParentOnly);
			kparams.AddIfNotNull("exclusiveLcn", this._ExclusiveLcn);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case EXTERNAL_ID_IN:
					return "ExternalIdIn";
				case ID_IN:
					return "IdIn";
				case PARENT_ID_EQUAL:
					return "ParentIdEqual";
				case LIVE_ASSET_ID_EQUAL:
					return "LiveAssetIdEqual";
				case PARENT_ONLY:
					return "ParentOnly";
				case EXCLUSIVE_LCN:
					return "ExclusiveLcn";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

