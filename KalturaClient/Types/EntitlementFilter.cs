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
	public class EntitlementFilter : Filter
	{
		#region Constants
		public const string PRODUCT_TYPE_EQUAL = "productTypeEqual";
		public const string ENTITY_REFERENCE_EQUAL = "entityReferenceEqual";
		public const string IS_EXPIRED_EQUAL = "isExpiredEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private TransactionType _ProductTypeEqual = null;
		private EntityReferenceBy _EntityReferenceEqual = null;
		private bool? _IsExpiredEqual = null;
		private EntitlementOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public TransactionType ProductTypeEqual
		{
			get { return _ProductTypeEqual; }
			set 
			{ 
				_ProductTypeEqual = value;
				OnPropertyChanged("ProductTypeEqual");
			}
		}
		[JsonProperty]
		public EntityReferenceBy EntityReferenceEqual
		{
			get { return _EntityReferenceEqual; }
			set 
			{ 
				_EntityReferenceEqual = value;
				OnPropertyChanged("EntityReferenceEqual");
			}
		}
		[JsonProperty]
		public bool? IsExpiredEqual
		{
			get { return _IsExpiredEqual; }
			set 
			{ 
				_IsExpiredEqual = value;
				OnPropertyChanged("IsExpiredEqual");
			}
		}
		[JsonProperty]
		public new EntitlementOrderBy OrderBy
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
		public EntitlementFilter()
		{
		}

		public EntitlementFilter(JToken node) : base(node)
		{
			if(node["productTypeEqual"] != null)
			{
				this._ProductTypeEqual = (TransactionType)StringEnum.Parse(typeof(TransactionType), node["productTypeEqual"].Value<string>());
			}
			if(node["entityReferenceEqual"] != null)
			{
				this._EntityReferenceEqual = (EntityReferenceBy)StringEnum.Parse(typeof(EntityReferenceBy), node["entityReferenceEqual"].Value<string>());
			}
			if(node["isExpiredEqual"] != null)
			{
				this._IsExpiredEqual = ParseBool(node["isExpiredEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (EntitlementOrderBy)StringEnum.Parse(typeof(EntitlementOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaEntitlementFilter");
			kparams.AddIfNotNull("productTypeEqual", this._ProductTypeEqual);
			kparams.AddIfNotNull("entityReferenceEqual", this._EntityReferenceEqual);
			kparams.AddIfNotNull("isExpiredEqual", this._IsExpiredEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PRODUCT_TYPE_EQUAL:
					return "ProductTypeEqual";
				case ENTITY_REFERENCE_EQUAL:
					return "EntityReferenceEqual";
				case IS_EXPIRED_EQUAL:
					return "IsExpiredEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

