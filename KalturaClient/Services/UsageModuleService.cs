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
	public class UsageModuleAddRequestBuilder : RequestBuilder<UsageModule>
	{
		#region Constants
		public const string USAGE_MODULE = "usageModule";
		#endregion

		public UsageModule UsageModule { get; set; }

		public UsageModuleAddRequestBuilder()
			: base("usagemodule", "add")
		{
		}

		public UsageModuleAddRequestBuilder(UsageModule usageModule)
			: this()
		{
			this.UsageModule = usageModule;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("usageModule"))
				kparams.AddIfNotNull("usageModule", UsageModule);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<UsageModule>(result);
		}
	}

	public class UsageModuleDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public UsageModuleDeleteRequestBuilder()
			: base("usagemodule", "delete")
		{
		}

		public UsageModuleDeleteRequestBuilder(long id)
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

	public class UsageModuleListRequestBuilder : RequestBuilder<ListResponse<UsageModule>>
	{
		#region Constants
		#endregion


		public UsageModuleListRequestBuilder()
			: base("usagemodule", "list")
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
			return ObjectFactory.Create<ListResponse<UsageModule>>(result);
		}
	}


	public class UsageModuleService
	{
		private UsageModuleService()
		{
		}

		public static UsageModuleAddRequestBuilder Add(UsageModule usageModule)
		{
			return new UsageModuleAddRequestBuilder(usageModule);
		}

		public static UsageModuleDeleteRequestBuilder Delete(long id)
		{
			return new UsageModuleDeleteRequestBuilder(id);
		}

		public static UsageModuleListRequestBuilder List()
		{
			return new UsageModuleListRequestBuilder();
		}
	}
}
