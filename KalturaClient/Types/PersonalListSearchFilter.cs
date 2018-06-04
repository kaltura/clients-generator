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
	public class PersonalListSearchFilter : BaseSearchAssetFilter
	{
		#region Constants
		public const string TYPE_IN = "typeIn";
		public const string KSQL = "kSql";
		public const string PARTNER_LIST_TYPE_EQUAL = "partnerListTypeEqual";
		#endregion

		#region Private Fields
		private string _TypeIn = null;
		private string _KSql = null;
		private int _PartnerListTypeEqual = Int32.MinValue;
		#endregion

		#region Properties
		public string TypeIn
		{
			get { return _TypeIn; }
			set 
			{ 
				_TypeIn = value;
				OnPropertyChanged("TypeIn");
			}
		}
		public string KSql
		{
			get { return _KSql; }
			set 
			{ 
				_KSql = value;
				OnPropertyChanged("KSql");
			}
		}
		public int PartnerListTypeEqual
		{
			get { return _PartnerListTypeEqual; }
			set 
			{ 
				_PartnerListTypeEqual = value;
				OnPropertyChanged("PartnerListTypeEqual");
			}
		}
		#endregion

		#region CTor
		public PersonalListSearchFilter()
		{
		}

		public PersonalListSearchFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "typeIn":
						this._TypeIn = propertyNode.InnerText;
						continue;
					case "kSql":
						this._KSql = propertyNode.InnerText;
						continue;
					case "partnerListTypeEqual":
						this._PartnerListTypeEqual = ParseInt(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaPersonalListSearchFilter");
			kparams.AddIfNotNull("typeIn", this._TypeIn);
			kparams.AddIfNotNull("kSql", this._KSql);
			kparams.AddIfNotNull("partnerListTypeEqual", this._PartnerListTypeEqual);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case TYPE_IN:
					return "TypeIn";
				case KSQL:
					return "KSql";
				case PARTNER_LIST_TYPE_EQUAL:
					return "PartnerListTypeEqual";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

