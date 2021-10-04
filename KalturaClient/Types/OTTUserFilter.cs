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
	public class OTTUserFilter : Filter
	{
		#region Constants
		public const string USERNAME_EQUAL = "usernameEqual";
		public const string EXTERNAL_ID_EQUAL = "externalIdEqual";
		public const string ID_IN = "idIn";
		public const string ROLE_IDS_IN = "roleIdsIn";
		public const string EMAIL_EQUAL = "emailEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _UsernameEqual = null;
		private string _ExternalIdEqual = null;
		private string _IdIn = null;
		private string _RoleIdsIn = null;
		private string _EmailEqual = null;
		private OTTUserOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string UsernameEqual
		{
			get { return _UsernameEqual; }
			set 
			{ 
				_UsernameEqual = value;
				OnPropertyChanged("UsernameEqual");
			}
		}
		[JsonProperty]
		public string ExternalIdEqual
		{
			get { return _ExternalIdEqual; }
			set 
			{ 
				_ExternalIdEqual = value;
				OnPropertyChanged("ExternalIdEqual");
			}
		}
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
		public string RoleIdsIn
		{
			get { return _RoleIdsIn; }
			set 
			{ 
				_RoleIdsIn = value;
				OnPropertyChanged("RoleIdsIn");
			}
		}
		[JsonProperty]
		public string EmailEqual
		{
			get { return _EmailEqual; }
			set 
			{ 
				_EmailEqual = value;
				OnPropertyChanged("EmailEqual");
			}
		}
		[JsonProperty]
		public new OTTUserOrderBy OrderBy
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
		public OTTUserFilter()
		{
		}

		public OTTUserFilter(JToken node) : base(node)
		{
			if(node["usernameEqual"] != null)
			{
				this._UsernameEqual = node["usernameEqual"].Value<string>();
			}
			if(node["externalIdEqual"] != null)
			{
				this._ExternalIdEqual = node["externalIdEqual"].Value<string>();
			}
			if(node["idIn"] != null)
			{
				this._IdIn = node["idIn"].Value<string>();
			}
			if(node["roleIdsIn"] != null)
			{
				this._RoleIdsIn = node["roleIdsIn"].Value<string>();
			}
			if(node["emailEqual"] != null)
			{
				this._EmailEqual = node["emailEqual"].Value<string>();
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (OTTUserOrderBy)StringEnum.Parse(typeof(OTTUserOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaOTTUserFilter");
			kparams.AddIfNotNull("usernameEqual", this._UsernameEqual);
			kparams.AddIfNotNull("externalIdEqual", this._ExternalIdEqual);
			kparams.AddIfNotNull("idIn", this._IdIn);
			kparams.AddIfNotNull("roleIdsIn", this._RoleIdsIn);
			kparams.AddIfNotNull("emailEqual", this._EmailEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case USERNAME_EQUAL:
					return "UsernameEqual";
				case EXTERNAL_ID_EQUAL:
					return "ExternalIdEqual";
				case ID_IN:
					return "IdIn";
				case ROLE_IDS_IN:
					return "RoleIdsIn";
				case EMAIL_EQUAL:
					return "EmailEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

