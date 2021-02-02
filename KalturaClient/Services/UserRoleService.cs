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
using System.IO;
using Kaltura.Request;
using Kaltura.Types;
using Kaltura.Enums;
using Newtonsoft.Json.Linq;

namespace Kaltura.Services
{
// BEO-9522 csharp2 before comment
	public class UserRoleAddRequestBuilder : RequestBuilder<UserRole>
	{
		#region Constants
		public const string ROLE = "role";
		#endregion

		public UserRole Role { get; set; }

		public UserRoleAddRequestBuilder()
			: base("userrole", "add")
		{
		}

		public UserRoleAddRequestBuilder(UserRole role)
			: this()
		{
			this.Role = role;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("role"))
				kparams.AddIfNotNull("role", Role);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<UserRole>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class UserRoleDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public UserRoleDeleteRequestBuilder()
			: base("userrole", "delete")
		{
		}

		public UserRoleDeleteRequestBuilder(long id)
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

// BEO-9522 csharp2 before comment
	public class UserRoleListRequestBuilder : RequestBuilder<ListResponse<UserRole>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public UserRoleFilter Filter { get; set; }

		public UserRoleListRequestBuilder()
			: base("userrole", "list")
		{
		}

		public UserRoleListRequestBuilder(UserRoleFilter filter)
			: this()
		{
			this.Filter = filter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("filter"))
				kparams.AddIfNotNull("filter", Filter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ListResponse<UserRole>>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class UserRoleUpdateRequestBuilder : RequestBuilder<UserRole>
	{
		#region Constants
		public const string ID = "id";
		public const string ROLE = "role";
		#endregion

		public long Id { get; set; }
		public UserRole Role { get; set; }

		public UserRoleUpdateRequestBuilder()
			: base("userrole", "update")
		{
		}

		public UserRoleUpdateRequestBuilder(long id, UserRole role)
			: this()
		{
			this.Id = id;
			this.Role = role;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("role"))
				kparams.AddIfNotNull("role", Role);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<UserRole>(result);
		}
	}


	public class UserRoleService
	{
		private UserRoleService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static UserRoleAddRequestBuilder Add(UserRole role)
		{
			return new UserRoleAddRequestBuilder(role);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static UserRoleDeleteRequestBuilder Delete(long id)
		{
			return new UserRoleDeleteRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static UserRoleListRequestBuilder List(UserRoleFilter filter = null)
		{
			return new UserRoleListRequestBuilder(filter);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static UserRoleUpdateRequestBuilder Update(long id, UserRole role)
		{
			return new UserRoleUpdateRequestBuilder(id, role);
		}
	}
}
