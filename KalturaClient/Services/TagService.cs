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
	public class TagAddRequestBuilder : RequestBuilder<Tag>
	{
		#region Constants
		public const string TAG = "tag";
		#endregion

		public Tag Tag { get; set; }

		public TagAddRequestBuilder()
			: base("tag", "add")
		{
		}

		public TagAddRequestBuilder(Tag tag)
			: this()
		{
			this.Tag = tag;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("tag"))
				kparams.AddIfNotNull("tag", Tag);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Tag>(result);
		}
	}

	public class TagDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public TagDeleteRequestBuilder()
			: base("tag", "delete")
		{
		}

		public TagDeleteRequestBuilder(long id)
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

	public class TagListRequestBuilder : RequestBuilder<ListResponse<Tag>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public TagFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public TagListRequestBuilder()
			: base("tag", "list")
		{
		}

		public TagListRequestBuilder(TagFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<Tag>>(result);
		}
	}

	public class TagUpdateRequestBuilder : RequestBuilder<Tag>
	{
		#region Constants
		public const string ID = "id";
		public const string TAG = "tag";
		#endregion

		public long Id { get; set; }
		public Tag Tag { get; set; }

		public TagUpdateRequestBuilder()
			: base("tag", "update")
		{
		}

		public TagUpdateRequestBuilder(long id, Tag tag)
			: this()
		{
			this.Id = id;
			this.Tag = tag;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("tag"))
				kparams.AddIfNotNull("tag", Tag);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Tag>(result);
		}
	}


	public class TagService
	{
		private TagService()
		{
		}

		public static TagAddRequestBuilder Add(Tag tag)
		{
			return new TagAddRequestBuilder(tag);
		}

		public static TagDeleteRequestBuilder Delete(long id)
		{
			return new TagDeleteRequestBuilder(id);
		}

		public static TagListRequestBuilder List(TagFilter filter = null, FilterPager pager = null)
		{
			return new TagListRequestBuilder(filter, pager);
		}

		public static TagUpdateRequestBuilder Update(long id, Tag tag)
		{
			return new TagUpdateRequestBuilder(id, tag);
		}
	}
}
