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
	public class DynamicListAddRequestBuilder : RequestBuilder<DynamicList>
	{
		#region Constants
		public const string OBJECT_TO_ADD = "objectToAdd";
		#endregion

		public DynamicList ObjectToAdd { get; set; }

		public DynamicListAddRequestBuilder()
			: base("dynamiclist", "add")
		{
		}

		public DynamicListAddRequestBuilder(DynamicList objectToAdd)
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
			return ObjectFactory.Create<DynamicList>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class DynamicListUpdateRequestBuilder : RequestBuilder<DynamicList>
	{
		#region Constants
		public const string ID = "id";
		public const string OBJECT_TO_UPDATE = "objectToUpdate";
		#endregion

		public long Id { get; set; }
		public DynamicList ObjectToUpdate { get; set; }

		public DynamicListUpdateRequestBuilder()
			: base("dynamiclist", "update")
		{
		}

		public DynamicListUpdateRequestBuilder(long id, DynamicList objectToUpdate)
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
			return ObjectFactory.Create<DynamicList>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class DynamicListDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public DynamicListDeleteRequestBuilder()
			: base("dynamiclist", "delete")
		{
		}

		public DynamicListDeleteRequestBuilder(long id)
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
	public class DynamicListListRequestBuilder : RequestBuilder<ListResponse<DynamicList>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public DynamicListFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public DynamicListListRequestBuilder()
			: base("dynamiclist", "list")
		{
		}

		public DynamicListListRequestBuilder(DynamicListFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<DynamicList>>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class DynamicListAddFromBulkUploadRequestBuilder : RequestBuilder<BulkUpload>
	{
		#region Constants
		public const string FILE_DATA = "fileData";
		public const string JOB_DATA = "jobData";
		public const string BULK_UPLOAD_DATA = "bulkUploadData";
		#endregion

		public Stream FileData { get; set; }
		public string FileData_FileName { get; set; }
		public BulkUploadExcelJobData JobData { get; set; }
		public BulkUploadDynamicListData BulkUploadData { get; set; }

		public DynamicListAddFromBulkUploadRequestBuilder()
			: base("dynamiclist", "addFromBulkUpload")
		{
		}

		public DynamicListAddFromBulkUploadRequestBuilder(Stream fileData, BulkUploadExcelJobData jobData, BulkUploadDynamicListData bulkUploadData)
			: this()
		{
			this.FileData = fileData;
			this.JobData = jobData;
			this.BulkUploadData = bulkUploadData;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("jobData"))
				kparams.AddIfNotNull("jobData", JobData);
			if (!isMapped("bulkUploadData"))
				kparams.AddIfNotNull("bulkUploadData", BulkUploadData);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			kfiles.Add("fileData", new FileData(FileData, FileData_FileName));
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<BulkUpload>(result);
		}
	}


	public class DynamicListService
	{
		private DynamicListService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static DynamicListAddRequestBuilder Add(DynamicList objectToAdd)
		{
			return new DynamicListAddRequestBuilder(objectToAdd);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static DynamicListUpdateRequestBuilder Update(long id, DynamicList objectToUpdate)
		{
			return new DynamicListUpdateRequestBuilder(id, objectToUpdate);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static DynamicListDeleteRequestBuilder Delete(long id)
		{
			return new DynamicListDeleteRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static DynamicListListRequestBuilder List(DynamicListFilter filter, FilterPager pager = null)
		{
			return new DynamicListListRequestBuilder(filter, pager);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static DynamicListAddFromBulkUploadRequestBuilder AddFromBulkUpload(Stream fileData, BulkUploadExcelJobData jobData, BulkUploadDynamicListData bulkUploadData)
		{
			return new DynamicListAddFromBulkUploadRequestBuilder(fileData, jobData, bulkUploadData);
		}
	}
}
