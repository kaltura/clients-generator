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
	public class FilterPager : ObjectBase
	{
		#region Constants
		public const string PAGE_SIZE = "pageSize";
		public const string PAGE_INDEX = "pageIndex";
		#endregion

		#region Private Fields
		private int _PageSize = Int32.MinValue;
		private int _PageIndex = Int32.MinValue;
		#endregion

		#region Properties
		public int PageSize
		{
			get { return _PageSize; }
			set 
			{ 
				_PageSize = value;
				OnPropertyChanged("PageSize");
			}
		}
		public int PageIndex
		{
			get { return _PageIndex; }
			set 
			{ 
				_PageIndex = value;
				OnPropertyChanged("PageIndex");
			}
		}
		#endregion

		#region CTor
		public FilterPager()
		{
		}

		public FilterPager(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "pageSize":
						this._PageSize = ParseInt(propertyNode.InnerText);
						continue;
					case "pageIndex":
						this._PageIndex = ParseInt(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaFilterPager");
			kparams.AddIfNotNull("pageSize", this._PageSize);
			kparams.AddIfNotNull("pageIndex", this._PageIndex);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PAGE_SIZE:
					return "PageSize";
				case PAGE_INDEX:
					return "PageIndex";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

