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
// Copyright (C) 2006-2020  Kaltura Inc.
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
using Newtonsoft.Json.Linq;

namespace Kaltura.Services
{
	public class UserInterestAddRequestBuilder : RequestBuilder<UserInterest>
	{
		#region Constants
		public const string USER_INTEREST = "userInterest";
		#endregion

		public UserInterest UserInterest { get; set; }

		public UserInterestAddRequestBuilder()
			: base("userinterest", "add")
		{
		}

		public UserInterestAddRequestBuilder(UserInterest userInterest)
			: this()
		{
			this.UserInterest = userInterest;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("userInterest"))
				kparams.AddIfNotNull("userInterest", UserInterest);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<UserInterest>(result);
		}
	}

	public class UserInterestDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public string Id { get; set; }

		public UserInterestDeleteRequestBuilder()
			: base("userinterest", "delete")
		{
		}

		public UserInterestDeleteRequestBuilder(string id)
			: this()
		{
			this.Id = id;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			if (result.Value<string>().Equals("1") || result.Value<string>().ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class UserInterestDeleteWithTokenRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		public const string TOKEN = "token";
		public new const string PARTNER_ID = "partnerId";
		#endregion

		public string Id { get; set; }
		public string Token { get; set; }
		public new int PartnerId { get; set; }

		public UserInterestDeleteWithTokenRequestBuilder()
			: base("userinterest", "deleteWithToken")
		{
		}

		public UserInterestDeleteWithTokenRequestBuilder(string id, string token, int partnerId)
			: this()
		{
			this.Id = id;
			this.Token = token;
			this.PartnerId = partnerId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("token"))
				kparams.AddIfNotNull("token", Token);
			if (!isMapped("partnerId"))
				kparams.AddIfNotNull("partnerId", PartnerId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return null;
		}
	}

	public class UserInterestListRequestBuilder : RequestBuilder<ListResponse<UserInterest>>
	{
		#region Constants
		#endregion


		public UserInterestListRequestBuilder()
			: base("userinterest", "list")
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

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ListResponse<UserInterest>>(result);
		}
	}


	public class UserInterestService
	{
		private UserInterestService()
		{
		}

		public static UserInterestAddRequestBuilder Add(UserInterest userInterest)
		{
			return new UserInterestAddRequestBuilder(userInterest);
		}

		public static UserInterestDeleteRequestBuilder Delete(string id)
		{
			return new UserInterestDeleteRequestBuilder(id);
		}

		public static UserInterestDeleteWithTokenRequestBuilder DeleteWithToken(string id, string token, int partnerId)
		{
			return new UserInterestDeleteWithTokenRequestBuilder(id, token, partnerId);
		}

		public static UserInterestListRequestBuilder List()
		{
			return new UserInterestListRequestBuilder();
		}
	}
}
