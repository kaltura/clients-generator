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
	public class BookmarkAddRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string BOOKMARK = "bookmark";
		#endregion

		public Bookmark Bookmark { get; set; }

		public BookmarkAddRequestBuilder()
			: base("bookmark", "add")
		{
		}

		public BookmarkAddRequestBuilder(Bookmark bookmark)
			: this()
		{
			this.Bookmark = bookmark;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("bookmark"))
				kparams.AddIfNotNull("bookmark", Bookmark);
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

	public class BookmarkListRequestBuilder : RequestBuilder<ListResponse<Bookmark>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public BookmarkFilter Filter { get; set; }

		public BookmarkListRequestBuilder()
			: base("bookmark", "list")
		{
		}

		public BookmarkListRequestBuilder(BookmarkFilter filter)
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
			return ObjectFactory.Create<ListResponse<Bookmark>>(result);
		}
	}


	public class BookmarkService
	{
		private BookmarkService()
		{
		}

		public static BookmarkAddRequestBuilder Add(Bookmark bookmark)
		{
			return new BookmarkAddRequestBuilder(bookmark);
		}

		public static BookmarkListRequestBuilder List(BookmarkFilter filter)
		{
			return new BookmarkListRequestBuilder(filter);
		}
	}
}
