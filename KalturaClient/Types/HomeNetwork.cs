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
	public class HomeNetwork : ObjectBase
	{
		#region Constants
		public const string EXTERNAL_ID = "externalId";
		public const string NAME = "name";
		public const string DESCRIPTION = "description";
		public const string IS_ACTIVE = "isActive";
		#endregion

		#region Private Fields
		private string _ExternalId = null;
		private string _Name = null;
		private string _Description = null;
		private bool? _IsActive = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
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
		public string Description
		{
			get { return _Description; }
			set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		[JsonProperty]
		public bool? IsActive
		{
			get { return _IsActive; }
			set 
			{ 
				_IsActive = value;
				OnPropertyChanged("IsActive");
			}
		}
		#endregion

		#region CTor
		public HomeNetwork()
		{
		}

		public HomeNetwork(JToken node) : base(node)
		{
			if(node["externalId"] != null)
			{
				this._ExternalId = node["externalId"].Value<string>();
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["description"] != null)
			{
				this._Description = node["description"].Value<string>();
			}
			if(node["isActive"] != null)
			{
				this._IsActive = ParseBool(node["isActive"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaHomeNetwork");
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("isActive", this._IsActive);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case EXTERNAL_ID:
					return "ExternalId";
				case NAME:
					return "Name";
				case DESCRIPTION:
					return "Description";
				case IS_ACTIVE:
					return "IsActive";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

