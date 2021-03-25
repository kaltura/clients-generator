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
	public class PermissionFilter : Filter
	{
		#region Constants
		public const string CURRENT_USER_PERMISSIONS_CONTAINS = "currentUserPermissionsContains";
		public const string ROLE_ID_IN = "roleIdIn";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private bool? _CurrentUserPermissionsContains = null;
		private long _RoleIdIn = long.MinValue;
		private PermissionOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public bool? CurrentUserPermissionsContains
		{
			get { return _CurrentUserPermissionsContains; }
			set 
			{ 
				_CurrentUserPermissionsContains = value;
				OnPropertyChanged("CurrentUserPermissionsContains");
			}
		}
		[JsonProperty]
		public long RoleIdIn
		{
			get { return _RoleIdIn; }
			set 
			{ 
				_RoleIdIn = value;
				OnPropertyChanged("RoleIdIn");
			}
		}
		[JsonProperty]
		public new PermissionOrderBy OrderBy
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
		public PermissionFilter()
		{
		}

		public PermissionFilter(JToken node) : base(node)
		{
			if(node["currentUserPermissionsContains"] != null)
			{
				this._CurrentUserPermissionsContains = ParseBool(node["currentUserPermissionsContains"].Value<string>());
			}
			if(node["roleIdIn"] != null)
			{
				this._RoleIdIn = ParseLong(node["roleIdIn"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (PermissionOrderBy)StringEnum.Parse(typeof(PermissionOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPermissionFilter");
			kparams.AddIfNotNull("currentUserPermissionsContains", this._CurrentUserPermissionsContains);
			kparams.AddIfNotNull("roleIdIn", this._RoleIdIn);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CURRENT_USER_PERMISSIONS_CONTAINS:
					return "CurrentUserPermissionsContains";
				case ROLE_ID_IN:
					return "RoleIdIn";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

