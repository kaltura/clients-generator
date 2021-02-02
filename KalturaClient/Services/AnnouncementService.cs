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
	public class AnnouncementAddRequestBuilder : RequestBuilder<Announcement>
	{
		#region Constants
		public const string ANNOUNCEMENT = "announcement";
		#endregion

		public Announcement Announcement { get; set; }

		public AnnouncementAddRequestBuilder()
			: base("announcement", "add")
		{
		}

		public AnnouncementAddRequestBuilder(Announcement announcement)
			: this()
		{
			this.Announcement = announcement;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("announcement"))
				kparams.AddIfNotNull("announcement", Announcement);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Announcement>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class AnnouncementDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public AnnouncementDeleteRequestBuilder()
			: base("announcement", "delete")
		{
		}

		public AnnouncementDeleteRequestBuilder(long id)
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

	public class AnnouncementEnableSystemAnnouncementsRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		#endregion


		public AnnouncementEnableSystemAnnouncementsRequestBuilder()
			: base("announcement", "enableSystemAnnouncements")
		{
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
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

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class AnnouncementListRequestBuilder : RequestBuilder<ListResponse<Announcement>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public AnnouncementFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public AnnouncementListRequestBuilder()
			: base("announcement", "list")
		{
		}

		public AnnouncementListRequestBuilder(AnnouncementFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<Announcement>>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class AnnouncementUpdateRequestBuilder : RequestBuilder<Announcement>
	{
		#region Constants
		public const string ANNOUNCEMENT_ID = "announcementId";
		public const string ANNOUNCEMENT = "announcement";
		#endregion

		public int AnnouncementId { get; set; }
		public Announcement Announcement { get; set; }

		public AnnouncementUpdateRequestBuilder()
			: base("announcement", "update")
		{
		}

		public AnnouncementUpdateRequestBuilder(int announcementId, Announcement announcement)
			: this()
		{
			this.AnnouncementId = announcementId;
			this.Announcement = announcement;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("announcementId"))
				kparams.AddIfNotNull("announcementId", AnnouncementId);
			if (!isMapped("announcement"))
				kparams.AddIfNotNull("announcement", Announcement);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Announcement>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class AnnouncementUpdateStatusRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		public const string STATUS = "status";
		#endregion

		public long Id { get; set; }
		public bool Status { get; set; }

		public AnnouncementUpdateStatusRequestBuilder()
			: base("announcement", "updateStatus")
		{
		}

		public AnnouncementUpdateStatusRequestBuilder(long id, bool status)
			: this()
		{
			this.Id = id;
			this.Status = status;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("status"))
				kparams.AddIfNotNull("status", Status);
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


	public class AnnouncementService
	{
		private AnnouncementService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static AnnouncementAddRequestBuilder Add(Announcement announcement)
		{
			return new AnnouncementAddRequestBuilder(announcement);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static AnnouncementDeleteRequestBuilder Delete(long id)
		{
			return new AnnouncementDeleteRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction

		public static AnnouncementEnableSystemAnnouncementsRequestBuilder EnableSystemAnnouncements()
		{
			return new AnnouncementEnableSystemAnnouncementsRequestBuilder();
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static AnnouncementListRequestBuilder List(AnnouncementFilter filter, FilterPager pager = null)
		{
			return new AnnouncementListRequestBuilder(filter, pager);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static AnnouncementUpdateRequestBuilder Update(int announcementId, Announcement announcement)
		{
			return new AnnouncementUpdateRequestBuilder(announcementId, announcement);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static AnnouncementUpdateStatusRequestBuilder UpdateStatus(long id, bool status)
		{
			return new AnnouncementUpdateStatusRequestBuilder(id, status);
		}
	}
}
