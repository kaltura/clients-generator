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
	public class AssetHistoryCleanRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public AssetHistoryFilter Filter { get; set; }

		public AssetHistoryCleanRequestBuilder()
			: base("assethistory", "clean")
		{
		}

		public AssetHistoryCleanRequestBuilder(AssetHistoryFilter filter)
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
			return null;
		}
	}

	public class AssetHistoryGetNextEpisodeRequestBuilder : RequestBuilder<AssetHistory>
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		#endregion

		public long AssetId { get; set; }

		public AssetHistoryGetNextEpisodeRequestBuilder()
			: base("assethistory", "getNextEpisode")
		{
		}

		public AssetHistoryGetNextEpisodeRequestBuilder(long assetId)
			: this()
		{
			this.AssetId = assetId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetId"))
				kparams.AddIfNotNull("assetId", AssetId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<AssetHistory>(result);
		}
	}

	public class AssetHistoryListRequestBuilder : RequestBuilder<ListResponse<AssetHistory>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public AssetHistoryFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public AssetHistoryListRequestBuilder()
			: base("assethistory", "list")
		{
		}

		public AssetHistoryListRequestBuilder(AssetHistoryFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<AssetHistory>>(result);
		}
	}


	public class AssetHistoryService
	{
		private AssetHistoryService()
		{
		}

		public static AssetHistoryCleanRequestBuilder Clean(AssetHistoryFilter filter = null)
		{
			return new AssetHistoryCleanRequestBuilder(filter);
		}

		public static AssetHistoryGetNextEpisodeRequestBuilder GetNextEpisode(long assetId)
		{
			return new AssetHistoryGetNextEpisodeRequestBuilder(assetId);
		}

		public static AssetHistoryListRequestBuilder List(AssetHistoryFilter filter = null, FilterPager pager = null)
		{
			return new AssetHistoryListRequestBuilder(filter, pager);
		}
	}
}
