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
	public class Tag : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string TYPE = "type";
		public const string TAG = "tag";
		public const string MULTILINGUAL_TAG = "multilingualTag";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private int _Type = Int32.MinValue;
		private string _Tag = null;
		private IList<TranslationToken> _MultilingualTag;
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
		public int Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		[JsonProperty]
		public string TagValue
		{
			get { return _Tag; }
			private set 
			{ 
				_Tag = value;
				OnPropertyChanged("Tag");
			}
		}
		[JsonProperty]
		public IList<TranslationToken> MultilingualTag
		{
			get { return _MultilingualTag; }
			set 
			{ 
				_MultilingualTag = value;
				OnPropertyChanged("MultilingualTag");
			}
		}
		#endregion

		#region CTor
		public Tag()
		{
		}

		public Tag(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = ParseInt(node["type"].Value<string>());
			}
			if(node["tag"] != null)
			{
				this._Tag = node["tag"].Value<string>();
			}
			if(node["multilingualTag"] != null)
			{
				this._MultilingualTag = new List<TranslationToken>();
				foreach(var arrayNode in node["multilingualTag"].Children())
				{
					this._MultilingualTag.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaTag");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("tag", this._Tag);
			kparams.AddIfNotNull("multilingualTag", this._MultilingualTag);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case TYPE:
					return "Type";
				case TAG:
					return "Tag";
				case MULTILINGUAL_TAG:
					return "MultilingualTag";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

