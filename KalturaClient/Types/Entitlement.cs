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
	public class Entitlement : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string PRODUCT_ID = "productId";
		public const string CURRENT_USES = "currentUses";
		public const string END_DATE = "endDate";
		public const string CURRENT_DATE = "currentDate";
		public const string LAST_VIEW_DATE = "lastViewDate";
		public const string PURCHASE_DATE = "purchaseDate";
		public const string PAYMENT_METHOD = "paymentMethod";
		public const string DEVICE_UDID = "deviceUdid";
		public const string DEVICE_NAME = "deviceName";
		public const string IS_CANCELATION_WINDOW_ENABLED = "isCancelationWindowEnabled";
		public const string MAX_USES = "maxUses";
		public const string USER_ID = "userId";
		public const string HOUSEHOLD_ID = "householdId";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _ProductId = null;
		private int _CurrentUses = Int32.MinValue;
		private long _EndDate = long.MinValue;
		private long _CurrentDate = long.MinValue;
		private long _LastViewDate = long.MinValue;
		private long _PurchaseDate = long.MinValue;
		private PaymentMethodType _PaymentMethod = null;
		private string _DeviceUdid = null;
		private string _DeviceName = null;
		private bool? _IsCancelationWindowEnabled = null;
		private int _MaxUses = Int32.MinValue;
		private string _UserId = null;
		private long _HouseholdId = long.MinValue;
		#endregion

		#region Properties
		public int Id
		{
			get { return _Id; }
		}
		public string ProductId
		{
			get { return _ProductId; }
		}
		public int CurrentUses
		{
			get { return _CurrentUses; }
		}
		public long EndDate
		{
			get { return _EndDate; }
		}
		public long CurrentDate
		{
			get { return _CurrentDate; }
		}
		public long LastViewDate
		{
			get { return _LastViewDate; }
		}
		public long PurchaseDate
		{
			get { return _PurchaseDate; }
		}
		public PaymentMethodType PaymentMethod
		{
			get { return _PaymentMethod; }
		}
		public string DeviceUdid
		{
			get { return _DeviceUdid; }
		}
		public string DeviceName
		{
			get { return _DeviceName; }
		}
		public bool? IsCancelationWindowEnabled
		{
			get { return _IsCancelationWindowEnabled; }
		}
		public int MaxUses
		{
			get { return _MaxUses; }
		}
		public string UserId
		{
			get { return _UserId; }
		}
		public long HouseholdId
		{
			get { return _HouseholdId; }
		}
		#endregion

		#region CTor
		public Entitlement()
		{
		}

		public Entitlement(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "productId":
						this._ProductId = propertyNode.InnerText;
						continue;
					case "currentUses":
						this._CurrentUses = ParseInt(propertyNode.InnerText);
						continue;
					case "endDate":
						this._EndDate = ParseLong(propertyNode.InnerText);
						continue;
					case "currentDate":
						this._CurrentDate = ParseLong(propertyNode.InnerText);
						continue;
					case "lastViewDate":
						this._LastViewDate = ParseLong(propertyNode.InnerText);
						continue;
					case "purchaseDate":
						this._PurchaseDate = ParseLong(propertyNode.InnerText);
						continue;
					case "paymentMethod":
						this._PaymentMethod = (PaymentMethodType)StringEnum.Parse(typeof(PaymentMethodType), propertyNode.InnerText);
						continue;
					case "deviceUdid":
						this._DeviceUdid = propertyNode.InnerText;
						continue;
					case "deviceName":
						this._DeviceName = propertyNode.InnerText;
						continue;
					case "isCancelationWindowEnabled":
						this._IsCancelationWindowEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "maxUses":
						this._MaxUses = ParseInt(propertyNode.InnerText);
						continue;
					case "userId":
						this._UserId = propertyNode.InnerText;
						continue;
					case "householdId":
						this._HouseholdId = ParseLong(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaEntitlement");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("productId", this._ProductId);
			kparams.AddIfNotNull("currentUses", this._CurrentUses);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("currentDate", this._CurrentDate);
			kparams.AddIfNotNull("lastViewDate", this._LastViewDate);
			kparams.AddIfNotNull("purchaseDate", this._PurchaseDate);
			kparams.AddIfNotNull("paymentMethod", this._PaymentMethod);
			kparams.AddIfNotNull("deviceUdid", this._DeviceUdid);
			kparams.AddIfNotNull("deviceName", this._DeviceName);
			kparams.AddIfNotNull("isCancelationWindowEnabled", this._IsCancelationWindowEnabled);
			kparams.AddIfNotNull("maxUses", this._MaxUses);
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("householdId", this._HouseholdId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case PRODUCT_ID:
					return "ProductId";
				case CURRENT_USES:
					return "CurrentUses";
				case END_DATE:
					return "EndDate";
				case CURRENT_DATE:
					return "CurrentDate";
				case LAST_VIEW_DATE:
					return "LastViewDate";
				case PURCHASE_DATE:
					return "PurchaseDate";
				case PAYMENT_METHOD:
					return "PaymentMethod";
				case DEVICE_UDID:
					return "DeviceUdid";
				case DEVICE_NAME:
					return "DeviceName";
				case IS_CANCELATION_WINDOW_ENABLED:
					return "IsCancelationWindowEnabled";
				case MAX_USES:
					return "MaxUses";
				case USER_ID:
					return "UserId";
				case HOUSEHOLD_ID:
					return "HouseholdId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

