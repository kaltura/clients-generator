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
// BEO-9522 csharp2 before comment
	public class TopicNotificationMessageAddRequestBuilder : RequestBuilder<TopicNotificationMessage>
	{
		#region Constants
		public const string TOPIC_NOTIFICATION_MESSAGE = "topicNotificationMessage";
		#endregion

		public TopicNotificationMessage TopicNotificationMessage { get; set; }

		public TopicNotificationMessageAddRequestBuilder()
			: base("topicnotificationmessage", "add")
		{
		}

		public TopicNotificationMessageAddRequestBuilder(TopicNotificationMessage topicNotificationMessage)
			: this()
		{
			this.TopicNotificationMessage = topicNotificationMessage;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("topicNotificationMessage"))
				kparams.AddIfNotNull("topicNotificationMessage", TopicNotificationMessage);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<TopicNotificationMessage>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class TopicNotificationMessageDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public TopicNotificationMessageDeleteRequestBuilder()
			: base("topicnotificationmessage", "delete")
		{
		}

		public TopicNotificationMessageDeleteRequestBuilder(long id)
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

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class TopicNotificationMessageListRequestBuilder : RequestBuilder<ListResponse<TopicNotificationMessage>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public TopicNotificationMessageFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public TopicNotificationMessageListRequestBuilder()
			: base("topicnotificationmessage", "list")
		{
		}

		public TopicNotificationMessageListRequestBuilder(TopicNotificationMessageFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<TopicNotificationMessage>>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class TopicNotificationMessageUpdateRequestBuilder : RequestBuilder<TopicNotificationMessage>
	{
		#region Constants
		public const string ID = "id";
		public const string TOPIC_NOTIFICATION_MESSAGE = "topicNotificationMessage";
		#endregion

		public int Id { get; set; }
		public TopicNotificationMessage TopicNotificationMessage { get; set; }

		public TopicNotificationMessageUpdateRequestBuilder()
			: base("topicnotificationmessage", "update")
		{
		}

		public TopicNotificationMessageUpdateRequestBuilder(int id, TopicNotificationMessage topicNotificationMessage)
			: this()
		{
			this.Id = id;
			this.TopicNotificationMessage = topicNotificationMessage;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("topicNotificationMessage"))
				kparams.AddIfNotNull("topicNotificationMessage", TopicNotificationMessage);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<TopicNotificationMessage>(result);
		}
	}


	public class TopicNotificationMessageService
	{
		private TopicNotificationMessageService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static TopicNotificationMessageAddRequestBuilder Add(TopicNotificationMessage topicNotificationMessage)
		{
			return new TopicNotificationMessageAddRequestBuilder(topicNotificationMessage);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static TopicNotificationMessageDeleteRequestBuilder Delete(long id)
		{
			return new TopicNotificationMessageDeleteRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static TopicNotificationMessageListRequestBuilder List(TopicNotificationMessageFilter filter = null, FilterPager pager = null)
		{
			return new TopicNotificationMessageListRequestBuilder(filter, pager);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static TopicNotificationMessageUpdateRequestBuilder Update(int id, TopicNotificationMessage topicNotificationMessage)
		{
			return new TopicNotificationMessageUpdateRequestBuilder(id, topicNotificationMessage);
		}
	}
}
