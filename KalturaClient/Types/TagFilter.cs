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
	public class TagFilter : Filter
	{
		#region Constants
		public const string TAG_EQUAL = "tagEqual";
		public const string TAG_STARTS_WITH = "tagStartsWith";
		public const string TYPE_EQUAL = "typeEqual";
		public const string LANGUAGE_EQUAL = "languageEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _TagEqual = null;
		private string _TagStartsWith = null;
		private int _TypeEqual = Int32.MinValue;
		private string _LanguageEqual = null;
		private TagOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public string TagEqual
		{
			get { return _TagEqual; }
			set 
			{ 
				_TagEqual = value;
				OnPropertyChanged("TagEqual");
			}
		}
		public string TagStartsWith
		{
			get { return _TagStartsWith; }
			set 
			{ 
				_TagStartsWith = value;
				OnPropertyChanged("TagStartsWith");
			}
		}
		public int TypeEqual
		{
			get { return _TypeEqual; }
			set 
			{ 
				_TypeEqual = value;
				OnPropertyChanged("TypeEqual");
			}
		}
		public string LanguageEqual
		{
			get { return _LanguageEqual; }
			set 
			{ 
				_LanguageEqual = value;
				OnPropertyChanged("LanguageEqual");
			}
		}
		public new TagOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		#endregion

		#region CTor
		public TagFilter()
		{
		}

		public TagFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "tagEqual":
						this._TagEqual = propertyNode.InnerText;
						continue;
					case "tagStartsWith":
						this._TagStartsWith = propertyNode.InnerText;
						continue;
					case "typeEqual":
						this._TypeEqual = ParseInt(propertyNode.InnerText);
						continue;
					case "languageEqual":
						this._LanguageEqual = propertyNode.InnerText;
						continue;
					case "orderBy":
						this._OrderBy = (TagOrderBy)StringEnum.Parse(typeof(TagOrderBy), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaTagFilter");
			kparams.AddIfNotNull("tagEqual", this._TagEqual);
			kparams.AddIfNotNull("tagStartsWith", this._TagStartsWith);
			kparams.AddIfNotNull("typeEqual", this._TypeEqual);
			kparams.AddIfNotNull("languageEqual", this._LanguageEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case TAG_EQUAL:
					return "TagEqual";
				case TAG_STARTS_WITH:
					return "TagStartsWith";
				case TYPE_EQUAL:
					return "TypeEqual";
				case LANGUAGE_EQUAL:
					return "LanguageEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

