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
	public class AssetStatisticsQueryRequestBuilder : RequestBuilder<ListResponse<AssetStatistics>>
	{
		#region Constants
		public const string QUERY = "query";
		#endregion

		public AssetStatisticsQuery Query
		{
			set;
			get;
		}

		public AssetStatisticsQueryRequestBuilder()
			: base("assetstatistics", "query")
		{
		}

		public AssetStatisticsQueryRequestBuilder(AssetStatisticsQuery query)
			: this()
		{
			this.Query = query;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("query"))
				kparams.AddIfNotNull("query", Query);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ListResponse<AssetStatistics>>(result);
		}
	}


	public class AssetStatisticsService
	{
		private AssetStatisticsService()
		{
		}

		public static AssetStatisticsQueryRequestBuilder Query(AssetStatisticsQuery query)
		{
			return new AssetStatisticsQueryRequestBuilder(query);
		}
	}
}
