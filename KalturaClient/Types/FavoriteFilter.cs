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
	public class FavoriteFilter : Filter
	{
		#region Constants
		public const string MEDIA_TYPE_EQUAL = "mediaTypeEqual";
		public const string MEDIA_ID_IN = "mediaIdIn";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private int _MediaTypeEqual = Int32.MinValue;
		private string _MediaIdIn = null;
		private FavoriteOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public int MediaTypeEqual
		{
			get { return _MediaTypeEqual; }
			set 
			{ 
				_MediaTypeEqual = value;
				OnPropertyChanged("MediaTypeEqual");
			}
		}
		public string MediaIdIn
		{
			get { return _MediaIdIn; }
			set 
			{ 
				_MediaIdIn = value;
				OnPropertyChanged("MediaIdIn");
			}
		}
		public new FavoriteOrderBy OrderBy
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
		public FavoriteFilter()
		{
		}

		public FavoriteFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "mediaTypeEqual":
						this._MediaTypeEqual = ParseInt(propertyNode.InnerText);
						continue;
					case "mediaIdIn":
						this._MediaIdIn = propertyNode.InnerText;
						continue;
					case "orderBy":
						this._OrderBy = (FavoriteOrderBy)StringEnum.Parse(typeof(FavoriteOrderBy), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaFavoriteFilter");
			kparams.AddIfNotNull("mediaTypeEqual", this._MediaTypeEqual);
			kparams.AddIfNotNull("mediaIdIn", this._MediaIdIn);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case MEDIA_TYPE_EQUAL:
					return "MediaTypeEqual";
				case MEDIA_ID_IN:
					return "MediaIdIn";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

