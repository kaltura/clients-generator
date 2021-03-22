// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
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
		public const string DYNAMIC_DATA = "dynamicData";
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
		private IDictionary<string, StringValue> _DynamicData;
		#endregion

		#region Properties
		[JsonProperty]
		public string Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
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
		public string SystemName
		{
			get { return _SystemName; }
			set 
			{ 
				_SystemName = value;
				OnPropertyChanged("SystemName");
			}
		}
		[JsonProperty]
		public MetaDataType DataType
		{
			get { return _DataType; }
			set 
			{ 
				_DataType = value;
				OnPropertyChanged("DataType");
			}
		}
		[JsonProperty]
		public bool? MultipleValue
		{
			get { return _MultipleValue; }
			set 
			{ 
				_MultipleValue = value;
				OnPropertyChanged("MultipleValue");
			}
		}
		[JsonProperty]
		public bool? IsProtected
		{
			get { return _IsProtected; }
			set 
			{ 
				_IsProtected = value;
				OnPropertyChanged("IsProtected");
			}
		}
		[JsonProperty]
		public string HelpText
		{
			get { return _HelpText; }
			set 
			{ 
				_HelpText = value;
				OnPropertyChanged("HelpText");
			}
		}
		[JsonProperty]
		public string Features
		{
			get { return _Features; }
			set 
			{ 
				_Features = value;
				OnPropertyChanged("Features");
			}
		}
		[JsonProperty]
		public string ParentId
		{
			get { return _ParentId; }
			set 
			{ 
				_ParentId = value;
				OnPropertyChanged("ParentId");
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
		public IDictionary<string, StringValue> DynamicData
		{
			get { return _DynamicData; }
			set 
			{ 
				_DynamicData = value;
				OnPropertyChanged("DynamicData");
			}
		}
		#endregion

		#region CTor
		public Meta()
		{
		}

		public Meta(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
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
			if(node["systemName"] != null)
			{
				this._SystemName = node["systemName"].Value<string>();
			}
			if(node["dataType"] != null)
			{
				this._DataType = (MetaDataType)StringEnum.Parse(typeof(MetaDataType), node["dataType"].Value<string>());
			}
			if(node["multipleValue"] != null)
			{
				this._MultipleValue = ParseBool(node["multipleValue"].Value<string>());
			}
			if(node["isProtected"] != null)
			{
				this._IsProtected = ParseBool(node["isProtected"].Value<string>());
			}
			if(node["helpText"] != null)
			{
				this._HelpText = node["helpText"].Value<string>();
			}
			if(node["features"] != null)
			{
				this._Features = node["features"].Value<string>();
			}
			if(node["parentId"] != null)
			{
				this._ParentId = node["parentId"].Value<string>();
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
			if(node["dynamicData"] != null)
			{
				{
					string key;
					this._DynamicData = new Dictionary<string, StringValue>();
					foreach(var arrayNode in node["dynamicData"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._DynamicData[key] = ObjectFactory.Create<StringValue>(arrayNode.Value);
					}
				}
			}
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
			kparams.AddIfNotNull("dynamicData", this._DynamicData);
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
				case DYNAMIC_DATA:
					return "DynamicData";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

