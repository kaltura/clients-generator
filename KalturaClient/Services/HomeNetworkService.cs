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
	public class HomeNetworkAddRequestBuilder : RequestBuilder<HomeNetwork>
	{
		#region Constants
		public const string HOME_NETWORK = "homeNetwork";
		#endregion

		public HomeNetwork HomeNetwork { get; set; }

		public HomeNetworkAddRequestBuilder()
			: base("homenetwork", "add")
		{
		}

		public HomeNetworkAddRequestBuilder(HomeNetwork homeNetwork)
			: this()
		{
			this.HomeNetwork = homeNetwork;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("homeNetwork"))
				kparams.AddIfNotNull("homeNetwork", HomeNetwork);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<HomeNetwork>(result);
		}
	}

	public class HomeNetworkDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string EXTERNAL_ID = "externalId";
		#endregion

		public string ExternalId { get; set; }

		public HomeNetworkDeleteRequestBuilder()
			: base("homenetwork", "delete")
		{
		}

		public HomeNetworkDeleteRequestBuilder(string externalId)
			: this()
		{
			this.ExternalId = externalId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("externalId"))
				kparams.AddIfNotNull("externalId", ExternalId);
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

	public class HomeNetworkListRequestBuilder : RequestBuilder<ListResponse<HomeNetwork>>
	{
		#region Constants
		#endregion


		public HomeNetworkListRequestBuilder()
			: base("homenetwork", "list")
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
			return ObjectFactory.Create<ListResponse<HomeNetwork>>(result);
		}
	}

	public class HomeNetworkUpdateRequestBuilder : RequestBuilder<HomeNetwork>
	{
		#region Constants
		public const string EXTERNAL_ID = "externalId";
		public const string HOME_NETWORK = "homeNetwork";
		#endregion

		public string ExternalId { get; set; }
		public HomeNetwork HomeNetwork { get; set; }

		public HomeNetworkUpdateRequestBuilder()
			: base("homenetwork", "update")
		{
		}

		public HomeNetworkUpdateRequestBuilder(string externalId, HomeNetwork homeNetwork)
			: this()
		{
			this.ExternalId = externalId;
			this.HomeNetwork = homeNetwork;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("externalId"))
				kparams.AddIfNotNull("externalId", ExternalId);
			if (!isMapped("homeNetwork"))
				kparams.AddIfNotNull("homeNetwork", HomeNetwork);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<HomeNetwork>(result);
		}
	}


	public class HomeNetworkService
	{
		private HomeNetworkService()
		{
		}

		public static HomeNetworkAddRequestBuilder Add(HomeNetwork homeNetwork)
		{
			return new HomeNetworkAddRequestBuilder(homeNetwork);
		}

		public static HomeNetworkDeleteRequestBuilder Delete(string externalId)
		{
			return new HomeNetworkDeleteRequestBuilder(externalId);
		}

		public static HomeNetworkListRequestBuilder List()
		{
			return new HomeNetworkListRequestBuilder();
		}

		public static HomeNetworkUpdateRequestBuilder Update(string externalId, HomeNetwork homeNetwork)
		{
			return new HomeNetworkUpdateRequestBuilder(externalId, homeNetwork);
		}
	}
}
