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
// Copyright (C) 2006-2019  Kaltura Inc.
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
		public const string INCLUDE_MAIL = "includeMail";
		public const string MAIL_TEMPLATE = "mailTemplate";
		public const string MAIL_SUBJECT = "mailSubject";
		public const string INCLUDE_SMS = "includeSms";
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
		private bool? _IncludeMail = null;
		private string _MailTemplate = null;
		private string _MailSubject = null;
		private bool? _IncludeSms = null;
		#endregion

		#region Properties
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
		public string Message
		{
			get { return _Message; }
			set 
			{ 
				_Message = value;
				OnPropertyChanged("Message");
			}
		}
		[JsonProperty]
		public bool? Enabled
		{
			get { return _Enabled; }
			set 
			{ 
				_Enabled = value;
				OnPropertyChanged("Enabled");
			}
		}
		[JsonProperty]
		public long StartTime
		{
			get { return _StartTime; }
			set 
			{ 
				_StartTime = value;
				OnPropertyChanged("StartTime");
			}
		}
		[JsonProperty]
		public string Timezone
		{
			get { return _Timezone; }
			set 
			{ 
				_Timezone = value;
				OnPropertyChanged("Timezone");
			}
		}
		[JsonProperty]
		public AnnouncementStatus Status
		{
			get { return _Status; }
			private set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
		public AnnouncementRecipientsType Recipients
		{
			get { return _Recipients; }
			set 
			{ 
				_Recipients = value;
				OnPropertyChanged("Recipients");
			}
		}
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
		public string ImageUrl
		{
			get { return _ImageUrl; }
			set 
			{ 
				_ImageUrl = value;
				OnPropertyChanged("ImageUrl");
			}
		}
		[JsonProperty]
		public bool? IncludeMail
		{
			get { return _IncludeMail; }
			set 
			{ 
				_IncludeMail = value;
				OnPropertyChanged("IncludeMail");
			}
		}
		[JsonProperty]
		public string MailTemplate
		{
			get { return _MailTemplate; }
			set 
			{ 
				_MailTemplate = value;
				OnPropertyChanged("MailTemplate");
			}
		}
		[JsonProperty]
		public string MailSubject
		{
			get { return _MailSubject; }
			set 
			{ 
				_MailSubject = value;
				OnPropertyChanged("MailSubject");
			}
		}
		[JsonProperty]
		public bool? IncludeSms
		{
			get { return _IncludeSms; }
			set 
			{ 
				_IncludeSms = value;
				OnPropertyChanged("IncludeSms");
			}
		}
		#endregion

		#region CTor
		public Announcement()
		{
		}

		public Announcement(JToken node) : base(node)
		{
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["message"] != null)
			{
				this._Message = node["message"].Value<string>();
			}
			if(node["enabled"] != null)
			{
				this._Enabled = ParseBool(node["enabled"].Value<string>());
			}
			if(node["startTime"] != null)
			{
				this._StartTime = ParseLong(node["startTime"].Value<string>());
			}
			if(node["timezone"] != null)
			{
				this._Timezone = node["timezone"].Value<string>();
			}
			if(node["status"] != null)
			{
				this._Status = (AnnouncementStatus)StringEnum.Parse(typeof(AnnouncementStatus), node["status"].Value<string>());
			}
			if(node["recipients"] != null)
			{
				this._Recipients = (AnnouncementRecipientsType)StringEnum.Parse(typeof(AnnouncementRecipientsType), node["recipients"].Value<string>());
			}
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["imageUrl"] != null)
			{
				this._ImageUrl = node["imageUrl"].Value<string>();
			}
			if(node["includeMail"] != null)
			{
				this._IncludeMail = ParseBool(node["includeMail"].Value<string>());
			}
			if(node["mailTemplate"] != null)
			{
				this._MailTemplate = node["mailTemplate"].Value<string>();
			}
			if(node["mailSubject"] != null)
			{
				this._MailSubject = node["mailSubject"].Value<string>();
			}
			if(node["includeSms"] != null)
			{
				this._IncludeSms = ParseBool(node["includeSms"].Value<string>());
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
			kparams.AddIfNotNull("includeMail", this._IncludeMail);
			kparams.AddIfNotNull("mailTemplate", this._MailTemplate);
			kparams.AddIfNotNull("mailSubject", this._MailSubject);
			kparams.AddIfNotNull("includeSms", this._IncludeSms);
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
				case INCLUDE_MAIL:
					return "IncludeMail";
				case MAIL_TEMPLATE:
					return "MailTemplate";
				case MAIL_SUBJECT:
					return "MailSubject";
				case INCLUDE_SMS:
					return "IncludeSms";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

