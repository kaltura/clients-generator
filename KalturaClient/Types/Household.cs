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
	public class Household : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string DESCRIPTION = "description";
		public const string EXTERNAL_ID = "externalId";
		public const string HOUSEHOLD_LIMITATIONS_ID = "householdLimitationsId";
		public const string DEVICES_LIMIT = "devicesLimit";
		public const string USERS_LIMIT = "usersLimit";
		public const string CONCURRENT_LIMIT = "concurrentLimit";
		public const string REGION_ID = "regionId";
		public const string STATE = "state";
		public const string IS_FREQUENCY_ENABLED = "isFrequencyEnabled";
		public const string FREQUENCY_NEXT_DEVICE_ACTION = "frequencyNextDeviceAction";
		public const string FREQUENCY_NEXT_USER_ACTION = "frequencyNextUserAction";
		public const string RESTRICTION = "restriction";
		public const string ROLE_ID = "roleId";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private string _Description = null;
		private string _ExternalId = null;
		private int _HouseholdLimitationsId = Int32.MinValue;
		private int _DevicesLimit = Int32.MinValue;
		private int _UsersLimit = Int32.MinValue;
		private int _ConcurrentLimit = Int32.MinValue;
		private int _RegionId = Int32.MinValue;
		private HouseholdState _State = null;
		private bool? _IsFrequencyEnabled = null;
		private long _FrequencyNextDeviceAction = long.MinValue;
		private long _FrequencyNextUserAction = long.MinValue;
		private HouseholdRestriction _Restriction = null;
		private int _RoleId = Int32.MinValue;
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
		public string Description
		{
			get { return _Description; }
			set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		public int HouseholdLimitationsId
		{
			get { return _HouseholdLimitationsId; }
		}
		public int DevicesLimit
		{
			get { return _DevicesLimit; }
		}
		public int UsersLimit
		{
			get { return _UsersLimit; }
		}
		public int ConcurrentLimit
		{
			get { return _ConcurrentLimit; }
		}
		public int RegionId
		{
			get { return _RegionId; }
		}
		public HouseholdState State
		{
			get { return _State; }
		}
		public bool? IsFrequencyEnabled
		{
			get { return _IsFrequencyEnabled; }
		}
		public long FrequencyNextDeviceAction
		{
			get { return _FrequencyNextDeviceAction; }
		}
		public long FrequencyNextUserAction
		{
			get { return _FrequencyNextUserAction; }
		}
		public HouseholdRestriction Restriction
		{
			get { return _Restriction; }
		}
		public int RoleId
		{
			get { return _RoleId; }
		}
		#endregion

		#region CTor
		public Household()
		{
		}

		public Household(XmlElement node) : base(node)
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
					case "description":
						this._Description = propertyNode.InnerText;
						continue;
					case "externalId":
						this._ExternalId = propertyNode.InnerText;
						continue;
					case "householdLimitationsId":
						this._HouseholdLimitationsId = ParseInt(propertyNode.InnerText);
						continue;
					case "devicesLimit":
						this._DevicesLimit = ParseInt(propertyNode.InnerText);
						continue;
					case "usersLimit":
						this._UsersLimit = ParseInt(propertyNode.InnerText);
						continue;
					case "concurrentLimit":
						this._ConcurrentLimit = ParseInt(propertyNode.InnerText);
						continue;
					case "regionId":
						this._RegionId = ParseInt(propertyNode.InnerText);
						continue;
					case "state":
						this._State = (HouseholdState)StringEnum.Parse(typeof(HouseholdState), propertyNode.InnerText);
						continue;
					case "isFrequencyEnabled":
						this._IsFrequencyEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "frequencyNextDeviceAction":
						this._FrequencyNextDeviceAction = ParseLong(propertyNode.InnerText);
						continue;
					case "frequencyNextUserAction":
						this._FrequencyNextUserAction = ParseLong(propertyNode.InnerText);
						continue;
					case "restriction":
						this._Restriction = (HouseholdRestriction)StringEnum.Parse(typeof(HouseholdRestriction), propertyNode.InnerText);
						continue;
					case "roleId":
						this._RoleId = ParseInt(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaHousehold");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("householdLimitationsId", this._HouseholdLimitationsId);
			kparams.AddIfNotNull("devicesLimit", this._DevicesLimit);
			kparams.AddIfNotNull("usersLimit", this._UsersLimit);
			kparams.AddIfNotNull("concurrentLimit", this._ConcurrentLimit);
			kparams.AddIfNotNull("regionId", this._RegionId);
			kparams.AddIfNotNull("state", this._State);
			kparams.AddIfNotNull("isFrequencyEnabled", this._IsFrequencyEnabled);
			kparams.AddIfNotNull("frequencyNextDeviceAction", this._FrequencyNextDeviceAction);
			kparams.AddIfNotNull("frequencyNextUserAction", this._FrequencyNextUserAction);
			kparams.AddIfNotNull("restriction", this._Restriction);
			kparams.AddIfNotNull("roleId", this._RoleId);
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
				case DESCRIPTION:
					return "Description";
				case EXTERNAL_ID:
					return "ExternalId";
				case HOUSEHOLD_LIMITATIONS_ID:
					return "HouseholdLimitationsId";
				case DEVICES_LIMIT:
					return "DevicesLimit";
				case USERS_LIMIT:
					return "UsersLimit";
				case CONCURRENT_LIMIT:
					return "ConcurrentLimit";
				case REGION_ID:
					return "RegionId";
				case STATE:
					return "State";
				case IS_FREQUENCY_ENABLED:
					return "IsFrequencyEnabled";
				case FREQUENCY_NEXT_DEVICE_ACTION:
					return "FrequencyNextDeviceAction";
				case FREQUENCY_NEXT_USER_ACTION:
					return "FrequencyNextUserAction";
				case RESTRICTION:
					return "Restriction";
				case ROLE_ID:
					return "RoleId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

