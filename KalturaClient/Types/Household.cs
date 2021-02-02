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
		[JsonProperty]
		public long Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public string Description
		{
			get { return _Description; }
			set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		[JsonProperty]
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		[JsonProperty]
		public int HouseholdLimitationsId
		{
			get { return _HouseholdLimitationsId; }
			private set 
			{ 
				_HouseholdLimitationsId = value;
				OnPropertyChanged("HouseholdLimitationsId");
			}
		}
		[JsonProperty]
		public int DevicesLimit
		{
			get { return _DevicesLimit; }
			private set 
			{ 
				_DevicesLimit = value;
				OnPropertyChanged("DevicesLimit");
			}
		}
		[JsonProperty]
		public int UsersLimit
		{
			get { return _UsersLimit; }
			private set 
			{ 
				_UsersLimit = value;
				OnPropertyChanged("UsersLimit");
			}
		}
		[JsonProperty]
		public int ConcurrentLimit
		{
			get { return _ConcurrentLimit; }
			private set 
			{ 
				_ConcurrentLimit = value;
				OnPropertyChanged("ConcurrentLimit");
			}
		}
		[JsonProperty]
		public int RegionId
		{
			get { return _RegionId; }
			set 
			{ 
				_RegionId = value;
				OnPropertyChanged("RegionId");
			}
		}
		[JsonProperty]
		public HouseholdState State
		{
			get { return _State; }
			private set 
			{ 
				_State = value;
				OnPropertyChanged("State");
			}
		}
		[JsonProperty]
		public bool? IsFrequencyEnabled
		{
			get { return _IsFrequencyEnabled; }
			private set 
			{ 
				_IsFrequencyEnabled = value;
				OnPropertyChanged("IsFrequencyEnabled");
			}
		}
		[JsonProperty]
		public long FrequencyNextDeviceAction
		{
			get { return _FrequencyNextDeviceAction; }
			private set 
			{ 
				_FrequencyNextDeviceAction = value;
				OnPropertyChanged("FrequencyNextDeviceAction");
			}
		}
		[JsonProperty]
		public long FrequencyNextUserAction
		{
			get { return _FrequencyNextUserAction; }
			private set 
			{ 
				_FrequencyNextUserAction = value;
				OnPropertyChanged("FrequencyNextUserAction");
			}
		}
		[JsonProperty]
		public HouseholdRestriction Restriction
		{
			get { return _Restriction; }
			private set 
			{ 
				_Restriction = value;
				OnPropertyChanged("Restriction");
			}
		}
		[JsonProperty]
		public int RoleId
		{
			get { return _RoleId; }
			private set 
			{ 
				_RoleId = value;
				OnPropertyChanged("RoleId");
			}
		}
		#endregion

		#region CTor
		public Household()
		{
		}

		public Household(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["description"] != null)
			{
				this._Description = node["description"].Value<string>();
			}
			if(node["externalId"] != null)
			{
				this._ExternalId = node["externalId"].Value<string>();
			}
			if(node["householdLimitationsId"] != null)
			{
				this._HouseholdLimitationsId = ParseInt(node["householdLimitationsId"].Value<string>());
			}
			if(node["devicesLimit"] != null)
			{
				this._DevicesLimit = ParseInt(node["devicesLimit"].Value<string>());
			}
			if(node["usersLimit"] != null)
			{
				this._UsersLimit = ParseInt(node["usersLimit"].Value<string>());
			}
			if(node["concurrentLimit"] != null)
			{
				this._ConcurrentLimit = ParseInt(node["concurrentLimit"].Value<string>());
			}
			if(node["regionId"] != null)
			{
				this._RegionId = ParseInt(node["regionId"].Value<string>());
			}
			if(node["state"] != null)
			{
				this._State = (HouseholdState)StringEnum.Parse(typeof(HouseholdState), node["state"].Value<string>());
			}
			if(node["isFrequencyEnabled"] != null)
			{
				this._IsFrequencyEnabled = ParseBool(node["isFrequencyEnabled"].Value<string>());
			}
			if(node["frequencyNextDeviceAction"] != null)
			{
				this._FrequencyNextDeviceAction = ParseLong(node["frequencyNextDeviceAction"].Value<string>());
			}
			if(node["frequencyNextUserAction"] != null)
			{
				this._FrequencyNextUserAction = ParseLong(node["frequencyNextUserAction"].Value<string>());
			}
			if(node["restriction"] != null)
			{
				this._Restriction = (HouseholdRestriction)StringEnum.Parse(typeof(HouseholdRestriction), node["restriction"].Value<string>());
			}
			if(node["roleId"] != null)
			{
				this._RoleId = ParseInt(node["roleId"].Value<string>());
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

