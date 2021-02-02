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
	public class ConfigurationsAddRequestBuilder : RequestBuilder<Configurations>
	{
		#region Constants
		public const string CONFIGURATIONS = "configurations";
		#endregion

		public Configurations Configurations { get; set; }

		public ConfigurationsAddRequestBuilder()
			: base("configurations", "add")
		{
		}

		public ConfigurationsAddRequestBuilder(Configurations configurations)
			: this()
		{
			this.Configurations = configurations;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("configurations"))
				kparams.AddIfNotNull("configurations", Configurations);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Configurations>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class ConfigurationsDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public string Id { get; set; }

		public ConfigurationsDeleteRequestBuilder()
			: base("configurations", "delete")
		{
		}

		public ConfigurationsDeleteRequestBuilder(string id)
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
	public class ConfigurationsGetRequestBuilder : RequestBuilder<Configurations>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public string Id { get; set; }

		public ConfigurationsGetRequestBuilder()
			: base("configurations", "get")
		{
		}

		public ConfigurationsGetRequestBuilder(string id)
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
			return ObjectFactory.Create<Configurations>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class ConfigurationsListRequestBuilder : RequestBuilder<ListResponse<Configurations>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public ConfigurationsFilter Filter { get; set; }

		public ConfigurationsListRequestBuilder()
			: base("configurations", "list")
		{
		}

		public ConfigurationsListRequestBuilder(ConfigurationsFilter filter)
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
			return ObjectFactory.Create<ListResponse<Configurations>>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class ConfigurationsUpdateRequestBuilder : RequestBuilder<Configurations>
	{
		#region Constants
		public const string ID = "id";
		public const string CONFIGURATIONS = "configurations";
		#endregion

		public string Id { get; set; }
		public Configurations Configurations { get; set; }

		public ConfigurationsUpdateRequestBuilder()
			: base("configurations", "update")
		{
		}

		public ConfigurationsUpdateRequestBuilder(string id, Configurations configurations)
			: this()
		{
			this.Id = id;
			this.Configurations = configurations;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("configurations"))
				kparams.AddIfNotNull("configurations", Configurations);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Configurations>(result);
		}
	}


	public class ConfigurationsService
	{
		private ConfigurationsService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static ConfigurationsAddRequestBuilder Add(Configurations configurations)
		{
			return new ConfigurationsAddRequestBuilder(configurations);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static ConfigurationsDeleteRequestBuilder Delete(string id)
		{
			return new ConfigurationsDeleteRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static ConfigurationsGetRequestBuilder Get(string id)
		{
			return new ConfigurationsGetRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static ConfigurationsListRequestBuilder List(ConfigurationsFilter filter)
		{
			return new ConfigurationsListRequestBuilder(filter);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static ConfigurationsUpdateRequestBuilder Update(string id, Configurations configurations)
		{
			return new ConfigurationsUpdateRequestBuilder(id, configurations);
		}
	}
}
