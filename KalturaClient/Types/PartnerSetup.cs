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
	public class PartnerSetup : ObjectBase
	{
		#region Constants
		public const string ADMIN_USERNAME = "adminUsername";
		public const string ADMIN_PASSWORD = "adminPassword";
		public const string BASE_PARTNER_CONFIGURATION = "basePartnerConfiguration";
		#endregion

		#region Private Fields
		private string _AdminUsername = null;
		private string _AdminPassword = null;
		private BasePartnerConfiguration _BasePartnerConfiguration;
		#endregion

		#region Properties
		[JsonProperty]
		public string AdminUsername
		{
			get { return _AdminUsername; }
			set 
			{ 
				_AdminUsername = value;
				OnPropertyChanged("AdminUsername");
			}
		}
		[JsonProperty]
		public string AdminPassword
		{
			get { return _AdminPassword; }
			set 
			{ 
				_AdminPassword = value;
				OnPropertyChanged("AdminPassword");
			}
		}
		[JsonProperty]
		public BasePartnerConfiguration BasePartnerConfiguration
		{
			get { return _BasePartnerConfiguration; }
			set 
			{ 
				_BasePartnerConfiguration = value;
				OnPropertyChanged("BasePartnerConfiguration");
			}
		}
		#endregion

		#region CTor
		public PartnerSetup()
		{
		}

		public PartnerSetup(JToken node) : base(node)
		{
			if(node["adminUsername"] != null)
			{
				this._AdminUsername = node["adminUsername"].Value<string>();
			}
			if(node["adminPassword"] != null)
			{
				this._AdminPassword = node["adminPassword"].Value<string>();
			}
			if(node["basePartnerConfiguration"] != null)
			{
				this._BasePartnerConfiguration = ObjectFactory.Create<BasePartnerConfiguration>(node["basePartnerConfiguration"]);
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPartnerSetup");
			kparams.AddIfNotNull("adminUsername", this._AdminUsername);
			kparams.AddIfNotNull("adminPassword", this._AdminPassword);
			kparams.AddIfNotNull("basePartnerConfiguration", this._BasePartnerConfiguration);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ADMIN_USERNAME:
					return "AdminUsername";
				case ADMIN_PASSWORD:
					return "AdminPassword";
				case BASE_PARTNER_CONFIGURATION:
					return "BasePartnerConfiguration";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

