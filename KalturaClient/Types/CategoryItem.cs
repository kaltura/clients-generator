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
	public class CategoryItem : CrudObject
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string MULTILINGUAL_NAME = "multilingualName";
		public const string PARENT_ID = "parentId";
		public const string CHILDREN_IDS = "childrenIds";
		public const string UNIFIED_CHANNELS = "unifiedChannels";
		public const string DYNAMIC_DATA = "dynamicData";
		public const string UPDATE_DATE = "updateDate";
		public const string IS_ACTIVE = "isActive";
		public const string START_DATE_IN_SECONDS = "startDateInSeconds";
		public const string END_DATE_IN_SECONDS = "endDateInSeconds";
		public const string TYPE = "type";
		public const string VIRTUAL_ASSET_ID = "virtualAssetId";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private IList<TranslationToken> _MultilingualName;
		private long _ParentId = long.MinValue;
		private string _ChildrenIds = null;
		private IList<UnifiedChannel> _UnifiedChannels;
		private IDictionary<string, StringValue> _DynamicData;
		private long _UpdateDate = long.MinValue;
		private bool? _IsActive = null;
		private long _StartDateInSeconds = long.MinValue;
		private long _EndDateInSeconds = long.MinValue;
		private string _Type = null;
		private long _VirtualAssetId = long.MinValue;
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
		public long ParentId
		{
			get { return _ParentId; }
			private set 
			{ 
				_ParentId = value;
				OnPropertyChanged("ParentId");
			}
		}
		[JsonProperty]
		public string ChildrenIds
		{
			get { return _ChildrenIds; }
			set 
			{ 
				_ChildrenIds = value;
				OnPropertyChanged("ChildrenIds");
			}
		}
		[JsonProperty]
		public IList<UnifiedChannel> UnifiedChannels
		{
			get { return _UnifiedChannels; }
			set 
			{ 
				_UnifiedChannels = value;
				OnPropertyChanged("UnifiedChannels");
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
		public bool? IsActive
		{
			get { return _IsActive; }
			set 
			{ 
				_IsActive = value;
				OnPropertyChanged("IsActive");
			}
		}
		[JsonProperty]
		public long StartDateInSeconds
		{
			get { return _StartDateInSeconds; }
			set 
			{ 
				_StartDateInSeconds = value;
				OnPropertyChanged("StartDateInSeconds");
			}
		}
		[JsonProperty]
		public long EndDateInSeconds
		{
			get { return _EndDateInSeconds; }
			set 
			{ 
				_EndDateInSeconds = value;
				OnPropertyChanged("EndDateInSeconds");
			}
		}
		[JsonProperty]
		public string Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		[JsonProperty]
		public long VirtualAssetId
		{
			get { return _VirtualAssetId; }
			private set 
			{ 
				_VirtualAssetId = value;
				OnPropertyChanged("VirtualAssetId");
			}
		}
		#endregion

		#region CTor
		public CategoryItem()
		{
		}

		public CategoryItem(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
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
			if(node["parentId"] != null)
			{
				this._ParentId = ParseLong(node["parentId"].Value<string>());
			}
			if(node["childrenIds"] != null)
			{
				this._ChildrenIds = node["childrenIds"].Value<string>();
			}
			if(node["unifiedChannels"] != null)
			{
				this._UnifiedChannels = new List<UnifiedChannel>();
				foreach(var arrayNode in node["unifiedChannels"].Children())
				{
					this._UnifiedChannels.Add(ObjectFactory.Create<UnifiedChannel>(arrayNode));
				}
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
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
			if(node["isActive"] != null)
			{
				this._IsActive = ParseBool(node["isActive"].Value<string>());
			}
			if(node["startDateInSeconds"] != null)
			{
				this._StartDateInSeconds = ParseLong(node["startDateInSeconds"].Value<string>());
			}
			if(node["endDateInSeconds"] != null)
			{
				this._EndDateInSeconds = ParseLong(node["endDateInSeconds"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = node["type"].Value<string>();
			}
			if(node["virtualAssetId"] != null)
			{
				this._VirtualAssetId = ParseLong(node["virtualAssetId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCategoryItem");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("multilingualName", this._MultilingualName);
			kparams.AddIfNotNull("parentId", this._ParentId);
			kparams.AddIfNotNull("childrenIds", this._ChildrenIds);
			kparams.AddIfNotNull("unifiedChannels", this._UnifiedChannels);
			kparams.AddIfNotNull("dynamicData", this._DynamicData);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("isActive", this._IsActive);
			kparams.AddIfNotNull("startDateInSeconds", this._StartDateInSeconds);
			kparams.AddIfNotNull("endDateInSeconds", this._EndDateInSeconds);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("virtualAssetId", this._VirtualAssetId);
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
				case PARENT_ID:
					return "ParentId";
				case CHILDREN_IDS:
					return "ChildrenIds";
				case UNIFIED_CHANNELS:
					return "UnifiedChannels";
				case DYNAMIC_DATA:
					return "DynamicData";
				case UPDATE_DATE:
					return "UpdateDate";
				case IS_ACTIVE:
					return "IsActive";
				case START_DATE_IN_SECONDS:
					return "StartDateInSeconds";
				case END_DATE_IN_SECONDS:
					return "EndDateInSeconds";
				case TYPE:
					return "Type";
				case VIRTUAL_ASSET_ID:
					return "VirtualAssetId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

