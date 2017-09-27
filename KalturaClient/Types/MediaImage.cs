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
	public class MediaImage : ObjectBase
	{
		#region Constants
		public const string RATIO = "ratio";
		public const string WIDTH = "width";
		public const string HEIGHT = "height";
		public const string URL = "url";
		public const string VERSION = "version";
		public const string ID = "id";
		public const string IS_DEFAULT = "isDefault";
		#endregion

		#region Private Fields
		private string _Ratio = null;
		private int _Width = Int32.MinValue;
		private int _Height = Int32.MinValue;
		private string _Url = null;
		private int _Version = Int32.MinValue;
		private string _Id = null;
		private bool? _IsDefault = null;
		#endregion

		#region Properties
		public string Ratio
		{
			get { return _Ratio; }
			set 
			{ 
				_Ratio = value;
				OnPropertyChanged("Ratio");
			}
		}
		public int Width
		{
			get { return _Width; }
			set 
			{ 
				_Width = value;
				OnPropertyChanged("Width");
			}
		}
		public int Height
		{
			get { return _Height; }
			set 
			{ 
				_Height = value;
				OnPropertyChanged("Height");
			}
		}
		public string Url
		{
			get { return _Url; }
			set 
			{ 
				_Url = value;
				OnPropertyChanged("Url");
			}
		}
		public int Version
		{
			get { return _Version; }
			set 
			{ 
				_Version = value;
				OnPropertyChanged("Version");
			}
		}
		public string Id
		{
			get { return _Id; }
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
		#endregion

		#region CTor
		public MediaImage()
		{
		}

		public MediaImage(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "ratio":
						this._Ratio = propertyNode.InnerText;
						continue;
					case "width":
						this._Width = ParseInt(propertyNode.InnerText);
						continue;
					case "height":
						this._Height = ParseInt(propertyNode.InnerText);
						continue;
					case "url":
						this._Url = propertyNode.InnerText;
						continue;
					case "version":
						this._Version = ParseInt(propertyNode.InnerText);
						continue;
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "isDefault":
						this._IsDefault = ParseBool(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaMediaImage");
			kparams.AddIfNotNull("ratio", this._Ratio);
			kparams.AddIfNotNull("width", this._Width);
			kparams.AddIfNotNull("height", this._Height);
			kparams.AddIfNotNull("url", this._Url);
			kparams.AddIfNotNull("version", this._Version);
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("isDefault", this._IsDefault);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case RATIO:
					return "Ratio";
				case WIDTH:
					return "Width";
				case HEIGHT:
					return "Height";
				case URL:
					return "Url";
				case VERSION:
					return "Version";
				case ID:
					return "Id";
				case IS_DEFAULT:
					return "IsDefault";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

