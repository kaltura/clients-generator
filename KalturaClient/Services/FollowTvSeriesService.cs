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

namespace Kaltura.Services
{
	public class FollowTvSeriesAddRequestBuilder : RequestBuilder<FollowTvSeries>
	{
		#region Constants
		public const string FOLLOW_TV_SERIES = "followTvSeries";
		#endregion

		public FollowTvSeries FollowTvSeries
		{
			set;
			get;
		}

		public FollowTvSeriesAddRequestBuilder()
			: base("followtvseries", "add")
		{
		}

		public FollowTvSeriesAddRequestBuilder(FollowTvSeries followTvSeries)
			: this()
		{
			this.FollowTvSeries = followTvSeries;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("followTvSeries"))
				kparams.AddIfNotNull("followTvSeries", FollowTvSeries);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<FollowTvSeries>(result);
		}
	}

	public class FollowTvSeriesDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		#endregion

		public int AssetId
		{
			set;
			get;
		}

		public FollowTvSeriesDeleteRequestBuilder()
			: base("followtvseries", "delete")
		{
		}

		public FollowTvSeriesDeleteRequestBuilder(int assetId)
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

		public override object Deserialize(XmlElement result)
		{
			if (result.InnerText.Equals("1") || result.InnerText.ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class FollowTvSeriesDeleteWithTokenRequestBuilder : RequestBuilder<object>
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string TOKEN = "token";
		public new const string PARTNER_ID = "partnerId";
		#endregion

		public int AssetId
		{
			set;
			get;
		}
		public string Token
		{
			set;
			get;
		}
		public new int PartnerId
		{
			set;
			get;
		}

		public FollowTvSeriesDeleteWithTokenRequestBuilder()
			: base("followtvseries", "deleteWithToken")
		{
		}

		public FollowTvSeriesDeleteWithTokenRequestBuilder(int assetId, string token, int partnerId)
			: this()
		{
			this.AssetId = assetId;
			this.Token = token;
			this.PartnerId = partnerId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetId"))
				kparams.AddIfNotNull("assetId", AssetId);
			if (!isMapped("token"))
				kparams.AddIfNotNull("token", Token);
			if (!isMapped("partnerId"))
				kparams.AddIfNotNull("partnerId", PartnerId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return null;
		}
	}

	public class FollowTvSeriesListRequestBuilder : RequestBuilder<ListResponse<FollowTvSeries>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public FollowTvSeriesFilter Filter
		{
			set;
			get;
		}
		public FilterPager Pager
		{
			set;
			get;
		}

		public FollowTvSeriesListRequestBuilder()
			: base("followtvseries", "list")
		{
		}

		public FollowTvSeriesListRequestBuilder(FollowTvSeriesFilter filter, FilterPager pager)
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

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ListResponse<FollowTvSeries>>(result);
		}
	}


	public class FollowTvSeriesService
	{
		private FollowTvSeriesService()
		{
		}

		public static FollowTvSeriesAddRequestBuilder Add(FollowTvSeries followTvSeries)
		{
			return new FollowTvSeriesAddRequestBuilder(followTvSeries);
		}

		public static FollowTvSeriesDeleteRequestBuilder Delete(int assetId)
		{
			return new FollowTvSeriesDeleteRequestBuilder(assetId);
		}

		public static FollowTvSeriesDeleteWithTokenRequestBuilder DeleteWithToken(int assetId, string token, int partnerId)
		{
			return new FollowTvSeriesDeleteWithTokenRequestBuilder(assetId, token, partnerId);
		}

		public static FollowTvSeriesListRequestBuilder List(FollowTvSeriesFilter filter, FilterPager pager = null)
		{
			return new FollowTvSeriesListRequestBuilder(filter, pager);
		}
	}
}
