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
// Copyright (C) 2006-2017  Kaltura Inc.
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
		public string Description
		{
			get { return _Description; }
			set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		public int Order
		{
			get { return _Order; }
			set 
			{ 
				_Order = value;
				OnPropertyChanged("Order");
			}
		}
		public int MediaTag
		{
			get { return _MediaTag; }
			set 
			{ 
				_MediaTag = value;
				OnPropertyChanged("MediaTag");
			}
		}
		public int EpgTag
		{
			get { return _EpgTag; }
			set 
			{ 
				_EpgTag = value;
				OnPropertyChanged("EpgTag");
			}
		}
		public bool? BlockAnonymousAccess
		{
			get { return _BlockAnonymousAccess; }
			set 
			{ 
				_BlockAnonymousAccess = value;
				OnPropertyChanged("BlockAnonymousAccess");
			}
		}
		public ParentalRuleType RuleType
		{
			get { return _RuleType; }
			set 
			{ 
				_RuleType = value;
				OnPropertyChanged("RuleType");
			}
		}
		public IList<StringValue> MediaTagValues
		{
			get { return _MediaTagValues; }
			set 
			{ 
				_MediaTagValues = value;
				OnPropertyChanged("MediaTagValues");
			}
		}
		public IList<StringValue> EpgTagValues
		{
			get { return _EpgTagValues; }
			set 
			{ 
				_EpgTagValues = value;
				OnPropertyChanged("EpgTagValues");
			}
		}
		public bool? IsDefault
		{
			get { return _IsDefault; }
			set 
			{ 
				_IsDefault = value;
				OnPropertyChanged("IsDefault");
			}
		}
		public RuleLevel Origin
		{
			get { return _Origin; }
			set 
			{ 
				_Origin = value;
				OnPropertyChanged("Origin");
			}
		}
		#endregion

		#region CTor
		public ParentalRule()
		{
		}

		public ParentalRule(XmlElement node) : base(node)
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
					case "description":
						this._Description = propertyNode.InnerText;
						continue;
					case "order":
						this._Order = ParseInt(propertyNode.InnerText);
						continue;
					case "mediaTag":
						this._MediaTag = ParseInt(propertyNode.InnerText);
						continue;
					case "epgTag":
						this._EpgTag = ParseInt(propertyNode.InnerText);
						continue;
					case "blockAnonymousAccess":
						this._BlockAnonymousAccess = ParseBool(propertyNode.InnerText);
						continue;
					case "ruleType":
						this._RuleType = (ParentalRuleType)StringEnum.Parse(typeof(ParentalRuleType), propertyNode.InnerText);
						continue;
					case "mediaTagValues":
						this._MediaTagValues = new List<StringValue>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._MediaTagValues.Add(ObjectFactory.Create<StringValue>(arrayNode));
						}
						continue;
					case "epgTagValues":
						this._EpgTagValues = new List<StringValue>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._EpgTagValues.Add(ObjectFactory.Create<StringValue>(arrayNode));
						}
						continue;
					case "isDefault":
						this._IsDefault = ParseBool(propertyNode.InnerText);
						continue;
					case "origin":
						this._Origin = (RuleLevel)StringEnum.Parse(typeof(RuleLevel), propertyNode.InnerText);
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
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

