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
	public class PersonalList : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string CREATE_DATE = "createDate";
		public const string KSQL = "ksql";
		public const string PARTNER_LIST_TYPE = "partnerListType";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private long _CreateDate = long.MinValue;
		private string _Ksql = null;
		private int _PartnerListType = Int32.MinValue;
		#endregion

		#region Properties
		public long Id
		{
			get { return _Id; }
		}
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		public long CreateDate
		{
			get { return _CreateDate; }
		}
		public string Ksql
		{
			get { return _Ksql; }
			set 
			{ 
				_Ksql = value;
				OnPropertyChanged("Ksql");
			}
		}
		public int PartnerListType
		{
			get { return _PartnerListType; }
			set 
			{ 
				_PartnerListType = value;
				OnPropertyChanged("PartnerListType");
			}
		}
		#endregion

		#region CTor
		public PersonalList()
		{
		}

		public PersonalList(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseLong(propertyNode.InnerText);
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "createDate":
						this._CreateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "ksql":
						this._Ksql = propertyNode.InnerText;
						continue;
					case "partnerListType":
						this._PartnerListType = ParseInt(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaPersonalList");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("ksql", this._Ksql);
			kparams.AddIfNotNull("partnerListType", this._PartnerListType);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case NAME:
					return "Name";
				case CREATE_DATE:
					return "CreateDate";
				case KSQL:
					return "Ksql";
				case PARTNER_LIST_TYPE:
					return "PartnerListType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

