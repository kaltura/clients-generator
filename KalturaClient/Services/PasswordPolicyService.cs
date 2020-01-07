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
	public class PasswordPolicyAddRequestBuilder : RequestBuilder<PasswordPolicy>
	{
		#region Constants
		public const string OBJECT_TO_ADD = "objectToAdd";
		#endregion

		public PasswordPolicy ObjectToAdd { get; set; }

		public PasswordPolicyAddRequestBuilder()
			: base("passwordpolicy", "add")
		{
		}

		public PasswordPolicyAddRequestBuilder(PasswordPolicy objectToAdd)
			: this()
		{
			this.ObjectToAdd = objectToAdd;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("objectToAdd"))
				kparams.AddIfNotNull("objectToAdd", ObjectToAdd);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PasswordPolicy>(result);
		}
	}

	public class PasswordPolicyUpdateRequestBuilder : RequestBuilder<PasswordPolicy>
	{
		#region Constants
		public const string ID = "id";
		public const string OBJECT_TO_UPDATE = "objectToUpdate";
		#endregion

		public long Id { get; set; }
		public PasswordPolicy ObjectToUpdate { get; set; }

		public PasswordPolicyUpdateRequestBuilder()
			: base("passwordpolicy", "update")
		{
		}

		public PasswordPolicyUpdateRequestBuilder(long id, PasswordPolicy objectToUpdate)
			: this()
		{
			this.Id = id;
			this.ObjectToUpdate = objectToUpdate;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("objectToUpdate"))
				kparams.AddIfNotNull("objectToUpdate", ObjectToUpdate);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PasswordPolicy>(result);
		}
	}

	public class PasswordPolicyDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public PasswordPolicyDeleteRequestBuilder()
			: base("passwordpolicy", "delete")
		{
		}

		public PasswordPolicyDeleteRequestBuilder(long id)
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

	public class PasswordPolicyListRequestBuilder : RequestBuilder<ListResponse<PasswordPolicy>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public PasswordPolicyFilter Filter { get; set; }

		public PasswordPolicyListRequestBuilder()
			: base("passwordpolicy", "list")
		{
		}

		public PasswordPolicyListRequestBuilder(PasswordPolicyFilter filter)
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
			return ObjectFactory.Create<ListResponse<PasswordPolicy>>(result);
		}
	}


	public class PasswordPolicyService
	{
		private PasswordPolicyService()
		{
		}

		public static PasswordPolicyAddRequestBuilder Add(PasswordPolicy objectToAdd)
		{
			return new PasswordPolicyAddRequestBuilder(objectToAdd);
		}

		public static PasswordPolicyUpdateRequestBuilder Update(long id, PasswordPolicy objectToUpdate)
		{
			return new PasswordPolicyUpdateRequestBuilder(id, objectToUpdate);
		}

		public static PasswordPolicyDeleteRequestBuilder Delete(long id)
		{
			return new PasswordPolicyDeleteRequestBuilder(id);
		}

		public static PasswordPolicyListRequestBuilder List(PasswordPolicyFilter filter = null)
		{
			return new PasswordPolicyListRequestBuilder(filter);
		}
	}
}
