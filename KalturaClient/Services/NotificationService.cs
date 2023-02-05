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
using System.IO;
using Kaltura.Request;
using Kaltura.Types;
using Kaltura.Enums;
using Newtonsoft.Json.Linq;

namespace Kaltura.Services
{
	public class NotificationRegisterRequestBuilder : RequestBuilder<RegistryResponse>
	{
		#region Constants
		public const string IDENTIFIER = "identifier";
		public const string TYPE = "type";
		#endregion

		public string Identifier { get; set; }
		public NotificationType Type { get; set; }

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

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<RegistryResponse>(result);
		}
	}

	public class NotificationSendPushRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public new const string USER_ID = "userId";
		public const string PUSH_MESSAGE = "pushMessage";
		#endregion

		public new int UserId { get; set; }
		public PushMessage PushMessage { get; set; }

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

		public override object Deserialize(JToken result)
		{
			if (result.Value<string>().Equals("1") || result.Value<string>().ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class NotificationSendSmsRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string MESSAGE = "message";
		public const string PHONE_NUMBER = "phoneNumber";
		public const string ADAPTER_DATA = "adapterData";
		#endregion

		public string Message { get; set; }
		public string PhoneNumber { get; set; }
		public IDictionary<string, StringValue> AdapterData { get; set; }

		public NotificationSendSmsRequestBuilder()
			: base("notification", "sendSms")
		{
		}

		public NotificationSendSmsRequestBuilder(string message, string phoneNumber, IDictionary<string, StringValue> adapterData)
			: this()
		{
			this.Message = message;
			this.PhoneNumber = phoneNumber;
			this.AdapterData = adapterData;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("message"))
				kparams.AddIfNotNull("message", Message);
			if (!isMapped("phoneNumber"))
				kparams.AddIfNotNull("phoneNumber", PhoneNumber);
			if (!isMapped("adapterData"))
				kparams.AddIfNotNull("adapterData", AdapterData);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			if (result.Value<string>().Equals("1") || result.Value<string>().ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class NotificationSetDevicePushTokenRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PUSH_TOKEN = "pushToken";
		#endregion

		public string PushToken { get; set; }

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

		public override object Deserialize(JToken result)
		{
			if (result.Value<string>().Equals("1") || result.Value<string>().ToLower().Equals("true"))
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

		public static NotificationSendSmsRequestBuilder SendSms(string message, string phoneNumber = null, IDictionary<string, StringValue> adapterData = null)
		{
			return new NotificationSendSmsRequestBuilder(message, phoneNumber, adapterData);
		}

		public static NotificationSetDevicePushTokenRequestBuilder SetDevicePushToken(string pushToken)
		{
			return new NotificationSetDevicePushTokenRequestBuilder(pushToken);
		}
	}
}
