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
	public class Announcement : ObjectBase
	{
		#region Constants
		public const string NAME = "name";
		public const string MESSAGE = "message";
		public const string ENABLED = "enabled";
		public const string START_TIME = "startTime";
		public const string TIMEZONE = "timezone";
		public const string STATUS = "status";
		public const string RECIPIENTS = "recipients";
		public const string ID = "id";
		public const string IMAGE_URL = "imageUrl";
		#endregion

		#region Private Fields
		private string _Name = null;
		private string _Message = null;
		private bool? _Enabled = null;
		private long _StartTime = long.MinValue;
		private string _Timezone = null;
		private AnnouncementStatus _Status = null;
		private AnnouncementRecipientsType _Recipients = null;
		private int _Id = Int32.MinValue;
		private string _ImageUrl = null;
		#endregion

		#region Properties
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		public string Message
		{
			get { return _Message; }
			set 
			{ 
				_Message = value;
				OnPropertyChanged("Message");
			}
		}
		public bool? Enabled
		{
			get { return _Enabled; }
			set 
			{ 
				_Enabled = value;
				OnPropertyChanged("Enabled");
			}
		}
		public long StartTime
		{
			get { return _StartTime; }
			set 
			{ 
				_StartTime = value;
				OnPropertyChanged("StartTime");
			}
		}
		public string Timezone
		{
			get { return _Timezone; }
			set 
			{ 
				_Timezone = value;
				OnPropertyChanged("Timezone");
			}
		}
		public AnnouncementStatus Status
		{
			get { return _Status; }
		}
		public AnnouncementRecipientsType Recipients
		{
			get { return _Recipients; }
			set 
			{ 
				_Recipients = value;
				OnPropertyChanged("Recipients");
			}
		}
		public int Id
		{
			get { return _Id; }
		}
		public string ImageUrl
		{
			get { return _ImageUrl; }
			set 
			{ 
				_ImageUrl = value;
				OnPropertyChanged("ImageUrl");
			}
		}
		#endregion

		#region CTor
		public Announcement()
		{
		}

		public Announcement(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "message":
						this._Message = propertyNode.InnerText;
						continue;
					case "enabled":
						this._Enabled = ParseBool(propertyNode.InnerText);
						continue;
					case "startTime":
						this._StartTime = ParseLong(propertyNode.InnerText);
						continue;
					case "timezone":
						this._Timezone = propertyNode.InnerText;
						continue;
					case "status":
						this._Status = (AnnouncementStatus)StringEnum.Parse(typeof(AnnouncementStatus), propertyNode.InnerText);
						continue;
					case "recipients":
						this._Recipients = (AnnouncementRecipientsType)StringEnum.Parse(typeof(AnnouncementRecipientsType), propertyNode.InnerText);
						continue;
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "imageUrl":
						this._ImageUrl = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaAnnouncement");
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("message", this._Message);
			kparams.AddIfNotNull("enabled", this._Enabled);
			kparams.AddIfNotNull("startTime", this._StartTime);
			kparams.AddIfNotNull("timezone", this._Timezone);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("recipients", this._Recipients);
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("imageUrl", this._ImageUrl);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NAME:
					return "Name";
				case MESSAGE:
					return "Message";
				case ENABLED:
					return "Enabled";
				case START_TIME:
					return "StartTime";
				case TIMEZONE:
					return "Timezone";
				case STATUS:
					return "Status";
				case RECIPIENTS:
					return "Recipients";
				case ID:
					return "Id";
				case IMAGE_URL:
					return "ImageUrl";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

