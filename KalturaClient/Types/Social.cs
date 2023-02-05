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
		[JsonProperty]
		public string Id
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
		public string FirstName
		{
			get { return _FirstName; }
			set 
			{ 
				_FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}
		[JsonProperty]
		public string LastName
		{
			get { return _LastName; }
			set 
			{ 
				_LastName = value;
				OnPropertyChanged("LastName");
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
		public string Gender
		{
			get { return _Gender; }
			set 
			{ 
				_Gender = value;
				OnPropertyChanged("Gender");
			}
		}
		[JsonProperty]
		public string UserId
		{
			get { return _UserId; }
			private set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		[JsonProperty]
		public string Birthday
		{
			get { return _Birthday; }
			set 
			{ 
				_Birthday = value;
				OnPropertyChanged("Birthday");
			}
		}
		[JsonProperty]
		public string Status
		{
			get { return _Status; }
			private set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
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

		public Social(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["firstName"] != null)
			{
				this._FirstName = node["firstName"].Value<string>();
			}
			if(node["lastName"] != null)
			{
				this._LastName = node["lastName"].Value<string>();
			}
			if(node["email"] != null)
			{
				this._Email = node["email"].Value<string>();
			}
			if(node["gender"] != null)
			{
				this._Gender = node["gender"].Value<string>();
			}
			if(node["userId"] != null)
			{
				this._UserId = node["userId"].Value<string>();
			}
			if(node["birthday"] != null)
			{
				this._Birthday = node["birthday"].Value<string>();
			}
			if(node["status"] != null)
			{
				this._Status = node["status"].Value<string>();
			}
			if(node["pictureUrl"] != null)
			{
				this._PictureUrl = node["pictureUrl"].Value<string>();
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

