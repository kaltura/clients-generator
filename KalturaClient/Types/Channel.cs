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
// Copyright (C) 2006-2020  Kaltura Inc.
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
	public class Channel : BaseChannel
	{
		#region Constants
		public const string NAME = "name";
		public const string MULTILINGUAL_NAME = "multilingualName";
		public const string OLD_NAME = "oldName";
		public const string SYSTEM_NAME = "systemName";
		public const string DESCRIPTION = "description";
		public const string MULTILINGUAL_DESCRIPTION = "multilingualDescription";
		public const string OLD_DESCRIPTION = "oldDescription";
		public const string IS_ACTIVE = "isActive";
		public const string ORDER_BY = "orderBy";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		public const string SUPPORT_SEGMENT_BASED_ORDERING = "supportSegmentBasedOrdering";
		public const string ASSET_USER_RULE_ID = "assetUserRuleId";
		public const string META_DATA = "metaData";
		#endregion

		#region Private Fields
		private string _Name = null;
		private IList<TranslationToken> _MultilingualName;
		private string _OldName = null;
		private string _SystemName = null;
		private string _Description = null;
		private IList<TranslationToken> _MultilingualDescription;
		private string _OldDescription = null;
		private bool? _IsActive = null;
		private ChannelOrder _OrderBy;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		private bool? _SupportSegmentBasedOrdering = null;
		private long _AssetUserRuleId = long.MinValue;
		private IDictionary<string, StringValue> _MetaData;
		#endregion

		#region Properties
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
		public string OldName
		{
			get { return _OldName; }
			set 
			{ 
				_OldName = value;
				OnPropertyChanged("OldName");
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
		public string OldDescription
		{
			get { return _OldDescription; }
			set 
			{ 
				_OldDescription = value;
				OnPropertyChanged("OldDescription");
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
		public ChannelOrder OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
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
		public bool? SupportSegmentBasedOrdering
		{
			get { return _SupportSegmentBasedOrdering; }
			set 
			{ 
				_SupportSegmentBasedOrdering = value;
				OnPropertyChanged("SupportSegmentBasedOrdering");
			}
		}
		[JsonProperty]
		public long AssetUserRuleId
		{
			get { return _AssetUserRuleId; }
			set 
			{ 
				_AssetUserRuleId = value;
				OnPropertyChanged("AssetUserRuleId");
			}
		}
		[JsonProperty]
		public IDictionary<string, StringValue> MetaData
		{
			get { return _MetaData; }
			set 
			{ 
				_MetaData = value;
				OnPropertyChanged("MetaData");
			}
		}
		#endregion

		#region CTor
		public Channel()
		{
		}

		public Channel(JToken node) : base(node)
		{
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
			if(node["oldName"] != null)
			{
				this._OldName = node["oldName"].Value<string>();
			}
			if(node["systemName"] != null)
			{
				this._SystemName = node["systemName"].Value<string>();
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
			if(node["oldDescription"] != null)
			{
				this._OldDescription = node["oldDescription"].Value<string>();
			}
			if(node["isActive"] != null)
			{
				this._IsActive = ParseBool(node["isActive"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = ObjectFactory.Create<ChannelOrder>(node["orderBy"]);
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
			if(node["supportSegmentBasedOrdering"] != null)
			{
				this._SupportSegmentBasedOrdering = ParseBool(node["supportSegmentBasedOrdering"].Value<string>());
			}
			if(node["assetUserRuleId"] != null)
			{
				this._AssetUserRuleId = ParseLong(node["assetUserRuleId"].Value<string>());
			}
			if(node["metaData"] != null)
			{
				{
					string key;
					this._MetaData = new Dictionary<string, StringValue>();
					foreach(var arrayNode in node["metaData"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._MetaData[key] = ObjectFactory.Create<StringValue>(arrayNode.Value);
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
				kparams.AddReplace("objectType", "KalturaChannel");
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("multilingualName", this._MultilingualName);
			kparams.AddIfNotNull("oldName", this._OldName);
			kparams.AddIfNotNull("systemName", this._SystemName);
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("multilingualDescription", this._MultilingualDescription);
			kparams.AddIfNotNull("oldDescription", this._OldDescription);
			kparams.AddIfNotNull("isActive", this._IsActive);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("supportSegmentBasedOrdering", this._SupportSegmentBasedOrdering);
			kparams.AddIfNotNull("assetUserRuleId", this._AssetUserRuleId);
			kparams.AddIfNotNull("metaData", this._MetaData);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NAME:
					return "Name";
				case MULTILINGUAL_NAME:
					return "MultilingualName";
				case OLD_NAME:
					return "OldName";
				case SYSTEM_NAME:
					return "SystemName";
				case DESCRIPTION:
					return "Description";
				case MULTILINGUAL_DESCRIPTION:
					return "MultilingualDescription";
				case OLD_DESCRIPTION:
					return "OldDescription";
				case IS_ACTIVE:
					return "IsActive";
				case ORDER_BY:
					return "OrderBy";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				case SUPPORT_SEGMENT_BASED_ORDERING:
					return "SupportSegmentBasedOrdering";
				case ASSET_USER_RULE_ID:
					return "AssetUserRuleId";
				case META_DATA:
					return "MetaData";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

