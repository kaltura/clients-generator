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
// Copyright (C) 2006-2018  Kaltura Inc.
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
	public class CdnAdapterProfileAddRequestBuilder : RequestBuilder<CDNAdapterProfile>
	{
		#region Constants
		public const string ADAPTER = "adapter";
		#endregion

		public CDNAdapterProfile Adapter
		{
			set;
			get;
		}

		public CdnAdapterProfileAddRequestBuilder()
			: base("cdnadapterprofile", "add")
		{
		}

		public CdnAdapterProfileAddRequestBuilder(CDNAdapterProfile adapter)
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
			return ObjectFactory.Create<CDNAdapterProfile>(result);
		}
	}

	public class CdnAdapterProfileDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ADAPTER_ID = "adapterId";
		#endregion

		public int AdapterId
		{
			set;
			get;
		}

		public CdnAdapterProfileDeleteRequestBuilder()
			: base("cdnadapterprofile", "delete")
		{
		}

		public CdnAdapterProfileDeleteRequestBuilder(int adapterId)
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

	public class CdnAdapterProfileGenerateSharedSecretRequestBuilder : RequestBuilder<CDNAdapterProfile>
	{
		#region Constants
		public const string ADAPTER_ID = "adapterId";
		#endregion

		public int AdapterId
		{
			set;
			get;
		}

		public CdnAdapterProfileGenerateSharedSecretRequestBuilder()
			: base("cdnadapterprofile", "generateSharedSecret")
		{
		}

		public CdnAdapterProfileGenerateSharedSecretRequestBuilder(int adapterId)
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
			return ObjectFactory.Create<CDNAdapterProfile>(result);
		}
	}

	public class CdnAdapterProfileListRequestBuilder : RequestBuilder<ListResponse<CDNAdapterProfile>>
	{
		#region Constants
		#endregion


		public CdnAdapterProfileListRequestBuilder()
			: base("cdnadapterprofile", "list")
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
			return ObjectFactory.Create<ListResponse<CDNAdapterProfile>>(result);
		}
	}

	public class CdnAdapterProfileUpdateRequestBuilder : RequestBuilder<CDNAdapterProfile>
	{
		#region Constants
		public const string ADAPTER_ID = "adapterId";
		public const string ADAPTER = "adapter";
		#endregion

		public int AdapterId
		{
			set;
			get;
		}
		public CDNAdapterProfile Adapter
		{
			set;
			get;
		}

		public CdnAdapterProfileUpdateRequestBuilder()
			: base("cdnadapterprofile", "update")
		{
		}

		public CdnAdapterProfileUpdateRequestBuilder(int adapterId, CDNAdapterProfile adapter)
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
			return ObjectFactory.Create<CDNAdapterProfile>(result);
		}
	}


	public class CdnAdapterProfileService
	{
		private CdnAdapterProfileService()
		{
		}

		public static CdnAdapterProfileAddRequestBuilder Add(CDNAdapterProfile adapter)
		{
			return new CdnAdapterProfileAddRequestBuilder(adapter);
		}

		public static CdnAdapterProfileDeleteRequestBuilder Delete(int adapterId)
		{
			return new CdnAdapterProfileDeleteRequestBuilder(adapterId);
		}

		public static CdnAdapterProfileGenerateSharedSecretRequestBuilder GenerateSharedSecret(int adapterId)
		{
			return new CdnAdapterProfileGenerateSharedSecretRequestBuilder(adapterId);
		}

		public static CdnAdapterProfileListRequestBuilder List()
		{
			return new CdnAdapterProfileListRequestBuilder();
		}

		public static CdnAdapterProfileUpdateRequestBuilder Update(int adapterId, CDNAdapterProfile adapter)
		{
			return new CdnAdapterProfileUpdateRequestBuilder(adapterId, adapter);
		}
	}
}
