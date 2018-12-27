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
		public string SystemName
		{
			get { return _SystemName; }
			set 
			{ 
				_SystemName = value;
				OnPropertyChanged("SystemName");
			}
		}
		public long RatioId
		{
			get { return _RatioId; }
			set 
			{ 
				_RatioId = value;
				OnPropertyChanged("RatioId");
			}
		}
		public string HelpText
		{
			get { return _HelpText; }
			set 
			{ 
				_HelpText = value;
				OnPropertyChanged("HelpText");
			}
		}
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

		public ImageType(XmlElement node) : base(node)
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
					case "systemName":
						this._SystemName = propertyNode.InnerText;
						continue;
					case "ratioId":
						this._RatioId = ParseLong(propertyNode.InnerText);
						continue;
					case "helpText":
						this._HelpText = propertyNode.InnerText;
						continue;
					case "defaultImageId":
						this._DefaultImageId = ParseLong(propertyNode.InnerText);
						continue;
				}
			}
		}

		public ImageType(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<long>("id");
			    this._Name = data.TryGetValueSafe<string>("name");
			    this._SystemName = data.TryGetValueSafe<string>("systemName");
			    this._RatioId = data.TryGetValueSafe<long>("ratioId");
			    this._HelpText = data.TryGetValueSafe<string>("helpText");
			    this._DefaultImageId = data.TryGetValueSafe<long>("defaultImageId");
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

