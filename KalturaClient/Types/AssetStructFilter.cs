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
	public class AssetStructFilter : Filter
	{
		#region Constants
		public const string ID_IN = "idIn";
		public const string META_ID_EQUAL = "metaIdEqual";
		public const string IS_PROTECTED_EQUAL = "isProtectedEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _IdIn = null;
		private long _MetaIdEqual = long.MinValue;
		private bool? _IsProtectedEqual = null;
		private AssetStructOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public string IdIn
		{
			get { return _IdIn; }
			set 
			{ 
				_IdIn = value;
				OnPropertyChanged("IdIn");
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
		public bool? IsProtectedEqual
		{
			get { return _IsProtectedEqual; }
			set 
			{ 
				_IsProtectedEqual = value;
				OnPropertyChanged("IsProtectedEqual");
			}
		}
		public new AssetStructOrderBy OrderBy
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
		public AssetStructFilter()
		{
		}

		public AssetStructFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "idIn":
						this._IdIn = propertyNode.InnerText;
						continue;
					case "metaIdEqual":
						this._MetaIdEqual = ParseLong(propertyNode.InnerText);
						continue;
					case "isProtectedEqual":
						this._IsProtectedEqual = ParseBool(propertyNode.InnerText);
						continue;
					case "orderBy":
						this._OrderBy = (AssetStructOrderBy)StringEnum.Parse(typeof(AssetStructOrderBy), propertyNode.InnerText);
						continue;
				}
			}
		}

		public AssetStructFilter(IDictionary<string,object> data) : base(data)
		{
			    this._IdIn = data.TryGetValueSafe<string>("idIn");
			    this._MetaIdEqual = data.TryGetValueSafe<long>("metaIdEqual");
			    this._IsProtectedEqual = data.TryGetValueSafe<bool>("isProtectedEqual");
			    this._OrderBy = (AssetStructOrderBy)StringEnum.Parse(typeof(AssetStructOrderBy), data.TryGetValueSafe<string>("orderBy"));
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetStructFilter");
			kparams.AddIfNotNull("idIn", this._IdIn);
			kparams.AddIfNotNull("metaIdEqual", this._MetaIdEqual);
			kparams.AddIfNotNull("isProtectedEqual", this._IsProtectedEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID_IN:
					return "IdIn";
				case META_ID_EQUAL:
					return "MetaIdEqual";
				case IS_PROTECTED_EQUAL:
					return "IsProtectedEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

