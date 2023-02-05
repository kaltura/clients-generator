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
	public class MetaFilter : Filter
	{
		#region Constants
		public const string ID_IN = "idIn";
		public const string ASSET_STRUCT_ID_EQUAL = "assetStructIdEqual";
		public const string DATA_TYPE_EQUAL = "dataTypeEqual";
		public const string MULTIPLE_VALUE_EQUAL = "multipleValueEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _IdIn = null;
		private long _AssetStructIdEqual = long.MinValue;
		private MetaDataType _DataTypeEqual = null;
		private bool? _MultipleValueEqual = null;
		private MetaOrderBy _OrderBy = null;
		#endregion

		#region Properties
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
		public long AssetStructIdEqual
		{
			get { return _AssetStructIdEqual; }
			set 
			{ 
				_AssetStructIdEqual = value;
				OnPropertyChanged("AssetStructIdEqual");
			}
		}
		[JsonProperty]
		public MetaDataType DataTypeEqual
		{
			get { return _DataTypeEqual; }
			set 
			{ 
				_DataTypeEqual = value;
				OnPropertyChanged("DataTypeEqual");
			}
		}
		[JsonProperty]
		public bool? MultipleValueEqual
		{
			get { return _MultipleValueEqual; }
			set 
			{ 
				_MultipleValueEqual = value;
				OnPropertyChanged("MultipleValueEqual");
			}
		}
		[JsonProperty]
		public new MetaOrderBy OrderBy
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
		public MetaFilter()
		{
		}

		public MetaFilter(JToken node) : base(node)
		{
			if(node["idIn"] != null)
			{
				this._IdIn = node["idIn"].Value<string>();
			}
			if(node["assetStructIdEqual"] != null)
			{
				this._AssetStructIdEqual = ParseLong(node["assetStructIdEqual"].Value<string>());
			}
			if(node["dataTypeEqual"] != null)
			{
				this._DataTypeEqual = (MetaDataType)StringEnum.Parse(typeof(MetaDataType), node["dataTypeEqual"].Value<string>());
			}
			if(node["multipleValueEqual"] != null)
			{
				this._MultipleValueEqual = ParseBool(node["multipleValueEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (MetaOrderBy)StringEnum.Parse(typeof(MetaOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaMetaFilter");
			kparams.AddIfNotNull("idIn", this._IdIn);
			kparams.AddIfNotNull("assetStructIdEqual", this._AssetStructIdEqual);
			kparams.AddIfNotNull("dataTypeEqual", this._DataTypeEqual);
			kparams.AddIfNotNull("multipleValueEqual", this._MultipleValueEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID_IN:
					return "IdIn";
				case ASSET_STRUCT_ID_EQUAL:
					return "AssetStructIdEqual";
				case DATA_TYPE_EQUAL:
					return "DataTypeEqual";
				case MULTIPLE_VALUE_EQUAL:
					return "MultipleValueEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

