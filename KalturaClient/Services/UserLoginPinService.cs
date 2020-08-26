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
// Copyright (C) 2006-2020  Kaltura Inc.
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
	public class UserLoginPinAddRequestBuilder : RequestBuilder<UserLoginPin>
	{
		#region Constants
		public const string SECRET = "secret";
		public const string PIN_USAGES = "pinUsages";
		public const string PIN_DURATION = "pinDuration";
		#endregion

		public string Secret { get; set; }
		public int PinUsages { get; set; }
		public int PinDuration { get; set; }

		public UserLoginPinAddRequestBuilder()
			: base("userloginpin", "add")
		{
		}

		public UserLoginPinAddRequestBuilder(string secret, int pinUsages, int pinDuration)
			: this()
		{
			this.Secret = secret;
			this.PinUsages = pinUsages;
			this.PinDuration = pinDuration;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("secret"))
				kparams.AddIfNotNull("secret", Secret);
			if (!isMapped("pinUsages"))
				kparams.AddIfNotNull("pinUsages", PinUsages);
			if (!isMapped("pinDuration"))
				kparams.AddIfNotNull("pinDuration", PinDuration);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<UserLoginPin>(result);
		}
	}

	public class UserLoginPinDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PIN_CODE = "pinCode";
		#endregion

		public string PinCode { get; set; }

		public UserLoginPinDeleteRequestBuilder()
			: base("userloginpin", "delete")
		{
		}

		public UserLoginPinDeleteRequestBuilder(string pinCode)
			: this()
		{
			this.PinCode = pinCode;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("pinCode"))
				kparams.AddIfNotNull("pinCode", PinCode);
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

	public class UserLoginPinDeleteAllRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		#endregion


		public UserLoginPinDeleteAllRequestBuilder()
			: base("userloginpin", "deleteAll")
		{
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
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

	public class UserLoginPinUpdateRequestBuilder : RequestBuilder<UserLoginPin>
	{
		#region Constants
		public const string PIN_CODE = "pinCode";
		public const string SECRET = "secret";
		public const string PIN_USAGES = "pinUsages";
		public const string PIN_DURATION = "pinDuration";
		#endregion

		public string PinCode { get; set; }
		public string Secret { get; set; }
		public int PinUsages { get; set; }
		public int PinDuration { get; set; }

		public UserLoginPinUpdateRequestBuilder()
			: base("userloginpin", "update")
		{
		}

		public UserLoginPinUpdateRequestBuilder(string pinCode, string secret, int pinUsages, int pinDuration)
			: this()
		{
			this.PinCode = pinCode;
			this.Secret = secret;
			this.PinUsages = pinUsages;
			this.PinDuration = pinDuration;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("pinCode"))
				kparams.AddIfNotNull("pinCode", PinCode);
			if (!isMapped("secret"))
				kparams.AddIfNotNull("secret", Secret);
			if (!isMapped("pinUsages"))
				kparams.AddIfNotNull("pinUsages", PinUsages);
			if (!isMapped("pinDuration"))
				kparams.AddIfNotNull("pinDuration", PinDuration);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<UserLoginPin>(result);
		}
	}


	public class UserLoginPinService
	{
		private UserLoginPinService()
		{
		}

		public static UserLoginPinAddRequestBuilder Add(string secret = null, int pinUsages = Int32.MinValue, int pinDuration = Int32.MinValue)
		{
			return new UserLoginPinAddRequestBuilder(secret, pinUsages, pinDuration);
		}

		public static UserLoginPinDeleteRequestBuilder Delete(string pinCode)
		{
			return new UserLoginPinDeleteRequestBuilder(pinCode);
		}

		public static UserLoginPinDeleteAllRequestBuilder DeleteAll()
		{
			return new UserLoginPinDeleteAllRequestBuilder();
		}

		public static UserLoginPinUpdateRequestBuilder Update(string pinCode, string secret = null, int pinUsages = Int32.MinValue, int pinDuration = Int32.MinValue)
		{
			return new UserLoginPinUpdateRequestBuilder(pinCode, secret, pinUsages, pinDuration);
		}
	}
}
