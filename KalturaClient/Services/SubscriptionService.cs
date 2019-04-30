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
using System.IO;
using Kaltura.Request;
using Kaltura.Types;
using Kaltura.Enums;
using Newtonsoft.Json.Linq;

namespace Kaltura.Services
{
	public class SubscriptionListRequestBuilder : RequestBuilder<ListResponse<Subscription>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public SubscriptionFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public SubscriptionListRequestBuilder()
			: base("subscription", "list")
		{
		}

		public SubscriptionListRequestBuilder(SubscriptionFilter filter, FilterPager pager)
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

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ListResponse<Subscription>>(result);
		}
	}

	public class SubscriptionValidateCouponRequestBuilder : RequestBuilder<Coupon>
	{
		#region Constants
		public const string ID = "id";
		public const string CODE = "code";
		#endregion

		public int Id { get; set; }
		public string Code { get; set; }

		public SubscriptionValidateCouponRequestBuilder()
			: base("subscription", "validateCoupon")
		{
		}

		public SubscriptionValidateCouponRequestBuilder(int id, string code)
			: this()
		{
			this.Id = id;
			this.Code = code;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("code"))
				kparams.AddIfNotNull("code", Code);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Coupon>(result);
		}
	}


	public class SubscriptionService
	{
		private SubscriptionService()
		{
		}

		public static SubscriptionListRequestBuilder List(SubscriptionFilter filter = null, FilterPager pager = null)
		{
			return new SubscriptionListRequestBuilder(filter, pager);
		}

		public static SubscriptionValidateCouponRequestBuilder ValidateCoupon(int id, string code)
		{
			return new SubscriptionValidateCouponRequestBuilder(id, code);
		}
	}
}
