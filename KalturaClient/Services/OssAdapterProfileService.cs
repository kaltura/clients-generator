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
// Copyright (C) 2006-2020  Kaltura Inc.
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
	public class OssAdapterProfileAddRequestBuilder : RequestBuilder<OSSAdapterProfile>
	{
		#region Constants
		public const string OSS_ADAPTER = "ossAdapter";
		#endregion

		public OSSAdapterProfile OssAdapter { get; set; }

		public OssAdapterProfileAddRequestBuilder()
			: base("ossadapterprofile", "add")
		{
		}

		public OssAdapterProfileAddRequestBuilder(OSSAdapterProfile ossAdapter)
			: this()
		{
			this.OssAdapter = ossAdapter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ossAdapter"))
				kparams.AddIfNotNull("ossAdapter", OssAdapter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<OSSAdapterProfile>(result);
		}
	}

	public class OssAdapterProfileDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string OSS_ADAPTER_ID = "ossAdapterId";
		#endregion

		public int OssAdapterId { get; set; }

		public OssAdapterProfileDeleteRequestBuilder()
			: base("ossadapterprofile", "delete")
		{
		}

		public OssAdapterProfileDeleteRequestBuilder(int ossAdapterId)
			: this()
		{
			this.OssAdapterId = ossAdapterId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ossAdapterId"))
				kparams.AddIfNotNull("ossAdapterId", OssAdapterId);
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

	public class OssAdapterProfileGenerateSharedSecretRequestBuilder : RequestBuilder<OSSAdapterProfile>
	{
		#region Constants
		public const string OSS_ADAPTER_ID = "ossAdapterId";
		#endregion

		public int OssAdapterId { get; set; }

		public OssAdapterProfileGenerateSharedSecretRequestBuilder()
			: base("ossadapterprofile", "generateSharedSecret")
		{
		}

		public OssAdapterProfileGenerateSharedSecretRequestBuilder(int ossAdapterId)
			: this()
		{
			this.OssAdapterId = ossAdapterId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ossAdapterId"))
				kparams.AddIfNotNull("ossAdapterId", OssAdapterId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<OSSAdapterProfile>(result);
		}
	}

	public class OssAdapterProfileGetRequestBuilder : RequestBuilder<OSSAdapterProfile>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public OssAdapterProfileGetRequestBuilder()
			: base("ossadapterprofile", "get")
		{
		}

		public OssAdapterProfileGetRequestBuilder(int id)
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
			return ObjectFactory.Create<OSSAdapterProfile>(result);
		}
	}

	public class OssAdapterProfileListRequestBuilder : RequestBuilder<ListResponse<OSSAdapterProfile>>
	{
		#region Constants
		#endregion


		public OssAdapterProfileListRequestBuilder()
			: base("ossadapterprofile", "list")
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
			return ObjectFactory.Create<ListResponse<OSSAdapterProfile>>(result);
		}
	}

	public class OssAdapterProfileUpdateRequestBuilder : RequestBuilder<OSSAdapterProfile>
	{
		#region Constants
		public const string OSS_ADAPTER_ID = "ossAdapterId";
		public const string OSS_ADAPTER = "ossAdapter";
		#endregion

		public int OssAdapterId { get; set; }
		public OSSAdapterProfile OssAdapter { get; set; }

		public OssAdapterProfileUpdateRequestBuilder()
			: base("ossadapterprofile", "update")
		{
		}

		public OssAdapterProfileUpdateRequestBuilder(int ossAdapterId, OSSAdapterProfile ossAdapter)
			: this()
		{
			this.OssAdapterId = ossAdapterId;
			this.OssAdapter = ossAdapter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ossAdapterId"))
				kparams.AddIfNotNull("ossAdapterId", OssAdapterId);
			if (!isMapped("ossAdapter"))
				kparams.AddIfNotNull("ossAdapter", OssAdapter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<OSSAdapterProfile>(result);
		}
	}


	public class OssAdapterProfileService
	{
		private OssAdapterProfileService()
		{
		}

		public static OssAdapterProfileAddRequestBuilder Add(OSSAdapterProfile ossAdapter)
		{
			return new OssAdapterProfileAddRequestBuilder(ossAdapter);
		}

		public static OssAdapterProfileDeleteRequestBuilder Delete(int ossAdapterId)
		{
			return new OssAdapterProfileDeleteRequestBuilder(ossAdapterId);
		}

		public static OssAdapterProfileGenerateSharedSecretRequestBuilder GenerateSharedSecret(int ossAdapterId)
		{
			return new OssAdapterProfileGenerateSharedSecretRequestBuilder(ossAdapterId);
		}

		public static OssAdapterProfileGetRequestBuilder Get(int id)
		{
			return new OssAdapterProfileGetRequestBuilder(id);
		}

		public static OssAdapterProfileListRequestBuilder List()
		{
			return new OssAdapterProfileListRequestBuilder();
		}

		public static OssAdapterProfileUpdateRequestBuilder Update(int ossAdapterId, OSSAdapterProfile ossAdapter)
		{
			return new OssAdapterProfileUpdateRequestBuilder(ossAdapterId, ossAdapter);
		}
	}
}
