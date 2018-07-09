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
	public class AssetStruct : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string MULTILINGUAL_NAME = "multilingualName";
		public const string SYSTEM_NAME = "systemName";
		public const string IS_PROTECTED = "isProtected";
		public const string META_IDS = "metaIds";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		public const string FEATURES = "features";
		public const string PLURAL_NAME = "pluralName";
		public const string PARENT_ID = "parentId";
		public const string CONNECTING_META_ID = "connectingMetaId";
		public const string CONNECTED_PARENT_META_ID = "connectedParentMetaId";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private IList<TranslationToken> _MultilingualName;
		private string _SystemName = null;
		private bool? _IsProtected = null;
		private string _MetaIds = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		private string _Features = null;
		private string _PluralName = null;
		private long _ParentId = long.MinValue;
		private long _ConnectingMetaId = long.MinValue;
		private long _ConnectedParentMetaId = long.MinValue;
		#endregion

		#region Properties
		public long Id
		{
			get { return _Id; }
		}
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
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
		public string SystemName
		{
			get { return _SystemName; }
			set 
			{ 
				_SystemName = value;
				OnPropertyChanged("SystemName");
			}
		}
		public bool? IsProtected
		{
			get { return _IsProtected; }
			set 
			{ 
				_IsProtected = value;
				OnPropertyChanged("IsProtected");
			}
		}
		public string MetaIds
		{
			get { return _MetaIds; }
			set 
			{ 
				_MetaIds = value;
				OnPropertyChanged("MetaIds");
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
		public string Features
		{
			get { return _Features; }
			set 
			{ 
				_Features = value;
				OnPropertyChanged("Features");
			}
		}
		public string PluralName
		{
			get { return _PluralName; }
			set 
			{ 
				_PluralName = value;
				OnPropertyChanged("PluralName");
			}
		}
		public long ParentId
		{
			get { return _ParentId; }
			set 
			{ 
				_ParentId = value;
				OnPropertyChanged("ParentId");
			}
		}
		public long ConnectingMetaId
		{
			get { return _ConnectingMetaId; }
			set 
			{ 
				_ConnectingMetaId = value;
				OnPropertyChanged("ConnectingMetaId");
			}
		}
		public long ConnectedParentMetaId
		{
			get { return _ConnectedParentMetaId; }
			set 
			{ 
				_ConnectedParentMetaId = value;
				OnPropertyChanged("ConnectedParentMetaId");
			}
		}
		#endregion

		#region CTor
		public AssetStruct()
		{
		}

		public AssetStruct(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseLong(propertyNode.InnerText);
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
					case "systemName":
						this._SystemName = propertyNode.InnerText;
						continue;
					case "isProtected":
						this._IsProtected = ParseBool(propertyNode.InnerText);
						continue;
					case "metaIds":
						this._MetaIds = propertyNode.InnerText;
						continue;
					case "createDate":
						this._CreateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "updateDate":
						this._UpdateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "features":
						this._Features = propertyNode.InnerText;
						continue;
					case "pluralName":
						this._PluralName = propertyNode.InnerText;
						continue;
					case "parentId":
						this._ParentId = ParseLong(propertyNode.InnerText);
						continue;
					case "connectingMetaId":
						this._ConnectingMetaId = ParseLong(propertyNode.InnerText);
						continue;
					case "connectedParentMetaId":
						this._ConnectedParentMetaId = ParseLong(propertyNode.InnerText);
						continue;
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetStruct");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("multilingualName", this._MultilingualName);
			kparams.AddIfNotNull("systemName", this._SystemName);
			kparams.AddIfNotNull("isProtected", this._IsProtected);
			kparams.AddIfNotNull("metaIds", this._MetaIds);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("features", this._Features);
			kparams.AddIfNotNull("pluralName", this._PluralName);
			kparams.AddIfNotNull("parentId", this._ParentId);
			kparams.AddIfNotNull("connectingMetaId", this._ConnectingMetaId);
			kparams.AddIfNotNull("connectedParentMetaId", this._ConnectedParentMetaId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case NAME:
					return "Name";
				case MULTILINGUAL_NAME:
					return "MultilingualName";
				case SYSTEM_NAME:
					return "SystemName";
				case IS_PROTECTED:
					return "IsProtected";
				case META_IDS:
					return "MetaIds";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				case FEATURES:
					return "Features";
				case PLURAL_NAME:
					return "PluralName";
				case PARENT_ID:
					return "ParentId";
				case CONNECTING_META_ID:
					return "ConnectingMetaId";
				case CONNECTED_PARENT_META_ID:
					return "ConnectedParentMetaId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

