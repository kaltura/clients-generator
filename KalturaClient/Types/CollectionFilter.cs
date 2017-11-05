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
	public class CollectionFilter : Filter
	{
		#region Constants
		public const string COLLECTION_ID_IN = "collectionIdIn";
		public const string MEDIA_FILE_ID_EQUAL = "mediaFileIdEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _CollectionIdIn = null;
		private int _MediaFileIdEqual = Int32.MinValue;
		private CollectionOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public string CollectionIdIn
		{
			get { return _CollectionIdIn; }
			set 
			{ 
				_CollectionIdIn = value;
				OnPropertyChanged("CollectionIdIn");
			}
		}
		public int MediaFileIdEqual
		{
			get { return _MediaFileIdEqual; }
			set 
			{ 
				_MediaFileIdEqual = value;
				OnPropertyChanged("MediaFileIdEqual");
			}
		}
		public new CollectionOrderBy OrderBy
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
		public CollectionFilter()
		{
		}

		public CollectionFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "collectionIdIn":
						this._CollectionIdIn = propertyNode.InnerText;
						continue;
					case "mediaFileIdEqual":
						this._MediaFileIdEqual = ParseInt(propertyNode.InnerText);
						continue;
					case "orderBy":
						this._OrderBy = (CollectionOrderBy)StringEnum.Parse(typeof(CollectionOrderBy), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaCollectionFilter");
			kparams.AddIfNotNull("collectionIdIn", this._CollectionIdIn);
			kparams.AddIfNotNull("mediaFileIdEqual", this._MediaFileIdEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case COLLECTION_ID_IN:
					return "CollectionIdIn";
				case MEDIA_FILE_ID_EQUAL:
					return "MediaFileIdEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

