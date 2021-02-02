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
	public class AssetStructAddRequestBuilder : RequestBuilder<AssetStruct>
	{
		#region Constants
		public const string ASSET_STRUCT = "assetStruct";
		#endregion

		public AssetStruct AssetStruct { get; set; }

		public AssetStructAddRequestBuilder()
			: base("assetstruct", "add")
		{
		}

		public AssetStructAddRequestBuilder(AssetStruct assetStruct)
			: this()
		{
			this.AssetStruct = assetStruct;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetStruct"))
				kparams.AddIfNotNull("assetStruct", AssetStruct);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<AssetStruct>(result);
		}
	}

	public class AssetStructDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public AssetStructDeleteRequestBuilder()
			: base("assetstruct", "delete")
		{
		}

		public AssetStructDeleteRequestBuilder(long id)
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

	public class AssetStructGetRequestBuilder : RequestBuilder<AssetStruct>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public AssetStructGetRequestBuilder()
			: base("assetstruct", "get")
		{
		}

		public AssetStructGetRequestBuilder(long id)
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
			return ObjectFactory.Create<AssetStruct>(result);
		}
	}

	public class AssetStructListRequestBuilder : RequestBuilder<ListResponse<AssetStruct>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public AssetStructFilter Filter { get; set; }

		public AssetStructListRequestBuilder()
			: base("assetstruct", "list")
		{
		}

		public AssetStructListRequestBuilder(AssetStructFilter filter)
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
			return ObjectFactory.Create<ListResponse<AssetStruct>>(result);
		}
	}

	public class AssetStructUpdateRequestBuilder : RequestBuilder<AssetStruct>
	{
		#region Constants
		public const string ID = "id";
		public const string ASSET_STRUCT = "assetStruct";
		#endregion

		public long Id { get; set; }
		public AssetStruct AssetStruct { get; set; }

		public AssetStructUpdateRequestBuilder()
			: base("assetstruct", "update")
		{
		}

		public AssetStructUpdateRequestBuilder(long id, AssetStruct assetStruct)
			: this()
		{
			this.Id = id;
			this.AssetStruct = assetStruct;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("assetStruct"))
				kparams.AddIfNotNull("assetStruct", AssetStruct);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<AssetStruct>(result);
		}
	}


	public class AssetStructService
	{
		private AssetStructService()
		{
		}

		public static AssetStructAddRequestBuilder Add(AssetStruct assetStruct)
		{
			return new AssetStructAddRequestBuilder(assetStruct);
		}

		public static AssetStructDeleteRequestBuilder Delete(long id)
		{
			return new AssetStructDeleteRequestBuilder(id);
		}

		public static AssetStructGetRequestBuilder Get(long id)
		{
			return new AssetStructGetRequestBuilder(id);
		}

		public static AssetStructListRequestBuilder List(AssetStructFilter filter = null)
		{
			return new AssetStructListRequestBuilder(filter);
		}

		public static AssetStructUpdateRequestBuilder Update(long id, AssetStruct assetStruct)
		{
			return new AssetStructUpdateRequestBuilder(id, assetStruct);
		}
	}
}
