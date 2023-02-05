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
	public class RegionAddRequestBuilder : RequestBuilder<Region>
	{
		#region Constants
		public const string REGION = "region";
		#endregion

		public Region Region { get; set; }

		public RegionAddRequestBuilder()
			: base("region", "add")
		{
		}

		public RegionAddRequestBuilder(Region region)
			: this()
		{
			this.Region = region;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("region"))
				kparams.AddIfNotNull("region", Region);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Region>(result);
		}
	}

	public class RegionDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public RegionDeleteRequestBuilder()
			: base("region", "delete")
		{
		}

		public RegionDeleteRequestBuilder(int id)
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

	public class RegionListRequestBuilder : RequestBuilder<ListResponse<Region>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public BaseRegionFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public RegionListRequestBuilder()
			: base("region", "list")
		{
		}

		public RegionListRequestBuilder(BaseRegionFilter filter, FilterPager pager)
			: this()
		{
			this.Filter = filter;
			this.Pager = pager;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("filter"))
				kparams.AddIfNotNull("filter", Filter);
			if (!isMapped("pager"))
				kparams.AddIfNotNull("pager", Pager);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ListResponse<Region>>(result);
		}
	}

	public class RegionUpdateRequestBuilder : RequestBuilder<Region>
	{
		#region Constants
		public const string ID = "id";
		public const string REGION = "region";
		#endregion

		public int Id { get; set; }
		public Region Region { get; set; }

		public RegionUpdateRequestBuilder()
			: base("region", "update")
		{
		}

		public RegionUpdateRequestBuilder(int id, Region region)
			: this()
		{
			this.Id = id;
			this.Region = region;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("region"))
				kparams.AddIfNotNull("region", Region);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Region>(result);
		}
	}


	public class RegionService
	{
		private RegionService()
		{
		}

		public static RegionAddRequestBuilder Add(Region region)
		{
			return new RegionAddRequestBuilder(region);
		}

		public static RegionDeleteRequestBuilder Delete(int id)
		{
			return new RegionDeleteRequestBuilder(id);
		}

		public static RegionListRequestBuilder List(BaseRegionFilter filter, FilterPager pager = null)
		{
			return new RegionListRequestBuilder(filter, pager);
		}

		public static RegionUpdateRequestBuilder Update(int id, Region region)
		{
			return new RegionUpdateRequestBuilder(id, region);
		}
	}
}
