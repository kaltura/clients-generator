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
// Copyright (C) 2006-2019  Kaltura Inc.
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
	public class TagFilter : Filter
	{
		#region Constants
		public const string TAG_EQUAL = "tagEqual";
		public const string TAG_STARTS_WITH = "tagStartsWith";
		public const string TYPE_EQUAL = "typeEqual";
		public const string LANGUAGE_EQUAL = "languageEqual";
		public const string ID_IN = "idIn";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _TagEqual = null;
		private string _TagStartsWith = null;
		private int _TypeEqual = Int32.MinValue;
		private string _LanguageEqual = null;
		private string _IdIn = null;
		private TagOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string TagEqual
		{
			get { return _TagEqual; }
			set 
			{ 
				_TagEqual = value;
				OnPropertyChanged("TagEqual");
			}
		}
		[JsonProperty]
		public string TagStartsWith
		{
			get { return _TagStartsWith; }
			set 
			{ 
				_TagStartsWith = value;
				OnPropertyChanged("TagStartsWith");
			}
		}
		[JsonProperty]
		public int TypeEqual
		{
			get { return _TypeEqual; }
			set 
			{ 
				_TypeEqual = value;
				OnPropertyChanged("TypeEqual");
			}
		}
		[JsonProperty]
		public string LanguageEqual
		{
			get { return _LanguageEqual; }
			set 
			{ 
				_LanguageEqual = value;
				OnPropertyChanged("LanguageEqual");
			}
		}
		[JsonProperty]
		public string IdIn
		{
			get { return _IdIn; }
			set 
			{ 
				_IdIn = value;
				OnPropertyChanged("IdIn");
			}
		}
		[JsonProperty]
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

		public TagFilter(JToken node) : base(node)
		{
			if(node["tagEqual"] != null)
			{
				this._TagEqual = node["tagEqual"].Value<string>();
			}
			if(node["tagStartsWith"] != null)
			{
				this._TagStartsWith = node["tagStartsWith"].Value<string>();
			}
			if(node["typeEqual"] != null)
			{
				this._TypeEqual = ParseInt(node["typeEqual"].Value<string>());
			}
			if(node["languageEqual"] != null)
			{
				this._LanguageEqual = node["languageEqual"].Value<string>();
			}
			if(node["idIn"] != null)
			{
				this._IdIn = node["idIn"].Value<string>();
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (TagOrderBy)StringEnum.Parse(typeof(TagOrderBy), node["orderBy"].Value<string>());
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
			kparams.AddIfNotNull("idIn", this._IdIn);
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
				case ID_IN:
					return "IdIn";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

