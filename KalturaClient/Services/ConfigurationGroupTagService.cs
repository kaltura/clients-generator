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
// Copyright (C) 2006-2019  Kaltura Inc.
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
	public class ConfigurationGroupTagAddRequestBuilder : RequestBuilder<ConfigurationGroupTag>
	{
		#region Constants
		public const string CONFIGURATION_GROUP_TAG = "configurationGroupTag";
		#endregion

		public ConfigurationGroupTag ConfigurationGroupTag { get; set; }

		public ConfigurationGroupTagAddRequestBuilder()
			: base("configurationgrouptag", "add")
		{
		}

		public ConfigurationGroupTagAddRequestBuilder(ConfigurationGroupTag configurationGroupTag)
			: this()
		{
			this.ConfigurationGroupTag = configurationGroupTag;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("configurationGroupTag"))
				kparams.AddIfNotNull("configurationGroupTag", ConfigurationGroupTag);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ConfigurationGroupTag>(result);
		}
	}

	public class ConfigurationGroupTagDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string TAG = "tag";
		#endregion

		public string Tag { get; set; }

		public ConfigurationGroupTagDeleteRequestBuilder()
			: base("configurationgrouptag", "delete")
		{
		}

		public ConfigurationGroupTagDeleteRequestBuilder(string tag)
			: this()
		{
			this.Tag = tag;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("tag"))
				kparams.AddIfNotNull("tag", Tag);
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

	public class ConfigurationGroupTagGetRequestBuilder : RequestBuilder<ConfigurationGroupTag>
	{
		#region Constants
		public const string TAG = "tag";
		#endregion

		public string Tag { get; set; }

		public ConfigurationGroupTagGetRequestBuilder()
			: base("configurationgrouptag", "get")
		{
		}

		public ConfigurationGroupTagGetRequestBuilder(string tag)
			: this()
		{
			this.Tag = tag;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("tag"))
				kparams.AddIfNotNull("tag", Tag);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ConfigurationGroupTag>(result);
		}
	}

	public class ConfigurationGroupTagListRequestBuilder : RequestBuilder<ListResponse<ConfigurationGroupTag>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public ConfigurationGroupTagFilter Filter { get; set; }

		public ConfigurationGroupTagListRequestBuilder()
			: base("configurationgrouptag", "list")
		{
		}

		public ConfigurationGroupTagListRequestBuilder(ConfigurationGroupTagFilter filter)
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
			return ObjectFactory.Create<ListResponse<ConfigurationGroupTag>>(result);
		}
	}


	public class ConfigurationGroupTagService
	{
		private ConfigurationGroupTagService()
		{
		}

		public static ConfigurationGroupTagAddRequestBuilder Add(ConfigurationGroupTag configurationGroupTag)
		{
			return new ConfigurationGroupTagAddRequestBuilder(configurationGroupTag);
		}

		public static ConfigurationGroupTagDeleteRequestBuilder Delete(string tag)
		{
			return new ConfigurationGroupTagDeleteRequestBuilder(tag);
		}

		public static ConfigurationGroupTagGetRequestBuilder Get(string tag)
		{
			return new ConfigurationGroupTagGetRequestBuilder(tag);
		}

		public static ConfigurationGroupTagListRequestBuilder List(ConfigurationGroupTagFilter filter)
		{
			return new ConfigurationGroupTagListRequestBuilder(filter);
		}
	}
}
