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
	public class UserSegmentAddRequestBuilder : RequestBuilder<UserSegment>
	{
		#region Constants
		public const string USER_SEGMENT = "userSegment";
		#endregion

		public UserSegment UserSegment
		{
			set;
			get;
		}

		public UserSegmentAddRequestBuilder()
			: base("usersegment", "add")
		{
		}

		public UserSegmentAddRequestBuilder(UserSegment userSegment)
			: this()
		{
			this.UserSegment = userSegment;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("userSegment"))
				kparams.AddIfNotNull("userSegment", UserSegment);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<UserSegment>(result);
		}
	}

	public class UserSegmentDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public new const string USER_ID = "userId";
		public const string SEGMENT_ID = "segmentId";
		#endregion

		public new string UserId
		{
			set;
			get;
		}
		public long SegmentId
		{
			set;
			get;
		}

		public UserSegmentDeleteRequestBuilder()
			: base("usersegment", "delete")
		{
		}

		public UserSegmentDeleteRequestBuilder(string userId, long segmentId)
			: this()
		{
			this.UserId = userId;
			this.SegmentId = segmentId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("userId"))
				kparams.AddIfNotNull("userId", UserId);
			if (!isMapped("segmentId"))
				kparams.AddIfNotNull("segmentId", SegmentId);
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

	public class UserSegmentListRequestBuilder : RequestBuilder<ListResponse<UserSegment>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public UserSegmentFilter Filter
		{
			set;
			get;
		}
		public FilterPager Pager
		{
			set;
			get;
		}

		public UserSegmentListRequestBuilder()
			: base("usersegment", "list")
		{
		}

		public UserSegmentListRequestBuilder(UserSegmentFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<UserSegment>>(result);
		}
	}


	public class UserSegmentService
	{
		private UserSegmentService()
		{
		}

		public static UserSegmentAddRequestBuilder Add(UserSegment userSegment)
		{
			return new UserSegmentAddRequestBuilder(userSegment);
		}

		public static UserSegmentDeleteRequestBuilder Delete(string userId, long segmentId)
		{
			return new UserSegmentDeleteRequestBuilder(userId, segmentId);
		}

		public static UserSegmentListRequestBuilder List(UserSegmentFilter filter, FilterPager pager = null)
		{
			return new UserSegmentListRequestBuilder(filter, pager);
		}
	}
}
