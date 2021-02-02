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
	public class AssetFilePpvAddRequestBuilder : RequestBuilder<AssetFilePpv>
	{
		#region Constants
		public const string ASSET_FILE_PPV = "assetFilePpv";
		#endregion

		public AssetFilePpv AssetFilePpv { get; set; }

		public AssetFilePpvAddRequestBuilder()
			: base("assetfileppv", "add")
		{
		}

		public AssetFilePpvAddRequestBuilder(AssetFilePpv assetFilePpv)
			: this()
		{
			this.AssetFilePpv = assetFilePpv;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetFilePpv"))
				kparams.AddIfNotNull("assetFilePpv", AssetFilePpv);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<AssetFilePpv>(result);
		}
	}

	public class AssetFilePpvDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ASSET_FILE_ID = "assetFileId";
		public const string PPV_MODULE_ID = "ppvModuleId";
		#endregion

		public long AssetFileId { get; set; }
		public long PpvModuleId { get; set; }

		public AssetFilePpvDeleteRequestBuilder()
			: base("assetfileppv", "delete")
		{
		}

		public AssetFilePpvDeleteRequestBuilder(long assetFileId, long ppvModuleId)
			: this()
		{
			this.AssetFileId = assetFileId;
			this.PpvModuleId = ppvModuleId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetFileId"))
				kparams.AddIfNotNull("assetFileId", AssetFileId);
			if (!isMapped("ppvModuleId"))
				kparams.AddIfNotNull("ppvModuleId", PpvModuleId);
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

	public class AssetFilePpvListRequestBuilder : RequestBuilder<ListResponse<AssetFilePpv>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public AssetFilePpvFilter Filter { get; set; }

		public AssetFilePpvListRequestBuilder()
			: base("assetfileppv", "list")
		{
		}

		public AssetFilePpvListRequestBuilder(AssetFilePpvFilter filter)
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
			return ObjectFactory.Create<ListResponse<AssetFilePpv>>(result);
		}
	}

	public class AssetFilePpvUpdateRequestBuilder : RequestBuilder<AssetFilePpv>
	{
		#region Constants
		public const string ASSET_FILE_ID = "assetFileId";
		public const string PPV_MODULE_ID = "ppvModuleId";
		public const string ASSET_FILE_PPV = "assetFilePpv";
		#endregion

		public long AssetFileId { get; set; }
		public long PpvModuleId { get; set; }
		public AssetFilePpv AssetFilePpv { get; set; }

		public AssetFilePpvUpdateRequestBuilder()
			: base("assetfileppv", "update")
		{
		}

		public AssetFilePpvUpdateRequestBuilder(long assetFileId, long ppvModuleId, AssetFilePpv assetFilePpv)
			: this()
		{
			this.AssetFileId = assetFileId;
			this.PpvModuleId = ppvModuleId;
			this.AssetFilePpv = assetFilePpv;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetFileId"))
				kparams.AddIfNotNull("assetFileId", AssetFileId);
			if (!isMapped("ppvModuleId"))
				kparams.AddIfNotNull("ppvModuleId", PpvModuleId);
			if (!isMapped("assetFilePpv"))
				kparams.AddIfNotNull("assetFilePpv", AssetFilePpv);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<AssetFilePpv>(result);
		}
	}


	public class AssetFilePpvService
	{
		private AssetFilePpvService()
		{
		}

		public static AssetFilePpvAddRequestBuilder Add(AssetFilePpv assetFilePpv)
		{
			return new AssetFilePpvAddRequestBuilder(assetFilePpv);
		}

		public static AssetFilePpvDeleteRequestBuilder Delete(long assetFileId, long ppvModuleId)
		{
			return new AssetFilePpvDeleteRequestBuilder(assetFileId, ppvModuleId);
		}

		public static AssetFilePpvListRequestBuilder List(AssetFilePpvFilter filter)
		{
			return new AssetFilePpvListRequestBuilder(filter);
		}

		public static AssetFilePpvUpdateRequestBuilder Update(long assetFileId, long ppvModuleId, AssetFilePpv assetFilePpv)
		{
			return new AssetFilePpvUpdateRequestBuilder(assetFileId, ppvModuleId, assetFilePpv);
		}
	}
}
