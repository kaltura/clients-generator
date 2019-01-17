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
		public const string START_DATE = "startDate";
		public const string END_DATE = "endDate";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		public const string EXTERNAL_ID = "externalId";
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
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		private string _ExternalId = null;
		#endregion

		#region Properties
		public long Id
		{
			get { return _Id; }
		}
		public int Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		public string Name
		{
			get { return _Name; }
		}
		public IList<TranslationToken> MultilingualName
		{
			get { return _MultilingualName; }
			set 
			{ 
				_MultilingualName = value;
				OnPropertyChanged("MultilingualName");
			}
		}
		public string Description
		{
			get { return _Description; }
		}
		public IList<TranslationToken> MultilingualDescription
		{
			get { return _MultilingualDescription; }
			set 
			{ 
				_MultilingualDescription = value;
				OnPropertyChanged("MultilingualDescription");
			}
		}
		public IList<MediaImage> Images
		{
			get { return _Images; }
		}
		public IList<MediaFile> MediaFiles
		{
			get { return _MediaFiles; }
		}
		public IDictionary<string, Value> Metas
		{
			get { return _Metas; }
			set 
			{ 
				_Metas = value;
				OnPropertyChanged("Metas");
			}
		}
		public IDictionary<string, MultilingualStringValueArray> Tags
		{
			get { return _Tags; }
			set 
			{ 
				_Tags = value;
				OnPropertyChanged("Tags");
			}
		}
		public long StartDate
		{
			get { return _StartDate; }
			set 
			{ 
				_StartDate = value;
				OnPropertyChanged("StartDate");
			}
		}
		public long EndDate
		{
			get { return _EndDate; }
			set 
			{ 
				_EndDate = value;
				OnPropertyChanged("EndDate");
			}
		}
		public long CreateDate
		{
			get { return _CreateDate; }
		}
		public long UpdateDate
		{
			get { return _UpdateDate; }
		}
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		#endregion

		#region CTor
		public Asset()
		{
		}

		public Asset(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseLong(propertyNode.InnerText);
						continue;
					case "type":
						this._Type = ParseInt(propertyNode.InnerText);
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "multilingualName":
						this._MultilingualName = new List<TranslationToken>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._MultilingualName.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
						}
						continue;
					case "description":
						this._Description = propertyNode.InnerText;
						continue;
					case "multilingualDescription":
						this._MultilingualDescription = new List<TranslationToken>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._MultilingualDescription.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
						}
						continue;
					case "images":
						this._Images = new List<MediaImage>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Images.Add(ObjectFactory.Create<MediaImage>(arrayNode));
						}
						continue;
					case "mediaFiles":
						this._MediaFiles = new List<MediaFile>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._MediaFiles.Add(ObjectFactory.Create<MediaFile>(arrayNode));
						}
						continue;
					case "metas":
						{
							string key;
							this._Metas = new Dictionary<string, Value>();
							foreach(XmlElement arrayNode in propertyNode.ChildNodes)
							{
								key = arrayNode["itemKey"].InnerText;;
								this._Metas[key] = ObjectFactory.Create<Value>(arrayNode);
							}
						}
						continue;
					case "tags":
						{
							string key;
							this._Tags = new Dictionary<string, MultilingualStringValueArray>();
							foreach(XmlElement arrayNode in propertyNode.ChildNodes)
							{
								key = arrayNode["itemKey"].InnerText;;
								this._Tags[key] = ObjectFactory.Create<MultilingualStringValueArray>(arrayNode);
							}
						}
						continue;
					case "startDate":
						this._StartDate = ParseLong(propertyNode.InnerText);
						continue;
					case "endDate":
						this._EndDate = ParseLong(propertyNode.InnerText);
						continue;
					case "createDate":
						this._CreateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "updateDate":
						this._UpdateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "externalId":
						this._ExternalId = propertyNode.InnerText;
						continue;
				}
			}
		}

		public Asset(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<long>("id");
			    this._Type = data.TryGetValueSafe<int>("type");
			    this._Name = data.TryGetValueSafe<string>("name");
			    this._MultilingualName = new List<TranslationToken>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("multilingualName", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._MultilingualName.Add(ObjectFactory.Create<TranslationToken>((IDictionary<string,object>)dataDictionary));
			    }
			    this._Description = data.TryGetValueSafe<string>("description");
			    this._MultilingualDescription = new List<TranslationToken>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("multilingualDescription", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._MultilingualDescription.Add(ObjectFactory.Create<TranslationToken>((IDictionary<string,object>)dataDictionary));
			    }
			    this._Images = new List<MediaImage>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("images", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._Images.Add(ObjectFactory.Create<MediaImage>((IDictionary<string,object>)dataDictionary));
			    }
			    this._MediaFiles = new List<MediaFile>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("mediaFiles", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._MediaFiles.Add(ObjectFactory.Create<MediaFile>((IDictionary<string,object>)dataDictionary));
			    }
			    this._Metas = new Dictionary<string, Value>();
			    foreach(var keyValuePair in data.TryGetValueSafe("metas", new Dictionary<string, object>()))
			    {
			        this._Metas[keyValuePair.Key] = ObjectFactory.Create<Value>((IDictionary<string,object>)keyValuePair.Value);
				}
			    this._Tags = new Dictionary<string, MultilingualStringValueArray>();
			    foreach(var keyValuePair in data.TryGetValueSafe("tags", new Dictionary<string, object>()))
			    {
			        this._Tags[keyValuePair.Key] = ObjectFactory.Create<MultilingualStringValueArray>((IDictionary<string,object>)keyValuePair.Value);
				}
			    this._StartDate = data.TryGetValueSafe<long>("startDate");
			    this._EndDate = data.TryGetValueSafe<long>("endDate");
			    this._CreateDate = data.TryGetValueSafe<long>("createDate");
			    this._UpdateDate = data.TryGetValueSafe<long>("updateDate");
			    this._ExternalId = data.TryGetValueSafe<string>("externalId");
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
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("externalId", this._ExternalId);
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
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

