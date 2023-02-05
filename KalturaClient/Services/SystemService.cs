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
	public class SystemClearLocalServerCacheRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string CLEAR_CACHE_ACTION = "clearCacheAction";
		public const string KEY = "key";
		#endregion

		public string ClearCacheAction { get; set; }
		public string Key { get; set; }

		public SystemClearLocalServerCacheRequestBuilder()
			: base("system", "clearLocalServerCache")
		{
		}

		public SystemClearLocalServerCacheRequestBuilder(string clearCacheAction, string key)
			: this()
		{
			this.ClearCacheAction = clearCacheAction;
			this.Key = key;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("clearCacheAction"))
				kparams.AddIfNotNull("clearCacheAction", ClearCacheAction);
			if (!isMapped("key"))
				kparams.AddIfNotNull("key", Key);
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

	public class SystemGetTimeRequestBuilder : RequestBuilder<long>
	{
		#region Constants
		#endregion


		public SystemGetTimeRequestBuilder()
			: base("system", "getTime")
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
			return result.Value<long>();
		}
	}

	public class SystemGetVersionRequestBuilder : RequestBuilder<string>
	{
		#region Constants
		#endregion


		public SystemGetVersionRequestBuilder()
			: base("system", "getVersion")
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

	public class SystemIncrementLayeredCacheGroupConfigVersionRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string GROUP_ID = "groupId";
		#endregion

		public int GroupId { get; set; }

		public SystemIncrementLayeredCacheGroupConfigVersionRequestBuilder()
			: base("system", "incrementLayeredCacheGroupConfigVersion")
		{
		}

		public SystemIncrementLayeredCacheGroupConfigVersionRequestBuilder(int groupId)
			: this()
		{
			this.GroupId = groupId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("groupId"))
				kparams.AddIfNotNull("groupId", GroupId);
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

	public class SystemPingRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		#endregion


		public SystemPingRequestBuilder()
			: base("system", "ping")
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
			if (result.Value<string>().Equals("1") || result.Value<string>().ToLower().Equals("true"))
				return true;
			return false;
		}
	}


	public class SystemService
	{
		private SystemService()
		{
		}

		public static SystemClearLocalServerCacheRequestBuilder ClearLocalServerCache(string clearCacheAction = null, string key = null)
		{
			return new SystemClearLocalServerCacheRequestBuilder(clearCacheAction, key);
		}

		public static SystemGetTimeRequestBuilder GetTime()
		{
			return new SystemGetTimeRequestBuilder();
		}

		public static SystemGetVersionRequestBuilder GetVersion()
		{
			return new SystemGetVersionRequestBuilder();
		}

		public static SystemIncrementLayeredCacheGroupConfigVersionRequestBuilder IncrementLayeredCacheGroupConfigVersion(int groupId = 0)
		{
			return new SystemIncrementLayeredCacheGroupConfigVersionRequestBuilder(groupId);
		}

		public static SystemPingRequestBuilder Ping()
		{
			return new SystemPingRequestBuilder();
		}
	}
}
