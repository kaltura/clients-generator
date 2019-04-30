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
	public class TransactionDowngradeRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string PURCHASE = "purchase";
		#endregion

		public Purchase Purchase { get; set; }

		public TransactionDowngradeRequestBuilder()
			: base("transaction", "downgrade")
		{
		}

		public TransactionDowngradeRequestBuilder(Purchase purchase)
			: this()
		{
			this.Purchase = purchase;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("purchase"))
				kparams.AddIfNotNull("purchase", Purchase);
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

	public class TransactionGetPurchaseSessionIdRequestBuilder : RequestBuilder<long>
	{
		#region Constants
		public const string PURCHASE_SESSION = "purchaseSession";
		#endregion

		public PurchaseSession PurchaseSession { get; set; }

		public TransactionGetPurchaseSessionIdRequestBuilder()
			: base("transaction", "getPurchaseSessionId")
		{
		}

		public TransactionGetPurchaseSessionIdRequestBuilder(PurchaseSession purchaseSession)
			: this()
		{
			this.PurchaseSession = purchaseSession;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("purchaseSession"))
				kparams.AddIfNotNull("purchaseSession", PurchaseSession);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return result.Value<long>();
		}
	}

	public class TransactionPurchaseRequestBuilder : RequestBuilder<Transaction>
	{
		#region Constants
		public const string PURCHASE = "purchase";
		#endregion

		public Purchase Purchase { get; set; }

		public TransactionPurchaseRequestBuilder()
			: base("transaction", "purchase")
		{
		}

		public TransactionPurchaseRequestBuilder(Purchase purchase)
			: this()
		{
			this.Purchase = purchase;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("purchase"))
				kparams.AddIfNotNull("purchase", Purchase);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Transaction>(result);
		}
	}

	public class TransactionSetWaiverRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string TRANSACTION_TYPE = "transactionType";
		#endregion

		public int AssetId { get; set; }
		public TransactionType TransactionType { get; set; }

		public TransactionSetWaiverRequestBuilder()
			: base("transaction", "setWaiver")
		{
		}

		public TransactionSetWaiverRequestBuilder(int assetId, TransactionType transactionType)
			: this()
		{
			this.AssetId = assetId;
			this.TransactionType = transactionType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetId"))
				kparams.AddIfNotNull("assetId", AssetId);
			if (!isMapped("transactionType"))
				kparams.AddIfNotNull("transactionType", TransactionType);
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

	public class TransactionUpdateStatusRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string EXTERNAL_TRANSACTION_ID = "externalTransactionId";
		public const string SIGNATURE = "signature";
		public const string STATUS = "status";
		#endregion

		public string PaymentGatewayId { get; set; }
		public string ExternalTransactionId { get; set; }
		public string Signature { get; set; }
		public TransactionStatus Status { get; set; }

		public TransactionUpdateStatusRequestBuilder()
			: base("transaction", "updateStatus")
		{
		}

		public TransactionUpdateStatusRequestBuilder(string paymentGatewayId, string externalTransactionId, string signature, TransactionStatus status)
			: this()
		{
			this.PaymentGatewayId = paymentGatewayId;
			this.ExternalTransactionId = externalTransactionId;
			this.Signature = signature;
			this.Status = status;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("paymentGatewayId"))
				kparams.AddIfNotNull("paymentGatewayId", PaymentGatewayId);
			if (!isMapped("externalTransactionId"))
				kparams.AddIfNotNull("externalTransactionId", ExternalTransactionId);
			if (!isMapped("signature"))
				kparams.AddIfNotNull("signature", Signature);
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
			return null;
		}
	}

	public class TransactionUpgradeRequestBuilder : RequestBuilder<Transaction>
	{
		#region Constants
		public const string PURCHASE = "purchase";
		#endregion

		public Purchase Purchase { get; set; }

		public TransactionUpgradeRequestBuilder()
			: base("transaction", "upgrade")
		{
		}

		public TransactionUpgradeRequestBuilder(Purchase purchase)
			: this()
		{
			this.Purchase = purchase;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("purchase"))
				kparams.AddIfNotNull("purchase", Purchase);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Transaction>(result);
		}
	}

	public class TransactionValidateReceiptRequestBuilder : RequestBuilder<Transaction>
	{
		#region Constants
		public const string EXTERNAL_RECEIPT = "externalReceipt";
		#endregion

		public ExternalReceipt ExternalReceipt { get; set; }

		public TransactionValidateReceiptRequestBuilder()
			: base("transaction", "validateReceipt")
		{
		}

		public TransactionValidateReceiptRequestBuilder(ExternalReceipt externalReceipt)
			: this()
		{
			this.ExternalReceipt = externalReceipt;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("externalReceipt"))
				kparams.AddIfNotNull("externalReceipt", ExternalReceipt);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Transaction>(result);
		}
	}


	public class TransactionService
	{
		private TransactionService()
		{
		}

		public static TransactionDowngradeRequestBuilder Downgrade(Purchase purchase)
		{
			return new TransactionDowngradeRequestBuilder(purchase);
		}

		public static TransactionGetPurchaseSessionIdRequestBuilder GetPurchaseSessionId(PurchaseSession purchaseSession)
		{
			return new TransactionGetPurchaseSessionIdRequestBuilder(purchaseSession);
		}

		public static TransactionPurchaseRequestBuilder Purchase(Purchase purchase)
		{
			return new TransactionPurchaseRequestBuilder(purchase);
		}

		public static TransactionSetWaiverRequestBuilder SetWaiver(int assetId, TransactionType transactionType)
		{
			return new TransactionSetWaiverRequestBuilder(assetId, transactionType);
		}

		public static TransactionUpdateStatusRequestBuilder UpdateStatus(string paymentGatewayId, string externalTransactionId, string signature, TransactionStatus status)
		{
			return new TransactionUpdateStatusRequestBuilder(paymentGatewayId, externalTransactionId, signature, status);
		}

		public static TransactionUpgradeRequestBuilder Upgrade(Purchase purchase)
		{
			return new TransactionUpgradeRequestBuilder(purchase);
		}

		public static TransactionValidateReceiptRequestBuilder ValidateReceipt(ExternalReceipt externalReceipt)
		{
			return new TransactionValidateReceiptRequestBuilder(externalReceipt);
		}
	}
}
