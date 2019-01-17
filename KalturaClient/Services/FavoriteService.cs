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
	public class FavoriteAddRequestBuilder : RequestBuilder<Favorite>
	{
		#region Constants
		public const string FAVORITE = "favorite";
		#endregion

		public Favorite Favorite
		{
			set;
			get;
		}

		public FavoriteAddRequestBuilder()
			: base("favorite", "add")
		{
		}

		public FavoriteAddRequestBuilder(Favorite favorite)
			: this()
		{
			this.Favorite = favorite;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("favorite"))
				kparams.AddIfNotNull("favorite", Favorite);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<Favorite>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<Favorite>((IDictionary<string,object>)result);
		}
	}

	public class FavoriteDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id
		{
			set;
			get;
		}

		public FavoriteDeleteRequestBuilder()
			: base("favorite", "delete")
		{
		}

		public FavoriteDeleteRequestBuilder(long id)
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

		public override object Deserialize(XmlElement result)
		{
			if (result.InnerText.Equals("1") || result.InnerText.ToLower().Equals("true"))
				return true;
			return false;
		}
		public override object DeserializeObject(object result)
		{
			var resultStr = (string)result;
			if (resultStr.Equals("1") || resultStr.ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class FavoriteListRequestBuilder : RequestBuilder<ListResponse<Favorite>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public FavoriteFilter Filter
		{
			set;
			get;
		}

		public FavoriteListRequestBuilder()
			: base("favorite", "list")
		{
		}

		public FavoriteListRequestBuilder(FavoriteFilter filter)
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

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ListResponse<Favorite>>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<ListResponse<Favorite>>((IDictionary<string,object>)result);
		}
	}


	public class FavoriteService
	{
		private FavoriteService()
		{
		}

		public static FavoriteAddRequestBuilder Add(Favorite favorite)
		{
			return new FavoriteAddRequestBuilder(favorite);
		}

		public static FavoriteDeleteRequestBuilder Delete(long id)
		{
			return new FavoriteDeleteRequestBuilder(id);
		}

		public static FavoriteListRequestBuilder List(FavoriteFilter filter = null)
		{
			return new FavoriteListRequestBuilder(filter);
		}
	}
}
