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
	public class Social : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string FIRST_NAME = "firstName";
		public const string LAST_NAME = "lastName";
		public const string EMAIL = "email";
		public const string GENDER = "gender";
		public const string USER_ID = "userId";
		public const string BIRTHDAY = "birthday";
		public const string STATUS = "status";
		public const string PICTURE_URL = "pictureUrl";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Name = null;
		private string _FirstName = null;
		private string _LastName = null;
		private string _Email = null;
		private string _Gender = null;
		private string _UserId = null;
		private string _Birthday = null;
		private string _Status = null;
		private string _PictureUrl = null;
		#endregion

		#region Properties
		public string Id
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
		public string FirstName
		{
			get { return _FirstName; }
			set 
			{ 
				_FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}
		public string LastName
		{
			get { return _LastName; }
			set 
			{ 
				_LastName = value;
				OnPropertyChanged("LastName");
			}
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
		public string Gender
		{
			get { return _Gender; }
			set 
			{ 
				_Gender = value;
				OnPropertyChanged("Gender");
			}
		}
		public string UserId
		{
			get { return _UserId; }
		}
		public string Birthday
		{
			get { return _Birthday; }
			set 
			{ 
				_Birthday = value;
				OnPropertyChanged("Birthday");
			}
		}
		public string Status
		{
			get { return _Status; }
		}
		public string PictureUrl
		{
			get { return _PictureUrl; }
			set 
			{ 
				_PictureUrl = value;
				OnPropertyChanged("PictureUrl");
			}
		}
		#endregion

		#region CTor
		public Social()
		{
		}

		public Social(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "firstName":
						this._FirstName = propertyNode.InnerText;
						continue;
					case "lastName":
						this._LastName = propertyNode.InnerText;
						continue;
					case "email":
						this._Email = propertyNode.InnerText;
						continue;
					case "gender":
						this._Gender = propertyNode.InnerText;
						continue;
					case "userId":
						this._UserId = propertyNode.InnerText;
						continue;
					case "birthday":
						this._Birthday = propertyNode.InnerText;
						continue;
					case "status":
						this._Status = propertyNode.InnerText;
						continue;
					case "pictureUrl":
						this._PictureUrl = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaSocial");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("firstName", this._FirstName);
			kparams.AddIfNotNull("lastName", this._LastName);
			kparams.AddIfNotNull("email", this._Email);
			kparams.AddIfNotNull("gender", this._Gender);
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("birthday", this._Birthday);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("pictureUrl", this._PictureUrl);
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
				case FIRST_NAME:
					return "FirstName";
				case LAST_NAME:
					return "LastName";
				case EMAIL:
					return "Email";
				case GENDER:
					return "Gender";
				case USER_ID:
					return "UserId";
				case BIRTHDAY:
					return "Birthday";
				case STATUS:
					return "Status";
				case PICTURE_URL:
					return "PictureUrl";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

