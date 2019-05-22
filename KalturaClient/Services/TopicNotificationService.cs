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
// Copyright (C) 2006-2019  Kaltura Inc.
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
	public class TopicNotificationAddRequestBuilder : RequestBuilder<TopicNotification>
	{
		#region Constants
		public const string TOPIC_NOTIFICATION = "topicNotification";
		#endregion

		public TopicNotification TopicNotification { get; set; }

		public TopicNotificationAddRequestBuilder()
			: base("topicnotification", "add")
		{
		}

		public TopicNotificationAddRequestBuilder(TopicNotification topicNotification)
			: this()
		{
			this.TopicNotification = topicNotification;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("topicNotification"))
				kparams.AddIfNotNull("topicNotification", TopicNotification);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<TopicNotification>(result);
		}
	}

	public class TopicNotificationDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public TopicNotificationDeleteRequestBuilder()
			: base("topicnotification", "delete")
		{
		}

		public TopicNotificationDeleteRequestBuilder(long id)
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

	public class TopicNotificationListRequestBuilder : RequestBuilder<ListResponse<TopicNotification>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public TopicNotificationFilter Filter { get; set; }

		public TopicNotificationListRequestBuilder()
			: base("topicnotification", "list")
		{
		}

		public TopicNotificationListRequestBuilder(TopicNotificationFilter filter)
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
			return ObjectFactory.Create<ListResponse<TopicNotification>>(result);
		}
	}

	public class TopicNotificationSubscribeRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string TOPIC_NOTIFICATION_ID = "topicNotificationId";
		#endregion

		public long TopicNotificationId { get; set; }

		public TopicNotificationSubscribeRequestBuilder()
			: base("topicnotification", "subscribe")
		{
		}

		public TopicNotificationSubscribeRequestBuilder(long topicNotificationId)
			: this()
		{
			this.TopicNotificationId = topicNotificationId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("topicNotificationId"))
				kparams.AddIfNotNull("topicNotificationId", TopicNotificationId);
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

	public class TopicNotificationUnsubscribeRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string TOPIC_NOTIFICATION_ID = "topicNotificationId";
		#endregion

		public long TopicNotificationId { get; set; }

		public TopicNotificationUnsubscribeRequestBuilder()
			: base("topicnotification", "unsubscribe")
		{
		}

		public TopicNotificationUnsubscribeRequestBuilder(long topicNotificationId)
			: this()
		{
			this.TopicNotificationId = topicNotificationId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("topicNotificationId"))
				kparams.AddIfNotNull("topicNotificationId", TopicNotificationId);
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

	public class TopicNotificationUpdateRequestBuilder : RequestBuilder<TopicNotification>
	{
		#region Constants
		public const string ID = "id";
		public const string TOPIC_NOTIFICATION = "topicNotification";
		#endregion

		public int Id { get; set; }
		public TopicNotification TopicNotification { get; set; }

		public TopicNotificationUpdateRequestBuilder()
			: base("topicnotification", "update")
		{
		}

		public TopicNotificationUpdateRequestBuilder(int id, TopicNotification topicNotification)
			: this()
		{
			this.Id = id;
			this.TopicNotification = topicNotification;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("topicNotification"))
				kparams.AddIfNotNull("topicNotification", TopicNotification);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<TopicNotification>(result);
		}
	}


	public class TopicNotificationService
	{
		private TopicNotificationService()
		{
		}

		public static TopicNotificationAddRequestBuilder Add(TopicNotification topicNotification)
		{
			return new TopicNotificationAddRequestBuilder(topicNotification);
		}

		public static TopicNotificationDeleteRequestBuilder Delete(long id)
		{
			return new TopicNotificationDeleteRequestBuilder(id);
		}

		public static TopicNotificationListRequestBuilder List(TopicNotificationFilter filter = null)
		{
			return new TopicNotificationListRequestBuilder(filter);
		}

		public static TopicNotificationSubscribeRequestBuilder Subscribe(long topicNotificationId)
		{
			return new TopicNotificationSubscribeRequestBuilder(topicNotificationId);
		}

		public static TopicNotificationUnsubscribeRequestBuilder Unsubscribe(long topicNotificationId)
		{
			return new TopicNotificationUnsubscribeRequestBuilder(topicNotificationId);
		}

		public static TopicNotificationUpdateRequestBuilder Update(int id, TopicNotification topicNotification)
		{
			return new TopicNotificationUpdateRequestBuilder(id, topicNotification);
		}
	}
}
