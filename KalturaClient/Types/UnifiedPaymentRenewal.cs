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
	public class UnifiedPaymentRenewal : ObjectBase
	{
		#region Constants
		public const string PRICE = "price";
		public const string DATE = "date";
		public const string UNIFIED_PAYMENT_ID = "unifiedPaymentId";
		public const string ENTITLEMENTS = "entitlements";
		#endregion

		#region Private Fields
		private Price _Price;
		private long _Date = long.MinValue;
		private long _UnifiedPaymentId = long.MinValue;
		private IList<EntitlementRenewalBase> _Entitlements;
		#endregion

		#region Properties
		public Price Price
		{
			get { return _Price; }
			set 
			{ 
				_Price = value;
				OnPropertyChanged("Price");
			}
		}
		public long Date
		{
			get { return _Date; }
			set 
			{ 
				_Date = value;
				OnPropertyChanged("Date");
			}
		}
		public long UnifiedPaymentId
		{
			get { return _UnifiedPaymentId; }
			set 
			{ 
				_UnifiedPaymentId = value;
				OnPropertyChanged("UnifiedPaymentId");
			}
		}
		public IList<EntitlementRenewalBase> Entitlements
		{
			get { return _Entitlements; }
			set 
			{ 
				_Entitlements = value;
				OnPropertyChanged("Entitlements");
			}
		}
		#endregion

		#region CTor
		public UnifiedPaymentRenewal()
		{
		}

		public UnifiedPaymentRenewal(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "price":
						this._Price = ObjectFactory.Create<Price>(propertyNode);
						continue;
					case "date":
						this._Date = ParseLong(propertyNode.InnerText);
						continue;
					case "unifiedPaymentId":
						this._UnifiedPaymentId = ParseLong(propertyNode.InnerText);
						continue;
					case "entitlements":
						this._Entitlements = new List<EntitlementRenewalBase>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Entitlements.Add(ObjectFactory.Create<EntitlementRenewalBase>(arrayNode));
						}
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
				kparams.AddReplace("objectType", "KalturaUnifiedPaymentRenewal");
			kparams.AddIfNotNull("price", this._Price);
			kparams.AddIfNotNull("date", this._Date);
			kparams.AddIfNotNull("unifiedPaymentId", this._UnifiedPaymentId);
			kparams.AddIfNotNull("entitlements", this._Entitlements);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PRICE:
					return "Price";
				case DATE:
					return "Date";
				case UNIFIED_PAYMENT_ID:
					return "UnifiedPaymentId";
				case ENTITLEMENTS:
					return "Entitlements";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

