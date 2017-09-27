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
using Kaltura.Enums;
using Kaltura.Request;

namespace Kaltura.Types
{
	public class ExternalReceipt : PurchaseBase
	{
		#region Constants
		public const string RECEIPT_ID = "receiptId";
		public const string PAYMENT_GATEWAY_NAME = "paymentGatewayName";
		#endregion

		#region Private Fields
		private string _ReceiptId = null;
		private string _PaymentGatewayName = null;
		#endregion

		#region Properties
		public string ReceiptId
		{
			get { return _ReceiptId; }
			set 
			{ 
				_ReceiptId = value;
				OnPropertyChanged("ReceiptId");
			}
		}
		public string PaymentGatewayName
		{
			get { return _PaymentGatewayName; }
			set 
			{ 
				_PaymentGatewayName = value;
				OnPropertyChanged("PaymentGatewayName");
			}
		}
		#endregion

		#region CTor
		public ExternalReceipt()
		{
		}

		public ExternalReceipt(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "receiptId":
						this._ReceiptId = propertyNode.InnerText;
						continue;
					case "paymentGatewayName":
						this._PaymentGatewayName = propertyNode.InnerText;
						continue;
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaExternalReceipt");
			kparams.AddIfNotNull("receiptId", this._ReceiptId);
			kparams.AddIfNotNull("paymentGatewayName", this._PaymentGatewayName);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case RECEIPT_ID:
					return "ReceiptId";
				case PAYMENT_GATEWAY_NAME:
					return "PaymentGatewayName";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

