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
	public class MetaAddRequestBuilder : RequestBuilder<Meta>
	{
		#region Constants
		public const string META = "meta";
		#endregion

		public Meta Meta { get; set; }

		public MetaAddRequestBuilder()
			: base("meta", "add")
		{
		}

		public MetaAddRequestBuilder(Meta meta)
			: this()
		{
			this.Meta = meta;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("meta"))
				kparams.AddIfNotNull("meta", Meta);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Meta>(result);
		}
	}

	public class MetaDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public MetaDeleteRequestBuilder()
			: base("meta", "delete")
		{
		}

		public MetaDeleteRequestBuilder(long id)
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

	public class MetaListRequestBuilder : RequestBuilder<ListResponse<Meta>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public MetaFilter Filter { get; set; }

		public MetaListRequestBuilder()
			: base("meta", "list")
		{
		}

		public MetaListRequestBuilder(MetaFilter filter)
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
			return ObjectFactory.Create<ListResponse<Meta>>(result);
		}
	}

	public class MetaUpdateRequestBuilder : RequestBuilder<Meta>
	{
		#region Constants
		public const string ID = "id";
		public const string META = "meta";
		#endregion

		public long Id { get; set; }
		public Meta Meta { get; set; }

		public MetaUpdateRequestBuilder()
			: base("meta", "update")
		{
		}

		public MetaUpdateRequestBuilder(long id, Meta meta)
			: this()
		{
			this.Id = id;
			this.Meta = meta;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("meta"))
				kparams.AddIfNotNull("meta", Meta);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Meta>(result);
		}
	}


	public class MetaService
	{
		private MetaService()
		{
		}

		public static MetaAddRequestBuilder Add(Meta meta)
		{
			return new MetaAddRequestBuilder(meta);
		}

		public static MetaDeleteRequestBuilder Delete(long id)
		{
			return new MetaDeleteRequestBuilder(id);
		}

		public static MetaListRequestBuilder List(MetaFilter filter = null)
		{
			return new MetaListRequestBuilder(filter);
		}

		public static MetaUpdateRequestBuilder Update(long id, Meta meta)
		{
			return new MetaUpdateRequestBuilder(id, meta);
		}
	}
}
