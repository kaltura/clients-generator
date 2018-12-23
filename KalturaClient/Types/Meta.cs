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
	public class Meta : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string MULTILINGUAL_NAME = "multilingualName";
		public const string SYSTEM_NAME = "systemName";
		public const string DATA_TYPE = "dataType";
		public const string MULTIPLE_VALUE = "multipleValue";
		public const string IS_PROTECTED = "isProtected";
		public const string HELP_TEXT = "helpText";
		public const string FEATURES = "features";
		public const string PARENT_ID = "parentId";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Name = null;
		private IList<TranslationToken> _MultilingualName;
		private string _SystemName = null;
		private MetaDataType _DataType = null;
		private bool? _MultipleValue = null;
		private bool? _IsProtected = null;
		private string _HelpText = null;
		private string _Features = null;
		private string _ParentId = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		#endregion

		#region Properties
		public string Id
		{
			get { return _Id; }
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
		public string SystemName
		{
			get { return _SystemName; }
			set 
			{ 
				_SystemName = value;
				OnPropertyChanged("SystemName");
			}
		}
		public MetaDataType DataType
		{
			get { return _DataType; }
			set 
			{ 
				_DataType = value;
				OnPropertyChanged("DataType");
			}
		}
		public bool? MultipleValue
		{
			get { return _MultipleValue; }
			set 
			{ 
				_MultipleValue = value;
				OnPropertyChanged("MultipleValue");
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
		public string HelpText
		{
			get { return _HelpText; }
			set 
			{ 
				_HelpText = value;
				OnPropertyChanged("HelpText");
			}
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
		public string ParentId
		{
			get { return _ParentId; }
			set 
			{ 
				_ParentId = value;
				OnPropertyChanged("ParentId");
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
		#endregion

		#region CTor
		public Meta()
		{
		}

		public Meta(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
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
					case "dataType":
						this._DataType = (MetaDataType)StringEnum.Parse(typeof(MetaDataType), propertyNode.InnerText);
						continue;
					case "multipleValue":
						this._MultipleValue = ParseBool(propertyNode.InnerText);
						continue;
					case "isProtected":
						this._IsProtected = ParseBool(propertyNode.InnerText);
						continue;
					case "helpText":
						this._HelpText = propertyNode.InnerText;
						continue;
					case "features":
						this._Features = propertyNode.InnerText;
						continue;
					case "parentId":
						this._ParentId = propertyNode.InnerText;
						continue;
					case "createDate":
						this._CreateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "updateDate":
						this._UpdateDate = ParseLong(propertyNode.InnerText);
						continue;
				}
			}
		}

		public Meta(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<string>("id");
			    this._Name = data.TryGetValueSafe<string>("name");
			    this._MultilingualName = new List<TranslationToken>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("multilingualName", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._MultilingualName.Add(ObjectFactory.Create<TranslationToken>((IDictionary<string,object>)dataDictionary));
			    }
			    this._SystemName = data.TryGetValueSafe<string>("systemName");
			    this._DataType = (MetaDataType)StringEnum.Parse(typeof(MetaDataType), data.TryGetValueSafe<string>("dataType"));
			    this._MultipleValue = data.TryGetValueSafe<bool>("multipleValue");
			    this._IsProtected = data.TryGetValueSafe<bool>("isProtected");
			    this._HelpText = data.TryGetValueSafe<string>("helpText");
			    this._Features = data.TryGetValueSafe<string>("features");
			    this._ParentId = data.TryGetValueSafe<string>("parentId");
			    this._CreateDate = data.TryGetValueSafe<long>("createDate");
			    this._UpdateDate = data.TryGetValueSafe<long>("updateDate");
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaMeta");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("multilingualName", this._MultilingualName);
			kparams.AddIfNotNull("systemName", this._SystemName);
			kparams.AddIfNotNull("dataType", this._DataType);
			kparams.AddIfNotNull("multipleValue", this._MultipleValue);
			kparams.AddIfNotNull("isProtected", this._IsProtected);
			kparams.AddIfNotNull("helpText", this._HelpText);
			kparams.AddIfNotNull("features", this._Features);
			kparams.AddIfNotNull("parentId", this._ParentId);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
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
				case DATA_TYPE:
					return "DataType";
				case MULTIPLE_VALUE:
					return "MultipleValue";
				case IS_PROTECTED:
					return "IsProtected";
				case HELP_TEXT:
					return "HelpText";
				case FEATURES:
					return "Features";
				case PARENT_ID:
					return "ParentId";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

