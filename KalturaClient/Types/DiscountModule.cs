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
	public class DiscountModule : ObjectBase
	{
		#region Constants
		public const string PERCENT = "percent";
		public const string START_DATE = "startDate";
		public const string END_DATE = "endDate";
		#endregion

		#region Private Fields
		private float _Percent = Single.MinValue;
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		#endregion

		#region Properties
		public float Percent
		{
			get { return _Percent; }
			set 
			{ 
				_Percent = value;
				OnPropertyChanged("Percent");
			}
		}
		public long StartDate
		{
			get { return _StartDate; }
			set 
			{ 
				_StartDate = value;
				OnPropertyChanged("StartDate");
			}
		}
		public long EndDate
		{
			get { return _EndDate; }
			set 
			{ 
				_EndDate = value;
				OnPropertyChanged("EndDate");
			}
		}
		#endregion

		#region CTor
		public DiscountModule()
		{
		}

		public DiscountModule(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "percent":
						this._Percent = ParseFloat(propertyNode.InnerText);
						continue;
					case "startDate":
						this._StartDate = ParseLong(propertyNode.InnerText);
						continue;
					case "endDate":
						this._EndDate = ParseLong(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaDiscountModule");
			kparams.AddIfNotNull("percent", this._Percent);
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("endDate", this._EndDate);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PERCENT:
					return "Percent";
				case START_DATE:
					return "StartDate";
				case END_DATE:
					return "EndDate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

