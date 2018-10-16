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
	public class BusinessModuleCondition : Condition
	{
		#region Constants
		public const string BUSINESS_MODULE_TYPE = "businessModuleType";
		public const string BUSINESS_MODULE_ID = "businessModuleId";
		#endregion

		#region Private Fields
		private TransactionType _BusinessModuleType = null;
		private long _BusinessModuleId = long.MinValue;
		#endregion

		#region Properties
		public TransactionType BusinessModuleType
		{
			get { return _BusinessModuleType; }
			set 
			{ 
				_BusinessModuleType = value;
				OnPropertyChanged("BusinessModuleType");
			}
		}
		public long BusinessModuleId
		{
			get { return _BusinessModuleId; }
			set 
			{ 
				_BusinessModuleId = value;
				OnPropertyChanged("BusinessModuleId");
			}
		}
		#endregion

		#region CTor
		public BusinessModuleCondition()
		{
		}

		public BusinessModuleCondition(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "businessModuleType":
						this._BusinessModuleType = (TransactionType)StringEnum.Parse(typeof(TransactionType), propertyNode.InnerText);
						continue;
					case "businessModuleId":
						this._BusinessModuleId = ParseLong(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaBusinessModuleCondition");
			kparams.AddIfNotNull("businessModuleType", this._BusinessModuleType);
			kparams.AddIfNotNull("businessModuleId", this._BusinessModuleId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case BUSINESS_MODULE_TYPE:
					return "BusinessModuleType";
				case BUSINESS_MODULE_ID:
					return "BusinessModuleId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

