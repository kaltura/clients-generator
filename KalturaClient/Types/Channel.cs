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
		#endregion

		#region Properties
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
		public string OldName
		{
			get { return _OldName; }
			set 
			{ 
				_OldName = value;
				OnPropertyChanged("OldName");
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
		public string OldDescription
		{
			get { return _OldDescription; }
			set 
			{ 
				_OldDescription = value;
				OnPropertyChanged("OldDescription");
			}
		}
		public bool? IsActive
		{
			get { return _IsActive; }
			set 
			{ 
				_IsActive = value;
				OnPropertyChanged("IsActive");
			}
		}
		public ChannelOrder OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
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
		public bool? SupportSegmentBasedOrdering
		{
			get { return _SupportSegmentBasedOrdering; }
			set 
			{ 
				_SupportSegmentBasedOrdering = value;
				OnPropertyChanged("SupportSegmentBasedOrdering");
			}
		}
		#endregion

		#region CTor
		public Channel()
		{
		}

		public Channel(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
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
					case "oldName":
						this._OldName = propertyNode.InnerText;
						continue;
					case "systemName":
						this._SystemName = propertyNode.InnerText;
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
					case "oldDescription":
						this._OldDescription = propertyNode.InnerText;
						continue;
					case "isActive":
						this._IsActive = ParseBool(propertyNode.InnerText);
						continue;
					case "orderBy":
						this._OrderBy = ObjectFactory.Create<ChannelOrder>(propertyNode);
						continue;
					case "createDate":
						this._CreateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "updateDate":
						this._UpdateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "supportSegmentBasedOrdering":
						this._SupportSegmentBasedOrdering = ParseBool(propertyNode.InnerText);
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
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

