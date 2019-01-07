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
	public class AssetFileGetContextRequestBuilder : RequestBuilder<AssetFileContext>
	{
		#region Constants
		public const string ID = "id";
		public const string CONTEXT_TYPE = "contextType";
		#endregion

		public string Id
		{
			set;
			get;
		}
		public ContextType ContextType
		{
			set;
			get;
		}

		public AssetFileGetContextRequestBuilder()
			: base("assetfile", "getContext")
		{
		}

		public AssetFileGetContextRequestBuilder(string id, ContextType contextType)
			: this()
		{
			this.Id = id;
			this.ContextType = contextType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("contextType"))
				kparams.AddIfNotNull("contextType", ContextType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<AssetFileContext>(result);
		}
	}

	public class AssetFilePlayManifestRequestBuilder : RequestBuilder<AssetFile>
	{
		#region Constants
		public new const string PARTNER_ID = "partnerId";
		public const string ASSET_ID = "assetId";
		public const string ASSET_TYPE = "assetType";
		public const string ASSET_FILE_ID = "assetFileId";
		public const string CONTEXT_TYPE = "contextType";
		public new const string KS = "ks";
		public const string TOKENIZED_URL = "tokenizedUrl";
		#endregion

		public new int PartnerId
		{
			set;
			get;
		}
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
		public long AssetFileId
		{
			set;
			get;
		}
		public PlaybackContextType ContextType
		{
			set;
			get;
		}
		public new string Ks
		{
			set;
			get;
		}
		public string TokenizedUrl
		{
			set;
			get;
		}

		public AssetFilePlayManifestRequestBuilder()
			: base("assetfile", "playManifest")
		{
		}

		public AssetFilePlayManifestRequestBuilder(int partnerId, string assetId, AssetType assetType, long assetFileId, PlaybackContextType contextType, string ks, string tokenizedUrl)
			: this()
		{
			this.PartnerId = partnerId;
			this.AssetId = assetId;
			this.AssetType = assetType;
			this.AssetFileId = assetFileId;
			this.ContextType = contextType;
			this.Ks = ks;
			this.TokenizedUrl = tokenizedUrl;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("partnerId"))
				kparams.AddIfNotNull("partnerId", PartnerId);
			if (!isMapped("assetId"))
				kparams.AddIfNotNull("assetId", AssetId);
			if (!isMapped("assetType"))
				kparams.AddIfNotNull("assetType", AssetType);
			if (!isMapped("assetFileId"))
				kparams.AddIfNotNull("assetFileId", AssetFileId);
			if (!isMapped("contextType"))
				kparams.AddIfNotNull("contextType", ContextType);
			if (!isMapped("ks"))
				kparams.AddIfNotNull("ks", Ks);
			if (!isMapped("tokenizedUrl"))
				kparams.AddIfNotNull("tokenizedUrl", TokenizedUrl);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<AssetFile>(result);
		}
	}


	public class AssetFileService
	{
		private AssetFileService()
		{
		}

		public static AssetFileGetContextRequestBuilder GetContext(string id, ContextType contextType)
		{
			return new AssetFileGetContextRequestBuilder(id, contextType);
		}

		public static AssetFilePlayManifestRequestBuilder PlayManifest(int partnerId, string assetId, AssetType assetType, long assetFileId, PlaybackContextType contextType, string ks = null, string tokenizedUrl = null)
		{
			return new AssetFilePlayManifestRequestBuilder(partnerId, assetId, assetType, assetFileId, contextType, ks, tokenizedUrl);
		}
	}
}
