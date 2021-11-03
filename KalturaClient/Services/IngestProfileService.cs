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
	public class IngestProfileAddRequestBuilder : RequestBuilder<IngestProfile>
	{
		#region Constants
		public const string INGEST_PROFILE = "ingestProfile";
		#endregion

		public IngestProfile IngestProfile { get; set; }

		public IngestProfileAddRequestBuilder()
			: base("ingestprofile", "add")
		{
		}

		public IngestProfileAddRequestBuilder(IngestProfile ingestProfile)
			: this()
		{
			this.IngestProfile = ingestProfile;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ingestProfile"))
				kparams.AddIfNotNull("ingestProfile", IngestProfile);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<IngestProfile>(result);
		}
	}

	public class IngestProfileDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string INGEST_PROFILE_ID = "ingestProfileId";
		#endregion

		public int IngestProfileId { get; set; }

		public IngestProfileDeleteRequestBuilder()
			: base("ingestprofile", "delete")
		{
		}

		public IngestProfileDeleteRequestBuilder(int ingestProfileId)
			: this()
		{
			this.IngestProfileId = ingestProfileId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ingestProfileId"))
				kparams.AddIfNotNull("ingestProfileId", IngestProfileId);
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

	public class IngestProfileListRequestBuilder : RequestBuilder<ListResponse<IngestProfile>>
	{
		#region Constants
		#endregion


		public IngestProfileListRequestBuilder()
			: base("ingestprofile", "list")
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
			return ObjectFactory.Create<ListResponse<IngestProfile>>(result);
		}
	}

	public class IngestProfileUpdateRequestBuilder : RequestBuilder<IngestProfile>
	{
		#region Constants
		public const string INGEST_PROFILE_ID = "ingestProfileId";
		public const string INGEST_PROFILE = "ingestProfile";
		#endregion

		public int IngestProfileId { get; set; }
		public IngestProfile IngestProfile { get; set; }

		public IngestProfileUpdateRequestBuilder()
			: base("ingestprofile", "update")
		{
		}

		public IngestProfileUpdateRequestBuilder(int ingestProfileId, IngestProfile ingestProfile)
			: this()
		{
			this.IngestProfileId = ingestProfileId;
			this.IngestProfile = ingestProfile;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ingestProfileId"))
				kparams.AddIfNotNull("ingestProfileId", IngestProfileId);
			if (!isMapped("ingestProfile"))
				kparams.AddIfNotNull("ingestProfile", IngestProfile);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<IngestProfile>(result);
		}
	}


	public class IngestProfileService
	{
		private IngestProfileService()
		{
		}

		public static IngestProfileAddRequestBuilder Add(IngestProfile ingestProfile)
		{
			return new IngestProfileAddRequestBuilder(ingestProfile);
		}

		public static IngestProfileDeleteRequestBuilder Delete(int ingestProfileId)
		{
			return new IngestProfileDeleteRequestBuilder(ingestProfileId);
		}

		public static IngestProfileListRequestBuilder List()
		{
			return new IngestProfileListRequestBuilder();
		}

		public static IngestProfileUpdateRequestBuilder Update(int ingestProfileId, IngestProfile ingestProfile)
		{
			return new IngestProfileUpdateRequestBuilder(ingestProfileId, ingestProfile);
		}
	}
}
