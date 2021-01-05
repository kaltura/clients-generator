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
	public class UserAssetsListItem : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string ORDER_INDEX = "orderIndex";
		public const string TYPE = "type";
		public const string USER_ID = "userId";
		public const string LIST_TYPE = "listType";
		#endregion

		#region Private Fields
		private string _Id = null;
		private int _OrderIndex = Int32.MinValue;
		private UserAssetsListItemType _Type = null;
		private string _UserId = null;
		private UserAssetsListType _ListType = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string Id
		{
			get { return _Id; }
			set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public int OrderIndex
		{
			get { return _OrderIndex; }
			set 
			{ 
				_OrderIndex = value;
				OnPropertyChanged("OrderIndex");
			}
		}
		[JsonProperty]
		public UserAssetsListItemType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		[JsonProperty]
		public string UserId
		{
			get { return _UserId; }
			private set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		[JsonProperty]
		public UserAssetsListType ListType
		{
			get { return _ListType; }
			set 
			{ 
				_ListType = value;
				OnPropertyChanged("ListType");
			}
		}
		#endregion

		#region CTor
		public UserAssetsListItem()
		{
		}

		public UserAssetsListItem(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["orderIndex"] != null)
			{
				this._OrderIndex = ParseInt(node["orderIndex"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = (UserAssetsListItemType)StringEnum.Parse(typeof(UserAssetsListItemType), node["type"].Value<string>());
			}
			if(node["userId"] != null)
			{
				this._UserId = node["userId"].Value<string>();
			}
			if(node["listType"] != null)
			{
				this._ListType = (UserAssetsListType)StringEnum.Parse(typeof(UserAssetsListType), node["listType"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaUserAssetsListItem");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("orderIndex", this._OrderIndex);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("listType", this._ListType);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case ORDER_INDEX:
					return "OrderIndex";
				case TYPE:
					return "Type";
				case USER_ID:
					return "UserId";
				case LIST_TYPE:
					return "ListType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

