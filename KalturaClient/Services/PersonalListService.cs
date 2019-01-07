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
// Copyright (C) 2006-2019  Kaltura Inc.
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
	public class PersonalListAddRequestBuilder : RequestBuilder<PersonalList>
	{
		#region Constants
		public const string PERSONAL_LIST = "personalList";
		#endregion

		public PersonalList PersonalList
		{
			set;
			get;
		}

		public PersonalListAddRequestBuilder()
			: base("personallist", "add")
		{
		}

		public PersonalListAddRequestBuilder(PersonalList personalList)
			: this()
		{
			this.PersonalList = personalList;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("personalList"))
				kparams.AddIfNotNull("personalList", PersonalList);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PersonalList>(result);
		}
	}

	public class PersonalListDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string PERSONAL_LIST_ID = "personalListId";
		#endregion

		public long PersonalListId
		{
			set;
			get;
		}

		public PersonalListDeleteRequestBuilder()
			: base("personallist", "delete")
		{
		}

		public PersonalListDeleteRequestBuilder(long personalListId)
			: this()
		{
			this.PersonalListId = personalListId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("personalListId"))
				kparams.AddIfNotNull("personalListId", PersonalListId);
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

	public class PersonalListListRequestBuilder : RequestBuilder<ListResponse<PersonalList>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public PersonalListFilter Filter
		{
			set;
			get;
		}
		public FilterPager Pager
		{
			set;
			get;
		}

		public PersonalListListRequestBuilder()
			: base("personallist", "list")
		{
		}

		public PersonalListListRequestBuilder(PersonalListFilter filter, FilterPager pager)
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
			return ObjectFactory.Create<ListResponse<PersonalList>>(result);
		}
	}


	public class PersonalListService
	{
		private PersonalListService()
		{
		}

		public static PersonalListAddRequestBuilder Add(PersonalList personalList)
		{
			return new PersonalListAddRequestBuilder(personalList);
		}

		public static PersonalListDeleteRequestBuilder Delete(long personalListId)
		{
			return new PersonalListDeleteRequestBuilder(personalListId);
		}

		public static PersonalListListRequestBuilder List(PersonalListFilter filter = null, FilterPager pager = null)
		{
			return new PersonalListListRequestBuilder(filter, pager);
		}
	}
}
