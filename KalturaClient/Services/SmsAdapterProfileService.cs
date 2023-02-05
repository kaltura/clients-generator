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
	public class SmsAdapterProfileAddRequestBuilder : RequestBuilder<SmsAdapterProfile>
	{
		#region Constants
		public const string OBJECT_TO_ADD = "objectToAdd";
		#endregion

		public SmsAdapterProfile ObjectToAdd { get; set; }

		public SmsAdapterProfileAddRequestBuilder()
			: base("smsadapterprofile", "add")
		{
		}

		public SmsAdapterProfileAddRequestBuilder(SmsAdapterProfile objectToAdd)
			: this()
		{
			this.ObjectToAdd = objectToAdd;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("objectToAdd"))
				kparams.AddIfNotNull("objectToAdd", ObjectToAdd);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<SmsAdapterProfile>(result);
		}
	}

	public class SmsAdapterProfileUpdateRequestBuilder : RequestBuilder<SmsAdapterProfile>
	{
		#region Constants
		public const string ID = "id";
		public const string OBJECT_TO_UPDATE = "objectToUpdate";
		#endregion

		public long Id { get; set; }
		public SmsAdapterProfile ObjectToUpdate { get; set; }

		public SmsAdapterProfileUpdateRequestBuilder()
			: base("smsadapterprofile", "update")
		{
		}

		public SmsAdapterProfileUpdateRequestBuilder(long id, SmsAdapterProfile objectToUpdate)
			: this()
		{
			this.Id = id;
			this.ObjectToUpdate = objectToUpdate;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("objectToUpdate"))
				kparams.AddIfNotNull("objectToUpdate", ObjectToUpdate);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<SmsAdapterProfile>(result);
		}
	}

	public class SmsAdapterProfileGetRequestBuilder : RequestBuilder<SmsAdapterProfile>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public SmsAdapterProfileGetRequestBuilder()
			: base("smsadapterprofile", "get")
		{
		}

		public SmsAdapterProfileGetRequestBuilder(long id)
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
			return ObjectFactory.Create<SmsAdapterProfile>(result);
		}
	}

	public class SmsAdapterProfileListRequestBuilder : RequestBuilder<ListResponse<SmsAdapterProfile>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public SmsAdapterProfileFilter Filter { get; set; }

		public SmsAdapterProfileListRequestBuilder()
			: base("smsadapterprofile", "list")
		{
		}

		public SmsAdapterProfileListRequestBuilder(SmsAdapterProfileFilter filter)
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
			return ObjectFactory.Create<ListResponse<SmsAdapterProfile>>(result);
		}
	}

	public class SmsAdapterProfileDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public SmsAdapterProfileDeleteRequestBuilder()
			: base("smsadapterprofile", "delete")
		{
		}

		public SmsAdapterProfileDeleteRequestBuilder(long id)
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
			return null;
		}
	}

	public class SmsAdapterProfileGenerateSharedSecretRequestBuilder : RequestBuilder<SmsAdapterProfile>
	{
		#region Constants
		public const string SMS_ADAPTER_ID = "smsAdapterId";
		#endregion

		public int SmsAdapterId { get; set; }

		public SmsAdapterProfileGenerateSharedSecretRequestBuilder()
			: base("smsadapterprofile", "generateSharedSecret")
		{
		}

		public SmsAdapterProfileGenerateSharedSecretRequestBuilder(int smsAdapterId)
			: this()
		{
			this.SmsAdapterId = smsAdapterId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("smsAdapterId"))
				kparams.AddIfNotNull("smsAdapterId", SmsAdapterId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<SmsAdapterProfile>(result);
		}
	}


	public class SmsAdapterProfileService
	{
		private SmsAdapterProfileService()
		{
		}

		public static SmsAdapterProfileAddRequestBuilder Add(SmsAdapterProfile objectToAdd)
		{
			return new SmsAdapterProfileAddRequestBuilder(objectToAdd);
		}

		public static SmsAdapterProfileUpdateRequestBuilder Update(long id, SmsAdapterProfile objectToUpdate)
		{
			return new SmsAdapterProfileUpdateRequestBuilder(id, objectToUpdate);
		}

		public static SmsAdapterProfileGetRequestBuilder Get(long id)
		{
			return new SmsAdapterProfileGetRequestBuilder(id);
		}

		public static SmsAdapterProfileListRequestBuilder List(SmsAdapterProfileFilter filter)
		{
			return new SmsAdapterProfileListRequestBuilder(filter);
		}

		public static SmsAdapterProfileDeleteRequestBuilder Delete(long id)
		{
			return new SmsAdapterProfileDeleteRequestBuilder(id);
		}

		public static SmsAdapterProfileGenerateSharedSecretRequestBuilder GenerateSharedSecret(int smsAdapterId)
		{
			return new SmsAdapterProfileGenerateSharedSecretRequestBuilder(smsAdapterId);
		}
	}
}
