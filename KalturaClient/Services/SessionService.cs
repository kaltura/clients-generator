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
using System.IO;
using Kaltura.Request;
using Kaltura.Types;
using Kaltura.Enums;

namespace Kaltura.Services
{
	public class SessionGetRequestBuilder : RequestBuilder<Session>
	{
		#region Constants
		public const string SESSION = "session";
		#endregion

		public string Session
		{
			set;
			get;
		}

		public SessionGetRequestBuilder()
			: base("session", "get")
		{
		}

		public SessionGetRequestBuilder(string session)
			: this()
		{
			this.Session = session;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("session"))
				kparams.AddIfNotNull("session", Session);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<Session>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<Session>((IDictionary<string,object>)result);
		}
	}

	public class SessionRevokeRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		#endregion


		public SessionRevokeRequestBuilder()
			: base("session", "revoke")
		{
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			if (result.InnerText.Equals("1") || result.InnerText.ToLower().Equals("true"))
				return true;
			return false;
		}
		public override object DeserializeObject(object result)
		{
			var resultStr = (string)result;
			if (resultStr.Equals("1") || resultStr.ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class SessionSwitchUserRequestBuilder : RequestBuilder<LoginSession>
	{
		#region Constants
		public const string USER_ID_TO_SWITCH = "userIdToSwitch";
		#endregion

		public string UserIdToSwitch
		{
			set;
			get;
		}

		public SessionSwitchUserRequestBuilder()
			: base("session", "switchUser")
		{
		}

		public SessionSwitchUserRequestBuilder(string userIdToSwitch)
			: this()
		{
			this.UserIdToSwitch = userIdToSwitch;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("userIdToSwitch"))
				kparams.AddIfNotNull("userIdToSwitch", UserIdToSwitch);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<LoginSession>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<LoginSession>((IDictionary<string,object>)result);
		}
	}


	public class SessionService
	{
		private SessionService()
		{
		}

		public static SessionGetRequestBuilder Get(string session = null)
		{
			return new SessionGetRequestBuilder(session);
		}

		public static SessionRevokeRequestBuilder Revoke()
		{
			return new SessionRevokeRequestBuilder();
		}

		public static SessionSwitchUserRequestBuilder SwitchUser(string userIdToSwitch)
		{
			return new SessionSwitchUserRequestBuilder(userIdToSwitch);
		}
	}
}
