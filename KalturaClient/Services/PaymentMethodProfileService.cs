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
	public class PaymentMethodProfileAddRequestBuilder : RequestBuilder<PaymentMethodProfile>
	{
		#region Constants
		public const string PAYMENT_METHOD = "paymentMethod";
		#endregion

		public PaymentMethodProfile PaymentMethod { get; set; }

		public PaymentMethodProfileAddRequestBuilder()
			: base("paymentmethodprofile", "add")
		{
		}

		public PaymentMethodProfileAddRequestBuilder(PaymentMethodProfile paymentMethod)
			: this()
		{
			this.PaymentMethod = paymentMethod;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentMethod"))
				kparams.AddIfNotNull("paymentMethod", PaymentMethod);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PaymentMethodProfile>(result);
		}
	}

	public class PaymentMethodProfileDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PAYMENT_METHOD_ID = "paymentMethodId";
		#endregion

		public int PaymentMethodId { get; set; }

		public PaymentMethodProfileDeleteRequestBuilder()
			: base("paymentmethodprofile", "delete")
		{
		}

		public PaymentMethodProfileDeleteRequestBuilder(int paymentMethodId)
			: this()
		{
			this.PaymentMethodId = paymentMethodId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentMethodId"))
				kparams.AddIfNotNull("paymentMethodId", PaymentMethodId);
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

	public class PaymentMethodProfileListRequestBuilder : RequestBuilder<ListResponse<PaymentMethodProfile>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public PaymentMethodProfileFilter Filter { get; set; }

		public PaymentMethodProfileListRequestBuilder()
			: base("paymentmethodprofile", "list")
		{
		}

		public PaymentMethodProfileListRequestBuilder(PaymentMethodProfileFilter filter)
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
			return ObjectFactory.Create<ListResponse<PaymentMethodProfile>>(result);
		}
	}

	public class PaymentMethodProfileUpdateRequestBuilder : RequestBuilder<PaymentMethodProfile>
	{
		#region Constants
		public const string PAYMENT_METHOD_ID = "paymentMethodId";
		public const string PAYMENT_METHOD = "paymentMethod";
		#endregion

		public int PaymentMethodId { get; set; }
		public PaymentMethodProfile PaymentMethod { get; set; }

		public PaymentMethodProfileUpdateRequestBuilder()
			: base("paymentmethodprofile", "update")
		{
		}

		public PaymentMethodProfileUpdateRequestBuilder(int paymentMethodId, PaymentMethodProfile paymentMethod)
			: this()
		{
			this.PaymentMethodId = paymentMethodId;
			this.PaymentMethod = paymentMethod;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentMethodId"))
				kparams.AddIfNotNull("paymentMethodId", PaymentMethodId);
			if (!isMapped("paymentMethod"))
				kparams.AddIfNotNull("paymentMethod", PaymentMethod);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PaymentMethodProfile>(result);
		}
	}


	public class PaymentMethodProfileService
	{
		private PaymentMethodProfileService()
		{
		}

		public static PaymentMethodProfileAddRequestBuilder Add(PaymentMethodProfile paymentMethod)
		{
			return new PaymentMethodProfileAddRequestBuilder(paymentMethod);
		}

		public static PaymentMethodProfileDeleteRequestBuilder Delete(int paymentMethodId)
		{
			return new PaymentMethodProfileDeleteRequestBuilder(paymentMethodId);
		}

		public static PaymentMethodProfileListRequestBuilder List(PaymentMethodProfileFilter filter)
		{
			return new PaymentMethodProfileListRequestBuilder(filter);
		}

		public static PaymentMethodProfileUpdateRequestBuilder Update(int paymentMethodId, PaymentMethodProfile paymentMethod)
		{
			return new PaymentMethodProfileUpdateRequestBuilder(paymentMethodId, paymentMethod);
		}
	}
}
