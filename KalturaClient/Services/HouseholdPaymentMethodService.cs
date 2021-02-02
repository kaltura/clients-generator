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
	public class HouseholdPaymentMethodAddRequestBuilder : RequestBuilder<HouseholdPaymentMethod>
	{
		#region Constants
		public const string HOUSEHOLD_PAYMENT_METHOD = "householdPaymentMethod";
		#endregion

		public HouseholdPaymentMethod HouseholdPaymentMethod { get; set; }

		public HouseholdPaymentMethodAddRequestBuilder()
			: base("householdpaymentmethod", "add")
		{
		}

		public HouseholdPaymentMethodAddRequestBuilder(HouseholdPaymentMethod householdPaymentMethod)
			: this()
		{
			this.HouseholdPaymentMethod = householdPaymentMethod;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("householdPaymentMethod"))
				kparams.AddIfNotNull("householdPaymentMethod", HouseholdPaymentMethod);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<HouseholdPaymentMethod>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class HouseholdPaymentMethodForceRemoveRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string PAYMENT_METHOD_ID = "paymentMethodId";
		#endregion

		public int PaymentGatewayId { get; set; }
		public int PaymentMethodId { get; set; }

		public HouseholdPaymentMethodForceRemoveRequestBuilder()
			: base("householdpaymentmethod", "forceRemove")
		{
		}

		public HouseholdPaymentMethodForceRemoveRequestBuilder(int paymentGatewayId, int paymentMethodId)
			: this()
		{
			this.PaymentGatewayId = paymentGatewayId;
			this.PaymentMethodId = paymentMethodId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentGatewayId"))
				kparams.AddIfNotNull("paymentGatewayId", PaymentGatewayId);
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

	public class HouseholdPaymentMethodListRequestBuilder : RequestBuilder<ListResponse<HouseholdPaymentMethod>>
	{
		#region Constants
		#endregion


		public HouseholdPaymentMethodListRequestBuilder()
			: base("householdpaymentmethod", "list")
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
			return ObjectFactory.Create<ListResponse<HouseholdPaymentMethod>>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class HouseholdPaymentMethodRemoveRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string PAYMENT_METHOD_ID = "paymentMethodId";
		#endregion

		public int PaymentGatewayId { get; set; }
		public int PaymentMethodId { get; set; }

		public HouseholdPaymentMethodRemoveRequestBuilder()
			: base("householdpaymentmethod", "remove")
		{
		}

		public HouseholdPaymentMethodRemoveRequestBuilder(int paymentGatewayId, int paymentMethodId)
			: this()
		{
			this.PaymentGatewayId = paymentGatewayId;
			this.PaymentMethodId = paymentMethodId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentGatewayId"))
				kparams.AddIfNotNull("paymentGatewayId", PaymentGatewayId);
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

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class HouseholdPaymentMethodSetAsDefaultRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string PAYMENT_METHOD_ID = "paymentMethodId";
		#endregion

		public int PaymentGatewayId { get; set; }
		public int PaymentMethodId { get; set; }

		public HouseholdPaymentMethodSetAsDefaultRequestBuilder()
			: base("householdpaymentmethod", "setAsDefault")
		{
		}

		public HouseholdPaymentMethodSetAsDefaultRequestBuilder(int paymentGatewayId, int paymentMethodId)
			: this()
		{
			this.PaymentGatewayId = paymentGatewayId;
			this.PaymentMethodId = paymentMethodId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentGatewayId"))
				kparams.AddIfNotNull("paymentGatewayId", PaymentGatewayId);
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


	public class HouseholdPaymentMethodService
	{
		private HouseholdPaymentMethodService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static HouseholdPaymentMethodAddRequestBuilder Add(HouseholdPaymentMethod householdPaymentMethod)
		{
			return new HouseholdPaymentMethodAddRequestBuilder(householdPaymentMethod);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static HouseholdPaymentMethodForceRemoveRequestBuilder ForceRemove(int paymentGatewayId, int paymentMethodId)
		{
			return new HouseholdPaymentMethodForceRemoveRequestBuilder(paymentGatewayId, paymentMethodId);
		}
// BEO-9522 csharp2 writeAction

		public static HouseholdPaymentMethodListRequestBuilder List()
		{
			return new HouseholdPaymentMethodListRequestBuilder();
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static HouseholdPaymentMethodRemoveRequestBuilder Remove(int paymentGatewayId, int paymentMethodId)
		{
			return new HouseholdPaymentMethodRemoveRequestBuilder(paymentGatewayId, paymentMethodId);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static HouseholdPaymentMethodSetAsDefaultRequestBuilder SetAsDefault(int paymentGatewayId, int paymentMethodId)
		{
			return new HouseholdPaymentMethodSetAsDefaultRequestBuilder(paymentGatewayId, paymentMethodId);
		}
	}
}
