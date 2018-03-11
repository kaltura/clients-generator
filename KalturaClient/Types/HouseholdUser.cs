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
	public class HouseholdUser : ObjectBase
	{
		#region Constants
		public const string HOUSEHOLD_ID = "householdId";
		public const string USER_ID = "userId";
		public const string IS_MASTER = "isMaster";
		public const string HOUSEHOLD_MASTER_USERNAME = "householdMasterUsername";
		public const string STATUS = "status";
		public const string IS_DEFAULT = "isDefault";
		#endregion

		#region Private Fields
		private int _HouseholdId = Int32.MinValue;
		private string _UserId = null;
		private bool? _IsMaster = null;
		private string _HouseholdMasterUsername = null;
		private HouseholdUserStatus _Status = null;
		private bool? _IsDefault = null;
		#endregion

		#region Properties
		public int HouseholdId
		{
			get { return _HouseholdId; }
			set 
			{ 
				_HouseholdId = value;
				OnPropertyChanged("HouseholdId");
			}
		}
		public string UserId
		{
			get { return _UserId; }
			set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		public bool? IsMaster
		{
			get { return _IsMaster; }
			set 
			{ 
				_IsMaster = value;
				OnPropertyChanged("IsMaster");
			}
		}
		public string HouseholdMasterUsername
		{
			get { return _HouseholdMasterUsername; }
			set 
			{ 
				_HouseholdMasterUsername = value;
				OnPropertyChanged("HouseholdMasterUsername");
			}
		}
		public HouseholdUserStatus Status
		{
			get { return _Status; }
		}
		public bool? IsDefault
		{
			get { return _IsDefault; }
		}
		#endregion

		#region CTor
		public HouseholdUser()
		{
		}

		public HouseholdUser(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "householdId":
						this._HouseholdId = ParseInt(propertyNode.InnerText);
						continue;
					case "userId":
						this._UserId = propertyNode.InnerText;
						continue;
					case "isMaster":
						this._IsMaster = ParseBool(propertyNode.InnerText);
						continue;
					case "householdMasterUsername":
						this._HouseholdMasterUsername = propertyNode.InnerText;
						continue;
					case "status":
						this._Status = (HouseholdUserStatus)StringEnum.Parse(typeof(HouseholdUserStatus), propertyNode.InnerText);
						continue;
					case "isDefault":
						this._IsDefault = ParseBool(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaHouseholdUser");
			kparams.AddIfNotNull("householdId", this._HouseholdId);
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("isMaster", this._IsMaster);
			kparams.AddIfNotNull("householdMasterUsername", this._HouseholdMasterUsername);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("isDefault", this._IsDefault);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case HOUSEHOLD_ID:
					return "HouseholdId";
				case USER_ID:
					return "UserId";
				case IS_MASTER:
					return "IsMaster";
				case HOUSEHOLD_MASTER_USERNAME:
					return "HouseholdMasterUsername";
				case STATUS:
					return "Status";
				case IS_DEFAULT:
					return "IsDefault";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

