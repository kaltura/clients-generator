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
	public class OTTCategory : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string PARENT_CATEGORY_ID = "parentCategoryId";
		public const string CHILD_CATEGORIES = "childCategories";
		public const string CHANNELS = "channels";
		public const string IMAGES = "images";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private long _ParentCategoryId = long.MinValue;
		private IList<OTTCategory> _ChildCategories;
		private IList<Channel> _Channels;
		private IList<MediaImage> _Images;
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
		public long ParentCategoryId
		{
			get { return _ParentCategoryId; }
			set 
			{ 
				_ParentCategoryId = value;
				OnPropertyChanged("ParentCategoryId");
			}
		}
		public IList<OTTCategory> ChildCategories
		{
			get { return _ChildCategories; }
			set 
			{ 
				_ChildCategories = value;
				OnPropertyChanged("ChildCategories");
			}
		}
		public IList<Channel> Channels
		{
			get { return _Channels; }
			set 
			{ 
				_Channels = value;
				OnPropertyChanged("Channels");
			}
		}
		public IList<MediaImage> Images
		{
			get { return _Images; }
			set 
			{ 
				_Images = value;
				OnPropertyChanged("Images");
			}
		}
		#endregion

		#region CTor
		public OTTCategory()
		{
		}

		public OTTCategory(XmlElement node) : base(node)
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
					case "parentCategoryId":
						this._ParentCategoryId = ParseLong(propertyNode.InnerText);
						continue;
					case "childCategories":
						this._ChildCategories = new List<OTTCategory>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._ChildCategories.Add(ObjectFactory.Create<OTTCategory>(arrayNode));
						}
						continue;
					case "channels":
						this._Channels = new List<Channel>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Channels.Add(ObjectFactory.Create<Channel>(arrayNode));
						}
						continue;
					case "images":
						this._Images = new List<MediaImage>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Images.Add(ObjectFactory.Create<MediaImage>(arrayNode));
						}
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
				kparams.AddReplace("objectType", "KalturaOTTCategory");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("parentCategoryId", this._ParentCategoryId);
			kparams.AddIfNotNull("childCategories", this._ChildCategories);
			kparams.AddIfNotNull("channels", this._Channels);
			kparams.AddIfNotNull("images", this._Images);
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
				case PARENT_CATEGORY_ID:
					return "ParentCategoryId";
				case CHILD_CATEGORIES:
					return "ChildCategories";
				case CHANNELS:
					return "Channels";
				case IMAGES:
					return "Images";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

