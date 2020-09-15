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
	public class PermissionAddRequestBuilder : RequestBuilder<Permission>
	{
		#region Constants
		public const string PERMISSION = "permission";
		#endregion

		public Permission Permission { get; set; }

		public PermissionAddRequestBuilder()
			: base("permission", "add")
		{
		}

		public PermissionAddRequestBuilder(Permission permission)
			: this()
		{
			this.Permission = permission;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("permission"))
				kparams.AddIfNotNull("permission", Permission);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Permission>(result);
		}
	}

	public class PermissionDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public PermissionDeleteRequestBuilder()
			: base("permission", "delete")
		{
		}

		public PermissionDeleteRequestBuilder(long id)
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
			return null;
		}
	}

	public class PermissionGetCurrentPermissionsRequestBuilder : RequestBuilder<string>
	{
		#region Constants
		#endregion


		public PermissionGetCurrentPermissionsRequestBuilder()
			: base("permission", "getCurrentPermissions")
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
			return result.Value<string>();
		}
	}

	public class PermissionListRequestBuilder : RequestBuilder<ListResponse<Permission>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public PermissionFilter Filter { get; set; }

		public PermissionListRequestBuilder()
			: base("permission", "list")
		{
		}

		public PermissionListRequestBuilder(PermissionFilter filter)
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
			return ObjectFactory.Create<ListResponse<Permission>>(result);
		}
	}


	public class PermissionService
	{
		private PermissionService()
		{
		}

		public static PermissionAddRequestBuilder Add(Permission permission)
		{
			return new PermissionAddRequestBuilder(permission);
		}

		public static PermissionDeleteRequestBuilder Delete(long id)
		{
			return new PermissionDeleteRequestBuilder(id);
		}

		public static PermissionGetCurrentPermissionsRequestBuilder GetCurrentPermissions()
		{
			return new PermissionGetCurrentPermissionsRequestBuilder();
		}

		public static PermissionListRequestBuilder List(PermissionFilter filter = null)
		{
			return new PermissionListRequestBuilder(filter);
		}
	}
}
