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
	public class PartnerAddRequestBuilder : RequestBuilder<Partner>
	{
		#region Constants
		public const string PARTNER = "partner";
		public const string PARTNER_SETUP = "partnerSetup";
		#endregion

		public Partner Partner { get; set; }
		public PartnerSetup PartnerSetup { get; set; }

		public PartnerAddRequestBuilder()
			: base("partner", "add")
		{
		}

		public PartnerAddRequestBuilder(Partner partner, PartnerSetup partnerSetup)
			: this()
		{
			this.Partner = partner;
			this.PartnerSetup = partnerSetup;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("partner"))
				kparams.AddIfNotNull("partner", Partner);
			if (!isMapped("partnerSetup"))
				kparams.AddIfNotNull("partnerSetup", PartnerSetup);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Partner>(result);
		}
	}

	public class PartnerExternalLoginRequestBuilder : RequestBuilder<LoginSession>
	{
		#region Constants
		#endregion


		public PartnerExternalLoginRequestBuilder()
			: base("partner", "externalLogin")
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
			return ObjectFactory.Create<LoginSession>(result);
		}
	}

	public class PartnerListRequestBuilder : RequestBuilder<ListResponse<Partner>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public PartnerFilter Filter { get; set; }

		public PartnerListRequestBuilder()
			: base("partner", "list")
		{
		}

		public PartnerListRequestBuilder(PartnerFilter filter)
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
			return ObjectFactory.Create<ListResponse<Partner>>(result);
		}
	}


	public class PartnerService
	{
		private PartnerService()
		{
		}

		public static PartnerAddRequestBuilder Add(Partner partner, PartnerSetup partnerSetup)
		{
			return new PartnerAddRequestBuilder(partner, partnerSetup);
		}

		public static PartnerExternalLoginRequestBuilder ExternalLogin()
		{
			return new PartnerExternalLoginRequestBuilder();
		}

		public static PartnerListRequestBuilder List(PartnerFilter filter = null)
		{
			return new PartnerListRequestBuilder(filter);
		}
	}
}
