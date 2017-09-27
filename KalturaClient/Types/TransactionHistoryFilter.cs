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
	public class TransactionHistoryFilter : Filter
	{
		#region Constants
		public const string ENTITY_REFERENCE_EQUAL = "entityReferenceEqual";
		public const string START_DATE_GREATER_THAN_OR_EQUAL = "startDateGreaterThanOrEqual";
		public const string END_DATE_LESS_THAN_OR_EQUAL = "endDateLessThanOrEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private EntityReferenceBy _EntityReferenceEqual = null;
		private int _StartDateGreaterThanOrEqual = Int32.MinValue;
		private int _EndDateLessThanOrEqual = Int32.MinValue;
		private TransactionHistoryOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public EntityReferenceBy EntityReferenceEqual
		{
			get { return _EntityReferenceEqual; }
			set 
			{ 
				_EntityReferenceEqual = value;
				OnPropertyChanged("EntityReferenceEqual");
			}
		}
		public int StartDateGreaterThanOrEqual
		{
			get { return _StartDateGreaterThanOrEqual; }
			set 
			{ 
				_StartDateGreaterThanOrEqual = value;
				OnPropertyChanged("StartDateGreaterThanOrEqual");
			}
		}
		public int EndDateLessThanOrEqual
		{
			get { return _EndDateLessThanOrEqual; }
			set 
			{ 
				_EndDateLessThanOrEqual = value;
				OnPropertyChanged("EndDateLessThanOrEqual");
			}
		}
		public new TransactionHistoryOrderBy OrderBy
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
		public TransactionHistoryFilter()
		{
		}

		public TransactionHistoryFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "entityReferenceEqual":
						this._EntityReferenceEqual = (EntityReferenceBy)StringEnum.Parse(typeof(EntityReferenceBy), propertyNode.InnerText);
						continue;
					case "startDateGreaterThanOrEqual":
						this._StartDateGreaterThanOrEqual = ParseInt(propertyNode.InnerText);
						continue;
					case "endDateLessThanOrEqual":
						this._EndDateLessThanOrEqual = ParseInt(propertyNode.InnerText);
						continue;
					case "orderBy":
						this._OrderBy = (TransactionHistoryOrderBy)StringEnum.Parse(typeof(TransactionHistoryOrderBy), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaTransactionHistoryFilter");
			kparams.AddIfNotNull("entityReferenceEqual", this._EntityReferenceEqual);
			kparams.AddIfNotNull("startDateGreaterThanOrEqual", this._StartDateGreaterThanOrEqual);
			kparams.AddIfNotNull("endDateLessThanOrEqual", this._EndDateLessThanOrEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ENTITY_REFERENCE_EQUAL:
					return "EntityReferenceEqual";
				case START_DATE_GREATER_THAN_OR_EQUAL:
					return "StartDateGreaterThanOrEqual";
				case END_DATE_LESS_THAN_OR_EQUAL:
					return "EndDateLessThanOrEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

