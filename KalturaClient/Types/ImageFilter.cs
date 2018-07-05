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
	public class ImageFilter : Filter
	{
		#region Constants
		public const string ID_IN = "idIn";
		public const string IMAGE_OBJECT_ID_EQUAL = "imageObjectIdEqual";
		public const string IMAGE_OBJECT_TYPE_EQUAL = "imageObjectTypeEqual";
		public const string IS_DEFAULT_EQUAL = "isDefaultEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _IdIn = null;
		private long _ImageObjectIdEqual = long.MinValue;
		private ImageObjectType _ImageObjectTypeEqual = null;
		private bool? _IsDefaultEqual = null;
		private ImageOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public string IdIn
		{
			get { return _IdIn; }
			set 
			{ 
				_IdIn = value;
				OnPropertyChanged("IdIn");
			}
		}
		public long ImageObjectIdEqual
		{
			get { return _ImageObjectIdEqual; }
			set 
			{ 
				_ImageObjectIdEqual = value;
				OnPropertyChanged("ImageObjectIdEqual");
			}
		}
		public ImageObjectType ImageObjectTypeEqual
		{
			get { return _ImageObjectTypeEqual; }
			set 
			{ 
				_ImageObjectTypeEqual = value;
				OnPropertyChanged("ImageObjectTypeEqual");
			}
		}
		public bool? IsDefaultEqual
		{
			get { return _IsDefaultEqual; }
			set 
			{ 
				_IsDefaultEqual = value;
				OnPropertyChanged("IsDefaultEqual");
			}
		}
		public new ImageOrderBy OrderBy
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
		public ImageFilter()
		{
		}

		public ImageFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "idIn":
						this._IdIn = propertyNode.InnerText;
						continue;
					case "imageObjectIdEqual":
						this._ImageObjectIdEqual = ParseLong(propertyNode.InnerText);
						continue;
					case "imageObjectTypeEqual":
						this._ImageObjectTypeEqual = (ImageObjectType)StringEnum.Parse(typeof(ImageObjectType), propertyNode.InnerText);
						continue;
					case "isDefaultEqual":
						this._IsDefaultEqual = ParseBool(propertyNode.InnerText);
						continue;
					case "orderBy":
						this._OrderBy = (ImageOrderBy)StringEnum.Parse(typeof(ImageOrderBy), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaImageFilter");
			kparams.AddIfNotNull("idIn", this._IdIn);
			kparams.AddIfNotNull("imageObjectIdEqual", this._ImageObjectIdEqual);
			kparams.AddIfNotNull("imageObjectTypeEqual", this._ImageObjectTypeEqual);
			kparams.AddIfNotNull("isDefaultEqual", this._IsDefaultEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID_IN:
					return "IdIn";
				case IMAGE_OBJECT_ID_EQUAL:
					return "ImageObjectIdEqual";
				case IMAGE_OBJECT_TYPE_EQUAL:
					return "ImageObjectTypeEqual";
				case IS_DEFAULT_EQUAL:
					return "IsDefaultEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

