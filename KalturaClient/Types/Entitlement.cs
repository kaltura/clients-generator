// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
// text.
//
// Copyright (C) 2006-2021  Kaltura Inc.
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
		[JsonProperty]
		public int Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string ProductId
		{
			get { return _ProductId; }
			private set 
			{ 
				_ProductId = value;
				OnPropertyChanged("ProductId");
			}
		}
		[JsonProperty]
		public int CurrentUses
		{
			get { return _CurrentUses; }
			private set 
			{ 
				_CurrentUses = value;
				OnPropertyChanged("CurrentUses");
			}
		}
		[JsonProperty]
		public long EndDate
		{
			get { return _EndDate; }
			private set 
			{ 
				_EndDate = value;
				OnPropertyChanged("EndDate");
			}
		}
		[JsonProperty]
		public long CurrentDate
		{
			get { return _CurrentDate; }
			private set 
			{ 
				_CurrentDate = value;
				OnPropertyChanged("CurrentDate");
			}
		}
		[JsonProperty]
		public long LastViewDate
		{
			get { return _LastViewDate; }
			private set 
			{ 
				_LastViewDate = value;
				OnPropertyChanged("LastViewDate");
			}
		}
		[JsonProperty]
		public long PurchaseDate
		{
			get { return _PurchaseDate; }
			private set 
			{ 
				_PurchaseDate = value;
				OnPropertyChanged("PurchaseDate");
			}
		}
		[JsonProperty]
		public PaymentMethodType PaymentMethod
		{
			get { return _PaymentMethod; }
			private set 
			{ 
				_PaymentMethod = value;
				OnPropertyChanged("PaymentMethod");
			}
		}
		[JsonProperty]
		public string DeviceUdid
		{
			get { return _DeviceUdid; }
			private set 
			{ 
				_DeviceUdid = value;
				OnPropertyChanged("DeviceUdid");
			}
		}
		[JsonProperty]
		public string DeviceName
		{
			get { return _DeviceName; }
			private set 
			{ 
				_DeviceName = value;
				OnPropertyChanged("DeviceName");
			}
		}
		[JsonProperty]
		public bool? IsCancelationWindowEnabled
		{
			get { return _IsCancelationWindowEnabled; }
			private set 
			{ 
				_IsCancelationWindowEnabled = value;
				OnPropertyChanged("IsCancelationWindowEnabled");
			}
		}
		[JsonProperty]
		public int MaxUses
		{
			get { return _MaxUses; }
			private set 
			{ 
				_MaxUses = value;
				OnPropertyChanged("MaxUses");
			}
		}
		[JsonProperty]
		public string UserId
		{
			get { return _UserId; }
			private set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		[JsonProperty]
		public long HouseholdId
		{
			get { return _HouseholdId; }
			private set 
			{ 
				_HouseholdId = value;
				OnPropertyChanged("HouseholdId");
			}
		}
		#endregion

		#region CTor
		public Entitlement()
		{
		}

		public Entitlement(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["productId"] != null)
			{
				this._ProductId = node["productId"].Value<string>();
			}
			if(node["currentUses"] != null)
			{
				this._CurrentUses = ParseInt(node["currentUses"].Value<string>());
			}
			if(node["endDate"] != null)
			{
				this._EndDate = ParseLong(node["endDate"].Value<string>());
			}
			if(node["currentDate"] != null)
			{
				this._CurrentDate = ParseLong(node["currentDate"].Value<string>());
			}
			if(node["lastViewDate"] != null)
			{
				this._LastViewDate = ParseLong(node["lastViewDate"].Value<string>());
			}
			if(node["purchaseDate"] != null)
			{
				this._PurchaseDate = ParseLong(node["purchaseDate"].Value<string>());
			}
			if(node["paymentMethod"] != null)
			{
				this._PaymentMethod = (PaymentMethodType)StringEnum.Parse(typeof(PaymentMethodType), node["paymentMethod"].Value<string>());
			}
			if(node["deviceUdid"] != null)
			{
				this._DeviceUdid = node["deviceUdid"].Value<string>();
			}
			if(node["deviceName"] != null)
			{
				this._DeviceName = node["deviceName"].Value<string>();
			}
			if(node["isCancelationWindowEnabled"] != null)
			{
				this._IsCancelationWindowEnabled = ParseBool(node["isCancelationWindowEnabled"].Value<string>());
			}
			if(node["maxUses"] != null)
			{
				this._MaxUses = ParseInt(node["maxUses"].Value<string>());
			}
			if(node["userId"] != null)
			{
				this._UserId = node["userId"].Value<string>();
			}
			if(node["householdId"] != null)
			{
				this._HouseholdId = ParseLong(node["householdId"].Value<string>());
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

