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
	public class LoginResponse : ObjectBase
	{
		#region Constants
		public const string USER = "user";
		public const string LOGIN_SESSION = "loginSession";
		#endregion

		#region Private Fields
		private OTTUser _User;
		private LoginSession _LoginSession;
		#endregion

		#region Properties
		[JsonProperty]
		public OTTUser User
		{
			get { return _User; }
			set 
			{ 
				_User = value;
				OnPropertyChanged("User");
			}
		}
		[JsonProperty]
		public LoginSession LoginSession
		{
			get { return _LoginSession; }
			set 
			{ 
				_LoginSession = value;
				OnPropertyChanged("LoginSession");
			}
		}
		#endregion

		#region CTor
		public LoginResponse()
		{
		}

		public LoginResponse(JToken node) : base(node)
		{
			if(node["user"] != null)
			{
				this._User = ObjectFactory.Create<OTTUser>(node["user"]);
			}
			if(node["loginSession"] != null)
			{
				this._LoginSession = ObjectFactory.Create<LoginSession>(node["loginSession"]);
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaLoginResponse");
			kparams.AddIfNotNull("user", this._User);
			kparams.AddIfNotNull("loginSession", this._LoginSession);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case USER:
					return "User";
				case LOGIN_SESSION:
					return "LoginSession";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

