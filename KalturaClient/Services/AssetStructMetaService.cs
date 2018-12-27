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
	public class AssetStructMetaListRequestBuilder : RequestBuilder<ListResponse<AssetStructMeta>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public AssetStructMetaFilter Filter
		{
			set;
			get;
		}

		public AssetStructMetaListRequestBuilder()
			: base("assetstructmeta", "list")
		{
		}

		public AssetStructMetaListRequestBuilder(AssetStructMetaFilter filter)
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
			return ObjectFactory.Create<ListResponse<AssetStructMeta>>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<ListResponse<AssetStructMeta>>((IDictionary<string,object>)result);
		}
	}

	public class AssetStructMetaUpdateRequestBuilder : RequestBuilder<AssetStructMeta>
	{
		#region Constants
		public const string ASSET_STRUCT_ID = "assetStructId";
		public const string META_ID = "metaId";
		public const string ASSET_STRUCT_META = "assetStructMeta";
		#endregion

		public long AssetStructId
		{
			set;
			get;
		}
		public long MetaId
		{
			set;
			get;
		}
		public AssetStructMeta AssetStructMeta
		{
			set;
			get;
		}

		public AssetStructMetaUpdateRequestBuilder()
			: base("assetstructmeta", "update")
		{
		}

		public AssetStructMetaUpdateRequestBuilder(long assetStructId, long metaId, AssetStructMeta assetStructMeta)
			: this()
		{
			this.AssetStructId = assetStructId;
			this.MetaId = metaId;
			this.AssetStructMeta = assetStructMeta;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetStructId"))
				kparams.AddIfNotNull("assetStructId", AssetStructId);
			if (!isMapped("metaId"))
				kparams.AddIfNotNull("metaId", MetaId);
			if (!isMapped("assetStructMeta"))
				kparams.AddIfNotNull("assetStructMeta", AssetStructMeta);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<AssetStructMeta>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<AssetStructMeta>((IDictionary<string,object>)result);
		}
	}


	public class AssetStructMetaService
	{
		private AssetStructMetaService()
		{
		}

		public static AssetStructMetaListRequestBuilder List(AssetStructMetaFilter filter)
		{
			return new AssetStructMetaListRequestBuilder(filter);
		}

		public static AssetStructMetaUpdateRequestBuilder Update(long assetStructId, long metaId, AssetStructMeta assetStructMeta)
		{
			return new AssetStructMetaUpdateRequestBuilder(assetStructId, metaId, assetStructMeta);
		}
	}
}
