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
// Copyright (C) 2006-2017  Kaltura Inc.
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
	public class ConfigurationGroupAddRequestBuilder : RequestBuilder<ConfigurationGroup>
	{
		#region Constants
		public const string CONFIGURATION_GROUP = "configurationGroup";
		#endregion

		public ConfigurationGroup ConfigurationGroup
		{
			set;
			get;
		}

		public ConfigurationGroupAddRequestBuilder()
			: base("configurationgroup", "add")
		{
		}

		public ConfigurationGroupAddRequestBuilder(ConfigurationGroup configurationGroup)
			: this()
		{
			this.ConfigurationGroup = configurationGroup;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("configurationGroup"))
				kparams.AddIfNotNull("configurationGroup", ConfigurationGroup);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ConfigurationGroup>(result);
		}
	}

	public class ConfigurationGroupDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public string Id
		{
			set;
			get;
		}

		public ConfigurationGroupDeleteRequestBuilder()
			: base("configurationgroup", "delete")
		{
		}

		public ConfigurationGroupDeleteRequestBuilder(string id)
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

		public override object Deserialize(XmlElement result)
		{
			if (result.InnerText.Equals("1") || result.InnerText.ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class ConfigurationGroupGetRequestBuilder : RequestBuilder<ConfigurationGroup>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public string Id
		{
			set;
			get;
		}

		public ConfigurationGroupGetRequestBuilder()
			: base("configurationgroup", "get")
		{
		}

		public ConfigurationGroupGetRequestBuilder(string id)
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

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ConfigurationGroup>(result);
		}
	}

	public class ConfigurationGroupListRequestBuilder : RequestBuilder<ListResponse<ConfigurationGroup>>
	{
		#region Constants
		#endregion


		public ConfigurationGroupListRequestBuilder()
			: base("configurationgroup", "list")
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
			return ObjectFactory.Create<ListResponse<ConfigurationGroup>>(result);
		}
	}

	public class ConfigurationGroupUpdateRequestBuilder : RequestBuilder<ConfigurationGroup>
	{
		#region Constants
		public const string ID = "id";
		public const string CONFIGURATION_GROUP = "configurationGroup";
		#endregion

		public string Id
		{
			set;
			get;
		}
		public ConfigurationGroup ConfigurationGroup
		{
			set;
			get;
		}

		public ConfigurationGroupUpdateRequestBuilder()
			: base("configurationgroup", "update")
		{
		}

		public ConfigurationGroupUpdateRequestBuilder(string id, ConfigurationGroup configurationGroup)
			: this()
		{
			this.Id = id;
			this.ConfigurationGroup = configurationGroup;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("configurationGroup"))
				kparams.AddIfNotNull("configurationGroup", ConfigurationGroup);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ConfigurationGroup>(result);
		}
	}


	public class ConfigurationGroupService
	{
		private ConfigurationGroupService()
		{
		}

		public static ConfigurationGroupAddRequestBuilder Add(ConfigurationGroup configurationGroup)
		{
			return new ConfigurationGroupAddRequestBuilder(configurationGroup);
		}

		public static ConfigurationGroupDeleteRequestBuilder Delete(string id)
		{
			return new ConfigurationGroupDeleteRequestBuilder(id);
		}

		public static ConfigurationGroupGetRequestBuilder Get(string id)
		{
			return new ConfigurationGroupGetRequestBuilder(id);
		}

		public static ConfigurationGroupListRequestBuilder List()
		{
			return new ConfigurationGroupListRequestBuilder();
		}

		public static ConfigurationGroupUpdateRequestBuilder Update(string id, ConfigurationGroup configurationGroup)
		{
			return new ConfigurationGroupUpdateRequestBuilder(id, configurationGroup);
		}
	}
}
