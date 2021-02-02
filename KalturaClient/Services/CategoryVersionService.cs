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
	public class CategoryVersionAddRequestBuilder : RequestBuilder<CategoryVersion>
	{
		#region Constants
		public const string OBJECT_TO_ADD = "objectToAdd";
		#endregion

		public CategoryVersion ObjectToAdd { get; set; }

		public CategoryVersionAddRequestBuilder()
			: base("categoryversion", "add")
		{
		}

		public CategoryVersionAddRequestBuilder(CategoryVersion objectToAdd)
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
			return ObjectFactory.Create<CategoryVersion>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class CategoryVersionUpdateRequestBuilder : RequestBuilder<CategoryVersion>
	{
		#region Constants
		public const string ID = "id";
		public const string OBJECT_TO_UPDATE = "objectToUpdate";
		#endregion

		public long Id { get; set; }
		public CategoryVersion ObjectToUpdate { get; set; }

		public CategoryVersionUpdateRequestBuilder()
			: base("categoryversion", "update")
		{
		}

		public CategoryVersionUpdateRequestBuilder(long id, CategoryVersion objectToUpdate)
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
			return ObjectFactory.Create<CategoryVersion>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class CategoryVersionDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public CategoryVersionDeleteRequestBuilder()
			: base("categoryversion", "delete")
		{
		}

		public CategoryVersionDeleteRequestBuilder(long id)
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
	public class CategoryVersionListRequestBuilder : RequestBuilder<ListResponse<CategoryVersion>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public CategoryVersionFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public CategoryVersionListRequestBuilder()
			: base("categoryversion", "list")
		{
		}

		public CategoryVersionListRequestBuilder(CategoryVersionFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<CategoryVersion>>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class CategoryVersionCreateTreeRequestBuilder : RequestBuilder<CategoryVersion>
	{
		#region Constants
		public const string CATEGORY_ITEM_ID = "categoryItemId";
		public const string NAME = "name";
		public const string COMMENT = "comment";
		#endregion

		public long CategoryItemId { get; set; }
		public string Name { get; set; }
		public string Comment { get; set; }

		public CategoryVersionCreateTreeRequestBuilder()
			: base("categoryversion", "createTree")
		{
		}

		public CategoryVersionCreateTreeRequestBuilder(long categoryItemId, string name, string comment)
			: this()
		{
			this.CategoryItemId = categoryItemId;
			this.Name = name;
			this.Comment = comment;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("categoryItemId"))
				kparams.AddIfNotNull("categoryItemId", CategoryItemId);
			if (!isMapped("name"))
				kparams.AddIfNotNull("name", Name);
			if (!isMapped("comment"))
				kparams.AddIfNotNull("comment", Comment);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<CategoryVersion>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class CategoryVersionSetDefaultRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		public const string FORCE = "force";
		#endregion

		public long Id { get; set; }
		public bool Force { get; set; }

		public CategoryVersionSetDefaultRequestBuilder()
			: base("categoryversion", "setDefault")
		{
		}

		public CategoryVersionSetDefaultRequestBuilder(long id, bool force)
			: this()
		{
			this.Id = id;
			this.Force = force;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("force"))
				kparams.AddIfNotNull("force", Force);
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


	public class CategoryVersionService
	{
		private CategoryVersionService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static CategoryVersionAddRequestBuilder Add(CategoryVersion objectToAdd)
		{
			return new CategoryVersionAddRequestBuilder(objectToAdd);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static CategoryVersionUpdateRequestBuilder Update(long id, CategoryVersion objectToUpdate)
		{
			return new CategoryVersionUpdateRequestBuilder(id, objectToUpdate);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static CategoryVersionDeleteRequestBuilder Delete(long id)
		{
			return new CategoryVersionDeleteRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static CategoryVersionListRequestBuilder List(CategoryVersionFilter filter, FilterPager pager = null)
		{
			return new CategoryVersionListRequestBuilder(filter, pager);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static CategoryVersionCreateTreeRequestBuilder CreateTree(long categoryItemId, string name, string comment)
		{
			return new CategoryVersionCreateTreeRequestBuilder(categoryItemId, name, comment);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static CategoryVersionSetDefaultRequestBuilder SetDefault(long id, bool force = false)
		{
			return new CategoryVersionSetDefaultRequestBuilder(id, force);
		}
	}
}
