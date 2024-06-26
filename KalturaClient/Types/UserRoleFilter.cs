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
	public class UserRoleFilter : Filter
	{
		#region Constants
		public const string ID_IN = "idIn";
		public const string CURRENT_USER_ROLE_IDS_CONTAINS = "currentUserRoleIdsContains";
		public const string TYPE_EQUAL = "typeEqual";
		public const string PROFILE_EQUAL = "profileEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _IdIn = null;
		private bool? _CurrentUserRoleIdsContains = null;
		private UserRoleType _TypeEqual = null;
		private UserRoleProfile _ProfileEqual = null;
		private UserRoleOrderBy _OrderBy = null;
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
		public bool? CurrentUserRoleIdsContains
		{
			get { return _CurrentUserRoleIdsContains; }
			set 
			{ 
				_CurrentUserRoleIdsContains = value;
				OnPropertyChanged("CurrentUserRoleIdsContains");
			}
		}
		[JsonProperty]
		public UserRoleType TypeEqual
		{
			get { return _TypeEqual; }
			set 
			{ 
				_TypeEqual = value;
				OnPropertyChanged("TypeEqual");
			}
		}
		[JsonProperty]
		public UserRoleProfile ProfileEqual
		{
			get { return _ProfileEqual; }
			set 
			{ 
				_ProfileEqual = value;
				OnPropertyChanged("ProfileEqual");
			}
		}
		[JsonProperty]
		public new UserRoleOrderBy OrderBy
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
		public UserRoleFilter()
		{
		}

		public UserRoleFilter(JToken node) : base(node)
		{
			if(node["idIn"] != null)
			{
				this._IdIn = node["idIn"].Value<string>();
			}
			if(node["currentUserRoleIdsContains"] != null)
			{
				this._CurrentUserRoleIdsContains = ParseBool(node["currentUserRoleIdsContains"].Value<string>());
			}
			if(node["typeEqual"] != null)
			{
				this._TypeEqual = (UserRoleType)StringEnum.Parse(typeof(UserRoleType), node["typeEqual"].Value<string>());
			}
			if(node["profileEqual"] != null)
			{
				this._ProfileEqual = (UserRoleProfile)StringEnum.Parse(typeof(UserRoleProfile), node["profileEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (UserRoleOrderBy)StringEnum.Parse(typeof(UserRoleOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaUserRoleFilter");
			kparams.AddIfNotNull("idIn", this._IdIn);
			kparams.AddIfNotNull("currentUserRoleIdsContains", this._CurrentUserRoleIdsContains);
			kparams.AddIfNotNull("typeEqual", this._TypeEqual);
			kparams.AddIfNotNull("profileEqual", this._ProfileEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID_IN:
					return "IdIn";
				case CURRENT_USER_ROLE_IDS_CONTAINS:
					return "CurrentUserRoleIdsContains";
				case TYPE_EQUAL:
					return "TypeEqual";
				case PROFILE_EQUAL:
					return "ProfileEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

