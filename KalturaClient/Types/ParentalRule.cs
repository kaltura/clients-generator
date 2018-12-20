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
	public class ParentalRule : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string DESCRIPTION = "description";
		public const string ORDER = "order";
		public const string MEDIA_TAG = "mediaTag";
		public const string EPG_TAG = "epgTag";
		public const string BLOCK_ANONYMOUS_ACCESS = "blockAnonymousAccess";
		public const string RULE_TYPE = "ruleType";
		public const string MEDIA_TAG_VALUES = "mediaTagValues";
		public const string EPG_TAG_VALUES = "epgTagValues";
		public const string IS_DEFAULT = "isDefault";
		public const string ORIGIN = "origin";
		public const string IS_ACTIVE = "isActive";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private string _Description = null;
		private int _Order = Int32.MinValue;
		private int _MediaTag = Int32.MinValue;
		private int _EpgTag = Int32.MinValue;
		private bool? _BlockAnonymousAccess = null;
		private ParentalRuleType _RuleType = null;
		private IList<StringValue> _MediaTagValues;
		private IList<StringValue> _EpgTagValues;
		private bool? _IsDefault = null;
		private RuleLevel _Origin = null;
		private bool? _IsActive = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
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
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public string Description
		{
			get { return _Description; }
			set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		[JsonProperty]
		public int Order
		{
			get { return _Order; }
			set 
			{ 
				_Order = value;
				OnPropertyChanged("Order");
			}
		}
		[JsonProperty]
		public int MediaTag
		{
			get { return _MediaTag; }
			set 
			{ 
				_MediaTag = value;
				OnPropertyChanged("MediaTag");
			}
		}
		[JsonProperty]
		public int EpgTag
		{
			get { return _EpgTag; }
			set 
			{ 
				_EpgTag = value;
				OnPropertyChanged("EpgTag");
			}
		}
		[JsonProperty]
		public bool? BlockAnonymousAccess
		{
			get { return _BlockAnonymousAccess; }
			set 
			{ 
				_BlockAnonymousAccess = value;
				OnPropertyChanged("BlockAnonymousAccess");
			}
		}
		[JsonProperty]
		public ParentalRuleType RuleType
		{
			get { return _RuleType; }
			set 
			{ 
				_RuleType = value;
				OnPropertyChanged("RuleType");
			}
		}
		[JsonProperty]
		public IList<StringValue> MediaTagValues
		{
			get { return _MediaTagValues; }
			set 
			{ 
				_MediaTagValues = value;
				OnPropertyChanged("MediaTagValues");
			}
		}
		[JsonProperty]
		public IList<StringValue> EpgTagValues
		{
			get { return _EpgTagValues; }
			set 
			{ 
				_EpgTagValues = value;
				OnPropertyChanged("EpgTagValues");
			}
		}
		[JsonProperty]
		public bool? IsDefault
		{
			get { return _IsDefault; }
			private set 
			{ 
				_IsDefault = value;
				OnPropertyChanged("IsDefault");
			}
		}
		[JsonProperty]
		public RuleLevel Origin
		{
			get { return _Origin; }
			private set 
			{ 
				_Origin = value;
				OnPropertyChanged("Origin");
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
		#endregion

		#region CTor
		public ParentalRule()
		{
		}

		public ParentalRule(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["description"] != null)
			{
				this._Description = node["description"].Value<string>();
			}
			if(node["order"] != null)
			{
				this._Order = ParseInt(node["order"].Value<string>());
			}
			if(node["mediaTag"] != null)
			{
				this._MediaTag = ParseInt(node["mediaTag"].Value<string>());
			}
			if(node["epgTag"] != null)
			{
				this._EpgTag = ParseInt(node["epgTag"].Value<string>());
			}
			if(node["blockAnonymousAccess"] != null)
			{
				this._BlockAnonymousAccess = ParseBool(node["blockAnonymousAccess"].Value<string>());
			}
			if(node["ruleType"] != null)
			{
				this._RuleType = (ParentalRuleType)StringEnum.Parse(typeof(ParentalRuleType), node["ruleType"].Value<string>());
			}
			if(node["mediaTagValues"] != null)
			{
				this._MediaTagValues = new List<StringValue>();
				foreach(var arrayNode in node["mediaTagValues"].Children())
				{
					this._MediaTagValues.Add(ObjectFactory.Create<StringValue>(arrayNode));
				}
			}
			if(node["epgTagValues"] != null)
			{
				this._EpgTagValues = new List<StringValue>();
				foreach(var arrayNode in node["epgTagValues"].Children())
				{
					this._EpgTagValues.Add(ObjectFactory.Create<StringValue>(arrayNode));
				}
			}
			if(node["isDefault"] != null)
			{
				this._IsDefault = ParseBool(node["isDefault"].Value<string>());
			}
			if(node["origin"] != null)
			{
				this._Origin = (RuleLevel)StringEnum.Parse(typeof(RuleLevel), node["origin"].Value<string>());
			}
			if(node["isActive"] != null)
			{
				this._IsActive = ParseBool(node["isActive"].Value<string>());
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaParentalRule");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("order", this._Order);
			kparams.AddIfNotNull("mediaTag", this._MediaTag);
			kparams.AddIfNotNull("epgTag", this._EpgTag);
			kparams.AddIfNotNull("blockAnonymousAccess", this._BlockAnonymousAccess);
			kparams.AddIfNotNull("ruleType", this._RuleType);
			kparams.AddIfNotNull("mediaTagValues", this._MediaTagValues);
			kparams.AddIfNotNull("epgTagValues", this._EpgTagValues);
			kparams.AddIfNotNull("isDefault", this._IsDefault);
			kparams.AddIfNotNull("origin", this._Origin);
			kparams.AddIfNotNull("isActive", this._IsActive);
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
				case DESCRIPTION:
					return "Description";
				case ORDER:
					return "Order";
				case MEDIA_TAG:
					return "MediaTag";
				case EPG_TAG:
					return "EpgTag";
				case BLOCK_ANONYMOUS_ACCESS:
					return "BlockAnonymousAccess";
				case RULE_TYPE:
					return "RuleType";
				case MEDIA_TAG_VALUES:
					return "MediaTagValues";
				case EPG_TAG_VALUES:
					return "EpgTagValues";
				case IS_DEFAULT:
					return "IsDefault";
				case ORIGIN:
					return "Origin";
				case IS_ACTIVE:
					return "IsActive";
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

