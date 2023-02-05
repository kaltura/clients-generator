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
	public class HouseholdLimitations : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string CONCURRENT_LIMIT = "concurrentLimit";
		public const string DEVICE_LIMIT = "deviceLimit";
		public const string DEVICE_FREQUENCY = "deviceFrequency";
		public const string DEVICE_FREQUENCY_DESCRIPTION = "deviceFrequencyDescription";
		public const string USER_FREQUENCY = "userFrequency";
		public const string USER_FREQUENCY_DESCRIPTION = "userFrequencyDescription";
		public const string NPVR_QUOTA_IN_SECONDS = "npvrQuotaInSeconds";
		public const string USERS_LIMIT = "usersLimit";
		public const string DEVICE_FAMILIES_LIMITATIONS = "deviceFamiliesLimitations";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _Name = null;
		private int _ConcurrentLimit = Int32.MinValue;
		private int _DeviceLimit = Int32.MinValue;
		private int _DeviceFrequency = Int32.MinValue;
		private string _DeviceFrequencyDescription = null;
		private int _UserFrequency = Int32.MinValue;
		private string _UserFrequencyDescription = null;
		private int _NpvrQuotaInSeconds = Int32.MinValue;
		private int _UsersLimit = Int32.MinValue;
		private IList<HouseholdDeviceFamilyLimitations> _DeviceFamiliesLimitations;
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
		public string Name
		{
			get { return _Name; }
			private set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
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
		public int DeviceLimit
		{
			get { return _DeviceLimit; }
			private set 
			{ 
				_DeviceLimit = value;
				OnPropertyChanged("DeviceLimit");
			}
		}
		[JsonProperty]
		public int DeviceFrequency
		{
			get { return _DeviceFrequency; }
			private set 
			{ 
				_DeviceFrequency = value;
				OnPropertyChanged("DeviceFrequency");
			}
		}
		[JsonProperty]
		public string DeviceFrequencyDescription
		{
			get { return _DeviceFrequencyDescription; }
			private set 
			{ 
				_DeviceFrequencyDescription = value;
				OnPropertyChanged("DeviceFrequencyDescription");
			}
		}
		[JsonProperty]
		public int UserFrequency
		{
			get { return _UserFrequency; }
			private set 
			{ 
				_UserFrequency = value;
				OnPropertyChanged("UserFrequency");
			}
		}
		[JsonProperty]
		public string UserFrequencyDescription
		{
			get { return _UserFrequencyDescription; }
			private set 
			{ 
				_UserFrequencyDescription = value;
				OnPropertyChanged("UserFrequencyDescription");
			}
		}
		[JsonProperty]
		public int NpvrQuotaInSeconds
		{
			get { return _NpvrQuotaInSeconds; }
			private set 
			{ 
				_NpvrQuotaInSeconds = value;
				OnPropertyChanged("NpvrQuotaInSeconds");
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
		public IList<HouseholdDeviceFamilyLimitations> DeviceFamiliesLimitations
		{
			get { return _DeviceFamiliesLimitations; }
			private set 
			{ 
				_DeviceFamiliesLimitations = value;
				OnPropertyChanged("DeviceFamiliesLimitations");
			}
		}
		#endregion

		#region CTor
		public HouseholdLimitations()
		{
		}

		public HouseholdLimitations(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["concurrentLimit"] != null)
			{
				this._ConcurrentLimit = ParseInt(node["concurrentLimit"].Value<string>());
			}
			if(node["deviceLimit"] != null)
			{
				this._DeviceLimit = ParseInt(node["deviceLimit"].Value<string>());
			}
			if(node["deviceFrequency"] != null)
			{
				this._DeviceFrequency = ParseInt(node["deviceFrequency"].Value<string>());
			}
			if(node["deviceFrequencyDescription"] != null)
			{
				this._DeviceFrequencyDescription = node["deviceFrequencyDescription"].Value<string>();
			}
			if(node["userFrequency"] != null)
			{
				this._UserFrequency = ParseInt(node["userFrequency"].Value<string>());
			}
			if(node["userFrequencyDescription"] != null)
			{
				this._UserFrequencyDescription = node["userFrequencyDescription"].Value<string>();
			}
			if(node["npvrQuotaInSeconds"] != null)
			{
				this._NpvrQuotaInSeconds = ParseInt(node["npvrQuotaInSeconds"].Value<string>());
			}
			if(node["usersLimit"] != null)
			{
				this._UsersLimit = ParseInt(node["usersLimit"].Value<string>());
			}
			if(node["deviceFamiliesLimitations"] != null)
			{
				this._DeviceFamiliesLimitations = new List<HouseholdDeviceFamilyLimitations>();
				foreach(var arrayNode in node["deviceFamiliesLimitations"].Children())
				{
					this._DeviceFamiliesLimitations.Add(ObjectFactory.Create<HouseholdDeviceFamilyLimitations>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaHouseholdLimitations");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("concurrentLimit", this._ConcurrentLimit);
			kparams.AddIfNotNull("deviceLimit", this._DeviceLimit);
			kparams.AddIfNotNull("deviceFrequency", this._DeviceFrequency);
			kparams.AddIfNotNull("deviceFrequencyDescription", this._DeviceFrequencyDescription);
			kparams.AddIfNotNull("userFrequency", this._UserFrequency);
			kparams.AddIfNotNull("userFrequencyDescription", this._UserFrequencyDescription);
			kparams.AddIfNotNull("npvrQuotaInSeconds", this._NpvrQuotaInSeconds);
			kparams.AddIfNotNull("usersLimit", this._UsersLimit);
			kparams.AddIfNotNull("deviceFamiliesLimitations", this._DeviceFamiliesLimitations);
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
				case CONCURRENT_LIMIT:
					return "ConcurrentLimit";
				case DEVICE_LIMIT:
					return "DeviceLimit";
				case DEVICE_FREQUENCY:
					return "DeviceFrequency";
				case DEVICE_FREQUENCY_DESCRIPTION:
					return "DeviceFrequencyDescription";
				case USER_FREQUENCY:
					return "UserFrequency";
				case USER_FREQUENCY_DESCRIPTION:
					return "UserFrequencyDescription";
				case NPVR_QUOTA_IN_SECONDS:
					return "NpvrQuotaInSeconds";
				case USERS_LIMIT:
					return "UsersLimit";
				case DEVICE_FAMILIES_LIMITATIONS:
					return "DeviceFamiliesLimitations";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

