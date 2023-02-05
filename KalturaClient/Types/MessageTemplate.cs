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
	public class MessageTemplate : ObjectBase
	{
		#region Constants
		public const string MESSAGE = "message";
		public const string DATE_FORMAT = "dateFormat";
		public const string MESSAGE_TYPE = "messageType";
		public const string SOUND = "sound";
		public const string ACTION = "action";
		public const string URL = "url";
		public const string MAIL_TEMPLATE = "mailTemplate";
		public const string MAIL_SUBJECT = "mailSubject";
		public const string RATIO_ID = "ratioId";
		#endregion

		#region Private Fields
		private string _Message = null;
		private string _DateFormat = null;
		private MessageTemplateType _MessageType = null;
		private string _Sound = null;
		private string _Action = null;
		private string _Url = null;
		private string _MailTemplate = null;
		private string _MailSubject = null;
		private string _RatioId = null;
		#endregion

		#region Properties
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
		public string DateFormat
		{
			get { return _DateFormat; }
			set 
			{ 
				_DateFormat = value;
				OnPropertyChanged("DateFormat");
			}
		}
		[JsonProperty]
		public MessageTemplateType MessageType
		{
			get { return _MessageType; }
			set 
			{ 
				_MessageType = value;
				OnPropertyChanged("MessageType");
			}
		}
		[JsonProperty]
		public string Sound
		{
			get { return _Sound; }
			set 
			{ 
				_Sound = value;
				OnPropertyChanged("Sound");
			}
		}
		[JsonProperty]
		public string Action
		{
			get { return _Action; }
			set 
			{ 
				_Action = value;
				OnPropertyChanged("Action");
			}
		}
		[JsonProperty]
		public string Url
		{
			get { return _Url; }
			set 
			{ 
				_Url = value;
				OnPropertyChanged("Url");
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
		public string RatioId
		{
			get { return _RatioId; }
			set 
			{ 
				_RatioId = value;
				OnPropertyChanged("RatioId");
			}
		}
		#endregion

		#region CTor
		public MessageTemplate()
		{
		}

		public MessageTemplate(JToken node) : base(node)
		{
			if(node["message"] != null)
			{
				this._Message = node["message"].Value<string>();
			}
			if(node["dateFormat"] != null)
			{
				this._DateFormat = node["dateFormat"].Value<string>();
			}
			if(node["messageType"] != null)
			{
				this._MessageType = (MessageTemplateType)StringEnum.Parse(typeof(MessageTemplateType), node["messageType"].Value<string>());
			}
			if(node["sound"] != null)
			{
				this._Sound = node["sound"].Value<string>();
			}
			if(node["action"] != null)
			{
				this._Action = node["action"].Value<string>();
			}
			if(node["url"] != null)
			{
				this._Url = node["url"].Value<string>();
			}
			if(node["mailTemplate"] != null)
			{
				this._MailTemplate = node["mailTemplate"].Value<string>();
			}
			if(node["mailSubject"] != null)
			{
				this._MailSubject = node["mailSubject"].Value<string>();
			}
			if(node["ratioId"] != null)
			{
				this._RatioId = node["ratioId"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaMessageTemplate");
			kparams.AddIfNotNull("message", this._Message);
			kparams.AddIfNotNull("dateFormat", this._DateFormat);
			kparams.AddIfNotNull("messageType", this._MessageType);
			kparams.AddIfNotNull("sound", this._Sound);
			kparams.AddIfNotNull("action", this._Action);
			kparams.AddIfNotNull("url", this._Url);
			kparams.AddIfNotNull("mailTemplate", this._MailTemplate);
			kparams.AddIfNotNull("mailSubject", this._MailSubject);
			kparams.AddIfNotNull("ratioId", this._RatioId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case MESSAGE:
					return "Message";
				case DATE_FORMAT:
					return "DateFormat";
				case MESSAGE_TYPE:
					return "MessageType";
				case SOUND:
					return "Sound";
				case ACTION:
					return "Action";
				case URL:
					return "Url";
				case MAIL_TEMPLATE:
					return "MailTemplate";
				case MAIL_SUBJECT:
					return "MailSubject";
				case RATIO_ID:
					return "RatioId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

