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
	public class ImageType : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string SYSTEM_NAME = "systemName";
		public const string RATIO_ID = "ratioId";
		public const string HELP_TEXT = "helpText";
		public const string DEFAULT_IMAGE_ID = "defaultImageId";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private string _SystemName = null;
		private long _RatioId = long.MinValue;
		private string _HelpText = null;
		private long _DefaultImageId = long.MinValue;
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
		public long RatioId
		{
			get { return _RatioId; }
			set 
			{ 
				_RatioId = value;
				OnPropertyChanged("RatioId");
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
		public long DefaultImageId
		{
			get { return _DefaultImageId; }
			set 
			{ 
				_DefaultImageId = value;
				OnPropertyChanged("DefaultImageId");
			}
		}
		#endregion

		#region CTor
		public ImageType()
		{
		}

		public ImageType(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["systemName"] != null)
			{
				this._SystemName = node["systemName"].Value<string>();
			}
			if(node["ratioId"] != null)
			{
				this._RatioId = ParseLong(node["ratioId"].Value<string>());
			}
			if(node["helpText"] != null)
			{
				this._HelpText = node["helpText"].Value<string>();
			}
			if(node["defaultImageId"] != null)
			{
				this._DefaultImageId = ParseLong(node["defaultImageId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaImageType");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("systemName", this._SystemName);
			kparams.AddIfNotNull("ratioId", this._RatioId);
			kparams.AddIfNotNull("helpText", this._HelpText);
			kparams.AddIfNotNull("defaultImageId", this._DefaultImageId);
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
				case SYSTEM_NAME:
					return "SystemName";
				case RATIO_ID:
					return "RatioId";
				case HELP_TEXT:
					return "HelpText";
				case DEFAULT_IMAGE_ID:
					return "DefaultImageId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

