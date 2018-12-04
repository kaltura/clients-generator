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
using System.IO;
using Kaltura.Request;
using Kaltura.Types;
using Kaltura.Enums;

namespace Kaltura.Services
{
	public class NotificationRegisterRequestBuilder : RequestBuilder<RegistryResponse>
	{
		#region Constants
		public const string IDENTIFIER = "identifier";
		public const string TYPE = "type";
		#endregion

		public string Identifier
		{
			set;
			get;
		}
		public NotificationType Type
		{
			set;
			get;
		}

		public NotificationRegisterRequestBuilder()
			: base("notification", "register")
		{
		}

		public NotificationRegisterRequestBuilder(string identifier, NotificationType type)
			: this()
		{
			this.Identifier = identifier;
			this.Type = type;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("identifier"))
				kparams.AddIfNotNull("identifier", Identifier);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<RegistryResponse>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<RegistryResponse>((IDictionary<string,object>)result);
		}
	}

	public class NotificationSendPushRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public new const string USER_ID = "userId";
		public const string PUSH_MESSAGE = "pushMessage";
		#endregion

		public new int UserId
		{
			set;
			get;
		}
		public PushMessage PushMessage
		{
			set;
			get;
		}

		public NotificationSendPushRequestBuilder()
			: base("notification", "sendPush")
		{
		}

		public NotificationSendPushRequestBuilder(int userId, PushMessage pushMessage)
			: this()
		{
			this.UserId = userId;
			this.PushMessage = pushMessage;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("userId"))
				kparams.AddIfNotNull("userId", UserId);
			if (!isMapped("pushMessage"))
				kparams.AddIfNotNull("pushMessage", PushMessage);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			if (result.InnerText.Equals("1") || result.InnerText.ToLower().Equals("true"))
				return true;
			return false;
		}
		public override object DeserializeObject(object result)
		{
			var resultStr = (string)result;
			if (resultStr.Equals("1") || resultStr.ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class NotificationSendSmsRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string MESSAGE = "message";
		#endregion

		public string Message
		{
			set;
			get;
		}

		public NotificationSendSmsRequestBuilder()
			: base("notification", "sendSms")
		{
		}

		public NotificationSendSmsRequestBuilder(string message)
			: this()
		{
			this.Message = message;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("message"))
				kparams.AddIfNotNull("message", Message);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			if (result.InnerText.Equals("1") || result.InnerText.ToLower().Equals("true"))
				return true;
			return false;
		}
		public override object DeserializeObject(object result)
		{
			var resultStr = (string)result;
			if (resultStr.Equals("1") || resultStr.ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class NotificationSetDevicePushTokenRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PUSH_TOKEN = "pushToken";
		#endregion

		public string PushToken
		{
			set;
			get;
		}

		public NotificationSetDevicePushTokenRequestBuilder()
			: base("notification", "setDevicePushToken")
		{
		}

		public NotificationSetDevicePushTokenRequestBuilder(string pushToken)
			: this()
		{
			this.PushToken = pushToken;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("pushToken"))
				kparams.AddIfNotNull("pushToken", PushToken);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			if (result.InnerText.Equals("1") || result.InnerText.ToLower().Equals("true"))
				return true;
			return false;
		}
		public override object DeserializeObject(object result)
		{
			var resultStr = (string)result;
			if (resultStr.Equals("1") || resultStr.ToLower().Equals("true"))
				return true;
			return false;
		}
	}


	public class NotificationService
	{
		private NotificationService()
		{
		}

		public static NotificationRegisterRequestBuilder Register(string identifier, NotificationType type)
		{
			return new NotificationRegisterRequestBuilder(identifier, type);
		}

		public static NotificationSendPushRequestBuilder SendPush(int userId, PushMessage pushMessage)
		{
			return new NotificationSendPushRequestBuilder(userId, pushMessage);
		}

		public static NotificationSendSmsRequestBuilder SendSms(string message)
		{
			return new NotificationSendSmsRequestBuilder(message);
		}

		public static NotificationSetDevicePushTokenRequestBuilder SetDevicePushToken(string pushToken)
		{
			return new NotificationSetDevicePushTokenRequestBuilder(pushToken);
		}
	}
}
