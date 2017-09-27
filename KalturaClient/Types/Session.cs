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
	public class Session : ObjectBase
	{
		#region Constants
		public const string KS = "ks";
		public const string PARTNER_ID = "partnerId";
		public const string USER_ID = "userId";
		public const string EXPIRY = "expiry";
		public const string PRIVILEGES = "privileges";
		public const string UDID = "udid";
		public const string CREATE_DATE = "createDate";
		#endregion

		#region Private Fields
		private string _Ks = null;
		private int _PartnerId = Int32.MinValue;
		private string _UserId = null;
		private int _Expiry = Int32.MinValue;
		private string _Privileges = null;
		private string _Udid = null;
		private int _CreateDate = Int32.MinValue;
		#endregion

		#region Properties
		public string Ks
		{
			get { return _Ks; }
			set 
			{ 
				_Ks = value;
				OnPropertyChanged("Ks");
			}
		}
		public int PartnerId
		{
			get { return _PartnerId; }
			set 
			{ 
				_PartnerId = value;
				OnPropertyChanged("PartnerId");
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
		public int Expiry
		{
			get { return _Expiry; }
			set 
			{ 
				_Expiry = value;
				OnPropertyChanged("Expiry");
			}
		}
		public string Privileges
		{
			get { return _Privileges; }
			set 
			{ 
				_Privileges = value;
				OnPropertyChanged("Privileges");
			}
		}
		public string Udid
		{
			get { return _Udid; }
			set 
			{ 
				_Udid = value;
				OnPropertyChanged("Udid");
			}
		}
		public int CreateDate
		{
			get { return _CreateDate; }
			set 
			{ 
				_CreateDate = value;
				OnPropertyChanged("CreateDate");
			}
		}
		#endregion

		#region CTor
		public Session()
		{
		}

		public Session(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "ks":
						this._Ks = propertyNode.InnerText;
						continue;
					case "partnerId":
						this._PartnerId = ParseInt(propertyNode.InnerText);
						continue;
					case "userId":
						this._UserId = propertyNode.InnerText;
						continue;
					case "expiry":
						this._Expiry = ParseInt(propertyNode.InnerText);
						continue;
					case "privileges":
						this._Privileges = propertyNode.InnerText;
						continue;
					case "udid":
						this._Udid = propertyNode.InnerText;
						continue;
					case "createDate":
						this._CreateDate = ParseInt(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaSession");
			kparams.AddIfNotNull("ks", this._Ks);
			kparams.AddIfNotNull("partnerId", this._PartnerId);
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("expiry", this._Expiry);
			kparams.AddIfNotNull("privileges", this._Privileges);
			kparams.AddIfNotNull("udid", this._Udid);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case KS:
					return "Ks";
				case PARTNER_ID:
					return "PartnerId";
				case USER_ID:
					return "UserId";
				case EXPIRY:
					return "Expiry";
				case PRIVILEGES:
					return "Privileges";
				case UDID:
					return "Udid";
				case CREATE_DATE:
					return "CreateDate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

