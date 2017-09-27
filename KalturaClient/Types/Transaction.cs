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
	public class Transaction : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string PAYMENT_GATEWAY_REFERENCE_ID = "paymentGatewayReferenceId";
		public const string PAYMENT_GATEWAY_RESPONSE_ID = "paymentGatewayResponseId";
		public const string STATE = "state";
		public const string FAIL_REASON_CODE = "failReasonCode";
		public const string CREATED_AT = "createdAt";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _PaymentGatewayReferenceId = null;
		private string _PaymentGatewayResponseId = null;
		private string _State = null;
		private int _FailReasonCode = Int32.MinValue;
		private int _CreatedAt = Int32.MinValue;
		#endregion

		#region Properties
		public string Id
		{
			get { return _Id; }
			set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		public string PaymentGatewayReferenceId
		{
			get { return _PaymentGatewayReferenceId; }
			set 
			{ 
				_PaymentGatewayReferenceId = value;
				OnPropertyChanged("PaymentGatewayReferenceId");
			}
		}
		public string PaymentGatewayResponseId
		{
			get { return _PaymentGatewayResponseId; }
			set 
			{ 
				_PaymentGatewayResponseId = value;
				OnPropertyChanged("PaymentGatewayResponseId");
			}
		}
		public string State
		{
			get { return _State; }
			set 
			{ 
				_State = value;
				OnPropertyChanged("State");
			}
		}
		public int FailReasonCode
		{
			get { return _FailReasonCode; }
			set 
			{ 
				_FailReasonCode = value;
				OnPropertyChanged("FailReasonCode");
			}
		}
		public int CreatedAt
		{
			get { return _CreatedAt; }
			set 
			{ 
				_CreatedAt = value;
				OnPropertyChanged("CreatedAt");
			}
		}
		#endregion

		#region CTor
		public Transaction()
		{
		}

		public Transaction(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "paymentGatewayReferenceId":
						this._PaymentGatewayReferenceId = propertyNode.InnerText;
						continue;
					case "paymentGatewayResponseId":
						this._PaymentGatewayResponseId = propertyNode.InnerText;
						continue;
					case "state":
						this._State = propertyNode.InnerText;
						continue;
					case "failReasonCode":
						this._FailReasonCode = ParseInt(propertyNode.InnerText);
						continue;
					case "createdAt":
						this._CreatedAt = ParseInt(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaTransaction");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("paymentGatewayReferenceId", this._PaymentGatewayReferenceId);
			kparams.AddIfNotNull("paymentGatewayResponseId", this._PaymentGatewayResponseId);
			kparams.AddIfNotNull("state", this._State);
			kparams.AddIfNotNull("failReasonCode", this._FailReasonCode);
			kparams.AddIfNotNull("createdAt", this._CreatedAt);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case PAYMENT_GATEWAY_REFERENCE_ID:
					return "PaymentGatewayReferenceId";
				case PAYMENT_GATEWAY_RESPONSE_ID:
					return "PaymentGatewayResponseId";
				case STATE:
					return "State";
				case FAIL_REASON_CODE:
					return "FailReasonCode";
				case CREATED_AT:
					return "CreatedAt";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

