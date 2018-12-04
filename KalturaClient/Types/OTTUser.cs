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
		#endregion

		#region Properties
		public int HouseholdId
		{
			get { return _HouseholdId; }
		}
		public string Email
		{
			get { return _Email; }
			set 
			{ 
				_Email = value;
				OnPropertyChanged("Email");
			}
		}
		public string Address
		{
			get { return _Address; }
			set 
			{ 
				_Address = value;
				OnPropertyChanged("Address");
			}
		}
		public string City
		{
			get { return _City; }
			set 
			{ 
				_City = value;
				OnPropertyChanged("City");
			}
		}
		public int CountryId
		{
			get { return _CountryId; }
			set 
			{ 
				_CountryId = value;
				OnPropertyChanged("CountryId");
			}
		}
		public string Zip
		{
			get { return _Zip; }
			set 
			{ 
				_Zip = value;
				OnPropertyChanged("Zip");
			}
		}
		public string Phone
		{
			get { return _Phone; }
			set 
			{ 
				_Phone = value;
				OnPropertyChanged("Phone");
			}
		}
		public string AffiliateCode
		{
			get { return _AffiliateCode; }
			set 
			{ 
				_AffiliateCode = value;
				OnPropertyChanged("AffiliateCode");
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
		public OTTUserType UserType
		{
			get { return _UserType; }
			set 
			{ 
				_UserType = value;
				OnPropertyChanged("UserType");
			}
		}
		public IDictionary<string, StringValue> DynamicData
		{
			get { return _DynamicData; }
			set 
			{ 
				_DynamicData = value;
				OnPropertyChanged("DynamicData");
			}
		}
		public bool? IsHouseholdMaster
		{
			get { return _IsHouseholdMaster; }
		}
		public HouseholdSuspensionState SuspensionState
		{
			get { return _SuspensionState; }
		}
		public UserState UserState
		{
			get { return _UserState; }
		}
		public string RoleIds
		{
			get { return _RoleIds; }
			set 
			{ 
				_RoleIds = value;
				OnPropertyChanged("RoleIds");
			}
		}
		public long CreateDate
		{
			get { return _CreateDate; }
		}
		public long UpdateDate
		{
			get { return _UpdateDate; }
		}
		#endregion

		#region CTor
		public OTTUser()
		{
		}

		public OTTUser(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "householdId":
						this._HouseholdId = ParseInt(propertyNode.InnerText);
						continue;
					case "email":
						this._Email = propertyNode.InnerText;
						continue;
					case "address":
						this._Address = propertyNode.InnerText;
						continue;
					case "city":
						this._City = propertyNode.InnerText;
						continue;
					case "countryId":
						this._CountryId = ParseInt(propertyNode.InnerText);
						continue;
					case "zip":
						this._Zip = propertyNode.InnerText;
						continue;
					case "phone":
						this._Phone = propertyNode.InnerText;
						continue;
					case "affiliateCode":
						this._AffiliateCode = propertyNode.InnerText;
						continue;
					case "externalId":
						this._ExternalId = propertyNode.InnerText;
						continue;
					case "userType":
						this._UserType = ObjectFactory.Create<OTTUserType>(propertyNode);
						continue;
					case "dynamicData":
						{
							string key;
							this._DynamicData = new Dictionary<string, StringValue>();
							foreach(XmlElement arrayNode in propertyNode.ChildNodes)
							{
								key = arrayNode["itemKey"].InnerText;;
								this._DynamicData[key] = ObjectFactory.Create<StringValue>(arrayNode);
							}
						}
						continue;
					case "isHouseholdMaster":
						this._IsHouseholdMaster = ParseBool(propertyNode.InnerText);
						continue;
					case "suspensionState":
						this._SuspensionState = (HouseholdSuspensionState)StringEnum.Parse(typeof(HouseholdSuspensionState), propertyNode.InnerText);
						continue;
					case "userState":
						this._UserState = (UserState)StringEnum.Parse(typeof(UserState), propertyNode.InnerText);
						continue;
					case "roleIds":
						this._RoleIds = propertyNode.InnerText;
						continue;
					case "createDate":
						this._CreateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "updateDate":
						this._UpdateDate = ParseLong(propertyNode.InnerText);
						continue;
				}
			}
		}

		public OTTUser(IDictionary<string,object> data) : base(data)
		{
			    this._HouseholdId = data.TryGetValueSafe<int>("householdId");
			    this._Email = data.TryGetValueSafe<string>("email");
			    this._Address = data.TryGetValueSafe<string>("address");
			    this._City = data.TryGetValueSafe<string>("city");
			    this._CountryId = data.TryGetValueSafe<int>("countryId");
			    this._Zip = data.TryGetValueSafe<string>("zip");
			    this._Phone = data.TryGetValueSafe<string>("phone");
			    this._AffiliateCode = data.TryGetValueSafe<string>("affiliateCode");
			    this._ExternalId = data.TryGetValueSafe<string>("externalId");
			    this._UserType = ObjectFactory.Create<OTTUserType>(data.TryGetValueSafe<IDictionary<string,object>>("userType"));
			    this._DynamicData = new Dictionary<string, StringValue>();
			    foreach(var keyValuePair in data.TryGetValueSafe("dynamicData", new Dictionary<string, object>()))
			    {
			        this._DynamicData[keyValuePair.Key] = ObjectFactory.Create<StringValue>((IDictionary<string,object>)keyValuePair.Value);
				}
			    this._IsHouseholdMaster = data.TryGetValueSafe<bool>("isHouseholdMaster");
			    this._SuspensionState = (HouseholdSuspensionState)StringEnum.Parse(typeof(HouseholdSuspensionState), data.TryGetValueSafe<string>("suspensionState"));
			    this._UserState = (UserState)StringEnum.Parse(typeof(UserState), data.TryGetValueSafe<string>("userState"));
			    this._RoleIds = data.TryGetValueSafe<string>("roleIds");
			    this._CreateDate = data.TryGetValueSafe<long>("createDate");
			    this._UpdateDate = data.TryGetValueSafe<long>("updateDate");
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
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

