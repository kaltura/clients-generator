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
	public class EngagementAddRequestBuilder : RequestBuilder<Engagement>
	{
		#region Constants
		public const string ENGAGEMENT = "engagement";
		#endregion

		public Engagement Engagement
		{
			set;
			get;
		}

		public EngagementAddRequestBuilder()
			: base("engagement", "add")
		{
		}

		public EngagementAddRequestBuilder(Engagement engagement)
			: this()
		{
			this.Engagement = engagement;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("engagement"))
				kparams.AddIfNotNull("engagement", Engagement);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Engagement>(result);
		}
	}

	public class EngagementDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id
		{
			set;
			get;
		}

		public EngagementDeleteRequestBuilder()
			: base("engagement", "delete")
		{
		}

		public EngagementDeleteRequestBuilder(int id)
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

	public class EngagementGetRequestBuilder : RequestBuilder<Engagement>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id
		{
			set;
			get;
		}

		public EngagementGetRequestBuilder()
			: base("engagement", "get")
		{
		}

		public EngagementGetRequestBuilder(int id)
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
			return ObjectFactory.Create<Engagement>(result);
		}
	}

	public class EngagementListRequestBuilder : RequestBuilder<ListResponse<Engagement>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public EngagementFilter Filter
		{
			set;
			get;
		}

		public EngagementListRequestBuilder()
			: base("engagement", "list")
		{
		}

		public EngagementListRequestBuilder(EngagementFilter filter)
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
			return ObjectFactory.Create<ListResponse<Engagement>>(result);
		}
	}


	public class EngagementService
	{
		private EngagementService()
		{
		}

		public static EngagementAddRequestBuilder Add(Engagement engagement)
		{
			return new EngagementAddRequestBuilder(engagement);
		}

		public static EngagementDeleteRequestBuilder Delete(int id)
		{
			return new EngagementDeleteRequestBuilder(id);
		}

		public static EngagementGetRequestBuilder Get(int id)
		{
			return new EngagementGetRequestBuilder(id);
		}

		public static EngagementListRequestBuilder List(EngagementFilter filter)
		{
			return new EngagementListRequestBuilder(filter);
		}
	}
}
