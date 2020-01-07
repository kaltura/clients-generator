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
	public class TopicDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public TopicDeleteRequestBuilder()
			: base("topic", "delete")
		{
		}

		public TopicDeleteRequestBuilder(int id)
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

	public class TopicGetRequestBuilder : RequestBuilder<Topic>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public TopicGetRequestBuilder()
			: base("topic", "get")
		{
		}

		public TopicGetRequestBuilder(int id)
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
			return ObjectFactory.Create<Topic>(result);
		}
	}

	public class TopicListRequestBuilder : RequestBuilder<ListResponse<Topic>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public TopicFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public TopicListRequestBuilder()
			: base("topic", "list")
		{
		}

		public TopicListRequestBuilder(TopicFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<Topic>>(result);
		}
	}

	public class TopicUpdateStatusRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		public const string AUTOMATIC_ISSUE_NOTIFICATION = "automaticIssueNotification";
		#endregion

		public int Id { get; set; }
		public TopicAutomaticIssueNotification AutomaticIssueNotification { get; set; }

		public TopicUpdateStatusRequestBuilder()
			: base("topic", "updateStatus")
		{
		}

		public TopicUpdateStatusRequestBuilder(int id, TopicAutomaticIssueNotification automaticIssueNotification)
			: this()
		{
			this.Id = id;
			this.AutomaticIssueNotification = automaticIssueNotification;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("automaticIssueNotification"))
				kparams.AddIfNotNull("automaticIssueNotification", AutomaticIssueNotification);
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


	public class TopicService
	{
		private TopicService()
		{
		}

		public static TopicDeleteRequestBuilder Delete(int id)
		{
			return new TopicDeleteRequestBuilder(id);
		}

		public static TopicGetRequestBuilder Get(int id)
		{
			return new TopicGetRequestBuilder(id);
		}

		public static TopicListRequestBuilder List(TopicFilter filter = null, FilterPager pager = null)
		{
			return new TopicListRequestBuilder(filter, pager);
		}

		public static TopicUpdateStatusRequestBuilder UpdateStatus(int id, TopicAutomaticIssueNotification automaticIssueNotification)
		{
			return new TopicUpdateStatusRequestBuilder(id, automaticIssueNotification);
		}
	}
}
