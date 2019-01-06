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
		public int Id
		{
			get { return _Id; }
		}
		public string Name
		{
			get { return _Name; }
		}
		public int ConcurrentLimit
		{
			get { return _ConcurrentLimit; }
		}
		public int DeviceLimit
		{
			get { return _DeviceLimit; }
		}
		public int DeviceFrequency
		{
			get { return _DeviceFrequency; }
		}
		public string DeviceFrequencyDescription
		{
			get { return _DeviceFrequencyDescription; }
		}
		public int UserFrequency
		{
			get { return _UserFrequency; }
		}
		public string UserFrequencyDescription
		{
			get { return _UserFrequencyDescription; }
		}
		public int NpvrQuotaInSeconds
		{
			get { return _NpvrQuotaInSeconds; }
		}
		public int UsersLimit
		{
			get { return _UsersLimit; }
		}
		public IList<HouseholdDeviceFamilyLimitations> DeviceFamiliesLimitations
		{
			get { return _DeviceFamiliesLimitations; }
		}
		#endregion

		#region CTor
		public HouseholdLimitations()
		{
		}

		public HouseholdLimitations(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "concurrentLimit":
						this._ConcurrentLimit = ParseInt(propertyNode.InnerText);
						continue;
					case "deviceLimit":
						this._DeviceLimit = ParseInt(propertyNode.InnerText);
						continue;
					case "deviceFrequency":
						this._DeviceFrequency = ParseInt(propertyNode.InnerText);
						continue;
					case "deviceFrequencyDescription":
						this._DeviceFrequencyDescription = propertyNode.InnerText;
						continue;
					case "userFrequency":
						this._UserFrequency = ParseInt(propertyNode.InnerText);
						continue;
					case "userFrequencyDescription":
						this._UserFrequencyDescription = propertyNode.InnerText;
						continue;
					case "npvrQuotaInSeconds":
						this._NpvrQuotaInSeconds = ParseInt(propertyNode.InnerText);
						continue;
					case "usersLimit":
						this._UsersLimit = ParseInt(propertyNode.InnerText);
						continue;
					case "deviceFamiliesLimitations":
						this._DeviceFamiliesLimitations = new List<HouseholdDeviceFamilyLimitations>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._DeviceFamiliesLimitations.Add(ObjectFactory.Create<HouseholdDeviceFamilyLimitations>(arrayNode));
						}
						continue;
				}
			}
		}

		public HouseholdLimitations(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<int>("id");
			    this._Name = data.TryGetValueSafe<string>("name");
			    this._ConcurrentLimit = data.TryGetValueSafe<int>("concurrentLimit");
			    this._DeviceLimit = data.TryGetValueSafe<int>("deviceLimit");
			    this._DeviceFrequency = data.TryGetValueSafe<int>("deviceFrequency");
			    this._DeviceFrequencyDescription = data.TryGetValueSafe<string>("deviceFrequencyDescription");
			    this._UserFrequency = data.TryGetValueSafe<int>("userFrequency");
			    this._UserFrequencyDescription = data.TryGetValueSafe<string>("userFrequencyDescription");
			    this._NpvrQuotaInSeconds = data.TryGetValueSafe<int>("npvrQuotaInSeconds");
			    this._UsersLimit = data.TryGetValueSafe<int>("usersLimit");
			    this._DeviceFamiliesLimitations = new List<HouseholdDeviceFamilyLimitations>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("deviceFamiliesLimitations", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._DeviceFamiliesLimitations.Add(ObjectFactory.Create<HouseholdDeviceFamilyLimitations>((IDictionary<string,object>)dataDictionary));
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

