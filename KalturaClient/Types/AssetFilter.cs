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
	public class AssetFilter : PersistedFilter
	{
		#region Constants
		public const string DYNAMIC_ORDER_BY = "dynamicOrderBy";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private DynamicOrderBy _DynamicOrderBy;
		private AssetOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public DynamicOrderBy DynamicOrderBy
		{
			get { return _DynamicOrderBy; }
			set 
			{ 
				_DynamicOrderBy = value;
				OnPropertyChanged("DynamicOrderBy");
			}
		}
		public new AssetOrderBy OrderBy
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
		public AssetFilter()
		{
		}

		public AssetFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "dynamicOrderBy":
						this._DynamicOrderBy = ObjectFactory.Create<DynamicOrderBy>(propertyNode);
						continue;
					case "orderBy":
						this._OrderBy = (AssetOrderBy)StringEnum.Parse(typeof(AssetOrderBy), propertyNode.InnerText);
						continue;
				}
			}
		}

		public AssetFilter(IDictionary<string,object> data) : base(data)
		{
			    this._DynamicOrderBy = ObjectFactory.Create<DynamicOrderBy>(data.TryGetValueSafe<IDictionary<string,object>>("dynamicOrderBy"));
			    this._OrderBy = (AssetOrderBy)StringEnum.Parse(typeof(AssetOrderBy), data.TryGetValueSafe<string>("orderBy"));
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetFilter");
			kparams.AddIfNotNull("dynamicOrderBy", this._DynamicOrderBy);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
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
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

