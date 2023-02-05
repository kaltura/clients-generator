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
	public class HouseholdCouponAddRequestBuilder : RequestBuilder<HouseholdCoupon>
	{
		#region Constants
		public const string OBJECT_TO_ADD = "objectToAdd";
		#endregion

		public HouseholdCoupon ObjectToAdd { get; set; }

		public HouseholdCouponAddRequestBuilder()
			: base("householdcoupon", "add")
		{
		}

		public HouseholdCouponAddRequestBuilder(HouseholdCoupon objectToAdd)
			: this()
		{
			this.ObjectToAdd = objectToAdd;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("objectToAdd"))
				kparams.AddIfNotNull("objectToAdd", ObjectToAdd);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<HouseholdCoupon>(result);
		}
	}

	public class HouseholdCouponDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public string Id { get; set; }

		public HouseholdCouponDeleteRequestBuilder()
			: base("householdcoupon", "delete")
		{
		}

		public HouseholdCouponDeleteRequestBuilder(string id)
			: this()
		{
			this.Id = id;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return null;
		}
	}

	public class HouseholdCouponListRequestBuilder : RequestBuilder<ListResponse<HouseholdCoupon>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public HouseholdCouponFilter Filter { get; set; }

		public HouseholdCouponListRequestBuilder()
			: base("householdcoupon", "list")
		{
		}

		public HouseholdCouponListRequestBuilder(HouseholdCouponFilter filter)
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
			return ObjectFactory.Create<ListResponse<HouseholdCoupon>>(result);
		}
	}


	public class HouseholdCouponService
	{
		private HouseholdCouponService()
		{
		}

		public static HouseholdCouponAddRequestBuilder Add(HouseholdCoupon objectToAdd)
		{
			return new HouseholdCouponAddRequestBuilder(objectToAdd);
		}

		public static HouseholdCouponDeleteRequestBuilder Delete(string id)
		{
			return new HouseholdCouponDeleteRequestBuilder(id);
		}

		public static HouseholdCouponListRequestBuilder List(HouseholdCouponFilter filter = null)
		{
			return new HouseholdCouponListRequestBuilder(filter);
		}
	}
}
