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
	public class CouponsGroup : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string DESCRIPTIONS = "descriptions";
		public const string START_DATE = "startDate";
		public const string END_DATE = "endDate";
		public const string MAX_USES_NUMBER = "maxUsesNumber";
		public const string MAX_USES_NUMBER_ON_RENEWABLE_SUB = "maxUsesNumberOnRenewableSub";
		public const string COUPON_GROUP_TYPE = "couponGroupType";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Name = null;
		private IList<TranslationToken> _Descriptions;
		private long _StartDate = long.MinValue;
		private long _EndDate = long.MinValue;
		private int _MaxUsesNumber = Int32.MinValue;
		private int _MaxUsesNumberOnRenewableSub = Int32.MinValue;
		private CouponGroupType _CouponGroupType = null;
		#endregion

		#region Properties
		public string Id
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
		public IList<TranslationToken> Descriptions
		{
			get { return _Descriptions; }
			set 
			{ 
				_Descriptions = value;
				OnPropertyChanged("Descriptions");
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
		public int MaxUsesNumber
		{
			get { return _MaxUsesNumber; }
			set 
			{ 
				_MaxUsesNumber = value;
				OnPropertyChanged("MaxUsesNumber");
			}
		}
		public int MaxUsesNumberOnRenewableSub
		{
			get { return _MaxUsesNumberOnRenewableSub; }
			set 
			{ 
				_MaxUsesNumberOnRenewableSub = value;
				OnPropertyChanged("MaxUsesNumberOnRenewableSub");
			}
		}
		public CouponGroupType CouponGroupType
		{
			get { return _CouponGroupType; }
			set 
			{ 
				_CouponGroupType = value;
				OnPropertyChanged("CouponGroupType");
			}
		}
		#endregion

		#region CTor
		public CouponsGroup()
		{
		}

		public CouponsGroup(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "descriptions":
						this._Descriptions = new List<TranslationToken>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Descriptions.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
						}
						continue;
					case "startDate":
						this._StartDate = ParseLong(propertyNode.InnerText);
						continue;
					case "endDate":
						this._EndDate = ParseLong(propertyNode.InnerText);
						continue;
					case "maxUsesNumber":
						this._MaxUsesNumber = ParseInt(propertyNode.InnerText);
						continue;
					case "maxUsesNumberOnRenewableSub":
						this._MaxUsesNumberOnRenewableSub = ParseInt(propertyNode.InnerText);
						continue;
					case "couponGroupType":
						this._CouponGroupType = (CouponGroupType)StringEnum.Parse(typeof(CouponGroupType), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaCouponsGroup");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("descriptions", this._Descriptions);
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("maxUsesNumber", this._MaxUsesNumber);
			kparams.AddIfNotNull("maxUsesNumberOnRenewableSub", this._MaxUsesNumberOnRenewableSub);
			kparams.AddIfNotNull("couponGroupType", this._CouponGroupType);
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
				case DESCRIPTIONS:
					return "Descriptions";
				case START_DATE:
					return "StartDate";
				case END_DATE:
					return "EndDate";
				case MAX_USES_NUMBER:
					return "MaxUsesNumber";
				case MAX_USES_NUMBER_ON_RENEWABLE_SUB:
					return "MaxUsesNumberOnRenewableSub";
				case COUPON_GROUP_TYPE:
					return "CouponGroupType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

