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
	public class SearchExternalFilter : AssetFilter
	{
		#region Constants
		public const string QUERY = "query";
		public const string UTC_OFFSET_EQUAL = "utcOffsetEqual";
		public const string TYPE_IN = "typeIn";
		#endregion

		#region Private Fields
		private string _Query = null;
		private int _UtcOffsetEqual = Int32.MinValue;
		private string _TypeIn = null;
		#endregion

		#region Properties
		public string Query
		{
			get { return _Query; }
			set 
			{ 
				_Query = value;
				OnPropertyChanged("Query");
			}
		}
		public int UtcOffsetEqual
		{
			get { return _UtcOffsetEqual; }
			set 
			{ 
				_UtcOffsetEqual = value;
				OnPropertyChanged("UtcOffsetEqual");
			}
		}
		public string TypeIn
		{
			get { return _TypeIn; }
			set 
			{ 
				_TypeIn = value;
				OnPropertyChanged("TypeIn");
			}
		}
		#endregion

		#region CTor
		public SearchExternalFilter()
		{
		}

		public SearchExternalFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "query":
						this._Query = propertyNode.InnerText;
						continue;
					case "utcOffsetEqual":
						this._UtcOffsetEqual = ParseInt(propertyNode.InnerText);
						continue;
					case "typeIn":
						this._TypeIn = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaSearchExternalFilter");
			kparams.AddIfNotNull("query", this._Query);
			kparams.AddIfNotNull("utcOffsetEqual", this._UtcOffsetEqual);
			kparams.AddIfNotNull("typeIn", this._TypeIn);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case QUERY:
					return "Query";
				case UTC_OFFSET_EQUAL:
					return "UtcOffsetEqual";
				case TYPE_IN:
					return "TypeIn";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

