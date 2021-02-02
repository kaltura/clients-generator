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
// BEO-9522 csharp2 before comment
	public class HouseholdDeviceAddRequestBuilder : RequestBuilder<HouseholdDevice>
	{
		#region Constants
		public const string DEVICE = "device";
		#endregion

		public HouseholdDevice Device { get; set; }

		public HouseholdDeviceAddRequestBuilder()
			: base("householddevice", "add")
		{
		}

		public HouseholdDeviceAddRequestBuilder(HouseholdDevice device)
			: this()
		{
			this.Device = device;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("device"))
				kparams.AddIfNotNull("device", Device);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<HouseholdDevice>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class HouseholdDeviceAddByPinRequestBuilder : RequestBuilder<HouseholdDevice>
	{
		#region Constants
		public const string DEVICE_NAME = "deviceName";
		public const string PIN = "pin";
		#endregion

		public string DeviceName { get; set; }
		public string Pin { get; set; }

		public HouseholdDeviceAddByPinRequestBuilder()
			: base("householddevice", "addByPin")
		{
		}

		public HouseholdDeviceAddByPinRequestBuilder(string deviceName, string pin)
			: this()
		{
			this.DeviceName = deviceName;
			this.Pin = pin;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("deviceName"))
				kparams.AddIfNotNull("deviceName", DeviceName);
			if (!isMapped("pin"))
				kparams.AddIfNotNull("pin", Pin);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<HouseholdDevice>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class HouseholdDeviceDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string UDID = "udid";
		#endregion

		public string Udid { get; set; }

		public HouseholdDeviceDeleteRequestBuilder()
			: base("householddevice", "delete")
		{
		}

		public HouseholdDeviceDeleteRequestBuilder(string udid)
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

		public override object Deserialize(JToken result)
		{
			if (result.Value<string>().Equals("1") || result.Value<string>().ToLower().Equals("true"))
				return true;
			return false;
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class HouseholdDeviceGeneratePinRequestBuilder : RequestBuilder<DevicePin>
	{
		#region Constants
		public const string UDID = "udid";
		public const string BRAND_ID = "brandId";
		#endregion

		public string Udid { get; set; }
		public int BrandId { get; set; }

		public HouseholdDeviceGeneratePinRequestBuilder()
			: base("householddevice", "generatePin")
		{
		}

		public HouseholdDeviceGeneratePinRequestBuilder(string udid, int brandId)
			: this()
		{
			this.Udid = udid;
			this.BrandId = brandId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("udid"))
				kparams.AddIfNotNull("udid", Udid);
			if (!isMapped("brandId"))
				kparams.AddIfNotNull("brandId", BrandId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<DevicePin>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class HouseholdDeviceGetRequestBuilder : RequestBuilder<HouseholdDevice>
	{
		#region Constants
		public const string UDID = "udid";
		#endregion

		public string Udid { get; set; }

		public HouseholdDeviceGetRequestBuilder()
			: base("householddevice", "get")
		{
		}

		public HouseholdDeviceGetRequestBuilder(string udid)
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

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<HouseholdDevice>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class HouseholdDeviceListRequestBuilder : RequestBuilder<ListResponse<HouseholdDevice>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public HouseholdDeviceFilter Filter { get; set; }

		public HouseholdDeviceListRequestBuilder()
			: base("householddevice", "list")
		{
		}

		public HouseholdDeviceListRequestBuilder(HouseholdDeviceFilter filter)
			: this()
		{
			this.Filter = filter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("filter"))
				kparams.AddIfNotNull("filter", Filter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ListResponse<HouseholdDevice>>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class HouseholdDeviceLoginWithPinRequestBuilder : RequestBuilder<LoginResponse>
	{
		#region Constants
		public new const string PARTNER_ID = "partnerId";
		public const string PIN = "pin";
		public const string UDID = "udid";
		#endregion

		public new int PartnerId { get; set; }
		public string Pin { get; set; }
		public string Udid { get; set; }

		public HouseholdDeviceLoginWithPinRequestBuilder()
			: base("householddevice", "loginWithPin")
		{
		}

		public HouseholdDeviceLoginWithPinRequestBuilder(int partnerId, string pin, string udid)
			: this()
		{
			this.PartnerId = partnerId;
			this.Pin = pin;
			this.Udid = udid;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("partnerId"))
				kparams.AddIfNotNull("partnerId", PartnerId);
			if (!isMapped("pin"))
				kparams.AddIfNotNull("pin", Pin);
			if (!isMapped("udid"))
				kparams.AddIfNotNull("udid", Udid);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<LoginResponse>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class HouseholdDeviceUpdateRequestBuilder : RequestBuilder<HouseholdDevice>
	{
		#region Constants
		public const string UDID = "udid";
		public const string DEVICE = "device";
		#endregion

		public string Udid { get; set; }
		public HouseholdDevice Device { get; set; }

		public HouseholdDeviceUpdateRequestBuilder()
			: base("householddevice", "update")
		{
		}

		public HouseholdDeviceUpdateRequestBuilder(string udid, HouseholdDevice device)
			: this()
		{
			this.Udid = udid;
			this.Device = device;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("udid"))
				kparams.AddIfNotNull("udid", Udid);
			if (!isMapped("device"))
				kparams.AddIfNotNull("device", Device);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<HouseholdDevice>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class HouseholdDeviceUpdateStatusRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string UDID = "udid";
		public const string STATUS = "status";
		#endregion

		public string Udid { get; set; }
		public DeviceStatus Status { get; set; }

		public HouseholdDeviceUpdateStatusRequestBuilder()
			: base("householddevice", "updateStatus")
		{
		}

		public HouseholdDeviceUpdateStatusRequestBuilder(string udid, DeviceStatus status)
			: this()
		{
			this.Udid = udid;
			this.Status = status;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("udid"))
				kparams.AddIfNotNull("udid", Udid);
			if (!isMapped("status"))
				kparams.AddIfNotNull("status", Status);
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


	public class HouseholdDeviceService
	{
		private HouseholdDeviceService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static HouseholdDeviceAddRequestBuilder Add(HouseholdDevice device)
		{
			return new HouseholdDeviceAddRequestBuilder(device);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static HouseholdDeviceAddByPinRequestBuilder AddByPin(string deviceName, string pin)
		{
			return new HouseholdDeviceAddByPinRequestBuilder(deviceName, pin);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static HouseholdDeviceDeleteRequestBuilder Delete(string udid)
		{
			return new HouseholdDeviceDeleteRequestBuilder(udid);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static HouseholdDeviceGeneratePinRequestBuilder GeneratePin(string udid, int brandId)
		{
			return new HouseholdDeviceGeneratePinRequestBuilder(udid, brandId);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static HouseholdDeviceGetRequestBuilder Get(string udid = null)
		{
			return new HouseholdDeviceGetRequestBuilder(udid);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static HouseholdDeviceListRequestBuilder List(HouseholdDeviceFilter filter = null)
		{
			return new HouseholdDeviceListRequestBuilder(filter);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static HouseholdDeviceLoginWithPinRequestBuilder LoginWithPin(int partnerId, string pin, string udid = null)
		{
			return new HouseholdDeviceLoginWithPinRequestBuilder(partnerId, pin, udid);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static HouseholdDeviceUpdateRequestBuilder Update(string udid, HouseholdDevice device)
		{
			return new HouseholdDeviceUpdateRequestBuilder(udid, device);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static HouseholdDeviceUpdateStatusRequestBuilder UpdateStatus(string udid, DeviceStatus status)
		{
			return new HouseholdDeviceUpdateStatusRequestBuilder(udid, status);
		}
	}
}
