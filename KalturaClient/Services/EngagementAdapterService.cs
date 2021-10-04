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
	public class EngagementAdapterAddRequestBuilder : RequestBuilder<EngagementAdapter>
	{
		#region Constants
		public const string ENGAGEMENT_ADAPTER = "engagementAdapter";
		#endregion

		public EngagementAdapter EngagementAdapter { get; set; }

		public EngagementAdapterAddRequestBuilder()
			: base("engagementadapter", "add")
		{
		}

		public EngagementAdapterAddRequestBuilder(EngagementAdapter engagementAdapter)
			: this()
		{
			this.EngagementAdapter = engagementAdapter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("engagementAdapter"))
				kparams.AddIfNotNull("engagementAdapter", EngagementAdapter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<EngagementAdapter>(result);
		}
	}

	public class EngagementAdapterDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public EngagementAdapterDeleteRequestBuilder()
			: base("engagementadapter", "delete")
		{
		}

		public EngagementAdapterDeleteRequestBuilder(int id)
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

	public class EngagementAdapterGenerateSharedSecretRequestBuilder : RequestBuilder<EngagementAdapter>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public EngagementAdapterGenerateSharedSecretRequestBuilder()
			: base("engagementadapter", "generateSharedSecret")
		{
		}

		public EngagementAdapterGenerateSharedSecretRequestBuilder(int id)
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
			return ObjectFactory.Create<EngagementAdapter>(result);
		}
	}

	public class EngagementAdapterGetRequestBuilder : RequestBuilder<EngagementAdapter>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public EngagementAdapterGetRequestBuilder()
			: base("engagementadapter", "get")
		{
		}

		public EngagementAdapterGetRequestBuilder(int id)
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
			return ObjectFactory.Create<EngagementAdapter>(result);
		}
	}

	public class EngagementAdapterListRequestBuilder : RequestBuilder<ListResponse<EngagementAdapter>>
	{
		#region Constants
		#endregion


		public EngagementAdapterListRequestBuilder()
			: base("engagementadapter", "list")
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
			return ObjectFactory.Create<ListResponse<EngagementAdapter>>(result);
		}
	}

	public class EngagementAdapterUpdateRequestBuilder : RequestBuilder<EngagementAdapter>
	{
		#region Constants
		public const string ID = "id";
		public const string ENGAGEMENT_ADAPTER = "engagementAdapter";
		#endregion

		public int Id { get; set; }
		public EngagementAdapter EngagementAdapter { get; set; }

		public EngagementAdapterUpdateRequestBuilder()
			: base("engagementadapter", "update")
		{
		}

		public EngagementAdapterUpdateRequestBuilder(int id, EngagementAdapter engagementAdapter)
			: this()
		{
			this.Id = id;
			this.EngagementAdapter = engagementAdapter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("engagementAdapter"))
				kparams.AddIfNotNull("engagementAdapter", EngagementAdapter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<EngagementAdapter>(result);
		}
	}


	public class EngagementAdapterService
	{
		private EngagementAdapterService()
		{
		}

		public static EngagementAdapterAddRequestBuilder Add(EngagementAdapter engagementAdapter)
		{
			return new EngagementAdapterAddRequestBuilder(engagementAdapter);
		}

		public static EngagementAdapterDeleteRequestBuilder Delete(int id)
		{
			return new EngagementAdapterDeleteRequestBuilder(id);
		}

		public static EngagementAdapterGenerateSharedSecretRequestBuilder GenerateSharedSecret(int id)
		{
			return new EngagementAdapterGenerateSharedSecretRequestBuilder(id);
		}

		public static EngagementAdapterGetRequestBuilder Get(int id)
		{
			return new EngagementAdapterGetRequestBuilder(id);
		}

		public static EngagementAdapterListRequestBuilder List()
		{
			return new EngagementAdapterListRequestBuilder();
		}

		public static EngagementAdapterUpdateRequestBuilder Update(int id, EngagementAdapter engagementAdapter)
		{
			return new EngagementAdapterUpdateRequestBuilder(id, engagementAdapter);
		}
	}
}
