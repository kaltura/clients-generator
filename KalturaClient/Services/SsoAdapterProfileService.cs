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
	public class SsoAdapterProfileAddRequestBuilder : RequestBuilder<SSOAdapterProfile>
	{
		#region Constants
		public const string SSO_ADAPTER = "ssoAdapter";
		#endregion

		public SSOAdapterProfile SsoAdapter
		{
			set;
			get;
		}

		public SsoAdapterProfileAddRequestBuilder()
			: base("ssoadapterprofile", "add")
		{
		}

		public SsoAdapterProfileAddRequestBuilder(SSOAdapterProfile ssoAdapter)
			: this()
		{
			this.SsoAdapter = ssoAdapter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ssoAdapter"))
				kparams.AddIfNotNull("ssoAdapter", SsoAdapter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<SSOAdapterProfile>(result);
		}
	}

	public class SsoAdapterProfileDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string SSO_ADAPTER_ID = "ssoAdapterId";
		#endregion

		public int SsoAdapterId
		{
			set;
			get;
		}

		public SsoAdapterProfileDeleteRequestBuilder()
			: base("ssoadapterprofile", "delete")
		{
		}

		public SsoAdapterProfileDeleteRequestBuilder(int ssoAdapterId)
			: this()
		{
			this.SsoAdapterId = ssoAdapterId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ssoAdapterId"))
				kparams.AddIfNotNull("ssoAdapterId", SsoAdapterId);
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

	public class SsoAdapterProfileGenerateSharedSecretRequestBuilder : RequestBuilder<SSOAdapterProfile>
	{
		#region Constants
		public const string SSO_ADAPTER_ID = "ssoAdapterId";
		#endregion

		public int SsoAdapterId
		{
			set;
			get;
		}

		public SsoAdapterProfileGenerateSharedSecretRequestBuilder()
			: base("ssoadapterprofile", "generateSharedSecret")
		{
		}

		public SsoAdapterProfileGenerateSharedSecretRequestBuilder(int ssoAdapterId)
			: this()
		{
			this.SsoAdapterId = ssoAdapterId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ssoAdapterId"))
				kparams.AddIfNotNull("ssoAdapterId", SsoAdapterId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<SSOAdapterProfile>(result);
		}
	}

	public class SsoAdapterProfileListRequestBuilder : RequestBuilder<ListResponse<SSOAdapterProfile>>
	{
		#region Constants
		#endregion


		public SsoAdapterProfileListRequestBuilder()
			: base("ssoadapterprofile", "list")
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
			return ObjectFactory.Create<ListResponse<SSOAdapterProfile>>(result);
		}
	}

	public class SsoAdapterProfileUpdateRequestBuilder : RequestBuilder<SSOAdapterProfile>
	{
		#region Constants
		public const string SSO_ADAPTER_ID = "ssoAdapterId";
		public const string SSO_ADAPTER = "ssoAdapter";
		#endregion

		public int SsoAdapterId
		{
			set;
			get;
		}
		public SSOAdapterProfile SsoAdapter
		{
			set;
			get;
		}

		public SsoAdapterProfileUpdateRequestBuilder()
			: base("ssoadapterprofile", "update")
		{
		}

		public SsoAdapterProfileUpdateRequestBuilder(int ssoAdapterId, SSOAdapterProfile ssoAdapter)
			: this()
		{
			this.SsoAdapterId = ssoAdapterId;
			this.SsoAdapter = ssoAdapter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ssoAdapterId"))
				kparams.AddIfNotNull("ssoAdapterId", SsoAdapterId);
			if (!isMapped("ssoAdapter"))
				kparams.AddIfNotNull("ssoAdapter", SsoAdapter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<SSOAdapterProfile>(result);
		}
	}


	public class SsoAdapterProfileService
	{
		private SsoAdapterProfileService()
		{
		}

		public static SsoAdapterProfileAddRequestBuilder Add(SSOAdapterProfile ssoAdapter)
		{
			return new SsoAdapterProfileAddRequestBuilder(ssoAdapter);
		}

		public static SsoAdapterProfileDeleteRequestBuilder Delete(int ssoAdapterId)
		{
			return new SsoAdapterProfileDeleteRequestBuilder(ssoAdapterId);
		}

		public static SsoAdapterProfileGenerateSharedSecretRequestBuilder GenerateSharedSecret(int ssoAdapterId)
		{
			return new SsoAdapterProfileGenerateSharedSecretRequestBuilder(ssoAdapterId);
		}

		public static SsoAdapterProfileListRequestBuilder List()
		{
			return new SsoAdapterProfileListRequestBuilder();
		}

		public static SsoAdapterProfileUpdateRequestBuilder Update(int ssoAdapterId, SSOAdapterProfile ssoAdapter)
		{
			return new SsoAdapterProfileUpdateRequestBuilder(ssoAdapterId, ssoAdapter);
		}
	}
}
