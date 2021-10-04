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
	public class HouseholdSegmentAddRequestBuilder : RequestBuilder<HouseholdSegment>
	{
		#region Constants
		public const string OBJECT_TO_ADD = "objectToAdd";
		#endregion

		public HouseholdSegment ObjectToAdd { get; set; }

		public HouseholdSegmentAddRequestBuilder()
			: base("householdsegment", "add")
		{
		}

		public HouseholdSegmentAddRequestBuilder(HouseholdSegment objectToAdd)
			: this()
		{
			this.ObjectToAdd = objectToAdd;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("objectToAdd"))
				kparams.AddIfNotNull("objectToAdd", ObjectToAdd);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<HouseholdSegment>(result);
		}
	}

	public class HouseholdSegmentDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public HouseholdSegmentDeleteRequestBuilder()
			: base("householdsegment", "delete")
		{
		}

		public HouseholdSegmentDeleteRequestBuilder(long id)
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

	public class HouseholdSegmentListRequestBuilder : RequestBuilder<ListResponse<HouseholdSegment>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public HouseholdSegmentFilter Filter { get; set; }

		public HouseholdSegmentListRequestBuilder()
			: base("householdsegment", "list")
		{
		}

		public HouseholdSegmentListRequestBuilder(HouseholdSegmentFilter filter)
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
			return ObjectFactory.Create<ListResponse<HouseholdSegment>>(result);
		}
	}


	public class HouseholdSegmentService
	{
		private HouseholdSegmentService()
		{
		}

		public static HouseholdSegmentAddRequestBuilder Add(HouseholdSegment objectToAdd)
		{
			return new HouseholdSegmentAddRequestBuilder(objectToAdd);
		}

		public static HouseholdSegmentDeleteRequestBuilder Delete(long id)
		{
			return new HouseholdSegmentDeleteRequestBuilder(id);
		}

		public static HouseholdSegmentListRequestBuilder List(HouseholdSegmentFilter filter = null)
		{
			return new HouseholdSegmentListRequestBuilder(filter);
		}
	}
}
