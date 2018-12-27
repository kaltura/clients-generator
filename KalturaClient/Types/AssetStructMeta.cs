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
using Kaltura.Enums;
using Kaltura.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class AssetStructMeta : ObjectBase
	{
		#region Constants
		public const string ASSET_STRUCT_ID = "assetStructId";
		public const string META_ID = "metaId";
		public const string INGEST_REFERENCE_PATH = "ingestReferencePath";
		public const string PROTECT_FROM_INGEST = "protectFromIngest";
		public const string DEFAULT_INGEST_VALUE = "defaultIngestValue";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		public const string IS_INHERITED = "isInherited";
		#endregion

		#region Private Fields
		private long _AssetStructId = long.MinValue;
		private long _MetaId = long.MinValue;
		private string _IngestReferencePath = null;
		private bool? _ProtectFromIngest = null;
		private string _DefaultIngestValue = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		private bool? _IsInherited = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long AssetStructId
		{
			get { return _AssetStructId; }
			private set 
			{ 
				_AssetStructId = value;
				OnPropertyChanged("AssetStructId");
			}
		}
		[JsonProperty]
		public long MetaId
		{
			get { return _MetaId; }
			private set 
			{ 
				_MetaId = value;
				OnPropertyChanged("MetaId");
			}
		}
		[JsonProperty]
		public string IngestReferencePath
		{
			get { return _IngestReferencePath; }
			set 
			{ 
				_IngestReferencePath = value;
				OnPropertyChanged("IngestReferencePath");
			}
		}
		[JsonProperty]
		public bool? ProtectFromIngest
		{
			get { return _ProtectFromIngest; }
			set 
			{ 
				_ProtectFromIngest = value;
				OnPropertyChanged("ProtectFromIngest");
			}
		}
		[JsonProperty]
		public string DefaultIngestValue
		{
			get { return _DefaultIngestValue; }
			set 
			{ 
				_DefaultIngestValue = value;
				OnPropertyChanged("DefaultIngestValue");
			}
		}
		[JsonProperty]
		public long CreateDate
		{
			get { return _CreateDate; }
			private set 
			{ 
				_CreateDate = value;
				OnPropertyChanged("CreateDate");
			}
		}
		[JsonProperty]
		public long UpdateDate
		{
			get { return _UpdateDate; }
			private set 
			{ 
				_UpdateDate = value;
				OnPropertyChanged("UpdateDate");
			}
		}
		[JsonProperty]
		public bool? IsInherited
		{
			get { return _IsInherited; }
			set 
			{ 
				_IsInherited = value;
				OnPropertyChanged("IsInherited");
			}
		}
		#endregion

		#region CTor
		public AssetStructMeta()
		{
		}

		public AssetStructMeta(JToken node) : base(node)
		{
			if(node["assetStructId"] != null)
			{
				this._AssetStructId = ParseLong(node["assetStructId"].Value<string>());
			}
			if(node["metaId"] != null)
			{
				this._MetaId = ParseLong(node["metaId"].Value<string>());
			}
			if(node["ingestReferencePath"] != null)
			{
				this._IngestReferencePath = node["ingestReferencePath"].Value<string>();
			}
			if(node["protectFromIngest"] != null)
			{
				this._ProtectFromIngest = ParseBool(node["protectFromIngest"].Value<string>());
			}
			if(node["defaultIngestValue"] != null)
			{
				this._DefaultIngestValue = node["defaultIngestValue"].Value<string>();
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
			if(node["isInherited"] != null)
			{
				this._IsInherited = ParseBool(node["isInherited"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetStructMeta");
			kparams.AddIfNotNull("assetStructId", this._AssetStructId);
			kparams.AddIfNotNull("metaId", this._MetaId);
			kparams.AddIfNotNull("ingestReferencePath", this._IngestReferencePath);
			kparams.AddIfNotNull("protectFromIngest", this._ProtectFromIngest);
			kparams.AddIfNotNull("defaultIngestValue", this._DefaultIngestValue);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("isInherited", this._IsInherited);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_STRUCT_ID:
					return "AssetStructId";
				case META_ID:
					return "MetaId";
				case INGEST_REFERENCE_PATH:
					return "IngestReferencePath";
				case PROTECT_FROM_INGEST:
					return "ProtectFromIngest";
				case DEFAULT_INGEST_VALUE:
					return "DefaultIngestValue";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				case IS_INHERITED:
					return "IsInherited";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

