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
	public class CDVRAdapterProfileAddRequestBuilder : RequestBuilder<CDVRAdapterProfile>
	{
		#region Constants
		public const string ADAPTER = "adapter";
		#endregion

		public CDVRAdapterProfile Adapter { get; set; }

		public CDVRAdapterProfileAddRequestBuilder()
			: base("cdvradapterprofile", "add")
		{
		}

		public CDVRAdapterProfileAddRequestBuilder(CDVRAdapterProfile adapter)
			: this()
		{
			this.Adapter = adapter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("adapter"))
				kparams.AddIfNotNull("adapter", Adapter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<CDVRAdapterProfile>(result);
		}
	}

	public class CDVRAdapterProfileDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ADAPTER_ID = "adapterId";
		#endregion

		public int AdapterId { get; set; }

		public CDVRAdapterProfileDeleteRequestBuilder()
			: base("cdvradapterprofile", "delete")
		{
		}

		public CDVRAdapterProfileDeleteRequestBuilder(int adapterId)
			: this()
		{
			this.AdapterId = adapterId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("adapterId"))
				kparams.AddIfNotNull("adapterId", AdapterId);
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

	public class CDVRAdapterProfileGenerateSharedSecretRequestBuilder : RequestBuilder<CDVRAdapterProfile>
	{
		#region Constants
		public const string ADAPTER_ID = "adapterId";
		#endregion

		public int AdapterId { get; set; }

		public CDVRAdapterProfileGenerateSharedSecretRequestBuilder()
			: base("cdvradapterprofile", "generateSharedSecret")
		{
		}

		public CDVRAdapterProfileGenerateSharedSecretRequestBuilder(int adapterId)
			: this()
		{
			this.AdapterId = adapterId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("adapterId"))
				kparams.AddIfNotNull("adapterId", AdapterId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<CDVRAdapterProfile>(result);
		}
	}

	public class CDVRAdapterProfileListRequestBuilder : RequestBuilder<ListResponse<CDVRAdapterProfile>>
	{
		#region Constants
		#endregion


		public CDVRAdapterProfileListRequestBuilder()
			: base("cdvradapterprofile", "list")
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
			return ObjectFactory.Create<ListResponse<CDVRAdapterProfile>>(result);
		}
	}

	public class CDVRAdapterProfileUpdateRequestBuilder : RequestBuilder<CDVRAdapterProfile>
	{
		#region Constants
		public const string ADAPTER_ID = "adapterId";
		public const string ADAPTER = "adapter";
		#endregion

		public int AdapterId { get; set; }
		public CDVRAdapterProfile Adapter { get; set; }

		public CDVRAdapterProfileUpdateRequestBuilder()
			: base("cdvradapterprofile", "update")
		{
		}

		public CDVRAdapterProfileUpdateRequestBuilder(int adapterId, CDVRAdapterProfile adapter)
			: this()
		{
			this.AdapterId = adapterId;
			this.Adapter = adapter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("adapterId"))
				kparams.AddIfNotNull("adapterId", AdapterId);
			if (!isMapped("adapter"))
				kparams.AddIfNotNull("adapter", Adapter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<CDVRAdapterProfile>(result);
		}
	}


	public class CDVRAdapterProfileService
	{
		private CDVRAdapterProfileService()
		{
		}

		public static CDVRAdapterProfileAddRequestBuilder Add(CDVRAdapterProfile adapter)
		{
			return new CDVRAdapterProfileAddRequestBuilder(adapter);
		}

		public static CDVRAdapterProfileDeleteRequestBuilder Delete(int adapterId)
		{
			return new CDVRAdapterProfileDeleteRequestBuilder(adapterId);
		}

		public static CDVRAdapterProfileGenerateSharedSecretRequestBuilder GenerateSharedSecret(int adapterId)
		{
			return new CDVRAdapterProfileGenerateSharedSecretRequestBuilder(adapterId);
		}

		public static CDVRAdapterProfileListRequestBuilder List()
		{
			return new CDVRAdapterProfileListRequestBuilder();
		}

		public static CDVRAdapterProfileUpdateRequestBuilder Update(int adapterId, CDVRAdapterProfile adapter)
		{
			return new CDVRAdapterProfileUpdateRequestBuilder(adapterId, adapter);
		}
	}
}
