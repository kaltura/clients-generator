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
	public class NotificationsPartnerSettings : ObjectBase
	{
		#region Constants
		public const string PUSH_NOTIFICATION_ENABLED = "pushNotificationEnabled";
		public const string PUSH_SYSTEM_ANNOUNCEMENTS_ENABLED = "pushSystemAnnouncementsEnabled";
		public const string PUSH_START_HOUR = "pushStartHour";
		public const string PUSH_END_HOUR = "pushEndHour";
		public const string INBOX_ENABLED = "inboxEnabled";
		public const string MESSAGE_TTL_DAYS = "messageTTLDays";
		public const string AUTOMATIC_ISSUE_FOLLOW_NOTIFICATION = "automaticIssueFollowNotification";
		public const string TOPIC_EXPIRATION_DURATION_DAYS = "topicExpirationDurationDays";
		public const string REMINDER_ENABLED = "reminderEnabled";
		public const string REMINDER_OFFSET_SEC = "reminderOffsetSec";
		public const string PUSH_ADAPTER_URL = "pushAdapterUrl";
		public const string CHURN_MAIL_TEMPLATE_NAME = "churnMailTemplateName";
		public const string CHURN_MAIL_SUBJECT = "churnMailSubject";
		public const string SENDER_EMAIL = "senderEmail";
		public const string MAIL_SENDER_NAME = "mailSenderName";
		public const string MAIL_NOTIFICATION_ADAPTER_ID = "mailNotificationAdapterId";
		public const string SMS_ENABLED = "smsEnabled";
		#endregion

		#region Private Fields
		private bool? _PushNotificationEnabled = null;
		private bool? _PushSystemAnnouncementsEnabled = null;
		private int _PushStartHour = Int32.MinValue;
		private int _PushEndHour = Int32.MinValue;
		private bool? _InboxEnabled = null;
		private int _MessageTTLDays = Int32.MinValue;
		private bool? _AutomaticIssueFollowNotification = null;
		private int _TopicExpirationDurationDays = Int32.MinValue;
		private bool? _ReminderEnabled = null;
		private int _ReminderOffsetSec = Int32.MinValue;
		private string _PushAdapterUrl = null;
		private string _ChurnMailTemplateName = null;
		private string _ChurnMailSubject = null;
		private string _SenderEmail = null;
		private string _MailSenderName = null;
		private long _MailNotificationAdapterId = long.MinValue;
		private bool? _SmsEnabled = null;
		#endregion

		#region Properties
		[JsonProperty]
		public bool? PushNotificationEnabled
		{
			get { return _PushNotificationEnabled; }
			set 
			{ 
				_PushNotificationEnabled = value;
				OnPropertyChanged("PushNotificationEnabled");
			}
		}
		[JsonProperty]
		public bool? PushSystemAnnouncementsEnabled
		{
			get { return _PushSystemAnnouncementsEnabled; }
			set 
			{ 
				_PushSystemAnnouncementsEnabled = value;
				OnPropertyChanged("PushSystemAnnouncementsEnabled");
			}
		}
		[JsonProperty]
		public int PushStartHour
		{
			get { return _PushStartHour; }
			set 
			{ 
				_PushStartHour = value;
				OnPropertyChanged("PushStartHour");
			}
		}
		[JsonProperty]
		public int PushEndHour
		{
			get { return _PushEndHour; }
			set 
			{ 
				_PushEndHour = value;
				OnPropertyChanged("PushEndHour");
			}
		}
		[JsonProperty]
		public bool? InboxEnabled
		{
			get { return _InboxEnabled; }
			set 
			{ 
				_InboxEnabled = value;
				OnPropertyChanged("InboxEnabled");
			}
		}
		[JsonProperty]
		public int MessageTTLDays
		{
			get { return _MessageTTLDays; }
			set 
			{ 
				_MessageTTLDays = value;
				OnPropertyChanged("MessageTTLDays");
			}
		}
		[JsonProperty]
		public bool? AutomaticIssueFollowNotification
		{
			get { return _AutomaticIssueFollowNotification; }
			set 
			{ 
				_AutomaticIssueFollowNotification = value;
				OnPropertyChanged("AutomaticIssueFollowNotification");
			}
		}
		[JsonProperty]
		public int TopicExpirationDurationDays
		{
			get { return _TopicExpirationDurationDays; }
			set 
			{ 
				_TopicExpirationDurationDays = value;
				OnPropertyChanged("TopicExpirationDurationDays");
			}
		}
		[JsonProperty]
		public bool? ReminderEnabled
		{
			get { return _ReminderEnabled; }
			set 
			{ 
				_ReminderEnabled = value;
				OnPropertyChanged("ReminderEnabled");
			}
		}
		[JsonProperty]
		public int ReminderOffsetSec
		{
			get { return _ReminderOffsetSec; }
			set 
			{ 
				_ReminderOffsetSec = value;
				OnPropertyChanged("ReminderOffsetSec");
			}
		}
		[JsonProperty]
		public string PushAdapterUrl
		{
			get { return _PushAdapterUrl; }
			set 
			{ 
				_PushAdapterUrl = value;
				OnPropertyChanged("PushAdapterUrl");
			}
		}
		[JsonProperty]
		public string ChurnMailTemplateName
		{
			get { return _ChurnMailTemplateName; }
			set 
			{ 
				_ChurnMailTemplateName = value;
				OnPropertyChanged("ChurnMailTemplateName");
			}
		}
		[JsonProperty]
		public string ChurnMailSubject
		{
			get { return _ChurnMailSubject; }
			set 
			{ 
				_ChurnMailSubject = value;
				OnPropertyChanged("ChurnMailSubject");
			}
		}
		[JsonProperty]
		public string SenderEmail
		{
			get { return _SenderEmail; }
			set 
			{ 
				_SenderEmail = value;
				OnPropertyChanged("SenderEmail");
			}
		}
		[JsonProperty]
		public string MailSenderName
		{
			get { return _MailSenderName; }
			set 
			{ 
				_MailSenderName = value;
				OnPropertyChanged("MailSenderName");
			}
		}
		[JsonProperty]
		public long MailNotificationAdapterId
		{
			get { return _MailNotificationAdapterId; }
			set 
			{ 
				_MailNotificationAdapterId = value;
				OnPropertyChanged("MailNotificationAdapterId");
			}
		}
		[JsonProperty]
		public bool? SmsEnabled
		{
			get { return _SmsEnabled; }
			set 
			{ 
				_SmsEnabled = value;
				OnPropertyChanged("SmsEnabled");
			}
		}
		#endregion

		#region CTor
		public NotificationsPartnerSettings()
		{
		}

		public NotificationsPartnerSettings(JToken node) : base(node)
		{
			if(node["pushNotificationEnabled"] != null)
			{
				this._PushNotificationEnabled = ParseBool(node["pushNotificationEnabled"].Value<string>());
			}
			if(node["pushSystemAnnouncementsEnabled"] != null)
			{
				this._PushSystemAnnouncementsEnabled = ParseBool(node["pushSystemAnnouncementsEnabled"].Value<string>());
			}
			if(node["pushStartHour"] != null)
			{
				this._PushStartHour = ParseInt(node["pushStartHour"].Value<string>());
			}
			if(node["pushEndHour"] != null)
			{
				this._PushEndHour = ParseInt(node["pushEndHour"].Value<string>());
			}
			if(node["inboxEnabled"] != null)
			{
				this._InboxEnabled = ParseBool(node["inboxEnabled"].Value<string>());
			}
			if(node["messageTTLDays"] != null)
			{
				this._MessageTTLDays = ParseInt(node["messageTTLDays"].Value<string>());
			}
			if(node["automaticIssueFollowNotification"] != null)
			{
				this._AutomaticIssueFollowNotification = ParseBool(node["automaticIssueFollowNotification"].Value<string>());
			}
			if(node["topicExpirationDurationDays"] != null)
			{
				this._TopicExpirationDurationDays = ParseInt(node["topicExpirationDurationDays"].Value<string>());
			}
			if(node["reminderEnabled"] != null)
			{
				this._ReminderEnabled = ParseBool(node["reminderEnabled"].Value<string>());
			}
			if(node["reminderOffsetSec"] != null)
			{
				this._ReminderOffsetSec = ParseInt(node["reminderOffsetSec"].Value<string>());
			}
			if(node["pushAdapterUrl"] != null)
			{
				this._PushAdapterUrl = node["pushAdapterUrl"].Value<string>();
			}
			if(node["churnMailTemplateName"] != null)
			{
				this._ChurnMailTemplateName = node["churnMailTemplateName"].Value<string>();
			}
			if(node["churnMailSubject"] != null)
			{
				this._ChurnMailSubject = node["churnMailSubject"].Value<string>();
			}
			if(node["senderEmail"] != null)
			{
				this._SenderEmail = node["senderEmail"].Value<string>();
			}
			if(node["mailSenderName"] != null)
			{
				this._MailSenderName = node["mailSenderName"].Value<string>();
			}
			if(node["mailNotificationAdapterId"] != null)
			{
				this._MailNotificationAdapterId = ParseLong(node["mailNotificationAdapterId"].Value<string>());
			}
			if(node["smsEnabled"] != null)
			{
				this._SmsEnabled = ParseBool(node["smsEnabled"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaNotificationsPartnerSettings");
			kparams.AddIfNotNull("pushNotificationEnabled", this._PushNotificationEnabled);
			kparams.AddIfNotNull("pushSystemAnnouncementsEnabled", this._PushSystemAnnouncementsEnabled);
			kparams.AddIfNotNull("pushStartHour", this._PushStartHour);
			kparams.AddIfNotNull("pushEndHour", this._PushEndHour);
			kparams.AddIfNotNull("inboxEnabled", this._InboxEnabled);
			kparams.AddIfNotNull("messageTTLDays", this._MessageTTLDays);
			kparams.AddIfNotNull("automaticIssueFollowNotification", this._AutomaticIssueFollowNotification);
			kparams.AddIfNotNull("topicExpirationDurationDays", this._TopicExpirationDurationDays);
			kparams.AddIfNotNull("reminderEnabled", this._ReminderEnabled);
			kparams.AddIfNotNull("reminderOffsetSec", this._ReminderOffsetSec);
			kparams.AddIfNotNull("pushAdapterUrl", this._PushAdapterUrl);
			kparams.AddIfNotNull("churnMailTemplateName", this._ChurnMailTemplateName);
			kparams.AddIfNotNull("churnMailSubject", this._ChurnMailSubject);
			kparams.AddIfNotNull("senderEmail", this._SenderEmail);
			kparams.AddIfNotNull("mailSenderName", this._MailSenderName);
			kparams.AddIfNotNull("mailNotificationAdapterId", this._MailNotificationAdapterId);
			kparams.AddIfNotNull("smsEnabled", this._SmsEnabled);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PUSH_NOTIFICATION_ENABLED:
					return "PushNotificationEnabled";
				case PUSH_SYSTEM_ANNOUNCEMENTS_ENABLED:
					return "PushSystemAnnouncementsEnabled";
				case PUSH_START_HOUR:
					return "PushStartHour";
				case PUSH_END_HOUR:
					return "PushEndHour";
				case INBOX_ENABLED:
					return "InboxEnabled";
				case MESSAGE_TTL_DAYS:
					return "MessageTTLDays";
				case AUTOMATIC_ISSUE_FOLLOW_NOTIFICATION:
					return "AutomaticIssueFollowNotification";
				case TOPIC_EXPIRATION_DURATION_DAYS:
					return "TopicExpirationDurationDays";
				case REMINDER_ENABLED:
					return "ReminderEnabled";
				case REMINDER_OFFSET_SEC:
					return "ReminderOffsetSec";
				case PUSH_ADAPTER_URL:
					return "PushAdapterUrl";
				case CHURN_MAIL_TEMPLATE_NAME:
					return "ChurnMailTemplateName";
				case CHURN_MAIL_SUBJECT:
					return "ChurnMailSubject";
				case SENDER_EMAIL:
					return "SenderEmail";
				case MAIL_SENDER_NAME:
					return "MailSenderName";
				case MAIL_NOTIFICATION_ADAPTER_ID:
					return "MailNotificationAdapterId";
				case SMS_ENABLED:
					return "SmsEnabled";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

