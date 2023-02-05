// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
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
	public class SegmentationTypeAddRequestBuilder : RequestBuilder<SegmentationType>
	{
		#region Constants
		public const string SEGMENTATION_TYPE = "segmentationType";
		#endregion

		public SegmentationType SegmentationType { get; set; }

		public SegmentationTypeAddRequestBuilder()
			: base("segmentationtype", "add")
		{
		}

		public SegmentationTypeAddRequestBuilder(SegmentationType segmentationType)
			: this()
		{
			this.SegmentationType = segmentationType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("segmentationType"))
				kparams.AddIfNotNull("segmentationType", SegmentationType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<SegmentationType>(result);
		}
	}

	public class SegmentationTypeDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public SegmentationTypeDeleteRequestBuilder()
			: base("segmentationtype", "delete")
		{
		}

		public SegmentationTypeDeleteRequestBuilder(long id)
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

	public class SegmentationTypeListRequestBuilder : RequestBuilder<ListResponse<SegmentationType>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public BaseSegmentationTypeFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public SegmentationTypeListRequestBuilder()
			: base("segmentationtype", "list")
		{
		}

		public SegmentationTypeListRequestBuilder(BaseSegmentationTypeFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<SegmentationType>>(result);
		}
	}

	public class SegmentationTypeUpdateRequestBuilder : RequestBuilder<SegmentationType>
	{
		#region Constants
		public const string SEGMENTATION_TYPE_ID = "segmentationTypeId";
		public const string SEGMENTATION_TYPE = "segmentationType";
		#endregion

		public long SegmentationTypeId { get; set; }
		public SegmentationType SegmentationType { get; set; }

		public SegmentationTypeUpdateRequestBuilder()
			: base("segmentationtype", "update")
		{
		}

		public SegmentationTypeUpdateRequestBuilder(long segmentationTypeId, SegmentationType segmentationType)
			: this()
		{
			this.SegmentationTypeId = segmentationTypeId;
			this.SegmentationType = segmentationType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("segmentationTypeId"))
				kparams.AddIfNotNull("segmentationTypeId", SegmentationTypeId);
			if (!isMapped("segmentationType"))
				kparams.AddIfNotNull("segmentationType", SegmentationType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<SegmentationType>(result);
		}
	}


	public class SegmentationTypeService
	{
		private SegmentationTypeService()
		{
		}

		public static SegmentationTypeAddRequestBuilder Add(SegmentationType segmentationType)
		{
			return new SegmentationTypeAddRequestBuilder(segmentationType);
		}

		public static SegmentationTypeDeleteRequestBuilder Delete(long id)
		{
			return new SegmentationTypeDeleteRequestBuilder(id);
		}

		public static SegmentationTypeListRequestBuilder List(BaseSegmentationTypeFilter filter = null, FilterPager pager = null)
		{
			return new SegmentationTypeListRequestBuilder(filter, pager);
		}

		public static SegmentationTypeUpdateRequestBuilder Update(long segmentationTypeId, SegmentationType segmentationType)
		{
			return new SegmentationTypeUpdateRequestBuilder(segmentationTypeId, segmentationType);
		}
	}
}
