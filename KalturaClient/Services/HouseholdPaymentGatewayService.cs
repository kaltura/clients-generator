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
// Copyright (C) 2006-2017  Kaltura Inc.
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
	public class HouseholdPaymentGatewayDisableRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		#endregion

		public int PaymentGatewayId
		{
			set;
			get;
		}

		public HouseholdPaymentGatewayDisableRequestBuilder()
			: base("householdpaymentgateway", "disable")
		{
		}

		public HouseholdPaymentGatewayDisableRequestBuilder(int paymentGatewayId)
			: this()
		{
			this.PaymentGatewayId = paymentGatewayId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentGatewayId"))
				kparams.AddIfNotNull("paymentGatewayId", PaymentGatewayId);
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
	}

	public class HouseholdPaymentGatewayEnableRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		#endregion

		public int PaymentGatewayId
		{
			set;
			get;
		}

		public HouseholdPaymentGatewayEnableRequestBuilder()
			: base("householdpaymentgateway", "enable")
		{
		}

		public HouseholdPaymentGatewayEnableRequestBuilder(int paymentGatewayId)
			: this()
		{
			this.PaymentGatewayId = paymentGatewayId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentGatewayId"))
				kparams.AddIfNotNull("paymentGatewayId", PaymentGatewayId);
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
	}

	public class HouseholdPaymentGatewayGetChargeIDRequestBuilder : RequestBuilder<string>
	{
		#region Constants
		public const string PAYMENT_GATEWAY_EXTERNAL_ID = "paymentGatewayExternalId";
		#endregion

		public string PaymentGatewayExternalId
		{
			set;
			get;
		}

		public HouseholdPaymentGatewayGetChargeIDRequestBuilder()
			: base("householdpaymentgateway", "getChargeID")
		{
		}

		public HouseholdPaymentGatewayGetChargeIDRequestBuilder(string paymentGatewayExternalId)
			: this()
		{
			this.PaymentGatewayExternalId = paymentGatewayExternalId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentGatewayExternalId"))
				kparams.AddIfNotNull("paymentGatewayExternalId", PaymentGatewayExternalId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return result.InnerText;
		}
	}

	public class HouseholdPaymentGatewayInvokeRequestBuilder : RequestBuilder<PaymentGatewayConfiguration>
	{
		#region Constants
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string INTENT = "intent";
		public const string EXTRA_PARAMETERS = "extraParameters";
		#endregion

		public int PaymentGatewayId
		{
			set;
			get;
		}
		public string Intent
		{
			set;
			get;
		}
		public IList<KeyValue> ExtraParameters
		{
			set;
			get;
		}

		public HouseholdPaymentGatewayInvokeRequestBuilder()
			: base("householdpaymentgateway", "invoke")
		{
		}

		public HouseholdPaymentGatewayInvokeRequestBuilder(int paymentGatewayId, string intent, IList<KeyValue> extraParameters)
			: this()
		{
			this.PaymentGatewayId = paymentGatewayId;
			this.Intent = intent;
			this.ExtraParameters = extraParameters;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentGatewayId"))
				kparams.AddIfNotNull("paymentGatewayId", PaymentGatewayId);
			if (!isMapped("intent"))
				kparams.AddIfNotNull("intent", Intent);
			if (!isMapped("extraParameters"))
				kparams.AddIfNotNull("extraParameters", ExtraParameters);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<PaymentGatewayConfiguration>(result);
		}
	}

	public class HouseholdPaymentGatewayListRequestBuilder : RequestBuilder<ListResponse<HouseholdPaymentGateway>>
	{
		#region Constants
		#endregion


		public HouseholdPaymentGatewayListRequestBuilder()
			: base("householdpaymentgateway", "list")
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

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ListResponse<HouseholdPaymentGateway>>(result);
		}
	}

	public class HouseholdPaymentGatewaySetChargeIDRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PAYMENT_GATEWAY_EXTERNAL_ID = "paymentGatewayExternalId";
		public const string CHARGE_ID = "chargeId";
		#endregion

		public string PaymentGatewayExternalId
		{
			set;
			get;
		}
		public string ChargeId
		{
			set;
			get;
		}

		public HouseholdPaymentGatewaySetChargeIDRequestBuilder()
			: base("householdpaymentgateway", "setChargeID")
		{
		}

		public HouseholdPaymentGatewaySetChargeIDRequestBuilder(string paymentGatewayExternalId, string chargeId)
			: this()
		{
			this.PaymentGatewayExternalId = paymentGatewayExternalId;
			this.ChargeId = chargeId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentGatewayExternalId"))
				kparams.AddIfNotNull("paymentGatewayExternalId", PaymentGatewayExternalId);
			if (!isMapped("chargeId"))
				kparams.AddIfNotNull("chargeId", ChargeId);
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
	}


	public class HouseholdPaymentGatewayService
	{
		private HouseholdPaymentGatewayService()
		{
		}

		public static HouseholdPaymentGatewayDisableRequestBuilder Disable(int paymentGatewayId)
		{
			return new HouseholdPaymentGatewayDisableRequestBuilder(paymentGatewayId);
		}

		public static HouseholdPaymentGatewayEnableRequestBuilder Enable(int paymentGatewayId)
		{
			return new HouseholdPaymentGatewayEnableRequestBuilder(paymentGatewayId);
		}

		public static HouseholdPaymentGatewayGetChargeIDRequestBuilder GetChargeID(string paymentGatewayExternalId)
		{
			return new HouseholdPaymentGatewayGetChargeIDRequestBuilder(paymentGatewayExternalId);
		}

		public static HouseholdPaymentGatewayInvokeRequestBuilder Invoke(int paymentGatewayId, string intent, IList<KeyValue> extraParameters)
		{
			return new HouseholdPaymentGatewayInvokeRequestBuilder(paymentGatewayId, intent, extraParameters);
		}

		public static HouseholdPaymentGatewayListRequestBuilder List()
		{
			return new HouseholdPaymentGatewayListRequestBuilder();
		}

		public static HouseholdPaymentGatewaySetChargeIDRequestBuilder SetChargeID(string paymentGatewayExternalId, string chargeId)
		{
			return new HouseholdPaymentGatewaySetChargeIDRequestBuilder(paymentGatewayExternalId, chargeId);
		}
	}
}
