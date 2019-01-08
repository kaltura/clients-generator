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
using Kaltura.Enums;
using Kaltura.Request;

namespace Kaltura.Types
{
	public class HouseholdPaymentMethod : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string EXTERNAL_ID = "externalId";
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string DETAILS = "details";
		public const string IS_DEFAULT = "isDefault";
		public const string PAYMENT_METHOD_PROFILE_ID = "paymentMethodProfileId";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _ExternalId = null;
		private int _PaymentGatewayId = Int32.MinValue;
		private string _Details = null;
		private bool? _IsDefault = null;
		private int _PaymentMethodProfileId = Int32.MinValue;
		#endregion

		#region Properties
		public int Id
		{
			get { return _Id; }
		}
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		public int PaymentGatewayId
		{
			get { return _PaymentGatewayId; }
			set 
			{ 
				_PaymentGatewayId = value;
				OnPropertyChanged("PaymentGatewayId");
			}
		}
		public string Details
		{
			get { return _Details; }
			set 
			{ 
				_Details = value;
				OnPropertyChanged("Details");
			}
		}
		public bool? IsDefault
		{
			get { return _IsDefault; }
		}
		public int PaymentMethodProfileId
		{
			get { return _PaymentMethodProfileId; }
			set 
			{ 
				_PaymentMethodProfileId = value;
				OnPropertyChanged("PaymentMethodProfileId");
			}
		}
		#endregion

		#region CTor
		public HouseholdPaymentMethod()
		{
		}

		public HouseholdPaymentMethod(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "externalId":
						this._ExternalId = propertyNode.InnerText;
						continue;
					case "paymentGatewayId":
						this._PaymentGatewayId = ParseInt(propertyNode.InnerText);
						continue;
					case "details":
						this._Details = propertyNode.InnerText;
						continue;
					case "isDefault":
						this._IsDefault = ParseBool(propertyNode.InnerText);
						continue;
					case "paymentMethodProfileId":
						this._PaymentMethodProfileId = ParseInt(propertyNode.InnerText);
						continue;
				}
			}
		}

		public HouseholdPaymentMethod(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<int>("id");
			    this._ExternalId = data.TryGetValueSafe<string>("externalId");
			    this._PaymentGatewayId = data.TryGetValueSafe<int>("paymentGatewayId");
			    this._Details = data.TryGetValueSafe<string>("details");
			    this._IsDefault = data.TryGetValueSafe<bool>("isDefault");
			    this._PaymentMethodProfileId = data.TryGetValueSafe<int>("paymentMethodProfileId");
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaHouseholdPaymentMethod");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("paymentGatewayId", this._PaymentGatewayId);
			kparams.AddIfNotNull("details", this._Details);
			kparams.AddIfNotNull("isDefault", this._IsDefault);
			kparams.AddIfNotNull("paymentMethodProfileId", this._PaymentMethodProfileId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case EXTERNAL_ID:
					return "ExternalId";
				case PAYMENT_GATEWAY_ID:
					return "PaymentGatewayId";
				case DETAILS:
					return "Details";
				case IS_DEFAULT:
					return "IsDefault";
				case PAYMENT_METHOD_PROFILE_ID:
					return "PaymentMethodProfileId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

