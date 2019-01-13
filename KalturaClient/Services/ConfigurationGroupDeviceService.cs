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
	public class ConfigurationGroupDeviceAddRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string CONFIGURATION_GROUP_DEVICE = "configurationGroupDevice";
		#endregion

		public ConfigurationGroupDevice ConfigurationGroupDevice
		{
			set;
			get;
		}

		public ConfigurationGroupDeviceAddRequestBuilder()
			: base("configurationgroupdevice", "add")
		{
		}

		public ConfigurationGroupDeviceAddRequestBuilder(ConfigurationGroupDevice configurationGroupDevice)
			: this()
		{
			this.ConfigurationGroupDevice = configurationGroupDevice;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("configurationGroupDevice"))
				kparams.AddIfNotNull("configurationGroupDevice", ConfigurationGroupDevice);
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

	public class ConfigurationGroupDeviceDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string UDID = "udid";
		#endregion

		public string Udid
		{
			set;
			get;
		}

		public ConfigurationGroupDeviceDeleteRequestBuilder()
			: base("configurationgroupdevice", "delete")
		{
		}

		public ConfigurationGroupDeviceDeleteRequestBuilder(string udid)
			: this()
		{
			this.Udid = udid;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("udid"))
				kparams.AddIfNotNull("udid", Udid);
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

	public class ConfigurationGroupDeviceGetRequestBuilder : RequestBuilder<ConfigurationGroupDevice>
	{
		#region Constants
		public const string UDID = "udid";
		#endregion

		public string Udid
		{
			set;
			get;
		}

		public ConfigurationGroupDeviceGetRequestBuilder()
			: base("configurationgroupdevice", "get")
		{
		}

		public ConfigurationGroupDeviceGetRequestBuilder(string udid)
			: this()
		{
			this.Udid = udid;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("udid"))
				kparams.AddIfNotNull("udid", Udid);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ConfigurationGroupDevice>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<ConfigurationGroupDevice>((IDictionary<string,object>)result);
		}
	}

	public class ConfigurationGroupDeviceListRequestBuilder : RequestBuilder<ListResponse<ConfigurationGroupDevice>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public ConfigurationGroupDeviceFilter Filter
		{
			set;
			get;
		}
		public FilterPager Pager
		{
			set;
			get;
		}

		public ConfigurationGroupDeviceListRequestBuilder()
			: base("configurationgroupdevice", "list")
		{
		}

		public ConfigurationGroupDeviceListRequestBuilder(ConfigurationGroupDeviceFilter filter, FilterPager pager)
			: this()
		{
			this.Filter = filter;
			this.Pager = pager;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("filter"))
				kparams.AddIfNotNull("filter", Filter);
			if (!isMapped("pager"))
				kparams.AddIfNotNull("pager", Pager);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ListResponse<ConfigurationGroupDevice>>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<ListResponse<ConfigurationGroupDevice>>((IDictionary<string,object>)result);
		}
	}


	public class ConfigurationGroupDeviceService
	{
		private ConfigurationGroupDeviceService()
		{
		}

		public static ConfigurationGroupDeviceAddRequestBuilder Add(ConfigurationGroupDevice configurationGroupDevice)
		{
			return new ConfigurationGroupDeviceAddRequestBuilder(configurationGroupDevice);
		}

		public static ConfigurationGroupDeviceDeleteRequestBuilder Delete(string udid)
		{
			return new ConfigurationGroupDeviceDeleteRequestBuilder(udid);
		}

		public static ConfigurationGroupDeviceGetRequestBuilder Get(string udid)
		{
			return new ConfigurationGroupDeviceGetRequestBuilder(udid);
		}

		public static ConfigurationGroupDeviceListRequestBuilder List(ConfigurationGroupDeviceFilter filter, FilterPager pager = null)
		{
			return new ConfigurationGroupDeviceListRequestBuilder(filter, pager);
		}
	}
}
