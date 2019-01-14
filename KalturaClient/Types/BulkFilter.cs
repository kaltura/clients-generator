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
	public class BulkFilter : PersistedFilter
	{
		#region Constants
		public const string STATUS_EQUAL = "statusEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private BatchJobStatus _StatusEqual = null;
		private BulkOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public BatchJobStatus StatusEqual
		{
			get { return _StatusEqual; }
			set 
			{ 
				_StatusEqual = value;
				OnPropertyChanged("StatusEqual");
			}
		}
		public new BulkOrderBy OrderBy
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
		public BulkFilter()
		{
		}

		public BulkFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "statusEqual":
						this._StatusEqual = (BatchJobStatus)StringEnum.Parse(typeof(BatchJobStatus), propertyNode.InnerText);
						continue;
					case "orderBy":
						this._OrderBy = (BulkOrderBy)StringEnum.Parse(typeof(BulkOrderBy), propertyNode.InnerText);
						continue;
				}
			}
		}

		public BulkFilter(IDictionary<string,object> data) : base(data)
		{
			    this._StatusEqual = (BatchJobStatus)StringEnum.Parse(typeof(BatchJobStatus), data.TryGetValueSafe<string>("statusEqual"));
			    this._OrderBy = (BulkOrderBy)StringEnum.Parse(typeof(BulkOrderBy), data.TryGetValueSafe<string>("orderBy"));
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBulkFilter");
			kparams.AddIfNotNull("statusEqual", this._StatusEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case STATUS_EQUAL:
					return "StatusEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

