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
	public class UserLoginPin : ObjectBase
	{
		#region Constants
		public const string PIN_CODE = "pinCode";
		public const string EXPIRATION_TIME = "expirationTime";
		public const string USER_ID = "userId";
		#endregion

		#region Private Fields
		private string _PinCode = null;
		private long _ExpirationTime = long.MinValue;
		private string _UserId = null;
		#endregion

		#region Properties
		public string PinCode
		{
			get { return _PinCode; }
			set 
			{ 
				_PinCode = value;
				OnPropertyChanged("PinCode");
			}
		}
		public long ExpirationTime
		{
			get { return _ExpirationTime; }
			set 
			{ 
				_ExpirationTime = value;
				OnPropertyChanged("ExpirationTime");
			}
		}
		public string UserId
		{
			get { return _UserId; }
		}
		#endregion

		#region CTor
		public UserLoginPin()
		{
		}

		public UserLoginPin(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "pinCode":
						this._PinCode = propertyNode.InnerText;
						continue;
					case "expirationTime":
						this._ExpirationTime = ParseLong(propertyNode.InnerText);
						continue;
					case "userId":
						this._UserId = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaUserLoginPin");
			kparams.AddIfNotNull("pinCode", this._PinCode);
			kparams.AddIfNotNull("expirationTime", this._ExpirationTime);
			kparams.AddIfNotNull("userId", this._UserId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PIN_CODE:
					return "PinCode";
				case EXPIRATION_TIME:
					return "ExpirationTime";
				case USER_ID:
					return "UserId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

