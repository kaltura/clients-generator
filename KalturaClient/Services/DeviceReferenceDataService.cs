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
	public class DeviceReferenceDataAddRequestBuilder : RequestBuilder<DeviceReferenceData>
	{
		#region Constants
		public const string OBJECT_TO_ADD = "objectToAdd";
		#endregion

		public DeviceReferenceData ObjectToAdd { get; set; }

		public DeviceReferenceDataAddRequestBuilder()
			: base("devicereferencedata", "add")
		{
		}

		public DeviceReferenceDataAddRequestBuilder(DeviceReferenceData objectToAdd)
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
			return ObjectFactory.Create<DeviceReferenceData>(result);
		}
	}

	public class DeviceReferenceDataUpdateRequestBuilder : RequestBuilder<DeviceReferenceData>
	{
		#region Constants
		public const string ID = "id";
		public const string OBJECT_TO_UPDATE = "objectToUpdate";
		#endregion

		public long Id { get; set; }
		public DeviceReferenceData ObjectToUpdate { get; set; }

		public DeviceReferenceDataUpdateRequestBuilder()
			: base("devicereferencedata", "update")
		{
		}

		public DeviceReferenceDataUpdateRequestBuilder(long id, DeviceReferenceData objectToUpdate)
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
			return ObjectFactory.Create<DeviceReferenceData>(result);
		}
	}

	public class DeviceReferenceDataDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public DeviceReferenceDataDeleteRequestBuilder()
			: base("devicereferencedata", "delete")
		{
		}

		public DeviceReferenceDataDeleteRequestBuilder(long id)
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

	public class DeviceReferenceDataListRequestBuilder : RequestBuilder<ListResponse<DeviceReferenceData>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public DeviceReferenceDataFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public DeviceReferenceDataListRequestBuilder()
			: base("devicereferencedata", "list")
		{
		}

		public DeviceReferenceDataListRequestBuilder(DeviceReferenceDataFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<DeviceReferenceData>>(result);
		}
	}


	public class DeviceReferenceDataService
	{
		private DeviceReferenceDataService()
		{
		}

		public static DeviceReferenceDataAddRequestBuilder Add(DeviceReferenceData objectToAdd)
		{
			return new DeviceReferenceDataAddRequestBuilder(objectToAdd);
		}

		public static DeviceReferenceDataUpdateRequestBuilder Update(long id, DeviceReferenceData objectToUpdate)
		{
			return new DeviceReferenceDataUpdateRequestBuilder(id, objectToUpdate);
		}

		public static DeviceReferenceDataDeleteRequestBuilder Delete(long id)
		{
			return new DeviceReferenceDataDeleteRequestBuilder(id);
		}

		public static DeviceReferenceDataListRequestBuilder List(DeviceReferenceDataFilter filter, FilterPager pager = null)
		{
			return new DeviceReferenceDataListRequestBuilder(filter, pager);
		}
	}
}
