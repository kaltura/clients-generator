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
using Kaltura.Enums;
using Kaltura.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class Asset : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string TYPE = "type";
		public const string NAME = "name";
		public const string MULTILINGUAL_NAME = "multilingualName";
		public const string DESCRIPTION = "description";
		public const string MULTILINGUAL_DESCRIPTION = "multilingualDescription";
		public const string IMAGES = "images";
		public const string MEDIA_FILES = "mediaFiles";
		public const string METAS = "metas";
		public const string TAGS = "tags";
		public const string RELATED_ENTITIES = "relatedEntities";
		public const string START_DATE = "startDate";
		public const string END_DATE = "endDate";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		public const string EXTERNAL_ID = "externalId";
		public const string INDEX_STATUS = "indexStatus";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private int _Type = Int32.MinValue;
		private string _Name = null;
		private IList<TranslationToken> _MultilingualName;
		private string _Description = null;
		private IList<TranslationToken> _MultilingualDescription;
		private IList<MediaImage> _Images;
		private IList<MediaFile> _MediaFiles;
		private IDictionary<string, Value> _Metas;
		private IDictionary<string, MultilingualStringValueArray> _Tags;
		private IDictionary<string, RelatedEntityArray> _RelatedEntities;
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		private string _ExternalId = null;
		private AssetIndexStatus _IndexStatus = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public int Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			private set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public IList<TranslationToken> MultilingualName
		{
			get { return _MultilingualName; }
			set 
			{ 
				_MultilingualName = value;
				OnPropertyChanged("MultilingualName");
			}
		}
		[JsonProperty]
		public string Description
		{
			get { return _Description; }
			private set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		[JsonProperty]
		public IList<TranslationToken> MultilingualDescription
		{
			get { return _MultilingualDescription; }
			set 
			{ 
				_MultilingualDescription = value;
				OnPropertyChanged("MultilingualDescription");
			}
		}
		[JsonProperty]
		public IList<MediaImage> Images
		{
			get { return _Images; }
			private set 
			{ 
				_Images = value;
				OnPropertyChanged("Images");
			}
		}
		[JsonProperty]
		public IList<MediaFile> MediaFiles
		{
			get { return _MediaFiles; }
			private set 
			{ 
				_MediaFiles = value;
				OnPropertyChanged("MediaFiles");
			}
		}
		[JsonProperty]
		public IDictionary<string, Value> Metas
		{
			get { return _Metas; }
			set 
			{ 
				_Metas = value;
				OnPropertyChanged("Metas");
			}
		}
		[JsonProperty]
		public IDictionary<string, MultilingualStringValueArray> Tags
		{
			get { return _Tags; }
			set 
			{ 
				_Tags = value;
				OnPropertyChanged("Tags");
			}
		}
		[JsonProperty]
		public IDictionary<string, RelatedEntityArray> RelatedEntities
		{
			get { return _RelatedEntities; }
			set 
			{ 
				_RelatedEntities = value;
				OnPropertyChanged("RelatedEntities");
			}
		}
		[JsonProperty]
		public long StartDate
		{
			get { return _StartDate; }
			set 
			{ 
				_StartDate = value;
				OnPropertyChanged("StartDate");
			}
		}
		[JsonProperty]
		public long EndDate
		{
			get { return _EndDate; }
			set 
			{ 
				_EndDate = value;
				OnPropertyChanged("EndDate");
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
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		[JsonProperty]
		public AssetIndexStatus IndexStatus
		{
			get { return _IndexStatus; }
			private set 
			{ 
				_IndexStatus = value;
				OnPropertyChanged("IndexStatus");
			}
		}
		#endregion

		#region CTor
		public Asset()
		{
		}

		public Asset(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = ParseInt(node["type"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["multilingualName"] != null)
			{
				this._MultilingualName = new List<TranslationToken>();
				foreach(var arrayNode in node["multilingualName"].Children())
				{
					this._MultilingualName.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
				}
			}
			if(node["description"] != null)
			{
				this._Description = node["description"].Value<string>();
			}
			if(node["multilingualDescription"] != null)
			{
				this._MultilingualDescription = new List<TranslationToken>();
				foreach(var arrayNode in node["multilingualDescription"].Children())
				{
					this._MultilingualDescription.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
				}
			}
			if(node["images"] != null)
			{
				this._Images = new List<MediaImage>();
				foreach(var arrayNode in node["images"].Children())
				{
					this._Images.Add(ObjectFactory.Create<MediaImage>(arrayNode));
				}
			}
			if(node["mediaFiles"] != null)
			{
				this._MediaFiles = new List<MediaFile>();
				foreach(var arrayNode in node["mediaFiles"].Children())
				{
					this._MediaFiles.Add(ObjectFactory.Create<MediaFile>(arrayNode));
				}
			}
			if(node["metas"] != null)
			{
				{
					string key;
					this._Metas = new Dictionary<string, Value>();
					foreach(var arrayNode in node["metas"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._Metas[key] = ObjectFactory.Create<Value>(arrayNode.Value);
					}
				}
			}
			if(node["tags"] != null)
			{
				{
					string key;
					this._Tags = new Dictionary<string, MultilingualStringValueArray>();
					foreach(var arrayNode in node["tags"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._Tags[key] = ObjectFactory.Create<MultilingualStringValueArray>(arrayNode.Value);
					}
				}
			}
			if(node["relatedEntities"] != null)
			{
				{
					string key;
					this._RelatedEntities = new Dictionary<string, RelatedEntityArray>();
					foreach(var arrayNode in node["relatedEntities"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._RelatedEntities[key] = ObjectFactory.Create<RelatedEntityArray>(arrayNode.Value);
					}
				}
			}
			if(node["startDate"] != null)
			{
				this._StartDate = ParseLong(node["startDate"].Value<string>());
			}
			if(node["endDate"] != null)
			{
				this._EndDate = ParseLong(node["endDate"].Value<string>());
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
			if(node["externalId"] != null)
			{
				this._ExternalId = node["externalId"].Value<string>();
			}
			if(node["indexStatus"] != null)
			{
				this._IndexStatus = (AssetIndexStatus)StringEnum.Parse(typeof(AssetIndexStatus), node["indexStatus"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAsset");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("multilingualName", this._MultilingualName);
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("multilingualDescription", this._MultilingualDescription);
			kparams.AddIfNotNull("images", this._Images);
			kparams.AddIfNotNull("mediaFiles", this._MediaFiles);
			kparams.AddIfNotNull("metas", this._Metas);
			kparams.AddIfNotNull("tags", this._Tags);
			kparams.AddIfNotNull("relatedEntities", this._RelatedEntities);
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("indexStatus", this._IndexStatus);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case TYPE:
					return "Type";
				case NAME:
					return "Name";
				case MULTILINGUAL_NAME:
					return "MultilingualName";
				case DESCRIPTION:
					return "Description";
				case MULTILINGUAL_DESCRIPTION:
					return "MultilingualDescription";
				case IMAGES:
					return "Images";
				case MEDIA_FILES:
					return "MediaFiles";
				case METAS:
					return "Metas";
				case TAGS:
					return "Tags";
				case RELATED_ENTITIES:
					return "RelatedEntities";
				case START_DATE:
					return "StartDate";
				case END_DATE:
					return "EndDate";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				case EXTERNAL_ID:
					return "ExternalId";
				case INDEX_STATUS:
					return "IndexStatus";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

