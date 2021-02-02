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
	public class PartnerConfigurationListRequestBuilder : RequestBuilder<ListResponse<PartnerConfiguration>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public PartnerConfigurationFilter Filter { get; set; }

		public PartnerConfigurationListRequestBuilder()
			: base("partnerconfiguration", "list")
		{
		}

		public PartnerConfigurationListRequestBuilder(PartnerConfigurationFilter filter)
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
			return ObjectFactory.Create<ListResponse<PartnerConfiguration>>(result);
		}
	}

	public class PartnerConfigurationUpdateRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string CONFIGURATION = "configuration";
		#endregion

		public PartnerConfiguration Configuration { get; set; }

		public PartnerConfigurationUpdateRequestBuilder()
			: base("partnerconfiguration", "update")
		{
		}

		public PartnerConfigurationUpdateRequestBuilder(PartnerConfiguration configuration)
			: this()
		{
			this.Configuration = configuration;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("configuration"))
				kparams.AddIfNotNull("configuration", Configuration);
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


	public class PartnerConfigurationService
	{
		private PartnerConfigurationService()
		{
		}

		public static PartnerConfigurationListRequestBuilder List(PartnerConfigurationFilter filter)
		{
			return new PartnerConfigurationListRequestBuilder(filter);
		}

		public static PartnerConfigurationUpdateRequestBuilder Update(PartnerConfiguration configuration)
		{
			return new PartnerConfigurationUpdateRequestBuilder(configuration);
		}
	}
}
