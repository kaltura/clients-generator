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
	public class CategoryTreeDuplicateRequestBuilder : RequestBuilder<CategoryTree>
	{
		#region Constants
		public const string CATEGORY_ITEM_ID = "categoryItemId";
		public const string NAME = "name";
		#endregion

		public long CategoryItemId { get; set; }
		public string Name { get; set; }

		public CategoryTreeDuplicateRequestBuilder()
			: base("categorytree", "duplicate")
		{
		}

		public CategoryTreeDuplicateRequestBuilder(long categoryItemId, string name)
			: this()
		{
			this.CategoryItemId = categoryItemId;
			this.Name = name;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("categoryItemId"))
				kparams.AddIfNotNull("categoryItemId", CategoryItemId);
			if (!isMapped("name"))
				kparams.AddIfNotNull("name", Name);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<CategoryTree>(result);
		}
	}

	public class CategoryTreeGetRequestBuilder : RequestBuilder<CategoryTree>
	{
		#region Constants
		public const string CATEGORY_ITEM_ID = "categoryItemId";
		public const string FILTER = "filter";
		#endregion

		public long CategoryItemId { get; set; }
		public bool Filter { get; set; }

		public CategoryTreeGetRequestBuilder()
			: base("categorytree", "get")
		{
		}

		public CategoryTreeGetRequestBuilder(long categoryItemId, bool filter)
			: this()
		{
			this.CategoryItemId = categoryItemId;
			this.Filter = filter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("categoryItemId"))
				kparams.AddIfNotNull("categoryItemId", CategoryItemId);
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
			return ObjectFactory.Create<CategoryTree>(result);
		}
	}

	public class CategoryTreeGetByVersion RequestBuilder : RequestBuilder<CategoryTree>
	{
		#region Constants
		public const string VERSION_ID = "versionId";
		#endregion

		public long VersionId { get; set; }

		public CategoryTreeGetByVersion RequestBuilder()
			: base("categorytree", "getByVersion ")
		{
		}

		public CategoryTreeGetByVersion RequestBuilder(long versionId)
			: this()
		{
			this.VersionId = versionId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("versionId"))
				kparams.AddIfNotNull("versionId", VersionId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<CategoryTree>(result);
		}
	}


	public class CategoryTreeService
	{
		private CategoryTreeService()
		{
		}

		public static CategoryTreeDuplicateRequestBuilder Duplicate(long categoryItemId, string name)
		{
			return new CategoryTreeDuplicateRequestBuilder(categoryItemId, name);
		}

		public static CategoryTreeGetRequestBuilder Get(long categoryItemId, bool filter = false)
		{
			return new CategoryTreeGetRequestBuilder(categoryItemId, filter);
		}

		public static CategoryTreeGetByVersion RequestBuilder GetByVersion (long versionId = null)
		{
			return new CategoryTreeGetByVersion RequestBuilder(versionId);
		}
	}
}
