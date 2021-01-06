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
	public class Permission : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string FRIENDLY_NAME = "friendlyName";
		public const string DEPENDS_ON_PERMISSION_NAMES = "dependsOnPermissionNames";
		public const string TYPE = "type";
		public const string PERMISSION_ITEMS_IDS = "permissionItemsIds";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private string _FriendlyName = null;
		private string _DependsOnPermissionNames = null;
		private PermissionType _Type = null;
		private string _PermissionItemsIds = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public string FriendlyName
		{
			get { return _FriendlyName; }
			set 
			{ 
				_FriendlyName = value;
				OnPropertyChanged("FriendlyName");
			}
		}
		[JsonProperty]
		public string DependsOnPermissionNames
		{
			get { return _DependsOnPermissionNames; }
			private set 
			{ 
				_DependsOnPermissionNames = value;
				OnPropertyChanged("DependsOnPermissionNames");
			}
		}
		[JsonProperty]
		public PermissionType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		[JsonProperty]
		public string PermissionItemsIds
		{
			get { return _PermissionItemsIds; }
			private set 
			{ 
				_PermissionItemsIds = value;
				OnPropertyChanged("PermissionItemsIds");
			}
		}
		#endregion

		#region CTor
		public Permission()
		{
		}

		public Permission(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["friendlyName"] != null)
			{
				this._FriendlyName = node["friendlyName"].Value<string>();
			}
			if(node["dependsOnPermissionNames"] != null)
			{
				this._DependsOnPermissionNames = node["dependsOnPermissionNames"].Value<string>();
			}
			if(node["type"] != null)
			{
				this._Type = (PermissionType)StringEnum.Parse(typeof(PermissionType), node["type"].Value<string>());
			}
			if(node["permissionItemsIds"] != null)
			{
				this._PermissionItemsIds = node["permissionItemsIds"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPermission");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("friendlyName", this._FriendlyName);
			kparams.AddIfNotNull("dependsOnPermissionNames", this._DependsOnPermissionNames);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("permissionItemsIds", this._PermissionItemsIds);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case NAME:
					return "Name";
				case FRIENDLY_NAME:
					return "FriendlyName";
				case DEPENDS_ON_PERMISSION_NAMES:
					return "DependsOnPermissionNames";
				case TYPE:
					return "Type";
				case PERMISSION_ITEMS_IDS:
					return "PermissionItemsIds";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

