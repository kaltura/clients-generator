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

	public class PermissionAddPermissionItemRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string PERMISSION_ID = "permissionId";
		public const string PERMISSION_ITEM_ID = "permissionItemId";
		#endregion

		public long PermissionId { get; set; }
		public long PermissionItemId { get; set; }

		public PermissionAddPermissionItemRequestBuilder()
			: base("permission", "addPermissionItem")
		{
		}

		public PermissionAddPermissionItemRequestBuilder(long permissionId, long permissionItemId)
			: this()
		{
			this.PermissionId = permissionId;
			this.PermissionItemId = permissionItemId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("permissionId"))
				kparams.AddIfNotNull("permissionId", PermissionId);
			if (!isMapped("permissionItemId"))
				kparams.AddIfNotNull("permissionItemId", PermissionItemId);
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

		public BasePermissionFilter Filter { get; set; }

		public PermissionListRequestBuilder()
			: base("permission", "list")
		{
		}

		public PermissionListRequestBuilder(BasePermissionFilter filter)
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

	public class PermissionRemovePermissionItemRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string PERMISSION_ID = "permissionId";
		public const string PERMISSION_ITEM_ID = "permissionItemId";
		#endregion

		public long PermissionId { get; set; }
		public long PermissionItemId { get; set; }

		public PermissionRemovePermissionItemRequestBuilder()
			: base("permission", "removePermissionItem")
		{
		}

		public PermissionRemovePermissionItemRequestBuilder(long permissionId, long permissionItemId)
			: this()
		{
			this.PermissionId = permissionId;
			this.PermissionItemId = permissionItemId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("permissionId"))
				kparams.AddIfNotNull("permissionId", PermissionId);
			if (!isMapped("permissionItemId"))
				kparams.AddIfNotNull("permissionItemId", PermissionItemId);
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

	public class PermissionUpdateRequestBuilder : RequestBuilder<Permission>
	{
		#region Constants
		public const string ID = "id";
		public const string PERMISSION = "permission";
		#endregion

		public long Id { get; set; }
		public Permission Permission { get; set; }

		public PermissionUpdateRequestBuilder()
			: base("permission", "update")
		{
		}

		public PermissionUpdateRequestBuilder(long id, Permission permission)
			: this()
		{
			this.Id = id;
			this.Permission = permission;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
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


	public class PermissionService
	{
		private PermissionService()
		{
		}

		public static PermissionAddRequestBuilder Add(Permission permission)
		{
			return new PermissionAddRequestBuilder(permission);
		}

		public static PermissionAddPermissionItemRequestBuilder AddPermissionItem(long permissionId, long permissionItemId)
		{
			return new PermissionAddPermissionItemRequestBuilder(permissionId, permissionItemId);
		}

		public static PermissionDeleteRequestBuilder Delete(long id)
		{
			return new PermissionDeleteRequestBuilder(id);
		}

		public static PermissionGetCurrentPermissionsRequestBuilder GetCurrentPermissions()
		{
			return new PermissionGetCurrentPermissionsRequestBuilder();
		}

		public static PermissionListRequestBuilder List(BasePermissionFilter filter = null)
		{
			return new PermissionListRequestBuilder(filter);
		}

		public static PermissionRemovePermissionItemRequestBuilder RemovePermissionItem(long permissionId, long permissionItemId)
		{
			return new PermissionRemovePermissionItemRequestBuilder(permissionId, permissionItemId);
		}

		public static PermissionUpdateRequestBuilder Update(long id, Permission permission)
		{
			return new PermissionUpdateRequestBuilder(id, permission);
		}
	}
}
