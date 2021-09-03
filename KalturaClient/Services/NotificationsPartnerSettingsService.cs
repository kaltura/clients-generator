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
	public class NotificationsPartnerSettingsGetRequestBuilder : RequestBuilder<NotificationsPartnerSettings>
	{
		#region Constants
		#endregion


		public NotificationsPartnerSettingsGetRequestBuilder()
			: base("notificationspartnersettings", "get")
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
			return ObjectFactory.Create<NotificationsPartnerSettings>(result);
		}
	}

	public class NotificationsPartnerSettingsUpdateRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string SETTINGS = "settings";
		#endregion

		public NotificationsPartnerSettings Settings { get; set; }

		public NotificationsPartnerSettingsUpdateRequestBuilder()
			: base("notificationspartnersettings", "update")
		{
		}

		public NotificationsPartnerSettingsUpdateRequestBuilder(NotificationsPartnerSettings settings)
			: this()
		{
			this.Settings = settings;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("settings"))
				kparams.AddIfNotNull("settings", Settings);
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


	public class NotificationsPartnerSettingsService
	{
		private NotificationsPartnerSettingsService()
		{
		}

		public static NotificationsPartnerSettingsGetRequestBuilder Get()
		{
			return new NotificationsPartnerSettingsGetRequestBuilder();
		}

		public static NotificationsPartnerSettingsUpdateRequestBuilder Update(NotificationsPartnerSettings settings)
		{
			return new NotificationsPartnerSettingsUpdateRequestBuilder(settings);
		}
	}
}
