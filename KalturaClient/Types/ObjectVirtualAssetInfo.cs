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
	public class ObjectVirtualAssetInfo : ObjectBase
	{
		#region Constants
		public const string ASSET_STRUCT_ID = "assetStructId";
		public const string META_ID = "metaId";
		public const string TYPE = "type";
		#endregion

		#region Private Fields
		private int _AssetStructId = Int32.MinValue;
		private int _MetaId = Int32.MinValue;
		private ObjectVirtualAssetInfoType _Type = null;
		#endregion

		#region Properties
		[JsonProperty]
		public int AssetStructId
		{
			get { return _AssetStructId; }
			set 
			{ 
				_AssetStructId = value;
				OnPropertyChanged("AssetStructId");
			}
		}
		[JsonProperty]
		public int MetaId
		{
			get { return _MetaId; }
			set 
			{ 
				_MetaId = value;
				OnPropertyChanged("MetaId");
			}
		}
		[JsonProperty]
		public ObjectVirtualAssetInfoType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		#endregion

		#region CTor
		public ObjectVirtualAssetInfo()
		{
		}

		public ObjectVirtualAssetInfo(JToken node) : base(node)
		{
			if(node["assetStructId"] != null)
			{
				this._AssetStructId = ParseInt(node["assetStructId"].Value<string>());
			}
			if(node["metaId"] != null)
			{
				this._MetaId = ParseInt(node["metaId"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = (ObjectVirtualAssetInfoType)StringEnum.Parse(typeof(ObjectVirtualAssetInfoType), node["type"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaObjectVirtualAssetInfo");
			kparams.AddIfNotNull("assetStructId", this._AssetStructId);
			kparams.AddIfNotNull("metaId", this._MetaId);
			kparams.AddIfNotNull("type", this._Type);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_STRUCT_ID:
					return "AssetStructId";
				case META_ID:
					return "MetaId";
				case TYPE:
					return "Type";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

