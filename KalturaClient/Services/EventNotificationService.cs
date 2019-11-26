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
	public class EventNotificationUpdateRequestBuilder : RequestBuilder<EventNotification>
	{
		#region Constants
		public const string ID = "id";
		public const string OBJECT_TO_UPDATE = "objectToUpdate";
		#endregion

		public string Id { get; set; }
		public EventNotification ObjectToUpdate { get; set; }

		public EventNotificationUpdateRequestBuilder()
			: base("eventnotification", "update")
		{
		}

		public EventNotificationUpdateRequestBuilder(string id, EventNotification objectToUpdate)
			: this()
		{
			this.Id = id;
			this.ObjectToUpdate = objectToUpdate;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("objectToUpdate"))
				kparams.AddIfNotNull("objectToUpdate", ObjectToUpdate);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<EventNotification>(result);
		}
	}

	public class EventNotificationListRequestBuilder : RequestBuilder<ListResponse<EventNotification>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public EventNotificationFilter Filter { get; set; }

		public EventNotificationListRequestBuilder()
			: base("eventnotification", "list")
		{
		}

		public EventNotificationListRequestBuilder(EventNotificationFilter filter)
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
			return ObjectFactory.Create<ListResponse<EventNotification>>(result);
		}
	}


	public class EventNotificationService
	{
		private EventNotificationService()
		{
		}

		public static EventNotificationUpdateRequestBuilder Update(string id, EventNotification objectToUpdate)
		{
			return new EventNotificationUpdateRequestBuilder(id, objectToUpdate);
		}

		public static EventNotificationListRequestBuilder List(EventNotificationFilter filter)
		{
			return new EventNotificationListRequestBuilder(filter);
		}
	}
}
