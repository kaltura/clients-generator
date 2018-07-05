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
	public class AssetAddRequestBuilder : RequestBuilder<Asset>
	{
		#region Constants
		public const string ASSET = "asset";
		#endregion

		public Asset Asset
		{
			set;
			get;
		}

		public AssetAddRequestBuilder()
			: base("asset", "add")
		{
		}

		public AssetAddRequestBuilder(Asset asset)
			: this()
		{
			this.Asset = asset;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("asset"))
				kparams.AddIfNotNull("asset", Asset);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<Asset>(result);
		}
	}

	public class AssetCountRequestBuilder : RequestBuilder<AssetCount>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public SearchAssetFilter Filter
		{
			set;
			get;
		}

		public AssetCountRequestBuilder()
			: base("asset", "count")
		{
		}

		public AssetCountRequestBuilder(SearchAssetFilter filter)
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
			return ObjectFactory.Create<AssetCount>(result);
		}
	}

	public class AssetDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		public const string ASSET_REFERENCE_TYPE = "assetReferenceType";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public AssetReferenceType AssetReferenceType
		{
			set;
			get;
		}

		public AssetDeleteRequestBuilder()
			: base("asset", "delete")
		{
		}

		public AssetDeleteRequestBuilder(long id, AssetReferenceType assetReferenceType)
			: this()
		{
			this.Id = id;
			this.AssetReferenceType = assetReferenceType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("assetReferenceType"))
				kparams.AddIfNotNull("assetReferenceType", AssetReferenceType);
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
	}

	public class AssetGetRequestBuilder : RequestBuilder<Asset>
	{
		#region Constants
		public const string ID = "id";
		public const string ASSET_REFERENCE_TYPE = "assetReferenceType";
		#endregion

		public string Id
		{
			set;
			get;
		}
		public AssetReferenceType AssetReferenceType
		{
			set;
			get;
		}

		public AssetGetRequestBuilder()
			: base("asset", "get")
		{
		}

		public AssetGetRequestBuilder(string id, AssetReferenceType assetReferenceType)
			: this()
		{
			this.Id = id;
			this.AssetReferenceType = assetReferenceType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("assetReferenceType"))
				kparams.AddIfNotNull("assetReferenceType", AssetReferenceType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<Asset>(result);
		}
	}

	public class AssetGetAdsContextRequestBuilder : RequestBuilder<AdsContext>
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string ASSET_TYPE = "assetType";
		public const string CONTEXT_DATA_PARAMS = "contextDataParams";
		#endregion

		public string AssetId
		{
			set;
			get;
		}
		public AssetType AssetType
		{
			set;
			get;
		}
		public PlaybackContextOptions ContextDataParams
		{
			set;
			get;
		}

		public AssetGetAdsContextRequestBuilder()
			: base("asset", "getAdsContext")
		{
		}

		public AssetGetAdsContextRequestBuilder(string assetId, AssetType assetType, PlaybackContextOptions contextDataParams)
			: this()
		{
			this.AssetId = assetId;
			this.AssetType = assetType;
			this.ContextDataParams = contextDataParams;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetId"))
				kparams.AddIfNotNull("assetId", AssetId);
			if (!isMapped("assetType"))
				kparams.AddIfNotNull("assetType", AssetType);
			if (!isMapped("contextDataParams"))
				kparams.AddIfNotNull("contextDataParams", ContextDataParams);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<AdsContext>(result);
		}
	}

	public class AssetGetPlaybackContextRequestBuilder : RequestBuilder<PlaybackContext>
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string ASSET_TYPE = "assetType";
		public const string CONTEXT_DATA_PARAMS = "contextDataParams";
		#endregion

		public string AssetId
		{
			set;
			get;
		}
		public AssetType AssetType
		{
			set;
			get;
		}
		public PlaybackContextOptions ContextDataParams
		{
			set;
			get;
		}

		public AssetGetPlaybackContextRequestBuilder()
			: base("asset", "getPlaybackContext")
		{
		}

		public AssetGetPlaybackContextRequestBuilder(string assetId, AssetType assetType, PlaybackContextOptions contextDataParams)
			: this()
		{
			this.AssetId = assetId;
			this.AssetType = assetType;
			this.ContextDataParams = contextDataParams;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetId"))
				kparams.AddIfNotNull("assetId", AssetId);
			if (!isMapped("assetType"))
				kparams.AddIfNotNull("assetType", AssetType);
			if (!isMapped("contextDataParams"))
				kparams.AddIfNotNull("contextDataParams", ContextDataParams);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<PlaybackContext>(result);
		}
	}

	public class AssetListRequestBuilder : RequestBuilder<ListResponse<Asset>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public AssetFilter Filter
		{
			set;
			get;
		}
		public FilterPager Pager
		{
			set;
			get;
		}

		public AssetListRequestBuilder()
			: base("asset", "list")
		{
		}

		public AssetListRequestBuilder(AssetFilter filter, FilterPager pager)
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

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ListResponse<Asset>>(result);
		}
	}

	public class AssetRemoveMetasAndTagsRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		public const string ASSET_REFERENCE_TYPE = "assetReferenceType";
		public const string ID_IN = "idIn";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public AssetReferenceType AssetReferenceType
		{
			set;
			get;
		}
		public string IdIn
		{
			set;
			get;
		}

		public AssetRemoveMetasAndTagsRequestBuilder()
			: base("asset", "removeMetasAndTags")
		{
		}

		public AssetRemoveMetasAndTagsRequestBuilder(long id, AssetReferenceType assetReferenceType, string idIn)
			: this()
		{
			this.Id = id;
			this.AssetReferenceType = assetReferenceType;
			this.IdIn = idIn;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("assetReferenceType"))
				kparams.AddIfNotNull("assetReferenceType", AssetReferenceType);
			if (!isMapped("idIn"))
				kparams.AddIfNotNull("idIn", IdIn);
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
	}

	public class AssetUpdateRequestBuilder : RequestBuilder<Asset>
	{
		#region Constants
		public const string ID = "id";
		public const string ASSET = "asset";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public Asset Asset
		{
			set;
			get;
		}

		public AssetUpdateRequestBuilder()
			: base("asset", "update")
		{
		}

		public AssetUpdateRequestBuilder(long id, Asset asset)
			: this()
		{
			this.Id = id;
			this.Asset = asset;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("asset"))
				kparams.AddIfNotNull("asset", Asset);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<Asset>(result);
		}
	}


	public class AssetService
	{
		private AssetService()
		{
		}

		public static AssetAddRequestBuilder Add(Asset asset)
		{
			return new AssetAddRequestBuilder(asset);
		}

		public static AssetCountRequestBuilder Count(SearchAssetFilter filter = null)
		{
			return new AssetCountRequestBuilder(filter);
		}

		public static AssetDeleteRequestBuilder Delete(long id, AssetReferenceType assetReferenceType)
		{
			return new AssetDeleteRequestBuilder(id, assetReferenceType);
		}

		public static AssetGetRequestBuilder Get(string id, AssetReferenceType assetReferenceType)
		{
			return new AssetGetRequestBuilder(id, assetReferenceType);
		}

		public static AssetGetAdsContextRequestBuilder GetAdsContext(string assetId, AssetType assetType, PlaybackContextOptions contextDataParams)
		{
			return new AssetGetAdsContextRequestBuilder(assetId, assetType, contextDataParams);
		}

		public static AssetGetPlaybackContextRequestBuilder GetPlaybackContext(string assetId, AssetType assetType, PlaybackContextOptions contextDataParams)
		{
			return new AssetGetPlaybackContextRequestBuilder(assetId, assetType, contextDataParams);
		}

		public static AssetListRequestBuilder List(AssetFilter filter = null, FilterPager pager = null)
		{
			return new AssetListRequestBuilder(filter, pager);
		}

		public static AssetRemoveMetasAndTagsRequestBuilder RemoveMetasAndTags(long id, AssetReferenceType assetReferenceType, string idIn)
		{
			return new AssetRemoveMetasAndTagsRequestBuilder(id, assetReferenceType, idIn);
		}

		public static AssetUpdateRequestBuilder Update(long id, Asset asset)
		{
			return new AssetUpdateRequestBuilder(id, asset);
		}
	}
}
