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
	public class OTTUser : BaseOTTUser
	{
		#region Constants
		public const string HOUSEHOLD_ID = "householdId";
		public const string EMAIL = "email";
		public const string ADDRESS = "address";
		public const string CITY = "city";
		public const string COUNTRY_ID = "countryId";
		public const string ZIP = "zip";
		public const string PHONE = "phone";
		public const string AFFILIATE_CODE = "affiliateCode";
		public const string EXTERNAL_ID = "externalId";
		public const string USER_TYPE = "userType";
		public const string DYNAMIC_DATA = "dynamicData";
		public const string IS_HOUSEHOLD_MASTER = "isHouseholdMaster";
		public const string SUSPENSION_STATE = "suspensionState";
		public const string USER_STATE = "userState";
		public const string ROLE_IDS = "roleIds";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		public const string LAST_LOGIN_DATE = "lastLoginDate";
		public const string FAILED_LOGIN_COUNT = "failedLoginCount";
		#endregion

		#region Private Fields
		private int _HouseholdId = Int32.MinValue;
		private string _Email = null;
		private string _Address = null;
		private string _City = null;
		private int _CountryId = Int32.MinValue;
		private string _Zip = null;
		private string _Phone = null;
		private string _AffiliateCode = null;
		private string _ExternalId = null;
		private OTTUserType _UserType;
		private IDictionary<string, StringValue> _DynamicData;
		private bool? _IsHouseholdMaster = null;
		private HouseholdSuspensionState _SuspensionState = null;
		private UserState _UserState = null;
		private string _RoleIds = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		private long _LastLoginDate = long.MinValue;
		private int _FailedLoginCount = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public int HouseholdId
		{
			get { return _HouseholdId; }
			private set 
			{ 
				_HouseholdId = value;
				OnPropertyChanged("HouseholdId");
			}
		}
		[JsonProperty]
		public string Email
		{
			get { return _Email; }
			set 
			{ 
				_Email = value;
				OnPropertyChanged("Email");
			}
		}
		[JsonProperty]
		public string Address
		{
			get { return _Address; }
			set 
			{ 
				_Address = value;
				OnPropertyChanged("Address");
			}
		}
		[JsonProperty]
		public string City
		{
			get { return _City; }
			set 
			{ 
				_City = value;
				OnPropertyChanged("City");
			}
		}
		[JsonProperty]
		public int CountryId
		{
			get { return _CountryId; }
			set 
			{ 
				_CountryId = value;
				OnPropertyChanged("CountryId");
			}
		}
		[JsonProperty]
		public string Zip
		{
			get { return _Zip; }
			set 
			{ 
				_Zip = value;
				OnPropertyChanged("Zip");
			}
		}
		[JsonProperty]
		public string Phone
		{
			get { return _Phone; }
			set 
			{ 
				_Phone = value;
				OnPropertyChanged("Phone");
			}
		}
		[JsonProperty]
		public string AffiliateCode
		{
			get { return _AffiliateCode; }
			set 
			{ 
				_AffiliateCode = value;
				OnPropertyChanged("AffiliateCode");
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
		public OTTUserType UserType
		{
			get { return _UserType; }
			set 
			{ 
				_UserType = value;
				OnPropertyChanged("UserType");
			}
		}
		[JsonProperty]
		public IDictionary<string, StringValue> DynamicData
		{
			get { return _DynamicData; }
			set 
			{ 
				_DynamicData = value;
				OnPropertyChanged("DynamicData");
			}
		}
		[JsonProperty]
		public bool? IsHouseholdMaster
		{
			get { return _IsHouseholdMaster; }
			private set 
			{ 
				_IsHouseholdMaster = value;
				OnPropertyChanged("IsHouseholdMaster");
			}
		}
		[JsonProperty]
		public HouseholdSuspensionState SuspensionState
		{
			get { return _SuspensionState; }
			private set 
			{ 
				_SuspensionState = value;
				OnPropertyChanged("SuspensionState");
			}
		}
		[JsonProperty]
		public UserState UserState
		{
			get { return _UserState; }
			private set 
			{ 
				_UserState = value;
				OnPropertyChanged("UserState");
			}
		}
		[JsonProperty]
		public string RoleIds
		{
			get { return _RoleIds; }
			set 
			{ 
				_RoleIds = value;
				OnPropertyChanged("RoleIds");
			}
		}
		[JsonProperty]
		public long CreateDate
		{
			get { return _CreateDate; }
			private set 
			{ 
				_CreateDate = value;
				OnPropertyChanged("CreateDate");
			}
		}
		[JsonProperty]
		public long UpdateDate
		{
			get { return _UpdateDate; }
			private set 
			{ 
				_UpdateDate = value;
				OnPropertyChanged("UpdateDate");
			}
		}
		[JsonProperty]
		public long LastLoginDate
		{
			get { return _LastLoginDate; }
			private set 
			{ 
				_LastLoginDate = value;
				OnPropertyChanged("LastLoginDate");
			}
		}
		[JsonProperty]
		public int FailedLoginCount
		{
			get { return _FailedLoginCount; }
			private set 
			{ 
				_FailedLoginCount = value;
				OnPropertyChanged("FailedLoginCount");
			}
		}
		#endregion

		#region CTor
		public OTTUser()
		{
		}

		public OTTUser(JToken node) : base(node)
		{
			if(node["householdId"] != null)
			{
				this._HouseholdId = ParseInt(node["householdId"].Value<string>());
			}
			if(node["email"] != null)
			{
				this._Email = node["email"].Value<string>();
			}
			if(node["address"] != null)
			{
				this._Address = node["address"].Value<string>();
			}
			if(node["city"] != null)
			{
				this._City = node["city"].Value<string>();
			}
			if(node["countryId"] != null)
			{
				this._CountryId = ParseInt(node["countryId"].Value<string>());
			}
			if(node["zip"] != null)
			{
				this._Zip = node["zip"].Value<string>();
			}
			if(node["phone"] != null)
			{
				this._Phone = node["phone"].Value<string>();
			}
			if(node["affiliateCode"] != null)
			{
				this._AffiliateCode = node["affiliateCode"].Value<string>();
			}
			if(node["externalId"] != null)
			{
				this._ExternalId = node["externalId"].Value<string>();
			}
			if(node["userType"] != null)
			{
				this._UserType = ObjectFactory.Create<OTTUserType>(node["userType"]);
			}
			if(node["dynamicData"] != null)
			{
				{
					string key;
					this._DynamicData = new Dictionary<string, StringValue>();
					foreach(var arrayNode in node["dynamicData"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._DynamicData[key] = ObjectFactory.Create<StringValue>(arrayNode.Value);
					}
				}
			}
			if(node["isHouseholdMaster"] != null)
			{
				this._IsHouseholdMaster = ParseBool(node["isHouseholdMaster"].Value<string>());
			}
			if(node["suspensionState"] != null)
			{
				this._SuspensionState = (HouseholdSuspensionState)StringEnum.Parse(typeof(HouseholdSuspensionState), node["suspensionState"].Value<string>());
			}
			if(node["userState"] != null)
			{
				this._UserState = (UserState)StringEnum.Parse(typeof(UserState), node["userState"].Value<string>());
			}
			if(node["roleIds"] != null)
			{
				this._RoleIds = node["roleIds"].Value<string>();
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
			if(node["lastLoginDate"] != null)
			{
				this._LastLoginDate = ParseLong(node["lastLoginDate"].Value<string>());
			}
			if(node["failedLoginCount"] != null)
			{
				this._FailedLoginCount = ParseInt(node["failedLoginCount"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaOTTUser");
			kparams.AddIfNotNull("householdId", this._HouseholdId);
			kparams.AddIfNotNull("email", this._Email);
			kparams.AddIfNotNull("address", this._Address);
			kparams.AddIfNotNull("city", this._City);
			kparams.AddIfNotNull("countryId", this._CountryId);
			kparams.AddIfNotNull("zip", this._Zip);
			kparams.AddIfNotNull("phone", this._Phone);
			kparams.AddIfNotNull("affiliateCode", this._AffiliateCode);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("userType", this._UserType);
			kparams.AddIfNotNull("dynamicData", this._DynamicData);
			kparams.AddIfNotNull("isHouseholdMaster", this._IsHouseholdMaster);
			kparams.AddIfNotNull("suspensionState", this._SuspensionState);
			kparams.AddIfNotNull("userState", this._UserState);
			kparams.AddIfNotNull("roleIds", this._RoleIds);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("lastLoginDate", this._LastLoginDate);
			kparams.AddIfNotNull("failedLoginCount", this._FailedLoginCount);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case HOUSEHOLD_ID:
					return "HouseholdId";
				case EMAIL:
					return "Email";
				case ADDRESS:
					return "Address";
				case CITY:
					return "City";
				case COUNTRY_ID:
					return "CountryId";
				case ZIP:
					return "Zip";
				case PHONE:
					return "Phone";
				case AFFILIATE_CODE:
					return "AffiliateCode";
				case EXTERNAL_ID:
					return "ExternalId";
				case USER_TYPE:
					return "UserType";
				case DYNAMIC_DATA:
					return "DynamicData";
				case IS_HOUSEHOLD_MASTER:
					return "IsHouseholdMaster";
				case SUSPENSION_STATE:
					return "SuspensionState";
				case USER_STATE:
					return "UserState";
				case ROLE_IDS:
					return "RoleIds";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				case LAST_LOGIN_DATE:
					return "LastLoginDate";
				case FAILED_LOGIN_COUNT:
					return "FailedLoginCount";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

