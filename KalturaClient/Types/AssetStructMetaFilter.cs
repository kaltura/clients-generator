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
// Copyright (C) 2006-2018  Kaltura Inc.
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

namespace Kaltura.Types
{
	public class AssetStructMetaFilter : Filter
	{
		#region Constants
		public const string ASSET_STRUCT_ID_EQUAL = "assetStructIdEqual";
		public const string META_ID_EQUAL = "metaIdEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private long _AssetStructIdEqual = long.MinValue;
		private long _MetaIdEqual = long.MinValue;
		private AssetStructMetaOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public long AssetStructIdEqual
		{
			get { return _AssetStructIdEqual; }
			set 
			{ 
				_AssetStructIdEqual = value;
				OnPropertyChanged("AssetStructIdEqual");
			}
		}
		public long MetaIdEqual
		{
			get { return _MetaIdEqual; }
			set 
			{ 
				_MetaIdEqual = value;
				OnPropertyChanged("MetaIdEqual");
			}
		}
		public new AssetStructMetaOrderBy OrderBy
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
		public AssetStructMetaFilter()
		{
		}

		public AssetStructMetaFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "assetStructIdEqual":
						this._AssetStructIdEqual = ParseLong(propertyNode.InnerText);
						continue;
					case "metaIdEqual":
						this._MetaIdEqual = ParseLong(propertyNode.InnerText);
						continue;
					case "orderBy":
						this._OrderBy = (AssetStructMetaOrderBy)StringEnum.Parse(typeof(AssetStructMetaOrderBy), propertyNode.InnerText);
						continue;
				}
			}
		}

		public AssetStructMetaFilter(IDictionary<string,object> data) : base(data)
		{
			    this._AssetStructIdEqual = data.TryGetValueSafe<long>("assetStructIdEqual");
			    this._MetaIdEqual = data.TryGetValueSafe<long>("metaIdEqual");
			    this._OrderBy = (AssetStructMetaOrderBy)StringEnum.Parse(typeof(AssetStructMetaOrderBy), data.TryGetValueSafe<string>("orderBy"));
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetStructMetaFilter");
			kparams.AddIfNotNull("assetStructIdEqual", this._AssetStructIdEqual);
			kparams.AddIfNotNull("metaIdEqual", this._MetaIdEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_STRUCT_ID_EQUAL:
					return "AssetStructIdEqual";
				case META_ID_EQUAL:
					return "MetaIdEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

