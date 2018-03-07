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
	public class Channel : BaseChannel
	{
		#region Constants
		public const string NAME = "name";
		public const string DESCRIPTION = "description";
		public const string IMAGES = "images";
		public const string ASSET_TYPES = "assetTypes";
		public const string FILTER_EXPRESSION = "filterExpression";
		public const string IS_ACTIVE = "isActive";
		public const string ORDER = "order";
		public const string GROUP_BY = "groupBy";
		#endregion

		#region Private Fields
		private string _Name = null;
		private string _Description = null;
		private IList<MediaImage> _Images;
		private IList<IntegerValue> _AssetTypes;
		private string _FilterExpression = null;
		private bool? _IsActive = null;
		private AssetOrderBy _Order = null;
		private AssetGroupBy _GroupBy;
		#endregion

		#region Properties
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
		public IList<MediaImage> Images
		{
			get { return _Images; }
			set 
			{ 
				_Images = value;
				OnPropertyChanged("Images");
			}
		}
		public IList<IntegerValue> AssetTypes
		{
			get { return _AssetTypes; }
			set 
			{ 
				_AssetTypes = value;
				OnPropertyChanged("AssetTypes");
			}
		}
		public string FilterExpression
		{
			get { return _FilterExpression; }
			set 
			{ 
				_FilterExpression = value;
				OnPropertyChanged("FilterExpression");
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
		public AssetOrderBy Order
		{
			get { return _Order; }
			set 
			{ 
				_Order = value;
				OnPropertyChanged("Order");
			}
		}
		public AssetGroupBy GroupBy
		{
			get { return _GroupBy; }
			set 
			{ 
				_GroupBy = value;
				OnPropertyChanged("GroupBy");
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
					case "description":
						this._Description = propertyNode.InnerText;
						continue;
					case "images":
						this._Images = new List<MediaImage>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Images.Add(ObjectFactory.Create<MediaImage>(arrayNode));
						}
						continue;
					case "assetTypes":
						this._AssetTypes = new List<IntegerValue>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._AssetTypes.Add(ObjectFactory.Create<IntegerValue>(arrayNode));
						}
						continue;
					case "filterExpression":
						this._FilterExpression = propertyNode.InnerText;
						continue;
					case "isActive":
						this._IsActive = ParseBool(propertyNode.InnerText);
						continue;
					case "order":
						this._Order = (AssetOrderBy)StringEnum.Parse(typeof(AssetOrderBy), propertyNode.InnerText);
						continue;
					case "groupBy":
						this._GroupBy = ObjectFactory.Create<AssetGroupBy>(propertyNode);
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
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("images", this._Images);
			kparams.AddIfNotNull("assetTypes", this._AssetTypes);
			kparams.AddIfNotNull("filterExpression", this._FilterExpression);
			kparams.AddIfNotNull("isActive", this._IsActive);
			kparams.AddIfNotNull("order", this._Order);
			kparams.AddIfNotNull("groupBy", this._GroupBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NAME:
					return "Name";
				case DESCRIPTION:
					return "Description";
				case IMAGES:
					return "Images";
				case ASSET_TYPES:
					return "AssetTypes";
				case FILTER_EXPRESSION:
					return "FilterExpression";
				case IS_ACTIVE:
					return "IsActive";
				case ORDER:
					return "Order";
				case GROUP_BY:
					return "GroupBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

